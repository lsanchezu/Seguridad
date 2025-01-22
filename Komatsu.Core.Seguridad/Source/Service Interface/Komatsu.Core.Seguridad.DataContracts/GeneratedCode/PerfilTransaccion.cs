using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Komatsu.Core.Seguridad.DataContracts
{
    [DataContract]
    public class PerfilTransaccion
    {
        [DataMember]
        public ListaUsuarioRol ListaUsuarioRol { get; set; }
        [DataMember]
        public int Option { get; set; }

        [DataMember]
        public int IdRol { get; set; }

        [DataMember]
        public Auditoria Auditoria { get; set; }
        [DataMember]
        public string Respuesta { get; set; }
    }
}
