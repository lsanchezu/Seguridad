using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Routing;


namespace Komatsu.Core.Seguridad.Provider.Filters
{
    public class RequiresAuthorizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool isAccessibleToUser = false;

            if ((!filterContext.HttpContext.Request.IsAuthenticated))
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new
                    {
                        controller = "Usuario",
                        action = "IniciarSesion" //Login
                    }
                ));
            }
            else
            {
                if (SiteMap.CurrentNode != null)
                {
                    isAccessibleToUser = ((SiteMap.Provider)).IsAccessibleToUser(HttpContext.Current, SiteMap.CurrentNode);
                }

                if (!isAccessibleToUser)
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new
                        {
                            controller = "Usuario",
                            action = "IniciarSesion" //Pagina de Inicio
                        }
                    ));
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
