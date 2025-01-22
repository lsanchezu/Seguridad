using System;
using System.Data;
using System.Data.Common;
using Komatsu.Core.Seguridad.DataAccess.Helper;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Komatsu.Core.Seguridad.DataContracts;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace Komatsu.Core.Seguridad.DataAccess
{
    public class ConfiguracionDataAccess
    {
        private Database oDatabase = EnterpriseLibraryContainer.Current.GetInstance<Database>(Conexion.cnsSql);
        SqlDatabase oSqlDatabase = (SqlDatabase)DatabaseFactory.CreateDatabase(Conexion.cnsSql);

        #region Configuracion - Empresa
        public EmpresaPaginacion ListarEmpresaPaginacion(Empresa empresa, Paginacion paginacion)
        {
            EmpresaPaginacion oEmpresaPaginacion = new EmpresaPaginacion();
            oEmpresaPaginacion.ListaEmpresa = new ListaEmpresa();
            oEmpresaPaginacion.Paginacion = paginacion;
            Empresa oEmpresa;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_EMPRESA_PAGINACION);

            oDatabase.AddInParameter(oDbCommand, "@Nombre", DbType.String, empresa.Nombre);

            oDatabase.AddInParameter(oDbCommand, "@SortType", DbType.String, paginacion.SortType);
            oDatabase.AddInParameter(oDbCommand, "@SortDir", DbType.String, paginacion.SortDir);
            oDatabase.AddInParameter(oDbCommand, "@Page", DbType.Int32, paginacion.Page);
            oDatabase.AddInParameter(oDbCommand, "@RowsPerPage", DbType.Int32, paginacion.RowsPerPage);
            oDatabase.AddOutParameter(oDbCommand, "@RowCount", DbType.Int32, 0);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdEmpresa = oIDataReader.GetOrdinal("IdEmpresa");
                int intNombreEmpresa = oIDataReader.GetOrdinal("NombreEmpresa");
                int indAbreviatura = oIDataReader.GetOrdinal("Abreviatura");
                int indIdEstado = oIDataReader.GetOrdinal("IdEstado");
                int indNombreEstado = oIDataReader.GetOrdinal("NombreEstado");
                int indRuc = oIDataReader.GetOrdinal("Ruc");

                while (oIDataReader.Read())
                {
                    oEmpresa = new Empresa();
                    oEmpresa.IdEmpresa = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEmpresa]);
                    oEmpresa.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[intNombreEmpresa]);
                    oEmpresa.Abreviatura = DataUtil.DbValueToDefault<String>(oIDataReader[indAbreviatura]);
                    oEmpresa.Estado = new Estado();
                    oEmpresa.Estado.IdEstado = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEstado]);
                    oEmpresa.Estado.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreEstado]);
                    oEmpresa.ContentStyle = DataUtil.DbValueToDefault<String>(oIDataReader[indRuc]);
                    oEmpresaPaginacion.ListaEmpresa.Add(oEmpresa);
                }
            }
            oEmpresaPaginacion.Paginacion.RowCount = Convert.ToInt32(oDatabase.GetParameterValue(oDbCommand, "@RowCount"));
            return oEmpresaPaginacion;
        }

        public Empresa BuscarEmpresa(Empresa empresa)
        {
            Empresa oEmpresa = new Empresa();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_EMPRESA_BUSCAR);

            oDatabase.AddInParameter(oDbCommand, "@IdEmpresa", DbType.Int32, empresa.IdEmpresa);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdEmpresa = oIDataReader.GetOrdinal("IdEmpresa");
                int indIdEstado = oIDataReader.GetOrdinal("IdEstado");
                int indNombreEmpresa = oIDataReader.GetOrdinal("NombreEmpresa");
                int indAbreviatura = oIDataReader.GetOrdinal("Abreviatura");
                int indNombreEstado = oIDataReader.GetOrdinal("NombreEstado");
                int indCodigoEmpresa = oIDataReader.GetOrdinal("CodigoEmpresa");
                int indContentStyle = oIDataReader.GetOrdinal("ContentStyle");

                while (oIDataReader.Read())
                {
                    oEmpresa.IdEmpresa = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEmpresa]);
                    oEmpresa.Abreviatura = DataUtil.DbValueToDefault<String>(oIDataReader[indAbreviatura]);
                    oEmpresa.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreEmpresa]);
                    oEmpresa.CodigoEmpresa = DataUtil.DbValueToDefault<String>(oIDataReader[indCodigoEmpresa]);
                    oEmpresa.ContentStyle = DataUtil.DbValueToDefault<String>(oIDataReader[indContentStyle]);
                    oEmpresa.Estado = new Estado();
                    oEmpresa.Estado.IdEstado = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEstado]);
                    oEmpresa.Estado.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreEstado]);
                }
            }
            return oEmpresa;
        }

        public int Insertar_Empresa(Empresa empresa)
        {
            try
            {
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_EMPRESA_INSERTAR);

                oDatabase.AddInParameter(oDbCommand, "@Nombre", DbType.String, empresa.Nombre);
                oDatabase.AddInParameter(oDbCommand, "@Abreviatura", DbType.String, empresa.Abreviatura);
                oDatabase.AddInParameter(oDbCommand, "@CodigoEmpresa", DbType.String, empresa.CodigoEmpresa);
                oDatabase.AddInParameter(oDbCommand, "@ContentStyle", DbType.String, empresa.ContentStyle);

                return oDatabase.ExecuteNonQuery(oDbCommand);

                /*return (int)oDatabase.ExecuteScalar(Procedimientos.USP_EMPRESA_INSERTAR, empresa.Nombre,
                                                             empresa.Abreviatura,
                                                             empresa.CodigoEmpresa,
                                                             empresa.ContentStyle);*/
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Actualizar_Empresa(Empresa empresa)
        {
            try
            {
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_EMPRESA_ACTUALIZAR);

                oDatabase.AddInParameter(oDbCommand, "@IdEmpresa", DbType.Int32, empresa.IdEmpresa);
                oDatabase.AddInParameter(oDbCommand, "@Nombre", DbType.String, empresa.Nombre);
                oDatabase.AddInParameter(oDbCommand, "@Abreviatura", DbType.String, empresa.Abreviatura);
                oDatabase.AddInParameter(oDbCommand, "@CodigoEmpresa", DbType.String, empresa.CodigoEmpresa);
                oDatabase.AddInParameter(oDbCommand, "@ContentStyle", DbType.String, empresa.ContentStyle);

                return oDatabase.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int ActualizarEstado_Empresa(Empresa empresa)
        {
            try
            {
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_EMPRESA_ACTUALIZARESTADO);

                oDatabase.AddInParameter(oDbCommand, "@IdEmpresa", DbType.Int32, empresa.IdEmpresa);
                oDatabase.AddInParameter(oDbCommand, "@IdEstado", DbType.String, empresa.Estado.IdEstado);

                return oDatabase.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public ListaEmpresa ListarEmpresa()
        {
            ListaEmpresa oListaEmpresa = new ListaEmpresa();
            Empresa oEmpresa;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_EMPRESA_LISTAR);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdEmpresa = oIDataReader.GetOrdinal("IdEmpresa");
                int intNombreEmpresa = oIDataReader.GetOrdinal("NombreEmpresa");
                int indAbreviatura = oIDataReader.GetOrdinal("Abreviatura");
                int indCodigoEmpresa = oIDataReader.GetOrdinal("CodigoEmpresa");
                int indContentStyle = oIDataReader.GetOrdinal("ContentStyle");
                int indIdEstado = oIDataReader.GetOrdinal("IdEstado");
                int indNombreEstado = oIDataReader.GetOrdinal("NombreEstado");

                while (oIDataReader.Read())
                {
                    oEmpresa = new Empresa();
                    oEmpresa.IdEmpresa = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEmpresa]);
                    oEmpresa.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[intNombreEmpresa]);
                    oEmpresa.Abreviatura = DataUtil.DbValueToDefault<String>(oIDataReader[indAbreviatura]);
                    oEmpresa.CodigoEmpresa = DataUtil.DbValueToDefault<String>(oIDataReader[indCodigoEmpresa]);
                    oEmpresa.ContentStyle = DataUtil.DbValueToDefault<String>(oIDataReader[indContentStyle]);
                    oEmpresa.Estado = new Estado();
                    oEmpresa.Estado.IdEstado = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEstado]);
                    oEmpresa.Estado.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreEstado]);
                    oListaEmpresa.Add(oEmpresa);
                }
            }
            return oListaEmpresa;
        }

        public ListaEmpresa BuscarEmpresasPorAplicacion(Int32 IdAplicacion)
        {
            ListaEmpresa oListaEmpresa = new ListaEmpresa();
            Empresa oEmpresa;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_EMPRESASPORAPLICACION);

            oDatabase.AddInParameter(oDbCommand, "@IdAplicacion", DbType.Int32, IdAplicacion);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdEmpresa = oIDataReader.GetOrdinal("IdEmpresa");
                int intNombreEmpresa = oIDataReader.GetOrdinal("NombreEmpresa");
                int indAbreviatura = oIDataReader.GetOrdinal("Abreviatura");
                int indCheck = oIDataReader.GetOrdinal("Check");

                while (oIDataReader.Read())
                {
                    oEmpresa = new Empresa();
                    oEmpresa.IdEmpresa = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEmpresa]);
                    oEmpresa.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[intNombreEmpresa]);
                    oEmpresa.Abreviatura = DataUtil.DbValueToDefault<String>(oIDataReader[indAbreviatura]);
                    oEmpresa.Check = DataUtil.DbValueToDefault<Boolean>(oIDataReader[indCheck]);
                    oListaEmpresa.Add(oEmpresa);
                }
            }
            return oListaEmpresa;
        }

        #endregion

        #region Configuracion - Aplicacion
        public AplicacionPaginacion ListarAplicacionPaginacion(Aplicacion aplicacion, Paginacion paginacion)
        {
            AplicacionPaginacion oAplicacionPaginacion = new AplicacionPaginacion();
            oAplicacionPaginacion.ListaAplicacion = new ListaAplicacion();
            oAplicacionPaginacion.Paginacion = paginacion;
            Aplicacion oAplicacion;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_APLICACION_PAGINACION);

            oDatabase.AddInParameter(oDbCommand, "@IdsEmpresa", DbType.String, aplicacion.Empresa.IdsEmpresa);
            oDatabase.AddInParameter(oDbCommand, "@Descripcion", DbType.String, aplicacion.Descripcion);
            oDatabase.AddInParameter(oDbCommand, "@Nombre", DbType.String, aplicacion.Nombre);

            oDatabase.AddInParameter(oDbCommand, "@SortType", DbType.String, paginacion.SortType);
            oDatabase.AddInParameter(oDbCommand, "@SortDir", DbType.String, paginacion.SortDir);
            oDatabase.AddInParameter(oDbCommand, "@Page", DbType.Int32, paginacion.Page);
            oDatabase.AddInParameter(oDbCommand, "@RowsPerPage", DbType.Int32, paginacion.RowsPerPage);
            oDatabase.AddOutParameter(oDbCommand, "@RowCount", DbType.Int32, 0);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdAplicacion = oIDataReader.GetOrdinal("IdAplicacion");
                int indIdEmpresa = oIDataReader.GetOrdinal("IdEmpresa");
                int indIdEstado = oIDataReader.GetOrdinal("IdEstado");
                int indNombreEmpresa = oIDataReader.GetOrdinal("NombreEmpresa");
                int indNombreEstado = oIDataReader.GetOrdinal("NombreEstado");
                int indNombreAplicacion = oIDataReader.GetOrdinal("NombreAplicacion");
                int indDescripcionAplicacion = oIDataReader.GetOrdinal("DescripcionAplicacion");
                int indAbreviatura = oIDataReader.GetOrdinal("Abreviatura");
                int indUrl = oIDataReader.GetOrdinal("Url");

                while (oIDataReader.Read())
                {
                    oAplicacion = new Aplicacion();
                    oAplicacion.IdAplicacion = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdAplicacion]);
                    oAplicacion.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreAplicacion]);
                    oAplicacion.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indDescripcionAplicacion]);
                    oAplicacion.Url = DataUtil.DbValueToDefault<String>(oIDataReader[indUrl]);
                    oAplicacion.Empresa = new Empresa();
                    oAplicacion.Empresa.IdEmpresa = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEmpresa]);
                    oAplicacion.Empresa.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreEmpresa]);
                    oAplicacion.Empresa.Abreviatura = DataUtil.DbValueToDefault<String>(oIDataReader[indAbreviatura]);
                    oAplicacion.Estado = new Estado();
                    oAplicacion.Estado.IdEstado = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEstado]);
                    oAplicacion.Estado.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreEstado]);
                    oAplicacionPaginacion.ListaAplicacion.Add(oAplicacion);
                }
            }
            oAplicacionPaginacion.Paginacion.RowCount = Convert.ToInt32(oDatabase.GetParameterValue(oDbCommand, "@RowCount"));
            return oAplicacionPaginacion;
        }

        public Aplicacion BuscarAplicacion(Aplicacion aplicacion)
        {
            Aplicacion oAplicacion = new Aplicacion();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_APLICACION_BUSCAR);

            oDatabase.AddInParameter(oDbCommand, "@IdAplicacion", DbType.Int32, aplicacion.IdAplicacion);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdAplicacion = oIDataReader.GetOrdinal("IdAplicacion");
                int indIdEstado = oIDataReader.GetOrdinal("IdEstado");
                int indNombreEstado = oIDataReader.GetOrdinal("NombreEstado");
                int indNombreAplicacion = oIDataReader.GetOrdinal("NombreAplicacion");
                int indDescripcionAplicacion = oIDataReader.GetOrdinal("DescripcionAplicacion");
                int indUrl = oIDataReader.GetOrdinal("Url");

                while (oIDataReader.Read())
                {
                    oAplicacion.IdAplicacion = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdAplicacion]);
                    oAplicacion.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreAplicacion]);
                    oAplicacion.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indDescripcionAplicacion]);
                    oAplicacion.Url = DataUtil.DbValueToDefault<String>(oIDataReader[indUrl]);
                    oAplicacion.Estado = new Estado();
                    oAplicacion.Estado.IdEstado = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEstado]);
                    oAplicacion.Estado.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreEstado]);
                }
            }
            return oAplicacion;
        }

        public int Insertar_Aplicacion(Aplicacion aplicacion)
        {
            try
            {
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_APLICACION_INSERTAR);

                oDatabase.AddInParameter(oDbCommand, "@Url", DbType.String, aplicacion.Url);
                oDatabase.AddInParameter(oDbCommand, "@Nombre", DbType.String, aplicacion.Nombre);
                oDatabase.AddInParameter(oDbCommand, "@Descripcion", DbType.String, aplicacion.Descripcion);
                oDatabase.AddOutParameter(oDbCommand, "@IdAplicacion", DbType.Int32, 0);

                oDatabase.ExecuteNonQuery(oDbCommand);

                return Convert.ToInt32(oDatabase.GetParameterValue(oDbCommand, "@IdAplicacion"));
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int Actualizar_Aplicacion(Aplicacion aplicacion)
        {
            try
            {
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_APLICACION_ACTUALIZAR);

                oDatabase.AddInParameter(oDbCommand, "@IdAplicacion", DbType.Int32, aplicacion.IdAplicacion);
                oDatabase.AddInParameter(oDbCommand, "@Url", DbType.String, aplicacion.Url);
                oDatabase.AddInParameter(oDbCommand, "@Nombre", DbType.String, aplicacion.Nombre);
                oDatabase.AddInParameter(oDbCommand, "@Descripcion", DbType.String, aplicacion.Descripcion);

                return oDatabase.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int ActualizarEstado_Aplicacion(Aplicacion aplicacion)
        {
            try
            {
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_APLICACION_ACTUALIZARESTADO);

                oDatabase.AddInParameter(oDbCommand, "@IdAplicacion", DbType.Int32, aplicacion.IdAplicacion);
                oDatabase.AddInParameter(oDbCommand, "@IdEstado", DbType.String, aplicacion.Estado.IdEstado);

                return oDatabase.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public ListaAplicacion ListarAplicacion()
        {
            ListaAplicacion oListaEmpresa = new ListaAplicacion();
            Aplicacion oAplicacion;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_APLICACION_LISTAR);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdAplicacion = oIDataReader.GetOrdinal("IdAplicacion");
                int indIdEstado = oIDataReader.GetOrdinal("IdEstado");
                int indNombreEstado = oIDataReader.GetOrdinal("NombreEstado");
                int indNombreAplicacion = oIDataReader.GetOrdinal("NombreAplicacion");
                int indDescripcionAplicacion = oIDataReader.GetOrdinal("DescripcionAplicacion");
                int indUrl = oIDataReader.GetOrdinal("Url");

                while (oIDataReader.Read())
                {
                    oAplicacion = new Aplicacion();
                    oAplicacion.IdAplicacion = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdAplicacion]);
                    oAplicacion.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreAplicacion]);
                    oAplicacion.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indDescripcionAplicacion]);
                    oAplicacion.Url = DataUtil.DbValueToDefault<String>(oIDataReader[indUrl]);
                    oAplicacion.Estado = new Estado();
                    oAplicacion.Estado.IdEstado = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEstado]);
                    oAplicacion.Estado.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreEstado]);
                    oListaEmpresa.Add(oAplicacion);
                }
            }
            return oListaEmpresa;
        }

        public ListaAplicacion AplicacionPorEmpresa(Aplicacion aplicacion)
        {
            ListaAplicacion oListaEmpresa = new ListaAplicacion();
            Aplicacion oAplicacion;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_APLICACIONPOREMPRESA);

            oDatabase.AddInParameter(oDbCommand, "@IdEmpresa", DbType.Int32, aplicacion.Empresa.IdEmpresa);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdAplicacion = oIDataReader.GetOrdinal("IdAplicacion");
                int indIdEstado = oIDataReader.GetOrdinal("IdEstado");
                int indNombreEstado = oIDataReader.GetOrdinal("NombreEstado");
                int indNombreAplicacion = oIDataReader.GetOrdinal("NombreAplicacion");
                int indDescripcionAplicacion = oIDataReader.GetOrdinal("DescripcionAplicacion");
                int indUrl = oIDataReader.GetOrdinal("Url");

                while (oIDataReader.Read())
                {
                    oAplicacion = new Aplicacion();
                    oAplicacion.IdAplicacion = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdAplicacion]);
                    oAplicacion.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreAplicacion]);
                    oAplicacion.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indDescripcionAplicacion]);
                    oAplicacion.Url = DataUtil.DbValueToDefault<String>(oIDataReader[indUrl]);
                    oAplicacion.Estado = new Estado();
                    oAplicacion.Estado.IdEstado = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEstado]);
                    oAplicacion.Estado.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreEstado]);
                    oListaEmpresa.Add(oAplicacion);
                }
            }
            return oListaEmpresa;
        }

        public int AsignarEmpresaAplicacion(ListaEmpresaAplicacion listaEmpresaAplicacion)
        {
            try
            {
                DbCommand cmd = oSqlDatabase.GetStoredProcCommand(Procedimientos.USP_EMPRESAAPLICACION_INSERTAR);
                oSqlDatabase.AddInParameter(cmd, "@TypeEmpresaAplicacion", SqlDbType.Structured, EmpresaAplicacionDetalle(listaEmpresaAplicacion));
                oSqlDatabase.ExecuteNonQuery(cmd);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public IEnumerable<SqlDataRecord> EmpresaAplicacionDetalle(ListaEmpresaAplicacion listaEmpresaAplicacion)
        {

            List<SqlDataRecord> records = new List<SqlDataRecord>();
            foreach (var item in listaEmpresaAplicacion)
            {

                SqlDataRecord record = new SqlDataRecord(
                    new SqlMetaData[]{  new SqlMetaData("IdEmpresa", SqlDbType.Int),
                                        new SqlMetaData("IdAplicacion", SqlDbType.Int)
                                    });
                record.SetInt32(0, item.IdEmpresa);
                record.SetInt32(1, item.IdAplicacion);
                records.Add(record);
            }

            return records;
        }

        public int EmpresaAplicacion_Eliminar(Aplicacion aplicacion)
        {
            try
            {
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_EMPRESAAPLICACION_ELIMINAR);
                oDatabase.AddInParameter(oDbCommand, "@IdAplicacion", DbType.Int32, aplicacion.IdAplicacion);
                oDatabase.ExecuteNonQuery(oDbCommand);
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int ValidExiste(int idempresa, string aplicaciontitulo)
        {
            return (int)oDatabase.ExecuteScalar(Procedimientos.USP_APLICACION_EXISTE, idempresa, aplicaciontitulo);
        }

        public string EmpresaNombre(int idempresa)
        {
            return (string)oDatabase.ExecuteScalar(Procedimientos.USP_EMPRESA_NOMBRE, idempresa);
        }

        #endregion

        #region Configuracion - Modulo
        public ModuloPaginacion ListarModuloPaginacion(Modulo modulo, Paginacion paginacion)
        {
            ModuloPaginacion oModuloPaginacion = new ModuloPaginacion();
            oModuloPaginacion.ListaModulo = new ListaModulo();
            oModuloPaginacion.Paginacion = paginacion;
            Modulo oModulo;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_MODULO_PAGINACION);

            oDatabase.AddInParameter(oDbCommand, "@IdEmpresa", DbType.Int32, modulo.Aplicacion.Empresa.IdEmpresa);
            oDatabase.AddInParameter(oDbCommand, "@IdAplicacion", DbType.Int32, modulo.Aplicacion.IdAplicacion);
            oDatabase.AddInParameter(oDbCommand, "@Nombre", DbType.String, modulo.Nombre);
            oDatabase.AddInParameter(oDbCommand, "@Descripcion", DbType.String, modulo.Descripcion);

            oDatabase.AddInParameter(oDbCommand, "@SortType", DbType.String, paginacion.SortType);
            oDatabase.AddInParameter(oDbCommand, "@SortDir", DbType.String, paginacion.SortDir);
            oDatabase.AddInParameter(oDbCommand, "@Page", DbType.Int32, paginacion.Page);
            oDatabase.AddInParameter(oDbCommand, "@RowsPerPage", DbType.Int32, paginacion.RowsPerPage);
            oDatabase.AddOutParameter(oDbCommand, "@RowCount", DbType.Int32, 0);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdModulo = oIDataReader.GetOrdinal("IdModulo");
                int indNombreModulo = oIDataReader.GetOrdinal("NombreModulo");
                int indDescripcionModulo = oIDataReader.GetOrdinal("DescripcionModulo");
                int indIdAplicacion = oIDataReader.GetOrdinal("IdAplicacion");
                int indIdEmpresa = oIDataReader.GetOrdinal("IdEmpresa");
                int indIdEstado = oIDataReader.GetOrdinal("IdEstado");
                int indNombreEmpresa = oIDataReader.GetOrdinal("NombreEmpresa");
                int indNombreEstado = oIDataReader.GetOrdinal("NombreEstado");
                int indNombreAplicacion = oIDataReader.GetOrdinal("NombreAplicacion");
                int indDescripcionAplicacion = oIDataReader.GetOrdinal("DescripcionAplicacion");
                int indAbreviatura = oIDataReader.GetOrdinal("Abreviatura");
                int indUrl = oIDataReader.GetOrdinal("Url");

                while (oIDataReader.Read())
                {
                    oModulo = new Modulo();
                    oModulo.IdModulo = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdModulo]);
                    oModulo.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreModulo]);
                    oModulo.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indDescripcionModulo]);
                    oModulo.Aplicacion = new Aplicacion();
                    oModulo.Aplicacion.IdAplicacion = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdAplicacion]);
                    oModulo.Aplicacion.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreAplicacion]);
                    oModulo.Aplicacion.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indDescripcionAplicacion]);
                    oModulo.Aplicacion.Url = DataUtil.DbValueToDefault<String>(oIDataReader[indUrl]);
                    oModulo.Aplicacion.Empresa = new Empresa();
                    oModulo.Aplicacion.Empresa.IdEmpresa = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEmpresa]);
                    oModulo.Aplicacion.Empresa.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreEmpresa]);
                    oModulo.Aplicacion.Empresa.Abreviatura = DataUtil.DbValueToDefault<String>(oIDataReader[indAbreviatura]);
                    oModulo.Estado = new Estado();
                    oModulo.Estado.IdEstado = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEstado]);
                    oModulo.Estado.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreEstado]);
                    oModuloPaginacion.ListaModulo.Add(oModulo);
                }
            }
            oModuloPaginacion.Paginacion.RowCount = Convert.ToInt32(oDatabase.GetParameterValue(oDbCommand, "@RowCount"));
            return oModuloPaginacion;
        }

        public Modulo BuscarModulo(Modulo modulo)
        {
            Modulo oModulo = new Modulo();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_MODULO_BUSCAR);

            oDatabase.AddInParameter(oDbCommand, "@IdModulo", DbType.Int32, modulo.IdModulo);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdModulo = oIDataReader.GetOrdinal("IdModulo");
                int indNombreModulo = oIDataReader.GetOrdinal("NombreModulo");
                int indDescripcionModulo = oIDataReader.GetOrdinal("DescripcionModulo");
                int indIdAplicacion = oIDataReader.GetOrdinal("IdAplicacion");
                int indIdEstado = oIDataReader.GetOrdinal("IdEstado");
                int indNombreEstado = oIDataReader.GetOrdinal("NombreEstado");
                int indNombreAplicacion = oIDataReader.GetOrdinal("NombreAplicacion");
                int indDescripcionAplicacion = oIDataReader.GetOrdinal("DescripcionAplicacion");
                int indUrl = oIDataReader.GetOrdinal("Url");

                while (oIDataReader.Read())
                {
                    oModulo.IdModulo = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdModulo]);
                    oModulo.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreModulo]);
                    oModulo.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indDescripcionModulo]);
                    oModulo.Aplicacion = new Aplicacion();
                    oModulo.Aplicacion.IdAplicacion = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdAplicacion]);
                    oModulo.Aplicacion.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreAplicacion]);
                    oModulo.Aplicacion.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indDescripcionAplicacion]);
                    oModulo.Aplicacion.Url = DataUtil.DbValueToDefault<String>(oIDataReader[indUrl]);
                    oModulo.Estado = new Estado();
                    oModulo.Estado.IdEstado = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEstado]);
                    oModulo.Estado.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreEstado]);
                }
            }
            return oModulo;
        }

        public int Insertar_Modulo(Modulo modulo)
        {
            try
            {
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_MODULO_INSERTAR);

                oDatabase.AddInParameter(oDbCommand, "@IdAplicacion", DbType.Int32, modulo.Aplicacion.IdAplicacion);
                oDatabase.AddInParameter(oDbCommand, "@Nombre", DbType.String, modulo.Nombre);
                oDatabase.AddInParameter(oDbCommand, "@Descripcion", DbType.String, modulo.Descripcion);

                return (int)oDatabase.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Actualizar_Modulo(Modulo modulo)
        {
            try
            {
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_MODULO_ACTUALIZAR);

                oDatabase.AddInParameter(oDbCommand, "@IdModulo", DbType.Int32, modulo.IdModulo);
                oDatabase.AddInParameter(oDbCommand, "@IdAplicacion", DbType.Int32, modulo.Aplicacion.IdAplicacion);
                oDatabase.AddInParameter(oDbCommand, "@Nombre", DbType.String, modulo.Nombre);
                oDatabase.AddInParameter(oDbCommand, "@Descripcion", DbType.String, modulo.Descripcion);

                return oDatabase.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int ActualizarEstado_Modulo(Modulo modulo)
        {
            try
            {
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_MODULO_ACTUALIZARESTADO);

                oDatabase.AddInParameter(oDbCommand, "@IdModulo", DbType.Int32, modulo.IdModulo);
                oDatabase.AddInParameter(oDbCommand, "@IdEstado", DbType.String, modulo.Estado.IdEstado);

                return oDatabase.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public ListaModulo ListarModulo()
        {
            ListaModulo oListaModulo = new ListaModulo();
            Modulo oModulo;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_MODULO_LISTAR);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdModulo = oIDataReader.GetOrdinal("IdModulo");
                int indNombreModulo = oIDataReader.GetOrdinal("NombreModulo");
                int indDescripcionModulo = oIDataReader.GetOrdinal("DescripcionModulo");
                int indIdAplicacion = oIDataReader.GetOrdinal("IdAplicacion");
                int indIdEstado = oIDataReader.GetOrdinal("IdEstado");
                int indNombreEstado = oIDataReader.GetOrdinal("NombreEstado");
                int indNombreAplicacion = oIDataReader.GetOrdinal("NombreAplicacion");
                int indDescripcionAplicacion = oIDataReader.GetOrdinal("DescripcionAplicacion");
                int indUrl = oIDataReader.GetOrdinal("Url");

                while (oIDataReader.Read())
                {
                    oModulo = new Modulo();
                    oModulo.IdModulo = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdModulo]);
                    oModulo.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreModulo]);
                    oModulo.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indDescripcionModulo]);
                    oModulo.Aplicacion = new Aplicacion();
                    oModulo.Aplicacion.IdAplicacion = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdAplicacion]);
                    oModulo.Aplicacion.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreAplicacion]);
                    oModulo.Aplicacion.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indDescripcionAplicacion]);
                    oModulo.Aplicacion.Url = DataUtil.DbValueToDefault<String>(oIDataReader[indUrl]);
                    oModulo.Estado = new Estado();
                    oModulo.Estado.IdEstado = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEstado]);
                    oModulo.Estado.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreEstado]);
                    oListaModulo.Add(oModulo);
                }
            }
            return oListaModulo;
        }

        public ListaModulo ModuloPorAplicacion(Modulo modulo)
        {
            ListaModulo oListaModulo = new ListaModulo();
            Modulo oModulo;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_MODULOPORAPLICACION);

            oDatabase.AddInParameter(oDbCommand, "@IdAplicacion", DbType.Int32, modulo.Aplicacion.IdAplicacion);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdModulo = oIDataReader.GetOrdinal("IdModulo");
                int indNombreModulo = oIDataReader.GetOrdinal("NombreModulo");
                int indDescripcionModulo = oIDataReader.GetOrdinal("DescripcionModulo");
                int indIdAplicacion = oIDataReader.GetOrdinal("IdAplicacion");
                int indIdEstado = oIDataReader.GetOrdinal("IdEstado");
                int indNombreEstado = oIDataReader.GetOrdinal("NombreEstado");
                int indNombreAplicacion = oIDataReader.GetOrdinal("NombreAplicacion");
                int indDescripcionAplicacion = oIDataReader.GetOrdinal("DescripcionAplicacion");
                int indUrl = oIDataReader.GetOrdinal("Url");

                while (oIDataReader.Read())
                {
                    oModulo = new Modulo();
                    oModulo.IdModulo = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdModulo]);
                    oModulo.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreModulo]);
                    oModulo.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indDescripcionModulo]);
                    oModulo.Aplicacion = new Aplicacion();
                    oModulo.Aplicacion.IdAplicacion = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdAplicacion]);
                    oModulo.Aplicacion.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreAplicacion]);
                    oModulo.Aplicacion.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indDescripcionAplicacion]);
                    oModulo.Aplicacion.Url = DataUtil.DbValueToDefault<String>(oIDataReader[indUrl]);
                    oModulo.Estado = new Estado();
                    oModulo.Estado.IdEstado = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEstado]);
                    oModulo.Estado.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreEstado]);
                    oListaModulo.Add(oModulo);
                }
            }
            return oListaModulo;
        }


        public ListaAgrupacion getListAgrupacionModulo(int IdModulo)
        {
            Agrupacion oAgrupacion = new Agrupacion();
            ListaAgrupacion oListaAgrupacion = new ListaAgrupacion();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.Usp_geListAgrupacion);

            oDatabase.AddInParameter(oDbCommand, "@IdModulo", DbType.Int32,IdModulo);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int iIdAgrupacion = oIDataReader.GetOrdinal("IdAgrupacion");
                int vDescripcion = oIDataReader.GetOrdinal("Descripcion");

                while (oIDataReader.Read())
                {
                    oAgrupacion = new Agrupacion();
                    oAgrupacion.IdAgrupacion = DataUtil.DbValueToDefault<Int32>(oIDataReader[iIdAgrupacion]);
                    oAgrupacion.IdModulo = IdModulo;
                    oAgrupacion.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[vDescripcion]);
                    oListaAgrupacion.Add(oAgrupacion);
                }
            }
            return oListaAgrupacion;
        }

        #endregion

        #region Configuracion - Pagina
        public PaginaPaginacion ListarPaginaPaginacion(Pagina pagina, Paginacion paginacion)
        {
            PaginaPaginacion oPaginaPaginacion = new PaginaPaginacion();
            oPaginaPaginacion.ListaPagina = new ListaPagina();
            oPaginaPaginacion.Paginacion = paginacion;
            Pagina oPagina;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_PAGINA_PAGINACION);

            oDatabase.AddInParameter(oDbCommand, "@IdEmpresa", DbType.Int32, pagina.Modulo.Aplicacion.Empresa.IdEmpresa);
            oDatabase.AddInParameter(oDbCommand, "@IdAplicacion", DbType.Int32, pagina.Modulo.Aplicacion.IdAplicacion);
            oDatabase.AddInParameter(oDbCommand, "@IdModulo", DbType.Int32, pagina.Modulo.IdModulo);
            oDatabase.AddInParameter(oDbCommand, "@Nombre", DbType.String, pagina.Nombre);

            oDatabase.AddInParameter(oDbCommand, "@SortType", DbType.String, paginacion.SortType);
            oDatabase.AddInParameter(oDbCommand, "@SortDir", DbType.String, paginacion.SortDir);
            oDatabase.AddInParameter(oDbCommand, "@Page", DbType.Int32, paginacion.Page);
            oDatabase.AddInParameter(oDbCommand, "@RowsPerPage", DbType.Int32, paginacion.RowsPerPage);
            oDatabase.AddOutParameter(oDbCommand, "@RowCount", DbType.Int32, 0);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdPagina = oIDataReader.GetOrdinal("IdPagina");
                int indIdPaginaPadre = oIDataReader.GetOrdinal("IdPaginaPadre");
                int indUrl = oIDataReader.GetOrdinal("Url");
                int indNombrePagina = oIDataReader.GetOrdinal("NombrePagina");
                int indIcono = oIDataReader.GetOrdinal("Icono");
                int indNivel = oIDataReader.GetOrdinal("Nivel");
                int indOrden = oIDataReader.GetOrdinal("Orden");
                int indVisible = oIDataReader.GetOrdinal("Visible");
                int indIdModulo = oIDataReader.GetOrdinal("IdModulo");
                int indNombreModulo = oIDataReader.GetOrdinal("NombreModulo");
                int indDescripcionModulo = oIDataReader.GetOrdinal("DescripcionModulo");
                int indIdAplicacion = oIDataReader.GetOrdinal("IdAplicacion");
                int indIdEmpresa = oIDataReader.GetOrdinal("IdEmpresa");
                int indIdEstado = oIDataReader.GetOrdinal("IdEstado");
                int indNombreEmpresa = oIDataReader.GetOrdinal("NombreEmpresa");
                int indNombreEstado = oIDataReader.GetOrdinal("NombreEstado");
                int indNombreAplicacion = oIDataReader.GetOrdinal("NombreAplicacion");
                int indDescripcionAplicacion = oIDataReader.GetOrdinal("DescripcionAplicacion");
                int indAbreviaturaEmpresa = oIDataReader.GetOrdinal("AbreviaturaEmpresa");
                int indDescripcionAgrupacion = oIDataReader.GetOrdinal("DescripcionAgrupacion");

                while (oIDataReader.Read())
                {
                    oPagina = new Pagina();
                    oPagina.IdPagina = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdPagina]);
                    oPagina.IdPaginaPadre = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdPaginaPadre]);
                    oPagina.Url = DataUtil.DbValueToDefault<String>(oIDataReader[indUrl]);
                    oPagina.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombrePagina]);
                    oPagina.Icono = DataUtil.DbValueToDefault<String>(oIDataReader[indIcono]);
                    oPagina.Nivel = DataUtil.DbValueToDefault<Int32>(oIDataReader[indNivel]);
                    oPagina.Orden = DataUtil.DbValueToDefault<Int32>(oIDataReader[indOrden]);
                    oPagina.Visible = DataUtil.DbValueToDefault<Boolean>(oIDataReader[indVisible]);
                    oPagina.Modulo = new Modulo();
                    oPagina.Modulo.IdModulo = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdModulo]);
                    oPagina.Modulo.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreModulo]);
                    oPagina.Modulo.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indDescripcionModulo]);
                    oPagina.Modulo.Aplicacion = new Aplicacion();
                    oPagina.Modulo.Aplicacion.IdAplicacion = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdAplicacion]);
                    oPagina.Modulo.Aplicacion.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreAplicacion]);
                    oPagina.Modulo.Aplicacion.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indDescripcionAplicacion]);
                    oPagina.Modulo.Aplicacion.Url = DataUtil.DbValueToDefault<String>(oIDataReader[indUrl]);
                    oPagina.Modulo.Aplicacion.Empresa = new Empresa();
                    oPagina.Modulo.Aplicacion.Empresa.IdEmpresa = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEmpresa]);
                    oPagina.Modulo.Aplicacion.Empresa.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreEmpresa]);
                    oPagina.Modulo.Aplicacion.Empresa.Abreviatura = DataUtil.DbValueToDefault<String>(oIDataReader[indAbreviaturaEmpresa]);
                    oPagina.Estado = new Estado();
                    oPagina.Estado.IdEstado = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEstado]);
                    oPagina.Estado.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreEstado]);
                    oPagina.DescripcionAgrupacion = DataUtil.DbValueToDefault<string>(oIDataReader[indDescripcionAgrupacion]);
                    oPaginaPaginacion.ListaPagina.Add(oPagina);
                }
            }
            oPaginaPaginacion.Paginacion.RowCount = Convert.ToInt32(oDatabase.GetParameterValue(oDbCommand, "@RowCount"));
            return oPaginaPaginacion;
        }

        public Pagina BuscarPagina(Pagina pagina)
        {
            Pagina oPagina = new Pagina();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_PAGINA_BUSCAR);

            oDatabase.AddInParameter(oDbCommand, "@IdPagina", DbType.Int32, pagina.IdPagina);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdPagina = oIDataReader.GetOrdinal("IdPagina");
                int indIdPaginaPadre = oIDataReader.GetOrdinal("IdPaginaPadre");
                int indUrl = oIDataReader.GetOrdinal("Url");
                int indNombrePagina = oIDataReader.GetOrdinal("NombrePagina");
                int indIcono = oIDataReader.GetOrdinal("Icono");
                int indNivel = oIDataReader.GetOrdinal("Nivel");
                int indOrden = oIDataReader.GetOrdinal("Orden");
                int indVisible = oIDataReader.GetOrdinal("Visible");
                int indIdModulo = oIDataReader.GetOrdinal("IdModulo");
                int indNombreModulo = oIDataReader.GetOrdinal("NombreModulo");
                int indDescripcionModulo = oIDataReader.GetOrdinal("DescripcionModulo");
                int indIdAplicacion = oIDataReader.GetOrdinal("IdAplicacion");
                int indIdEstado = oIDataReader.GetOrdinal("IdEstado");
                int indNombreEstado = oIDataReader.GetOrdinal("NombreEstado");
                int indNombreAplicacion = oIDataReader.GetOrdinal("NombreAplicacion");
                int indDescripcionAplicacion = oIDataReader.GetOrdinal("DescripcionAplicacion");

                int indIdEmpresa = oIDataReader.GetOrdinal("IdEmpresa");
                int iIdAgrupacion = oIDataReader.GetOrdinal("IdAgrupacion");
                int iIdTamanoMenu = oIDataReader.GetOrdinal("IdTamanoMenu");

                while (oIDataReader.Read())
                {
                    oPagina.IdPagina = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdPagina]);
                    oPagina.IdPaginaPadre = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdPaginaPadre]);
                    oPagina.Url = DataUtil.DbValueToDefault<String>(oIDataReader[indUrl]);
                    oPagina.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombrePagina]);
                    oPagina.Icono = DataUtil.DbValueToDefault<String>(oIDataReader[indIcono]);
                    oPagina.Nivel = DataUtil.DbValueToDefault<Int32>(oIDataReader[indNivel]);
                    oPagina.Orden = DataUtil.DbValueToDefault<Int32>(oIDataReader[indOrden]);
                    oPagina.Visible = DataUtil.DbValueToDefault<Boolean>(oIDataReader[indVisible]);
                    oPagina.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombrePagina]);
                    oPagina.Modulo = new Modulo();
                    oPagina.Modulo.IdModulo = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdModulo]);
                    oPagina.Modulo.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreModulo]);
                    oPagina.Modulo.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indDescripcionModulo]);
                    oPagina.Modulo.Aplicacion = new Aplicacion();
                    oPagina.Modulo.Aplicacion.IdAplicacion = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdAplicacion]);
                    oPagina.Modulo.Aplicacion.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreAplicacion]);
                    oPagina.Modulo.Aplicacion.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indDescripcionAplicacion]);
                    oPagina.Modulo.Aplicacion.Url = DataUtil.DbValueToDefault<String>(oIDataReader[indUrl]);
                    oPagina.Estado = new Estado();
                    oPagina.Estado.IdEstado = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEstado]);
                    oPagina.Estado.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreEstado]);

                    oPagina.Modulo.Aplicacion.Empresa = new Empresa();
                    oPagina.Modulo.Aplicacion.Empresa.IdEmpresa = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEmpresa]);

                    oPagina.IdAgrupacion = DataUtil.DbValueToDefault<Int32>(oIDataReader[iIdAgrupacion]);
                    oPagina.IdTamanoMenu = DataUtil.DbValueToDefault<Int32>(oIDataReader[iIdTamanoMenu]);
                }
            }
            return oPagina;
        }

        public int Insertar_Pagina(Pagina pagina)
        {
            try
            {
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_PAGINA_INSERTAR);

                oDatabase.AddInParameter(oDbCommand, "@IdModulo", DbType.Int32, pagina.Modulo.IdModulo);
                oDatabase.AddInParameter(oDbCommand, "@IdPaginaPadre", DbType.Int32, pagina.IdPaginaPadre);
                oDatabase.AddInParameter(oDbCommand, "@Url", DbType.String, pagina.Url);
                oDatabase.AddInParameter(oDbCommand, "@Nombre", DbType.String, pagina.Nombre);
                oDatabase.AddInParameter(oDbCommand, "@Icono", DbType.String, pagina.Icono);
                oDatabase.AddInParameter(oDbCommand, "@Nivel", DbType.String, pagina.Nivel);
                oDatabase.AddInParameter(oDbCommand, "@Orden", DbType.String, pagina.Orden);
                oDatabase.AddInParameter(oDbCommand, "@Visible", DbType.String, pagina.Visible);
                oDatabase.AddOutParameter(oDbCommand, "@IdPagina", DbType.Int32, pagina.IdPagina);
                oDatabase.AddInParameter(oDbCommand, "@IdAgrupacion", DbType.Int32, pagina.IdAgrupacion);
                oDatabase.AddInParameter(oDbCommand, "@IdTamanoMenu", DbType.Int32, pagina.IdTamanoMenu);

                oDatabase.ExecuteNonQuery(oDbCommand);
                return Convert.ToInt32(oDatabase.GetParameterValue(oDbCommand, "@IdPagina").ToString());
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int Actualizar_Pagina(Pagina pagina)
        {
            try
            {
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_PAGINA_ACTUALIZAR);

                oDatabase.AddInParameter(oDbCommand, "@IdPagina", DbType.Int32, pagina.IdPagina);
                oDatabase.AddInParameter(oDbCommand, "@IdModulo", DbType.Int32, pagina.Modulo.IdModulo);
                oDatabase.AddInParameter(oDbCommand, "@IdPaginaPadre", DbType.Int32, pagina.IdPaginaPadre);
                oDatabase.AddInParameter(oDbCommand, "@Url", DbType.String, pagina.Url);
                oDatabase.AddInParameter(oDbCommand, "@Nombre", DbType.String, pagina.Nombre);
                oDatabase.AddInParameter(oDbCommand, "@Icono", DbType.String, pagina.Icono);
                oDatabase.AddInParameter(oDbCommand, "@Nivel", DbType.String, pagina.Nivel);
                oDatabase.AddInParameter(oDbCommand, "@Orden", DbType.String, pagina.Orden);
                oDatabase.AddInParameter(oDbCommand, "@Visible", DbType.String, pagina.Visible);
                oDatabase.AddInParameter(oDbCommand, "@IdAgrupacion", DbType.Int32, pagina.IdAgrupacion);
                oDatabase.AddInParameter(oDbCommand, "@IdTamanoMenu", DbType.Int32, pagina.IdTamanoMenu);

                return oDatabase.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int ActualizarEstado_Pagina(Pagina pagina)
        {
            try
            {
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_PAGINA_ACTUALIZARESTADO);

                oDatabase.AddInParameter(oDbCommand, "@IdPagina", DbType.Int32, pagina.IdPagina);
                oDatabase.AddInParameter(oDbCommand, "@IdEstado", DbType.String, pagina.Estado.IdEstado);

                return oDatabase.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public ListaPagina ListarPagina()
        {
            ListaPagina oListaPagina = new ListaPagina();
            Pagina oPagina;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_PAGINA_LISTAR);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdPagina = oIDataReader.GetOrdinal("IdPagina");
                int indIdPaginaPadre = oIDataReader.GetOrdinal("IdPaginaPadre");
                int indUrl = oIDataReader.GetOrdinal("Url");
                int indNombrePagina = oIDataReader.GetOrdinal("NombrePagina");
                int indIcono = oIDataReader.GetOrdinal("Icono");
                int indNivel = oIDataReader.GetOrdinal("Nivel");
                int indOrden = oIDataReader.GetOrdinal("Orden");
                int indVisible = oIDataReader.GetOrdinal("Visible");
                int indIdModulo = oIDataReader.GetOrdinal("IdModulo");
                int indNombreModulo = oIDataReader.GetOrdinal("NombreModulo");
                int indDescripcionModulo = oIDataReader.GetOrdinal("DescripcionModulo");
                int indIdAplicacion = oIDataReader.GetOrdinal("IdAplicacion");
                int indIdEstado = oIDataReader.GetOrdinal("IdEstado");
                int indNombreEstado = oIDataReader.GetOrdinal("NombreEstado");
                int indNombreAplicacion = oIDataReader.GetOrdinal("NombreAplicacion");
                int indDescripcionAplicacion = oIDataReader.GetOrdinal("DescripcionAplicacion");

                while (oIDataReader.Read())
                {
                    oPagina = new Pagina();
                    oPagina.IdPagina = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdPagina]);
                    oPagina.IdPaginaPadre = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdPaginaPadre]);
                    oPagina.Url = DataUtil.DbValueToDefault<String>(oIDataReader[indUrl]);
                    oPagina.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombrePagina]);
                    oPagina.Icono = DataUtil.DbValueToDefault<String>(oIDataReader[indIcono]);
                    oPagina.Nivel = DataUtil.DbValueToDefault<Int32>(oIDataReader[indNivel]);
                    oPagina.Orden = DataUtil.DbValueToDefault<Int32>(oIDataReader[indOrden]);
                    oPagina.Visible = DataUtil.DbValueToDefault<Boolean>(oIDataReader[indVisible]);
                    oPagina.Modulo = new Modulo();
                    oPagina.Modulo.IdModulo = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdModulo]);
                    oPagina.Modulo.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreModulo]);
                    oPagina.Modulo.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indDescripcionModulo]);
                    oPagina.Modulo.Aplicacion = new Aplicacion();
                    oPagina.Modulo.Aplicacion.IdAplicacion = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdAplicacion]);
                    oPagina.Modulo.Aplicacion.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreAplicacion]);
                    oPagina.Modulo.Aplicacion.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indDescripcionAplicacion]);
                    oPagina.Modulo.Aplicacion.Url = DataUtil.DbValueToDefault<String>(oIDataReader[indUrl]);
                    oPagina.Estado = new Estado();
                    oPagina.Estado.IdEstado = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEstado]);
                    oPagina.Estado.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreEstado]);
                    oListaPagina.Add(oPagina);
                }
            }
            return oListaPagina;
        }

        public ListaPagina PaginaPorModulo(Pagina pagina)
        {
            ListaPagina oListaPagina = new ListaPagina();
            Pagina oPagina;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_MODULOPORAPLICACION);

            oDatabase.AddInParameter(oDbCommand, "@IdModulo", DbType.Int32, pagina.Modulo.IdModulo);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdPagina = oIDataReader.GetOrdinal("IdPagina");
                int indIdPaginaPadre = oIDataReader.GetOrdinal("IdPaginaPadre");
                int indUrl = oIDataReader.GetOrdinal("Url");
                int indNombrePagina = oIDataReader.GetOrdinal("NombrePagina");
                int indIcono = oIDataReader.GetOrdinal("Icono");
                int indNivel = oIDataReader.GetOrdinal("Nivel");
                int indOrden = oIDataReader.GetOrdinal("Orden");
                int indVisible = oIDataReader.GetOrdinal("Visible");
                int indIdModulo = oIDataReader.GetOrdinal("IdModulo");
                int indNombreModulo = oIDataReader.GetOrdinal("NombreModulo");
                int indDescripcionModulo = oIDataReader.GetOrdinal("DescripcionModulo");
                int indIdAplicacion = oIDataReader.GetOrdinal("IdAplicacion");
                int indIdEmpresa = oIDataReader.GetOrdinal("IdEmpresa");
                int indIdEstado = oIDataReader.GetOrdinal("IdEstado");
                int indNombreEmpresa = oIDataReader.GetOrdinal("NombreEmpresa");
                int indNombreEstado = oIDataReader.GetOrdinal("NombreEstado");
                int indNombreAplicacion = oIDataReader.GetOrdinal("NombreAplicacion");
                int indDescripcionAplicacion = oIDataReader.GetOrdinal("DescripcionAplicacion");
                int indAbreviaturaEmpresa = oIDataReader.GetOrdinal("AbreviaturaEmpresa");
                int indIdGrupo = oIDataReader.GetOrdinal("IdGrupo");
                int indNombreGrupo = oIDataReader.GetOrdinal("NombreGrupo");
                int indAbreviaturaGrupo = oIDataReader.GetOrdinal("AbreviaturaGrupo");

                while (oIDataReader.Read())
                {
                    oPagina = new Pagina();
                    oPagina.IdPagina = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdPagina]);
                    oPagina.IdPaginaPadre = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdPaginaPadre]);
                    oPagina.Url = DataUtil.DbValueToDefault<String>(oIDataReader[indUrl]);
                    oPagina.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombrePagina]);
                    oPagina.Icono = DataUtil.DbValueToDefault<String>(oIDataReader[indIcono]);
                    oPagina.Nivel = DataUtil.DbValueToDefault<Int32>(oIDataReader[indNivel]);
                    oPagina.Orden = DataUtil.DbValueToDefault<Int32>(oIDataReader[indOrden]);
                    oPagina.Visible = DataUtil.DbValueToDefault<Boolean>(oIDataReader[indVisible]);
                    oPagina.Modulo = new Modulo();
                    oPagina.Modulo.IdModulo = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdModulo]);
                    oPagina.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreModulo]);
                    oPagina.Modulo = new Modulo();
                    oPagina.Modulo.IdModulo = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdModulo]);
                    oPagina.Modulo.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreModulo]);
                    oPagina.Modulo.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indDescripcionModulo]);
                    oPagina.Modulo.Aplicacion = new Aplicacion();
                    oPagina.Modulo.Aplicacion.IdAplicacion = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdAplicacion]);
                    oPagina.Modulo.Aplicacion.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreAplicacion]);
                    oPagina.Modulo.Aplicacion.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indDescripcionAplicacion]);
                    oPagina.Modulo.Aplicacion.Url = DataUtil.DbValueToDefault<String>(oIDataReader[indUrl]);
                    oPagina.Modulo.Aplicacion.Empresa = new Empresa();
                    oPagina.Modulo.Aplicacion.Empresa.IdEmpresa = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEmpresa]);
                    oPagina.Modulo.Aplicacion.Empresa.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreEmpresa]);
                    oPagina.Modulo.Aplicacion.Empresa.Abreviatura = DataUtil.DbValueToDefault<String>(oIDataReader[indAbreviaturaEmpresa]);
                    oPagina.Estado = new Estado();
                    oPagina.Estado.IdEstado = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEstado]);
                    oPagina.Estado.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreEstado]);
                    oPagina.Grupo = new Grupo();
                    oPagina.Grupo.IdGrupo = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdGrupo]);
                    oListaPagina.Add(oPagina);
                }
            }
            return oListaPagina;
        }

        public ListaPagina PaginaPadreListar(Pagina pagina)
        {
            ListaPagina oListaPagina = new ListaPagina();
            Pagina oPagina;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_PAGINAPADRE_LISTAR);

            oDatabase.AddInParameter(oDbCommand, "@IdModulo", DbType.Int32, pagina.Modulo.IdModulo);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdPagina = oIDataReader.GetOrdinal("IdPagina");
                int indIdPaginaPadre = oIDataReader.GetOrdinal("IdPaginaPadre");
                int indUrl = oIDataReader.GetOrdinal("Url");
                int indNombrePagina = oIDataReader.GetOrdinal("NombrePagina");
                int indIcono = oIDataReader.GetOrdinal("Icono");
                int indNivel = oIDataReader.GetOrdinal("Nivel");
                int indOrden = oIDataReader.GetOrdinal("Orden");
                int indVisible = oIDataReader.GetOrdinal("Visible");
                int indIdModulo = oIDataReader.GetOrdinal("IdModulo");

                while (oIDataReader.Read())
                {
                    oPagina = new Pagina();
                    oPagina.IdPagina = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdPagina]);
                    oPagina.IdPaginaPadre = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdPaginaPadre]);
                    oPagina.Url = DataUtil.DbValueToDefault<String>(oIDataReader[indUrl]);
                    oPagina.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombrePagina]);
                    oPagina.Icono = DataUtil.DbValueToDefault<String>(oIDataReader[indIcono]);
                    oPagina.Nivel = DataUtil.DbValueToDefault<Int32>(oIDataReader[indNivel]);
                    oPagina.Orden = DataUtil.DbValueToDefault<Int32>(oIDataReader[indOrden]);
                    oPagina.Visible = DataUtil.DbValueToDefault<Boolean>(oIDataReader[indVisible]);
                    oPagina.Modulo = new Modulo();
                    oPagina.Modulo.IdModulo = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdModulo]);
                    oListaPagina.Add(oPagina);
                }
            }
            return oListaPagina;
        }

        #endregion

        #region Acción
        public PaginaAccionPaginacion ListarAccionPaginacion(PaginaAccion oPaginaAccion, Paginacion paginacion)
        {
            PaginaAccionPaginacion oPaginaAccionPaginacion = new PaginaAccionPaginacion();
            oPaginaAccionPaginacion.ListaPaginaAccion = new ListaPaginaAccion();
            oPaginaAccionPaginacion.Paginacion = paginacion;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_ACCION_PAGINACION);

            oDatabase.AddInParameter(oDbCommand, "@IdPagina", DbType.Int32, oPaginaAccion.Pagina.IdPagina);
            oDatabase.AddInParameter(oDbCommand, "@IdsGrupo", DbType.String, oPaginaAccion.Pagina.Grupo.IdsGrupo);

            oDatabase.AddInParameter(oDbCommand, "@SortType", DbType.String, paginacion.SortType);
            oDatabase.AddInParameter(oDbCommand, "@SortDir", DbType.String, paginacion.SortDir);
            oDatabase.AddInParameter(oDbCommand, "@Page", DbType.Int32, paginacion.Page);
            oDatabase.AddInParameter(oDbCommand, "@RowsPerPage", DbType.Int32, paginacion.RowsPerPage);
            oDatabase.AddOutParameter(oDbCommand, "@RowCount", DbType.Int32, 0);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdPaginaAccion = oIDataReader.GetOrdinal("IdPaginaAccion");
                int indChkAgregar = oIDataReader.GetOrdinal("ChkAgregar");
                int indIdAccion = oIDataReader.GetOrdinal("IdAccion");
                int indCodigoHTML = oIDataReader.GetOrdinal("CodigoHTML");
                int indNombreAccion = oIDataReader.GetOrdinal("NombreAccion");
                int indEtiqueta = oIDataReader.GetOrdinal("Etiqueta");
                int indMensajeToolTip = oIDataReader.GetOrdinal("MensajeToolTip");
                int indIdEstado = oIDataReader.GetOrdinal("IdEstado");
                int indIdGrupo = oIDataReader.GetOrdinal("IdGrupo");
                int indCodigoGrupo = oIDataReader.GetOrdinal("CodigoGrupo");
                int indNombreGrupo = oIDataReader.GetOrdinal("NombreGrupo");
                int indAbreviatura = oIDataReader.GetOrdinal("Abreviatura");

                while (oIDataReader.Read())
                {
                    oPaginaAccion = new PaginaAccion();
                    oPaginaAccion.IdPaginaAccion = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdPaginaAccion]);
                    oPaginaAccion.ChkAgregar = DataUtil.DbValueToDefault<Boolean>(oIDataReader[indChkAgregar]);
                    oPaginaAccion.Accion = new Accion();
                    oPaginaAccion.Accion.IdAccion = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdAccion]);
                    oPaginaAccion.Accion.CodigoHTML = DataUtil.DbValueToDefault<String>(oIDataReader[indCodigoHTML]);
                    oPaginaAccion.Accion.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreAccion]);
                    oPaginaAccion.Accion.Etiqueta = DataUtil.DbValueToDefault<String>(oIDataReader[indEtiqueta]);
                    oPaginaAccion.Accion.MensajeToolTip = DataUtil.DbValueToDefault<String>(oIDataReader[indMensajeToolTip]);
                    oPaginaAccion.Accion.Grupo = new Grupo();
                    oPaginaAccion.Accion.Grupo.IdGrupo = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdGrupo]);
                    oPaginaAccion.Accion.Grupo.Codigo = DataUtil.DbValueToDefault<String>(oIDataReader[indCodigoGrupo]);
                    oPaginaAccion.Accion.Grupo.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreGrupo]);
                    oPaginaAccion.Accion.Grupo.Abreviatura = DataUtil.DbValueToDefault<String>(oIDataReader[indAbreviatura]);
                    oPaginaAccionPaginacion.ListaPaginaAccion.Add(oPaginaAccion);
                }
            }
            oPaginaAccionPaginacion.Paginacion.RowCount = Convert.ToInt32(oDatabase.GetParameterValue(oDbCommand, "@RowCount"));
            return oPaginaAccionPaginacion;
        }

        public ListaPaginaAccion BuscarPaginaAccion(PaginaAccion oPaginaAccion)
        {
            ListaPaginaAccion oListaPaginaAccion = new ListaPaginaAccion();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_ACCION_BUSCAR);

            oDatabase.AddInParameter(oDbCommand, "@IdPagina", DbType.Int32, oPaginaAccion.Pagina.IdPagina);
            oDatabase.AddInParameter(oDbCommand, "@IdsGrupo", DbType.String, oPaginaAccion.Pagina.Grupo.IdsGrupo);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdPagina = oIDataReader.GetOrdinal("IdPagina");
                int indIdPaginaAccion = oIDataReader.GetOrdinal("IdPaginaAccion");
                int indChkAgregar = oIDataReader.GetOrdinal("ChkAgregar");
                int indIdAccion = oIDataReader.GetOrdinal("IdAccion");
                int indCodigoHTML = oIDataReader.GetOrdinal("CodigoHTML");
                int indNombreAccion = oIDataReader.GetOrdinal("NombreAccion");
                int indEtiqueta = oIDataReader.GetOrdinal("Etiqueta");
                int indMensajeToolTip = oIDataReader.GetOrdinal("MensajeToolTip");
                int indIdEstado = oIDataReader.GetOrdinal("IdEstado");
                int indNombreEstado = oIDataReader.GetOrdinal("NombreEstado");

                while (oIDataReader.Read())
                {
                    oPaginaAccion = new PaginaAccion();
                    oPaginaAccion.Pagina = new Pagina();
                    oPaginaAccion.Pagina.IdPagina = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdPagina]);                    
                    oPaginaAccion.IdPaginaAccion = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdPaginaAccion]);
                    oPaginaAccion.ChkAgregar = DataUtil.DbValueToDefault<Boolean>(oIDataReader[indChkAgregar]);
                    oPaginaAccion.Accion = new Accion();
                    oPaginaAccion.Accion.IdAccion = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdAccion]);
                    oPaginaAccion.Accion.CodigoHTML = DataUtil.DbValueToDefault<String>(oIDataReader[indCodigoHTML]);
                    oPaginaAccion.Accion.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreAccion]);
                    oPaginaAccion.Accion.Etiqueta = DataUtil.DbValueToDefault<String>(oIDataReader[indEtiqueta]);
                    oPaginaAccion.Accion.MensajeToolTip = DataUtil.DbValueToDefault<String>(oIDataReader[indMensajeToolTip]);
                    oListaPaginaAccion.Add(oPaginaAccion);
                }
            }

            return oListaPaginaAccion;
        }

        public int AsignarPaginaAccion(ListaPaginaAccion listaPaginaAccion)
        {
            try
            {
                DbCommand cmd = oSqlDatabase.GetStoredProcCommand(Procedimientos.USP_PAGINAACCION_INSERTAR);
                oSqlDatabase.AddInParameter(cmd, "@TypePaginaAccion", SqlDbType.Structured, PaginaAccionDetalle(listaPaginaAccion));
                return Convert.ToInt32(oSqlDatabase.ExecuteScalar(cmd));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public IEnumerable<SqlDataRecord> PaginaAccionDetalle(ListaPaginaAccion listaPaginaAccion)
        {

            List<SqlDataRecord> records = new List<SqlDataRecord>();
            foreach (var item in listaPaginaAccion)
            {

                SqlDataRecord record = new SqlDataRecord(
                    new SqlMetaData[]{  new SqlMetaData("IdPaginaAccion", SqlDbType.Int),
                                        new SqlMetaData("IdPagina", SqlDbType.Int),
                                        new SqlMetaData("IdAccion", SqlDbType.Int),
                                        new SqlMetaData("ChkAgregar", SqlDbType.Bit)
                                    });
                record.SetInt32(0, item.IdPaginaAccion);
                record.SetInt32(1, item.Pagina.IdPagina);
                record.SetInt32(2, item.Accion.IdAccion);
                record.SetBoolean(3, item.ChkAgregar);
                records.Add(record);
            }

            return records;
        }

        public int PaginaAccion_Eliminar(Pagina pagina)
        {
            try
            {
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_PAGINAACCION_ELIMINAR);
                oDatabase.AddInParameter(oDbCommand, "@IdPagina", DbType.Int32, pagina.IdPagina);

                oDatabase.ExecuteNonQuery(oDbCommand);
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        #endregion

        #region Grupo
        public ListaGrupo ListarGrupo()
        {
            ListaGrupo oListaGrupo = new ListaGrupo();
            Grupo oGrupo;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_GRUPO_LISTAR);
            
            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int IndIdGrupo = oIDataReader.GetOrdinal("IdGrupo");
                int indCodigo = oIDataReader.GetOrdinal("Codigo");
                int indNombre = oIDataReader.GetOrdinal("Nombre");
                int indAbreviatura = oIDataReader.GetOrdinal("Abreviatura");
                int indIdEstado = oIDataReader.GetOrdinal("IdEstado");
                int indNombreEstado = oIDataReader.GetOrdinal("NombreEstado");
                
                while (oIDataReader.Read())
                {
                    oGrupo = new Grupo();
                    oGrupo.IdGrupo = DataUtil.DbValueToDefault<Int32>(oIDataReader[IndIdGrupo]);
                    oGrupo.Codigo = DataUtil.DbValueToDefault<String>(oIDataReader[indCodigo]);
                    oGrupo.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombre]);
                    oGrupo.Abreviatura = DataUtil.DbValueToDefault<String>(oIDataReader[indAbreviatura]);
                    oGrupo.Estado = new Estado();
                    oGrupo.Estado.IdEstado = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEstado]);
                    oGrupo.Estado.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreEstado]);
                    oListaGrupo.Add(oGrupo);
                }
            }
            return oListaGrupo;
        }

        public int AsignarPaginaGrupo(ListaPaginaGrupo listaPaginaGrupo)
        {
            try
            {
                DbCommand cmd = oSqlDatabase.GetStoredProcCommand(Procedimientos.USP_PAGINAGRUPO_INSERTAR);
                oSqlDatabase.AddInParameter(cmd, "@TypePaginaGrupo", SqlDbType.Structured, GrupoPaginaDetalle(listaPaginaGrupo));
                return Convert.ToInt32(oSqlDatabase.ExecuteScalar(cmd));
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public IEnumerable<SqlDataRecord> GrupoPaginaDetalle(ListaPaginaGrupo listaPaginaGrupo)
        {

            List<SqlDataRecord> records = new List<SqlDataRecord>();
            foreach (var item in listaPaginaGrupo)
            {

                SqlDataRecord record = new SqlDataRecord(
                    new SqlMetaData[]{  new SqlMetaData("IdPaginaGrupo", SqlDbType.Int),
                                        new SqlMetaData("IdPagina", SqlDbType.Int),
                                        new SqlMetaData("IdGrupo", SqlDbType.Int)
                                    });
                record.SetInt32(0, 0);
                record.SetInt32(1, item.Pagina.IdPagina);
                record.SetInt32(2, item.Grupo.IdGrupo);
                records.Add(record);
            }

            return records;
        }

        public int PaginaGrupo_Eliminar(Pagina pagina)
        {
            try
            {
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_PAGINAGRUPO_ELIMINAR);
                oDatabase.AddInParameter(oDbCommand, "@IdPagina", DbType.Int32, pagina.IdPagina);

                oDatabase.ExecuteNonQuery(oDbCommand);
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public ListaGrupo GruposPorPagina(Pagina pagina)
        {
            ListaGrupo oListaGrupo = new ListaGrupo();
            Grupo Grupo;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_GRUPOSPORPAGINA);

            oDatabase.AddInParameter(oDbCommand, "@IdPagina", DbType.Int32, pagina.IdPagina);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdGrupo = oIDataReader.GetOrdinal("IdGrupo");
                int indCodigo = oIDataReader.GetOrdinal("Codigo");
                int indNombreGrupo = oIDataReader.GetOrdinal("NombreGrupo");
                int indAbreviatura = oIDataReader.GetOrdinal("Abreviatura");
                int indCheck = oIDataReader.GetOrdinal("Check");
                
                while (oIDataReader.Read())
                {
                    Grupo = new Grupo();
                    Grupo.IdGrupo = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdGrupo]);
                    Grupo.Codigo = DataUtil.DbValueToDefault<String>(oIDataReader[indCodigo]);
                    Grupo.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreGrupo]);
                    Grupo.Abreviatura = DataUtil.DbValueToDefault<String>(oIDataReader[indAbreviatura]);
                    Grupo.Check = DataUtil.DbValueToDefault<Boolean>(oIDataReader[indCheck]);
                    oListaGrupo.Add(Grupo);
                }
            }
            return oListaGrupo;
        }

        #endregion

        #region OEFA
        public ListaUnidadOrganica ListarUnidadOrganica()
        {
            ListaUnidadOrganica oListaUnidadOrganica = new ListaUnidadOrganica();
            UnidadOrganica oUnidadOrganica;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_UNIDADORGANICA_LISTAR);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indice_1 = oIDataReader.GetOrdinal("ID_UNIDAD_ORGANICA");
                int indice_2 = oIDataReader.GetOrdinal("DESCRIPCION");

                while (oIDataReader.Read())
                {
                    oUnidadOrganica = new UnidadOrganica();
                    oUnidadOrganica.IdUnidadOrganica = DataUtil.DbValueToDefault<Int32>(oIDataReader[indice_1]);
                    oUnidadOrganica.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indice_2]);
                    oListaUnidadOrganica.Add(oUnidadOrganica);
                }
            }
            return oListaUnidadOrganica;
        }

        public ListaArea ListarArea(int idunidadorganica)
        {
            ListaArea oListaArea = new ListaArea();
            Area oArea;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_AREA_LISTAR);
            //oDatabase.AddInParameter(oDbCommand, "@IdUnidadOrganica", DbType.Int32, idunidadorganica);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indice_1 = oIDataReader.GetOrdinal("IdArea");
                int indice_2 = oIDataReader.GetOrdinal("Descripcion");

                while (oIDataReader.Read())
                {
                    oArea = new Area();
                    oArea.IdArea = DataUtil.DbValueToDefault<Int32>(oIDataReader[indice_1]);
                    oArea.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indice_2]);
                    oListaArea.Add(oArea);
                }
            }
            return oListaArea;
        }

        #endregion
    }
}
