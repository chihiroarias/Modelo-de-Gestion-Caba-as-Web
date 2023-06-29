using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Hotel.LogicaNegocio.Entidades;
using HotelLogicaDeApp.Interfaces;
using System;
using HotelMVC.Models.EntidadesModel;
using HotelLogicaNegocio.DominioException;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Security.Policy;
using Azure;
using System.Text;

namespace HotelWebApp.Controllers
{

    public class CabaniaController : Controller
    {

        private HttpClient _cli = new HttpClient();
        private JsonSerializerOptions _opciones = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        public CabaniaController()
        {
            _cli.BaseAddress = new Uri("https://localhost:7242/api/Cabanias");
            _cli.DefaultRequestHeaders.Accept.Clear();
            _cli.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
         JsonSerializerOptions opciones = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true, ///
            WriteIndented = true ///
        };
        public IEnumerable<TipoModel> GetTipos()
        {
            try
            {
                var principio = new Uri("https://localhost:7242/api/Tipos");
                var response = _cli.GetAsync(principio).Result;
                response.EnsureSuccessStatusCode();
                var json = response.Content.ReadAsStringAsync().Result;
                var tipos = JsonSerializer.Deserialize<IEnumerable<TipoModel>>(json, opciones);
                return tipos;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }



        }
        public ActionResult Index()
        {
            try
            {

                ViewBag.Message = TempData["message"] as string;
                var tipos = GetTipos();
                ViewBag.Tipos = tipos;
                if (tipos == null || tipos.Count() == 0)
                {
                    return RedirectToAction("Create", "Tipo");
                }
                var principio = _cli.BaseAddress;
                var response = _cli.GetAsync(principio).Result;
                response.EnsureSuccessStatusCode();
                var json = response.Content.ReadAsStringAsync().Result;
                if (json == null)
                {
                    ViewBag.Error = "No se obtuvieron cabania";
                    return View();
                }


                var cabanias = JsonSerializer.Deserialize<IEnumerable<CabaniaModel>>(json, opciones);
                return View(cabanias);


            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();

            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string NameInput, int maxPersonas, bool habilitada, int tipoAsociado)
        {
            try
            {
                var tipos = GetTipos();
                ViewBag.Tipos = tipos;
                if (tipos == null || tipos.Count() == 0)
                {
                    return RedirectToAction("Create", "Tipo");
                }
                var principio = _cli.BaseAddress;
                var url = $"{_cli.BaseAddress}/GetByFiltros?nombre={NameInput}&maxPersonas={maxPersonas}&habilitado={habilitada}&tipoAsociado={tipoAsociado}";
                var json = _cli.GetAsync(url).Result;
                json.EnsureSuccessStatusCode();
                var cab = json.Content.ReadAsStringAsync().Result;
                var cabanias = JsonSerializer.Deserialize<IEnumerable<CabaniaModel>>(cab, opciones);
                return View(cabanias);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }

        }
        [HttpGet]
        public IActionResult Create()
        {

            {
                ViewBag.Tipos = GetTipos();
                return View();
            }
        }

        [HttpPost]
        public IActionResult Create(CabaniaModel cm)
        {
            try
            {
                if (cm == null)
                {
                    ViewBag.message = "No puede ser nulo";
                    return View();
                }
                var tipoJson = JsonSerializer.Serialize(cm);
                var contenido = new StringContent(tipoJson, Encoding.UTF8, "application/json");
                var response = _cli.PostAsync("", contenido).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Cabania");
                }
                else
                {
                    ViewBag.Message = "Error";
                    var tip = JsonSerializer.Deserialize<TipoModel>(response.Content.ReadAsStringAsync().Result);
                    return View(tip);
                }
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                return View();

            }

        }
        [HttpGet]
        public IActionResult Buscador()
        {
            ViewBag.message = TempData["error"] as string;
            return View();
        }
        [HttpPost]
        public IActionResult Buscar(int? idTipo, double? monto)
        {
            try
            {
                var principio = _cli.BaseAddress;
                var url = $"{_cli.BaseAddress}/GetByIdTipo?idTipo={idTipo}&monto={monto}";
                var json = _cli.GetAsync(url).Result;
                json.EnsureSuccessStatusCode(); 
                var mod = json.Content.ReadAsStringAsync().Result;
                var cabs = JsonSerializer.Deserialize<IEnumerable<CabaniaModel>>(mod, opciones);
                if(cabs == null)
                {
                    ViewBag.Message = "Ninguna cabaña cumple esta condicion";
                    return View();
                }
                return View(cabs);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();

            }
        }
    }
    
}
