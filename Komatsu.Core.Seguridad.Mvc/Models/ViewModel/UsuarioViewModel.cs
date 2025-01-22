using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Komatsu.Core.Seguridad.ServicioUsuario;
using Komatsu.Core.Seguridad.ServicioPerfil;


namespace Komatsu.Core.Seguridad.Mvc.Models.ViewModel
{
    public class UsuarioViewModel
    {
        public Komatsu.Core.Seguridad.ServicioUsuario.Usuario Usuario { get; set; }
        public Komatsu.Core.Seguridad.ServicioUsuario.UsuarioTipo UsuarioTipo { get; set; }
        public ListaUsuarioTipo ListaUsuarioTipo { get; set; }
        public  ListaRolUsusario ListaUsuariosxRol { get; set; }
        public UsuarioPaginacion UsuarioPaginacion { get; set; }
        public String UsuarioSistema { get; set; }
        public ListaPaginaAccion ListaPaginaAccion { get; set; }
        public Komatsu.Core.Seguridad.ServicioConfiguracion.Empresa Empresa { get; set; }
        public Komatsu.Core.Seguridad.ServicioConfiguracion.ListaEmpresa ListaEmpresa { get; set; }
        public Komatsu.Core.Seguridad.ServicioUsuario.Aplicacion Aplicacion { get; set; }
        public Komatsu.Core.Seguridad.ServicioUsuario.ListaAplicacion ListaAplicacion { get; set; }
        public Komatsu.Core.Seguridad.ServicioUsuario.Rol Rol { get; set; }
        public Komatsu.Core.Seguridad.ServicioUsuario.ListaRol ListaRol { get; set; }
        public Politica Politica { get; set; }
        public ListaPolitica ListaPolitica { get; set; }
        public Komatsu.Core.Seguridad.ServicioConfiguracion.ListaUnidadOrganica ListaUnidadOrganica { get; set; }
        public Komatsu.Core.Seguridad.ServicioConfiguracion.ListaArea ListaArea { get; set; }
        public int IdUnidadOrganica { get; set; }
        public int IdArea { get; set; }
        public ListaEmpresa ListaEmpresaRelacionada { get; set; }
    } 
}