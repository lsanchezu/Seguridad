using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Komatsu.Core.Seguridad.Provider.Filters;

namespace Komatsu.Core.Seguridad.Mvc.Controllers
{
    public class ReporteController : Controller
    {

        //[RequiresAuthenticationAjaxAttribute]

        public ActionResult ReporteEmpresa()
        {
            return View();
        }

        public ActionResult ReporteAplicacion()
        {
            return View();
        }

        public ActionResult ReporteRoles()
        {
            return View();
        }

        public ActionResult ReporteUsuarios()
        {
            return View();
        }
        //[RequiresAuthenticationAjaxAttribute]
        public ActionResult VerReporteEmpresa()
        {
            return PartialView();
        }

        public ActionResult VerReporteAplicacion()
        {
            return PartialView();
        }

        public ActionResult VerReporteRoles()
        {
            return PartialView();
        }

        public ActionResult VerReporteUsuarios()
        {
            return PartialView();
        }
    }
}