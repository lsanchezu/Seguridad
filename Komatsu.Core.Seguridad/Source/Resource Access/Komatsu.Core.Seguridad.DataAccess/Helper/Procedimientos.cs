using System;

namespace Komatsu.Core.Seguridad.DataAccess.Helper
{
    public class Procedimientos
    {
        #region Procedimientos Almacenados - Log
        public const String Usp_AuditoriaAcceso = "[Seguridad].[Usp_AuditoriaAcceso]";
        public const String Usp_InsAuditoriaTransa = "[Seguridad].[Usp_InsAuditoriaTransa]";
        public const String Usp_Gest_Politica = "[Seguridad].[Usp_Gest_Politica]";
        public const String Usp_Ins_Auditoria = "[Auditoria].[Usp_Ins_Auditoria]";
        public const string Usp_Ins_Auditoria_UsuarioRol = "[Auditoria].[Usp_Ins_Auditoria_UsuarioRol]";

        #endregion

        #region Procedimientos Almacenados - Configuración
        #region Empresa
        public const String USP_EMPRESA_PAGINACION = "[Seguridad].[Usp_Empresa_Paginacion]";
            public const String USP_EMPRESA_LISTAR = "[Seguridad].[Usp_Empresa_Listar]";
            public const String USP_EMPRESA_INSERTAR = "[Seguridad].[Usp_Empresa_Insertar]";
            public const String USP_EMPRESA_ACTUALIZAR = "[Seguridad].[Usp_Empresa_Actualizar]";
            public const String USP_EMPRESA_BUSCAR = "[Seguridad].[Usp_Empresa_Buscar]";
            public const String USP_EMPRESA_ACTUALIZARESTADO = "[Seguridad].[Usp_Empresa_ActualizarEstado]";
            public const String USP_EMPRESASPORAPLICACION = "[Seguridad].[Usp_EmpresasPorAplicacion]";
            public const String USP_EMPRESA_NOMBRE = "[Seguridad].[Usp_Empresa_Nombre]";

            #endregion

            #region Aplicación
            public const String USP_APLICACION_PAGINACION = "[Seguridad].[Usp_Aplicacion_Paginacion]";
            public const String USP_APLICACION_LISTAR = "[Seguridad].[Usp_Aplicacion_Listar]";
            public const String USP_APLICACION_INSERTAR = "[Seguridad].[Usp_Aplicacion_Insertar]";
            public const String USP_APLICACION_ACTUALIZAR = "[Seguridad].[Usp_Aplicacion_Actualizar]";
            public const String USP_APLICACION_BUSCAR = "[Seguridad].[Usp_Aplicacion_Buscar]";
            public const String USP_APLICACION_ACTUALIZARESTADO = "[Seguridad].[Usp_Aplicacion_ActualizarEstado]";
            public const String USP_APLICACIONPOREMPRESA = "[Seguridad].[Usp_AplicacionPorEmpresa]";
            public const String USP_EMPRESAAPLICACION_ELIMINAR = "[Seguridad].[Usp_EmpresaAplicacion_Eliminar]";
            public const String USP_EMPRESAAPLICACION_INSERTAR = "[Seguridad].[Usp_EmpresaAplicacion_Insertar]";
            public const String USP_APLICACION_EXISTE = "[Seguridad].[Usp_Aplicacion_Existe]";
        

            #endregion

            #region Modulo
            public const String USP_MODULO_PAGINACION = "[Seguridad].[Usp_Modulo_Paginacion]";
            public const String USP_MODULO_LISTAR = "[Seguridad].[Usp_Modulo_Listar]";
            public const String USP_MODULO_INSERTAR = "[Seguridad].[Usp_Modulo_Insertar]";
            public const String USP_MODULO_ACTUALIZAR = "[Seguridad].[Usp_Modulo_Actualizar]";
            public const String USP_MODULO_BUSCAR = "[Seguridad].[Usp_Modulo_Buscar]";
            public const String USP_MODULO_ACTUALIZARESTADO = "[Seguridad].[Usp_Modulo_ActualizarEstado]";
            public const String USP_MODULOPORAPLICACION = "[Seguridad].[Usp_ModuloPorAplicacion]";   
            public const String Usp_geListAgrupacion =  "Seguridad.Usp_geListAgrupacion";

            #endregion

            #region Página
            public const String USP_PAGINA_PAGINACION = "[Seguridad].[Usp_Pagina_Paginacion]";
            public const String USP_PAGINA_LISTAR = "[Seguridad].[Usp_Pagina_Listar]";
            public const String USP_PAGINA_INSERTAR = "[Seguridad].[Usp_Pagina_Insertar]";
            public const String USP_PAGINA_ACTUALIZAR = "[Seguridad].[Usp_Pagina_Actualizar]";
            public const String USP_PAGINA_BUSCAR = "[Seguridad].[Usp_Pagina_Buscar]";
            public const String USP_PAGINA_ACTUALIZARESTADO = "[Seguridad].[Usp_Pagina_ActualizarEstado]";
            public const String USP_PAGINAPORMODULO = "[Seguridad].[Usp_PaginaPorModulo]";
            public const String USP_PAGINAPADRE_LISTAR = "[Seguridad].[Usp_PaginaPadre_Listar]";        
            
            #endregion

            #region Acción
            public const String USP_ACCION_PAGINACION = "[Seguridad].[Usp_Accion_Paginacion]";
            public const String USP_ACCION_BUSCAR = "[Seguridad].[Usp_Accion_Buscar]";
            public const String USP_PAGINAACCION_ELIMINAR = "[Seguridad].[Usp_PaginaAccion_Eliminar]";
            public const String USP_PAGINAACCION_INSERTAR = "[Seguridad].[Usp_PaginaAccion_Insertar]";

            #endregion

            #region Grupo
            public const String USP_GRUPO_LISTAR = "[Seguridad].[Usp_Grupo_Listar]";
            public const String USP_PAGINAGRUPO_ELIMINAR = "[Seguridad].[Usp_PaginaGrupo_Eliminar]";
            public const String USP_PAGINAGRUPO_INSERTAR = "[Seguridad].[Usp_PaginaGrupo_Insertar]";
            public const String USP_GRUPOSPORPAGINA = "[Seguridad].[Usp_GruposPorPagina]";

            #endregion

            #region OEFA
            public const String USP_UNIDADORGANICA_LISTAR = "[Seguridad].[Usp_UnidadOrganica_Listar]";
            public const String USP_AREA_LISTAR = "[Estructura].[Usp_Area_Listar]";

            #endregion

        #endregion

        #region Procedimientos Almacenados - Usuario

        public const String USP_USUARIO_PAGINACION = "[Seguridad].[Usp_Usuario_Paginacion]";
        public const String USP_USUARIO_LISTAR = "[Seguridad].[Usp_Usuario_Listar]";
        public const String Usp_Sel_ListaEnfermeraServicio = "[Seguridad].[Usp_Sel_ListaEnfermeraServicio]";
        public const String USP_USUARIO_LISTARPORROL = "[Seguridad].[Usp_Usuario_ListarPorRol]";
        public const String USP_USUARIO_LISTARASIGNADOSPORROL = "[Seguridad].[Usp_Usuario_ListarAsignadosPorRol]";
        public const String USP_USUARIO_LISTARNOASIGNADOSPORROL = "[Seguridad].[Usp_Usuario_ListarNoAsignadosPorRol]";

        public const String Usp_Empleados_Rol_Servicios = "Seguridad.Usp_Empleados_Rol_Servicios";

        public const String USP_USUARIO_POR_USUARIO = "Seguridad.Usp_Usuario_ListarPorUsuario";
        public const String USP_USUARIO_INSERTAR2 = "[Seguridad].[Usp_Usuario_Insertar]";
        public const String USP_USUARIO_INSERTAR = "[Seguridad].[Usp_Usuario_Insertar]";
        public const String USP_USUARIO_ACTUALIZAR = "[Seguridad].[Usp_Usuario_Actualizar]";
        public const String USP_USUARIO_BUSCAR = "[Seguridad].[Usp_Usuario_Buscar]";
        public const String USP_USUARIO_ACTUALIZARESTADO = "[Seguridad].[Usp_Usuario_ActualizarEstado]";
        public const String USP_USUARIOTIPO_LISTAR = "[Seguridad].[Usp_UsuarioTipo_Listar]";
        public const String USP_AUTENTICARUSUARIO = "[Seguridad].[Usp_AutenticarUsuario]";
        public const String USP_ROLESPORUSUARIO = "Seguridad.Usp_RolesPorUsuario ";
        public const String USP_LISTAR_APLICACIONUSUARIO = "Seguridad.Usp_Listar_AplicacionUsuario";
        public const String USP_USUARIOSXAPLICACION = "[Seguridad].[Usp_UsuariosxAplicacion]";
        public const String USP_USUARIO_ACTUALIZARESTADO2 = "[Seguridad].[Usp_Usuario_ActualizarEstado]";
        public const String USP_USUARIO_ACTUALIZAR2 = "[Seguridad].[Usp_Usuario_Actualizar]";
        public const String USP_USUARIO_BUSCAR2 = "[Seguridad].[Usp_Usuario_Buscar]";
        public const String Usp_List_Politica = "[Seguridad].[Usp_List_Politica]";
        public const String Usp_ValModo_Usuario = "[Seguridad].[Usp_ValModo_Usuario]";
        public const String Usp_AutenticarUsuario_Modo = "[Seguridad].[Usp_AutenticarUsuario_Modo]";
        public const String Usp_ValContrasena_Usuario = "[Seguridad].[Usp_ValContrasena_Usuario]";
        public const String Usp_UpdContrasena_Usuario = "[Seguridad].[Usp_UpdContrasena_Usuario]";
        public const String Usp_Bloquear_Usuario = "[Seguridad].[Usp_Bloquear_Usuario]";
        public const String Usp_ExistContrasenaOperation = "[Seguridad].[Usp_ExistContrasenaOperation]";
        public const String Usp_Upd_Usuario_Contrasena = "[Seguridad].[Usp_Upd_Usuario_Contrasena]";
        public const String Usp_UsuariobyContrasenaOperacion = "[Seguridad].[Usp_UsuariobyContrasenaOperacion]";
        public const String Usp_Sel_Empresa_Relacionada = "[Seguridad].[Usp_Sel_Empresa_Relacionada]";
        public const String Usp_ValidarUsuarioPorContrasenaOperacion = "[Seguridad].[Usp_ValidarUsuarioPorContrasenaOperacion]";
        public const String Usp_Login_Usuario = "[Seguridad].[Usp_Login_Usuario]";


        #endregion 

        #region Procedimientos Almacenados - Rol

        public const String USP_ROL_PAGINACION = "[Seguridad].[Usp_Rol_Paginacion]";
        public const String USP_ROL_LISTAR = "[Seguridad].[Usp_Rol_Listar]";
        public const String USP_ROL_INSERTAR = "[Seguridad].[Usp_Rol_Insertar]";
        public const String USP_ROL_ACTUALIZAR = "[Seguridad].[Usp_Rol_Actualizar]";
        public const String USP_ROL_BUSCAR = "[Seguridad].[Usp_Rol_Buscar]";
        public const String USP_ROL_ACTUALIZARESTADO = "[Seguridad].[Usp_Rol_ActualizarEstado]";
        public const String USP_OPERACION_LISTAR = "[Seguridad].[Usp_Operacion_Listar]";
        public const String USP_SEL_OPERACION_POR_ROL = "[Seguridad].[Usp_Sel_OperacionPorRol]";

        #endregion 

        #region Procedimientos Almacenados - Perfil
        public const String USP_USUARIOROL_CREARPERFIL = "[Seguridad].[Usp_UsuarioRol_CrearPerfil]";
        public const String USP_TREEVIEW_LISTAR = "[Seguridad].[Usp_Treeview_Listar]";
        public const String USP_PERMISOPERFIL_INSERTAR = "[Seguridad].[Usp_PermisoPerfil_Insertar]";
        public const String USP_OBTENERSITEMAP = "[Seguridad].[Usp_ObtenerSiteMap]";
        public const String USP_OBTENERSITEMAP_POR_USUARIO = "[Seguridad].[Usp_ObtenerSiteMapPorUsuario]";
        public const String USP_OBTENERSITEMAPFARMACIA = "[Seguridad].[Usp_ObtenerSiteMapFarmacia]";
        public const String USP_PERMISOPERFILPORGRUPOS = "[Seguridad].[Usp_PermisoPerfilPorGrupos]";
        public const String USP_ROLESPORAPLICACION = "[Seguridad].[Usp_RolesPorAplicacion]";
        
        #endregion

        public struct Usuario
        {
            public const string USP_OBTENER_LISTA_USUARIOXROL = "Seguridad.ObtenerRolUsuario";
        }
    }
}
