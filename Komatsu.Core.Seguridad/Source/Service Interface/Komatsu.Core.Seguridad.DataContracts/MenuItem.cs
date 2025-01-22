using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Komatsu.Core.Seguridad.DataContracts
{
    [DataContract]
    public class MenuItem
    {
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public string Icono { get; set; }
    }

    [CollectionDataContract]
    public class ListaMenuItem : List<MenuItem> { }
}
