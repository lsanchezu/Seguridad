using Komatsu.Core.Seguridad.ServicioConfiguracion;

namespace Komatsu.Core.Seguridad.Mvc.ServiceController
{
    public class ServicioConfiguracionController
    {
        private static ServicioConfiguracionController _Instancia;

        public static ServicioConfiguracionController Instancia
        {
            get { return _Instancia ?? (_Instancia = new ServicioConfiguracionController()); }
        }

        #region Empresa

        public EmpresaPaginacion ListarEmpresaPaginacion(Empresa empresa, Paginacion paginacion)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.ListarEmpresaPaginacion(empresa, paginacion);
            }
        }

        public Empresa BuscarEmpresa(Empresa empresa)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.BuscarEmpresa(empresa);
            }
        }

        public int Insertar_Empresa(Empresa empresa)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.Insertar_Empresa(empresa);
            }
        }

        public int Actualizar_Empresa(Empresa empresa)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.Actualizar_Empresa(empresa);
            }
        }

        public int ActualizarEstado_Empresa(Empresa empresa)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.ActualizarEstado_Empresa(empresa);
            }
        }

        public ListaEmpresa ListarEmpresa()
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.ListarEmpresa();
            }
        }

        public ListaEmpresa BuscarEmpresasPorAplicacion(int IdAplicacion)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.BuscarEmpresasPorAplicacion(IdAplicacion);
            }
        }

        public ListaUnidadOrganica ListarUnidadOrganica()
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.ListarUnidadOrganica();
            }
        }

        public ListaArea ListarArea(int idunidaorganica)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.ListarArea(idunidaorganica);
            }
        }

        #endregion

        #region Aplicacion
        public AplicacionPaginacion ListarAplicacionPaginacion(Aplicacion aplicacion, Paginacion paginacion)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.ListarAplicacionPaginacion(aplicacion, paginacion);
            }
        }

        public Aplicacion BuscarAplicacion(Aplicacion aplicacion)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.BuscarAplicacion(aplicacion);
            }
        }

        public string Insertar_Aplicacion(Aplicacion aplicacion)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.Insertar_Aplicacion(aplicacion);
            }
        }

        public int Actualizar_Aplicacion(Aplicacion aplicacion)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.Actualizar_Aplicacion(aplicacion);
            }
        }

        public int ActualizarEstado_Aplicacion(Aplicacion aplicacion)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.ActualizarEstado_Aplicacion(aplicacion);
            }
        }

        public ListaAplicacion ListarAplicacion()
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.ListarAplicacion();
            }
        }

        public ListaAplicacion AplicacionPorEmpresa(Aplicacion aplicacion)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.AplicacionPorEmpresa(aplicacion);
            }
        }

        #endregion

        #region Modulo
        public ModuloPaginacion ListarModuloPaginacion(Modulo modulo, Paginacion paginacion)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.ListarModuloPaginacion(modulo, paginacion);
            }
        }

        public Modulo BuscarModulo(Modulo modulo)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.BuscarModulo(modulo);
            }
        }

        public int Insertar_Modulo(Modulo modulo)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.Insertar_Modulo(modulo);
            }
        }

        public int Actualizar_Modulo(Modulo modulo)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.Actualizar_Modulo(modulo);
            }
        }

        public int ActualizarEstado_Modulo(Modulo modulo)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.ActualizarEstado_Modulo(modulo);
            }
        }

        public ListaModulo ListarModulo()
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.ListarModulo();
            }
        }

        public ListaModulo ModuloPorAplicacion(Modulo modulo)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.ModuloPorAplicacion(modulo);
            }
        }

        public ListaAgrupacion getListAgrupacionModulo(int IdModulo)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.getListAgrupacionModulo(IdModulo);
            }
        }

        #endregion

        #region Pagina
        public PaginaPaginacion ListarPaginaPaginacion(Pagina pagina, Paginacion paginacion)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.ListarPaginaPaginacion(pagina, paginacion);
            }
        }

        public Pagina BuscarPagina(Pagina pagina)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.BuscarPagina(pagina);
            }
        }

        public int Insertar_Pagina(Pagina pagina, ListaPaginaAccion listaPaginaAccion)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.Insertar_Pagina(pagina, listaPaginaAccion);
            }
        }

        public int Actualizar_Pagina(Pagina pagina, ListaPaginaAccion listaPaginaAccion)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.Actualizar_Pagina(pagina, listaPaginaAccion);
            }
        }

        public int ActualizarEstado_Pagina(Pagina pagina)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.ActualizarEstado_Pagina(pagina);
            }
        }

        public ListaPagina ListarPagina()
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.ListarPagina();
            }
        }

        public ListaPagina PaginaPorModulo(Pagina pagina)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.PaginaPorModulo(pagina);
            }
        }

        public ListaPagina PaginaPadreListar(Pagina pagina)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.PaginaPadreListar(pagina);
            }
        }

        #endregion

        #region Acción
        public PaginaAccionPaginacion ListarAccionPaginacion(PaginaAccion paginaAccion, Paginacion paginacion)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.ListarAccionPaginacion(paginaAccion, paginacion);
            }
        }

        public int AsignarPaginaAccion(ListaPaginaAccion listaPaginaAccion)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.AsignarPaginaAccion(listaPaginaAccion);
            }
        }

        #endregion

        #region Grupo
        public ListaGrupo ListarGrupo()
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.ListarGrupo();
            }
        }

        public int AsignarPaginaGrupo(ListaPaginaGrupo listaPaginaGrupo)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.AsignarPaginaGrupo(listaPaginaGrupo);
            }
        }

        public int PaginaGrupo_Eliminar(Pagina pagina)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.PaginaGrupo_Eliminar(pagina);
            }
        }

        public ListaGrupo GruposPorPagina(Pagina pagina)
        {
            using (ServicioConfiguracionClient client = new ServicioConfiguracionClient())
            {
                return client.GruposPorPagina(pagina);
            }
        }
        #endregion
    }
}