using System;
using System.Data;
using System.Data.Common;
using Komatsu.Core.Seguridad.DataAccess.Helper;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Komatsu.Core.Seguridad.DataContracts;

namespace Komatsu.Core.Seguridad.DataAccess
{
    public class RolDataAccess
    {
        private Database oDatabase = EnterpriseLibraryContainer.Current.GetInstance<Database>(Conexion.cnsSql);

        public RolPaginacion ListarRolPaginacion(Rol rol, Paginacion paginacion)    
        {
            RolPaginacion oRolPaginacion = new RolPaginacion();
            oRolPaginacion.ListaRol = new ListaRol();
            oRolPaginacion.Paginacion = paginacion;
            Rol oRol;

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_ROL_PAGINACION);

            oDatabase.AddInParameter(oDbCommand, "@Nombre", DbType.String, rol.Nombre);
            oDatabase.AddInParameter(oDbCommand, "@IdAplicacion", DbType.String, rol.Aplicacion.IdAplicacion);

            oDatabase.AddInParameter(oDbCommand, "@SortType", DbType.String, paginacion.SortType);
            oDatabase.AddInParameter(oDbCommand, "@SortDir", DbType.String, paginacion.SortDir);
            oDatabase.AddInParameter(oDbCommand, "@Page", DbType.Int32, paginacion.Page);
            oDatabase.AddInParameter(oDbCommand, "@RowsPerPage", DbType.Int32, paginacion.RowsPerPage);
            oDatabase.AddOutParameter(oDbCommand, "@RowCount", DbType.Int32, 0);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdRol = oIDataReader.GetOrdinal("IdRol");
                int indSiSuperAdmi = oIDataReader.GetOrdinal("SiSuperAdmi");
                int indNombreRol = oIDataReader.GetOrdinal("NombreRol");
                int indSiRango = oIDataReader.GetOrdinal("SiRango");
                int indFechaInicio = oIDataReader.GetOrdinal("FechaInicio");
                int indFechaFin = oIDataReader.GetOrdinal("FechaFin");
                int indIdEstado = oIDataReader.GetOrdinal("IdEstado");
                int indNombreEstado = oIDataReader.GetOrdinal("NombreEstado");
                int indIdAplicacion = oIDataReader.GetOrdinal("IdAplicacion");
                int indNombreAplicacion = oIDataReader.GetOrdinal("NombreAplicacion");
                int indDescripcionAplicacion = oIDataReader.GetOrdinal("DescripcionAplicacion");
                int indUrl = oIDataReader.GetOrdinal("Url");

                while (oIDataReader.Read())
                {
                    oRol = new Rol();
                    oRol.IdRol = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdRol]);
                    oRol.SiSuperAdmi = DataUtil.DbValueToDefault<Boolean>(oIDataReader[indSiSuperAdmi]);
                    oRol.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreRol]);
                    oRol.SiRango = DataUtil.DbValueToDefault<Boolean>(oIDataReader[indSiRango]);
                    oRol.FechaInicio = DataUtil.DbValueToDefault<DateTime>(oIDataReader[indFechaInicio]);
                    oRol.FechaFin = DataUtil.DbValueToDefault<DateTime>(oIDataReader[indFechaFin]);
                    oRol.Estado = new Estado();
                    oRol.Estado.IdEstado = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEstado]);
                    oRol.Estado.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreEstado]);
                    oRol.Aplicacion = new Aplicacion();
                    oRol.Aplicacion.IdAplicacion = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdAplicacion]);
                    oRol.Aplicacion.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreAplicacion]);
                    oRol.Aplicacion.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indDescripcionAplicacion]);
                    oRol.Aplicacion.Url = DataUtil.DbValueToDefault<String>(oIDataReader[indUrl]);
                    oRolPaginacion.ListaRol.Add(oRol);
                }
            }
            oRolPaginacion.Paginacion.RowCount = Convert.ToInt32(oDatabase.GetParameterValue(oDbCommand, "@RowCount"));
            return oRolPaginacion;
        }

        public Rol Buscar_Rol(Rol rol)
        {
            Rol oRol = new Rol();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_ROL_BUSCAR);

            oDatabase.AddInParameter(oDbCommand, "@IdRol", DbType.Int32, rol.IdRol);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdRol = oIDataReader.GetOrdinal("IdRol");
                int indNombreRol = oIDataReader.GetOrdinal("NombreRol");
                int indSiSuperAdmi = oIDataReader.GetOrdinal("SiSuperAdmi");
                int indSiRango = oIDataReader.GetOrdinal("SiRango");
                int indFechaInicio = oIDataReader.GetOrdinal("FechaInicio");
                int indFechaFin = oIDataReader.GetOrdinal("FechaFin");
                int indIdEstado = oIDataReader.GetOrdinal("IdEstado");
                int indNombreEstado = oIDataReader.GetOrdinal("NombreEstado");
                int indIdAplicacion = oIDataReader.GetOrdinal("IdAplicacion");
                int indNombreAplicacion = oIDataReader.GetOrdinal("NombreAplicacion");
                int indDescripcionAplicacion = oIDataReader.GetOrdinal("NombreEstado");
                int indUrl = oIDataReader.GetOrdinal("Url");
                
                while (oIDataReader.Read())
                {
                    oRol.IdRol = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdRol]);
                    oRol.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreRol]);
                    oRol.SiSuperAdmi = DataUtil.DbValueToDefault<Boolean>(oIDataReader[indSiSuperAdmi]);
                    oRol.SiRango = DataUtil.DbValueToDefault<Boolean>(oIDataReader[indSiRango]);
                    oRol.FechaInicio = DataUtil.DbValueToDefault<DateTime>(oIDataReader[indFechaInicio]);
                    oRol.FechaFin = DataUtil.DbValueToDefault<DateTime>(oIDataReader[indFechaFin]);
                    oRol.Estado = new Estado();
                    oRol.Estado.IdEstado = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEstado]);
                    oRol.Estado.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreEstado]);
                    oRol.Aplicacion = new Aplicacion();
                    oRol.Aplicacion.IdAplicacion = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdAplicacion]);
                    oRol.Aplicacion.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreAplicacion]);
                    oRol.Aplicacion.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indDescripcionAplicacion]);
                    oRol.Aplicacion.Url = DataUtil.DbValueToDefault<String>(oIDataReader[indUrl]);
                }
            }
            return oRol;
        }

        public int Insertar_Rol(Rol rol)
        {
            try
            {
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_ROL_INSERTAR);

                oDatabase.AddInParameter(oDbCommand, "@Nombre", DbType.String, rol.Nombre);
                oDatabase.AddInParameter(oDbCommand, "@SiRango", DbType.Boolean, rol.SiRango);
                oDatabase.AddInParameter(oDbCommand, "@FechaInicio", DbType.Date, rol.FechaInicio);
                oDatabase.AddInParameter(oDbCommand, "@FechaFin", DbType.Date, rol.FechaFin);
                oDatabase.AddInParameter(oDbCommand, "@IdAplicacion", DbType.Int32, rol.Aplicacion.IdAplicacion);
                oDatabase.AddInParameter(oDbCommand, "@SiSuperAdmi", DbType.Boolean, rol.SiSuperAdmi);
                
                return oDatabase.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Actualizar_Rol(Rol rol)
        {
            try
            {
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_ROL_ACTUALIZAR);

                oDatabase.AddInParameter(oDbCommand, "@IdRol", DbType.Int32, rol.IdRol);
                oDatabase.AddInParameter(oDbCommand, "@Nombre", DbType.String, rol.Nombre);
                oDatabase.AddInParameter(oDbCommand, "@SiRango", DbType.Boolean, rol.SiRango);
                oDatabase.AddInParameter(oDbCommand, "@FechaInicio", DbType.Date, rol.FechaInicio);
                oDatabase.AddInParameter(oDbCommand, "@FechaFin", DbType.Date, rol.FechaFin);
                oDatabase.AddInParameter(oDbCommand, "@IdAplicacion", DbType.Int32, rol.Aplicacion.IdAplicacion);
                oDatabase.AddInParameter(oDbCommand, "@SiSuperAdmi", DbType.Boolean, rol.SiSuperAdmi);
                
                return oDatabase.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int ActualizarEstado_Rol(Rol rol)
        {
            try
            {
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_ROL_ACTUALIZARESTADO);

                oDatabase.AddInParameter(oDbCommand, "@IdRol", DbType.Int32, rol.IdRol);
                oDatabase.AddInParameter(oDbCommand, "@IdEstado", DbType.String, rol.Estado.IdEstado);

                return oDatabase.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public ListaRol ListarRol()
        {
            ListaRol oListaRol = new ListaRol();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_ROL_LISTAR);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indNombreRol = oIDataReader.GetOrdinal("NombreRol");
                int indIdRol = oIDataReader.GetOrdinal("IdRol");
                int indSiSuperAdmi = oIDataReader.GetOrdinal("SiSuperAdmi");
                int indSiRango = oIDataReader.GetOrdinal("SiRango");
                int indFechaInicio = oIDataReader.GetOrdinal("FechaInicio");
                int indFechaFin = oIDataReader.GetOrdinal("FechaFin");
                int indIdEstado = oIDataReader.GetOrdinal("IdEstado");
                int indNombreEstado = oIDataReader.GetOrdinal("NombreEstado");
                int indIdAplicacion = oIDataReader.GetOrdinal("IdAplicacion");
                int indNombreAplicacion = oIDataReader.GetOrdinal("NombreAplicacion");
                int indDescripcionAplicacion = oIDataReader.GetOrdinal("NombreEstado");
                int indUrl = oIDataReader.GetOrdinal("Url");

                while (oIDataReader.Read())
                {
                    Rol oRol = new Rol();
                    oRol.IdRol = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdRol]);
                    oRol.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreRol]);
                    oRol.SiSuperAdmi = DataUtil.DbValueToDefault<Boolean>(oIDataReader[indSiSuperAdmi]);
                    oRol.SiRango = DataUtil.DbValueToDefault<Boolean>(oIDataReader[indSiRango]);
                    oRol.FechaInicio = DataUtil.DbValueToDefault<DateTime>(oIDataReader[indFechaInicio]);
                    oRol.FechaFin = DataUtil.DbValueToDefault<DateTime>(oIDataReader[indFechaFin]);
                    oRol.Estado = new Estado();
                    oRol.Estado.IdEstado = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEstado]);
                    oRol.Estado.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreEstado]);
                    oRol.Aplicacion = new Aplicacion();
                    oRol.Aplicacion.IdAplicacion = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdAplicacion]);
                    oRol.Aplicacion.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreAplicacion]);
                    oRol.Aplicacion.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indDescripcionAplicacion]);
                    oRol.Aplicacion.Url = DataUtil.DbValueToDefault<String>(oIDataReader[indUrl]);
                    oListaRol.Add(oRol);
                }
            }
            return oListaRol;
        }

        public ListaRol RolesPorAplicacion(Aplicacion aplicacion)
        {
            ListaRol oListaRol = new ListaRol();
            Rol oRol;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_ROLESPORAPLICACION);
            oDatabase.AddInParameter(oDbCommand, "@IdAplicacion", DbType.Int32, aplicacion.IdAplicacion);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdRol = oIDataReader.GetOrdinal("IdRol");
                int indNombreRol = oIDataReader.GetOrdinal("NombreRol");
                int indSiSuperAdmi = oIDataReader.GetOrdinal("SiSuperAdmi");
                int indSiRango = oIDataReader.GetOrdinal("SiRango");
                int indFechaInicio = oIDataReader.GetOrdinal("FechaInicio");
                int indFechaFin = oIDataReader.GetOrdinal("FechaFin");
                int indIdAplicacion = oIDataReader.GetOrdinal("IdAplicacion");
                
                while (oIDataReader.Read())
                {
                    oRol = new Rol();
                    oRol.IdRol = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdRol]);
                    oRol.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreRol]);
                    oRol.SiSuperAdmi = DataUtil.DbValueToDefault<Boolean>(oIDataReader[indSiSuperAdmi]);
                    oRol.SiRango = DataUtil.DbValueToDefault<Boolean>(oIDataReader[indSiRango]);
                    oRol.FechaInicio = DataUtil.DbValueToDefault<DateTime>(oIDataReader[indFechaInicio]);
                    oRol.FechaFin = DataUtil.DbValueToDefault<DateTime>(oIDataReader[indFechaFin]);
                    oRol.Aplicacion = new Aplicacion();
                    oRol.Aplicacion.IdAplicacion = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdAplicacion]);
                    oListaRol.Add(oRol);
                }
            }
            return oListaRol;
        }

        public ListaRol BuscarRolesPorUsuario(Usuario usuario)
        {
            ListaRol oListaRol = new ListaRol();
            Rol oRol;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_ROLESPORUSUARIO);

            oDatabase.AddInParameter(oDbCommand, "@IdUsuario", DbType.Int32, usuario.IdUsuario);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdRol = oIDataReader.GetOrdinal("IdRol");
                int indNombreRol = oIDataReader.GetOrdinal("NombreRol");
                int indSiSuperAdmi = oIDataReader.GetOrdinal("SiSuperAdmi");
                int indSiRango = oIDataReader.GetOrdinal("SiRango");
                int indFechaInicio = oIDataReader.GetOrdinal("FechaInicio");
                int indFechaFin = oIDataReader.GetOrdinal("FechaFin");
                int indIdAplicacion = oIDataReader.GetOrdinal("IdAplicacion");
                int indNombreAplicacion = oIDataReader.GetOrdinal("NombreAplicacion");
                int indDescripcionAplicacion = oIDataReader.GetOrdinal("DescripcionAplicacion");
                int indIdCentroAtencion = oIDataReader.GetOrdinal("IdCentroAtencion");

                while (oIDataReader.Read())
                {
                    oRol = new Rol();
                    oRol.IdRol = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdRol]);
                    oRol.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreRol]);
                    oRol.SiSuperAdmi = DataUtil.DbValueToDefault<Boolean>(oIDataReader[indSiSuperAdmi]);
                    oRol.SiRango = DataUtil.DbValueToDefault<Boolean>(oIDataReader[indSiRango]);
                    oRol.FechaInicio = DataUtil.DbValueToDefault<DateTime>(oIDataReader[indFechaInicio]);
                    oRol.FechaFin = DataUtil.DbValueToDefault<DateTime>(oIDataReader[indFechaFin]);
                    oRol.Aplicacion = new Aplicacion();
                    oRol.Aplicacion.IdAplicacion = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdAplicacion]);
                    oRol.Aplicacion.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreAplicacion]);
                    oRol.Aplicacion.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indDescripcionAplicacion]);
                    oRol.IdCentroAtencion = DataUtil.DbValueToDefault<int>(oIDataReader[indIdCentroAtencion]);
                    oListaRol.Add(oRol);
                }
            }
            return oListaRol;
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

        public ListaRol BuscarRolesPorUsuario_OEFA(User usuario)
        {
            ListaRol oListaRol = new ListaRol();
            Rol oRol;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_ROLESPORUSUARIO);

            oDatabase.AddInParameter(oDbCommand, "@IdUsuario", DbType.Int32, usuario.IdUsuario);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdRol = oIDataReader.GetOrdinal("IdRol");
                int indNombreRol = oIDataReader.GetOrdinal("NombreRol");
                int indSiSuperAdmi = oIDataReader.GetOrdinal("SiSuperAdmi");
                int indSiRango = oIDataReader.GetOrdinal("SiRango");
                int indFechaInicio = oIDataReader.GetOrdinal("FechaInicio");
                int indFechaFin = oIDataReader.GetOrdinal("FechaFin");
                int indIdAplicacion = oIDataReader.GetOrdinal("IdAplicacion");
                int indNombreAplicacion = oIDataReader.GetOrdinal("NombreAplicacion");
                int indDescripcionAplicacion = oIDataReader.GetOrdinal("DescripcionAplicacion");

                while (oIDataReader.Read())
                {
                    oRol = new Rol();
                    oRol.IdRol = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdRol]);
                    oRol.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreRol]);
                    oRol.SiSuperAdmi = DataUtil.DbValueToDefault<Boolean>(oIDataReader[indSiSuperAdmi]);
                    oRol.SiRango = DataUtil.DbValueToDefault<Boolean>(oIDataReader[indSiRango]);
                    oRol.FechaInicio = DataUtil.DbValueToDefault<DateTime>(oIDataReader[indFechaInicio]);
                    oRol.FechaFin = DataUtil.DbValueToDefault<DateTime>(oIDataReader[indFechaFin]);
                    oRol.Aplicacion = new Aplicacion();
                    oRol.Aplicacion.IdAplicacion = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdRol]);
                    oRol.Aplicacion.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreAplicacion]);
                    oRol.Aplicacion.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indDescripcionAplicacion]);
                    oListaRol.Add(oRol);
                }
            }
            return oListaRol;
        }

        public ListaOperacion ListarOperacion()
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_OPERACION_LISTAR);
            ListaOperacion oListaOperacion = new ListaOperacion();
            Operacion oOperacion;

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdOperacion = oIDataReader.GetOrdinal("IdOperacion");
                int indDescripcion = oIDataReader.GetOrdinal("Descripcion");

                while (oIDataReader.Read())
                {
                    oOperacion = new Operacion();

                    oOperacion.IdOperacion = DataUtil.DbValueToDefault<int>(oIDataReader[indIdOperacion]);
                    oOperacion.Descripcion = DataUtil.DbValueToDefault<string>(oIDataReader[indDescripcion]);
                    oListaOperacion.Add(oOperacion);
                }
            }

            return oListaOperacion;
        }

        public ListaOperacion ListarOperacionesPorRol(int idRol)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_SEL_OPERACION_POR_ROL);
            oDatabase.AddInParameter(oDbCommand, "@IdRol", DbType.Int32, idRol);

            ListaOperacion oListaOperacion = new ListaOperacion();
            Operacion oOperacion;

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdOperacion = oIDataReader.GetOrdinal("IdOperacion");
                int indDescripcion = oIDataReader.GetOrdinal("Descripcion");
                int indIdEstado = oIDataReader.GetOrdinal("IdEstado");

                while (oIDataReader.Read())
                {
                    oOperacion = new Operacion();
                    oOperacion.Estado = new Estado();

                    oOperacion.IdOperacion = DataUtil.DbValueToDefault<int>(oIDataReader[indIdOperacion]);
                    oOperacion.Descripcion = DataUtil.DbValueToDefault<string>(oIDataReader[indDescripcion]);
                    oOperacion.Estado.IdEstado = DataUtil.DbValueToDefault<int>(oIDataReader[indIdEstado]);
                    oListaOperacion.Add(oOperacion);
                }
            }
            return oListaOperacion;
        }
    }
}
