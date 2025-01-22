using System.ComponentModel.DataAnnotations;
using Komatsu.Core.Seguridad.Validation.Helper;

namespace Komatsu.Core.Seguridad.Validation.Validation.Metadata
{
    public class RolMetaData
    {
        private int idRol;
        private bool siSuperAdmi;
        private string nombre;
        private bool siRango;
        private System.DateTime fechaInicio;
        private System.DateTime fechaFin;

        [LocalizedDisplayName("IdCausa", NameResourceType = typeof(Komatsu.Core.Seguridad.Validation.Labels.RolLabel))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "CampoRequerido")]
        public int IdRol
        {
            get { return idRol; }
            set { idRol = value; }
        }

        public bool SiSuperAdmi
        {
            get { return siSuperAdmi; }
            set { siSuperAdmi = value; }
        }

        [LocalizedDisplayName("Nombre", NameResourceType = typeof(Komatsu.Core.Seguridad.Validation.Labels.RolLabel))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "CampoRequerido")]
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public bool SiRango
        {
            get { return siRango; }
            set { siRango = value; }
        }

        [LocalizedDisplayName("FechaInicio", NameResourceType = typeof(Komatsu.Core.Seguridad.Validation.Labels.RolLabel))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "CampoRequerido")]
        [RegularExpression("^(([0-9])|([0-2][0-9])|(3[0-1]))/(([1-9])|(0[1-9])|(1[0-2]))/(([0-9][0-9])|([1-2][0,9][0-9][0-9]))$")]
        public System.DateTime FechaInicio
        {
            get { return fechaInicio; }
            set { fechaInicio = value; }
        }

        [LocalizedDisplayName("FechaFin", NameResourceType = typeof(Komatsu.Core.Seguridad.Validation.Labels.RolLabel))]
        [Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName = "CampoRequerido")]
        [RegularExpression("^(([0-9])|([0-2][0-9])|(3[0-1]))/(([1-9])|(0[1-9])|(1[0-2]))/(([0-9][0-9])|([1-2][0,9][0-9][0-9]))$")]
        public System.DateTime FechaFin
        {
            get { return fechaFin; }
            set { fechaFin = value; }
        }				
    }
}
