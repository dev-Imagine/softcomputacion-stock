using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace softcomputacion.Controllers
{
    public class ProveedorController : Controller
    {
        // GET: Proveedores
        public ActionResult ListaProveedores()
        {
            return View();
        }
    }
}