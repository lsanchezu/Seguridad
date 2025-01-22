using Komatsu.Core.Seguridad.Mvc.Models;
using Komatsu.Core.Seguridad.ServicioUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;

namespace Komatsu.Core.Seguridad.Mvc.Helper
{
    public class BaseAuditoria
    {
        public static AuditoriaModel ObtenerData_Auditoria(int idTablaReferecia, string idTipoAccion)
        {
            AuditoriaModel oAuditoriaModel = new AuditoriaModel();
            oAuditoriaModel.IP = ObtenerIpUsuario();
            oAuditoriaModel.UserName = UtilSession.Obtener_UsuarioSession().NombreApellido;
            oAuditoriaModel.FechaHora = DateTime.Now;
            oAuditoriaModel.IdTablaReferencia = idTablaReferecia;
            oAuditoriaModel.IdTipoAccion = idTipoAccion;
            oAuditoriaModel.IdUsuario = UtilSession.Obtener_UsuarioSession().IdUsuario;
            return oAuditoriaModel;
        }
        
        public static string GetIpAddress()
        {
            if (HttpContext.Current.Request.Headers["CF-CONNECTING-IP"] != null)
                return HttpContext.Current.Request.Headers["CF-CONNECTING-IP"].ToString();

            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();

            return HttpContext.Current.Request.UserHostAddress;
        }

        public static string ObtenerIpUsuario()
        {
            string ip = null;
            if (HttpContext.Current != null)
            { 
                ip = string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"])
                    ? HttpContext.Current.Request.UserHostAddress
                    : HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            if (string.IsNullOrEmpty(ip) || ip.Trim() == "::1")
            { 
                var lan = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(r => r.AddressFamily == AddressFamily.InterNetwork);
                ip = lan == null ? string.Empty : lan.ToString();
            }
            return ip;
        }

        public static string ObtenerHostNameUsuario()
        {
            System.Net.IPHostEntry hostEntry = System.Net.Dns.GetHostByName(HttpContext.Current.Request.UserHostAddress);
            string r = (Dns.GetHostEntry(HttpContext.Current.Request.ServerVariables["REMOTE_HOST"]).HostName);
            return r;
        }



    }
}