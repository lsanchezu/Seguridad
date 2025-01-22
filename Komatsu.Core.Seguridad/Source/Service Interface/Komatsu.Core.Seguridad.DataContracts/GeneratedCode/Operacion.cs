
using System;
using WcfSerialization = global::System.Runtime.Serialization;

namespace Komatsu.Core.Seguridad.DataContracts
{
    [WcfSerialization::DataContract(Namespace = "http://komatsu.core.seguridad.model/2013/dckomatsuseguridad", Name = "Operacion")]
    public class Operacion
    {
        [WcfSerialization::DataMember(Name = "IdOperacion", IsRequired = false, Order = 0)]
        public int IdOperacion { get; set; }

        [WcfSerialization::DataMember(Name = "Descripcion", IsRequired = false, Order = 1)]
        public string Descripcion { get; set; }

        [WcfSerialization::DataMember(Name = "Estado", IsRequired = false, Order = 2)]
        public Estado Estado { get; set; }
    }
}
