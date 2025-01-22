using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Komatsu.Core.Seguridad.Mvc.Controllers
{
    public class ConsultaController : Controller
    {
        //
        // GET: /Consulta/

        public ActionResult ConsultaActividades()
        {
            return View(); 
        }

        public ActionResult DenunciasAmbientales() 
        {
            return View(); 
        }

    }
}
