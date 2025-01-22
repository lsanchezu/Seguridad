using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Routing;
using System.Diagnostics;
using System.Web.Security;

namespace Komatsu.Core.Seguridad.Provider.Filters
{
    public class RequiresAuthenticationAjaxAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                if ((!filterContext.HttpContext.Request.IsAuthenticated) || (HttpContext.Current.Session["NombreUsuario"] == null))
                {
                    JavaScriptResult result = new JavaScriptResult()
                    {
                        Script = "window.location='" + "/Usuario/IniciarSesion" + "';"
                    };
                    filterContext.Result = result;
                }
            }
            else
            {
                if (!filterContext.HttpContext.Request.IsAuthenticated || (HttpContext.Current.Session["NombreUsuario"] == null))
                {

                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new
                        {
                            controller = "Usuario",
                            action = "IniciarSesion" //Login
                        }
                    ));
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
