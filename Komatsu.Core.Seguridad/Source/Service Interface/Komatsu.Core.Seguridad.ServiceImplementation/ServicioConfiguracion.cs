using System;
using Komatsu.Core.Seguridad.BusinessLogic;
using Komatsu.Core.Seguridad.DataContracts;
using Komatsu.Core.Seguridad.ServiceContracts;

namespace Komatsu.Core.Seguridad.ServiceImplementation
{
    public class ServicioConfiguracion : IServicioConfiguracion
    {
        #region Empresa
        public EmpresaPaginacion ListarEmpresaPaginacion(Empresa empresa, Paginacion paginacion)
        {
            return new ConfiguracionBusinessLogic().ListarEmpresaPaginacion(empresa, paginacion);
        }

        public Empresa BuscarEmpresa(Empresa empresa)
        {
            return new ConfiguracionBusinessLogic().BuscarEmpresa(empresa);
        }

        public int Insertar_Empresa(Empresa empresa)
        {
            return new ConfiguracionBusinessLogic().Insertar_Empresa(empresa);
        }

        public int Actualizar_Empresa(Empresa empresa)
        {
            return new ConfiguracionBusinessLogic().Actualizar_Empresa(empresa);
        }

        public int ActualizarEstado_Empresa(Empresa empresa)
        {
            return new ConfiguracionBusinessLogic().ActualizarEstado_Empresa(empresa);
        }

        public ListaEmpresa ListarEmpresa()
        {
            return new ConfiguracionBusinessLogic().ListarEmpresa();
        }

        public ListaEmpresa BuscarEmpresasPorAplicacion(int IdAplicacion)
        {
            return new ConfiguracionBusinessLogic().BuscarEmpresasPorAplicacion(IdAplicacion);
        }

        #endregion

        #region Aplicacion
        public AplicacionPaginacion ListarAplicacionPaginacion(Aplicacion aplicacion, Paginacion paginacion)
        {
            return new ConfiguracionBusinessLogic().ListarAplicacionPaginacion(aplicacion, paginacion);
        }

        public Aplicacion BuscarAplicacion(Aplicacion aplicacion)
        {
            return new ConfiguracionBusinessLogic().BuscarAplicacion(aplicacion);
        }

        public string Insertar_Aplicacion(Aplicacion aplicacion)
        {
            return new ConfiguracionBusinessLogic().Insertar_Aplicacion(aplicacion);
        }

        public int Actualizar_Aplicacion(Aplicacion aplicacion)
        {
            return new ConfiguracionBusinessLogic().Actualizar_Aplicacion(aplicacion);
        }

        public int ActualizarEstado_Aplicacion(Aplicacion aplicacion)
        {
            return new ConfiguracionBusinessLogic().ActualizarEstado_Aplicacion(aplicacion);
        }

        public ListaAplicacion ListarAplicacion()
        {
            return new ConfiguracionBusinessLogic().ListarAplicacion();
        }

        public ListaAplicacion AplicacionPorEmpresa(Aplicacion aplicacion)
        {
            return new ConfiguracionBusinessLogic().AplicacionPorEmpresa(aplicacion);
        }

        #endregion

        #region Modulo
        public ModuloPaginacion ListarModuloPaginacion(Modulo modulo, Paginacion paginacion)
        {
            return new ConfiguracionBusinessLogic().ListarModuloPaginacion(modulo, paginacion);
        }

        public Modulo BuscarModulo(Modulo modulo)
        {
            return new ConfiguracionBusinessLogic().BuscarModulo(modulo);
        }

        public int Insertar_Modulo(Modulo modulo)
        {
            return new ConfiguracionBusinessLogic().Insertar_Modulo(modulo);
        }

        public int Actualizar_Modulo(Modulo modulo)
        {
            return new ConfiguracionBusinessLogic().Actualizar_Modulo(modulo);
        }

        public int ActualizarEstado_Modulo(Modulo modulo)
        {
            return new ConfiguracionBusinessLogic().ActualizarEstado_Modulo(modulo);
        }

        public ListaModulo ListarModulo()
        {
            return new ConfiguracionBusinessLogic().ListarModulo();
        }

        public ListaModulo ModuloPorAplicacion(Modulo modulo)
        {
            return new ConfiguracionBusinessLogic().ModuloPorAplicacion(modulo);
        }

        public ListaAgrupacion getListAgrupacionModulo(int IdModulo)
        {
            return new ConfiguracionBusinessLogic().getListAgrupacionModulo(IdModulo);
        }

        #endregion

        #region Pagina

        public PaginaPaginacion ListarPaginaPaginacion(Pagina pagina, Paginacion paginacion)
        {
            return new ConfiguracionBusinessLogic().ListarPaginaPaginacion(pagina, paginacion);
        }

        public Pagina BuscarPagina(Pagina pagina)
        {
            return new ConfiguracionBusinessLogic().BuscarPagina(pagina);
        }

        public int Insertar_Pagina(Pagina pagina, ListaPaginaAccion listaPaginaAccion)
        {
            return new ConfiguracionBusinessLogic().Insertar_Pagina(pagina, listaPaginaAccion);
        }

        public int Actualizar_Pagina(Pagina pagina, ListaPaginaAccion listaPaginaAccion)
        {
            return new ConfiguracionBusinessLogic().Actualizar_Pagina(pagina, listaPaginaAccion);
        }

        public int ActualizarEstado_Pagina(Pagina pagina)
        {
            return new ConfiguracionBusinessLogic().ActualizarEstado_Pagina(pagina);
        }

        public ListaPagina ListarPagina()
        {
            return new ConfiguracionBusinessLogic().ListarPagina();
        }

        public ListaPagina PaginaPorModulo(Pagina pagina)
        {
            return new ConfiguracionBusinessLogic().PaginaPorModulo(pagina);
        }

        public ListaPagina PaginaPadreListar(Pagina pagina)
        {
            return new ConfiguracionBusinessLogic().PaginaPadreListar(pagina);
        }

        #endregion

        #region Acción
        public PaginaAccionPaginacion ListarAccionPaginacion(PaginaAccion paginaAccion, Paginacion paginacion)
        {
            return new ConfiguracionBusinessLogic().ListarAccionPaginacion(paginaAccion, paginacion);
        }

        public int AsignarPaginaAccion(ListaPaginaAccion listaPaginaAccion)
        {
            return new ConfiguracionBusinessLogic().AsignarPaginaAccion(listaPaginaAccion);
        }

        #endregion

        #region Grupo
        public ListaGrupo ListarGrupo()
        {
            return new ConfiguracionBusinessLogic().ListarGrupo();
        }

        public int AsignarPaginaGrupo(ListaPaginaGrupo listaPaginaGrupo)
        {
            return new ConfiguracionBusinessLogic().AsignarPaginaGrupo(listaPaginaGrupo);
        }

        public int PaginaGrupo_Eliminar(Pagina pagina)
        {
            return new ConfiguracionBusinessLogic().PaginaGrupo_Eliminar(pagina);
        }

        public ListaGrupo GruposPorPagina(Pagina pagina)
        {
            return new ConfiguracionBusinessLogic().GruposPorPagina(pagina);
        }

        #endregion

        #region OEFA
        public ListaUnidadOrganica ListarUnidadOrganica()
        {
            return new ConfiguracionBusinessLogic().ListarUnidadOrganica();
        }

        public ListaArea ListarArea(int idunidadorganica)
        {
            return new ConfiguracionBusinessLogic().ListarArea(idunidadorganica);
        }
        #endregion
    }
}
