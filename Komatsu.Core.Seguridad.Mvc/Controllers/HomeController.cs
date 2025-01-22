using Komatsu.Core.Seguridad.Provider.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Komatsu.Core.Seguridad.Mvc.Controllers
{
    public class HomeController : Controller
    {

        [RequiresAuthenticationAttribute]
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Controles() 
        {
            return View();
        }
    }
}
