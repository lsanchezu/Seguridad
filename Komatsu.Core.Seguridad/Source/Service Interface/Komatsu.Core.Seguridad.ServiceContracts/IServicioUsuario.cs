using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Komatsu.Core.Seguridad.DataContracts;
using System.Net.Security;
using Komatsu.Core.Seguridad.DataContracts.GeneratedCode;

namespace Komatsu.Core.Seguridad.ServiceContracts
{
    [ServiceContract]
    public interface IServicioUsuario
    {
        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/ListarUsuarioPaginacion", ProtectionLevel = ProtectionLevel.None)]
        UsuarioPaginacion ListarUsuarioPaginacion(Usuario usuario, Int32 IdEmpresa, Int32 IdAplicacion, Int32 IdRol, Paginacion Paginacion);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/ListarUsuario", ProtectionLevel = ProtectionLevel.None)]
        ListaUsuario ListarUsuario();

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/ListarArea", ProtectionLevel = ProtectionLevel.None)]
        ListaArea ListarArea();

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/BuscarUsuario", ProtectionLevel = ProtectionLevel.None)]
        ListaUsuario BuscarUsuario(Usuario oUsuario);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/BuscarRolesPorUsuario", ProtectionLevel = ProtectionLevel.None)]
        ListaRol BuscarRolesPorUsuario(Usuario usuario);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/ListarAplicacionUsuario", ProtectionLevel = ProtectionLevel.None)]
        ListaAplicacion ListarAplicacionUsuario(String Usuario);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/ListarUsuariosxAplicacion", ProtectionLevel = ProtectionLevel.None)]
        ListaUsuario ListarUsuariosxAplicacion(Int16 IdAplicacion);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/ListarUsuarioPorRol", ProtectionLevel = ProtectionLevel.None)]
        ListaUsuario ListarUsuarioPorRol(int IdRol);
        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/ListaEnfermerasServicio", ProtectionLevel = ProtectionLevel.None)]
        ListaUsuario ListaEnfermerasServicio();
        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/ListarUsuarioAsignadosPorRol", ProtectionLevel = ProtectionLevel.None)]
        ListaUsuario ListarUsuarioAsignadosPorRol(int IdRol);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/ListarUsuarioNoAsignadosPorRol", ProtectionLevel = ProtectionLevel.None)]
        ListaUsuario ListarUsuarioNoAsignadosPorRol(int IdRol);
        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/ListarUsuariosxRol", ProtectionLevel = ProtectionLevel.None)]
        ListaRolUsusario ListarUsuariosxRol(Int32 idUsuario);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/ListarUsuarioTipo", ProtectionLevel = ProtectionLevel.None)]
        ListaUsuarioTipo ListarUsuarioTipo();

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/AutenticarUsuario", ProtectionLevel = ProtectionLevel.None)]
        Usuario AutenticarUsuario(Usuario usuario);
     
        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/AplicacionPorEmpresa", ProtectionLevel = ProtectionLevel.None)]
        ListaAplicacion AplicacionPorEmpresa(Aplicacion aplicacion);
        
        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/RolesPorAplicacion", ProtectionLevel = ProtectionLevel.None)]
        ListaRol RolesPorAplicacion(Aplicacion aplicacion);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/ListarUsuarioPorRolServicios", ProtectionLevel = ProtectionLevel.None)]
        ListaUsuario ListarUsuarioPorRolServicios(int Opc, int Rol);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioRol/Insertar_Usuario", ProtectionLevel = ProtectionLevel.None)]
        int Insertar_Usuario(Usuario Usuario, int Modo, String Contrasena);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/ActualizarEstado_Usuario", ProtectionLevel = ProtectionLevel.None)]
        int ActualizarEstado_Usuario(Usuario usuario);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/Actualizar_Usuario", ProtectionLevel = ProtectionLevel.None)]
        int Actualizar_Usuario(Usuario usuario);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/Buscar_Usuario", ProtectionLevel = ProtectionLevel.None)]
        Usuario Buscar_Usuario(Usuario usuario);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/InsertarLogAcceso", ProtectionLevel = ProtectionLevel.None)]
        int InsertarLogAcceso(string strHostName, string IP, int IdUsu);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/InsertarLog", ProtectionLevel = ProtectionLevel.None)]
        int InsertarLog(int IdAccion, int IdUsuario, string Descripcion);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/GuardarPolitica", ProtectionLevel = ProtectionLevel.None)]
        int GuardarPolitica(int Long, int Vigencia, int Diferencia, int NroMaximo, int DiasBloq, int Compleji, int CantContrHis);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/ListaPolitica", ProtectionLevel = ProtectionLevel.None)]
        ListaPolitica Listar_Politica();

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/ModoUsuario", ProtectionLevel = ProtectionLevel.None)]
        int ModoUsuario(string Username);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/AutenticarUsuario_Modo", ProtectionLevel = ProtectionLevel.None)]
        Usuario AutenticarUsuario_Modo(Usuario usuario, string Contrasena);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioRol/ValidarContrasena", ProtectionLevel = ProtectionLevel.None)]
        int ValidarContrasena(Int32 IdUsuario, string Contrasena);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioRol/UpdatContrasena", ProtectionLevel = ProtectionLevel.None)]
        int UpdatContrasena(Int32 IdUsuario, string Contrasena);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioRol/Actualizar_Contrasenia", ProtectionLevel = ProtectionLevel.None)]
        int Actualizar_Contrasenia(Int32 IdUsuario, string Contrasena);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/BloquearUsuario", ProtectionLevel = ProtectionLevel.None)]
        int BloquearUsuario(string Username);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/AuthenticateUser", ProtectionLevel = ProtectionLevel.None)]
        Usuario AuthenticateUser(Usuario usuario);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/ExistUserActiveDirectory", ProtectionLevel = ProtectionLevel.None)]
        bool ExistUserActiveDirectory(string usuario);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/ExistCotrasenaOperacion", ProtectionLevel = ProtectionLevel.None)]
        bool ExistCotrasenaOperacion(string contrasenaoperacion);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/GetUsuariobyContrasenaOperacion", ProtectionLevel = ProtectionLevel.None)]
        Usuario GetUsuariobyContrasenaOperacion(string contrasenaoperacion);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/ObtenerEmpresaRelacionada", ProtectionLevel = ProtectionLevel.None)]
        ListaEmpresa ObtenerEmpresaRelacionada(string tipoEmpresaRelacionada);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioUsuario/ValidarUsuarioPorContrasenaOperacion", ProtectionLevel = ProtectionLevel.None)]
        bool ValidarUsuarioPorContrasenaOperacion(int idUsuario, string contrasenaOperacion);
    }
}
