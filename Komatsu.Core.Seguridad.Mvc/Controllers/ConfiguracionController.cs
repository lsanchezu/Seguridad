using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Komatsu.Core.Seguridad.Mvc.Models.ViewModel;
using Komatsu.Core.Seguridad.Mvc.Helpers;
using Komatsu.Core.Seguridad.Common;
using Komatsu.Core.Seguridad.Mvc.ServiceController;
using Komatsu.Core.Seguridad.ServicioConfiguracion;
using Komatsu.Core.Seguridad.Mvc.Helper;
using Komatsu.Core.Seguridad.Provider.Filters;
using Komatsu.Core.Seguridad.Validation;
using Newtonsoft.Json;

namespace Komatsu.Core.Seguridad.Mvc.Controllers
{
    [NoCacheAttribute]
    public class ConfiguracionController : Controller
    {
        [RequiresAuthenticationAttribute]
        public ActionResult Gestionar_Configuracion()
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.EmpresaPaginacion = new EmpresaPaginacion();
            VM.EmpresaPaginacion.ListaEmpresa = new ListaEmpresa();
            VM.AplicacionPaginacion = new AplicacionPaginacion();
            VM.AplicacionPaginacion.ListaAplicacion = new ListaAplicacion();
            VM.ModuloPaginacion = new ModuloPaginacion();
            VM.ModuloPaginacion.ListaModulo = new ListaModulo();
            VM.PaginaPaginacion = new PaginaPaginacion();
            VM.PaginaPaginacion.ListaPagina = new ListaPagina();
            VM.PaginaAccionPaginacion = new PaginaAccionPaginacion();
            VM.PaginaAccionPaginacion.ListaPaginaAccion = new ListaPaginaAccion();
            VM.ListaGrupo = new ListaGrupo();
            VM.Pagina = new Pagina();

            VM.ListaEmpresa = ServiceController.ServicioConfiguracionController.Instancia.ListarEmpresa();
            VM.ListaAplicacion = ServiceController.ServicioConfiguracionController.Instancia.ListarAplicacion();
            VM.ListaModulo = ServiceController.ServicioConfiguracionController.Instancia.ListarModulo();
            VM.ListaPagina = ServiceController.ServicioConfiguracionController.Instancia.ListarPagina();
            string IdsGrupo = Constantes.ConsultarEmpresa + "," + Constantes.ConsultarAplicacion + "," + Constantes.ConsultarModulo + "," + Constantes.ConsultarPagina;
            VM.ListaPaginaAccionPermiso = ServicioPerfilController.Instancia.ObtenerPermisoPerfilPorGrupo(UtilSession.Obtener_UsuarioSession().IdUsuario, IdsGrupo);
            VM.Usuario = UtilSession.Obtener_UsuarioSession();

            return View(VM);
        }

        #region Configuracion - Empresa
        [RequiresAuthenticationAttribute]
        public ActionResult Consultar_Empresa(String NombreEmpresa = Constantes.ValorDefecto,
            String sortdir = Constantes.ValorDefecto,
            String sort = Constantes.ValorDefecto,
            Int32 page = Constantes.FirstPage)
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.EmpresaPaginacion = new EmpresaPaginacion();
            VM.EmpresaPaginacion.Paginacion = new Paginacion();
            VM.EmpresaPaginacion.Paginacion.Page = page;
            VM.EmpresaPaginacion.Paginacion.RowsPerPage = Constantes.RowsPerPage;
            VM.EmpresaPaginacion.Paginacion.SortDir = sortdir;
            VM.EmpresaPaginacion.Paginacion.SortType = sort;
            VM.EmpresaPaginacion = ServicioConfiguracionController.Instancia.ListarEmpresaPaginacion(new Empresa() { Nombre = NombreEmpresa }, VM.EmpresaPaginacion.Paginacion);
            VM.EmpresaPaginacion.Paginacion.FooterDesc = UtilWebGrid.ContadorRegistrosWebGrid(VM.EmpresaPaginacion.Paginacion.Page, VM.EmpresaPaginacion.Paginacion.RowsPerPage, VM.EmpresaPaginacion.Paginacion.RowCount);
            string IdsGrupo = Constantes.ModificarEmpresa + "," + Constantes.ActDesEmpresa;
            VM.ListaPaginaAccionPermiso = ServicioPerfilController.Instancia.ObtenerPermisoPerfilPorGrupo(UtilSession.Obtener_UsuarioSession().IdUsuario, IdsGrupo);

            if (NombreEmpresa != Constantes.ValorDefecto) 
            {
                int iLog = 0;
                String DescripcionAuditoria = Constantes.ValorDefecto;
                DescripcionAuditoria = "NombreEmpresa: " + NombreEmpresa.ToString();

                int idAccion = Constantes.BuscarEmpresa;
                int IdUsuario2 = (int)Session["IdUsuario"];

                iLog = ServicioUsuarioController.Instancia.InsertarLog(idAccion, IdUsuario2, DescripcionAuditoria);
            }
            
            return PartialView(ParametrosPartialView.Consultar_Empresa_Configuracion_Grilla, VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult Editar_Empresa(Int32 IdEmpresa = Constantes.ValorCero)
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.Empresa = ServicioConfiguracionController.Instancia.BuscarEmpresa(new Empresa() { IdEmpresa = IdEmpresa });
            VM.ListaPaginaAccionPermiso = ServicioPerfilController.Instancia.ObtenerPermisoPerfilPorGrupo(UtilSession.Obtener_UsuarioSession().IdUsuario, Constantes.EditarEmpresa);    

            return PartialView(ParametrosPartialView.Gestionar_Empresa_ConfiguracionPV, VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult Registrar_Empresa()
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.Empresa = new Empresa();
            VM.ListaPaginaAccionPermiso = ServicioPerfilController.Instancia.ObtenerPermisoPerfilPorGrupo(UtilSession.Obtener_UsuarioSession().IdUsuario, Constantes.NuevaEmpresa);

            return PartialView(ParametrosPartialView.Gestionar_Empresa_ConfiguracionPV, VM);
        }
        
        [RequiresAuthenticationAttribute]
        public ActionResult Insertar_Empresa(String NombreEmpresa = Constantes.ValorDefecto, String Abreviatura = Constantes.ValorDefecto,
            String CodigoEmpresa = Constantes.ValorDefecto, String ContentStyle = Constantes.ValorDefecto)
        {
            int result = ServicioConfiguracionController.Instancia.Insertar_Empresa(new Empresa() { Nombre = NombreEmpresa, Abreviatura = Abreviatura, CodigoEmpresa = CodigoEmpresa, ContentStyle = ContentStyle });
            var mensaje = string.Empty;

            switch (result)
            {
                case -1:
                    mensaje = String.Format(Validation.Security.Security.Registro_Repetido, NombreEmpresa);
                    break;
                case 0:
                    mensaje = Validation.Security.Security.Registro_Incorrecto;
                    break;
                case 1:
                    mensaje = Validation.Security.Security.Registro_Correcto;
                    break;
            }

            if (result == 1) 
            {
                int iLog = 0;
                String DescripcionAuditoria = Constantes.ValorDefecto;
                DescripcionAuditoria = "NombreEmpresa: " + NombreEmpresa.ToString() + " |Abreviatura: " + Abreviatura.ToString()
                                        + " |CodigoEmpresa: " + CodigoEmpresa.ToString() + " |ContentStyle: " + ContentStyle.ToString();

                int idAccion = Constantes.InsertarEmpresa;
                int IdUsuario2 = (int)Session["IdUsuario"];

                iLog = ServicioUsuarioController.Instancia.InsertarLog(idAccion, IdUsuario2, DescripcionAuditoria);
            }
            var jsonMensaje = new { mensaje = mensaje };
            return Json(jsonMensaje, JsonRequestBehavior.AllowGet);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult Actualizar_Empresa(Int32 IdEmpresa = Constantes.ValorCero, String NombreEmpresa = Constantes.ValorDefecto, String Abreviatura = Constantes.ValorDefecto,
            String CodigoEmpresa = Constantes.ValorDefecto, String ContentStyle = Constantes.ValorDefecto)
        {
            int result = ServicioConfiguracionController.Instancia.Actualizar_Empresa(new Empresa() { IdEmpresa = IdEmpresa, Nombre = NombreEmpresa, Abreviatura = Abreviatura, CodigoEmpresa = CodigoEmpresa, ContentStyle = ContentStyle });
            var mensaje = string.Empty;

            switch (result)
            {
                case -1:
                    mensaje = String.Format(Validation.Security.Security.Registro_Repetido, NombreEmpresa);
                    break;
                case 0:
                    mensaje = Validation.Security.Security.Actualizacion_Incorrecta;
                    break;
                case 1:
                    mensaje = Validation.Security.Security.Actualizacion_Correcta;
                    break;
            }

            if (result == 1)
            {
                int iLog = 0;
                String DescripcionAuditoria = Constantes.ValorDefecto;
                DescripcionAuditoria = "IdEmpresa: " + IdEmpresa.ToString() + " |NombreEmpresa: " + NombreEmpresa.ToString()
                                        + " |Abreviatura: " + Abreviatura.ToString() + " |CodigoEmpresa: " + CodigoEmpresa.ToString()
                                        + " |ContentStyle: " + ContentStyle.ToString();

                int idAccion = Constantes.Modificar_Empresa;
                int IdUsuario2 = (int)Session["IdUsuario"];

                iLog = ServicioUsuarioController.Instancia.InsertarLog(idAccion, IdUsuario2, DescripcionAuditoria);
            }

            var jsonMensaje = new { mensaje = mensaje };
            return Json(jsonMensaje, JsonRequestBehavior.AllowGet);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult ActualizarEstado_Empresa(Int32 IdEmpresa = Constantes.ValorCero, Int32 IdEstado = Constantes.ValorCero)
        {
            int result = ServicioConfiguracionController.Instancia.ActualizarEstado_Empresa(new Empresa() { IdEmpresa = IdEmpresa, Estado = new Estado() { IdEstado = IdEstado } });
            var mensaje = string.Empty;

            IdEstado = (result == 0 ? IdEstado * -1 : IdEstado);

            switch (IdEstado)
            {
                case -3:
                    mensaje = Validation.Security.Security.Eliminar_Incorrecto;
                    break;
                case -2:
                    mensaje = Validation.Security.Security.Desactivar_Incorrecto;
                    break;
                case -1:
                    mensaje = Validation.Security.Security.Activar_Incorrecto;
                    break;
                case 1:
                    mensaje = Validation.Security.Security.Activar_Correcto;
                    break;
                case 2:
                    mensaje = Validation.Security.Security.Desactivar_Correcto;
                    break;
                case 3:
                    mensaje = Validation.Security.Security.Eliminar_Correcto;
                    break;
            }

            var jsonMensaje = new { mensaje = mensaje };
            return Json(jsonMensaje, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Configuracion - Aplicacion
        [RequiresAuthenticationAttribute]
        public ActionResult Consultar_Aplicacion(String IdsEmpresa = Constantes.ValorDefecto, String NombreAplicacion = Constantes.ValorDefecto, String DescripcionAplicacion = Constantes.ValorDefecto,
            String sortdir = Constantes.ValorDefecto,
            String sort = Constantes.ValorDefecto,
            Int32 page = Constantes.FirstPage)
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.AplicacionPaginacion = new AplicacionPaginacion();
            VM.AplicacionPaginacion.Paginacion = new Paginacion();
            VM.AplicacionPaginacion.Paginacion.Page = page;
            VM.AplicacionPaginacion.Paginacion.RowsPerPage = Constantes.RowsPerPage;
            VM.AplicacionPaginacion.Paginacion.SortDir = sortdir;
            VM.AplicacionPaginacion.Paginacion.SortType = sort;
            VM.AplicacionPaginacion = ServicioConfiguracionController.Instancia.ListarAplicacionPaginacion(new Aplicacion() { Nombre = NombreAplicacion, Descripcion = DescripcionAplicacion, Empresa = new Empresa() 
                { 
                    IdsEmpresa = IdsEmpresa
                } 
            }, VM.AplicacionPaginacion.Paginacion);
            VM.AplicacionPaginacion.Paginacion.FooterDesc = UtilWebGrid.ContadorRegistrosWebGrid(VM.AplicacionPaginacion.Paginacion.Page, VM.AplicacionPaginacion.Paginacion.RowsPerPage, VM.AplicacionPaginacion.Paginacion.RowCount);
            string IdsGrupo =  Constantes.ModificarAplicacion + "," + Constantes.ActDesAplicacion;
            VM.ListaPaginaAccionPermiso = ServicioPerfilController.Instancia.ObtenerPermisoPerfilPorGrupo(UtilSession.Obtener_UsuarioSession().IdUsuario, IdsGrupo);
            VM.Usuario = UtilSession.Obtener_UsuarioSession();

            if (IdsEmpresa != Constantes.ValorDefecto || NombreAplicacion != Constantes.ValorDefecto || DescripcionAplicacion != Constantes.ValorDefecto) 
            {
                int iLog = 0;
                String DescripcionAuditoria = Constantes.ValorDefecto;
                DescripcionAuditoria = "IdEmpresa: " + IdsEmpresa.ToString() + " |NombreAplicacion: " + NombreAplicacion.ToString()
                                        + " |DescripcionAplicacion: " + DescripcionAplicacion.ToString();

                int idAccion = Constantes.BuscarAplicacion;
                int IdUsuario2 = (int)Session["IdUsuario"];

                iLog = ServicioUsuarioController.Instancia.InsertarLog(idAccion, IdUsuario2, DescripcionAuditoria);
            }           

            return PartialView(ParametrosPartialView.Consultar_Aplicacion_Configuracion_Grilla, VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult Editar_Aplicacion(Int32 IdAplicacion = Constantes.ValorCero)
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.Aplicacion = ServicioConfiguracionController.Instancia.BuscarAplicacion(new Aplicacion() { IdAplicacion = IdAplicacion });
            VM.ListaEmpresa = ServicioConfiguracionController.Instancia.BuscarEmpresasPorAplicacion(IdAplicacion);
            VM.Empresa = new Empresa();
            VM.ListaPaginaAccionPermiso = ServicioPerfilController.Instancia.ObtenerPermisoPerfilPorGrupo(UtilSession.Obtener_UsuarioSession().IdUsuario, Constantes.EditarAplicacion);    

            return PartialView(ParametrosPartialView.Gestionar_Aplicacion_ConfiguracionPV, VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult Registrar_Aplicacion()
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.Aplicacion = new Aplicacion();
            VM.ListaEmpresa = ServicioConfiguracionController.Instancia.BuscarEmpresasPorAplicacion(0);
            VM.ListaPaginaAccionPermiso = ServicioPerfilController.Instancia.ObtenerPermisoPerfilPorGrupo(UtilSession.Obtener_UsuarioSession().IdUsuario, Constantes.NuevaAplicacion);

            return PartialView(ParametrosPartialView.Gestionar_Aplicacion_ConfiguracionPV, VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult Insertar_Aplicacion(String IdsEmpresa = Constantes.ValorDefecto,
            String Url = Constantes.ValorDefecto,
            String NombreAplicacion = Constantes.ValorDefecto,
            String DescripcionAplicacion = Constantes.ValorDefecto)
        {
            string result = ServicioConfiguracionController.Instancia.Insertar_Aplicacion(new Aplicacion() { Empresa = new Empresa() { IdsEmpresa = IdsEmpresa }, Url = Url.Trim(), Nombre = NombreAplicacion.Trim(), Descripcion = DescripcionAplicacion.Trim() });
            var mensaje = string.Empty;

            switch (Int32.Parse(result.Split('/')[0]))
            {
                case -1:
                    mensaje = String.Format("El Registro {0} con empresa : {1} ya se encuentra registrado", NombreAplicacion.Trim(), result.Split('/')[1]);
                    break;
                case 0:
                    mensaje = Validation.Security.Security.Registro_Incorrecto;
                    break;
                case 1:
                    mensaje = Validation.Security.Security.Registro_Correcto;
                    break;
            }

            if (Int32.Parse(result.Split('/')[0]) == 1) 
            {
                int iLog = 0;
                String DescripcionAuditoria = Constantes.ValorDefecto;
                DescripcionAuditoria = "IdEmpresa: " + IdsEmpresa.ToString() + " |Url: " + Url.ToString()
                                        + " |NombreAplicacion: " + NombreAplicacion.ToString() + " |DescripcionAplicacion: " + DescripcionAplicacion.ToString();

                int idAccion = Constantes.InsertarAplicacion;
                int IdUsuario2 = (int)Session["IdUsuario"];

                iLog = ServicioUsuarioController.Instancia.InsertarLog(idAccion, IdUsuario2, DescripcionAuditoria);
            }

            var jsonMensaje = new { mensaje = mensaje , identificador = Int32.Parse(result.Split('/')[0])};
            return Json(jsonMensaje, JsonRequestBehavior.AllowGet);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult Actualizar_Aplicacion(Int32 IdAplicacion = Constantes.ValorCero, String IdsEmpresa = Constantes.ValorDefecto,
            String Url = Constantes.ValorDefecto,
            String NombreAplicacion = Constantes.ValorDefecto,
            String DescripcionAplicacion = Constantes.ValorDefecto)
        {
            int result = ServicioConfiguracionController.Instancia.Actualizar_Aplicacion(new Aplicacion() { Empresa = new Empresa() { IdsEmpresa = IdsEmpresa }, IdAplicacion = IdAplicacion, Url = Url, Nombre = NombreAplicacion, Descripcion = DescripcionAplicacion });
            var mensaje = string.Empty;

            switch (result)
            {
                case -1:
                    mensaje = String.Format(Validation.Security.Security.Registro_Repetido, NombreAplicacion);
                    break;
                case 0:
                    mensaje = Validation.Security.Security.Actualizacion_Incorrecta;
                    break;
                case 1:
                    mensaje = Validation.Security.Security.Actualizacion_Correcta;
                    break;
            }

            if (result == 1)
            {
                int iLog = 0;
                String DescripcionAuditoria = Constantes.ValorDefecto;
                DescripcionAuditoria = "IdAplicacion: " + IdAplicacion.ToString() + " |IdsEmpresa: " + IdsEmpresa.ToString()
                                        + " |Url: " + Url.ToString() + " |NombreAplicacion: " + NombreAplicacion.ToString()
                                        + " |DescripcionAplicacion: " + DescripcionAplicacion.ToString();

                int idAccion = Constantes.Modificar_Aplicacion;
                int IdUsuario2 = (int)Session["IdUsuario"];

                iLog = ServicioUsuarioController.Instancia.InsertarLog(idAccion, IdUsuario2, DescripcionAuditoria);
            }

            var jsonMensaje = new { mensaje = mensaje };
            return Json(jsonMensaje, JsonRequestBehavior.AllowGet);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult ActualizarEstado_Aplicacion(Int32 IdAplicacion = Constantes.ValorCero, Int32 IdEstado = Constantes.ValorCero)
        {
            int result = ServicioConfiguracionController.Instancia.ActualizarEstado_Aplicacion(new Aplicacion() { IdAplicacion = IdAplicacion, Estado = new Estado() { IdEstado = IdEstado } });
            var mensaje = string.Empty;

            IdEstado = (result == 0 ? IdEstado * -1 : IdEstado);

            switch (IdEstado)
            {
                case -3:
                    mensaje = Validation.Security.Security.Eliminar_Incorrecto;
                    break;
                case -2:
                    mensaje = Validation.Security.Security.Desactivar_Incorrecto;
                    break;
                case -1:
                    mensaje = Validation.Security.Security.Activar_Incorrecto;
                    break;
                case 1:
                    mensaje = Validation.Security.Security.Activar_Correcto;
                    break;
                case 2:
                    mensaje = Validation.Security.Security.Desactivar_Correcto;
                    break;
                case 3:
                    mensaje = Validation.Security.Security.Eliminar_Correcto;
                    break;
            }

            var jsonMensaje = new { mensaje = mensaje };
            return Json(jsonMensaje, JsonRequestBehavior.AllowGet);
        }

        public ActionResult A_Empresa(Int32 IdEmpresa = Constantes.ValorCero, Int32 IdEstado = Constantes.ValorCero)
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.ListaEmpresa = ServicioConfiguracionController.Instancia.ListarEmpresa();
            VM.Empresa = new Empresa();
            VM.Empresa.IdEmpresa = IdEmpresa;

            return PartialView(ParametrosPartialView.A_EmpresaPV, VM);
        }

        #endregion

        #region Configuracion - Modulo
        [RequiresAuthenticationAttribute]
        public ActionResult Consultar_Modulo(Int32 IdEmpresa = Constantes.ValorCero, Int32 IdAplicacion = Constantes.ValorCero,
            String NombreModulo = Constantes.ValorDefecto, String DescripcionModulo = Constantes.ValorDefecto,
            String sortdir = Constantes.ValorDefecto,
            String sort = Constantes.ValorDefecto,
            Int32 page = Constantes.FirstPage)
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.ModuloPaginacion = new ModuloPaginacion();
            VM.ModuloPaginacion.Paginacion = new Paginacion();
            VM.ModuloPaginacion.Paginacion.Page = page;
            VM.ModuloPaginacion.Paginacion.RowsPerPage = Constantes.RowsPerPage;
            VM.ModuloPaginacion.Paginacion.SortDir = sortdir;
            VM.ModuloPaginacion.Paginacion.SortType = sort;
            VM.ModuloPaginacion = ServicioConfiguracionController.Instancia.ListarModuloPaginacion(new Modulo() { Nombre = NombreModulo, Descripcion = DescripcionModulo, Aplicacion = new Aplicacion() { IdAplicacion = IdAplicacion, Empresa = new Empresa() { IdEmpresa = IdEmpresa } } }, VM.ModuloPaginacion.Paginacion);
            VM.ModuloPaginacion.Paginacion.FooterDesc = UtilWebGrid.ContadorRegistrosWebGrid(VM.ModuloPaginacion.Paginacion.Page, VM.ModuloPaginacion.Paginacion.RowsPerPage, VM.ModuloPaginacion.Paginacion.RowCount);
            string IdsGrupo = Constantes.ModificarModulo + "," + Constantes.ActDesModulo;
            VM.ListaPaginaAccionPermiso = ServicioPerfilController.Instancia.ObtenerPermisoPerfilPorGrupo(UtilSession.Obtener_UsuarioSession().IdUsuario, IdsGrupo);

            if (IdEmpresa != Constantes.ValorCero || IdAplicacion != Constantes.ValorCero || NombreModulo != Constantes.ValorDefecto || DescripcionModulo != Constantes.ValorDefecto)
            {
                int iLog = 0;
                String DescripcionAuditoria = Constantes.ValorDefecto;
                DescripcionAuditoria = "IdEmpresa: " + IdEmpresa.ToString() + " |IdAplicacion: " + IdAplicacion.ToString()
                                        + " |NombreModulo: " + NombreModulo.ToString() + " |DescripcionModulo: " + DescripcionModulo.ToString();

                int idAccion = Constantes.BuscarModulo;
                int IdUsuario2 = (int)Session["IdUsuario"];

                iLog = ServicioUsuarioController.Instancia.InsertarLog(idAccion, IdUsuario2, DescripcionAuditoria);
            }      

            return PartialView(ParametrosPartialView.Consultar_Modulo_Configuracion_Grilla, VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult Editar_Modulo(Int32 IdModulo = Constantes.ValorCero)
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.Modulo = ServicioConfiguracionController.Instancia.BuscarModulo(new Modulo() { IdModulo = IdModulo });
            VM.ListaAplicacion = ServicioConfiguracionController.Instancia.ListarAplicacion();
            VM.Aplicacion = new Aplicacion();
            VM.Aplicacion.IdAplicacion = VM.Modulo.Aplicacion.IdAplicacion;
            VM.ListaPaginaAccionPermiso = ServicioPerfilController.Instancia.ObtenerPermisoPerfilPorGrupo(UtilSession.Obtener_UsuarioSession().IdUsuario, Constantes.EditarModulo);    

            return PartialView(ParametrosPartialView.Gestionar_Modulo_ConfiguracionPV, VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult Registrar_Modulo()
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.Modulo = new Modulo();
            VM.ListaAplicacion = ServicioConfiguracionController.Instancia.ListarAplicacion();
            VM.ListaPaginaAccionPermiso = ServicioPerfilController.Instancia.ObtenerPermisoPerfilPorGrupo(UtilSession.Obtener_UsuarioSession().IdUsuario, Constantes.NuevoModulo);    

            return PartialView(ParametrosPartialView.Gestionar_Modulo_ConfiguracionPV, VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult Insertar_Modulo(Int32 IdAplicacion = Constantes.ValorCero,
            String NombreModulo = Constantes.ValorDefecto,
            String DescripcionModulo = Constantes.ValorDefecto)
        {
            int result = ServicioConfiguracionController.Instancia.Insertar_Modulo(new Modulo() { Nombre = NombreModulo, Descripcion = DescripcionModulo, Aplicacion = new Aplicacion() { IdAplicacion = IdAplicacion } });
            var mensaje = string.Empty;

            switch (result)
            {
                case -1:
                    mensaje = String.Format("El módulo: {0} con aplicación: {1} ya se encuentra registrado", NombreModulo, "{1}");
                    break;
                case 0:
                    mensaje = Validation.Security.Security.Registro_Incorrecto;
                    break;
                case 1:
                    mensaje = Validation.Security.Security.Registro_Correcto;
                    break;
            }

            if (result == 1) 
            {
                int iLog = 0;
                String DescripcionAuditoria = Constantes.ValorDefecto;
                DescripcionAuditoria = "IdAplicacion: " + IdAplicacion.ToString() + " |NombreModulo: " + NombreModulo.ToString()
                                        + " |DescripcionModulo: " + DescripcionModulo.ToString();

                int idAccion = Constantes.InsertarModulo;
                int IdUsuario2 = (int)Session["IdUsuario"];

                iLog = ServicioUsuarioController.Instancia.InsertarLog(idAccion, IdUsuario2, DescripcionAuditoria);
            }

            var jsonMensaje = new { mensaje = mensaje };
            return Json(jsonMensaje, JsonRequestBehavior.AllowGet);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult Actualizar_Modulo(Int32 IdModulo = Constantes.ValorCero, Int32 IdAplicacion = Constantes.ValorCero,
            String NombreModulo = Constantes.ValorDefecto,
            String DescripcionModulo = Constantes.ValorDefecto)
        {
            int result = ServicioConfiguracionController.Instancia.Actualizar_Modulo(new Modulo() { IdModulo = IdModulo, Nombre = NombreModulo, Descripcion = DescripcionModulo, Aplicacion = new Aplicacion() { IdAplicacion = IdAplicacion } });
            var mensaje = string.Empty;

            switch (result)
            {
                case -1:
                    mensaje = String.Format(Validation.Security.Security.Registro_Repetido, NombreModulo);
                    break;
                case 0:
                    mensaje = Validation.Security.Security.Actualizacion_Incorrecta;
                    break;
                case 1:
                    mensaje = Validation.Security.Security.Actualizacion_Correcta;
                    break;
            }

            if (result == 1)
            {
                int iLog = 0;
                String DescripcionAuditoria = Constantes.ValorDefecto;
                DescripcionAuditoria = "IdModulo: " + IdModulo.ToString() + " |IdAplicacion: " + IdAplicacion.ToString()
                                        + " |NombreModulo: " + NombreModulo.ToString() + " |DescripcionModulo: " + DescripcionModulo.ToString();

                int idAccion = Constantes.Modificar_Modulo;
                int IdUsuario2 = (int)Session["IdUsuario"];

                iLog = ServicioUsuarioController.Instancia.InsertarLog(idAccion, IdUsuario2, DescripcionAuditoria);
            }

            var jsonMensaje = new { mensaje = mensaje };
            return Json(jsonMensaje, JsonRequestBehavior.AllowGet);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult ActualizarEstado_Modulo(Int32 IdModulo = Constantes.ValorCero, Int32 IdEstado = Constantes.ValorCero)
        {
            int result = ServicioConfiguracionController.Instancia.ActualizarEstado_Modulo(new Modulo() { IdModulo = IdModulo, Estado = new Estado() { IdEstado = IdEstado } });
            var mensaje = string.Empty;

            IdEstado = (result == 0 ? IdEstado * -1 : IdEstado);

            switch (IdEstado)
            {
                case -3:
                    mensaje = Validation.Security.Security.Eliminar_Incorrecto;
                    break;
                case -2:
                    mensaje = Validation.Security.Security.Desactivar_Incorrecto;
                    break;
                case -1:
                    mensaje = Validation.Security.Security.Activar_Incorrecto;
                    break;
                case 1:
                    mensaje = Validation.Security.Security.Activar_Correcto;
                    break;
                case 2:
                    mensaje = Validation.Security.Security.Desactivar_Correcto;
                    break;
                case 3:
                    mensaje = Validation.Security.Security.Eliminar_Correcto;
                    break;
            }

            var jsonMensaje = new { mensaje = mensaje };
            return Json(jsonMensaje, JsonRequestBehavior.AllowGet);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult M_AplicacionPorEmpresa(Int32 IdEmpresa = Constantes.ValorCero)
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.ListaAplicacion = ServicioConfiguracionController.Instancia.AplicacionPorEmpresa(new Aplicacion() { Empresa = new Empresa() { IdEmpresa = IdEmpresa } });

            return PartialView(ParametrosPartialView.M_AplicacionPV, VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult M_Empresa(Int32 IdEmpresa = Constantes.ValorCero)
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.ListaEmpresa = ServicioConfiguracionController.Instancia.ListarEmpresa();
            VM.Empresa = new Empresa();
            VM.Empresa.IdEmpresa = IdEmpresa;
           

            return PartialView(ParametrosPartialView.M_EmpresaPV, VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult M_Aplicacion(Int32 IdAplicacion = Constantes.ValorCero, Int32 IdEmpresa = Constantes.ValorCero)
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.ListaAplicacion = ServicioConfiguracionController.Instancia.AplicacionPorEmpresa(new Aplicacion() { Empresa = new Empresa() { IdEmpresa = IdEmpresa } });
            VM.Aplicacion = new Aplicacion();
            VM.Aplicacion.IdAplicacion = IdAplicacion;

            return PartialView(ParametrosPartialView.M_AplicacionPV, VM);
        }
        #endregion

        #region Configuracion - Pagina
        [RequiresAuthenticationAttribute]
        public ActionResult Consultar_Pagina(
            Int32 IdEmpresa = Constantes.ValorCero, 
            Int32 IdAplicacion = Constantes.ValorCero, 
            Int32 IdModulo = Constantes.ValorCero, 
            String NombrePagina = Constantes.ValorDefecto,
            String sortdir = Constantes.ValorDefecto,
            String sort = Constantes.ValorDefecto,
            Int32 page = Constantes.FirstPage)
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.PaginaPaginacion = new PaginaPaginacion();
            VM.PaginaPaginacion.Paginacion = new Paginacion();
            VM.PaginaPaginacion.Paginacion.Page = page;
            VM.PaginaPaginacion.Paginacion.RowsPerPage = Constantes.RowsPerPage;
            VM.PaginaPaginacion.Paginacion.SortDir = sortdir;
            VM.PaginaPaginacion.Paginacion.SortType = sort;
            VM.PaginaPaginacion = ServicioConfiguracionController.Instancia.ListarPaginaPaginacion(new Pagina() { Nombre = NombrePagina, Modulo = new Modulo() { IdModulo = IdModulo, Aplicacion = new Aplicacion() { IdAplicacion = IdAplicacion, Empresa = new Empresa() { IdEmpresa = IdEmpresa } } } }, VM.PaginaPaginacion.Paginacion);
            VM.PaginaPaginacion.Paginacion.FooterDesc = UtilWebGrid.ContadorRegistrosWebGrid(VM.PaginaPaginacion.Paginacion.Page, VM.PaginaPaginacion.Paginacion.RowsPerPage, VM.PaginaPaginacion.Paginacion.RowCount);
            string IdsGrupo = Constantes.ModificarPagina + "," + Constantes.ActDesPagina;
            VM.ListaPaginaAccionPermiso = ServicioPerfilController.Instancia.ObtenerPermisoPerfilPorGrupo(UtilSession.Obtener_UsuarioSession().IdUsuario, IdsGrupo);

            if (IdEmpresa != Constantes.ValorCero || IdAplicacion != Constantes.ValorCero || IdModulo != Constantes.ValorCero || NombrePagina != Constantes.ValorDefecto)
            {
                int iLog = 0;
                String DescripcionAuditoria = Constantes.ValorDefecto;
                DescripcionAuditoria = "IdEmpresa: " + IdEmpresa.ToString() + " |IdAplicacion: " + IdAplicacion.ToString()
                                        + " |IdModulo: " + IdModulo.ToString() + " |NombrePagina: " + NombrePagina.ToString();

                int idAccion = Constantes.BuscarPagina;
                int IdUsuario2 = (int)Session["IdUsuario"];

                iLog = ServicioUsuarioController.Instancia.InsertarLog(idAccion, IdUsuario2, DescripcionAuditoria);
            }      

            return PartialView(ParametrosPartialView.Consultar_Pagina_Configuracion_Grilla, VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult Consultar_PaginaAccion(
            Int32 IdPagina = Constantes.ValorCero, 
            String IdsGrupo = Constantes.ValorDefecto,
            String sortdir = Constantes.ValorDefecto,
            String sort = Constantes.ValorDefecto,
            Int32 page = Constantes.FirstPage)
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.PaginaAccionPaginacion = new PaginaAccionPaginacion();
            VM.PaginaAccionPaginacion.Paginacion = new Paginacion();
            VM.PaginaAccionPaginacion.Paginacion.Page = page;
            VM.PaginaAccionPaginacion.Paginacion.RowsPerPage = Constantes.RowsPerPage;
            VM.PaginaAccionPaginacion.Paginacion.SortDir = sortdir;
            VM.PaginaAccionPaginacion.Paginacion.SortType = sort;
            VM.PaginaAccionPaginacion = ServicioConfiguracionController.Instancia.ListarAccionPaginacion(new PaginaAccion() 
            { 
                Pagina = new Pagina() 
                { 
                    IdPagina = IdPagina, 
                    Grupo = new Grupo() 
                    { 
                        IdsGrupo = IdsGrupo
                    } 
                } 
            }, VM.PaginaAccionPaginacion.Paginacion);
            VM.PaginaAccionPaginacion.Paginacion.FooterDesc = UtilWebGrid.ContadorRegistrosWebGrid(VM.PaginaAccionPaginacion.Paginacion.Page, VM.PaginaAccionPaginacion.Paginacion.RowsPerPage, VM.PaginaAccionPaginacion.Paginacion.RowCount);

            return PartialView(ParametrosPartialView.Consultar_Accion_Configuracion_Grilla, VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult Editar_Pagina(Int32 IdPagina = Constantes.ValorCero, Int32 IdPaginaPadre = Constantes.ValorCatorce)
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.Pagina = ServicioConfiguracionController.Instancia.BuscarPagina(new Pagina() { IdPagina = IdPagina });
            //VM.ListaModulo = ServicioConfiguracionController.Instancia.ListarModulo();

            VM.PaginaAccionPaginacion = new PaginaAccionPaginacion();
            VM.PaginaAccionPaginacion.Paginacion = new Paginacion();
            VM.PaginaAccionPaginacion.ListaPaginaAccion = new ListaPaginaAccion();
            VM.ListaAgrupacion = new ListaAgrupacion();


            VM.ListaEmpresa = ServiceController.ServicioConfiguracionController.Instancia.ListarEmpresa();
            VM.ListaAplicacion = ServicioConfiguracionController.Instancia.AplicacionPorEmpresa(VM.Pagina.Modulo.Aplicacion);
            VM.ListaModulo = ServicioConfiguracionController.Instancia.ModuloPorAplicacion(VM.Pagina.Modulo); 
            VM.ListaGrupo = ServicioConfiguracionController.Instancia.GruposPorPagina(VM.Pagina);
            VM.ListaPagina = ServicioConfiguracionController.Instancia.ListarPagina();
            VM.ListaAgrupacion = ServicioConfiguracionController.Instancia.getListAgrupacionModulo(VM.Pagina.Modulo.IdModulo);
            VM.Agrupacion = new DataContracts.Agrupacion();
            VM.Agrupacion.IdAgrupacion = VM.Pagina.IdAgrupacion;
            VM.IdTamanoMenu = VM.Pagina.IdTamanoMenu;

            VM.Empresa = new Empresa();
            VM.Modulo = new Modulo();
            VM.Grupo = new Grupo() { };
            VM.Aplicacion = new Aplicacion();
            VM.Modulo.IdModulo = VM.Pagina.Modulo.IdModulo;
            VM.ListaPaginaAccionPermiso = ServicioPerfilController.Instancia.ObtenerPermisoPerfilPorGrupo(UtilSession.Obtener_UsuarioSession().IdUsuario, Constantes.EditarPagina);

            if (IdPaginaPadre > 0)
                return PartialView(ParametrosPartialView.Gestionar_Pagina_ConfiguracionPV, VM);
            else
                return PartialView(ParametrosPartialView.PGP_PaginaPadreEditPartialView, VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult PaginaTipo()
        {
            return PartialView(ParametrosPartialView.PG_PaginaTipoPartialView);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult Registrar_Pagina()
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.Pagina = new Pagina();

            VM.ListaEmpresa = ServiceController.ServicioConfiguracionController.Instancia.ListarEmpresa();
            VM.ListaAplicacion = new ListaAplicacion();
            VM.ListaModulo = new ListaModulo();//ServicioConfiguracionController.Instancia.ListarModulo();
            VM.ListaPagina = new ListaPagina();
            VM.ListaAgrupacion = new ListaAgrupacion();

            VM.PaginaAccionPaginacion = new PaginaAccionPaginacion();
            VM.PaginaAccionPaginacion.ListaPaginaAccion = new ListaPaginaAccion();
            VM.ListaGrupo = ServicioConfiguracionController.Instancia.ListarGrupo();
            VM.Pagina.Visible = true;
            VM.ListaPaginaAccionPermiso = ServicioPerfilController.Instancia.ObtenerPermisoPerfilPorGrupo(UtilSession.Obtener_UsuarioSession().IdUsuario, Constantes.NuevaPagina);
            VM.ListaAgrupacion = ServicioConfiguracionController.Instancia.getListAgrupacionModulo(0);

            return PartialView(ParametrosPartialView.Gestionar_Pagina_ConfiguracionPV, VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult Registrar_PaginaPadre()
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.Pagina = new Pagina();

            VM.ListaEmpresa = ServiceController.ServicioConfiguracionController.Instancia.ListarEmpresa();
            VM.ListaAplicacion = new ListaAplicacion();
            VM.ListaModulo = new ListaModulo();//ServicioConfiguracionController.Instancia.ListarModulo();
            VM.ListaPagina = new ListaPagina();

            VM.PaginaAccionPaginacion = new PaginaAccionPaginacion();
            VM.PaginaAccionPaginacion.ListaPaginaAccion = new ListaPaginaAccion();
            
            VM.Pagina.Visible = true;
            VM.ListaPaginaAccionPermiso = ServicioPerfilController.Instancia.ObtenerPermisoPerfilPorGrupo(UtilSession.Obtener_UsuarioSession().IdUsuario, Constantes.NuevaPagina);


            return PartialView(ParametrosPartialView.PG_PaginaPadrePartialView, VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult Insertar_Pagina(String cadPagina = Constantes.ValorDefecto, 
                                            String cadlistapaginaaccion = Constantes.ValorDefecto,
                                            int vIdAgrupacion = Constantes.ValorCero,
                                            int vIdTamanoMenu = Constantes.ValorUno)
        {
            Pagina pagina = JsonConvert.DeserializeObject<Pagina>(cadPagina);
            pagina.IdAgrupacion = vIdAgrupacion;
            pagina.IdTamanoMenu = vIdTamanoMenu;

            ListaPaginaAccion listapaginaaccion;
            if (cadlistapaginaaccion != string.Empty)
                listapaginaaccion = JsonConvert.DeserializeObject<ListaPaginaAccion>(cadlistapaginaaccion);
            else
                listapaginaaccion = null;

            int result = ServicioConfiguracionController.Instancia.Insertar_Pagina(pagina, listapaginaaccion);
            var mensaje = string.Empty;

            switch (result)
            {
                case -1:
                    mensaje = mensaje = String.Format("La Pagina: {0} ya esta asociada al módulo {1}", pagina.Nombre, "{1}");
                    break;
                case 0:
                    mensaje = Validation.Security.Security.Registro_Incorrecto;
                    break;
                case 1:
                    mensaje = Validation.Security.Security.Registro_Correcto;
                    break;
            }

            if (result == 1)
            {
                int iLog = 0;
                String DescripcionAuditoria = Constantes.ValorDefecto;
                DescripcionAuditoria = "cadPagina: " + cadPagina.ToString() + " |cadlistapaginaaccion: " + cadlistapaginaaccion.ToString();

                int idAccion = Constantes.InsertarPagina;
                int IdUsuario2 = (int)Session["IdUsuario"];

                iLog = ServicioUsuarioController.Instancia.InsertarLog(idAccion, IdUsuario2, DescripcionAuditoria);
            }      

            var jsonMensaje = new { mensaje = mensaje , identificador = result};
            return Json(jsonMensaje, JsonRequestBehavior.AllowGet);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult Actualizar_Pagina(String cadPagina = Constantes.ValorDefecto, 
                                              String cadlistapaginaaccion = Constantes.ValorDefecto,
                                              int vIdAgrupacion = Constantes.ValorCero,
                                              int vIdTamanoMenu = Constantes.ValorUno)
        {
            Pagina pagina = JsonConvert.DeserializeObject<Pagina>(cadPagina);
            pagina.IdAgrupacion = vIdAgrupacion;
            pagina.IdTamanoMenu = vIdTamanoMenu;

            ListaPaginaAccion listapaginaaccion;
            if (cadlistapaginaaccion != string.Empty)
                listapaginaaccion = JsonConvert.DeserializeObject<ListaPaginaAccion>(cadlistapaginaaccion);
            else
                listapaginaaccion = null;

            int result = ServicioConfiguracionController.Instancia.Actualizar_Pagina(pagina, listapaginaaccion);
            var mensaje = string.Empty;

            switch (result)
            {
                case 0:
                    mensaje = Validation.Security.Security.Actualizacion_Incorrecta;
                    break;
                case 1:
                    mensaje = Validation.Security.Security.Actualizacion_Correcta;
                    break;
            }

            if (result == 1)
            {
                int iLog = 0;
                String DescripcionAuditoria = Constantes.ValorDefecto;
                DescripcionAuditoria = "cadPagina: " + cadPagina.ToString() + " |cadlistapaginaaccion: " + cadlistapaginaaccion.ToString();

                int idAccion = Constantes.Modificar_Pagina;
                int IdUsuario2 = (int)Session["IdUsuario"];

                iLog = ServicioUsuarioController.Instancia.InsertarLog(idAccion, IdUsuario2, DescripcionAuditoria);
            }      

            var jsonMensaje = new { mensaje = mensaje };
            return Json(jsonMensaje, JsonRequestBehavior.AllowGet);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult ActualizarEstado_Pagina(Int32 IdPagina = Constantes.ValorCero, Int32 IdEstado = Constantes.ValorCero)
        {
            int result = ServicioConfiguracionController.Instancia.ActualizarEstado_Pagina(new Pagina() { IdPagina = IdPagina, Estado = new Estado() { IdEstado = IdEstado } });
            var mensaje = string.Empty;

            IdEstado = (result == 0 ? IdEstado * -1 : IdEstado);

            switch (IdEstado)
            {
                case -3:
                    mensaje = Validation.Security.Security.Eliminar_Incorrecto;
                    break;
                case -2:
                    mensaje = Validation.Security.Security.Desactivar_Incorrecto;
                    break;
                case -1:
                    mensaje = Validation.Security.Security.Activar_Incorrecto;
                    break;
                case 1:
                    mensaje = Validation.Security.Security.Activar_Correcto;
                    break;
                case 2:
                    mensaje = Validation.Security.Security.Desactivar_Correcto;
                    break;
                case 3:
                    mensaje = Validation.Security.Security.Eliminar_Correcto;
                    break;
            }

            var jsonMensaje = new { mensaje = mensaje };
            return Json(jsonMensaje, JsonRequestBehavior.AllowGet);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult P_AplicacionPorEmpresa(Int32 IdEmpresa = Constantes.ValorCero, Int32 Type = Constantes.ValorCero)
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.ListaAplicacion = ServicioConfiguracionController.Instancia.AplicacionPorEmpresa(new Aplicacion() { Empresa = new Empresa() { IdEmpresa = IdEmpresa } });

            if (Type == 0)
                return PartialView(ParametrosPartialView.P_AplicacionPV, VM);
            else
                return PartialView(ParametrosPartialView.PG_AplicacionPV, VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult P_ModuloPorAplicacion(Int32 IdAplicacion = Constantes.ValorCero, Int32 Type = Constantes.ValorCero)
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.ListaModulo = ServicioConfiguracionController.Instancia.ModuloPorAplicacion(new Modulo() { Aplicacion = new Aplicacion() { IdAplicacion = IdAplicacion } });

            if (Type == 0)
                return PartialView(ParametrosPartialView.P_ModuloPV, VM);
            else
                return PartialView(ParametrosPartialView.PG_ModuloPV, VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult P_AgrupacionPorModulo(Int32 IdModulo = Constantes.ValorCero)
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.ListaAgrupacion = ServicioConfiguracionController.Instancia.getListAgrupacionModulo(IdModulo);

            return PartialView(ParametrosPartialView.PG_AgrupacionPV, VM);
        }
        [RequiresAuthenticationAttribute]
        public ActionResult P_PaginaPadrePorModulo(Int32 IdModulo = Constantes.ValorCero, Int32 Type = Constantes.ValorCero)
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.ListaPagina = ServicioConfiguracionController.Instancia.PaginaPadreListar(new Pagina() { Modulo = new Modulo() { IdModulo = IdModulo } });

            return PartialView(ParametrosPartialView.PG_PaginaPadrePV, VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult P_Empresa(Int32 IdEmpresa = Constantes.ValorCero)
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.ListaEmpresa = ServicioConfiguracionController.Instancia.ListarEmpresa();
            VM.Empresa = new Empresa();
            VM.Empresa.IdEmpresa = IdEmpresa;

            return PartialView(ParametrosPartialView.P_EmpresaPV, VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult P_Aplicacion(Int32 IdAplicacion = Constantes.ValorCero, Int32 IdEmpresa = Constantes.ValorCero)
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.ListaAplicacion = ServicioConfiguracionController.Instancia.AplicacionPorEmpresa(new Aplicacion() { Empresa = new Empresa() { IdEmpresa = IdEmpresa } });//ServicioConfiguracionController.Instancia.ListarAplicacion();
            VM.Aplicacion = new Aplicacion();
            VM.Aplicacion.IdAplicacion = IdAplicacion;

            return PartialView(ParametrosPartialView.P_AplicacionPV, VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult P_Modulo(Int32 IdModulo = Constantes.ValorCero, Int32 IdAplicacion = Constantes.ValorCero)
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.ListaModulo = ServicioConfiguracionController.Instancia.ModuloPorAplicacion(new Modulo() { Aplicacion = new Aplicacion() { IdAplicacion = IdAplicacion } });
            VM.Modulo = new Modulo();
            VM.Modulo.IdModulo = IdModulo;

            return PartialView(ParametrosPartialView.P_ModuloPV, VM);
        }


        [RequiresAuthenticationAttribute]
        public ActionResult P_AplicacionPorEmpresaPadre(Int32 IdEmpresa = Constantes.ValorCero, Int32 Type = Constantes.ValorCero)
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.ListaAplicacion = ServicioConfiguracionController.Instancia.AplicacionPorEmpresa(new Aplicacion() { Empresa = new Empresa() { IdEmpresa = IdEmpresa } });

            if (Type == 0)
                return PartialView(ParametrosPartialView.PGP_AplicacionPartialView, VM);
            else
                return PartialView(ParametrosPartialView.PGP_AplicacionEditPartialView, VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult P_ModuloPorAplicacionPadre(Int32 IdAplicacion = Constantes.ValorCero, Int32 Type = Constantes.ValorCero)
        {
            ConfiguracionViewModel VM = new ConfiguracionViewModel();
            VM.ListaModulo = ServicioConfiguracionController.Instancia.ModuloPorAplicacion(new Modulo() { Aplicacion = new Aplicacion() { IdAplicacion = IdAplicacion } });

            if (Type == 0)
                return PartialView(ParametrosPartialView.PGP_ModuloPartialView, VM);
            else
                return PartialView(ParametrosPartialView.PGP_ModuloEditPartialView, VM);
        }

        #endregion
    }
}