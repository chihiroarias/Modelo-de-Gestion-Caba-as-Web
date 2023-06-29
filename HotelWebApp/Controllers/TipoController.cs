
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
using HotelMVC.Models.EntidadesModel;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System;
using System.Text.Encodings.Web;
using Azure;
using HotelLogicaDeApp.Implementaciones.Cabania;
using System.Net;

namespace HotelWebApp.Controllers
{

    public class TipoController : Controller
    {

        private HttpClient _cli = new HttpClient();
        private JsonSerializerOptions _opciones = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        public TipoController()
        {
            _cli.BaseAddress = new Uri("https://localhost:7242/api/Tipos");
            _cli.DefaultRequestHeaders.Accept.Clear();
            _cli.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        JsonSerializerOptions opciones = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true, ///
            WriteIndented = true ///
        };




        public ActionResult Index()
        ///<summary>
        /// Método auxiliar para retornar el json obtenido a partir de la api.
        /// </summary>
        /// <param name="uri">Url donde está ubicado el recurso de la API a consumir</param>
        /// <returns>Un string con el contenido del body obtenido en la respuesta; habitualmente contendrá la info de un objeto o de una lista de objetos y se formateará como Json</returns>

        {
            try
            {
                ViewBag.Message = TempData["message"] as string;

                var principio = _cli.BaseAddress;
                string json = GetRespuesta(principio);
                if (json == null)
                {
                    ViewBag.Error = "No se obtuvieron tipos";
                    return View();
                }

                var tipos = JsonSerializer.Deserialize<IEnumerable<TipoModel>>(json, opciones);
                return View(tipos);


            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();

            }
        }
        // GET: Tipo/Create
        public ActionResult Create()
        {

            var tm = new TipoModel();
            return View(tm);


        }

        //POST tipo/create
        [HttpPost]
        public ActionResult Create(TipoModel tm)
        {
            try
            {
                if (tm == null)
                {
                    ViewBag.message = "No puede ser nulo";
                    return View();
                }
                var tipoJson = JsonSerializer.Serialize(tm);
                var contenido = new StringContent(tipoJson, Encoding.UTF8, "application/json");
                var response = _cli.PostAsync("", contenido).Result;
                //var response = _cli.PostAsync($"{_cli.BaseAddress}/Tipos/Nuevo", contenido);
                //response.Wait();
                //response.Result.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Tipo");
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
        //get buscar por nombre
        public ActionResult Buscarlo()
        {

            ViewBag.message = TempData["error"] as string;
            return View("Buscar");

        }

        private string? GetRespuesta(Uri uri)
        {
            try
            {
                var response = _cli.GetAsync(uri).Result;
                response.EnsureSuccessStatusCode(); //Espera hasta obtener la respuesta; si no lo logra lanza una excepción

                //Leer el json que viene incluido en el contenido (body) 
                var json = response.Content.ReadAsStringAsync().Result;
                return json;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Buscar(string NombreInput)
        {

            try
            {

                var principio = _cli.BaseAddress;
                var url = $"{_cli.BaseAddress}/{NombreInput}";
                var json = _cli.GetAsync(url).Result;

                json.EnsureSuccessStatusCode(); //Espera hasta obtener la respuesta; si no lo logra lanza una excepcion
                var mod = json.Content.ReadAsStringAsync().Result;
                var buscado = JsonSerializer.Deserialize<TipoModel>(mod, opciones);


                if (buscado == null)
                {
                    ViewBag.Error = "No hay tipo con ese nombre";
                    return RedirectToAction("Buscarlo", "Tipo");
                }



                return RedirectToAction("Details", "Tipo", buscado);


            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();

            }


        }

        public ActionResult Details(TipoModel buscado)
        {
            return View(buscado);

        }



        public ActionResult Delete(string NombreInput)
        {
            try
            {
                if (NombreInput == null)
                {
                    TempData["message"] = "error al borrar";
                    return RedirectToAction("Index", "Tipo");

                }
                var principio = _cli.BaseAddress;
                var url = $"{_cli.BaseAddress}/{NombreInput}";
                var json = _cli.DeleteAsync(url).Result;
                if (json.StatusCode == HttpStatusCode.NoContent)
                {
                    TempData["message"] = "Se elimino el tipo";
                    return RedirectToAction("Index", "Tipo");
                }


                return RedirectToAction("Index", "Tipo");



            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();

            }
        }




        //get/tipo/edit/nombre
        public ActionResult Edit(string nombre)
        {
            var principio = _cli.BaseAddress;
            var url = $"{_cli.BaseAddress}/{nombre}";
            var json = _cli.GetAsync(url).Result;

            json.EnsureSuccessStatusCode(); //Espera hasta obtener la respuesta; si no lo logra lanza una excepcion
            var mod = json.Content.ReadAsStringAsync().Result;
            var buscado = JsonSerializer.Deserialize<TipoModel>(mod, opciones);



            return View(buscado);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TipoModel tipo)
        {
            try
            {
                if (tipo != null)
                {
                    var tipoJson = JsonSerializer.Serialize(tipo);

                    var url = new StringContent(tipoJson, Encoding.UTF8, "application/json");
                    var json = _cli.PutAsync("", url).Result;
                    if (json.IsSuccessStatusCode)
                    {

                        TempData["message"] = "Se edito el tipo";
                        return RedirectToAction("Index", "Tipo");


                    }
                }
                ViewBag.Message = "El tipo es nullo";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Los campos deben seguir los mismos requisitos que al crear {ex.Message}";

                return View();
            }
        }
    }
}
