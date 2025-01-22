using System;
using WcfSerialization = global::System.Runtime.Serialization;

namespace Komatsu.Core.Seguridad.DataContracts
{
    [WcfSerialization::CollectionDataContract(Namespace = "http://komatsu.core.seguridad.model/2013/dckomatsuseguridad")]
    public class ListaOperacion : System.Collections.Generic.List<Operacion>
    {
    }
}
