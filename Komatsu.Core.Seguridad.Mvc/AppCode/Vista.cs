using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")]
public struct ParametrosPartialView
{
    #region Nombre Vistas
    //VISTA PARCIALES USADAS PARA EMPRESA
    public const string Consultar_Empresa_ConfiguracionPV = "~/Views/Configuracion/Consultar_Empresa_ConfiguracionPV.cshtml";
    public const string Consultar_Empresa_Configuracion_Grilla = "~/Views/Configuracion/Consultar_Empresa_Configuracion_Grilla.cshtml";
    public const string Gestionar_Empresa_ConfiguracionPV = "~/Views/Configuracion/Gestionar_Empresa_ConfiguracionPV.cshtml";

    //VISTA PARCIALES USADAS PARA APLICACION
    public const string Consultar_Aplicacion_ConfiguracionPV = "~/Views/Configuracion/Consultar_Aplicacion_ConfiguracionPV.cshtml";
    public const string Consultar_Aplicacion_Configuracion_Grilla = "~/Views/Configuracion/Consultar_Aplicacion_Configuracion_Grilla.cshtml";
    public const string Gestionar_Aplicacion_ConfiguracionPV = "~/Views/Configuracion/Gestionar_Aplicacion_ConfiguracionPV.cshtml";
    public const string A_EmpresaPV = "~/Views/Configuracion/A_EmpresaPV.cshtml";

    //VISTA PARCIALES USADAS PARA MODULO
    public const string Consultar_Modulo_ConfiguracionPV = "~/Views/Configuracion/Consultar_Modulo_ConfiguracionPV.cshtml";
    public const string Consultar_Modulo_Configuracion_Grilla = "~/Views/Configuracion/Consultar_Modulo_Configuracion_Grilla.cshtml";
    public const string Gestionar_Modulo_ConfiguracionPV = "~/Views/Configuracion/Gestionar_Modulo_ConfiguracionPV.cshtml";
    public const string M_EmpresaPV = "~/Views/Configuracion/M_EmpresaPV.cshtml";
    public const string M_AplicacionPV = "~/Views/Configuracion/M_AplicacionPV.cshtml";

    //VISTA PARCIALES USADAS PARA PAGINA
    public const string Consultar_Pagina_ConfiguracionPV = "~/Views/Configuracion/Consultar_Pagina_ConfiguracionPV.cshtml";
    public const string Gestionar_Pagina_ConfiguracionPV = "~/Views/Configuracion/Gestionar_Pagina_ConfiguracionPV.cshtml";
    public const string Consultar_Accion_Configuracion_Grilla = "~/Views/Configuracion/Consultar_Accion_Configuracion_Grilla.cshtml";
    public const string Consultar_Pagina_Configuracion_Grilla = "~/Views/Configuracion/Consultar_Pagina_Configuracion_Grilla.cshtml";
    public const string P_EmpresaPV = "~/Views/Configuracion/P_EmpresaPV.cshtml";
    public const string P_AplicacionPV = "~/Views/Configuracion/P_AplicacionPV.cshtml";
    public const string P_ModuloPV = "~/Views/Configuracion/P_ModuloPV.cshtml";
    public const string PG_AplicacionPV = "~/Views/Configuracion/PG_AplicacionPV.cshtml";
    public const string PG_ModuloPV = "~/Views/Configuracion/PG_ModuloPV.cshtml";
    public const string PG_PaginaPadrePV = "~/Views/Configuracion/PG_PaginaPadrePV.cshtml";
    public const string PG_AgrupacionPV = "~/Views/Configuracion/PG_AgrupacionPV.cshtml";

    public const string PGP_AplicacionPartialView = "~/Views/Configuracion/Pagina/PGP_AplicacionPartialView.cshtml";
    public const string PGP_ModuloPartialView = "~/Views/Configuracion/Pagina/PGP_ModuloPartialView.cshtml";

    public const string PGP_AplicacionEditPartialView = "~/Views/Configuracion/Pagina/PGP_AplicacionEditPartialView.cshtml";
    public const string PGP_ModuloEditPartialView = "~/Views/Configuracion/Pagina/PGP_ModuloEditPartialView.cshtml";

    public const string PGP_PaginaPadreEditPartialView = "~/Views/Configuracion/Pagina/PGP_PaginaPadreEditPartialView.cshtml";
    public const string PG_PaginaPadrePartialView = "~/Views/Configuracion/Pagina/PG_PaginaPadrePartialView.cshtml";
    public const string PG_PaginaTipoPartialView = "~/Views/Configuracion/Pagina/PG_PaginaTipoPartialView.cshtml";

    //VISTA PARCIALES USADAS PARA ROl
    public const string Consultar_Rol_Grilla = "~/Views/Rol/Consultar_Rol_Grilla.cshtml";
    public const string Gestionar_Rol_PV = "~/Views/Rol/Gestionar_Rol_PV.cshtml";

    //VISTA PARCIALES USADAS PARA USUARIO
    public const string Consultar_Usuario_Grilla = "~/Views/Usuario/Consultar_Usuario_Grilla.cshtml";
    public const string U_AplicacionPV = "~/Views/Usuario/AplicacionPV.cshtml";
    public const string Gestionar_Usuario = "~/Views/Usuario/Gestionar_Usuario.cshtml";
    public const string GestionarUsuario = "~/Views/Usuario/GestionarUsuario.cshtml";
    public const string U_RolesPV = "~/Views/Usuario/RolPV.cshtml";
    public const string Gestionar_Usuario_PV = "~/Views/Usuario/EditarUsuario.cshtml";
    public const string VerRoles_Usuario = "~/Views/Usuario/VerRolesPorUsuario.cshtml";
    public const string U_AreaPV = "~/Views/Usuario/AreaPV.cshtml";
    public const string U_AreaPV_Edit = "~/Views/Usuario/AreaPV_Edit.cshtml";
    public const string U_EmpresaRelacionadaPV = "~/Views/Usuario/EmpresaRelacionadaPV.cshtml";

    //VISTA PARCIALES USADAS PARA PERFIL
    public const string AplicacionPV = "~/Views/Perfil/AplicacionPV.cshtml";
    public const string RolPV = "~/Views/Perfil/RolPV.cshtml";
    public const string RolesPV = "~/Views/Perfil/RolesPV.cshtml";

    //VISTA PARCIAL USADA PARA REPORTES
    public const string VerReporteEmpresa = "~/Views/Reporte/VerReporteEmpresa.cshtml";
    public const string VerReporteAplicacion = "~/Views/Reporte/VerReporteAplicacion.cshtml";
    public const string VerReporteRoles = "~/Views/Reporte/VerReporteRoles.cshtml";
    public const string VerReporteUsuarios = "~/Views/Reporte/VerReporteUsuarios.cshtml";

    #endregion

}

[SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")]
public struct ParametrosViewUserControl
{
    #region Nombre Controles de Usuario



    #endregion
}

[SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")]
public struct ParametrosController
{
    #region Nombre Controladoras


    #endregion
}

[SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")]
public struct ParametrosAction
{
    #region Nombre Action

    #endregion
}

[SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")]
public struct ParametrosSession
{
    #region Nombre Session

    #endregion
}


[SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")]
public struct Parametros
{
    
}
