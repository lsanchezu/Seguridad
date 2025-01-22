using Komatsu.Core.Seguridad.DataAccess.Helper;
using Komatsu.Core.Seguridad.DataContracts;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Komatsu.Core.Seguridad.DataAccess
{
    public class AuditoriaDataAccess
    {
        private readonly Database oDatabase = EnterpriseLibraryContainer.Current.GetInstance<Database>(Conexion.cnsSql);
        private readonly SqlDatabase oSqlDatabase = (SqlDatabase)DatabaseFactory.CreateDatabase(Conexion.cnsSql);

        public int InsertarAuditoria(Auditoria oAuditoria)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.Usp_Ins_Auditoria);

            oDatabase.AddInParameter(oDbCommand, "@IdAccion", DbType.Int32, oAuditoria.IdAccion);
            oDatabase.AddInParameter(oDbCommand, "@IdUsuario", DbType.Int32, oAuditoria.IdUsuario);
            oDatabase.AddInParameter(oDbCommand, "@Descripcion", DbType.String, oAuditoria.Observaciones);
            oDatabase.AddInParameter(oDbCommand, "@IdTablaReferencia", DbType.Int32, oAuditoria.IdTablaReferencia);
            oDatabase.AddInParameter(oDbCommand, "@IdTipoAccion", DbType.String, oAuditoria.IdTipoAccion);
            oDatabase.AddInParameter(oDbCommand, "@IdReferencia", DbType.Int32, oAuditoria.IdReferencia);
            oDatabase.AddInParameter(oDbCommand, "@UserName", DbType.String, oAuditoria.UserName);
            oDatabase.AddInParameter(oDbCommand, "@IP", DbType.String, oAuditoria.IP);
            oDatabase.AddInParameter(oDbCommand, "@Fecha", DbType.DateTime, oAuditoria.FechaHora);

            var result = Convert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            return result;
        }

        public int InsertarAuditoriaRolUsuario(Auditoria oAuditoria, ListaUsuarioRol oListaRolUsuario)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.Usp_Ins_Auditoria_UsuarioRol);

            oSqlDatabase.AddInParameter(oDbCommand, "@IdAccion", DbType.Int32, oAuditoria.IdAccion);
            oSqlDatabase.AddInParameter(oDbCommand, "@IdUsuario", DbType.Int32, oAuditoria.IdUsuario);
            oSqlDatabase.AddInParameter(oDbCommand, "@Descripcion", DbType.String, oAuditoria.Observaciones);
            oSqlDatabase.AddInParameter(oDbCommand, "@IdTablaReferencia", DbType.Int32, oAuditoria.IdTablaReferencia);
            oSqlDatabase.AddInParameter(oDbCommand, "@IdTipoAccion", DbType.String, oAuditoria.IdTipoAccion);
            oSqlDatabase.AddInParameter(oDbCommand, "@IdReferencia", DbType.Int32, oAuditoria.IdReferencia);
            oSqlDatabase.AddInParameter(oDbCommand, "@UserName", DbType.String, oAuditoria.UserName);
            oSqlDatabase.AddInParameter(oDbCommand, "@IP", DbType.String, oAuditoria.IP);
            oSqlDatabase.AddInParameter(oDbCommand, "@Fecha", DbType.DateTime, oAuditoria.FechaHora);
            oSqlDatabase.AddInParameter(oDbCommand, "@TypeRolUsuario", SqlDbType.Structured, TipoTablaUtil.ObtenerListaUsuarioRol(oListaRolUsuario));

            var result = Convert.ToInt32(oSqlDatabase.ExecuteScalar(oDbCommand));
            return result;
        }



    }
}
