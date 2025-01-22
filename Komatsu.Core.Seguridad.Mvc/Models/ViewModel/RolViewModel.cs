using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Komatsu.Core.Seguridad.ServicioRol;
using Komatsu.Core.Seguridad.ServicioPerfil;

namespace Komatsu.Core.Seguridad.Mvc.Models.ViewModel
{
    public class RolViewModel
    {
        public Komatsu.Core.Seguridad.ServicioRol.Rol Rol { get; set; }
        public Komatsu.Core.Seguridad.ServicioConfiguracion.ListaAplicacion ListaAplicacion { get; set; }
        public Komatsu.Core.Seguridad.ServicioConfiguracion.Aplicacion Aplicacion { get; set; }
        public RolPaginacion RolPaginacion { get; set; }
        public ListaPaginaAccion ListaPaginaAccion { get; set; }
        public String FechaIni { get; set; }
        public String FechaFin { get; set; }
        public ServicioRol.ListaOperacion ListaOperacion { get; set; }
    }
}