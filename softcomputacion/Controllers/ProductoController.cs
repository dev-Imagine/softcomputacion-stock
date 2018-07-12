using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using softcomputacion.Models;
using softcomputacion.Servicios;

namespace softcomputacion.Controllers
{
    public class ProductoController : Controller
    {
        // *************** Vistas
        public ActionResult Producto()
        {
            try
            {
                srvCategoria sCategoria = new srvCategoria();
                ViewBag.lstCategorias = sCategoria.ObtenerCategorias();
                srvProveedor sProveedor = new srvProveedor();
                ViewBag.lstProveedores = sProveedor.ObtenerProveedores();
                ViewBag.oProducto = new producto();
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Error", new { stError = "Se produjo un error al intentar obtener los datos del servidor." });
            }
            
        }

        // *************** Metodos
        public JsonResult ObtenerSubcategoriaDeCategoria(int idCategoria)
        {
            try
            {
                srvCategoria sCategoria = new srvCategoria();
                categoria oCategoria = sCategoria.ObtenerCategoria(idCategoria);
                string stListaOption = "<option selected value=\"0\">Subcategoria</option>";
                foreach (subcategoria oSubcategoria in oCategoria.subcategoria.ToList())
                {
                    stListaOption = stListaOption + "<option value=\"" + oSubcategoria.idSubCategoria + "\">" + oSubcategoria.nombre + "</option>";
                }
                return Json(stListaOption);
            }
            catch (Exception)
            {
                return Json("<option selected value=\"0\">Subcategoria</option>");
            }
            
        }
    }
}