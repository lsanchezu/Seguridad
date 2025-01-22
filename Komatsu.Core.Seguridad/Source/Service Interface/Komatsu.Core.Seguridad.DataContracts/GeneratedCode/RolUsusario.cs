using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Komatsu.Core.Seguridad.DataContracts.GeneratedCode
{
   [DataContract]
   public  class RolUsusario
    {
        [DataMember]
        public string Empresa { get; set; }
        [DataMember]
        public string Aplicacion { get; set; }
        [DataMember]
        public string Rol { get; set; }
        [DataMember]
        public Int32 Estado { get; set; }
        [DataMember]
        public DateTime FechaAsignacion { get; set; }
    }
    [CollectionDataContract]
    public class ListaRolUsusario : List<RolUsusario>
    {

    }
}
