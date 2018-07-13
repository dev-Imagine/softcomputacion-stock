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

        // *************** Vistas parciales
        public PartialViewResult _PopUpGuardarModificarCategoria(int idCategoria =0)
        {
            srvCategoria sCategoria = new srvCategoria();
            return PartialView(sCategoria.ObtenerCategoria(idCategoria));
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
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult GuardarModificarProducto(producto oProducto, string idProveedores)
        {
            try
            {
                srvProducto sProducto = new srvProducto();
                string [] idProveedor = idProveedores.Split(';');

                proveedorXproducto oProvedorXproducto = new proveedorXproducto();
                foreach (string idProv in idProveedor)
                {
                    if (idProv != "")
                    {
                        oProvedorXproducto = new proveedorXproducto();
                        oProvedorXproducto.idProducto = oProducto.idProducto;
                        oProvedorXproducto.idProveedor = Convert.ToInt32(idProv);
                        oProducto.proveedorXproducto.Add(oProvedorXproducto);
                    }
 
                }
                if (oProducto.stockMinimo>oProducto.stockActual)
                {
                    oProducto.idEstado = 3;
                }
                else
                {
                    if (oProducto.stockActual >= oProducto.stockMinimo + 1)
                    {
                        oProducto.idEstado = 2;
                    }
                    else
                    {
                        if (oProducto.stockActual >= oProducto.stockIdeal)
                        {
                            oProducto.idEstado = 1;
                        }
                    }
                }
                

                sProducto.GuardarModificarProducto(oProducto);
                return RedirectToAction("Producto");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Error", new { stError = "Se produjo un error al intentar guardar o modificar el producto." });
            }
        }
        public JsonResult GuardarModificarCategoria(categoria oCategoria, string[] Subcategorias)
        {
            try
            {
                oCategoria.nombre = oCategoria.nombre.ToUpper();
                subcategoria oSubcategoria;
                foreach (string stCategoria in Subcategorias)
                {
                    string[] stCat = stCategoria.Split(';');
                    oSubcategoria = new subcategoria();
                    oSubcategoria.idCategoria = oCategoria.idCategoria;
                    oSubcategoria.idSubCategoria = Convert.ToInt32(stCat[0]);
                    oSubcategoria.nombre = stCat[1].ToUpper();
                    oCategoria.subcategoria.Add(oSubcategoria);
                }
                srvCategoria sCategoria = new srvCategoria();
                oCategoria = sCategoria.GuardarModificarCategoria(oCategoria);
                return Json(oCategoria.idCategoria + ";" + oCategoria.nombre);
            }
            catch (Exception)
            {
                return Json("");
            }
        }
    }
}