using System;
using Komatsu.Core.Seguridad.DataAccess;
using Komatsu.Core.Seguridad.DataContracts;
using System.Transactions;

namespace Komatsu.Core.Seguridad.BusinessLogic
{
    public class RolBusinessLogic
    {
        public RolPaginacion ListarRolPaginacion(Rol rol, Paginacion paginacion)
        {
            return new RolDataAccess().ListarRolPaginacion(rol, paginacion);   
        }

        public ListaRol ListarRol()
        {
           return new RolDataAccess().ListarRol();
        }

        public Rol Buscar_Rol(Rol rol)
        {
            return new RolDataAccess().Buscar_Rol(rol);
        }

        public int Insertar_Rol(Rol rol)
        {
            int result = 0;
            var RolAdmi = (new RolDataAccess().ListarRol()).Find(x => x.Aplicacion.IdAplicacion == rol.Aplicacion.IdAplicacion && x.SiSuperAdmi && x.IdRol != rol.IdRol && rol.SiSuperAdmi);
            if (RolAdmi == null)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    result = new RolDataAccess().Insertar_Rol(rol);
                    if (result > 0)
                    {
                        result = 1;
                        scope.Complete();
                    }
                        
                }
            }
            else
                result = -2;

            return result;
        }

        public int Actualizar_Rol(Rol rol)
        {
            int result = 0;
            var RolAdmi = (new RolDataAccess().ListarRol()).Find(x => x.Aplicacion.IdAplicacion == rol.Aplicacion.IdAplicacion && x.SiSuperAdmi && x.IdRol != rol.IdRol && rol.SiSuperAdmi);
            if (RolAdmi == null)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    result = new RolDataAccess().Actualizar_Rol(rol);
                    if (result > 0)
                        scope.Complete();
                }
            }
            else
                result = -2;

            return result;
        }

        public int ActualizarEstado_Rol(Rol rol)
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                result = new RolDataAccess().ActualizarEstado_Rol(rol);
                if (result > 0)
                    scope.Complete();
            }
            return result;
        }

        public ListaRol RolesPorAplicacion(Aplicacion aplicacion)
        {
            return new RolDataAccess().RolesPorAplicacion(aplicacion);
        }

        public ListaRol BuscarRolesPorUsuario(Usuario Usuario)
        {
            return new RolDataAccess().BuscarRolesPorUsuario(Usuario);
        }


        public ListaAplicacion ListarAplicacion()

        {
            return new RolDataAccess().ListarAplicacion();
        }

        public ListaOperacion ListarOperacion()
        {
            return new RolDataAccess().ListarOperacion();
        }

        public ListaOperacion ListarOperacionesPorRol(int idRol)
        {
            return new RolDataAccess().ListarOperacionesPorRol(idRol);
        }
    }
}
