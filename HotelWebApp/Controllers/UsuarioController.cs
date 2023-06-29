using Hotel.LogicaNegocio.Entidades;
using Hotel.LogicaNegocio.InterfacesRepositorio;
using HotelLogicaDeApp.Interfaces;
using HotelLogicaDeApp.Interfaces.IUsuario;
using HotelLogicaNegocio.DominioException;
using HotelLogicaNegocio.ValueObjects;
using HotelMVC.Models.EntidadesModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebApp.Controllers
{
    public class UsuarioController : Controller
    {
        
        private IAddU cu_Add;
        private IUpdateU cu_upd;
        private ICheckEsU cu_Check;
        public UsuarioController(IAddU cu_Add, IUpdateU cu_upd, ICheckEsU cu_Check)
        {
            this.cu_Add = cu_Add;
            this.cu_upd = cu_upd;
            this.cu_Check = cu_Check;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PreCargaUsuario()
        {
            Contra c1 = new Contra("Queondapariente123");
            Contra c3 = new Contra("Queondapariente123");
            Contra cr = new Contra("Queondapariente123"); 

            Usuario uwu = new("chihiro@gmail.com", cr);
            Usuario usr3 = new Usuario("dasas@gmaaaail.com", c3);
            Usuario usr2 = new Usuario("dasas@gmaaaail.com", c3);
            uwu.Validar();  
            usr2.Validar();
            usr3.Validar();
            try
            {
                cu_Add.Add(uwu);
                cu_Add.Add(usr2);
                cu_Add.Add(usr3);
                TempData["message"] = "Usuarios Precargados";
            }
            catch
            {
                TempData["message"] = "Ya precargo los usuarios";
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Login()
        {
            UsuarioModel us = new UsuarioModel { Email="", PassInput=""};
             return View(us);
        }
        //set login y guardar session
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UsuarioModel us)
        {
            try
            {
                Contra cr = new Contra(us.PassInput);
                us.PassInput = cr.Pass;
                Usuario usu = HotelMVC.Models.Conversiones.ConvertUsuToModel(us);
                bool esUsuario = cu_Check.CheckEs(usu.Email, usu.Password);



                if (esUsuario)
                {
                    HttpContext.Session.SetString("Email", usu.Email);
                    HttpContext.Session.SetString("Password", usu.Password.Pass);
                    return RedirectToAction("Index", "Home");
                }
                else
                {

                    throw new DominioException("Revisar datos");

                }
            }
            catch (Exception ex)
            {

                ViewBag.message = ex.Message;
                return View();

            }



        }
        public IActionResult LogOut()
        {
                HttpContext.Session.Clear();
                return RedirectToAction("Login");
    
        }
    }
}
