using System;
using Komatsu.Core.Seguridad.BusinessLogic;
using Komatsu.Core.Seguridad.DataContracts;
using Komatsu.Core.Seguridad.ServiceContracts;
using Komatsu.Core.Seguridad.DataContracts.GeneratedCode;

namespace Komatsu.Core.Seguridad.ServiceImplementation
{
    public class ServicioUsuario : IServicioUsuario
    {
        public UsuarioPaginacion ListarUsuarioPaginacion(DataContracts.Usuario usuario, Int32 IdEmpresa, Int32 IdAplicacion, Int32 IdRol, DataContracts.Paginacion Paginacion)
        {
            return new UsuarioBusinessLogic().ListarUsuarioPaginacion(usuario, IdEmpresa, IdAplicacion, IdRol, Paginacion);
        }

        public ListaUsuario ListarUsuario()
        {
            return new UsuarioBusinessLogic().ListarUsuario();
        }

        public ListaRol BuscarRolesPorUsuario(Usuario usuario)
        {
            return new UsuarioBusinessLogic().BuscarRolesPorUsuario(usuario);
        }

        public ListaAplicacion ListarAplicacionUsuario(String Usuario)
        {
            return new UsuarioBusinessLogic().ListarAplicacionUsuario(Usuario);
        }

        public ListaUsuario ListarUsuariosxAplicacion(Int16 IdAplicacion)
        {
            return new UsuarioBusinessLogic().ListarUsuariosxAplicacion(IdAplicacion);
        }
        public ListaUsuario ListaEnfermerasServicio()
        {
            return new UsuarioBusinessLogic().ListaEnfermerasServicio();
        }
        public ListaUsuario ListarUsuarioPorRol(int IdRol)
        {
            return new UsuarioBusinessLogic().ListarUsuarioPorRol(IdRol);
        }

        public ListaUsuario ListarUsuarioAsignadosPorRol(int IdRol)
        {
            return new UsuarioBusinessLogic().ListarUsuarioAsignadosPorRol(IdRol);
        }

        public ListaUsuario ListarUsuarioNoAsignadosPorRol(int IdRol)
        {
            return new UsuarioBusinessLogic().ListarUsuarioNoAsignadosPorRol(IdRol);
        }

        public ListaRolUsusario ListarUsuariosxRol(Int32 idUsuario)
        {
            return new UsuarioBusinessLogic().ListarUsuariosxRol(idUsuario);
        }

        public ListaUsuarioTipo ListarUsuarioTipo()
        {
            return new UsuarioBusinessLogic().ListarUsuarioTipo();
        }


        public ListaAplicacion AplicacionPorEmpresa(Aplicacion aplicacion)
        {
            return new UsuarioBusinessLogic().AplicacionPorEmpresa(aplicacion);
        }

        public ListaRol RolesPorAplicacion(Aplicacion aplicacion)
        {

            return new UsuarioBusinessLogic().RolesPorAplicacion(aplicacion);
        }

        public ListaUsuario ListarUsuarioPorRolServicios(int Opc, int Rol)
        {
            return new UsuarioBusinessLogic().ListarUsuarioPorRolServicios(Opc, Rol);
        }

        public int Insertar_Usuario(Usuario Usuario, int Modo, String Contrasena)
        {
            return new UsuarioBusinessLogic().Insertar_Usuario(Usuario, Modo, Contrasena);
        }

        public int ActualizarEstado_Usuario(Usuario usuario)
        {
            return new UsuarioBusinessLogic().ActualizarEstado_Usuario(usuario);
        }

        public int Actualizar_Usuario(Usuario usuario)
        {
            return new UsuarioBusinessLogic().Actualizar_Usuario(usuario);
        }

        public Usuario Buscar_Usuario(Usuario usuario)
        {
            return new UsuarioBusinessLogic().Buscar_Usuario(usuario);
        }

        public int InsertarLogAcceso(string strHostName, string IP, int IdUsu)
        {
            return new UsuarioBusinessLogic().InsertarLogAcceso(strHostName, IP, IdUsu);
        }

        public int InsertarLog(int IdAccion, int IdUsuario, string Descripcion)
        {
            return new UsuarioBusinessLogic().InsertarLog(IdAccion, IdUsuario, Descripcion);
        }

        public int GuardarPolitica(int Long, int Vigencia, int Diferencia, int NroMaximo, int DiasBloq, int Compleji, int CantContrHis)
        {
            return new UsuarioBusinessLogic().GuardarPolitica(Long, Vigencia, Diferencia, NroMaximo, DiasBloq, Compleji, CantContrHis);
        }

        public ListaPolitica Listar_Politica()
        {
            return new UsuarioBusinessLogic().Listar_Politica();
        }

        public int ModoUsuario(string Username)
        {
            return new UsuarioBusinessLogic().ModoUsuario(Username);
        }

        public ListaArea ListarArea()
        {
            return new UsuarioBusinessLogic().ListarArea();
        }

        public Usuario AutenticarUsuario_Modo(Usuario usuario, string Contrasena)
        {
            return new UsuarioBusinessLogic().AutenticarUsuario_Modo(usuario, Contrasena);
        }

        public int ValidarContrasena(Int32 IdUsuario, string Contrasena)
        {
            return new UsuarioBusinessLogic().ValidarContrasena(IdUsuario, Contrasena);
        }

        public int UpdatContrasena(Int32 IdUsuario, string Contrasena)
        {
            return new UsuarioBusinessLogic().UpdatContrasena(IdUsuario, Contrasena);
        }

        public int Actualizar_Contrasenia(Int32 IdUsuario, string Contrasena)
        {
            return new UsuarioBusinessLogic().Actualizar_Contrasenia(IdUsuario, Contrasena);
        }
        public ListaUsuario BuscarUsuario(Usuario oUsuario)
        {
            return new UsuarioBusinessLogic().BuscarUsuario(oUsuario);
        }

        public int BloquearUsuario(string Username)
        {
            return new UsuarioBusinessLogic().BloquearUsuario(Username);
        }

        public Usuario AuthenticateUser(Usuario usuario) //WEB
        {
            return new UsuarioBusinessLogic().AuthenticateUser(usuario);
        }

        public Usuario AutenticarUsuario(Usuario usuario) // Servicio Otros
        {
            return new UsuarioBusinessLogic().AutenticarUsuario(usuario);
        }
        
        public bool ExistUserActiveDirectory(string username)
        {
            return new UsuarioBusinessLogic().ExistUserActiveDirectory(username);
        }


        public bool ExistCotrasenaOperacion(string contrasenaoperacion)
        {
            return new UsuarioBusinessLogic().ExistCotrasenaOperacion(contrasenaoperacion);
        }


        public Usuario GetUsuariobyContrasenaOperacion(string contrasenaoperacion)
        {
            return new UsuarioBusinessLogic().GetUsuariobyContrasenaOperacion(contrasenaoperacion);
        }

        public ListaEmpresa ObtenerEmpresaRelacionada(string tipoEmpresaRelacionada)
        {
            return new UsuarioBusinessLogic().ObtenerEmpresaRelacionada(tipoEmpresaRelacionada);
        }

        public bool ValidarUsuarioPorContrasenaOperacion(int idUsuario, string contrasenaOperacion)
        {
            return new UsuarioBusinessLogic().ValidarUsuarioPorContrasenaOperacion(idUsuario, contrasenaOperacion);
        }
    }
}
