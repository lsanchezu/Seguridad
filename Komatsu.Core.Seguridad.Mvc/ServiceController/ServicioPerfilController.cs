using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Komatsu.Core.Seguridad.ServicioPerfil;

namespace Komatsu.Core.Seguridad.Mvc.ServiceController
{
    public class ServicioPerfilController
    {
        private static ServicioPerfilController _Instancia;

        public static ServicioPerfilController Instancia
        {
            get { return _Instancia ?? (_Instancia = new ServicioPerfilController()); }
        }

        public PerfilTransaccion CrearPerfil(PerfilTransaccion oPerfilTransaccion)
        {
            using (ServicioPerfilClient client = new ServicioPerfilClient())
            {
                return client.CrearPerfil(oPerfilTransaccion);
            }
        }

        public ListaTreeviewHierarchyObject ListarTreeview(Int32 IdRol, Int32 IdEmpresa)
        {
            using (ServicioPerfilClient client = new ServicioPerfilClient())
            {
                return client.ListarTreeview(IdRol, IdEmpresa);
            }
        }

        public int AsignarPerfil(ListaPermisoPerfil oListaPermisoPerfil)
        {
            using (ServicioPerfilClient client = new ServicioPerfilClient())
            {
                return client.AsignarPerfil(oListaPermisoPerfil);
            }
        }

        public ListaPaginaAccion ObtenerPermisoPerfilPorGrupo(int IdUsuario, string IdsGrupo)
        {
            using (ServicioPerfilClient client = new ServicioPerfilClient())
            {
                return client.ObtenerPermisoPerfilPorGrupo(IdUsuario, IdsGrupo);
            }
        }

    }
}