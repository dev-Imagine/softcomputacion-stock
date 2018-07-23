﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using softcomputacion.Models;
using softcomputacion.Servicios;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
                if (oUsuario == null || oUsuario.idTipoUsuario != 2)
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
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Producto(int idProducto)
        {
            try
            {
                usuario oUsuario = (usuario)Session["Usuario"];
                if (oUsuario == null || oUsuario.idTipoUsuario != 2)
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
                ViewBag.ValorUSD = GetValorUsd();
                PagedList<producto> model = new PagedList<producto>(lstProductos.ToList(), nroPagina, tamañoPagina);
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Error", new { stError = "Se produjo un error al intentar obtener los datos del servidor." });
            }

        }
        [HttpPost]
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
                ViewBag.ValorUSD = GetValorUsd();
                PagedList<producto> model = new PagedList<producto>(lstProductos.ToList(), 1, 10);
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Error", new { stError = "Se produjo un error al intentar obtener los datos del servidor." });
            }

        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult VistaProducto(int idProducto=0)
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
                producto oProducto = sProducto.ObtenerProducto(idProducto);
                if (oProducto == null || oProducto.idProducto == 0)
                {
                    throw new Exception();
                }
                return View(oProducto);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Error", new { stError = "El producto solicitado no se ha encontrado." });
            }

        }
        [HttpPost]
        public ViewResult ImprimirCodigoBarra(int idProducto, int cantidad)
        {
            ViewBag.idProducto = idProducto;
            ViewBag.cantidad = cantidad;
            return View();
        }

        // *************** Vistas parciales
        [HttpPost]
        public PartialViewResult _PopUpGuardarModificarCategoria(int idCategoria =0)
        {
            srvCategoria sCategoria = new srvCategoria();
            ViewBag.lstCategorias = sCategoria.ObtenerCategorias();
            return PartialView(sCategoria.ObtenerCategoria(idCategoria));
        }

        [HttpPost]
        public PartialViewResult _PopUpImprimirCodigoBarra(int idProducto)
        {
            return PartialView(idProducto);
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
                if (oUsuario == null || oUsuario.idTipoUsuario != 2)
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
                usuario oUsuario = (usuario)Session["Usuario"];
                if (oUsuario == null || oUsuario.idTipoUsuario != 2)
                {
                    throw new Exception();
                }
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
                usuario oUsuario = (usuario)Session["Usuario"];
                if (oUsuario == null || oUsuario.idTipoUsuario != 2)
                {
                    throw new Exception();
                }
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
                usuario oUsuario = (usuario)Session["Usuario"];
                if (oUsuario == null || oUsuario.idTipoUsuario != 2)
                {
                    throw new Exception();
                }
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
        [HttpPost]
        public JsonResult DescontarStock(int idProducto, int cantidad)
        {
            try
            {
                string stRespuesta = "";
                srvProducto sProducto = new srvProducto();
                producto oProducto = sProducto.ObtenerProducto(idProducto);
                if (oProducto.stockActual >= cantidad)
                {
                    oProducto.stockActual = oProducto.stockActual - cantidad;
                    oProducto = sProducto.GuardarModificarProducto(oProducto);
                    stRespuesta = oProducto.stockActual + ";" + oProducto.estado.nombre + ";" + srvEstado.ObtenerColorEstado(oProducto.idEstado);
                    return Json(stRespuesta);
                }
                else
                {
                    return Json("");
                }
            }
            catch (Exception)
            {
                return Json("");
            }

        }
        [OutputCache(Duration = 3600, Location =System.Web.UI.OutputCacheLocation.Server)]
        public double GetValorUsd()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://ws.geeklab.com.ar");
                var responseTask = client.GetAsync("dolar/get-dolar-json.php");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    //  {\"libre\":\"28.41\",\"blue\":\"28.65\"}
                    string stResult = readTask.Result.Substring(10, 5).Replace(".",",");
                    return Convert.ToDouble(stResult);
                }
                else //web api sent error response 
                {
                    return 0;
                }
            }
        }

        
    }
}