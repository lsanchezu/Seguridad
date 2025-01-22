using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Komatsu.Core.Seguridad.DataContracts
{
    [DataContract]
    public class Auditoria
    {
        [DataMember]
        public int IdAuditoria { get; set; }

        [DataMember]
        public int IdAccion { get; set; }

        [DataMember]
        public int IdTablaReferencia { get; set; }

        [DataMember]
        public string IdTipoAccion { get; set; }

        [DataMember]
        public string IdReferencia { get; set; }

        [DataMember]
        public DateTime FechaHora { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string HostName { get; set; }

        [DataMember]
        public string IP { get; set; }

        [DataMember]
        public string Observaciones { get; set; }

        [DataMember]
        public int IdUsuario { get; set; }
    }
}
