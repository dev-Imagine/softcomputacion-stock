using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using softcomputacion.Models;
using softcomputacion.Servicios;

namespace softcomputacion.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(bool DatosIncorrectos = false, string dni ="", string contraseña ="")
        {
            ViewBag.DatosIncorrectos = DatosIncorrectos;
            ViewBag.dni = dni;
            ViewBag.contraseña = contraseña;
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Ingresar(string dni, string contraseña)
        {
            srvUsuario sUsuario = new srvUsuario();
            usuario oUsuario = sUsuario.ObtenerUsuario(dni, contraseña);
            if (oUsuario != null)
            {
                Session["Usuario"] = oUsuario;
                return RedirectToAction("ListarProducto", "Producto");
            }
            Session["Usuario"] = null;
            return RedirectToAction("Index", new { DatosIncorrectos = true, dni = dni, contraseña = contraseña });
        }
        public ActionResult CerrarSession()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}