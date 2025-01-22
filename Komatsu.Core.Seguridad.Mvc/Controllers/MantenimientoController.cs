using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Komatsu.Core.Seguridad.Mvc.Controllers
{
    public class MantenimientoController : Controller
    {
        //
        // GET: /Mantenimiento/

        public ActionResult EntidadFiscalizacionAmbiental()
        {
            return View(); 
        }

        public ActionResult Administrados()
        {
            return View(); 
        }

        public ActionResult SupervisoresAmbientales()
        {
            return View(); 
        }
    }
}
