using System.Transactions;
using Komatsu.Core.Seguridad.DataAccess;
using Komatsu.Core.Seguridad.DataContracts;

namespace Komatsu.Core.Seguridad.BusinessLogic
{
    public class ConfiguracionBusinessLogic
    {
        #region Empresa

        public EmpresaPaginacion ListarEmpresaPaginacion(Empresa empresa, Paginacion paginacion)
        {
            return new ConfiguracionDataAccess().ListarEmpresaPaginacion(empresa, paginacion);
        }

        public Empresa BuscarEmpresa(Empresa empresa)
        {
            return new ConfiguracionDataAccess().BuscarEmpresa(empresa);
        }

        public int Insertar_Empresa(Empresa empresa)
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                result = new ConfiguracionDataAccess().Insertar_Empresa(empresa);
                if (result > 0)
                    scope.Complete();
            }
            return result;
        }

        public int Actualizar_Empresa(Empresa empresa)
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                result = new ConfiguracionDataAccess().Actualizar_Empresa(empresa);
                if (result > 0)
                    scope.Complete();
            }
            return result;
        }

        public int ActualizarEstado_Empresa(Empresa empresa)
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                result = new ConfiguracionDataAccess().ActualizarEstado_Empresa(empresa);
                if (result > 0)
                    scope.Complete();
            }
            return result;
        }

        public ListaEmpresa ListarEmpresa()
        {
            return new ConfiguracionDataAccess().ListarEmpresa();
        }

        public ListaEmpresa BuscarEmpresasPorAplicacion(int IdAplicacion)
        {
            return new ConfiguracionDataAccess().BuscarEmpresasPorAplicacion(IdAplicacion);
        }

        #endregion

        #region Aplicacion

        public AplicacionPaginacion ListarAplicacionPaginacion(Aplicacion aplicacion, Paginacion paginacion)
        {
            return new ConfiguracionDataAccess().ListarAplicacionPaginacion(aplicacion, paginacion);
        }

        public Aplicacion BuscarAplicacion(Aplicacion aplicacion)
        {
            return new ConfiguracionDataAccess().BuscarAplicacion(aplicacion);
        }

        public string Insertar_Aplicacion(Aplicacion aplicacion)
        {
            string result = "";
            using (TransactionScope scope = new TransactionScope())
            {
                /*************************************************************************************/
                int exist = 0;
                string nombreempresa = string.Empty;
                foreach (string Id in aplicacion.Empresa.IdsEmpresa.Split(','))
                {
                    nombreempresa = new ConfiguracionDataAccess().EmpresaNombre(int.Parse(Id));
                    exist = new ConfiguracionDataAccess().ValidExiste(int.Parse(Id), aplicacion.Nombre);
                    if (exist > 0)
                        break;
                }
                if (exist > 0)
                    return "-1/" + nombreempresa;

                /*************************************************************************************/    

                result = new ConfiguracionDataAccess().Insertar_Aplicacion(aplicacion).ToString();
                aplicacion.IdAplicacion = int.Parse(result); //Nuevo IdAplicacion
                if (int.Parse(result) > 0)
                {
                    var Ids = aplicacion.Empresa.IdsEmpresa.Split(',');
                    ListaEmpresaAplicacion listaEmpresaAplicacion = new ListaEmpresaAplicacion();

                    foreach (string Id in Ids)
                    {
                        EmpresaAplicacion EmpresaAplicacion = new EmpresaAplicacion();
                        EmpresaAplicacion.IdAplicacion = aplicacion.IdAplicacion;
                        EmpresaAplicacion.IdEmpresa = int.Parse(Id);
                        listaEmpresaAplicacion.Add(EmpresaAplicacion);
                    }

                    if (listaEmpresaAplicacion.Count > 0)
                        result = new ConfiguracionDataAccess().AsignarEmpresaAplicacion(listaEmpresaAplicacion).ToString() + "/";
                    else
                        result = "1/";

                    if (int.Parse(result.Split('/')[0]) > 0)
                        scope.Complete();
                }
                else
                {
                    result = "0/";
                    scope.Complete();
                }
            }
            return result;
        }

        public int Actualizar_Aplicacion(Aplicacion aplicacion)
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                result = new ConfiguracionDataAccess().Actualizar_Aplicacion(aplicacion);
                if (result > 0)
                {
                    result = new ConfiguracionDataAccess().EmpresaAplicacion_Eliminar(aplicacion);
                    if (result > 0)
                    {
                        var Ids = aplicacion.Empresa.IdsEmpresa.Split(',');
                        ListaEmpresaAplicacion listaEmpresaAplicacion = new ListaEmpresaAplicacion();

                        foreach (string Id in Ids)
                        {
                            EmpresaAplicacion EmpresaAplicacion = new EmpresaAplicacion();
                            EmpresaAplicacion.IdAplicacion = aplicacion.IdAplicacion;
                            EmpresaAplicacion.IdEmpresa = int.Parse(Id);
                            listaEmpresaAplicacion.Add(EmpresaAplicacion);
                        }

                        if (listaEmpresaAplicacion.Count > 0)
                            result = new ConfiguracionDataAccess().AsignarEmpresaAplicacion(listaEmpresaAplicacion);
                        else
                            result = 1;

                        if (result > 0)
                            scope.Complete();
                    }
                }
                    
            }
            return result;
        }

        public int ActualizarEstado_Aplicacion(Aplicacion aplicacion)
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                result = new ConfiguracionDataAccess().ActualizarEstado_Aplicacion(aplicacion);
                if (result > 0)
                    scope.Complete();
            }
            return result;
        }

        public ListaAplicacion ListarAplicacion()
        {
            return new ConfiguracionDataAccess().ListarAplicacion();
        }

        public ListaAplicacion AplicacionPorEmpresa(Aplicacion aplicacion)
        {
            return new ConfiguracionDataAccess().AplicacionPorEmpresa(aplicacion);
        }

        #endregion

        #region Modulo

        public ModuloPaginacion ListarModuloPaginacion(Modulo modulo, Paginacion paginacion)
        {
            return new ConfiguracionDataAccess().ListarModuloPaginacion(modulo, paginacion);
        }

        public Modulo BuscarModulo(Modulo modulo)
        {
            return new ConfiguracionDataAccess().BuscarModulo(modulo);
        }

        public int Insertar_Modulo(Modulo modulo)
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                result = new ConfiguracionDataAccess().Insertar_Modulo(modulo);
                if (result > 0)
                    scope.Complete();
            }
            return result;
        }

        public int Actualizar_Modulo(Modulo modulo)
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                result = new ConfiguracionDataAccess().Actualizar_Modulo(modulo);
                if (result > 0)
                    scope.Complete();
            }
            return result;
        }

        public int ActualizarEstado_Modulo(Modulo modulo)
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                result = new ConfiguracionDataAccess().ActualizarEstado_Modulo(modulo);
                if (result > 0)
                    scope.Complete();
            }
            return result;
        }

        public ListaModulo ListarModulo()
        {
            return new ConfiguracionDataAccess().ListarModulo();
        }

        public ListaModulo ModuloPorAplicacion(Modulo modulo)
        {
            return new ConfiguracionDataAccess().ModuloPorAplicacion(modulo);
        }
        public ListaAgrupacion getListAgrupacionModulo(int IdModulo)
        {
            return new ConfiguracionDataAccess().getListAgrupacionModulo(IdModulo);
        }

        #endregion

        #region Pagina

        public PaginaPaginacion ListarPaginaPaginacion(Pagina pagina, Paginacion paginacion)
        {
            return new ConfiguracionDataAccess().ListarPaginaPaginacion(pagina, paginacion);
        }

        public Pagina BuscarPagina(Pagina pagina)
        {
            return new ConfiguracionDataAccess().BuscarPagina(pagina);
        }

        public int Insertar_Pagina(Pagina pagina, ListaPaginaAccion listaPaginaAccion)
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                ConfiguracionDataAccess oConfiguracionDataAccess = new ConfiguracionDataAccess();
                result = oConfiguracionDataAccess.Insertar_Pagina(pagina); //Insertar La pagina

                if (listaPaginaAccion != null)
                {
                    if (result > 0)
                    {
                        pagina.IdPagina = result;
                        var Ids = pagina.Grupo.IdsGrupo.Split(',');
                        ListaPaginaGrupo listaPaginaGrupo = new ListaPaginaGrupo();

                        foreach (string Id in Ids)
                        {
                            PaginaGrupo PaginaGrupo = new PaginaGrupo();
                            PaginaGrupo.Pagina = new Pagina();
                            PaginaGrupo.Pagina.IdPagina = pagina.IdPagina;
                            PaginaGrupo.Grupo = new Grupo();
                            PaginaGrupo.Grupo.IdGrupo = int.Parse(Id);
                            listaPaginaGrupo.Add(PaginaGrupo);
                        }

                        if (listaPaginaGrupo.Count > 0)
                            result = new ConfiguracionDataAccess().AsignarPaginaGrupo(listaPaginaGrupo);
                        else
                            result = 1;

                        if (result > 0)
                        {
                            if (listaPaginaAccion.Count > 0)
                            {
                                foreach (PaginaAccion PA in listaPaginaAccion)
                                {
                                    PA.Pagina = new Pagina();
                                    PA.Pagina.IdPagina = result;
                                }
                                result = oConfiguracionDataAccess.AsignarPaginaAccion(listaPaginaAccion);
                            }
                        }

                    }
                }

                if (result > 0)
                {
                    result = 1;
                    scope.Complete();
                }
            }
            return result;
        }

        public int Actualizar_Pagina(Pagina pagina, ListaPaginaAccion listaPaginaAccion)
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                result = new ConfiguracionDataAccess().Actualizar_Pagina(pagina);
                if (listaPaginaAccion != null)
                {
                    if (result > 0)
                    {
                        ConfiguracionDataAccess oConfiguracionDataAccess = new ConfiguracionDataAccess();

                        //Guardar Grupo
                        result = oConfiguracionDataAccess.PaginaGrupo_Eliminar(pagina);
                        if (result > 0)
                        {
                            var Ids = pagina.Grupo.IdsGrupo.Split(',');
                            ListaPaginaGrupo listaPaginaGrupo = new ListaPaginaGrupo();

                            foreach (string Id in Ids)
                            {
                                PaginaGrupo PaginaGrupo = new PaginaGrupo();
                                PaginaGrupo.Pagina = new Pagina();
                                PaginaGrupo.Pagina.IdPagina = pagina.IdPagina;
                                PaginaGrupo.Grupo = new Grupo();
                                PaginaGrupo.Grupo.IdGrupo = int.Parse(Id);
                                listaPaginaGrupo.Add(PaginaGrupo);
                            }

                            if (listaPaginaGrupo.Count > 0)
                                result = new ConfiguracionDataAccess().AsignarPaginaGrupo(listaPaginaGrupo);
                            else
                                result = 1;

                            if (result > 0)
                            {
                                ListaPaginaAccion listaDataSql = oConfiguracionDataAccess.BuscarPaginaAccion(new PaginaAccion() { Pagina = new Pagina() { IdPagina = pagina.IdPagina, Grupo = new Grupo() { IdsGrupo = pagina.Grupo.IdsGrupo } } });
                                listaPaginaAccion = DepurarPaginaAccion(listaDataSql, listaPaginaAccion);
                                result = oConfiguracionDataAccess.PaginaAccion_Eliminar(new Pagina() { IdPagina = pagina.IdPagina });
                                if (result > 0)
                                {
                                    result = oConfiguracionDataAccess.AsignarPaginaAccion(listaPaginaAccion);
                                    if (result > 0)
                                        result = 1;
                                }
                            }
                        }
                    }
                }

                if (result > 0)
                {
                    result = 1;
                    scope.Complete();
                }
            }
            return result;
        }

        public int ActualizarEstado_Pagina(Pagina pagina)
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                result = new ConfiguracionDataAccess().ActualizarEstado_Pagina(pagina);
                if (result > 0)
                    scope.Complete();
            }
            return result;
        }

        public ListaPagina ListarPagina()
        {
            return new ConfiguracionDataAccess().ListarPagina();
        }

        public ListaPagina PaginaPorModulo(Pagina pagina)
        {
            return new ConfiguracionDataAccess().PaginaPorModulo(pagina);
        }

        public ListaPagina PaginaPadreListar(Pagina pagina)
        {
            return new ConfiguracionDataAccess().PaginaPadreListar(pagina);
        }

        private ListaPaginaAccion DepurarPaginaAccion(ListaPaginaAccion listaDataSql, ListaPaginaAccion listaDataCliente)
        {
            ListaPaginaAccion oListaResultado = new ListaPaginaAccion();

            if (listaDataSql.Count > 0)
            {
                foreach (PaginaAccion PASql in listaDataSql)
                {
                    PaginaAccion oPaginaAccion = listaDataCliente.Find(x => x.Accion.IdAccion == PASql.Accion.IdAccion);
                    if (oPaginaAccion != null)
                        oListaResultado.Add(oPaginaAccion);
                    else if (PASql.IdPaginaAccion != 0 && PASql.ChkAgregar)
                        oListaResultado.Add(PASql);
                }
            }
            else 
                oListaResultado = listaDataCliente;

            return oListaResultado;
        }

        #endregion

        #region Accion
        public PaginaAccionPaginacion ListarAccionPaginacion(PaginaAccion paginaAccion, Paginacion paginacion)
        {
            return new ConfiguracionDataAccess().ListarAccionPaginacion(paginaAccion, paginacion);
        }

        public int AsignarPaginaAccion(ListaPaginaAccion listaPaginaAccion)
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                result = new ConfiguracionDataAccess().AsignarPaginaAccion(listaPaginaAccion);
                if (result > 0)
                    scope.Complete();
            }
            return result;
        }

        #endregion

        #region Grupo
        public ListaGrupo ListarGrupo()
        {
            return new ConfiguracionDataAccess().ListarGrupo();
        }

        public int AsignarPaginaGrupo(ListaPaginaGrupo listaPaginaGrupo)
        {
            return new ConfiguracionDataAccess().AsignarPaginaGrupo(listaPaginaGrupo);
        }

        public int PaginaGrupo_Eliminar(Pagina pagina)
        {
            return new ConfiguracionDataAccess().PaginaGrupo_Eliminar(pagina);
        }

        public ListaGrupo GruposPorPagina(Pagina pagina)
        {
            return new ConfiguracionDataAccess().GruposPorPagina(pagina);
        }

        #endregion

        #region Accion
        public ListaUnidadOrganica ListarUnidadOrganica()
        {
            return new ConfiguracionDataAccess().ListarUnidadOrganica();
        }

        public ListaArea ListarArea(int idunidadorganica)
        {
            return new ConfiguracionDataAccess().ListarArea(idunidadorganica);
        }
        #endregion
    }
}
