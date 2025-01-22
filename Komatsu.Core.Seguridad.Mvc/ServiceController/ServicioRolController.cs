using Komatsu.Core.Seguridad.ServicioRol;

namespace Komatsu.Core.Seguridad.Mvc.ServiceController
{
    public class ServicioRolController
    {
        private static ServicioRolController _Instancia;

        public static ServicioRolController Instancia
        {
            get { return _Instancia ?? (_Instancia = new ServicioRolController()); }
        }
        
        public ListaRol ListarRol() 
        {
            using (ServicioRolClient client = new ServicioRolClient())
            {
                return client.ListarRol();
            }
        }

        public RolPaginacion ListarRolPaginacion(Rol rol, Paginacion paginacion)
        {
            using (ServicioRolClient client = new ServicioRolClient())
            {
                return client.ListarRolPaginacion(rol, paginacion);
            }
        }

        public Rol Buscar_Rol(Rol rol)
        {
            using (ServicioRolClient client = new ServicioRolClient())
            {
                return client.Buscar_Rol(rol);
            }
        }

        public int Insertar_Rol(Rol rol)
        {
            using (ServicioRolClient client = new ServicioRolClient())
            {
                return client.Insertar_Rol(rol);
            }
        }

        public int Actualizar_Rol(Rol rol)
        {
            using (ServicioRolClient client = new ServicioRolClient())
            {
                return client.Actualizar_Rol(rol);
            }
        }

        public int ActualizarEstado_Rol(Rol rol)
        {
            using (ServicioRolClient client = new ServicioRolClient())
            {
                return client.ActualizarEstado_Rol(rol);
            }
        }

        public ListaRol RolesPorAplicacion(Aplicacion aplicacion)
        {
            using (ServicioRolClient client = new ServicioRolClient())
            {
                return client.RolesPorAplicacion(aplicacion);
            }
        }

        public ListaAplicacion ListarAplicacion()
        {
            using (ServicioRolClient client = new ServicioRolClient())
            {
                return client.ListarAplicacion();
            }
        }

        public ListaOperacion ListarOperacion()
        {
            using (ServicioRolClient client = new ServicioRolClient())
            {
                return client.ListarOperacion();
            }
        }
    }
}