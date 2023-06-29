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
using HotelLogicaDeApp.Implementaciones.Cabania;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;
using HotelMVC.Models;

namespace HotelWebApp.Controllers
{
    public class MantenimientoController : Controller
    {
        private HttpClient _cli = new HttpClient();
        private JsonSerializerOptions _opciones = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        public MantenimientoController()
        {
            _cli.BaseAddress = new Uri("https://localhost:7242/api/Mantenimientos");
            _cli.DefaultRequestHeaders.Accept.Clear();
            _cli.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        JsonSerializerOptions opciones = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true, ///
            WriteIndented = true ///
        };

        public ActionResult Index(int idCab)
        {
            try
            {
                var principio = _cli.BaseAddress;
                var url = $"{_cli.BaseAddress}/GetMantxIdCab/{idCab}";
                var response = _cli.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                var json = response.Content.ReadAsStringAsync().Result;

                ViewBag.cabId = idCab;
                var mant = JsonSerializer.Deserialize<IEnumerable<MantenimientoModel>>(json, opciones);
                return View(mant);
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;

                return RedirectToAction("Index", "Cabania");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(int cabId, DateTime fechain, DateTime fechafin)
        {
            try

            {
                ViewBag.cabId = cabId;
                var principio = _cli.BaseAddress;
                //var fechaInicial = fechain.ToString("MM-dd-yyyy");
                //var fechaFinal = fechafin.ToString("MM-dd-yyyy");
                //DateTime fi = new DateTime(fechain.Year, fechain.Month, fechain.Day);
                //DateTime ff = new DateTime(fechafin.Year, fechafin.Month, fechafin.Day);
                var url = $"{_cli.BaseAddress}/GetByDate?id={cabId}&d1=" +
                    $"{fechain.ToString("yyyy-MM-dd")}" +
                    $"&d2={fechafin.ToString("yyyy-MM-dd")}";
                var response = _cli.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                var json = response.Content.ReadAsStringAsync().Result;
                if (json == null)
                {
                    ViewBag.Error = "No se obtuvieron mantenimientos";
                    return View();
                }
                var mant = JsonSerializer.Deserialize<IEnumerable<MantenimientoModel>>(json, opciones);

                return View(mant);
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
            return View();
        }
        [HttpPost]
        public IActionResult Create(MantenimientoModel m)
        {
            if (m == null)
            {
                ViewBag.message = "No puede ser nulo";
                return View();
            }
            try
            {
                var tipoJson = JsonSerializer.Serialize(m);
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
                ViewBag.Message = ex.Message;
                return View();
            }
        }
        [HttpGet]
        public ActionResult GetByCantidad()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetByCantidad(int q1, int q2)
        {
            if(q1 ==0 || q1==null || q2==0 || q2 == null) 
            {
                ViewBag.message = "No puede ser nulo";
                return View();
            }
            try
            {
                var principio = _cli.BaseAddress;
                var url = $"{_cli.BaseAddress}/GetByQpersonas?q1={q1}&q2={q2}";
                var response = _cli.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                var json = response.Content.ReadAsStringAsync().Result;
                var mants = JsonSerializer.Deserialize<IEnumerable<MantenimientoModel>>(json, opciones);
                if (mants == null)
                {
                    ViewBag.Message = "No hay mantenimientos";
                    return View();
                }
                return View(mants);
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}

