﻿using System.Web.Mvc;
using Komatsu.Core.Seguridad.Mvc.Helper;

namespace Komatsu.Core.Seguridad.Mvc.Controllers.Base
{
    [NoCacheAttribute]
    //[Authorize]
    public class BaseController : Controller
    {
        public RedirectResult RedirectJSEnviarPagina(string actiondestino, string controllerdestino)
        {
            return Redirect(@Url.Action(actiondestino, controllerdestino));
        }

        protected override RedirectResult Redirect(string url)
        {
            return new AjaxAwareRedirectResult(url);
        }

        public class AjaxAwareRedirectResult : RedirectResult
        {

            public AjaxAwareRedirectResult(string url) : base(url)
            {

            }

            public override void ExecuteResult(ControllerContext context)
            {
                if (context.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                    string destinationUrl = UrlHelper.GenerateContentUrl(Url, context.HttpContext);

                    JavaScriptResult result = new JavaScriptResult()
                    {
                        Script = "window.location='" + destinationUrl + "';"
                    };
                    result.ExecuteResult(context);
                }
                else
                    base.ExecuteResult(context);
            }
        }
    }
}
