using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Komatsu.Core.Seguridad.DataContracts;
using Komatsu.Core.Seguridad.DataAccess;
using System.Transactions;
using System.DirectoryServices;
using System.Configuration;
using Komatsu.Core.Seguridad.DataContracts.GeneratedCode;
using Komatsu.Core.Seguridad.BusinessLogic.Helper;
using System.DirectoryServices.AccountManagement;

namespace Komatsu.Core.Seguridad.BusinessLogic
{
    public class UsuarioBusinessLogic
    {
        public UsuarioPaginacion ListarUsuarioPaginacion(Usuario usuario,Int32 IdEmpresa,Int32 IdAplicacion, Int32 IdRol, Paginacion paginacion)
        {
            return new UsuarioDataAccess().ListarUsuarioPaginacion(usuario,IdEmpresa,IdAplicacion,IdRol, paginacion);
        }

        public ListaUsuario ListarUsuario() 
        {
            return new UsuarioDataAccess().ListarUsuario();
        }

        public ListaAplicacion ListarAplicacionUsuario(String Usuario)
        {
            return new UsuarioDataAccess().ListarAplicacionUsuario(Usuario);
        }

        public ListaUsuario ListarUsuariosxAplicacion(Int16 IdAplicacion)
        {
            return new UsuarioDataAccess().ListarUsuariosxAplicacion(IdAplicacion);
        }

        public ListaUsuario ListaEnfermerasServicio()
        {
            return new UsuarioDataAccess().ListaEnfermerasServicio();
        }
        public ListaUsuario ListarUsuarioPorRol(int IdRol) 
        {
            return new UsuarioDataAccess().ListarUsuarioPorRol(IdRol);
        }

        public ListaUsuario ListarUsuarioAsignadosPorRol(int IdRol) 
        {
            return new UsuarioDataAccess().ListarUsuarioAsignadosPorRol(IdRol);
        }

        public ListaRolUsusario ListarUsuariosxRol(Int32 idUsuario)
        {
            return new UsuarioDataAccess().ListarUsuariosxRol(idUsuario);
        }

        public ListaUsuario ListarUsuarioNoAsignadosPorRol(int IdRol) 
        {
            return new UsuarioDataAccess().ListarUsuarioNoAsignadosPorRol(IdRol);
        }

        public ListaUsuarioTipo ListarUsuarioTipo()
        {
            return new UsuarioDataAccess().ListarUsuarioTipo();
        }

        public Usuario AutenticarUsuario(Usuario usuario) 
        {
            Usuario oUser = new Usuario();
            int Modo = ModoUsuario(usuario.UserName);
            try
            {
                if (Modo == 1 || Modo == 0)
                    oUser = ValidActiveDirectory(usuario);
                else if (Modo == 2)
                    oUser = ValidSinActiveDirectory(usuario);

                if (oUser != null)
                    return oUser;
                else
                    return new Usuario();
            }
            catch (Exception)
            {
                return new Usuario();
            }
        }


        public Usuario ValidActiveDirectory(Usuario usuario)
        {
            if (ValidarUsuarioActiveDirectory(usuario.UserName, usuario.Password))
            {
                Usuario oUser = new UsuarioDataAccess().AutenticarUsuario(usuario.UserName);
                if (oUser.IdUsuario != 0)
                {
                    oUser.ListaRol = new RolDataAccess().BuscarRolesPorUsuario(oUser);

                    if (oUser.ListaRol.Any(m => m.Aplicacion.IdAplicacion == usuario.IdAplicacion))
                        return oUser;
                    else
                        return new Usuario();
                }
            }

            return new Usuario();
        }

        private bool ValidarUsuarioActiveDirectory(string usuario, string password)
        {
            try
            {
                if (int.Parse(ConfigurationManager.AppSettings["ValidarAD"]) == 1)
                {
                    using (var context = new PrincipalContext(ContextType.Domain,
                                                        ConfigurationManager.AppSettings["DomainAD"],
                                                        ConfigurationManager.AppSettings["UserAD"],
                                                        ConfigurationManager.AppSettings["PasswordAD"]))
                    {
                        return context.ValidateCredentials(usuario, password);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public Usuario ValidSinActiveDirectory(Usuario usuario)
        {
            Usuario oUser = AuthenticateUser(usuario);

            if (oUser.ListaRol.Any(m => m.Aplicacion.IdAplicacion == usuario.IdAplicacion))
                return oUser;
            else
                return new Usuario();
        }

        public ListaAplicacion AplicacionPorEmpresa(Aplicacion aplicacion)
        {
            return new UsuarioDataAccess().AplicacionPorEmpresa(aplicacion);
        }

        public ListaRol RolesPorAplicacion(Aplicacion aplicacion)
        {

            return new UsuarioDataAccess().RolesPorAplicacion(aplicacion);
        }

        public ListaUsuario ListarUsuarioPorRolServicios(int Opc, int Rol)
        {
            return new UsuarioDataAccess().ListarUsuarioPorRolServicios(Opc, Rol);
        }

        public int Insertar_Usuario(Usuario Usuario, int Modo, String Contrasena)
        {
            return new UsuarioDataAccess().Insertar_Usuario(Usuario, Modo, Contrasena);
        }

        public int ActualizarEstado_Usuario(Usuario usuario)
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                result = new UsuarioDataAccess().ActualizarEstado_Usuario(usuario);
                if (result > 0)
                    scope.Complete();
            }
            return result;
        }

        public int Actualizar_Usuario(Usuario usuario)
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                result = new UsuarioDataAccess().Actualizar_Usuario(usuario);
                if (result > 0)
                    scope.Complete();
            }

            return result;
        }

        public Usuario Buscar_Usuario(Usuario usuario)
        {
            return new UsuarioDataAccess().Buscar_Usuario(usuario);
        }

        public int InsertarLogAcceso(string host, string Ip, int IdUsuario)
        {
            return new UsuarioDataAccess().InsertarLogAcceso(host, Ip, IdUsuario);
        }

        public int InsertarLog(int IdAccion, int IdUsuario, string Descripcion)
        {
            return new UsuarioDataAccess().InsertarLog(IdAccion, IdUsuario, Descripcion);
        }

        public int GuardarPolitica(int Long, int Vigencia, int Diferencia, int NroMaximo, int DiasBloq, int Compleji, int CantContrHis)
        {
            return new UsuarioDataAccess().GuardarPolitica(Long, Vigencia, Diferencia, NroMaximo, DiasBloq, Compleji, CantContrHis);
        }

        public ListaPolitica Listar_Politica()
        {
            return new UsuarioDataAccess().Listar_Politica();
        }

        public int ModoUsuario(string Username)
        {
            return new UsuarioDataAccess().ModoUsuario(Username);
        }

        public Usuario AutenticarUsuario_Modo(Usuario usuario, string Contrasena)
        {
            RolDataAccess rolDataAccess = new RolDataAccess();
            UsuarioDataAccess usuarioDataAccess = new UsuarioDataAccess();
            usuario = new UsuarioDataAccess().AutenticarUsuario_Modo(usuario, StringCipher.Encrypt(Contrasena));
            if (usuario.IdUsuario != 0)
                usuario.ListaRol = new RolDataAccess().BuscarRolesPorUsuario(usuario);
            return usuario;
        }
        
        public int ValidarContrasena(Int32 IdUsuario, string Contrasena)
        {
            return new UsuarioDataAccess().ValidarContrasena(IdUsuario, Contrasena);
        }

        public int UpdatContrasena(Int32 IdUsuario, string Contrasena)
        {
            return new UsuarioDataAccess().UpdatContrasena(IdUsuario, Contrasena);
        }

        public int Actualizar_Contrasenia(Int32 IdUsuario, string Contrasena)
        {
            return new UsuarioDataAccess().Actualizar_Contrasenia(IdUsuario, Contrasena);
        }

        public int BloquearUsuario(string Username)
        {
            return new UsuarioDataAccess().BloquearUsuario(Username);
        }

        public ListaRol BuscarRolesPorUsuario(Usuario usuario)
        {
            return new UsuarioDataAccess().BuscarRolesPorUsuario(usuario);
        }

        public Usuario AuthenticateUser(Usuario usuario)
        {
            RolDataAccess rolDataAccess = new RolDataAccess();
            UsuarioDataAccess usuarioDataAccess = new UsuarioDataAccess();
            usuario.Password = StringCipher.Encrypt(usuario.Password);
            usuario = new UsuarioDataAccess().AutenticarUsuario(usuario);
            if (usuario.IdUsuario != 0)
                usuario.ListaRol = new RolDataAccess().BuscarRolesPorUsuario(usuario);
            return usuario;
        }
        
        public ListaArea ListarArea()
        {
            return new UsuarioDataAccess().ListarArea();
        }

        public ListaUsuario BuscarUsuario(Usuario oUsuario)
        {
            return new UsuarioDataAccess().BuscarUsuario(oUsuario);
        }

        public bool ExistUserActiveDirectory(string username)
        {
            try
            {
                using (DirectoryEntry de = new DirectoryEntry(ConfigurationManager.AppSettings[""]))
                {
                    using (DirectorySearcher adSearch = new DirectorySearcher(de))
                    {
                        adSearch.Filter = "(sAMAccountName=" + username + ")";
                        SearchResult adSearchResult = adSearch.FindOne();

                        if (adSearchResult != null)
                            return true;
                        else
                            return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ExistCotrasenaOperacion(string contrasenaoperacion)
        {
            int result = new UsuarioDataAccess().ExistCotrasenaOperacion(contrasenaoperacion);
            if (result > 0)
                return false;
            else
                return true;
        }

        public Usuario GetUsuariobyContrasenaOperacion(string contrasenaoperacion)
        {
            try
            {
                return new UsuarioDataAccess().GetUsuariobyContrasenaOperacion(contrasenaoperacion);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ListaEmpresa ObtenerEmpresaRelacionada(string tipoEmpresaRelacionada)
        {
            return new UsuarioDataAccess().ObtenerEmpresaRelacionada(tipoEmpresaRelacionada);
        }

        public bool ValidarUsuarioPorContrasenaOperacion(int idUsuario, string contrasenaOperacion)
        {
            return new UsuarioDataAccess().ValidarUsuarioPorContrasenaOperacion(idUsuario, contrasenaOperacion);
        }
    }
}
