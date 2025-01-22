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
    public interface IServicioConfiguracion
    {
        #region Empresa
        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/ListarEmpresaPaginacion", ProtectionLevel = ProtectionLevel.None)]
        EmpresaPaginacion ListarEmpresaPaginacion(Empresa empresa, Paginacion paginacion);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/BuscarEmpresa", ProtectionLevel = ProtectionLevel.None)]
        Empresa BuscarEmpresa(Empresa empresa);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/Insertar_Empresa", ProtectionLevel = ProtectionLevel.None)]
        int Insertar_Empresa(Empresa empresa);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/Actualizar_Empresa", ProtectionLevel = ProtectionLevel.None)]
        int Actualizar_Empresa(Empresa empresa);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/ActualizarEstado_Empresa", ProtectionLevel = ProtectionLevel.None)]
        int ActualizarEstado_Empresa(Empresa empresa);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/ListarEmpresa", ProtectionLevel = ProtectionLevel.None)]
        ListaEmpresa ListarEmpresa();

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/BuscarEmpresasPorAplicacion", ProtectionLevel = ProtectionLevel.None)]
        ListaEmpresa BuscarEmpresasPorAplicacion(int IdAplicacion);

        #endregion

        #region Aplicacion
        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/ListarAplicacionPaginacion", ProtectionLevel = ProtectionLevel.None)]
        AplicacionPaginacion ListarAplicacionPaginacion(Aplicacion aplicacion, Paginacion paginacion);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/BuscarAplicacion", ProtectionLevel = ProtectionLevel.None)]
        Aplicacion BuscarAplicacion(Aplicacion aplicacion);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/Insertar_Aplicacion", ProtectionLevel = ProtectionLevel.None)]
        string Insertar_Aplicacion(Aplicacion aplicacion);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/Actualizar_Aplicacion", ProtectionLevel = ProtectionLevel.None)]
        int Actualizar_Aplicacion(Aplicacion aplicacion);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/ActualizarEstado_Aplicacion", ProtectionLevel = ProtectionLevel.None)]
        int ActualizarEstado_Aplicacion(Aplicacion aplicacion);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/ListarAplicacion", ProtectionLevel = ProtectionLevel.None)]
        ListaAplicacion ListarAplicacion();

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/AplicacionPorEmpresa", ProtectionLevel = ProtectionLevel.None)]
        ListaAplicacion AplicacionPorEmpresa(Aplicacion aplicacion);

        #endregion

        #region Modulo
        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/ListarModuloPaginacion", ProtectionLevel = ProtectionLevel.None)]
        ModuloPaginacion ListarModuloPaginacion(Modulo modulo, Paginacion paginacion);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/BuscarModulo", ProtectionLevel = ProtectionLevel.None)]
        Modulo BuscarModulo(Modulo modulo);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/Insertar_Modulo", ProtectionLevel = ProtectionLevel.None)]
        int Insertar_Modulo(Modulo modulo);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/Actualizar_Modulo", ProtectionLevel = ProtectionLevel.None)]
        int Actualizar_Modulo(Modulo modulo);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/ActualizarEstado_Modulo", ProtectionLevel = ProtectionLevel.None)]
        int ActualizarEstado_Modulo(Modulo modulo);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/ListarModulo", ProtectionLevel = ProtectionLevel.None)]
        ListaModulo ListarModulo();

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/ModuloPorAplicacion", ProtectionLevel = ProtectionLevel.None)]
        ListaModulo ModuloPorAplicacion(Modulo modulo);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/getListAgrupacionModulo", ProtectionLevel = ProtectionLevel.None)]
        ListaAgrupacion getListAgrupacionModulo(int IdModulo);

        #endregion

        #region Pagina
        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/ListarPaginaPaginacion", ProtectionLevel = ProtectionLevel.None)]
        PaginaPaginacion ListarPaginaPaginacion(Pagina pagina, Paginacion paginacion);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/BuscarPagina", ProtectionLevel = ProtectionLevel.None)]
        Pagina BuscarPagina(Pagina pagina);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/Insertar_Pagina", ProtectionLevel = ProtectionLevel.None)]
        int Insertar_Pagina(Pagina pagina, ListaPaginaAccion listaPaginaAccion);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/Actualizar_Pagina", ProtectionLevel = ProtectionLevel.None)]
        int Actualizar_Pagina(Pagina pagina, ListaPaginaAccion listaPaginaAccion);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/ActualizarEstado_Pagina", ProtectionLevel = ProtectionLevel.None)]
        int ActualizarEstado_Pagina(Pagina pagina);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/ListarPagina", ProtectionLevel = ProtectionLevel.None)]
        ListaPagina ListarPagina();

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/PaginaPorModulo", ProtectionLevel = ProtectionLevel.None)]
        ListaPagina PaginaPorModulo(Pagina pagina);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/PaginaPadreListar", ProtectionLevel = ProtectionLevel.None)]
        ListaPagina PaginaPadreListar(Pagina pagina);
        
        #endregion

        #region Accion
        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/ListarAccionPaginacion", ProtectionLevel = ProtectionLevel.None)]
        PaginaAccionPaginacion ListarAccionPaginacion(PaginaAccion paginaAccion, Paginacion paginacion);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/AsignarPaginaAccion", ProtectionLevel = ProtectionLevel.None)]
        int AsignarPaginaAccion(ListaPaginaAccion listaPaginaAccion);

        #endregion

        #region Grupo
        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/ListarGrupo", ProtectionLevel = ProtectionLevel.None)]
        ListaGrupo ListarGrupo();

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/AsignarPaginaGrupo", ProtectionLevel = ProtectionLevel.None)]
        int AsignarPaginaGrupo(ListaPaginaGrupo listaPaginaGrupo);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/PaginaGrupo_Eliminar", ProtectionLevel = ProtectionLevel.None)]
        int PaginaGrupo_Eliminar(Pagina pagina);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/GruposPorPagina", ProtectionLevel = ProtectionLevel.None)]
        ListaGrupo GruposPorPagina(Pagina pagina);

        #endregion

        #region OEFA
        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/ListarUnidadOrganica", ProtectionLevel = ProtectionLevel.None)]
        ListaUnidadOrganica ListarUnidadOrganica();

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreSeguridadServicioConfiguracion/ListarArea", ProtectionLevel = ProtectionLevel.None)]
        ListaArea ListarArea(int idunidadorganica);
        #endregion
    }
}
