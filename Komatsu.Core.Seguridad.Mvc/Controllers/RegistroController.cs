using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Komatsu.Core.Seguridad.Mvc.Controllers
{
    public class RegistroController : Controller
    {
        //
        // GET: /Registro/

        public ActionResult ActividadesSupervision()
        {
            return View(); 
        }
        
        public ActionResult MatrizSupervision()
        {
            return View(); 
        }

        public ActionResult EjecucionActividad()
        {
            return View();  
        }
    }
}
