using System.Transactions;
using Komatsu.Core.Seguridad.DataAccess;
using Komatsu.Core.Seguridad.DataContracts;
using System;
using Komatsu.Core.Seguridad.Common;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Linq;

namespace Komatsu.Core.Seguridad.BusinessLogic
{
    public class PerfilBusinessLogic
    {
        public PerfilTransaccion CrearPerfil(PerfilTransaccion oPerfilTransaccion)
        {
            try
            {
                int resultado = new PerfilDataAccess().CrearPerfil(oPerfilTransaccion);
                oPerfilTransaccion.Respuesta = (resultado == Constantes.ValorUno) ? Constantes.MensajeRegistroCorrecto : Constantes.MensajeRegistroInCorrecto;
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                oPerfilTransaccion.Respuesta = Constantes.MensajeRegistroInCorrecto;
            }

            return oPerfilTransaccion;
        }


        public ListaTreeviewHierarchyObject ListarTreeview(int IdRol, int IdEmpresa)
        {
            return new PerfilDataAccess().ListarTreeview(IdRol, IdEmpresa);
        }

        public int AsignarPerfil(ListaPermisoPerfil oListaPermisoPerfil)
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                result = new PerfilDataAccess().AsignarPerfil(oListaPermisoPerfil);
                if (result > 0)
                    scope.Complete();
            }
            return result;
        }

        public ListaPagina ObtenerSitemapPorUsuario(string nombreUsuario, int IdAplicacion) 
        {
            return new PerfilDataAccess().ObtenerSitemapPorUsuario(nombreUsuario, IdAplicacion);
        }

        public ListaPagina ObtenerSitemapPorUsuarioFarmacia(string nombreUsuario, int IdAplicacion, string IP)
        {
            return new PerfilDataAccess().ObtenerSitemapPorUsuarioFarmacia(nombreUsuario, IdAplicacion, IP);
        }

        public ListaMenu ObtenerSitemap(string nombreUsuario, int idAplicacion, int idRol)
        {
            ListaMenu oListaMenu = new ListaMenu();
            ListaPagina oListaPagina = new PerfilDataAccess().ObtenerSitemap(nombreUsuario, idAplicacion, idRol);

            foreach (var modulo in oListaPagina.Where(x => x.IdPaginaPadre == 0 && x.Visible == true))
            {
                Menu menu = new Menu();
                menu.Nombre = modulo.Nombre;
                menu.Icono = modulo.Icono;
                menu.ListaMenuItem = new ListaMenuItem();

                foreach (var menuItem in oListaPagina.Where(x => x.IdPaginaPadre == modulo.IdPagina && x.Visible == true))
                {
                    MenuItem oMenuItem = new MenuItem();
                    oMenuItem.Nombre = menuItem.Nombre;
                    oMenuItem.Icono = menuItem.Icono;
                    oMenuItem.Url = menuItem.Url;

                    menu.ListaMenuItem.Add(oMenuItem);
                }

                oListaMenu.Add(menu);
            }

            return oListaMenu;
        }

        public ListaPaginaAccion ObtenerPermisoPerfilPorGrupo(int IdUsuario, string IdsGrupo) 
        {
            return new PerfilDataAccess().ObtenerPermisoPerfilPorGrupo(IdUsuario, IdsGrupo);
        }
        
    }
}
