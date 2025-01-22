using Komatsu.Core.Seguridad.DataContracts;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Komatsu.Core.Seguridad.DataAccess.Helper
{
    public static class TipoTablaUtil
    {
        public static IEnumerable<SqlDataRecord> ObtenerListaUsuarioRol(ListaUsuarioRol oListaUsuarioRol)
        {

            List<SqlDataRecord> records = new List<SqlDataRecord>();
            foreach (var item in oListaUsuarioRol)
            {

                SqlDataRecord record = new SqlDataRecord(
                    new SqlMetaData[]{  new SqlMetaData("IdUsuario", SqlDbType.Int),
                                        new SqlMetaData("IdRol", SqlDbType.Int)
                                    });
                record.SetInt32(0, item.IdUsuario);
                record.SetInt32(1, item.IdRol);
                records.Add(record);
            }

            return records;
        }
    }
}
