using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Komatsu.Core.Seguridad.Mvc.Models.ViewModel;
using Komatsu.Core.Seguridad.Mvc.Helpers;
using Komatsu.Core.Seguridad.Common;
using Komatsu.Core.Seguridad.Mvc.ServiceController;
using Komatsu.Core.Seguridad.ServicioRol;
using Komatsu.Core.Seguridad.Mvc.Helper;
using Komatsu.Core.Seguridad.Provider.Filters;
using Komatsu.Core.Seguridad.Validation;

namespace Komatsu.Core.Seguridad.Mvc.Controllers
{
    [NoCacheAttribute]
    public class RolController : Controller
    {
        [RequiresAuthenticationAttribute]
        public ActionResult Consultar()
        {
            RolViewModel VM = new RolViewModel();
            VM.RolPaginacion = new RolPaginacion();

                VM.RolPaginacion.Paginacion = new Paginacion();
                VM.RolPaginacion = new RolPaginacion();
                VM.RolPaginacion.ListaRol = new ListaRol();
                VM.ListaPaginaAccion = ServicioPerfilController.Instancia.ObtenerPermisoPerfilPorGrupo(UtilSession.Obtener_UsuarioSession().IdUsuario, Constantes.ConsultarRol);
                VM.ListaAplicacion = ServicioConfiguracionController.Instancia.ListarAplicacion();
                return View(VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult Consultar_Rol(Int32 IdAplicacion = Constantes.ValorCero, String NombreRol = Constantes.ValorDefecto,
            String sortdir = Constantes.ValorDefecto,
            String sort = Constantes.ValorDefecto,
            Int32 page = Constantes.FirstPage,
            Int32 sipartial = Constantes.ValorCero)
        {
            RolViewModel VM = new RolViewModel();
            VM.RolPaginacion = new RolPaginacion();

            VM.RolPaginacion.Paginacion = new Paginacion();
            VM.RolPaginacion.Paginacion.Page = page;
            VM.RolPaginacion.Paginacion.RowsPerPage = Constantes.RowsPerPage;
            VM.RolPaginacion.Paginacion.SortDir = sortdir;
            VM.RolPaginacion.Paginacion.SortType = sort;

            VM.RolPaginacion = ServicioRolController.Instancia.ListarRolPaginacion(new Rol() { Nombre = NombreRol, Aplicacion = new Aplicacion() { IdAplicacion = IdAplicacion } }, VM.RolPaginacion.Paginacion);

            if (IdAplicacion != Constantes.ValorCero || NombreRol != Constantes.ValorDefecto)
            {
                int iLog = 0;
                String DescripcionAuditoria = Constantes.ValorDefecto;
                DescripcionAuditoria = "IdAplicacion: " + IdAplicacion.ToString() + " |NombreRol: " + NombreRol.ToString();

                int idAccion = Constantes.BuscarRol;
                int IdUsuario2 = (int)Session["IdUsuario"];

                iLog = ServicioUsuarioController.Instancia.InsertarLog(idAccion, IdUsuario2, DescripcionAuditoria);
            }

            VM.RolPaginacion.Paginacion.FooterDesc = UtilWebGrid.ContadorRegistrosWebGrid(VM.RolPaginacion.Paginacion.Page, VM.RolPaginacion.Paginacion.RowsPerPage, VM.RolPaginacion.Paginacion.RowCount);
            string IdsGrupo = Constantes.ConsultarRol + ","  + Constantes.ModificarRol + "," + Constantes.ActDesRol + ",";
            VM.ListaPaginaAccion = ServicioPerfilController.Instancia.ObtenerPermisoPerfilPorGrupo(UtilSession.Obtener_UsuarioSession().IdUsuario, IdsGrupo);
            VM.ListaAplicacion = ServicioConfiguracionController.Instancia.ListarAplicacion();//asdadasd
            return PartialView(ParametrosPartialView.Consultar_Rol_Grilla, VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult Editar_Rol(Int32 IdRol = Constantes.ValorCero)
        {
            RolViewModel VM = new RolViewModel();
            VM.Rol = ServicioRolController.Instancia.Buscar_Rol(new Rol() { IdRol = IdRol } );
            VM.FechaIni = VM.Rol.FechaInicio.ToString("dd/MM/yyyy");
            VM.FechaFin = VM.Rol.FechaFin.ToString("dd/MM/yyyy");
            VM.ListaPaginaAccion = ServicioPerfilController.Instancia.ObtenerPermisoPerfilPorGrupo(UtilSession.Obtener_UsuarioSession().IdUsuario, Constantes.EditarRol);
            VM.ListaAplicacion = ServicioConfiguracionController.Instancia.ListarAplicacion();
            
            return PartialView(ParametrosPartialView.Gestionar_Rol_PV, VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult Registrar_Rol()
        {
            RolViewModel VM = new RolViewModel();
            VM.Rol = new Rol();
            VM.FechaIni = DateTime.Now.ToString("dd/MM/yyyy");
            VM.FechaFin = DateTime.Now.ToString("dd/MM/yyyy");
            VM.ListaAplicacion = ServicioConfiguracionController.Instancia.ListarAplicacion();
            VM.ListaPaginaAccion = ServicioPerfilController.Instancia.ObtenerPermisoPerfilPorGrupo(UtilSession.Obtener_UsuarioSession().IdUsuario, Constantes.NuevoRol);    

            return PartialView(ParametrosPartialView.Gestionar_Rol_PV, VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult Insertar_Rol(Int32 IdAplicacion = Constantes.ValorCero, Boolean SiSuperAdmi = false, String NombreRol = Constantes.ValorDefecto, Boolean SiRango = false, 
            String FechaInicio = Constantes.FechaDefecto, String FechaFin = Constantes.FechaDefecto)
        {
          int result = ServicioRolController.Instancia.Insertar_Rol(new Rol()
                {
                    Nombre = NombreRol,
                    SiRango = SiRango,
                    SiSuperAdmi = SiSuperAdmi,
                    FechaInicio = Convert.ToDateTime(FechaInicio),
                    FechaFin = Convert.ToDateTime(FechaFin),
                    Aplicacion = new Aplicacion() 
                    {
                        IdAplicacion = IdAplicacion
                    }
            });

            var mensaje = string.Empty;

            switch (result)
            {
                case -2:
                    mensaje = String.Format(Validation.Security.Security.Rol_AdmiEnAplicacion);
                    break;
                case -1:
                    mensaje = String.Format("El rol: {0} ya esta asociado a la applicación: {1}", NombreRol, "{1}");
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
                DescripcionAuditoria = "IdAplicacion: " + IdAplicacion.ToString() + " |SiSuperAdmi: " + SiSuperAdmi.ToString()
                                        + " |NombreRol: " + NombreRol.ToString() + " |SiRango: " + SiRango.ToString()
                                        + " |FechaInicio: " + FechaInicio.ToString() + " |FechaFin: " + FechaFin.ToString();

                int idAccion = Constantes.InsertarRol;
                int IdUsuario2 = (int)Session["IdUsuario"];

                iLog = ServicioUsuarioController.Instancia.InsertarLog(idAccion, IdUsuario2, DescripcionAuditoria);
            }

            var jsonMensaje = new { mensaje = mensaje, block = result < 1 ? true : false};
            return Json(jsonMensaje, JsonRequestBehavior.AllowGet);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult Actualizar_Rol(Int32 IdRol = Constantes.ValorCero, Int32 IdAplicacion = Constantes.ValorCero, Boolean SiSuperAdmi = false,
            String NombreRol = Constantes.ValorDefecto, Boolean SiRango = false, String FechaInicio = Constantes.FechaDefecto, String FechaFin = Constantes.FechaDefecto)
        {
            int result = ServicioRolController.Instancia.Actualizar_Rol(new Rol()
                {
                    IdRol = IdRol,
                    Nombre = NombreRol,
                    SiRango = SiRango,
                    FechaInicio = Convert.ToDateTime(FechaInicio),
                    FechaFin = Convert.ToDateTime(FechaFin),
                    SiSuperAdmi = SiSuperAdmi,
                    Aplicacion = new Aplicacion() 
                    {
                        IdAplicacion = IdAplicacion
                    }
            });

            var mensaje = string.Empty;

            switch (result)
            {
                case -2:
                    mensaje = String.Format(Validation.Security.Security.Rol_AdmiEnAplicacion);
                    break;
                case -1:
                    mensaje = String.Format(Validation.Security.Security.Registro_Repetido, NombreRol);
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
                DescripcionAuditoria = " |IdRol: " + IdRol.ToString() + " |IdAplicacion: " + IdAplicacion.ToString() + " |SiSuperAdmi: " + SiSuperAdmi.ToString()
                                        + " |NombreRol: " + NombreRol.ToString() + " |SiRango: " + SiRango.ToString()
                                        + " |FechaInicio: " + FechaInicio.ToString() + " |FechaFin: " + FechaFin.ToString();

                int idAccion = Constantes.Modificar_Rol;
                int IdUsuario2 = (int)Session["IdUsuario"];

                iLog = ServicioUsuarioController.Instancia.InsertarLog(idAccion, IdUsuario2, DescripcionAuditoria);
            }

            var jsonMensaje = new { mensaje = mensaje, block = result < 1 ? true : false };
            return Json(jsonMensaje, JsonRequestBehavior.AllowGet);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult ActualizarEstado_Rol(Int32 IdRol = Constantes.ValorCero,
            Int32 IdEstado = Constantes.ValorCero)
        {
            int result = ServicioRolController.Instancia.ActualizarEstado_Rol(new Rol() { IdRol = IdRol, Estado = new Estado() { IdEstado = IdEstado } });
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
    }
}

