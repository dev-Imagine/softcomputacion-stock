using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace softcomputacion.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
       
        public ActionResult ListarUsuario()
        {
            return View();
        }
    }
}