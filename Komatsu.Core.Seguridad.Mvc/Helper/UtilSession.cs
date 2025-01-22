using System;
using System.Web;
using Komatsu.Core.Seguridad.DataContracts;

namespace Komatsu.Core.Seguridad.Mvc.Helper
{
    public class UtilSession
    {
        public static Usuario Obtener_UsuarioSession()
        {
            Usuario oUsuario = new Usuario();

            if (HttpContext.Current.Session["Usuario"] != null)
                oUsuario = (Usuario)HttpContext.Current.Session["Usuario"];

            return oUsuario;
        }
    }
}