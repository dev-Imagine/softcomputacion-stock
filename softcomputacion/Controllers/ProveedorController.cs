using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using softcomputacion.Models;
using softcomputacion.Servicios;

namespace softcomputacion.Controllers
{
    public class ProveedorController : Controller
    {
        [HttpGet]
        public ActionResult ListaProveedores()
        {
            try
            {
                srvProveedor sProveedor = new srvProveedor();
                ViewBag.lstProveedores = sProveedor.ObtenerProveedores();
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Error", new { stError = "Se produjo un error al intentar obtener los proveedores." });
            }
        }
        [HttpPost]
        public ActionResult ListaProveedores(string nombreEmpresa)
        {
            try
            {
                srvProveedor sProveedor = new srvProveedor();
                ViewBag.lstProveedores = sProveedor.ObtenerProveedores(nombreEmpresa);
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Error", new { stError = "Se produjo un error al intentar obtener los proveedores." });
            }
        }

        //vistas parciales

        //metodos
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult GuardarModificarProveedor(proveedor oProveedor)
        {
            try
            {
                srvProveedor sProveedor = new srvProveedor();
                sProveedor.GuardarModificarProveedor(oProveedor);
                return RedirectToAction("ListaProveedores");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Error", new { stError = "Se produjo un error al intentar guardar o modificar el proveedor." });
            }
        }
    }
}