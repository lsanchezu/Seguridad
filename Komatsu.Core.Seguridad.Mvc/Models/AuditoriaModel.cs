using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Komatsu.Core.Seguridad.Mvc.Models
{
    public class AuditoriaModel
    {
        public Int32 IdUsuario { get; set; }
        public Int32 IdAuditoria { get; set; }
        public Int32 IdTablaReferencia { get; set; }
        public String IdTipoAccion { get; set; }
        public String IdReferencia { get; set; }
        public DateTime FechaHora { get; set; }
        public String UserName { get; set; }
        public String HostName { get; set; }
        public String IP { get; set; }
        public String Observaciones { get; set; }
    }
}