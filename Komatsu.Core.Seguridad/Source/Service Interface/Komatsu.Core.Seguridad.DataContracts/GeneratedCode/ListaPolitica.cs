using System;
using WcfSerialization = global::System.Runtime.Serialization;

namespace Komatsu.Core.Seguridad.DataContracts
{
    [WcfSerialization::CollectionDataContract(Namespace = "http://komatsu.core.seguridad.model/2013/dckomatsuseguridad")]
    public partial class ListaPolitica : System.Collections.Generic.List<Politica>
    {
    }
}
