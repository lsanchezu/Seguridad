using System;
using WcfSerialization = global::System.Runtime.Serialization;

namespace Komatsu.Core.Seguridad.DataContracts
{
    [WcfSerialization::DataContract(Namespace = "http://komatsu.core.seguridad.model/2013/dckomatsuseguridad", Name = "Politica")]
    public class Politica
    {
        private int longminima_contrasena;
        private int vigencia_contrasena;
        private int diferencia_contrasenaUsuario;
        private int nroMaximoIntentos;
        private int diasBloqueoUsuario;
        private int complejidadContraseña;
        private int cantidadContrasenaHist;

        [WcfSerialization::DataMember(Name = "IDLongMinima_Contrasena", IsRequired = false, Order = 0)]
        public int LongMinima_Contrasena
        {
            get { return longminima_contrasena; }
            set { longminima_contrasena = value; } 
        }

        [WcfSerialization::DataMember(Name = "IDVigencia_Contrasena", IsRequired = false, Order = 1)]
        public int Vigencia_Contrasena 
        {
            get { return vigencia_contrasena; }
            set { vigencia_contrasena = value; }
        }

        [WcfSerialization::DataMember(Name = "IDDiferencia_ContrasenaUsuario", IsRequired = false, Order = 2)]
        public int Diferencia_ContrasenaUsuario
        {
            get { return diferencia_contrasenaUsuario; }
            set { diferencia_contrasenaUsuario = value; }
        }

        [WcfSerialization::DataMember(Name = "IDNroMaximoIntentos", IsRequired = false, Order = 3)]
        public int NroMaximoIntentos
        {
            get { return nroMaximoIntentos; }
            set { nroMaximoIntentos = value; } 
        }

        [WcfSerialization::DataMember(Name = "IDDiasBloqueoUsuario", IsRequired = false, Order = 4)]
        public int DiasBloqueoUsuario
        {
            get { return diasBloqueoUsuario; }
            set { diasBloqueoUsuario = value; } 
        }

        [WcfSerialization::DataMember(Name = "IDComplejidadContraseña", IsRequired = false, Order = 5)]
        public int ComplejidadContraseña
        {
            get { return complejidadContraseña; }
            set { complejidadContraseña = value; } 
        }

        [WcfSerialization::DataMember(Name = "IDCantidadContrasenaHist", IsRequired = false, Order = 6)]
        public int CantidadContrasenaHist
        {
            get { return cantidadContrasenaHist; }
            set { cantidadContrasenaHist = value; } 
        }
    }
}
