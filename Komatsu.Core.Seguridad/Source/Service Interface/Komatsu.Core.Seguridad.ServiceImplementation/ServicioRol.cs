using System;
using Komatsu.Core.Seguridad.BusinessLogic;
using Komatsu.Core.Seguridad.DataContracts;
using Komatsu.Core.Seguridad.ServiceContracts;

namespace Komatsu.Core.Seguridad.ServiceImplementation
{
    public class ServicioRol : IServicioRol
    {
        public RolPaginacion ListarRolPaginacion(Rol rol, Paginacion paginacion)
        {
            return new RolBusinessLogic().ListarRolPaginacion(rol, paginacion);
        }

        public ListaRol ListarRol()
        {
            return new RolBusinessLogic().ListarRol();
        }

        public Rol Buscar_Rol(Rol rol)
        {
            return new RolBusinessLogic().Buscar_Rol(rol);
        }

        public int Insertar_Rol(Rol rol)
        {
            return new RolBusinessLogic().Insertar_Rol(rol);
        }

        public int Actualizar_Rol(Rol rol)
        {
            return new RolBusinessLogic().Actualizar_Rol(rol);
        }

        public int ActualizarEstado_Rol(Rol rol)
        {
            return new RolBusinessLogic().ActualizarEstado_Rol(rol);
        }

        public ListaRol RolesPorAplicacion(Aplicacion aplicacion)
        {
            return new RolBusinessLogic().RolesPorAplicacion(aplicacion);
        }

        public ListaRol BuscarRolesPorUsuario(Usuario Usuario)
        {
            return new RolBusinessLogic().BuscarRolesPorUsuario(Usuario);
        }

        public ListaAplicacion ListarAplicacion()
        {
            return new RolBusinessLogic().ListarAplicacion();
        }

        public ListaOperacion ListarOperacion()
        {
            return new RolBusinessLogic().ListarOperacion();
        }

        public ListaOperacion ListarOperacionesPorRol(int idRol)
        {
            return new RolBusinessLogic().ListarOperacionesPorRol(idRol);
        }
    }
}
