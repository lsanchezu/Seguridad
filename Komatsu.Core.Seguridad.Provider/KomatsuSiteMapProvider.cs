using System;
using System.Collections.Generic;
using System.Web;
using Komatsu.Core.Seguridad.Common;
using Komatsu.Core.Seguridad.ServicioPerfil;
using MvcSiteMapProvider;

namespace Komatsu.Core.Seguridad.Provider
{
    public class KomatsuNodeProvider : DynamicNodeProviderBase
    {
        //public override IEnumerable<DynamicNode> GetDynamicNodeCollection()
        //{
        //    var idioma = Thread.CurrentThread.CurrentUICulture.Name.Substring(0, 2);
        //    if (idioma != "es" && idioma != "pt")
        //        idioma = "es";
        //    string url = "";
        //    ListaPagina listaMenu = new ListaPagina();

        //    if (System.Web.HttpContext.Current.Session["NombreUsuario"] != null)
        //    {
        //        string UserName = System.Web.HttpContext.Current.Session["NombreUsuario"].ToString();
        //        int IdAplicacion = (int)System.Web.HttpContext.Current.Session["IdAplicacion"];
        //        listaMenu = new PermisoBusinessLogic().SiteMapByUser(UserName, IdAplicacion, idioma);
        //    }
        //    foreach (Pagina item in listaMenu)
        //    {
        //        Dictionary<string, string> attr = new Dictionary<string, string>();
        //        attr.Add("level", item.Nivel.ToString());
        //        if (string.IsNullOrEmpty(item.Url))
        //        {
        //            url += "#";
        //            attr.Add("imageUrl", item.UrlImagen);
                   

        //            yield return new DynamicNode
        //            {
        //                Key = item.CodigoPagina,
        //                ParentKey =  item.CodigoPaginaPadre ,
        //                Title = item.Nombre,
        //                Controller = null,
        //                Action = null,
        //                RouteValues = new Dictionary<string, object> { { "url", url } },
        //                Attributes = attr
        //            };
        //        }
        //        else
        //        {
        //            attr.Add("visibility", item.Visible ? "SiteMapPathHelper" : "!*");
        //            attr.Add("imageUrl", !string.IsNullOrEmpty(item.UrlImagen) ? item.UrlImagen : null);
        //            yield return new DynamicNode
        //            {
        //                Key = item.CodigoPagina,
        //                ParentKey =  item.CodigoPaginaPadre ,
        //                Title = item.Nombre,
        //                Controller = item.Url.Substring(0, item.Url.IndexOf("/")),
        //                Action = item.Url.Substring(item.Url.IndexOf("/") + 1),
        //                Attributes = attr
        //            };
        //        }

        //    }
        //}

        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            string url = string.Empty;
            ListaPagina oListaPagina = new ListaPagina();

            using (ServicioPerfilClient client = new ServicioPerfilClient())
            {
                if (HttpContext.Current.Session["Usuario"] != null)
                {
                    ServicioUsuario.Usuario oUsuario = new ServicioUsuario.Usuario();
                    oListaPagina = client.ObtenerSitemapPorUsuario(
                        ((DataContracts.Usuario)HttpContext.Current.Session["Usuario"]).UserName, 
                        Constantes.IdAplicacion
                    );
                }

            }

            foreach (Pagina item in oListaPagina)
            {
                DynamicNode dynamicNode = new DynamicNode();
                Dictionary<string, object> attr = new Dictionary<string, object>();
                attr.Add("visibility", Convert.ToInt32(item.Nivel) == 1 ? "SiteMapPathHelper" : "!*");
                //attr.Add("imagSeUrl", !string.IsNullOrEmpty(item.Icono) ? item.Icono : "");
                
                dynamicNode.Key = item.IdPagina.ToString();
                dynamicNode.ParentKey = item.IdPaginaPadre != 0 ? item.IdPaginaPadre.ToString() : null;
                dynamicNode.Title = item.Nombre;
                dynamicNode.Controller = item.Url != null ? item.Url.Substring(0, item.Url.IndexOf("/")) : "Home";
                dynamicNode.Action = item.Url != null ? item.Url.Substring(item.Url.IndexOf("/") + 1) : "Index";
                dynamicNode.ImageUrl = item.Icono != null ? item.Icono : null;
                dynamicNode.Attributes = attr;

                yield return dynamicNode;
            }
        }
    }
}

