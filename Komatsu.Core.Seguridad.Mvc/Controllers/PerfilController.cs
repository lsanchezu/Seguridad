using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Komatsu.Core.Seguridad.Mvc.Models;
using Komatsu.Core.Seguridad.Mvc.Models.ViewModel;
using Komatsu.Core.Seguridad.Mvc.ServiceController;
using Komatsu.Core.Seguridad.ServicioPerfil;
using Komatsu.Core.Seguridad.Mvc.Helper;
using Komatsu.Core.Seguridad.Common;
using Komatsu.Core.Seguridad.Provider.Filters;

namespace Komatsu.Core.Seguridad.Mvc.Controllers
{
    [NoCacheAttribute]
    public class PerfilController : Controller
    {
        [RequiresAuthenticationAttribute]
        [HttpGet]
        public ActionResult CrearPerfil()
        {
            PerfilViewModel oPerfilViewModel = new PerfilViewModel();

            oPerfilViewModel.ListaRol = new ServicioRol.ListaRol();
            oPerfilViewModel.ListaUsuario = ServicioUsuarioController.Instancia.ListarUsuario();
            oPerfilViewModel.ListaPaginaAccion = ServicioPerfilController.Instancia.ObtenerPermisoPerfilPorGrupo(UtilSession.Obtener_UsuarioSession().IdUsuario, Constantes.CrearPerfil);
            oPerfilViewModel.ListaEmpresa = ServicioConfiguracionController.Instancia.ListarEmpresa();
            oPerfilViewModel.ListaAplicacion = new ServicioConfiguracion.ListaAplicacion();
            var lista = Session["UsuariosAsignados"];
            return View(oPerfilViewModel);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult ObtenerUsuarios_Partial(int IdRol)
        {
            PerfilViewModel oPerfilViewModel = new PerfilViewModel();

            oPerfilViewModel.ListaUsuariosAsignados = ServicioUsuarioController.Instancia.ListarUsuarioAsignadosPorRol(IdRol);
            Session["UsuariosAsignados"] = oPerfilViewModel.ListaUsuariosAsignados;

            oPerfilViewModel.ListaUsuariosNoAsignados = ServicioUsuarioController.Instancia.ListarUsuarioNoAsignadosPorRol(IdRol);
            

            return PartialView(oPerfilViewModel);
        }
        
        [RequiresAuthenticationAttribute]
        [HttpPost]
        public ActionResult CrearPerfil(int idRol, string IdUsuarios)
        {
            ListaUsuarioRol oListaUsuarioRol = ObtenerListaUsuarioRol(idRol, IdUsuarios);

            PerfilTransaccion oPerfilTransaccion = new PerfilTransaccion();
            oPerfilTransaccion.ListaUsuarioRol = oListaUsuarioRol;
            oPerfilTransaccion.IdRol = idRol;
            oPerfilTransaccion.Auditoria = SetearDatosAuditoria(Constantes.UsuarioRol, Constantes.AUDITORIA_TIPOACCION_REGISTRAR);
            oPerfilTransaccion = ServicioPerfilController.Instancia.CrearPerfil(oPerfilTransaccion);
            
            return Content(oPerfilTransaccion.Respuesta);
        }

        public ListaUsuarioRol ObtenerListaUsuarioRol(int idRol, string IdUsuarios)
        {
            ListaUsuarioRol oListaUsuarioRol = new ListaUsuarioRol();
            UsuarioRol oUsuarioRol;
            IdUsuarios.Split(',').ToList().ForEach(usuario =>
            {
                oUsuarioRol = new UsuarioRol();
                oUsuarioRol.IdRol = idRol;
                oUsuarioRol.IdUsuario = (string.IsNullOrEmpty(usuario) ? 0 : Convert.ToInt32(usuario));
                oListaUsuarioRol.Add(oUsuarioRol);
            });
            return oListaUsuarioRol;
        }
        
        [RequiresAuthenticationAttribute]
        [HttpGet]
        public ActionResult AsignarPerfil()
        {
            PerfilViewModel oPerfilViewModel = new PerfilViewModel();
            oPerfilViewModel.ListaRol = ServicioRolController.Instancia.ListarRol();
            oPerfilViewModel.ListaPaginaAccion = ServicioPerfilController.Instancia.ObtenerPermisoPerfilPorGrupo(UtilSession.Obtener_UsuarioSession().IdUsuario, Constantes.CrearAsignarPerfil);
            oPerfilViewModel.ListaEmpresa = ServicioConfiguracionController.Instancia.ListarEmpresa();
            oPerfilViewModel.ListaAplicacion = new ServicioConfiguracion.ListaAplicacion();

            return View(oPerfilViewModel);
        }

        [RequiresAuthenticationAttribute]
        [HttpPost]
        public ActionResult AsignarPerfil(ListaPermisoPerfil listaPermisoPerfil)
        {
            if (listaPermisoPerfil != null && listaPermisoPerfil.Count > 0)
            {
                if(listaPermisoPerfil[0].IdRol == 0)
                    return Content("Debe seleccionar algún rol");
                else 
                {
                    int iLog = 0;
                    String DescripcionAuditoria = Constantes.ValorDefecto;
                    DescripcionAuditoria = "ListaUsuarios: " + listaPermisoPerfil.ToString();

                    int idAccion = Constantes.AsignarRol;
                    int IdUsuario2 = (int)Session["IdUsuario"];

                    iLog = ServicioUsuarioController.Instancia.InsertarLog(idAccion, IdUsuario2, DescripcionAuditoria);

                    if(ServicioPerfilController.Instancia.AsignarPerfil(listaPermisoPerfil) > 0)
                        return Content(Validation.Security.Security.Asignacion_Correcta);
                    else
                        return Content(Validation.Security.Security.Asignacion_Incorrecta);
                }
                
            }else
                return Content(Validation.Security.Security.Asignacion_Seleccione);
            
        }

        [RequiresAuthenticationAttribute]
        public JsonResult TreeViewJson(Int32 IdRol = Constantes.ValorCero, Int32 IdEmpresa = Constantes.ValorCero)
        {
            PerfilViewModel VM = new PerfilViewModel();
            return Json(ObtenerTreeViewJson(IdRol, IdEmpresa), JsonRequestBehavior.AllowGet);
        }

        [RequiresAuthenticationAttribute]
        private object[] ObtenerTreeViewJson(Int32 IdRol, Int32 IdEmpresa)
        {
            var ListaTreeview = ServicioPerfilController.Instancia.ListarTreeview(IdRol, IdEmpresa);
            var ListaTreeViewParent = ListaTreeview.Where(m => m.Nivel == 2).ToList();


            object[] listobject = new object[ListaTreeViewParent.Count()];

            for (int i = 0; i < ListaTreeViewParent.Count(); i++)
            {
                bool isFather = true;

                listobject.SetValue(new
                {
                    @id = ListaTreeViewParent[i].Id,
                    @label = ListaTreeViewParent[i].Nombre,
                    @branch = ObtenerTreeViewChild(ListaTreeview, ListaTreeViewParent[i], out isFather),
                    @inode = isFather,//(ListaTreeview[i].),
                    @open = false,
                    @checked = ListaTreeViewParent[i].Check,
                    @radio = true
                }, i);
            }

            return listobject;

            #region MyRegion
            //return new object[]
            //            {
            //            new {
            //                    id = "1",
            //                    label = "Aplicación 1",
            //                    inode = true,
            //                    open = true,
            //                    checkbox = true,
            //                    radio = false,
            //                    branch = new object[]
            //                    {
            //                        new {
            //                                id = "1",
            //                                label = "Módulo 1",
            //                                inode = false,
            //                                open = true,
            //                                checkbox = true,
            //                                radio = false,
            //                                branch = new object[]
            //                                {
            //                                    new {
            //                                        id = "1",
            //                                        label = "Página 1",
            //                                        inode = false,
            //                                        open = true,
            //                                        checkbox = true,
            //                                        radio = false
            //                                    },
            //                                    new {
            //                                        id = "2",
            //                                        label = "Página 2",
            //                                        inode = false,
            //                                        open = true,
            //                                        checkbox = true,
            //                                        radio = false
            //                                    },
            //                                    new {
            //                                        id = "3",
            //                                        label = "Página 3",
            //                                        inode = false,
            //                                        open = true,
            //                                        checkbox = true,
            //                                        radio = false
            //                                    }
            //                                }
            //                            },
            //                        new {
            //                            id = "2",
            //                            label = "Módulo 2",
            //                            inode = false,
            //                            open = true,
            //                            checkbox = true,
            //                            radio = false,
            //                                branch = new object[]
            //                                {
            //                                    new {
            //                                        id = "1",
            //                                        label = "Página 1",
            //                                        inode = false,
            //                                        open = true,
            //                                        checkbox = true,
            //                                        radio = false
            //                                    },
            //                                    new {
            //                                        id = "2",
            //                                        label = "Página 2",
            //                                        inode = false,
            //                                        open = true,
            //                                        checkbox = true,
            //                                        radio = false
            //                                    },
            //                                    new {
            //                                        id = "3",
            //                                        label = "Página 3",
            //                                        inode = false,
            //                                        open = true,
            //                                        checkbox = true,
            //                                        radio = false
            //                                    }
            //                                }
            //                        }
            //                    }
            //                },
            //                new {
            //                    id = "2",
            //                    label = "Aplicación 2",
            //                    inode = true,
            //                    open = true,
            //                    checkbox = true,
            //                    radio = false,
            //                    branch = new object[]
            //                    {
            //                        new {
            //                                id = "1",
            //                                label = "Módulo 1",
            //                                inode = false,
            //                                open = true,
            //                                checkbox = true,
            //                                radio = false
            //                            },
            //                        new {
            //                            id = "2",
            //                            label = "Módulo 2",
            //                            inode = false,
            //                            open = true,
            //                            checkbox = true,
            //                            radio = false,
            //                                branch = new object[]
            //                                {
            //                                    new {
            //                                        id = "1",
            //                                        label = "Página 1",
            //                                        inode = false,
            //                                        open = true,
            //                                        checkbox = true,
            //                                        radio = false
            //                                    },
            //                                    new {
            //                                        id = "2",
            //                                        label = "Página 2",
            //                                        inode = false,
            //                                        open = true,
            //                                        checkbox = true,
            //                                        radio = false
            //                                    },
            //                                    new {
            //                                        id = "3",
            //                                        label = "Página 3",
            //                                        inode = false,
            //                                        open = true,
            //                                        checkbox = true,
            //                                        radio = false
            //                                    }
            //                                }
            //                        }
            //                    }
            //                }
            //                ,
            //                new {
            //                    id = "3",
            //                    label = "Aplicación 3",
            //                    inode = true,
            //                    open = true,
            //                    checkbox = true,
            //                    radio = false,
            //                    branch = new object[]
            //                    {
            //                        new {
            //                                id = "2",
            //                                label = "Módulo 1",
            //                                inode = false,
            //                                open = true,
            //                                checkbox = true,
            //                                radio = false,
            //                                branch = new object[]
            //                                {
            //                                    new {
            //                                        id = "1",
            //                                        label = "Página 1",
            //                                        inode = false,
            //                                        open = true,
            //                                        checkbox = true,
            //                                        radio = false
            //                                    },
            //                                    new {
            //                                        id = "2",
            //                                        label = "Página 2",
            //                                        inode = false,
            //                                        open = true,
            //                                        checkbox = true,
            //                                        radio = false
            //                                    },
            //                                    new {
            //                                        id = "3",
            //                                        label = "Página 3",
            //                                        inode = false,
            //                                        open = true,
            //                                        checkbox = true,
            //                                        radio = false
            //                                    }
            //                                }
            //                            }
            //                    }
            //                }

            //            }; 
            #endregion
        }

        [RequiresAuthenticationAttribute]
        private object[] ObtenerTreeViewChild(ListaTreeviewHierarchyObject ListaTreeView,TreeviewHierarchyObject Parent,out bool isFather)
        {
            List<TreeviewHierarchyObject> ListaHijosGen = ListaTreeView.Where(c => c.IdPadre == Convert.ToInt32(Parent.Id) && Parent.Nivel == (c.Nivel - 1)).ToList();
            isFather = ListaHijosGen.Count > 0;
            ListaTreeviewHierarchyObject ListaHijos = new ListaTreeviewHierarchyObject();
            bool esPapa = true;
            ListaHijosGen.ForEach(ListaHijos.Add);

            object[] NodosHijos = new object[ListaHijos.Count];

            for (int i = 0; i < ListaHijos.Count; i++)
            {
                object nodoHijo = new object();

                NodosHijos.SetValue(new
                {
                    @id = ListaHijos[i].Id,
                    @label = ListaHijos[i].Nombre,
                    @branch = ObtenerTreeViewChild(ListaTreeView, ListaHijos[i], out esPapa),
                    @inode = esPapa,//(ListaTreeview[i].),
                    @open = false,
                    @checked = ListaHijos[i].Check,
                    @radio = false

                }, i);
            }
            return NodosHijos;
        }

        [RequiresAuthenticationAttribute]
        private ListaTreeView ObtenerTreeView()
        {
            return new ListaTreeView 
            { 
                new TreeView
                { 
                    id = "1", 
                    label="Pagina 1",
                    inode ="true",
                    open = "true",
                    checkbox = "true",
                    radio = "false"
                },
                new TreeView
                { 
                    id = "2", 
                    label="Pagina 2",
                    inode ="true",
                    open = "true",
                    checkbox = "true",
                    radio = "false"
                }
            };
        }

        [RequiresAuthenticationAttribute]
        public ActionResult AplicacionPorEmpresa(Int32 IdEmpresa = Constantes.ValorCero)
        {
            PerfilViewModel oPerfilViewModel = new PerfilViewModel();
            oPerfilViewModel.ListaAplicacion = ServicioConfiguracionController.Instancia.AplicacionPorEmpresa(new ServicioConfiguracion.Aplicacion() { Empresa = new ServicioConfiguracion.Empresa() { IdEmpresa = IdEmpresa } });

            return PartialView(ParametrosPartialView.AplicacionPV, oPerfilViewModel);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult RolesPorAplicacion(Int32 IdAplicacion = Constantes.ValorCero)
        {
            PerfilViewModel VM = new PerfilViewModel();
            VM.ListaRol = ServicioRolController.Instancia.RolesPorAplicacion(new Komatsu.Core.Seguridad.ServicioRol.Aplicacion() { IdAplicacion = IdAplicacion });

            return PartialView(ParametrosPartialView.RolesPV, VM);
        }

        public ActionResult RolesPorAplicacion_CrearPerfil(Int32 IdAplicacion = Constantes.ValorCero)
        {
            PerfilViewModel VM = new PerfilViewModel();
            VM.ListaRol = ServicioRolController.Instancia.RolesPorAplicacion(new Komatsu.Core.Seguridad.ServicioRol.Aplicacion() { IdAplicacion = IdAplicacion });

            return PartialView(ParametrosPartialView.RolPV, VM);
        }

        [RequiresAuthenticationAttribute]
        public ActionResult CargarRol()
        {
            PerfilViewModel VM = new PerfilViewModel();
            VM.ListaRol = ServicioRolController.Instancia.ListarRol();

            return PartialView(ParametrosPartialView.RolPV, VM);
        }

        #region AUDITORIA

        public Auditoria SetearDatosAuditoria(int IdTablaReferencia, string idTipoAccion)
        {
            Auditoria oAuditoria = new Auditoria();

            AuditoriaModel oAuditoriaModel = BaseAuditoria.ObtenerData_Auditoria(IdTablaReferencia, idTipoAccion);
            oAuditoria.IdTablaReferencia = IdTablaReferencia;
            oAuditoria.IdUsuario = oAuditoriaModel.IdUsuario;
            oAuditoria.UserName = oAuditoriaModel.UserName;
            oAuditoria.HostName = oAuditoriaModel.HostName;
            oAuditoria.IP = oAuditoriaModel.IP;
            oAuditoria.FechaHora = DateTime.Now;
            oAuditoria.IdTipoAccion = idTipoAccion;
            return oAuditoria;
        }

        #endregion
    }
}
