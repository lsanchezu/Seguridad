using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Komatsu.Core.Seguridad.DataAccess.Helper;
using Komatsu.Core.Seguridad.DataContracts;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.SqlServer.Server;

namespace Komatsu.Core.Seguridad.DataAccess
{
    public class PerfilDataAccess
    {
        private readonly Database oDatabase = EnterpriseLibraryContainer.Current.GetInstance<Database>(Conexion.cnsSql);
        private readonly SqlDatabase oSqlDatabase = (SqlDatabase)DatabaseFactory.CreateDatabase(Conexion.cnsSql);
        
        public int CrearPerfil(PerfilTransaccion oPerfilTransaccion)
        {
            DbCommand cmd = oSqlDatabase.GetStoredProcCommand(Procedimientos.USP_USUARIOROL_CREARPERFIL);
            oSqlDatabase.AddInParameter(cmd, "@Data", SqlDbType.Structured, TipoTablaUtil.ObtenerListaUsuarioRol(oPerfilTransaccion.ListaUsuarioRol));
            oSqlDatabase.AddInParameter(cmd, "@IdRol", DbType.Int32, oPerfilTransaccion.IdRol);
            oSqlDatabase.AddInParameter(cmd, "@IdAccion", DbType.Int32, oPerfilTransaccion.Auditoria.IdAccion);
            oSqlDatabase.AddInParameter(cmd, "@IdUsuario", DbType.Int32, oPerfilTransaccion.Auditoria.IdUsuario);
            oSqlDatabase.AddInParameter(cmd, "@IdTablaReferencia", DbType.Int32, oPerfilTransaccion.Auditoria.IdTablaReferencia);
            oSqlDatabase.AddInParameter(cmd, "@IdTipoAccion", DbType.String, oPerfilTransaccion.Auditoria.IdTipoAccion);
            oSqlDatabase.AddInParameter(cmd, "@UserName", DbType.String, oPerfilTransaccion.Auditoria.UserName);
            oSqlDatabase.AddInParameter(cmd, "@IP", DbType.String, oPerfilTransaccion.Auditoria.IP);

            int resultado = (Int32)oSqlDatabase.ExecuteScalar(cmd);
            return resultado;
        }

        public ListaTreeviewHierarchyObject ListarTreeview(int IdRol, int IdEmpresa)
        {
            ListaTreeviewHierarchyObject oListaTreeviewHierarchyObject = new ListaTreeviewHierarchyObject();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_TREEVIEW_LISTAR);
            oDatabase.AddInParameter(oDbCommand, "@IdRol", DbType.String, IdRol);
            oDatabase.AddInParameter(oDbCommand, "@IdEmpresa", DbType.String, IdEmpresa);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indiceId = oIDataReader.GetOrdinal("Id");
                int indiceDescripcion = oIDataReader.GetOrdinal("Nombre");
                int indiceIdPadre = oIDataReader.GetOrdinal("IdPadre");
                int indiceNivel = oIDataReader.GetOrdinal("Nivel");
                int indiceNivelPagina = oIDataReader.GetOrdinal("NivelPagina");
                int indiceIdPaginaPadre = oIDataReader.GetOrdinal("IdPaginaPadre");
                int indiceCheck = oIDataReader.GetOrdinal("Check");

                while (oIDataReader.Read())
                {
                    TreeviewHierarchyObject oTreeviewHierarchyObject = new TreeviewHierarchyObject();
                    oTreeviewHierarchyObject.Id = DataUtil.DbValueToDefault<int>(oIDataReader[indiceId]);
                    oTreeviewHierarchyObject.Nombre = DataUtil.DbValueToDefault<string>(oIDataReader[indiceDescripcion]);
                    oTreeviewHierarchyObject.IdPadre = DataUtil.DbValueToDefault<int>(oIDataReader[indiceIdPadre]);
                    oTreeviewHierarchyObject.Nivel = DataUtil.DbValueToDefault<int>(oIDataReader[indiceNivel]);
                    oTreeviewHierarchyObject.NivelPagina = DataUtil.DbValueToDefault<int>(oIDataReader[indiceNivelPagina]);
                    oTreeviewHierarchyObject.IdPaginaPadre = DataUtil.DbValueToDefault<int>(oIDataReader[indiceIdPaginaPadre]);
                    oTreeviewHierarchyObject.Check = DataUtil.DbValueToDefault<Boolean>(oIDataReader[indiceCheck]);

                    oListaTreeviewHierarchyObject.Add(oTreeviewHierarchyObject);
                }
            }
            return oListaTreeviewHierarchyObject;
        }

        public int AsignarPerfil(ListaPermisoPerfil oListaPermisoPerfil)
        {
            try
            {
                DbCommand cmd = oSqlDatabase.GetStoredProcCommand(Procedimientos.USP_PERMISOPERFIL_INSERTAR);
                oSqlDatabase.AddInParameter(cmd, "@TypeRolPerfil", SqlDbType.Structured, PerfilRolDetalle(oListaPermisoPerfil));
                return (Int32)oSqlDatabase.ExecuteScalar(cmd);
            }
            catch (Exception)
            {
                return -1;
            }
            
        }

        public IEnumerable<SqlDataRecord> PerfilRolDetalle(ListaPermisoPerfil oListaPermisoPerfil)
        {

            List<SqlDataRecord> records = new List<SqlDataRecord>();
            foreach (var item in oListaPermisoPerfil)
            {

                SqlDataRecord record = new SqlDataRecord(
                    new SqlMetaData[]{  new SqlMetaData("IdEmpresa", SqlDbType.Int),
                                        new SqlMetaData("IdAplicacion", SqlDbType.Int),
                                        new SqlMetaData("IdModulo", SqlDbType.Int),
                                        new SqlMetaData("IdPagina", SqlDbType.Int),
                                        new SqlMetaData("IdAccion", SqlDbType.Int),
                                        new SqlMetaData("IdRol", SqlDbType.Int)
                                    });
                record.SetInt32(0, item.IdEmpresa);
                record.SetInt32(1, item.IdAplicacion);
                record.SetInt32(2, item.IdModulo);
                record.SetInt32(3, item.IdPagina);
                record.SetInt32(4, item.IdAccion);
                record.SetInt32(5, item.IdRol);
                records.Add(record);
            }

            return records;
        }

        public ListaPagina ObtenerSitemapPorUsuario(string nombreUsuario, int IdAplicacion) 
        {

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_OBTENERSITEMAP);
            ListaPagina oListaPagina = new ListaPagina();
            oDatabase.AddInParameter(oDbCommand, "@nombreUsuario", DbType.String, nombreUsuario);
            oDatabase.AddInParameter(oDbCommand, "@IdAplicacion", DbType.Int32, IdAplicacion);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indiceIdPagina = oIDataReader.GetOrdinal("IdPagina");
                int indiceiceDescripcion = oIDataReader.GetOrdinal("Descripcion");
                int indiceUrl = oIDataReader.GetOrdinal("Url");
                int indiceIdPaginaPadre = oIDataReader.GetOrdinal("IdPaginaPadre");
                int indiceVisible = oIDataReader.GetOrdinal("Visible");
                int indiceNivel = oIDataReader.GetOrdinal("Nivel");
                int indiceIcono = oIDataReader.GetOrdinal("Icono");
                int iAgrupacion = oIDataReader.GetOrdinal("Agrupacion");
                int iTamanoMenu = oIDataReader.GetOrdinal("TamanoMenu");

                while (oIDataReader.Read())
                {
                    Pagina oPagina = new Pagina();
                    oPagina.IdPagina = DataUtil.DbValueToDefault<Int32>(oIDataReader[indiceIdPagina]);
                    oPagina.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indiceiceDescripcion]);
                    oPagina.Url = DataUtil.DbValueToDefault<String>(oIDataReader[indiceUrl]);
                    oPagina.IdPaginaPadre = DataUtil.DbValueToDefault<Int32>(oIDataReader[indiceIdPaginaPadre]);
                    oPagina.Visible = DataUtil.DbValueToDefault<Boolean>(oIDataReader[indiceVisible]);
                    oPagina.Nivel = DataUtil.DbValueToDefault<Int32>(oIDataReader[indiceNivel]);
                    oPagina.Icono = DataUtil.DbValueToDefault<String>(oIDataReader[indiceIcono]);
                    oPagina.DescripcionAgrupacion = DataUtil.DbValueToDefault<String>(oIDataReader[iAgrupacion]);
                    oPagina.IdTamanoMenu = DataUtil.DbValueToDefault<Int32>(oIDataReader[iTamanoMenu]);
                    oListaPagina.Add(oPagina);
                }
            }

            return oListaPagina;
        }

        public ListaPagina ObtenerSitemapPorUsuarioFarmacia(string nombreUsuario, int IdAplicacion, string IP)
        {

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_OBTENERSITEMAPFARMACIA);
            ListaPagina oListaPagina = new ListaPagina();
            oDatabase.AddInParameter(oDbCommand, "@nombreUsuario", DbType.String, nombreUsuario);
            oDatabase.AddInParameter(oDbCommand, "@IdAplicacion", DbType.Int32, IdAplicacion);
            oDatabase.AddInParameter(oDbCommand, "@Ip", DbType.String, IP);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indiceIdPagina = oIDataReader.GetOrdinal("IdPagina");
                int indiceiceDescripcion = oIDataReader.GetOrdinal("Descripcion");
                int indiceUrl = oIDataReader.GetOrdinal("Url");
                int indiceIdPaginaPadre = oIDataReader.GetOrdinal("IdPaginaPadre");
                int indiceVisible = oIDataReader.GetOrdinal("Visible");
                int indiceNivel = oIDataReader.GetOrdinal("Nivel");
                int indiceIcono = oIDataReader.GetOrdinal("Icono");
                int iAgrupacion = oIDataReader.GetOrdinal("Agrupacion");
                int iTamanoMenu = oIDataReader.GetOrdinal("TamanoMenu");

                while (oIDataReader.Read())
                {
                    Pagina oPagina = new Pagina();
                    oPagina.IdPagina = DataUtil.DbValueToDefault<Int32>(oIDataReader[indiceIdPagina]);
                    oPagina.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indiceiceDescripcion]);
                    oPagina.Url = DataUtil.DbValueToDefault<String>(oIDataReader[indiceUrl]);
                    oPagina.IdPaginaPadre = DataUtil.DbValueToDefault<Int32>(oIDataReader[indiceIdPaginaPadre]);
                    oPagina.Visible = DataUtil.DbValueToDefault<Boolean>(oIDataReader[indiceVisible]);
                    oPagina.Nivel = DataUtil.DbValueToDefault<Int32>(oIDataReader[indiceNivel]);
                    oPagina.Icono = DataUtil.DbValueToDefault<String>(oIDataReader[indiceIcono]);
                    oPagina.DescripcionAgrupacion = DataUtil.DbValueToDefault<String>(oIDataReader[iAgrupacion]);
                    oPagina.IdTamanoMenu = DataUtil.DbValueToDefault<Int32>(oIDataReader[iTamanoMenu]);
                    oListaPagina.Add(oPagina);
                }
            }

            return oListaPagina;
        }

        public ListaPagina ObtenerSitemap(string nombreUsuario, int IdAplicacion, int idRol)
        {

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_OBTENERSITEMAP_POR_USUARIO);
            ListaPagina oListaPagina = new ListaPagina();
            oDatabase.AddInParameter(oDbCommand, "@nombreUsuario", DbType.String, nombreUsuario);
            oDatabase.AddInParameter(oDbCommand, "@IdAplicacion", DbType.Int32, IdAplicacion);
            oDatabase.AddInParameter(oDbCommand, "@IdRol", DbType.Int32, idRol);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indiceIdPagina = oIDataReader.GetOrdinal("IdPagina");
                int indiceiceDescripcion = oIDataReader.GetOrdinal("Descripcion");
                int indiceUrl = oIDataReader.GetOrdinal("Url");
                int indiceIdPaginaPadre = oIDataReader.GetOrdinal("IdPaginaPadre");
                int indiceVisible = oIDataReader.GetOrdinal("Visible");
                int indiceNivel = oIDataReader.GetOrdinal("Nivel");
                int indiceIcono = oIDataReader.GetOrdinal("Icono");
                int iAgrupacion = oIDataReader.GetOrdinal("Agrupacion");
                int iTamanoMenu = oIDataReader.GetOrdinal("TamanoMenu");

                while (oIDataReader.Read())
                {
                    Pagina oPagina = new Pagina();
                    oPagina.IdPagina = DataUtil.DbValueToDefault<Int32>(oIDataReader[indiceIdPagina]);
                    oPagina.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indiceiceDescripcion]);
                    oPagina.Url = DataUtil.DbValueToDefault<String>(oIDataReader[indiceUrl]);
                    oPagina.IdPaginaPadre = DataUtil.DbValueToDefault<Int32>(oIDataReader[indiceIdPaginaPadre]);
                    oPagina.Visible = DataUtil.DbValueToDefault<Boolean>(oIDataReader[indiceVisible]);
                    oPagina.Nivel = DataUtil.DbValueToDefault<Int32>(oIDataReader[indiceNivel]);
                    oPagina.Icono = DataUtil.DbValueToDefault<String>(oIDataReader[indiceIcono]);
                    oPagina.DescripcionAgrupacion = DataUtil.DbValueToDefault<String>(oIDataReader[iAgrupacion]);
                    oPagina.IdTamanoMenu = DataUtil.DbValueToDefault<Int32>(oIDataReader[iTamanoMenu]);
                    oListaPagina.Add(oPagina);
                }
            }

            return oListaPagina;
        }

        public ListaPaginaAccion ObtenerPermisoPerfilPorGrupo(Int32 IdUsuario, String IdsGrupo)
        {

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_PERMISOPERFILPORGRUPOS);
            ListaPaginaAccion oListaPaginaAccion = new ListaPaginaAccion();
            oDatabase.AddInParameter(oDbCommand, "@IdUsuario", DbType.String, IdUsuario);
            oDatabase.AddInParameter(oDbCommand, "@IdsGrupo", DbType.String, IdsGrupo);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdPaginaAccion = oIDataReader.GetOrdinal("IdPaginaAccion");
                int indIdPagina = oIDataReader.GetOrdinal("IdPagina");
                int indNombrePagina = oIDataReader.GetOrdinal("NombrePagina");
                int indUrl = oIDataReader.GetOrdinal("Url");
                int indIcono = oIDataReader.GetOrdinal("Icono");
                int indIdAccion = oIDataReader.GetOrdinal("IdAccion");
                int indCodigoHTML = oIDataReader.GetOrdinal("CodigoHTML");
                int indNombreAccion = oIDataReader.GetOrdinal("NombreAccion");
                int indEtiqueta = oIDataReader.GetOrdinal("Etiqueta");
                int indMensajeToolTip = oIDataReader.GetOrdinal("MensajeToolTip");
                int indEvento = oIDataReader.GetOrdinal("Evento");
                int indIdGrupo = oIDataReader.GetOrdinal("IdGrupo");
                int indCodigo = oIDataReader.GetOrdinal("Codigo");
                int indNombreGrupo = oIDataReader.GetOrdinal("NombreGrupo");
                int indAbreviatura = oIDataReader.GetOrdinal("Abreviatura");
                
                while (oIDataReader.Read())
                {
                    PaginaAccion oPaginaAccion = new PaginaAccion();
                    oPaginaAccion.IdPaginaAccion = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdPaginaAccion]);
                    oPaginaAccion.Pagina = new Pagina();
                    oPaginaAccion.Pagina.IdPagina = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdPagina]);
                    oPaginaAccion.Pagina.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombrePagina]);
                    oPaginaAccion.Pagina.Url = DataUtil.DbValueToDefault<String>(oIDataReader[indUrl]);
                    oPaginaAccion.Pagina.Icono = DataUtil.DbValueToDefault<String>(oIDataReader[indIcono]);
                    oPaginaAccion.Accion = new Accion();
                    oPaginaAccion.Accion.IdAccion = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdAccion]);
                    oPaginaAccion.Accion.CodigoHTML = DataUtil.DbValueToDefault<String>(oIDataReader[indCodigoHTML]);
                    oPaginaAccion.Accion.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreAccion]);
                    oPaginaAccion.Accion.Etiqueta = DataUtil.DbValueToDefault<String>(oIDataReader[indEtiqueta]);
                    oPaginaAccion.Accion.MensajeToolTip = DataUtil.DbValueToDefault<String>(oIDataReader[indMensajeToolTip]);
                    oPaginaAccion.Accion.Evento = DataUtil.DbValueToDefault<String>(oIDataReader[indEvento]);
                    oPaginaAccion.Accion.Grupo = new Grupo();
                    oPaginaAccion.Accion.Grupo.IdGrupo = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdGrupo]);
                    oPaginaAccion.Accion.Grupo.Codigo = DataUtil.DbValueToDefault<String>(oIDataReader[indCodigo]);
                    oPaginaAccion.Accion.Grupo.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreGrupo]);
                    oPaginaAccion.Accion.Grupo.Abreviatura = DataUtil.DbValueToDefault<String>(oIDataReader[indAbreviatura]);

                    oListaPaginaAccion.Add(oPaginaAccion);
                }
            }

            return oListaPaginaAccion;
        }

    }
}
