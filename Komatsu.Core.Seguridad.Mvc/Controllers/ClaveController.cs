using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Komatsu.Core.Seguridad.Mvc.Models.ViewModel;
using Komatsu.Core.Seguridad.Mvc.Helpers;
using Komatsu.Core.Seguridad.Common;
using Komatsu.Core.Seguridad.Mvc.ServiceController;
using Komatsu.Core.Seguridad.ServicioUsuario;
using Komatsu.Core.Seguridad.ServicioPerfil;
using Komatsu.Core.Seguridad.Mvc.Helper;
using Komatsu.Core.Seguridad.Provider.Filters;
using Komatsu.Core.Seguridad.Validation;

namespace Komatsu.Core.Seguridad.Mvc.Controllers
{
    public class ClaveController : Controller
    {
        //
        // GET: /Clave/

        public ActionResult Cambiar()
        {
            UsuarioViewModel oUsuarioViewModel = new UsuarioViewModel();
            oUsuarioViewModel.ListaPaginaAccion = ServicioPerfilController.Instancia.ObtenerPermisoPerfilPorGrupo(UtilSession.Obtener_UsuarioSession().IdUsuario, Constantes.CambiarClave);
            oUsuarioViewModel.ListaPolitica = ServicioUsuarioController.Instancia.Listar_Politica();
            return View(oUsuarioViewModel);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult ValidarContrasena(string Contrasena)
        {
            string Mensaje = "";

            int resultado = ServicioUsuarioController.Instancia.ValidarContrasena(UtilSession.Obtener_UsuarioSession().IdUsuario, Contrasena);

            switch (resultado)
            {
                case 1:
                    Mensaje = Validation.Security.Security.Registro_Correcto;
                    break;
                case -1:
                    Mensaje = Validation.Security.Security.Registro_Incorrecto;
                    break;
            }

            Mensaje = resultado + "/" + Mensaje;
            return Content(Mensaje);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult CambiarClave(string Contrasena)
        {
            string Mensaje = "";
             
            int resultado = ServicioUsuarioController.Instancia.UpdatContrasena(UtilSession.Obtener_UsuarioSession().IdUsuario, Contrasena);

            switch (resultado)
            {
                case 1:
                    Mensaje = "Contraseña modificada correctamente";
                    break;
                case -1:
                    Mensaje = "Se produjo un error al modificar la contraseña";
                    break;
                case 2:
                    Mensaje = "Se alcanzo el tope máximo de duplicidad de contraseñas permitidas";
                    break;
            }

            Mensaje = resultado + "/" + Mensaje;
            return Content(Mensaje);
        }

    }
}
