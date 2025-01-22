using System;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Komatsu.Core.Seguridad.Mvc.Controllers.Base;
using Komatsu.Core.Seguridad.Mvc.Models;
using Komatsu.Core.Seguridad.Mvc.Models.ViewModel;
using MvcSiteMapProvider;
using System.Net;
using System.Net.NetworkInformation;
using Komatsu.Core.Seguridad.Mvc.ServiceController;
using Komatsu.Core.Seguridad.ServicioUsuario;
using Komatsu.Core.Seguridad.Mvc.Helper;

namespace Komatsu.Core.Seguridad.Mvc.Controllers
{
    public class CuentaController : Controller
    {
        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }
            base.Initialize(requestContext);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public ActionResult IniciarSesion()
        {
            UsuarioViewModel VM = new UsuarioViewModel();
            String usuarioSistema = WindowsIdentity.GetCurrent().Name.ToString();

            VM.UsuarioSistema = usuarioSistema.Contains("\\") ? (usuarioSistema.Split(new string[] { "\\" }, StringSplitOptions.None)[1].ToString()) : (null);
            return View(VM);
        }

        [HttpPost]
        public ActionResult IniciarSesion(string usuario, string clave, string returnUrl)
        {
            bool validar = Membership.Provider.ValidateUser(usuario, clave);
            int Modo = 10;
            try {
                if (Session.Keys.Count > 0 && Session["Modo"] != null)
                {
                    Modo = (int)Session["Modo"];
                }
            }
            catch {
                Modo = 10;
            }
            
            int Intentos = 0;
            int Cont = 0;
            int Bloqueo = 0;

            UsuarioViewModel viewModel = new UsuarioViewModel();
            viewModel.ListaPolitica = ServicioUsuarioController.Instancia.Listar_Politica();

            foreach (var item in viewModel.ListaPolitica)
            {
                Intentos = item.IDNroMaximoIntentos;
            }

            if (validar)
            {
                FormsService.SignIn(usuario, false);
                Roles.GetRolesForUser(usuario);
                //new DefaultSiteMapProvider().Refresh();

                string IP = "";
                int iLog = 0;
                int IdUsu = (int)Session["IdUsuario"];

                string strHostName = string.Empty;
                strHostName = Dns.GetHostName();
                //strHostName = BaseAuditoria.ObtenerHostNameUsuario();
                IP = BaseAuditoria.ObtenerIpUsuario();

                //iLog = ServicioUsuarioController.Instancia.InsertarLogAcceso(strHostName, IP, IdUsu);

                int Caduco = (int)Session["Caduco"];

                if (Caduco == 1)
                {
                    try
                    {
                        Session["Intentos"] = null;
                        Session["Usu"] = null;
                    }
                    catch
                    {
                        Session["Intentos"] = null;
                        Session["Usu"] = null;
                    }
                                        
                    return Content("3", "3");
                }
                else {
                    try
                    {
                        Session["Intentos"] = null;
                        Session["Usu"] = null;
                    }
                    catch
                    {
                        Session["Intentos"] = null;
                        Session["Usu"] = null;
                    }
                    return Content("1", "1");
                }
                
            }else if (validar == false && Modo==2){
                try {

                    if (usuario != (string)Session["Usu"]) {
                        Session["Usu"] = usuario;
                        Session["Intentos"] = Cont;
                    }

                    if (Session["Intentos"] == null && Session["Usu"]==null)
                    {
                        Session["Intentos"] = (int)Session["Intentos"] + 1;
                    }
                    Session["Intentos"] = (int)Session["Intentos"] + 1;
                    if (Intentos <= (int)Session["Intentos"] && usuario == (string)Session["Usu"])
                    {
                        Bloqueo = ServicioUsuarioController.Instancia.BloquearUsuario(usuario);
                        return Content("4", "4");
                    }
                    else {
                        return Content("2", "2");
                    }
                }
                catch {
                    Session["Intentos"] = Cont + 1;
                    Session["Usu"] = usuario;
                    return Content("2", "2");
                }
            }
            else if (validar == false && Modo == 5)
            {
                return Content("5", "5");
                                
            }else{
                Session["Intentos"] = null;
                Session["Usu"] = null;
                return Content("2", "2");
            }
        }


        public ActionResult CerrarSesion()
        {
            FormsService.SignOut();
            Session.RemoveAll();
            return RedirectToAction("IniciarSesion", "Cuenta");
        }
    }
}
