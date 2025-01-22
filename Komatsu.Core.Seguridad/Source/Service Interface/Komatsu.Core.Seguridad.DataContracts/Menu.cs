using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Komatsu.Core.Seguridad.DataContracts
{
    [DataContract]
    public class Menu
    {
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Icono { get; set; }
        [DataMember]
        public ListaMenuItem ListaMenuItem { get; set; }
    }

    [CollectionDataContract]
    public class ListaMenu : List<Menu> { }
}
