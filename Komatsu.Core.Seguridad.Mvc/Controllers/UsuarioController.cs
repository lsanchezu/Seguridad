using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Komatsu.Core.Seguridad.Mvc.Models.ViewModel;
using Komatsu.Core.Seguridad.Mvc.Helpers;
using Komatsu.Core.Seguridad.Common;
using Komatsu.Core.Seguridad.Mvc.ServiceController;
using Komatsu.Core.Seguridad.ServicioUsuario;
using Komatsu.Core.Seguridad.ServicioPerfil;
using Komatsu.Core.Seguridad.Mvc.Helper;
using Komatsu.Core.Seguridad.Provider.Filters;
using Komatsu.Core.Seguridad.Validation;
using System.Web.Security;
using Komatsu.Core.Seguridad.Provider.Helper;

namespace Komatsu.Core.Seguridad.Mvc.Controllers
{
    public class UsuarioController : Controller
    {
        [RequiresAuthenticationAttribute]
        public ActionResult Consultar()
        {
            UsuarioViewModel oUsuarioViewModel = new UsuarioViewModel();
            oUsuarioViewModel.UsuarioPaginacion = new UsuarioPaginacion();
            oUsuarioViewModel.UsuarioPaginacion.ListaUsuario = new ListaUsuario();
            oUsuarioViewModel.ListaUsuarioTipo = ServicioUsuarioController.Instancia.ListarUsuarioTipo();
            oUsuarioViewModel.ListaPaginaAccion = ServicioPerfilController.Instancia.ObtenerPermisoPerfilPorGrupo(UtilSession.Obtener_UsuarioSession().IdUsuario, Constantes.ConsultarUsuario);
            oUsuarioViewModel.ListaEmpresa = ServicioConfiguracionController.Instancia.ListarEmpresa();
            oUsuarioViewModel.ListaAplicacion = new ServicioUsuario.ListaAplicacion();
            oUsuarioViewModel.ListaRol = new ServicioUsuario.ListaRol();

            return View(oUsuarioViewModel);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult Consultar_Usuario(String NombreApellido = Constantes.ValorDefecto, String Usuario = Constantes.ValorDefecto, Int32 IdUsuarioTipo = Constantes.ValorCero,
            Int32 IdEmpresa = Constantes.ValorCero,
            Int32 IdAplicacion = Constantes.ValorCero,
            Int32 IdRol = Constantes.ValorCero,
            String sortdir = Constantes.ValorDefecto,
            String sort = Constantes.ValorDefecto,
            Int32 page = Constantes.FirstPage)
        {
            UsuarioViewModel oUsuarioViewModel = new UsuarioViewModel();
            oUsuarioViewModel.UsuarioPaginacion = new UsuarioPaginacion();
            oUsuarioViewModel.UsuarioPaginacion.Paginacion = new Paginacion();
            oUsuarioViewModel.UsuarioPaginacion.Paginacion.Page = page;
            oUsuarioViewModel.UsuarioPaginacion.Paginacion.RowsPerPage = Constantes.RowsPerPage;
            oUsuarioViewModel.UsuarioPaginacion.Paginacion.SortDir = sortdir;
            oUsuarioViewModel.UsuarioPaginacion.Paginacion.SortType = sort;

            int iLog = 0;
            String DescripcionAuditoria = Constantes.ValorDefecto;
            DescripcionAuditoria = "Nombre Apellido: " + NombreApellido.ToString() + " |Usuario: " + Usuario
                                    + " |Tipo Usuario: " + IdUsuarioTipo + " |Codigo Empresa: " + IdEmpresa
                                    + " |Aplicacion: " + IdAplicacion + " |Rol: " + IdRol;

            int idAccion = Constantes.BuscarUsuario;
            int IdUsuario = (int)Session["IdUsuario"];

            oUsuarioViewModel.UsuarioPaginacion = ServicioUsuarioController.Instancia.ListarUsuarioPaginacion(
                new Komatsu.Core.Seguridad.ServicioUsuario.Usuario() 
                { 
                    UserName = Usuario, NombreApellido = NombreApellido, UsuarioTipo = 
                    new Komatsu.Core.Seguridad.ServicioUsuario.UsuarioTipo() { 
                        IdUsuarioTipo = IdUsuarioTipo 
                    } 
                },IdEmpresa,IdAplicacion,IdRol ,oUsuarioViewModel.UsuarioPaginacion.Paginacion);

            if (NombreApellido != Constantes.ValorDefecto || Usuario != Constantes.ValorDefecto || IdUsuarioTipo != Constantes.ValorCero || IdEmpresa != Constantes.ValorCero || IdAplicacion != Constantes.ValorCero || IdRol != Constantes.ValorCero)
            {
                iLog = ServicioUsuarioController.Instancia.InsertarLog(idAccion, IdUsuario, DescripcionAuditoria);
            }

            oUsuarioViewModel.UsuarioPaginacion.Paginacion.FooterDesc = UtilWebGrid.ContadorRegistrosWebGrid(oUsuarioViewModel.UsuarioPaginacion.Paginacion.Page, oUsuarioViewModel.UsuarioPaginacion.Paginacion.RowsPerPage, oUsuarioViewModel.UsuarioPaginacion.Paginacion.RowCount);

            return PartialView(ParametrosPartialView.Consultar_Usuario_Grilla, oUsuarioViewModel);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult AplicacionPorEmpresa(Int32 IdEmpresa = Constantes.ValorCero)
        {
            UsuarioViewModel oUsuarioViewModel = new UsuarioViewModel();
            oUsuarioViewModel.ListaAplicacion = ServicioUsuarioController.Instancia.AplicacionPorEmpresa(new ServicioUsuario.Aplicacion() { Empresa = new ServicioUsuario.Empresa() { IdEmpresa = IdEmpresa } });
           
            return PartialView(ParametrosPartialView.U_AplicacionPV, oUsuarioViewModel);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult RolesPorAplicacion(Int32 IdAplicacion = Constantes.ValorCero)
        {
            UsuarioViewModel oUsuarioViewModel = new UsuarioViewModel();
            oUsuarioViewModel.ListaRol = ServicioUsuarioController.Instancia.RolesPorAplicacion(new Komatsu.Core.Seguridad.ServicioUsuario.Aplicacion() { IdAplicacion = IdAplicacion });
          
            return PartialView(ParametrosPartialView.U_RolesPV, oUsuarioViewModel);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult NuevoUsuario()
        {
            UsuarioViewModel oUsuarioViewModel = new UsuarioViewModel();
            oUsuarioViewModel.ListaEmpresa = ServicioConfiguracionController.Instancia.ListarEmpresa();
            oUsuarioViewModel.ListaUsuarioTipo = ServicioUsuarioController.Instancia.ListarUsuarioTipo();
            oUsuarioViewModel.ListaPaginaAccion = ServicioPerfilController.Instancia.ObtenerPermisoPerfilPorGrupo(UtilSession.Obtener_UsuarioSession().IdUsuario, Constantes.NuevoUsuario);
            oUsuarioViewModel.ListaPolitica = ServicioUsuarioController.Instancia.Listar_Politica();
            oUsuarioViewModel.ListaUnidadOrganica = ServicioConfiguracionController.Instancia.ListarUnidadOrganica();
            oUsuarioViewModel.ListaArea = ServicioConfiguracionController.Instancia.ListarArea(0);
            oUsuarioViewModel.ListaEmpresaRelacionada = new ListaEmpresa();
            return PartialView(ParametrosPartialView.GestionarUsuario, oUsuarioViewModel);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult GuardarUsuario(
            string IdUsuario,
            string CodEmp,
            string Nombres,
            int Tipo,
            string Empresa,
            string Sexo,
            string email,
            string dire,
            string dni,
            int ModoUsu,
            string Contrasena,
            int IdEstado,
            int IdEmpresaRelacionada
            )
        {
            string Mensaje = "";
            int resultado = ServicioUsuarioController.Instancia.Insertar_Usuario(new Komatsu.Core.Seguridad.ServicioUsuario.Usuario()
            {
                UserName = IdUsuario,
                CodigoEmp = string.Empty,
                NombreApellido = Nombres,
                UsuarioTipo =
                            new Komatsu.Core.Seguridad.ServicioUsuario.UsuarioTipo()
                            {
                                IdUsuarioTipo = Tipo
                            },
                Empresa =
                            new Komatsu.Core.Seguridad.ServicioUsuario.Empresa()
                            {
                                CodigoEmpresa = Empresa
                            },
                Sexo = Sexo,
                EmailCoorporativo = email,
                Direccion = dire,
                DNI = dni,
                Estado = new Komatsu.Core.Seguridad.ServicioUsuario.Estado() { IdEstado = IdEstado },
                IdCargo = Tipo,
                EmpresaRelacionada = new ServicioUsuario.Empresa { IdEmpresa = IdEmpresaRelacionada }
                //}, ModoUsu, Contrasena);
            }, ModoUsu, StringCipher.Encrypt(Contrasena));

            switch (resultado)
            {
                case 1:
                    Mensaje = Validation.Security.Security.Registro_Correcto;
                    break;
                case 2:
                    Mensaje = Validation.Security.Security.Usuario_duplicado;
                    break;
                case 3:
                    Mensaje = Validation.Security.Security.Nombre_duplicado;
                    break;
                case 4:
                    Mensaje = Validation.Security.Security.DNI_duplicado;
                    break;
                case -1:
                    Mensaje = Validation.Security.Security.Registro_Incorrecto;
                    break;
            }

            if (resultado == 1)
            {
                int iLog = 0;
                String DescripcionAuditoria = Constantes.ValorDefecto;
                DescripcionAuditoria = "IdUsuario: " + IdUsuario.ToString() + " |Codigo Empleado: " + CodEmp
                                        + " |Nombres: " + Nombres + " |Tipo Usuario: " + Tipo
                                        + " |Empresa: " + Empresa + " |Sexo: " + Sexo + " |email: " + email
                                        + " |direccion: " + dire + " |DNI: " + dni;

                int idAccion = Constantes.InsertarUsuario;
                int IdUsuario2 = (int)Session["IdUsuario"];

                iLog = ServicioUsuarioController.Instancia.InsertarLog(idAccion, IdUsuario2, DescripcionAuditoria);
            }

            Mensaje = resultado + "/" + Mensaje;
            return Content(Mensaje);

        }

        [RequiresAuthenticationAttribute]
        public ActionResult ActualizarEstado_Usuario(Int32 IdUsuario = Constantes.ValorCero,
            Int32 IdEstado = Constantes.ValorCero)
        {
            int result = ServicioUsuarioController.Instancia.ActualizarEstado_Usuario(new Komatsu.Core.Seguridad.ServicioUsuario.Usuario() { IdUsuario = IdUsuario, Estado = new Komatsu.Core.Seguridad.ServicioUsuario.Estado() { IdEstado = IdEstado } });
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
        public ActionResult Editar_Usuario(Int32 IdUsuario = Constantes.ValorCero)
        {
            UsuarioViewModel VM = new UsuarioViewModel();
            VM.ListaEmpresa = ServicioConfiguracionController.Instancia.ListarEmpresa();
            VM.ListaUsuarioTipo = ServicioUsuarioController.Instancia.ListarUsuarioTipo();
            VM.Usuario = ServicioUsuarioController.Instancia.Buscar_Usuario(new Komatsu.Core.Seguridad.ServicioUsuario.Usuario() { IdUsuario = IdUsuario });
            VM.ListaPaginaAccion = ServicioPerfilController.Instancia.ObtenerPermisoPerfilPorGrupo(UtilSession.Obtener_UsuarioSession().IdUsuario, Constantes.ActualizarUsuario);

            VM.IdUnidadOrganica = VM.Usuario.IdSociedad;
            VM.IdArea = VM.Usuario.IdAreaBU;

            VM.ListaUnidadOrganica = ServicioConfiguracionController.Instancia.ListarUnidadOrganica();
            VM.ListaArea = ServicioConfiguracionController.Instancia.ListarArea(VM.IdUnidadOrganica);

            return PartialView(ParametrosPartialView.Gestionar_Usuario_PV, VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult Ver_Roles_Usuario(Int32 IdUsuario)
        {
            UsuarioViewModel VM = new UsuarioViewModel();
            VM.ListaUsuariosxRol = ServicioUsuarioController.Instancia.ObtenerListaRolxUsuario(IdUsuario);
            return PartialView(ParametrosPartialView.VerRoles_Usuario, VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult Actualizar_Usuario(Int32 IdUsuario = Constantes.ValorCero, 
                                               String UserName = Constantes.ValorDefecto,
                                               String NombreApellido = Constantes.ValorDefecto,
                                               Int32 IdUsuarioTipo = Constantes.ValorCero,
                                               String CodigoEmpresa = Constantes.ValorDefecto,
                                               String EmailCoorporativo = Constantes.ValorDefecto,
                                               String Direccion = Constantes.ValorDefecto,
                                               String DNI = Constantes.ValorDefecto,
                                               string CodEmp = Constantes.ValorDefecto,
                                               bool FlagResetearContrasena = false,
                                               Int32 IdEstado = Constantes.ValorCero)
        {
            int result = ServicioUsuarioController.Instancia.Actualizar_Usuario(new Komatsu.Core.Seguridad.ServicioUsuario.Usuario()
            {
                IdUsuario = IdUsuario,
                UserName = UserName,
                NombreApellido = NombreApellido,
                UsuarioTipo = new Komatsu.Core.Seguridad.ServicioUsuario.UsuarioTipo()
                {
                    IdUsuarioTipo = IdUsuarioTipo
                },
                Empresa = new Komatsu.Core.Seguridad.ServicioUsuario.Empresa()
                {
                    CodigoEmpresa = CodigoEmpresa
                },
                EmailCoorporativo = EmailCoorporativo,
                Direccion = Direccion,
                DNI = DNI,
                Estado = new Komatsu.Core.Seguridad.ServicioUsuario.Estado() { IdEstado = IdEstado },
                CodigoEmp = string.Empty,
                CambiarPassword = FlagResetearContrasena,
                Password = StringCipher.Encrypt(Constantes.ContrasenaDefecto)
                
                /*IdSociedad = IdUnidadOrganica,
                SociedadDescripcionCorta = (IdUnidadOrganica == 0 ? string.Empty : UnidadOrganicaDescripcion),
                IdAreaBU = IdArea,
                AreaBUDescripcionCorta = (IdArea == 0 ? string.Empty : AreaDescripcion)*/
            });

            var mensaje = string.Empty;

            switch (result)
            {
                case 1:
                    mensaje = Validation.Security.Security.Actualizacion_Correcta;
                    break;
                case 2:
                    mensaje = Validation.Security.Security.Usuario_duplicado;
                    break;
                case 3:
                    mensaje = Validation.Security.Security.Nombre_duplicado;
                    break;
                case 4:
                    mensaje = Validation.Security.Security.DNI_duplicado;
                    break;
                case -1:
                    mensaje = Validation.Security.Security.Actualizacion_Incorrecta;
                    break;
            }

            if (result == 1)
            {
                int iLog = 0;
                String DescripcionAuditoria = Constantes.ValorDefecto;
                DescripcionAuditoria = "IdUsuario: " + IdUsuario.ToString() + " |Nombre de Usuario: " + UserName
                                        + " |NombreApellido: " + NombreApellido + " |Tipo Usuario: " + IdUsuarioTipo
                                        + " |Empresa: " + CodigoEmpresa + " |EmailCoorporativo: " + EmailCoorporativo
                                        + " |Direccion: " + Direccion + " |DNI: " + DNI;

                int idAccion = Constantes.ModificarUsuario;
                int IdUsuario2 = (int)Session["IdUsuario"];

                iLog = ServicioUsuarioController.Instancia.InsertarLog(idAccion, IdUsuario2, DescripcionAuditoria);
            }

            //var jsonMensaje = new { mensaje = mensaje, block = result < 1 ? true : false };
            var jsonMensaje = new { mensaje = mensaje, block = (result == 1) };
            return Json(jsonMensaje, JsonRequestBehavior.AllowGet);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult GetArea(int IdUnidadOrganica,int flag)
        {
            UsuarioViewModel oUsuarioViewModel = new UsuarioViewModel();
            oUsuarioViewModel.ListaArea = ServicioConfiguracionController.Instancia.ListarArea(IdUnidadOrganica);

            if (flag == 0)
                return PartialView(ParametrosPartialView.U_AreaPV, oUsuarioViewModel);
            else
                return PartialView(ParametrosPartialView.U_AreaPV_Edit, oUsuarioViewModel);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult ObtenerEmpresarelacionada(string tipoEmpresaRelacionada)
        {
            UsuarioViewModel oUsuarioViewModel = new UsuarioViewModel();
            oUsuarioViewModel.ListaEmpresaRelacionada = ServicioUsuarioController.Instancia.ObtenerEmpresaRelacionada(tipoEmpresaRelacionada);
            return PartialView(ParametrosPartialView.U_EmpresaRelacionadaPV, oUsuarioViewModel);
        }

        //[RequiresAuthenticationAttribute]
        public bool ValidExistContrasenaOperacion(string contrasenaoperacion)
        {
            return ServicioUsuarioController.Instancia.ExistCotrasenaOperacion(contrasenaoperacion);

            /*if (result)
                return Content("1");
            else
                return Content("0");*/
        }
        
        //public JsonResult Editar_Usuario(Int32 IdUsuario = Constantes.ValorCero)
        //{

        //    UsuarioViewModel VM = new UsuarioViewModel();
        //    VM.Usuario = ServicioUsuarioController.Instancia.Buscar_Usuario(new Komatsu.Core.Seguridad.ServicioUsuario.Usuario() { IdUsuario = IdUsuario });
        //    int Encontrado = VM.Usuario.IdUsuario == 0 ? Constantes.ValorCero : Constantes.ValorUno;

        //    Object jsonData;
        //    jsonData = new
        //    {
        //        IdUsuario = VM.Usuario.IdUsuario,
        //        UserName = VM.Usuario.UserName,
        //        NombreApellido = VM.Usuario.NombreApellido,
        //        UsuarioTipo = VM.Usuario.UsuarioTipo.Nombre,
        //        Empresa = VM.Usuario.Empresa.Nombre,
        //        EmailCoorporativo = VM.Usuario.EmailCoorporativo,
        //        Direccion = VM.Usuario.Direccion,
        //        DNI = VM.Usuario.DNI,
        //        Encontrado = Encontrado
        //    };
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}
    }
}