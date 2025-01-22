using System;

namespace Komatsu.Core.Seguridad.Common
{
    public class Constantes
    {
        #region ================ CONSTANTES CADENA =================

        public const string ValorNull = null;
        public const string ValorDefecto = "";
        public const string ValorDefectoCero = "0";
        public const string ValorDefectoNegativo = "-1";
        public const string ValorDefectoUno = "1";

        #endregion

        #region ================= CONSTANTES FECHA =================

        public const string FechaDefecto = "10-10-1900";
        public const string HoraDefecto = "12:00";

        #endregion

        #region ================ VALORES NUMÉRICOS =================

        public const int ValorMenosUno = -1;
        public const int ValorCero = 0;
        public const int ValorUno = 1;
        public const int ValorDos = 2;
        public const int ValorTres = 3;
        public const int ValorCuatro = 4;
        public const int ValorCinco = 5;
        public const int ValorSeis = 6;
        public const int ValorSiete = 7;
        public const int ValorOcho = 8;
        public const int ValorNueve = 9;
        public const int ValorDiez = 10;
        public const int ValorOnce = 11;
        public const int ValorDoce = 12;
        public const int ValorTrece = 13;
        public const int ValorCatorce = 14;
        public const int ValorQuince = 15;

        #endregion

        #region ================ VALORES PAGINACIÓN ================

        public const string DefaultSortType = "";
        public const string SortDirASC = "ASC";
        public const string SortDirDESC = "DESC";
        public const int FirstPage = 1;
        public const int RowsPerPage = 10;
        public const int MaxRowsPerPage = Int32.MaxValue - 1;

        #endregion

        #region ================ VALORES DE APLICACIÓN ================
        public const int IdAplicacion = 1;
        public const string ConsultarUsuario = "1";
        public const string NuevoUsuario = "51";
        public const string ActualizarUsuario = "52";
        public const string GuardarPolitica = "53";
        public const string CambiarClave = "54";
        public const string ConsultarRol = "2";
        public const string NuevoRol = "3";
        public const string EditarRol = "4";
        public const string ModificarRol = "5";
        public const string ConsultarEmpresa = "6";
        public const string NuevaEmpresa = "7";
        public const string EditarEmpresa = "8";
        public const string ModificarEmpresa = "9";
        public const string ConsultarAplicacion = "10";
        public const string NuevaAplicacion = "11";
        public const string EditarAplicacion = "12";
        public const string ModificarAplicacion = "13";
        public const string ConsultarModulo = "14";
        public const string NuevoModulo = "15";
        public const string EditarModulo = "16";
        public const string ModificarModulo = "17";
        public const string ConsultarPagina = "18";
        public const string NuevaPagina = "19";
        public const string EditarPagina = "20";
        public const string ModificarPagina = "21";
        public const string ActDesRol = "22";
        public const string ActDesEmpresa = "23";
        public const string ActDesAplicacion = "24";
        public const string ActDesModulo = "25";
        public const string ActDesPagina = "26";
        public const string CrearAsignarPerfil = "27";
        public const string CrearPerfil = "28";

        #endregion

        #region ================ CONSTANTES ACCION =================

        public const int BuscarUsuario = 1;
        public const int InsertarUsuario = 111;
        public const int ModificarUsuario = 114;
        public const int BuscarEmpresa = 9;
        public const int InsertarEmpresa = 11;
        public const int Modificar_Empresa = 15;
        public const int BuscarAplicacion = 16; 
        public const int InsertarAplicacion = 18;
        public const int Modificar_Aplicacion = 22;
        public const int BuscarModulo = 23;
        public const int InsertarModulo = 25;
        public const int Modificar_Modulo = 29;
        public const int BuscarPagina = 30;
        public const int InsertarPagina = 32;
        public const int Modificar_Pagina = 36;
        public const int BuscarRol = 2;
        public const int InsertarRol = 4;
        public const int Modificar_Rol = 8;
        public const int AsignarRol = 42;
        #endregion

        #region ================ CONSTANTES TABLAS =================

        public const int Rol = 1;
        public const int Menu = 2;
        public const int Usuario = 3;
        public const int UsuarioRol = 4;
        public const int PerfilPermiso = 5;
        public const int Cargo = 6;
        public const int PerfilUbicacion = 7;
        public const int UsuarioCargo = 8;
        public const int SolicitudAcceso = 9;
        #endregion

        #region ======================== CONSTANTESS ID TIPO ACCION ======

        public const string AUDITORIA_TIPOACCION_REGISTRAR = "01";
        public const string AUDITORIA_TIPOACCION_MODIFICAR = "02";
        public const string AUDITORIA_TIPOACCION_ANULAR = "03";
        public const string AUDITORIA_TIPOACCION_ENVIAR = "04";
        public const string AUDITORIA_TIPOACCION_AUTENTICAR = "05";
        public const string AUDITORIA_TIPOACCION_ELIMINAR = "06";

        #endregion

        #region CONTRASEÑA POR DEFECTO

        public const string ContrasenaDefecto = "abc123";

        #endregion

        #region MENSAJES VALIDACIÓN

        public const string MensajeRegistroCorrecto = "/Se registró correctamente.";
        public const string MensajeRegistroInCorrecto = "/Ocurrió un error al realizar la operación.";

        #endregion
    }
}
