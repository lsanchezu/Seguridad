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

namespace Komatsu.Core.Seguridad.Mvc.Controllers
{
    public class PoliticaController : Controller
    {
        //
        // GET: /Politica/

        public ActionResult ConfiguracionPolitica()
        {
            UsuarioViewModel oUsuarioViewModel = new UsuarioViewModel();
            oUsuarioViewModel.ListaPaginaAccion = ServicioPerfilController.Instancia.ObtenerPermisoPerfilPorGrupo(UtilSession.Obtener_UsuarioSession().IdUsuario, Constantes.GuardarPolitica);
            oUsuarioViewModel.ListaPolitica = ServicioUsuarioController.Instancia.Listar_Politica();
            return View(oUsuarioViewModel); 
        }

        [RequiresAuthenticationAttribute]
        public ActionResult GuardarPolitica(int Long, int Vigencia, int Diferencia, int NroMaximo, int DiasBloq, int Compleji, int CantContrHis)
        {
            string Mensaje = "";

            int resultado = ServicioUsuarioController.Instancia.GuardarPolitica(Long,Vigencia,Diferencia,NroMaximo,DiasBloq,Compleji,CantContrHis);

            switch (resultado)
            {
                case 1:
                    Mensaje = Validation.Security.Security.Registro_Correcto;
                    break;
                case -1:
                    Mensaje = Validation.Security.Security.Registro_Incorrecto;
                    break;
            }

            Mensaje = resultado + "/" + Mensaje;
            return Content(Mensaje);
        }
    }
}
