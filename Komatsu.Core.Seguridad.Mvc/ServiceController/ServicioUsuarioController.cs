using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Komatsu.Core.Seguridad.ServicioUsuario;
using conf = Komatsu.Core.Seguridad.ServicioConfiguracion;

namespace Komatsu.Core.Seguridad.Mvc.ServiceController
{
    public class ServicioUsuarioController
    {
        public ServicioUsuarioController()
        {

        }

        private static ServicioUsuarioController _Instancia;

        public static ServicioUsuarioController Instancia
        {
            get { return _Instancia ?? (_Instancia = new ServicioUsuarioController()); }
        }

        public UsuarioPaginacion ListarUsuarioPaginacion(Usuario usuario, Int32 IdEmpresa, Int32 IdAplicacion, Int32 IdRol, Paginacion paginacion)
        {
            using (ServicioUsuarioClient client = new ServicioUsuarioClient())
            {
                return client.ListarUsuarioPaginacion(usuario,IdEmpresa,IdAplicacion,IdRol, paginacion);
            }
        }

        public ListaUsuario ListarUsuario() 
        {
            using (ServicioUsuarioClient cliente = new ServicioUsuarioClient())
            {
                return cliente.ListarUsuario();
            }
        }

        public ListaRolUsusario ObtenerListaRolxUsuario(Int32 idUsusario)
        {
            using (ServicioUsuarioClient cliente = new ServicioUsuarioClient())
            {
                return cliente.ListarUsuariosxRol(idUsusario);
            }

        }

        public ListaUsuario ListarUsuarioPorRol(int IdRol)
        {
            using (ServicioUsuarioClient cliente = new ServicioUsuarioClient())
            {
                return cliente.ListarUsuarioPorRol(IdRol);
            }
        }

        public ListaUsuarioTipo ListarUsuarioTipo()
        {
            using (ServicioUsuarioClient cliente = new ServicioUsuarioClient())
            {
                return cliente.ListarUsuarioTipo();
            }
        }

        public ListaUsuario ListarUsuarioAsignadosPorRol(int IdRol)
        {
            using (ServicioUsuarioClient cliente = new ServicioUsuarioClient())
            {
                return cliente.ListarUsuarioAsignadosPorRol(IdRol);
            }
        }

        public ListaUsuario ListarUsuarioNoAsignadosPorRol(int IdRol)
        {
            using (ServicioUsuarioClient cliente = new ServicioUsuarioClient())
            {
                return cliente.ListarUsuarioNoAsignadosPorRol(IdRol);
            }
        }

        public ListaAplicacion AplicacionPorEmpresa(Aplicacion aplicacion)
        {
            using (ServicioUsuarioClient client = new ServicioUsuarioClient())
            {
                return client.AplicacionPorEmpresa(aplicacion);
            }
        }

        public ListaRol RolesPorAplicacion(Aplicacion aplicacion)
        {
            using (ServicioUsuarioClient client = new ServicioUsuarioClient())
            {
                return client.RolesPorAplicacion(aplicacion);
            }
        }

        public int Insertar_Usuario(Usuario Usuario, int Modo, String Contrasena)
        {
            using (ServicioUsuarioClient client = new ServicioUsuarioClient())
            {
                return client.Insertar_Usuario(Usuario, Modo, Contrasena);
            }
        }

        public int ActualizarEstado_Usuario(Usuario usuario)
        {
            using (ServicioUsuarioClient client = new ServicioUsuarioClient())
            {
                return client.ActualizarEstado_Usuario(usuario);
            }
        }

        public int Actualizar_Usuario(Usuario usuario)
        {
            using (ServicioUsuarioClient client = new ServicioUsuarioClient())
            {
                return client.Actualizar_Usuario(usuario);
            }
        }

        public Usuario Buscar_Usuario(Usuario usuario)
        {
            using (ServicioUsuarioClient client = new ServicioUsuarioClient())
            {
                return client.Buscar_Usuario(usuario);
            }
        }

        public int InsertarLogAcceso(string host, string Ip, int IdUsuario)
        {
            using (ServicioUsuarioClient client = new ServicioUsuarioClient())
            {
                return client.InsertarLogAcceso(host, Ip,IdUsuario);
            }
        }

        public int InsertarLog(int IdAccion, int IdUsuario, string Descripcion)
        {
            using (ServicioUsuarioClient client = new ServicioUsuarioClient())
            {
                return client.InsertarLog(IdAccion, IdUsuario, Descripcion);
            }
        }

        public int GuardarPolitica(int Long, int Vigencia, int Diferencia, int NroMaximo, int DiasBloq, int Compleji, int CantContrHis)
        {
            using (ServicioUsuarioClient client = new ServicioUsuarioClient())
            {
                return client.GuardarPolitica(Long, Vigencia, Diferencia, NroMaximo, DiasBloq, Compleji, CantContrHis);
            }
        }

        public ListaPolitica Listar_Politica()
        {
            using (ServicioUsuarioClient client = new ServicioUsuarioClient())
            {
                return client.Listar_Politica();
            }
        }

        public int ModoUsuario(string Username)
        {
            using (ServicioUsuarioClient client = new ServicioUsuarioClient())
            {
                return client.ModoUsuario(Username);
            }
        }

        public int ValidarContrasena(Int32 IdUsuario, string Contrasena)
        {
            using (ServicioUsuarioClient client = new ServicioUsuarioClient())
            {
                return client.ValidarContrasena(IdUsuario, Contrasena);
            }
        }

        public int UpdatContrasena(Int32 IdUsuario, string Contrasena)
        {
            using (ServicioUsuarioClient client = new ServicioUsuarioClient())
            {
                return client.UpdatContrasena(IdUsuario, Contrasena);
            }
        }

        public int BloquearUsuario(string Username)
        {
            using (ServicioUsuarioClient client = new ServicioUsuarioClient())
            {
                return client.BloquearUsuario(Username);
            }
        }

        public bool ExistCotrasenaOperacion(string contrasenaoperacion)
        {
            using (ServicioUsuarioClient client = new ServicioUsuarioClient())
            {
                return client.ExistCotrasenaOperacion(contrasenaoperacion);
            }
        }

        public ListaEmpresa ObtenerEmpresaRelacionada(string tipoEmpresaRelacionada)
        {
            using (ServicioUsuarioClient client = new ServicioUsuarioClient())
            {
                return client.ObtenerEmpresaRelacionada(tipoEmpresaRelacionada);
            }
        }

        public Usuario GetUsuariobyContrasenaOperacion(string contrasenaoperacion)
        {
            using (ServicioUsuarioClient client = new ServicioUsuarioClient())
            {
                return client.GetUsuariobyContrasenaOperacion(contrasenaoperacion);
            }
        }

        public bool ValidarUsuarioPorContrasenaOperacion(int idUsuario, string contrasenaOperacion)
        {
            using (ServicioUsuarioClient client = new ServicioUsuarioClient())
            {
                return client.ValidarUsuarioPorContrasenaOperacion(idUsuario, contrasenaOperacion);
            }
        }

    }

}
