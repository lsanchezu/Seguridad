using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Komatsu.Core.Seguridad.ServicioConfiguracion;

namespace Komatsu.Core.Seguridad.Mvc.Models.ViewModel
{
    public class ConfiguracionViewModel
    {
        public Empresa Empresa { get; set; }
        public ListaEmpresa ListaEmpresa { get; set; }
        public EmpresaPaginacion EmpresaPaginacion { get; set; }

        public Aplicacion Aplicacion { get; set; }
        public ListaAplicacion ListaAplicacion { get; set; }
        public AplicacionPaginacion AplicacionPaginacion { get; set; }

        public Modulo Modulo { get;set; }
        public ListaModulo ListaModulo { get; set; }
        public ModuloPaginacion ModuloPaginacion { get; set; }

        public Pagina Pagina { get; set; }
        public ListaPagina ListaPagina { get; set; }
        public PaginaPaginacion PaginaPaginacion { get; set; }

        public PaginaAccion PaginaAccion { get; set; }
        public ListaPaginaAccion ListaPaginaAccion { get; set; }
        public PaginaAccionPaginacion PaginaAccionPaginacion { get; set; }

        public ListaGrupo ListaGrupo { get; set; }
        public Grupo Grupo { get; set; }
        public Komatsu.Core.Seguridad.ServicioPerfil.ListaPaginaAccion  ListaPaginaAccionPermiso { get; set; }

        public Komatsu.Core.Seguridad.DataContracts.Usuario Usuario { get; set; }

        public Komatsu.Core.Seguridad.DataContracts.Agrupacion Agrupacion { get; set; }
        public ListaAgrupacion ListaAgrupacion { get; set; }
        public int IdTamanoMenu { get; set; }

    }
}