using Komatsu.Core.Seguridad.BusinessLogic;
using Komatsu.Core.Seguridad.ServiceContracts;

namespace Komatsu.Core.Seguridad.ServiceImplementation
{
    public class ServicioPerfil : IServicioPerfil
    {
        public DataContracts.PerfilTransaccion CrearPerfil(DataContracts.PerfilTransaccion oPerfilTransaccion)
        {
            return new PerfilBusinessLogic().CrearPerfil(oPerfilTransaccion);
        }

        public DataContracts.ListaTreeviewHierarchyObject ListarTreeview(int IdRol, int IdEmpresa)
        {
            return new PerfilBusinessLogic().ListarTreeview(IdRol, IdEmpresa);
        }

        public int AsignarPerfil(DataContracts.ListaPermisoPerfil oListaPermisoPerfil)
        {
            return new PerfilBusinessLogic().AsignarPerfil(oListaPermisoPerfil);
        }

        public DataContracts.ListaPagina ObtenerSitemapPorUsuario(string nombreUsuario, int IdAplicacion)
        {
            return new PerfilBusinessLogic().ObtenerSitemapPorUsuario(nombreUsuario, IdAplicacion);
        }

        public DataContracts.ListaPagina ObtenerSitemapPorUsuarioFarmacia(string nombreUsuario, int IdAplicacion, string IP)
        {
            return new PerfilBusinessLogic().ObtenerSitemapPorUsuarioFarmacia(nombreUsuario, IdAplicacion, IP);
        }

        public DataContracts.ListaMenu ObtenerSitemap(string nombreUsuario, int idAplicacion, int idRol)
        {
            return new PerfilBusinessLogic().ObtenerSitemap(nombreUsuario, idAplicacion, idRol);
        }

        public DataContracts.ListaPaginaAccion ObtenerPermisoPerfilPorGrupo(int IdUsuario, string IdsGrupo)
        {
            return new PerfilBusinessLogic().ObtenerPermisoPerfilPorGrupo(IdUsuario, IdsGrupo);
        }
    }
}
