using System;
using System.Data;
using System.Data.Common;
using Komatsu.Core.Seguridad.DataAccess.Helper;
using Komatsu.Core.Seguridad.DataContracts;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Komatsu.Core.Seguridad.DataContracts.GeneratedCode;

namespace Komatsu.Core.Seguridad.DataAccess
{
    public class UsuarioDataAccess
    {
        private Database oDatabase = EnterpriseLibraryContainer.Current.GetInstance<Database>(Conexion.cnsSql);

        public ListaArea ListarArea()
        {
            ListaArea oListaArea = new ListaArea();
            Area oArea;

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_AREA_LISTAR);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdArea = oIDataReader.GetOrdinal("IdArea");
                int indNombreArea = oIDataReader.GetOrdinal("NombreArea");
                int indDescripcion = oIDataReader.GetOrdinal("Descripcion");
                int indEstado = oIDataReader.GetOrdinal("Estado");


                while (oIDataReader.Read())
                {
                    oArea = new Area();
                    oArea.IdArea = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdArea]);
                    oArea.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreArea]);
                    oArea.Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[indDescripcion]);
                    oArea.Estado = DataUtil.DbValueToDefault<Int32>(oIDataReader[indEstado]);

                    oListaArea.Add(oArea);
                }
            }

            return oListaArea;
        }
        
        public UsuarioPaginacion ListarUsuarioPaginacion(Usuario usuario,Int32 IdEmpresa,Int32 IdAplicacion, Int32 IdRol, Paginacion paginacion)
        {
            UsuarioPaginacion oUsuarioPaginacion = new UsuarioPaginacion();
            oUsuarioPaginacion.ListaUsuario = new ListaUsuario();
            oUsuarioPaginacion.Paginacion = paginacion;
            Usuario obj;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_USUARIO_PAGINACION);

            oDatabase.AddInParameter(oDbCommand, "@Usuario", DbType.String, usuario.UserName);
            oDatabase.AddInParameter(oDbCommand, "@NombreApellido", DbType.String, usuario.NombreApellido);
            oDatabase.AddInParameter(oDbCommand, "@IdUsuarioTipo", DbType.Int32, usuario.UsuarioTipo.IdUsuarioTipo);
            oDatabase.AddInParameter(oDbCommand, "@IdEmpresa", DbType.Int32, IdEmpresa);
            oDatabase.AddInParameter(oDbCommand, "@IdAplicacion", DbType.Int32, IdAplicacion);
            oDatabase.AddInParameter(oDbCommand, "@IdRol", DbType.Int32, IdRol);

            oDatabase.AddInParameter(oDbCommand, "@SortType", DbType.String, paginacion.SortType);
            oDatabase.AddInParameter(oDbCommand, "@SortDir", DbType.String, paginacion.SortDir);
            oDatabase.AddInParameter(oDbCommand, "@Page", DbType.Int32, paginacion.Page);
            oDatabase.AddInParameter(oDbCommand, "@RowsPerPage", DbType.Int32, paginacion.RowsPerPage);
            oDatabase.AddOutParameter(oDbCommand, "@RowCount", DbType.Int32, 0);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdUsuario = oIDataReader.GetOrdinal("IdUsuario");
                int indUsuario = oIDataReader.GetOrdinal("Usuario");
                int indCodigoEmp = oIDataReader.GetOrdinal("CodigoEmp");
                int indEmailCoorporativo = oIDataReader.GetOrdinal("EmailCoorporativo");
                int indNombreApellido = oIDataReader.GetOrdinal("NombreApellido");
                int indIdEstado = oIDataReader.GetOrdinal("IdEstado");
                int indNombreEstado = oIDataReader.GetOrdinal("NombreEstado");
                int indIdUsuarioTipo = oIDataReader.GetOrdinal("IdUsuarioTipo");
                int indNombreUsuarioTipo = oIDataReader.GetOrdinal("NombreUsuarioTipo");

                while (oIDataReader.Read())
                {
                    obj = new Usuario();
                    obj.IdUsuario = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdUsuario]);
                    obj.UserName = DataUtil.DbValueToDefault<String>(oIDataReader[indUsuario]);
                    obj.CodigoEmp = DataUtil.DbValueToDefault<String>(oIDataReader[indCodigoEmp]);
                    obj.EmailCoorporativo = DataUtil.DbValueToDefault<String>(oIDataReader[indEmailCoorporativo]);
                    obj.NombreApellido = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreApellido]);
                    obj.Estado = new Estado();
                    obj.Estado.IdEstado = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEstado]);
                    obj.Estado.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreEstado]);
                    obj.UsuarioTipo = new UsuarioTipo();
                    obj.UsuarioTipo.IdUsuarioTipo = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdUsuarioTipo]);
                    obj.UsuarioTipo.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreUsuarioTipo]);
                    oUsuarioPaginacion.ListaUsuario.Add(obj);
                }
            }

            oUsuarioPaginacion.Paginacion.RowCount = Convert.ToInt32(oDatabase.GetParameterValue(oDbCommand, "@RowCount"));
            return oUsuarioPaginacion;
        }

        public ListaUsuarioTipo ListarUsuarioTipo()
        {
            ListaUsuarioTipo oListaUsuarioTipo = new ListaUsuarioTipo();
            UsuarioTipo oUsuarioTipo;

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_USUARIOTIPO_LISTAR);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdUsuarioTipo = oIDataReader.GetOrdinal("IdUsuarioTipo");
                int indNombreUsuarioTipo = oIDataReader.GetOrdinal("NombreUsuarioTipo");

                while (oIDataReader.Read())
                {
                    oUsuarioTipo = new UsuarioTipo();
                    oUsuarioTipo.IdUsuarioTipo = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdUsuarioTipo]);
                    oUsuarioTipo.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreUsuarioTipo]);
                    oListaUsuarioTipo.Add(oUsuarioTipo);
                }
            }

            return oListaUsuarioTipo;
        }

        public ListaAplicacion ListarAplicacionUsuario(String Usuario)
        {
            ListaAplicacion oListaAplicacion = new ListaAplicacion();
            Aplicacion oAplicacion;

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_LISTAR_APLICACIONUSUARIO);
            oDatabase.AddInParameter(oDbCommand, "@Usuario", DbType.String, Usuario);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int vURL = oIDataReader.GetOrdinal("URL");
                int vAPLICACION = oIDataReader.GetOrdinal("APLICACION");

                while (oIDataReader.Read())
                {
                    oAplicacion = new Aplicacion()
                    {
                        Url = DataUtil.DbValueToDefault<String>(oIDataReader[vURL]),
                        Descripcion = DataUtil.DbValueToDefault<String>(oIDataReader[vAPLICACION])
                    };
                    oListaAplicacion.Add(oAplicacion);
                }
            }

            return oListaAplicacion;
        }

        public ListaUsuario ListarUsuariosxAplicacion(Int16 IdAplicacion)
        {
            ListaUsuario oListaUsuario = new ListaUsuario();
            Usuario oUsuario;

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_USUARIOSXAPLICACION);
            oDatabase.AddInParameter(oDbCommand, "@IdAplicacion", DbType.String, IdAplicacion);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int iIDUSUARIO = oIDataReader.GetOrdinal("IDUSUARIO");
                int vUSUARIO = oIDataReader.GetOrdinal("USUARIO");
                int vNOMBRE_USUARIO = oIDataReader.GetOrdinal("NOMBRE_USUARIO");
                int vEMAIL = oIDataReader.GetOrdinal("EMAIL");

                while (oIDataReader.Read())
                {
                    oUsuario = new Usuario()
                    {
                        IdUsuario = DataUtil.DbValueToDefault<Int32>(oIDataReader[iIDUSUARIO]),
                        UserName = DataUtil.DbValueToDefault<String>(oIDataReader[vUSUARIO]),
                        NombreApellido = DataUtil.DbValueToDefault<String>(oIDataReader[vNOMBRE_USUARIO]),
                        EmailCoorporativo = DataUtil.DbValueToDefault<String>(oIDataReader[vEMAIL])
                    };
                    oListaUsuario.Add(oUsuario);
                }
            }

            return oListaUsuario;
        }

        public ListaRolUsusario ListarUsuariosxRol(Int32 idUsuario)
        {
            ListaRolUsusario oListaRolUsusario  = new ListaRolUsusario();
            RolUsusario oRolUsusario = new RolUsusario();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.Usuario.USP_OBTENER_LISTA_USUARIOXROL);
            oDatabase.AddInParameter(oDbCommand, "@IdUsuario", DbType.Int32, idUsuario);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int iEmpresa = oIDataReader.GetOrdinal("Empresa");
                int iAplicacion = oIDataReader.GetOrdinal("Aplicacion");
                int iRol = oIDataReader.GetOrdinal("Rol");
                int iFecha = oIDataReader.GetOrdinal("FechaInicio");
                int iEstado = oIDataReader.GetOrdinal("IdEstado");

                while (oIDataReader.Read())
                {
                    oRolUsusario = new RolUsusario();
                    oRolUsusario.Empresa = DataUtil.DbValueToDefault<String>(oIDataReader[iEmpresa]);
                    oRolUsusario.Aplicacion = DataUtil.DbValueToDefault<String>(oIDataReader[iAplicacion]);
                    oRolUsusario.Rol = DataUtil.DbValueToDefault<String>(oIDataReader[iRol]);
                    oRolUsusario.FechaAsignacion = DataUtil.DbValueToDefault<DateTime>(oIDataReader[iFecha]);
                    oRolUsusario.Estado = DataUtil.DbValueToDefault<Int32>(oIDataReader[iEstado]);
                    oListaRolUsusario.Add(oRolUsusario);
                }
            }

            return oListaRolUsusario;
        }
        public ListaUsuario ListaEnfermerasServicio()
        {
            ListaUsuario oListaUsuario = new ListaUsuario();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.Usp_Sel_ListaEnfermeraServicio);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdUsuario = oIDataReader.GetOrdinal("IdUsuario");
                int indUsuario = oIDataReader.GetOrdinal("Usuario");
                //int indEmailCoorporativo = oIDataReader.GetOrdinal("EmailCoorporativo");
                int indNombreApellido = oIDataReader.GetOrdinal("NombreApellido");

                while (oIDataReader.Read())
                {
                    Usuario oUsuario = new Usuario();
                    oUsuario.IdUsuario = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdUsuario]);
                    oUsuario.UserName = DataUtil.DbValueToDefault<String>(oIDataReader[indUsuario]);
                    //oUsuario.EmailCoorporativo = DataUtil.DbValueToDefault<String>(oIDataReader[indEmailCoorporativo]);
                    oUsuario.NombreApellido = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreApellido]);

                    oListaUsuario.Add(oUsuario);
                }
            }

            return oListaUsuario;
        }


        public ListaUsuario ListarUsuario()
        {
            ListaUsuario oListaUsuario = new ListaUsuario();
            
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_USUARIO_LISTAR);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdUsuario = oIDataReader.GetOrdinal("IdUsuario");
                int indUsuario = oIDataReader.GetOrdinal("Usuario");
                int indEmailCoorporativo = oIDataReader.GetOrdinal("EmailCoorporativo");
                int indNombreApellido = oIDataReader.GetOrdinal("NombreApellido");

                while (oIDataReader.Read())
                {
                    Usuario oUsuario = new Usuario();
                    oUsuario.IdUsuario = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdUsuario]);
                    oUsuario.UserName = DataUtil.DbValueToDefault<String>(oIDataReader[indUsuario]);
                    oUsuario.EmailCoorporativo = DataUtil.DbValueToDefault<String>(oIDataReader[indEmailCoorporativo]);
                    oUsuario.NombreApellido = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreApellido]);

                    oListaUsuario.Add(oUsuario);
                }
            }

            return oListaUsuario;
        }

        public ListaUsuario ListarUsuarioPorRol(int IdRol)
        {
            ListaUsuario oListaUsuario = new ListaUsuario();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_USUARIO_LISTARPORROL);
            oDatabase.AddInParameter(oDbCommand, "@IdRol", DbType.Int32, IdRol);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdUsuario = oIDataReader.GetOrdinal("IdUsuario");
                int indUsuario = oIDataReader.GetOrdinal("Usuario");
                int indEmailCoorporativo = oIDataReader.GetOrdinal("EmailCoorporativo");
                int indNombreApellido = oIDataReader.GetOrdinal("NombreApellido");

                while (oIDataReader.Read())
                {
                    Usuario oUsuario = new Usuario();
                    oUsuario.IdUsuario = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdUsuario]);
                    oUsuario.UserName = DataUtil.DbValueToDefault<String>(oIDataReader[indUsuario]);
                    oUsuario.EmailCoorporativo = DataUtil.DbValueToDefault<String>(oIDataReader[indEmailCoorporativo]);
                    oUsuario.NombreApellido = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreApellido]);

                    oListaUsuario.Add(oUsuario);
                }
            }

            return oListaUsuario;
        }

        public ListaUsuario ListarUsuarioAsignadosPorRol(int IdRol)
        {
            ListaUsuario oListaUsuario = new ListaUsuario();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_USUARIO_LISTARASIGNADOSPORROL);
            oDatabase.AddInParameter(oDbCommand, "@IdRol", DbType.Int32, IdRol);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdUsuario = oIDataReader.GetOrdinal("IdUsuario");
                int indUsuario = oIDataReader.GetOrdinal("Usuario");
                int indEmailCoorporativo = oIDataReader.GetOrdinal("EmailCoorporativo");
                int indNombreApellido = oIDataReader.GetOrdinal("NombreApellido");

                while (oIDataReader.Read())
                {
                    Usuario oUsuario = new Usuario();
                    oUsuario.IdUsuario = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdUsuario]);
                    oUsuario.UserName = DataUtil.DbValueToDefault<String>(oIDataReader[indUsuario]);
                    oUsuario.EmailCoorporativo = DataUtil.DbValueToDefault<String>(oIDataReader[indEmailCoorporativo]);
                    oUsuario.NombreApellido = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreApellido]);

                    oListaUsuario.Add(oUsuario);
                }
            }

            return oListaUsuario;
        }
        
        public ListaUsuario ListarUsuarioNoAsignadosPorRol(int IdRol)
        {
            ListaUsuario oListaUsuario = new ListaUsuario();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_USUARIO_LISTARNOASIGNADOSPORROL);
            oDatabase.AddInParameter(oDbCommand, "@IdRol", DbType.Int32, IdRol);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdUsuario = oIDataReader.GetOrdinal("IdUsuario");
                int indUsuario = oIDataReader.GetOrdinal("Usuario");
                int indEmailCoorporativo = oIDataReader.GetOrdinal("EmailCoorporativo");
                int indNombreApellido = oIDataReader.GetOrdinal("NombreApellido");

                while (oIDataReader.Read())
                {
                    Usuario oUsuario = new Usuario();
                    oUsuario.IdUsuario = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdUsuario]);
                    oUsuario.UserName = DataUtil.DbValueToDefault<String>(oIDataReader[indUsuario]);
                    oUsuario.EmailCoorporativo = DataUtil.DbValueToDefault<String>(oIDataReader[indEmailCoorporativo]);
                    oUsuario.NombreApellido = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreApellido]);

                    oListaUsuario.Add(oUsuario);
                }
            }

            return oListaUsuario;
        }

        public Usuario AutenticarUsuario(string username)
        {
            Usuario oUsuario = new Usuario();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_AUTENTICARUSUARIO);
            oDatabase.AddInParameter(oDbCommand, "@Usuario", DbType.String, username);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdUsuario = oIDataReader.GetOrdinal("IdUsuario");
                int indUsuario = oIDataReader.GetOrdinal("Usuario");
                int indCodigoEmp = oIDataReader.GetOrdinal("CodigoEmp");
                int indNombreApellido = oIDataReader.GetOrdinal("NombreApellido");
                int indEmailCoorporativo = oIDataReader.GetOrdinal("EmailCoorporativo");
                int indSexo = oIDataReader.GetOrdinal("Sexo");
                int indCodigoEmpresa = oIDataReader.GetOrdinal("CodigoEmpresa");
                int indCorreo = oIDataReader.GetOrdinal("Correo");
                int indDireccion = oIDataReader.GetOrdinal("Direccion");
                int indDNI = oIDataReader.GetOrdinal("DNI");
                int indContentStyle = oIDataReader.GetOrdinal("ContentStyle");

                int indIdSociedad = oIDataReader.GetOrdinal("IdSociedad");
                int indSociedadDescripcionCorta = oIDataReader.GetOrdinal("SociedadDescripcionCorta");
                int indIdAreaBU = oIDataReader.GetOrdinal("IdAreaBU");
                int indAreaBUDescripcionCorta = oIDataReader.GetOrdinal("AreaBUDescripcionCorta");
                int indIdCargo = oIDataReader.GetOrdinal("IdCargo");
                int indCargoDescripcionCorta = oIDataReader.GetOrdinal("CargoDescripcionCorta");
                int indIdComite = oIDataReader.GetOrdinal("IdComite");

                if (oIDataReader.Read())
                {
                    oUsuario = new Usuario
                    {
                        IdUsuario = DataUtil.DbValueToDefault<int>(oIDataReader[indIdUsuario]),
                        UserName = DataUtil.DbValueToDefault<string>(oIDataReader[indUsuario]),
                        CodigoEmp = DataUtil.DbValueToDefault<string>(oIDataReader[indCodigoEmp]),
                        NombreApellido = DataUtil.DbValueToDefault<string>(oIDataReader[indNombreApellido]),
                        EmailCoorporativo = DataUtil.DbValueToDefault<string>(oIDataReader[indEmailCoorporativo]),
                        Sexo = DataUtil.DbValueToDefault<string>(oIDataReader[indSexo]),
                        Correo = DataUtil.DbValueToDefault<string>(oIDataReader[indCorreo]),
                        DNI = DataUtil.DbValueToDefault<string>(oIDataReader[indDNI]),
                        Empresa = new Empresa() 
                        {
                            CodigoEmpresa = DataUtil.DbValueToDefault<string>(oIDataReader[indCodigoEmpresa]),
                            ContentStyle = DataUtil.DbValueToDefault<string>(oIDataReader[indContentStyle]),
                        },
                        IdSociedad = DataUtil.DbValueToDefault<int>(oIDataReader[indIdSociedad]),
                        SociedadDescripcionCorta = DataUtil.DbValueToDefault<string>(oIDataReader[indSociedadDescripcionCorta]),
                        IdAreaBU = DataUtil.DbValueToDefault<int>(oIDataReader[indIdAreaBU]),
                        AreaBUDescripcionCorta = DataUtil.DbValueToDefault<string>(oIDataReader[indAreaBUDescripcionCorta]),
                        IdCargo = DataUtil.DbValueToDefault<int>(oIDataReader[indIdCargo]),
                        CargoDescripcionCorta = DataUtil.DbValueToDefault<string>(oIDataReader[indCargoDescripcionCorta]),
                        IdComite = DataUtil.DbValueToDefault<int>(oIDataReader[indIdComite])
                    };
                }
            }
            return oUsuario;
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

        public ListaUsuario ListarUsuarioPorRolServicios(int Opc,int Rol)
        {
            ListaUsuario oListaUsuario = new ListaUsuario();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.Usp_Empleados_Rol_Servicios);
            oDatabase.AddInParameter(oDbCommand, "@Opc", DbType.Int32, Opc);
            oDatabase.AddInParameter(oDbCommand, "@Rol", DbType.Int32, Rol);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indCodigoEmp = oIDataReader.GetOrdinal("CodigoEmp");

                while (oIDataReader.Read())
                {
                    Usuario oUsuario = new Usuario();
                    oUsuario.CodigoEmp = DataUtil.DbValueToDefault<String>(oIDataReader[indCodigoEmp]);  
                    oListaUsuario.Add(oUsuario);
                }
            }

            return oListaUsuario;
        }

        public int Insertar_Usuario(Usuario Usuario, int Modo, String Contrasena)
        {
            int Resultado = 0;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_USUARIO_INSERTAR2);
            oDatabase.AddInParameter(oDbCommand, "@IdUsuario", DbType.String, Usuario.UserName);
            oDatabase.AddInParameter(oDbCommand, "@IdCodEmp", DbType.String, Usuario.CodigoEmp);
            oDatabase.AddInParameter(oDbCommand, "@Nombres", DbType.String, Usuario.NombreApellido);
            oDatabase.AddInParameter(oDbCommand, "@IdTipoUsu", DbType.Int32, Usuario.UsuarioTipo.IdUsuarioTipo);
            oDatabase.AddInParameter(oDbCommand, "@CodEmpresa", DbType.String, Usuario.Empresa.CodigoEmpresa);
            oDatabase.AddInParameter(oDbCommand, "@Sexo", DbType.String, Usuario.Sexo);
            oDatabase.AddInParameter(oDbCommand, "@Email", DbType.String, Usuario.EmailCoorporativo);
            oDatabase.AddInParameter(oDbCommand, "@Direccion", DbType.String, Usuario.Direccion);
            oDatabase.AddInParameter(oDbCommand, "@DNI", DbType.String, Usuario.DNI);
            oDatabase.AddInParameter(oDbCommand, "@Modo", DbType.Int32, Modo);
            oDatabase.AddInParameter(oDbCommand, "@Contrasena", DbType.String, Contrasena);
            oDatabase.AddInParameter(oDbCommand, "@IdEstado", DbType.Int32, Usuario.Estado.IdEstado);
            oDatabase.AddInParameter(oDbCommand, "@IdEmpresaRelacionada", DbType.Int32, Usuario.EmpresaRelacionada.IdEmpresa);
            Resultado = Convert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            return Resultado;
        }

        public int ActualizarEstado_Usuario(Usuario usuario)
        {
            try
            {
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_USUARIO_ACTUALIZARESTADO2);

                oDatabase.AddInParameter(oDbCommand, "@IdUsuario", DbType.Int32, usuario.IdUsuario);
                oDatabase.AddInParameter(oDbCommand, "@IdEstado", DbType.String, usuario.Estado.IdEstado);

                return oDatabase.ExecuteNonQuery(oDbCommand);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public ListaUsuario BuscarUsuario(Usuario oUsuario)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_USUARIO_POR_USUARIO);
            oDatabase.AddInParameter(oDbCommand, "@IdUsuario", DbType.Int32, oUsuario.IdUsuario);
            oDatabase.AddInParameter(oDbCommand, "@Usuario", DbType.String, oUsuario.UserName);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdUsuario = oIDataReader.GetOrdinal("IdUsuario");
                int indDNI = oIDataReader.GetOrdinal("DNI");
                int indUsuario = oIDataReader.GetOrdinal("Usuario");
                int indClave = oIDataReader.GetOrdinal("Clave");
                int indNombreApellido = oIDataReader.GetOrdinal("NombreApellido");
                int indCodigoEmpresa = oIDataReader.GetOrdinal("CodigoEmpresa");
                int intEmailCoorporativo = oIDataReader.GetOrdinal("EmailCoorporativo");
                int indIdCargo = oIDataReader.GetOrdinal("IdCargo");
                int indNombreCargo = oIDataReader.GetOrdinal("NombreCargo");
                int indIdUsuarioTipo = oIDataReader.GetOrdinal("IdUsuarioTipo");
                int indIdEstado = oIDataReader.GetOrdinal("IdEstado");
                int indIdArea = oIDataReader.GetOrdinal("IdArea");
                int indNombreArea = oIDataReader.GetOrdinal("NombreArea");
                int indIdAreas = oIDataReader.GetOrdinal("IdAreas");

                ListaUsuario oListaUsuario = new ListaUsuario();

                while (oIDataReader.Read())
                {
                    oUsuario = new Usuario();
                    oUsuario.IdUsuario = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdUsuario]);
                    oUsuario.DNI = DataUtil.DbValueToDefault<String>(oIDataReader[indDNI]);
                    oUsuario.UserName = DataUtil.DbValueToDefault<String>(oIDataReader[indUsuario]);
                    oUsuario.Password = DataUtil.DbValueToDefault<String>(oIDataReader[indClave]);
                    oUsuario.NombreApellido = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreApellido]);
                    oUsuario.EmailCoorporativo = DataUtil.DbValueToDefault<String>(oIDataReader[intEmailCoorporativo]);
                    oUsuario.Empresa = new Empresa { CodigoEmpresa = DataUtil.DbValueToDefault<String>(oIDataReader[indCodigoEmpresa]) };
                    oUsuario.Cargo = new Cargo
                    {
                        IdCargo = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdCargo]),
                        NombreCargo = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreCargo])
                    };
                    oUsuario.UsuarioTipo = new UsuarioTipo { IdUsuarioTipo = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdUsuarioTipo]) };
                    oUsuario.Estado = new Estado { IdEstado = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdEstado]) };
                    oUsuario.Area = new Area
                    {
                        IdArea = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdArea]),
                        Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreArea])
                    };
                    oUsuario.IdAreas = DataUtil.DbValueToDefault<String>(oIDataReader[indIdAreas]);
                    oListaUsuario.Add(oUsuario);
                }

                return oListaUsuario;
            }
        }


        public int Actualizar_Usuario(Usuario usuario)
        {
            try
            {
                DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_USUARIO_ACTUALIZAR2);

                oDatabase.AddInParameter(oDbCommand, "@IdUsuario", DbType.Int32, usuario.IdUsuario);
                oDatabase.AddInParameter(oDbCommand, "@UsuarioName", DbType.String, usuario.UserName);
                oDatabase.AddInParameter(oDbCommand, "@NOMBRES", DbType.String, usuario.NombreApellido);
                oDatabase.AddInParameter(oDbCommand, "@IdTipoUsu", DbType.Int32, usuario.UsuarioTipo.IdUsuarioTipo);
                oDatabase.AddInParameter(oDbCommand, "@CodEmpresa", DbType.String, usuario.Empresa.CodigoEmpresa);
                oDatabase.AddInParameter(oDbCommand, "@Email", DbType.String, usuario.EmailCoorporativo);
                oDatabase.AddInParameter(oDbCommand, "@Direccion", DbType.String, usuario.Direccion);
                oDatabase.AddInParameter(oDbCommand, "@DNI", DbType.String, usuario.DNI);
                oDatabase.AddInParameter(oDbCommand, "@IdCodEmp", DbType.String, usuario.CodigoEmp);
                oDatabase.AddInParameter(oDbCommand, "@FlagResetearContrasena", DbType.Boolean, usuario.CambiarPassword);
                oDatabase.AddInParameter(oDbCommand, "@Contrasena", DbType.String, usuario.Password);
                /*oDatabase.AddInParameter(oDbCommand, "@IdUnidadOrganica", DbType.Int32, usuario.IdSociedad);
                oDatabase.AddInParameter(oDbCommand, "@UnidadOrganicaDescripcion", DbType.String, usuario.SociedadDescripcionCorta);
                oDatabase.AddInParameter(oDbCommand, "@IdArea", DbType.Int32, usuario.IdAreaBU);
                oDatabase.AddInParameter(oDbCommand, "@AreaDescripcion", DbType.String, usuario.AreaBUDescripcionCorta);*/

                return (int)oDatabase.ExecuteScalar(oDbCommand);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public Usuario Buscar_Usuario(Usuario usuario)
        {
            Usuario oUsuario = new Usuario();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_USUARIO_BUSCAR2);

            oDatabase.AddInParameter(oDbCommand, "@IdUsuario", DbType.Int32, usuario.IdUsuario);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdUsuario = oIDataReader.GetOrdinal("IdUsuario");
                int indUsuario = oIDataReader.GetOrdinal("Usuario");
                int indNombreApellidol = oIDataReader.GetOrdinal("NombreApellido");
                int indTipo = oIDataReader.GetOrdinal("Tipo");
                int indEmpresa = oIDataReader.GetOrdinal("Empresa");
                int indEmailCoorporativo = oIDataReader.GetOrdinal("EmailCoorporativo");
                int indDireccion = oIDataReader.GetOrdinal("Direccion");
                int indDNI = oIDataReader.GetOrdinal("DNI");
                int indIdUnidadOrganica = oIDataReader.GetOrdinal("IdUnidadOrganica");
                int indUnidadOrganicaDescripcion = oIDataReader.GetOrdinal("UnidadOrganicaDescripcion");
                int indIdArea = oIDataReader.GetOrdinal("IdArea");
                int indAreaDescripcion = oIDataReader.GetOrdinal("AreaDescripcion");
                int indCodigoEmp = oIDataReader.GetOrdinal("CodigoEmp");

                while (oIDataReader.Read())
                {
                    oUsuario.IdUsuario = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdUsuario]);
                    oUsuario.UserName = DataUtil.DbValueToDefault<String>(oIDataReader[indUsuario]);
                    oUsuario.NombreApellido = DataUtil.DbValueToDefault<String>(oIDataReader[indNombreApellidol]);
                    oUsuario.UsuarioTipo = new UsuarioTipo();
                    oUsuario.UsuarioTipo.IdUsuarioTipo = DataUtil.DbValueToDefault<Int32>(oIDataReader[indTipo]);
                    oUsuario.Empresa = new Empresa();
                    oUsuario.Empresa.IdEmpresa = DataUtil.DbValueToDefault<Int32>(oIDataReader[indEmpresa]);
                    oUsuario.EmailCoorporativo = DataUtil.DbValueToDefault<String>(oIDataReader[indEmailCoorporativo]);
                    oUsuario.Direccion = DataUtil.DbValueToDefault<String>(oIDataReader[indDireccion]);
                    oUsuario.DNI = DataUtil.DbValueToDefault<String>(oIDataReader[indDNI]);

                    oUsuario.IdSociedad = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdUnidadOrganica]);
                    oUsuario.SociedadDescripcionCorta = DataUtil.DbValueToDefault<String>(oIDataReader[indUnidadOrganicaDescripcion]);
                    oUsuario.IdAreaBU = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdArea]);
                    oUsuario.AreaBUDescripcionCorta = DataUtil.DbValueToDefault<String>(oIDataReader[indAreaDescripcion]);
                    oUsuario.CodigoEmp = DataUtil.DbValueToDefault<string>(oIDataReader[indCodigoEmp]);
                }
            }
            return oUsuario;
        }

        public int InsertarLogAcceso(string host, string Ip, int IdUsuario)
        {
            int Resultado = 0;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.Usp_AuditoriaAcceso);
            oDatabase.AddInParameter(oDbCommand, "@Host", DbType.String, host);
            oDatabase.AddInParameter(oDbCommand, "@Ip", DbType.String, Ip);
            oDatabase.AddInParameter(oDbCommand, "@IdUsuario", DbType.Int32, IdUsuario);
            Resultado = Convert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            return Resultado;
        }

        public int InsertarLog(int IdAccion, int IdUsuario, string Descripcion)
        {
            int Resultado = 0;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.Usp_InsAuditoriaTransa);
            oDatabase.AddInParameter(oDbCommand, "@IdAccion", DbType.Int32, IdAccion);
            oDatabase.AddInParameter(oDbCommand, "@IdUsuario", DbType.Int32, IdUsuario);
            oDatabase.AddInParameter(oDbCommand, "@Descripcion", DbType.String, Descripcion);
            Resultado = Convert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            return Resultado;
        }

        public int GuardarPolitica(int Long, int Vigencia, int Diferencia, int NroMaximo, int DiasBloq, int Compleji, int CantContrHis)
        {
            int Resultado = 0;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.Usp_Gest_Politica);
            oDatabase.AddInParameter(oDbCommand, "@LongMinima_Contrasena", DbType.Int32, Long);
            oDatabase.AddInParameter(oDbCommand, "@Vigencia_Contrasena", DbType.Int32, Vigencia);
            oDatabase.AddInParameter(oDbCommand, "@Diferencia_ContrasenaUsuario", DbType.Int32, Diferencia);
            oDatabase.AddInParameter(oDbCommand, "@NroMaximoIntentos", DbType.Int32, NroMaximo);
            oDatabase.AddInParameter(oDbCommand, "@DiasBloqueoUsuario", DbType.Int32, DiasBloq);
            oDatabase.AddInParameter(oDbCommand, "@ComplejidadContraseña", DbType.String, Compleji);
            oDatabase.AddInParameter(oDbCommand, "@CantidadContrasenaHist", DbType.String, CantContrHis);
            Resultado = Convert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            return Resultado;
        }
         
        public ListaPolitica Listar_Politica()
        {
            ListaPolitica oListaPolitica = new ListaPolitica();
            Politica oPolitica = new Politica();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.Usp_List_Politica);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indLongMinima_Contrasena = oIDataReader.GetOrdinal("LongMinima_Contrasena");
                int indVigencia_Contrasena = oIDataReader.GetOrdinal("Vigencia_Contrasena");
                int indDiferencia_ContrasenaUsuario = oIDataReader.GetOrdinal("Diferencia_ContrasenaUsuario");
                int indNroMaximoIntentos = oIDataReader.GetOrdinal("NroMaximoIntentos");
                int indDiasBloqueoUsuario = oIDataReader.GetOrdinal("DiasBloqueoUsuario");
                int indComplejidadContraseña = oIDataReader.GetOrdinal("ComplejidadContraseña");
                int indCantidadContrasenaHist = oIDataReader.GetOrdinal("CantidadContrasenaHist");

                while (oIDataReader.Read())
                {
                    oPolitica.LongMinima_Contrasena = DataUtil.DbValueToDefault<Int32>(oIDataReader[indLongMinima_Contrasena]);
                    oPolitica.Vigencia_Contrasena = DataUtil.DbValueToDefault<Int32>(oIDataReader[indVigencia_Contrasena]);
                    oPolitica.Diferencia_ContrasenaUsuario = DataUtil.DbValueToDefault<Int32>(oIDataReader[indDiferencia_ContrasenaUsuario]);
                    oPolitica.NroMaximoIntentos = DataUtil.DbValueToDefault<Int32>(oIDataReader[indNroMaximoIntentos]);
                    oPolitica.DiasBloqueoUsuario = DataUtil.DbValueToDefault<Int32>(oIDataReader[indDiasBloqueoUsuario]);
                    oPolitica.ComplejidadContraseña = DataUtil.DbValueToDefault<Int32>(oIDataReader[indComplejidadContraseña]);
                    oPolitica.CantidadContrasenaHist = DataUtil.DbValueToDefault<Int32>(oIDataReader[indCantidadContrasenaHist]);
                    oListaPolitica.Add(oPolitica);
                }
            }
            return oListaPolitica;
        }

        public int ModoUsuario(string Username)
        {
            int Resultado = 0;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.Usp_ValModo_Usuario);
            oDatabase.AddInParameter(oDbCommand, "@Username", DbType.String, Username);
            Resultado = Convert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            return Resultado;
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
                    oListaRol.Add(oRol);
                }
            }
            return oListaRol;
        }


        public Usuario AutenticarUsuario(Usuario usuario)
        {
            Usuario oUsuario = new Usuario();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_AUTENTICARUSUARIO);
            oDatabase.AddInParameter(oDbCommand, "@Usuario", DbType.String, usuario.UserName);
            oDatabase.AddInParameter(oDbCommand, "@Clave", DbType.String, usuario.Password);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdUsuario = oIDataReader.GetOrdinal("IdUsuario");
                int indUsuario = oIDataReader.GetOrdinal("Usuario");
                int indCodigoEmp = oIDataReader.GetOrdinal("CodigoEmp");
                int indNombreApellido = oIDataReader.GetOrdinal("NombreApellido");
                int indEmailCoorporativo = oIDataReader.GetOrdinal("EmailCoorporativo");
                int indSexo = oIDataReader.GetOrdinal("Sexo");
                int indCodigoEmpresa = oIDataReader.GetOrdinal("CodigoEmpresa");
                int indCorreo = oIDataReader.GetOrdinal("Correo");
                int indDireccion = oIDataReader.GetOrdinal("Direccion");
                int indDNI = oIDataReader.GetOrdinal("DNI");
                int indContentStyle = oIDataReader.GetOrdinal("ContentStyle");
                int indIdSociedad = oIDataReader.GetOrdinal("IdSociedad");
                int indSociedadDescripcionCorta = oIDataReader.GetOrdinal("SociedadDescripcionCorta");
                int indIdAreaBU = oIDataReader.GetOrdinal("IdAreaBU");
                int indAreaBUDescripcionCorta = oIDataReader.GetOrdinal("AreaBUDescripcionCorta");
                int indIdCargo = oIDataReader.GetOrdinal("IdCargo");
                int indCargoDescripcionCorta = oIDataReader.GetOrdinal("CargoDescripcionCorta");
                int indIdComite = oIDataReader.GetOrdinal("IdComite");
                int intCambiarPassword = oIDataReader.GetOrdinal("CambiarPassword");

                if (oIDataReader.Read())
                {
                    oUsuario = new Usuario
                    {
                        IdUsuario = DataUtil.DbValueToDefault<int>(oIDataReader[indIdUsuario]),
                        UserName = DataUtil.DbValueToDefault<string>(oIDataReader[indUsuario]),
                        CodigoEmp = DataUtil.DbValueToDefault<string>(oIDataReader[indCodigoEmp]),
                        NombreApellido = DataUtil.DbValueToDefault<string>(oIDataReader[indNombreApellido]),
                        EmailCoorporativo = DataUtil.DbValueToDefault<string>(oIDataReader[indEmailCoorporativo]),
                        Sexo = DataUtil.DbValueToDefault<string>(oIDataReader[indSexo]),
                        Correo = DataUtil.DbValueToDefault<string>(oIDataReader[indCorreo]),
                        DNI = DataUtil.DbValueToDefault<string>(oIDataReader[indDNI]),
                        Empresa = new Empresa()
                        {
                            CodigoEmpresa = DataUtil.DbValueToDefault<string>(oIDataReader[indCodigoEmpresa]),
                            ContentStyle = DataUtil.DbValueToDefault<string>(oIDataReader[indContentStyle]),
                        },
                        IdSociedad = DataUtil.DbValueToDefault<int>(oIDataReader[indIdSociedad]),
                        SociedadDescripcionCorta = DataUtil.DbValueToDefault<string>(oIDataReader[indSociedadDescripcionCorta]),
                        IdAreaBU = DataUtil.DbValueToDefault<int>(oIDataReader[indIdAreaBU]),
                        AreaBUDescripcionCorta = DataUtil.DbValueToDefault<string>(oIDataReader[indAreaBUDescripcionCorta]),
                        IdCargo = DataUtil.DbValueToDefault<int>(oIDataReader[indIdCargo]),
                        CargoDescripcionCorta = DataUtil.DbValueToDefault<string>(oIDataReader[indCargoDescripcionCorta]),
                        IdComite = DataUtil.DbValueToDefault<int>(oIDataReader[indIdComite]),
                        CambiarPassword = DataUtil.DbValueToDefault<bool>(oIDataReader[intCambiarPassword]) 
                    };
                }
            }
            return oUsuario;
        }
        
        public Usuario AutenticarUsuario_Modo(Usuario usuario, string Contrasena)
        {
            Usuario oUsuario = new Usuario();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.Usp_AutenticarUsuario_Modo);
            oDatabase.AddInParameter(oDbCommand, "@Usuario", DbType.String, usuario.UserName);
            oDatabase.AddInParameter(oDbCommand, "@Contraseña", DbType.String, Contrasena);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdUsuario = oIDataReader.GetOrdinal("IdUsuario");
                int indUsuario = oIDataReader.GetOrdinal("Usuario");
                int indCodigoEmp = oIDataReader.GetOrdinal("CodigoEmp");
                int indNombreApellido = oIDataReader.GetOrdinal("NombreApellido");
                int indEmailCoorporativo = oIDataReader.GetOrdinal("EmailCoorporativo");
                int indSexo = oIDataReader.GetOrdinal("Sexo");
                int indCodigoEmpresa = oIDataReader.GetOrdinal("CodigoEmpresa");
                int indCorreo = oIDataReader.GetOrdinal("Correo");
                int indDireccion = oIDataReader.GetOrdinal("Direccion");
                int indDNI = oIDataReader.GetOrdinal("DNI");
                int indContentStyle = oIDataReader.GetOrdinal("ContentStyle");

                int indIdSociedad = oIDataReader.GetOrdinal("IdSociedad");
                int indSociedadDescripcionCorta = oIDataReader.GetOrdinal("SociedadDescripcionCorta");
                int indIdAreaBU = oIDataReader.GetOrdinal("IdAreaBU");
                int indAreaBUDescripcionCorta = oIDataReader.GetOrdinal("AreaBUDescripcionCorta");
                int indIdCargo = oIDataReader.GetOrdinal("IdCargo");
                int indCargoDescripcionCorta = oIDataReader.GetOrdinal("CargoDescripcionCorta");
                int indIdComite = oIDataReader.GetOrdinal("Caduco");

                if (oIDataReader.Read())
                {
                    oUsuario = new Usuario
                    {
                        IdUsuario = DataUtil.DbValueToDefault<int>(oIDataReader[indIdUsuario]),
                        UserName = DataUtil.DbValueToDefault<string>(oIDataReader[indUsuario]),
                        CodigoEmp = DataUtil.DbValueToDefault<string>(oIDataReader[indCodigoEmp]),
                        NombreApellido = DataUtil.DbValueToDefault<string>(oIDataReader[indNombreApellido]),
                        EmailCoorporativo = DataUtil.DbValueToDefault<string>(oIDataReader[indEmailCoorporativo]),
                        Sexo = DataUtil.DbValueToDefault<string>(oIDataReader[indSexo]),
                        Correo = DataUtil.DbValueToDefault<string>(oIDataReader[indCorreo]),
                        DNI = DataUtil.DbValueToDefault<string>(oIDataReader[indDNI]),
                        Empresa = new Empresa()
                        {
                            CodigoEmpresa = DataUtil.DbValueToDefault<string>(oIDataReader[indCodigoEmpresa]),
                            ContentStyle = DataUtil.DbValueToDefault<string>(oIDataReader[indContentStyle]),
                        },
                        IdSociedad = DataUtil.DbValueToDefault<int>(oIDataReader[indIdSociedad]),
                        SociedadDescripcionCorta = DataUtil.DbValueToDefault<string>(oIDataReader[indSociedadDescripcionCorta]),
                        IdAreaBU = DataUtil.DbValueToDefault<int>(oIDataReader[indIdAreaBU]),
                        AreaBUDescripcionCorta = DataUtil.DbValueToDefault<string>(oIDataReader[indAreaBUDescripcionCorta]),
                        IdCargo = DataUtil.DbValueToDefault<int>(oIDataReader[indIdCargo]),
                        CargoDescripcionCorta = DataUtil.DbValueToDefault<string>(oIDataReader[indCargoDescripcionCorta]),
                        IdComite = DataUtil.DbValueToDefault<int>(oIDataReader[indIdComite])
                        
                    };
                }
            }
            return oUsuario;
        }
        
        public int ValidarContrasena(Int32 IdUsuario, string Contrasena)
        {
            int Resultado = 0;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.Usp_ValContrasena_Usuario);
            oDatabase.AddInParameter(oDbCommand, "@Username", DbType.Int32, IdUsuario);
            oDatabase.AddInParameter(oDbCommand, "@Contrasena", DbType.String, Contrasena);
            Resultado = Convert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            return Resultado;
        }

        public int UpdatContrasena(Int32 IdUsuario, string Contrasena)
        {
            int Resultado = 0;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.Usp_UpdContrasena_Usuario);
            oDatabase.AddInParameter(oDbCommand, "@Username", DbType.Int32, IdUsuario);
            oDatabase.AddInParameter(oDbCommand, "@Contrasena", DbType.String, Contrasena);
            Resultado = Convert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            return Resultado;
        }

        public int Actualizar_Contrasenia(Int32 IdUsuario, string Contrasena)
        {
            int Resultado = 0;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.Usp_Upd_Usuario_Contrasena);
            oDatabase.AddInParameter(oDbCommand, "@IdUsuario", DbType.Int32, IdUsuario);
            oDatabase.AddInParameter(oDbCommand, "@Contrasena", DbType.String, Contrasena);
            Resultado = Convert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            return Resultado;
        }

        public int BloquearUsuario(string Username)
        {
            int Resultado = 0;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.Usp_Bloquear_Usuario);
            oDatabase.AddInParameter(oDbCommand, "@Username", DbType.String, Username);
            Resultado = Convert.ToInt32(oDatabase.ExecuteScalar(oDbCommand));
            return Resultado;
        }

        public User AutenticarUsuario_OEFA(string username)
        {
            User oUsuario = new User();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.USP_AUTENTICARUSUARIO);
            oDatabase.AddInParameter(oDbCommand, "@Usuario", DbType.String, username);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdUsuario = oIDataReader.GetOrdinal("IdUsuario");
                int indUsuario = oIDataReader.GetOrdinal("Usuario");
                int indCodigoEmp = oIDataReader.GetOrdinal("CodigoEmp");
                int indNombreApellido = oIDataReader.GetOrdinal("NombreApellido");
                int indEmailCoorporativo = oIDataReader.GetOrdinal("EmailCoorporativo");
                int indSexo = oIDataReader.GetOrdinal("Sexo");
                int indCodigoEmpresa = oIDataReader.GetOrdinal("CodigoEmpresa");
                int indCorreo = oIDataReader.GetOrdinal("Correo");
                int indDireccion = oIDataReader.GetOrdinal("Direccion");
                int indDNI = oIDataReader.GetOrdinal("DNI");
                int indContentStyle = oIDataReader.GetOrdinal("ContentStyle");
                int indIdSociedad = oIDataReader.GetOrdinal("IdSociedad");
                int indSociedadDescripcionCorta = oIDataReader.GetOrdinal("SociedadDescripcionCorta");
                int indIdAreaBU = oIDataReader.GetOrdinal("IdAreaBU");
                int indAreaBUDescripcionCorta = oIDataReader.GetOrdinal("AreaBUDescripcionCorta");
                int indIdCargo = oIDataReader.GetOrdinal("IdCargo");
                int indCargoDescripcionCorta = oIDataReader.GetOrdinal("CargoDescripcionCorta");
                int indIdComite = oIDataReader.GetOrdinal("IdComite");

                if (oIDataReader.Read())
                {
                    oUsuario = new User
                    {
                        IdUsuario = DataUtil.DbValueToDefault<int>(oIDataReader[indIdUsuario]),
                        UserName = DataUtil.DbValueToDefault<string>(oIDataReader[indUsuario]),
                        CodigoEmp = DataUtil.DbValueToDefault<string>(oIDataReader[indCodigoEmp]),
                        NombreApellido = DataUtil.DbValueToDefault<string>(oIDataReader[indNombreApellido]),
                        EmailCoorporativo = DataUtil.DbValueToDefault<string>(oIDataReader[indEmailCoorporativo]),
                        DNI = DataUtil.DbValueToDefault<string>(oIDataReader[indDNI]),
                        IdUnidadOrganica = DataUtil.DbValueToDefault<int>(oIDataReader[indIdSociedad]),
                        UnidadOrganicaDescripcionCorta = DataUtil.DbValueToDefault<string>(oIDataReader[indSociedadDescripcionCorta]),
                        IdAreaBU = DataUtil.DbValueToDefault<int>(oIDataReader[indIdAreaBU]),
                        AreaBUDescripcionCorta = DataUtil.DbValueToDefault<string>(oIDataReader[indAreaBUDescripcionCorta]),
                        IdCargo = DataUtil.DbValueToDefault<int>(oIDataReader[indIdCargo]),
                        CargoDescripcionCorta = DataUtil.DbValueToDefault<string>(oIDataReader[indCargoDescripcionCorta]),
                        IdComite = DataUtil.DbValueToDefault<int>(oIDataReader[indIdComite])
                    };
                }
            }
            return oUsuario;
        }

        public User AutenticarUsuario_Modo_OEFA(User usuario, string Contrasena)
        {
            User oUsuario = new User();

            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.Usp_AutenticarUsuario_Modo);
            oDatabase.AddInParameter(oDbCommand, "@Usuario", DbType.String, usuario.UserName);
            oDatabase.AddInParameter(oDbCommand, "@Contraseña", DbType.String, Contrasena);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdUsuario = oIDataReader.GetOrdinal("IdUsuario");
                int indUsuario = oIDataReader.GetOrdinal("Usuario");
                int indCodigoEmp = oIDataReader.GetOrdinal("CodigoEmp");
                int indNombreApellido = oIDataReader.GetOrdinal("NombreApellido");
                int indEmailCoorporativo = oIDataReader.GetOrdinal("EmailCoorporativo");
                int indSexo = oIDataReader.GetOrdinal("Sexo");
                int indCodigoEmpresa = oIDataReader.GetOrdinal("CodigoEmpresa");
                int indCorreo = oIDataReader.GetOrdinal("Correo");
                int indDireccion = oIDataReader.GetOrdinal("Direccion");
                int indDNI = oIDataReader.GetOrdinal("DNI");
                int indContentStyle = oIDataReader.GetOrdinal("ContentStyle");

                int indIdSociedad = oIDataReader.GetOrdinal("IdSociedad");
                int indSociedadDescripcionCorta = oIDataReader.GetOrdinal("SociedadDescripcionCorta");
                int indIdAreaBU = oIDataReader.GetOrdinal("IdAreaBU");
                int indAreaBUDescripcionCorta = oIDataReader.GetOrdinal("AreaBUDescripcionCorta");
                int indIdCargo = oIDataReader.GetOrdinal("IdCargo");
                int indCargoDescripcionCorta = oIDataReader.GetOrdinal("CargoDescripcionCorta");
                int indIdComite = oIDataReader.GetOrdinal("Caduco");

                if (oIDataReader.Read())
                {
                    oUsuario = new User
                    {
                        IdUsuario = DataUtil.DbValueToDefault<int>(oIDataReader[indIdUsuario]),
                        UserName = DataUtil.DbValueToDefault<string>(oIDataReader[indUsuario]),
                        CodigoEmp = DataUtil.DbValueToDefault<string>(oIDataReader[indCodigoEmp]),
                        NombreApellido = DataUtil.DbValueToDefault<string>(oIDataReader[indNombreApellido]),
                        EmailCoorporativo = DataUtil.DbValueToDefault<string>(oIDataReader[indEmailCoorporativo]),
                        DNI = DataUtil.DbValueToDefault<string>(oIDataReader[indDNI]),
                        IdUnidadOrganica = DataUtil.DbValueToDefault<int>(oIDataReader[indIdSociedad]),
                        UnidadOrganicaDescripcionCorta = DataUtil.DbValueToDefault<string>(oIDataReader[indSociedadDescripcionCorta]),
                        IdAreaBU = DataUtil.DbValueToDefault<int>(oIDataReader[indIdAreaBU]),
                        AreaBUDescripcionCorta = DataUtil.DbValueToDefault<string>(oIDataReader[indAreaBUDescripcionCorta]),
                        IdCargo = DataUtil.DbValueToDefault<int>(oIDataReader[indIdCargo]),
                        CargoDescripcionCorta = DataUtil.DbValueToDefault<string>(oIDataReader[indCargoDescripcionCorta]),
                        IdComite = DataUtil.DbValueToDefault<int>(oIDataReader[indIdComite])

                    };
                }
            }
            return oUsuario;
        }


        public int ExistCotrasenaOperacion(string contrasenaoperacion)
        {
            return (int)oDatabase.ExecuteScalar(Procedimientos.Usp_ExistContrasenaOperation, contrasenaoperacion);
        }

        public Usuario GetUsuariobyContrasenaOperacion(string contrasenaoperacion)
        {
            Usuario oUsuario = new Usuario();
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.Usp_UsuariobyContrasenaOperacion);

            oDatabase.AddInParameter(oDbCommand, "@ContrasenaOperacion", DbType.String, contrasenaoperacion);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int i1 = oIDataReader.GetOrdinal("CodigoEmp");
                int i2 = oIDataReader.GetOrdinal("Usuario");
                int i3 = oIDataReader.GetOrdinal("IdUsuario");
                int i4 = oIDataReader.GetOrdinal("NombreApellido");
                int i5 = oIDataReader.GetOrdinal("EmailCoorporativo");
                int i6 = oIDataReader.GetOrdinal("Direccion");
                int i7 = oIDataReader.GetOrdinal("DNI");

                while (oIDataReader.Read())
                {
                    oUsuario.CodigoEmp = DataUtil.DbValueToDefault<String>(oIDataReader[i1]);
                    oUsuario.UserName = DataUtil.DbValueToDefault<String>(oIDataReader[i2]);
                    oUsuario.IdUsuario = DataUtil.DbValueToDefault<Int32>(oIDataReader[i3]);
                    oUsuario.NombreApellido = DataUtil.DbValueToDefault<String>(oIDataReader[i4]);
                    oUsuario.EmailCoorporativo = DataUtil.DbValueToDefault<String>(oIDataReader[i5]);
                    oUsuario.Direccion = DataUtil.DbValueToDefault<String>(oIDataReader[i6]);
                    oUsuario.DNI = DataUtil.DbValueToDefault<String>(oIDataReader[i7]);

                }
            }
            return oUsuario;
        }

        public ListaEmpresa ObtenerEmpresaRelacionada(string tipoEmpresaRelacionada)
        {
            ListaEmpresa oListaEmpresa = new ListaEmpresa();
            Empresa oEmpresa;
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.Usp_Sel_Empresa_Relacionada);
            oDatabase.AddInParameter(oDbCommand, "@TipoEmpresa", DbType.String, tipoEmpresaRelacionada);

            using (IDataReader oIDataReader = oDatabase.ExecuteReader(oDbCommand))
            {
                int indIdCTACTE = oIDataReader.GetOrdinal("IdCTACTE");
                int indRazonSocial = oIDataReader.GetOrdinal("RazonSocial");

                while (oIDataReader.Read())
                {
                    oEmpresa = new Empresa();
                    oEmpresa.IdEmpresa = DataUtil.DbValueToDefault<Int32>(oIDataReader[indIdCTACTE]);
                    oEmpresa.Nombre = DataUtil.DbValueToDefault<String>(oIDataReader[indRazonSocial]);
                    oListaEmpresa.Add(oEmpresa);
                }
            }
            return oListaEmpresa;
        }

        public bool ValidarUsuarioPorContrasenaOperacion(int idUsuario, string contrasenaOperacion)
        {
            DbCommand oDbCommand = oDatabase.GetStoredProcCommand(Procedimientos.Usp_ValidarUsuarioPorContrasenaOperacion);
            oDatabase.AddInParameter(oDbCommand, "@IdUsuario", DbType.Int32, idUsuario);
            oDatabase.AddInParameter(oDbCommand, "@ContrasenaOperacion", DbType.String, contrasenaOperacion);

            return (bool)oDatabase.ExecuteScalar(oDbCommand);
        }
    }
}

