using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Komatsu.Core.Seguridad.DataContracts;
using System.ServiceModel;
using System.Net.Security;

namespace Komatsu.Core.Seguridad.ServiceContracts
{
    [ServiceContract]
    public interface IServicioPerfil
    {
        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuSSCAServicioPerfil/CrearPerfil", ProtectionLevel = ProtectionLevel.None)]
        DataContracts.PerfilTransaccion CrearPerfil(PerfilTransaccion oPerfilTransaccion);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuSSCAServicioPerfil/ListarTreeview", ProtectionLevel = ProtectionLevel.None)]
        ListaTreeviewHierarchyObject ListarTreeview(int IdRol, int IdEmpresa);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuSSCAServicioPerfil/AsignarPerfil", ProtectionLevel = ProtectionLevel.None)]
        int AsignarPerfil(ListaPermisoPerfil oListaPermisoPerfil);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuSSCAServicioPerfil/ObtenerSitemapPorUsuario", ProtectionLevel = ProtectionLevel.None)]
        ListaPagina ObtenerSitemapPorUsuario(string nombreUsuario, int IdAplicacion);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuSSCAServicioPerfil/ObtenerSitemapPorUsuarioFarmacia", ProtectionLevel = ProtectionLevel.None)]
        ListaPagina ObtenerSitemapPorUsuarioFarmacia(string nombreUsuario, int IdAplicacion, string IP);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuSSCAServicioPerfil/ObtenerPermisoPerfilPorGrupo", ProtectionLevel = ProtectionLevel.None)]
        ListaPaginaAccion ObtenerPermisoPerfilPorGrupo(int IdUsuario, string IdsGrupo);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuSSCAServicioPerfil/ObtenerSitemap", ProtectionLevel = ProtectionLevel.None)]
        ListaMenu ObtenerSitemap(string nombreUsuario, int idAplicacion, int idRol);
    }
}
