using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Komatsu.Core.Seguridad.DataContracts
{
    [DataContract]
    public class UnidadOrganica
    {

        private int _IdUnidadOrganica;
        private string _Nombre;
        private string _Descripcion;
        private string _Correlativo;

        [DataMember]
        public int IdUnidadOrganica
        {

            get { return _IdUnidadOrganica; }
            set { _IdUnidadOrganica = value; }
        }

        [DataMember]
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        [DataMember]
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        [DataMember]
        public string Correlativo
        {
            get { return _Correlativo; }
            set { _Correlativo = value; }
        }
    }
}
