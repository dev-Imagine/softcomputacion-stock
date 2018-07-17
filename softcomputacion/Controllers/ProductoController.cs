using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
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
                usuario oUsuario = (usuario)Session["Usuario"];
                if (oUsuario == null)
                {
                    Session.Clear();
                    return RedirectToAction("Index","Home");
                }
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
        [HttpPost]
        public ActionResult Producto(int idProducto)
        {
            try
            {
                usuario oUsuario = (usuario)Session["Usuario"];
                if (oUsuario == null)
                {
                    Session.Clear();
                    return RedirectToAction("Index", "Home");
                }
                srvCategoria sCategoria = new srvCategoria();
                ViewBag.lstCategorias = sCategoria.ObtenerCategorias();
                srvProveedor sProveedor = new srvProveedor();
                ViewBag.lstProveedores = sProveedor.ObtenerProveedores();
                srvProducto sProducto = new srvProducto();
                ViewBag.oProducto = sProducto.ObtenerProducto(idProducto);
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Error", new { stError = "Se produjo un error al intentar obtener los datos del servidor." });
            }

        }

        public ActionResult ListarProducto(int nroPagina = 1, int tamañoPagina = 10)
        {
            try
            {
                usuario oUsuario = (usuario)Session["Usuario"];
                if (oUsuario == null)
                {
                    Session.Clear();
                    return RedirectToAction("Index", "Home");
                }
                srvEstado sEstado = new srvEstado();
                srvProducto sProducto = new srvProducto();
                srvCategoria sCategoria = new srvCategoria();
                List<producto> lstProductos = new List<producto>();
                lstProductos = sProducto.ObtenerProductos();
                ViewBag.lstCategorias = sCategoria.ObtenerCategorias();
                ViewBag.lstEstados = sEstado.ObtenerEstados();
                ViewBag.filtros = ";;;";
                PagedList<producto> model = new PagedList<producto>(lstProductos.ToList(), nroPagina, tamañoPagina);
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Error", new { stError = "Se produjo un error al intentar obtener los datos del servidor." });
            }

        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ListarProducto(string nombreProducto = "", int idEstado = 0, int idCategoria = 0, int idSubCategoria = 0)
        {
            try
            {
                usuario oUsuario = (usuario)Session["Usuario"];
                if (oUsuario == null)
                {
                    Session.Clear();
                    return RedirectToAction("Index", "Home");
                }
                srvEstado sEstado = new srvEstado();
                srvProducto sProducto = new srvProducto();
                srvCategoria sCategoria = new srvCategoria();
                List<producto> lstProductos = new List<producto>();
                lstProductos = sProducto.ObtenerProductos(nombreProducto, idCategoria, idSubCategoria, idEstado);
                ViewBag.lstCategorias = sCategoria.ObtenerCategorias();
                ViewBag.lstEstados = sEstado.ObtenerEstados();
                ViewBag.filtros = Convert.ToString(nombreProducto + ";" + idCategoria + ";" + idSubCategoria + ";" + idEstado);
                PagedList<producto> model = new PagedList<producto>(lstProductos.ToList(), 1, 10);
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Error", new { stError = "Se produjo un error al intentar obtener los datos del servidor." });
            }

        }


        // *************** Vistas parciales
        [HttpPost]
        public PartialViewResult _PopUpGuardarModificarCategoria(int idCategoria =0)
        {
            srvCategoria sCategoria = new srvCategoria();
            ViewBag.lstCategorias = sCategoria.ObtenerCategorias();
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
                usuario oUsuario = (usuario)Session["Usuario"];
                if (oUsuario == null)
                {
                    Session.Clear();
                    return RedirectToAction("Index", "Home");
                }
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
                if (oProducto.stockMinimo>=oProducto.stockActual)
                {
                    oProducto.idEstado = 3;
                }
                else
                {
                    //if (oProducto.stockActual >= oProducto.stockMinimo + 2)
                    if (oProducto.stockActual <= oProducto.stockMinimo + 2)
                    {
                        oProducto.idEstado = 2;
                    }
                    else
                    {
                        //if (oProducto.stockActual >= oProducto.stockIdeal)
                        //{
                            oProducto.idEstado = 1;
                        //}
                    }
                }
                

                sProducto.GuardarModificarProducto(oProducto);
                return RedirectToAction("ListarProducto");
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
        public JsonResult EliminarSubCategoria(int id_Subcategoria)
        {
            try
            {
                srvCategoria sCategoria = new srvCategoria();
                sCategoria.EliminarSubcategoria(id_Subcategoria);
                return Json("True");
            }
            catch (Exception)
            {
                return Json("No se ha podido eliminar la subcategoría. Verifique que no tenga productos asignados.");
            }
        }
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult ActualizarPrecios(int costo, int gremio, int contado, int lista, int idProducto)
        {
            try
            {
                srvProducto sProducto = new srvProducto();
                producto oProducto = new producto();
                oProducto = sProducto.ObtenerProducto(idProducto);
                oProducto.precioCosto = costo;
                oProducto.precioGremio = gremio;
                oProducto.precioContado = contado;
                oProducto.precioLista = lista;
                sProducto.GuardarModificarProducto(oProducto);
                return Json("True");
            }
            catch (Exception)
            {
                return Json("False");
            }

        }
        
    }
}