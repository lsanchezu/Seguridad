using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Komatsu.Core.Seguridad.DataContracts
{
    [DataContract]
    public class Area
    {

        private int _IdArea;
        private string _Nombre;
        private string _Descripcion;
        private string _Correlativo;
        private int _Estado;
        private bool _IsEnableCheck;
        private bool _IsCheked;

        [DataMember]
        public int IdArea
        {

            get { return _IdArea; }
            set { _IdArea = value; }
        }

        [DataMember]
        public bool IsEnableCheck
        {

            get { return _IsEnableCheck; }
            set { _IsEnableCheck = value; }
        }



        [DataMember]
        public bool IsChecked
        {

            get { return _IsCheked; }
            set { _IsCheked = value; }
        }

        [DataMember]
        public int Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
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
