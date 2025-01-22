using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Komatsu.Core.Seguridad.ServicioRol;
using Komatsu.Core.Seguridad.ServicioUsuario;
using Komatsu.Core.Seguridad.ServicioPerfil;

namespace Komatsu.Core.Seguridad.Mvc.Models.ViewModel
{
    public class PerfilViewModel
    {
        public TreeView TreeView { get; set; }
        public ListaTreeView ListaTreeView { get; set; }
        public Komatsu.Core.Seguridad.ServicioRol.ListaRol ListaRol { get; set; }
        public Komatsu.Core.Seguridad.ServicioRol.Rol Rol { get; set; }
        public ListaUsuario ListaUsuario { get; set; }
        public Komatsu.Core.Seguridad.ServicioUsuario.Usuario Usuario { get; set; }
        public ListaUsuario ListaUsuariosAsignados { get; set; }
        public ListaUsuario ListaUsuariosNoAsignados { get; set; }
        public ListaPaginaAccion ListaPaginaAccion { get; set; }
        public Komatsu.Core.Seguridad.ServicioConfiguracion.Empresa Empresa { get; set; }
        public Komatsu.Core.Seguridad.ServicioConfiguracion.ListaEmpresa ListaEmpresa { get; set; }
        public Komatsu.Core.Seguridad.ServicioConfiguracion.Aplicacion Aplicacion { get; set; }
        public Komatsu.Core.Seguridad.ServicioConfiguracion.ListaAplicacion ListaAplicacion { get; set; }
    }
}