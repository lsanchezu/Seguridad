using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Komatsu.Core.Seguridad.DataContracts.GeneratedCode
{
    public class Cargo
    {
        private int idCargo;
        private string nombreCargo;
        private string descripcion;
        private int estado;

        [DataMember]
        public int IdCargo
        {
            get { return idCargo; }
            set { idCargo = value; }
        }

        [DataMember]
        public string NombreCargo
        {
            get { return nombreCargo; }
            set { nombreCargo = value; }
        }

        [DataMember]
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        [DataMember]
        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }
    }
}
