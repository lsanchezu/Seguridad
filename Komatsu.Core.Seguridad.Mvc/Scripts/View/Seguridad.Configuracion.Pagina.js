var PrimeraVezAccion = true;
var DataAccion = [];

String.format = function () {
    // The string containing the format items (e.g. "{0}")
    // will and always has to be the first argument.
    var theString = arguments[0];

    // start with the second argument (i = 1)
    for (var i = 1; i < arguments.length; i++) {
        // "gm" = RegEx options for Global search (more than one instance)
        // and for Multiline search
        var regEx = new RegExp("\\{" + (i - 1) + "\\}", "gm");
        theString = theString.replace(regEx, arguments[i]);
    }

    return theString;
}

function NuevaPaginaTipo() {
    var ParamUrl = $("#Url_Registrar_PaginaTipo").val();
    $.ajax({
        url: ParamUrl,
        data: {},
        type: "post",
        cache: false,
        success: function (data) {
            $("#dialogGestionarPaginaTipo").html(data);
            InicioJPopUpOpen('#dialogGestionarPaginaTipo');
        },
    });
}

//Transacciones
function NuevaPagina() {
    var ParamUrlHijo = $("#Url_Registrar_Pagina").val();
    var ParamUrlPadre = $("#Url_Registrar_PaginaPadre").val();

    var valtipo = $("input:radio[name='TipoPaginaId']:checked").val();

    $("#dialogGestionarPaginaTipo").dialog('close');

    if (valtipo == '1') {
        $.ajax({
            url: ParamUrlHijo,
            data: {},
            type: "post",
            cache: false,
            success: function (data) {
                $("#dialogGestionarPagina").html(data);
                InicioJPopUpOpen('#dialogGestionarPagina');
            }
        });
    } else {
        $.ajax({
            url: ParamUrlPadre,
            data: {},
            type: "post",
            cache: false,
            success: function (data) {
                $("#dialogGestionarPaginaPadre").html(data);
                InicioJPopUpOpen('#dialogGestionarPaginaPadre');
            }
        });
    }
}

function EditarPagina(IdPagina, IdPaginaPadre) {
    var ParamUrl = $("#Url_Editar_Pagina").val();

    $.ajax({
        url: ParamUrl,
        data: { IdPagina: IdPagina , IdPaginaPadre: IdPaginaPadre},
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#dialogGestionarPagina").html(data);
            InicioJPopUpOpen('#dialogGestionarPagina');

          
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
        }
    });
}

function ActivarDesactivarPagina(IdPagina, IdEstado, Mensaje) {
    $("#dialogConfirmacion p").text(Mensaje);
    $("#dialogConfirmacion").data('Opcion', 'Actualizar_EstadoPagina');
    $("#dialogConfirmacion").data('IdPagina', IdPagina);
    $("#dialogConfirmacion").data('IdEstado', IdEstado);
    $("#dialogConfirmacion").dialog('open');
}

function Insertar_Pagina() {
    //url
    var ParamUrl = $("#Url_Insertar_Pagina").val();
    
    var cadPagina = DataPagina();
    var cadlistapaginaaccion = DataCheckAccion();

    //server request
    $.ajax({
        url: ParamUrl,
        data: {
            cadPagina: JSON.stringify(cadPagina),
            cadlistapaginaaccion: JSON.stringify(cadlistapaginaaccion),
            vIdAgrupacion : $("#PG_IdAgrupacion").val(),
            vIdTamanoMenu: $("#iIdTamanoButton").val()     
        },
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#dialogResultado p").html(String.format(data.mensaje, "", $("#PG_IdModulo option:selected").text()));
            $("#dialogResultado").dialog('open');
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            BuscarPagina();
        }
    });
}

function Insertar_PaginaPadre() {
    //url
    var ParamUrl = $("#Url_Insertar_Pagina").val();

    var cadPagina = DataPaginaPadre();

    //server request
    $.ajax({
        url: ParamUrl,
        data: { cadPagina: JSON.stringify(cadPagina), cadlistapaginaaccion: "" },
        type: "post",
        cache: false,
        success: function (data) {
            $("#dialogResultado p").html(String.format(data.mensaje, "", $("#PG_IdModulo option:selected").text()));
            $("#dialogResultado").dialog('open');
        },
        complete: function () {
            BuscarPagina();
        }
    });
}

function Actualizar_Pagina() {
    //url
    var ParamUrl = $("#Url_Actualizar_Pagina").val();
    
    var cadPagina = DataPagina();
    var cadlistapaginaaccion = DataCheckAccion();

    //server request
    $.ajax({
        url: ParamUrl,
        data: {
            cadPagina: JSON.stringify(cadPagina),
            cadlistapaginaaccion: JSON.stringify(cadlistapaginaaccion),
            vIdAgrupacion: $("#PG_IdAgrupacion").val(),
            vIdTamanoMenu: $("#iIdTamanoButton").val()
        },
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#dialogResultado p").html(data.mensaje);
            $("#dialogResultado").dialog('open');
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            BuscarPagina();
        }
    });
}

function Actualizar_PaginaPadre() {
    //url
    var ParamUrl = $("#Url_Actualizar_Pagina").val();

    var cadPagina = DataPaginaPadreEdit();

    //server request
    $.ajax({
        url: ParamUrl,
        data: { cadPagina: JSON.stringify(cadPagina), cadlistapaginaaccion: "" },
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#dialogResultado p").html(data.mensaje);
            $("#dialogResultado").dialog('open');
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            BuscarPagina();
        }
    });
}

function ActualizarEstado_Pagina(IdPagina, IdEstado) {
    var ParamUrl = $("#Url_ActualizarEstado_Pagina").val();

    $.ajax({
        url: ParamUrl,
        data: { IdPagina: IdPagina, IdEstado: IdEstado },
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#dialogResultado p").html(data.mensaje);
            $("#dialogResultado").dialog('open');
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            BuscarPagina();
        }
    });
}

function ValidarPagina() {
    var valid = true;
    //appendErrorMessage("#errorPG_Url", "");
//    appendErrorMessage("#errorPG_Icono", "");
    appendErrorMessage("#errorPG_NombrePagina", "");
    appendErrorMessage("#errorPG_IdEmpresa", "");
    appendErrorMessage("#errorPG_IdAplicacion", "");
    appendErrorMessage("#errorPG_IdModulo", "");
    appendErrorMessage("#errorPG_IdGrupo", "");
    appendErrorMessage("#errorPG_IdOrden", "");
    
//    if ($("#PE_Url").val().trim() == '') {
//        appendErrorMessage("#errorPE_Url", "Ingrese Url", true);
//        valid = false;
//    } else
//        appendErrorMessage("#errorPE_Url", "");

//    if ($("#PG_Icono").val().trim() == '') {
//        appendErrorMessage("#errorPG_Icono", "Ingrese ícono", true);
//        valid = false;
//    } else
//        appendErrorMessage("#errorPG_Icono", "");



    if ($("#PG_NombrePagina").val() == 0) {
        appendErrorMessage("#errorPG_NombrePagina", "Ingrese nombre página", true);
        valid = false;
    } else
        appendErrorMessage("#errorPG_NombrePagina", "");


    if ($("#PG_IdEmpresa").val() == 0 || $("#PG_IdEmpresa").val() == "") {
        appendErrorMessage("#errorPG_IdEmpresa", "Seleccione Empresa", true);
        valid = false;
    } else
        appendErrorMessage("#errorPG_IdEmpresa", "");



    if ($("#PG_IdAplicacion").val() == 0) {
        appendErrorMessage("#errorPG_IdAplicacion", "Seleccione aplicación", true);
        valid = false;
    } else
        appendErrorMessage("#errorPG_IdAplicacion", "");

    if ($("#PG_IdModulo").val() == 0) {
        appendErrorMessage("#errorPG_IdModulo", "Seleccione módulo", true);
        valid = false;
    } else
        appendErrorMessage("#errorPG_IdModulo", "");

    if ($("#PG_IdGrupo").val() == null || $("#PG_IdGrupo").val() == '') {
        appendErrorMessage("#errorPG_IdGrupo", "Seleccione grupo", true);
        valid = false;
    } else
        appendErrorMessage("#errorPG_IdGrupo", "");
    
    if ($("#PG_IdOrden").val().trim() == '') {
        appendErrorMessage("#errorPG_IdOrden", "Ingrese orden", true);
        valid = false;
    } else
        appendErrorMessage("#errorPG_IdOrden", "");

    if (valid) {
        $('#dialogGestionarPagina').dialog('close');
        if ($("#P_IdPagina").val() == 0) {
            Insertar_Pagina();
        }
        else {
            Actualizar_Pagina();
        }
    }
}

function ValidarPaginaPadre() {
    var valid = true;
    appendErrorMessage("#errorPG_NombrePaginaPadre", "");
    appendErrorMessage("#errorPG_IdEmpresaPadre", "");
    appendErrorMessage("#errorPG_IdAplicacionPadre", "");
    appendErrorMessage("#errorPG_IdModuloPadre", "");
    appendErrorMessage("#errorPG_IdOrdenPadre", "");


    if ($("#PG_NombrePaginaPadre").val() == 0) {
        appendErrorMessage("#errorPG_NombrePaginaPadre", "Ingrese nombre página", true);
        valid = false;
    } else
        appendErrorMessage("#errorPG_NombrePaginaPadre", "");


    if ($("#PG_IdEmpresaPadre").val() == 0 || $("#PG_IdEmpresaPadre").val() == "") {
        appendErrorMessage("#errorPG_IdEmpresaPadre", "Seleccione Empresa", true);
        valid = false;
    } else
        appendErrorMessage("#errorPG_IdEmpresaPadre", "");



    if ($("#PG_IdAplicacionPadre").val() == 0) {
        appendErrorMessage("#errorPG_IdAplicacionPadre", "Seleccione aplicación", true);
        valid = false;
    } else
        appendErrorMessage("#errorPG_IdAplicacionPadre", "");

    if ($("#PG_IdModuloPadre").val() == 0) {
        appendErrorMessage("#errorPG_IdModuloPadre", "Seleccione módulo", true);
        valid = false;
    } else
        appendErrorMessage("#errorPG_IdModuloPadre", "");


    if ($("#PG_IdOrdenPadre").val().trim() == '') {
        appendErrorMessage("#errorPG_IdOrdenPadre", "Ingrese orden", true);
        valid = false;
    } else
        appendErrorMessage("#errorPG_IdOrdenPadre", "");


    if (valid) {
        $('#dialogGestionarPaginaPadre').dialog('close');
        Insertar_PaginaPadre();
    }
}


function ValidarPaginaPadreEdit() {
    var valid = true;
    appendErrorMessage("#errorPG_NombrePaginaPadreEdit", "");
    appendErrorMessage("#errorPG_IdEmpresaPadreEdit", "");
    appendErrorMessage("#errorPG_IdAplicacionPadreEdit", "");
    appendErrorMessage("#errorPG_IdModuloPadreEdit", "");
    appendErrorMessage("#errorPG_IdOrdenPadreEdit", "");


    if ($("#PG_NombrePaginaPadreEdit").val() == 0) {
        appendErrorMessage("#errorPG_NombrePaginaPadreEdit", "Ingrese nombre página", true);
        valid = false;
    } else
        appendErrorMessage("#errorPG_NombrePaginaPadreEdit", "");


    if ($("#PG_IdEmpresaPadreEdit").val() == 0 || $("#PG_IdEmpresaPadreEdit").val() == "") {
        appendErrorMessage("#errorPG_IdEmpresaPadreEdit", "Seleccione Empresa", true);
        valid = false;
    } else
        appendErrorMessage("#errorPG_IdEmpresaPadreEdit", "");



    if ($("#PG_IdAplicacionPadreEdit").val() == 0) {
        appendErrorMessage("#errorPG_IdAplicacionPadreEdit", "Seleccione aplicación", true);
        valid = false;
    } else
        appendErrorMessage("#errorPG_IdAplicacionPadreEdit", "");

    if ($("#PG_IdModuloPadreEdit").val() == 0) {
        appendErrorMessage("#errorPG_IdModuloPadreEdit", "Seleccione módulo", true);
        valid = false;
    } else
        appendErrorMessage("#errorPG_IdModuloPadreEdit", "");


    if ($("#PG_IdOrdenPadreEdit").val().trim() == '') {
        appendErrorMessage("#errorPG_IdOrdenPadreEdit", "Ingrese orden", true);
        valid = false;
    } else
        appendErrorMessage("#errorPG_IdOrdenPadreEdit", "");


    if (valid) {
        $('#dialogGestionarPagina').dialog('close');
        Actualizar_PaginaPadre();
    }
}

//Consultas!
function BuscarPagina() {
    //url
    var ParamUrl = $("#Url_Consultar_Pagina").val();
    var IdEmpresa = $("#P_IdEmpresa").val();
    var IdAplicacion = $("#P_IdAplicacion").val();
    var IdModulo = $("#P_IdModulo").val();
    var NombrePagina = $("#P_NombrePagina").val();

    //server request
    $.ajax({
        url: ParamUrl,
        data: {
            IdEmpresa: IdEmpresa,
            IdAplicacion: IdAplicacion,
            IdModulo: IdModulo,
            NombrePagina: NombrePagina
        },
        type: "get",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#Id_Consultar_Pagina_Grilla").html(data);
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            SetTotalRegistrosPagina();
        }
    });
}

function BuscarPaginaAccion() {
    var ParamUrl = $("#Url_Consultar_PaginaAccion").val();

    PrimeraVezAccion = true;
    DataAccion = [];

    var IdPagina = $("#P_IdPagina").val();
    var IdsGrupo = ($("#PG_IdGrupo").val() == null ? "" : $("#PG_IdGrupo").val() + '');

    //server request
    $.ajax({
        url: ParamUrl,
        data: { IdPagina: IdPagina, IdsGrupo: IdsGrupo },
        type: "get",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            PrimeraVezAccion = true;
            $("#Id_Consultar_Accion_Grilla").html(data);
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            SetTotalRegistrosAccion();
        }
    });
}

function SetTotalRegistrosAccion() {
    var page = 1;
    $("#GrillaConsultaPaginaAccion tfoot tr a, #GrillaConsultaPaginaAccion thead tr a").on("click", function (e) {


        //pagination parameters
        var sortdir = "";
        var sort = "";
        var page = 1;
        var queue = "";
        //logic for url action
        
        var ddd = this;
        var url = $(this).attr('href');

        var arr = new Array()
        arr = url.split("?")
        queue = arr[0].toString() + "?";
        arr = arr[1].toString().split("&")

        for (var i = 0; i <= arr.length - 1; i++) {
            if (arr[i].indexOf("page") >= 0)
                page = arr[i].toString().split("=")[1].toString();
            if (arr[i].indexOf("sort=") >= 0)
                sort = arr[i].toString().split("=")[1].toString();
            if (arr[i].indexOf("sortdir") >= 0)
                sortdir = arr[i].toString().split("=")[1].toString();
        }

        var IdPagina = $("#P_IdPagina").val();
        var IdsGrupo = ($("#PG_IdGrupo").val() == null ? "" : $("#PG_IdGrupo").val() + '');

        //adding filter and pagination parameters
        var newurl = "";
        newurl = queue +
            "&IdPagina=" + IdPagina +
            "&IdsGrupo=" + IdsGrupo +
            "&sortdir=" + sortdir +
            "&sort=" + sort +
            "&page=" + page;
        $(this).attr('href', newurl);
        
        if (PrimeraVezAccion) {
            MemoryChecks(1, true);
            PrimeraVezAccion = false;
        } else if (DataAccion.length > 0) {
            MemoryChecks($("#PagePreview").val(), false);
        }
        $("#PagePreview").val(page);
    });
    
    $("#GrillaConsultaPaginaAccion tfoot tr:last td").prepend("<a id='Ac_tfootPage'  class='total_registros'></a>");
    $("#Ac_tfootPage").html($('#Ac_FooterDesc').val());

    if (DataAccion.length > 0) {
        CargarChecks($("#PagePreview").val(), DataAccion);
    }
}

function SetTotalRegistrosPagina() {
    $("#GrillaConsultaPagina tfoot tr a, #GrillaConsultaPagina thead tr a").on("click", function (e) {

        var IdEmpresa = $("#P_IdEmpresa").val();
        var IdAplicacion = $("#P_IdAplicacion").val();
        var IdModulo = $("#P_IdModulo").val();
        var NombrePagina = $("#P_NombrePagina").val();

        //pagination parameters
        var sortdir = "";
        var sort = "";
        var page = 1;
        var queue = "";
        //logic for url action
        var url = $(this).attr('href');

        var arr = new Array()
        arr = url.split("?")
        queue = arr[0].toString() + "?";
        arr = arr[1].toString().split("&")

        for (var i = 0; i <= arr.length - 1; i++) {
            if (arr[i].indexOf("page") >= 0)
                page = arr[i].toString().split("=")[1].toString();
            if (arr[i].indexOf("sort=") >= 0)
                sort = arr[i].toString().split("=")[1].toString();
            if (arr[i].indexOf("sortdir") >= 0)
                sortdir = arr[i].toString().split("=")[1].toString();
        }

        //adding filter and pagination parameters
        var newurl = "";
        newurl = queue +
            "&IdEmpresa=" + IdEmpresa +
            "&IdAplicacion=" + IdAplicacion +
            "&IdModulo=" + IdModulo +
            "&NombrePagina=" + NombrePagina +
            "&sortdir=" + sortdir +
            "&sort=" + sort +
            "&page=" + page;
        $(this).attr('href', newurl);
    });

    $("#GrillaConsultaPagina tfoot tr:last td").prepend("<a id='P_tfootPage'  class='total_registros'></a>");
    $("#P_tfootPage").html($('#P_FooterDesc').val());
}

$(function () {
    SetTotalRegistrosPagina();
    SetTotalRegistrosAccion();
});

function P_BuscarAplicacionPorEmpresa(Gestionar) {
    var ParamUrl = $("#Url_P_AplicacionPorEmpresa").val();
    var IdEmpresa;
    var Type;
    if (!Gestionar) {
        IdEmpresa = $("#P_IdEmpresa").val();
        Type = 0;
    }
    else {
        IdEmpresa = $("#PG_IdEmpresa").val();
        Type = 1;
    }

    $.ajax({
        url: ParamUrl,
        data: { IdEmpresa: IdEmpresa, Type: Type },
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            if (!Gestionar)
                $("#P_Div_Aplicacion").html(data);
            else
                $("#PG_Div_Aplicacion").html(data);
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            if (!Gestionar)
                P_BuscarModuloPorAplicacion();
            else
                P_BuscarModuloPorAplicacion(true);
        }
    });
}


function P_BuscarAplicacionPorEmpresaPadre(_this) {
    var ParamUrl = $("#Url_P_AplicacionPorEmpresaPadre").val();
    var IdEmpresa = $("#" + _this.id).val();
   
    $.ajax({
        url: ParamUrl,
        data: { IdEmpresa: IdEmpresa},
        type: "post",
        cache: false,
        success: function (data) {
            $("#PG_Div_AplicacionPadre").html(data);
        },
        complete: function () {
            P_BuscarModuloPorAplicacionPadre($("#PG_IdAplicacionPadre")[0]);
        }
    });
}

function P_BuscarModuloPorAplicacionPadre(_this) {
    var ParamUrl = $("#Url_P_ModuloPorAplicacionPadre").val();
    IdAplicacion = $("#" + _this.id).val();

    $.ajax({
        url: ParamUrl,
        data: { IdAplicacion: IdAplicacion},
        type: "post",
        cache: false,
        success: function (data) {
            $("#PG_Div_ModuloPadre").html(data);
        }
    });
}

function P_BuscarAplicacionPorEmpresaPadreEdit(_this) {
    var ParamUrl = $("#Url_P_AplicacionPorEmpresaPadre").val();
    var IdEmpresa = $("#" + _this.id).val();

    $.ajax({
        url: ParamUrl,
        data: { IdEmpresa: IdEmpresa, Type : 1 },
        type: "post",
        cache: false,
        success: function (data) {
            $("#PG_Div_AplicacionPadreEdit").html(data);
        },
        complete: function () {
            P_BuscarModuloPorAplicacionPadreEdit($("#PG_IdAplicacionPadreEdit")[0]);
        }
    });
}

function P_BuscarModuloPorAplicacionPadreEdit(_this) {
    var ParamUrl = $("#Url_P_ModuloPorAplicacionPadre").val();
    IdAplicacion = $("#" + _this.id).val();

    $.ajax({
        url: ParamUrl,
        data: { IdAplicacion: IdAplicacion, Type: 1 },
        type: "post",
        cache: false,
        success: function (data) {
            $("#PG_Div_ModuloPadreEdit").html(data);
        }
    });
}

function P_BuscarModuloPorAplicacion(Gestionar) {
    var ParamUrl = $("#Url_P_ModuloPorAplicacion").val();
    var IdAplicacion;
    var Type;
    if (!Gestionar) {
        IdAplicacion = $("#P_IdAplicacion").val();
        Type = 0;
    }
    else {
        IdAplicacion = $("#PG_IdAplicacion").val();
        Type = 1;
    }

    $.ajax({
        url: ParamUrl,
        data: { IdAplicacion: IdAplicacion, Type: Type },
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            if (!Gestionar) 
                $("#P_Div_Modulo").html(data);
            else
                $("#PG_Div_Modulo").html(data);
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            if (Gestionar) 
                P_BuscarPaginaPadre();
        }
    });
}

function P_BuscarPaginaPadre(InsAct) {
    var ParamUrl = $("#Url_P_PaginaPadrePorModulo").val();

    var IdModulo = $("#PG_IdModulo").val();
    var Type = 1;

    $.ajax({
        url: ParamUrl,
        data: { IdModulo: IdModulo, Type: Type },
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#PG_Div_Padre").html(data);
            P_BuscarAgrupacionPorModulo();
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
        }
    });
}

function P_BuscarAgrupacionPorModulo() {
    var ParamUrl = $("#Url_AgrupacionPorModulo").val();

    var IdModulo = $("#PG_IdModulo").val();

    $.ajax({
        url: ParamUrl,
        data: { IdModulo: IdModulo},
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#PG_Div_Agrupacion").html(data);
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
        }
    });
}

var ChangeCheckEscritorio = function () {

    if ($('#iIdEscritorio').attr('checked')) {
        $("#DivAgrupacion").css("display", "block");
        $("#DivTamañoMenu").css("display", "block");        
    } else {
        $("#DivAgrupacion").css("display", "none");
        $("#DivTamañoMenu").css("display", "none");
    }
}

function VerPagina(IdEmpresa, IdAplicacion, IdModulo) {
    var ParamUrl = $("#Url_P_Empresa").val();

    $.ajax({
        url: ParamUrl,
        data: { IdEmpresa: IdEmpresa },
        type: "get",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#P_Div_Empresa").html(data);
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            $("#lnkPagina").click();
            $('#P_NombrePagina').val('');
            BuscarAplicacion();
        }
    });

    ParamUrl = $("#Url_P_Aplicacion").val();

    $.ajax({
        url: ParamUrl,
        data: { IdAplicacion: IdAplicacion, IdEmpresa: IdEmpresa },
        type: "get",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#P_Div_Aplicacion").html(data);
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
        }
    });

    ParamUrl = $("#Url_P_Modulo").val();

    $.ajax({
        url: ParamUrl,
        data: { IdModulo: IdModulo, IdAplicacion: IdAplicacion },
        type: "get",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#P_Div_Modulo").html(data);
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            BuscarPagina();
        }
    });
}

function CargarChecks(page, checks) {
    var Total = parseInt($('#Ac_RowCount').val());
    var RegPag = parseInt($('#Ac_RowsPerPage').val());

    var TotalPage = 1;
    if ((Total / RegPag) > Math.round(Total / RegPag)) {
        TotalPage = Math.round(Total / RegPag) + 1;
    }
    else {
        TotalPage = Math.round(Total / RegPag);
    }
    
    for (var j = 1; j < TotalPage + 1; j++) {
        if (j == page) {
            $("#GrillaConsultaPaginaAccion tbody tr").each(function (i, element) {
                if (!checks[((page - 1) * RegPag) + i].SiSql)
                    ($(element).find('td:nth-child(1) input[type=checkbox]')[0]).checked = checks[((page - 1) * RegPag) + i].Check;
            });
            break;
        } 
    }
}

function MemoryChecks(page, PrimeraVez) {
    if (PrimeraVez) {
        var checks = [];
    } else {
        var checks = DataAccion;
    }
    
    var Total = parseInt($('#Ac_RowCount').val());
    var RegPag = parseInt($('#Ac_RowsPerPage').val());
    
    var TotalPage = 1;
    if ((Total / RegPag) > Math.round(Total / RegPag)) {
        TotalPage = Math.round(Total / RegPag) + 1;
    }
    else {
        TotalPage = Math.round(Total / RegPag);
    }
    
    for (var w = 1; w < TotalPage + 1; w++) {
        if (w == page) {
            $("#GrillaConsultaPaginaAccion tbody tr").each(function (i, element) {
                
                var Data = {
                    Check: ($(element).find('td:nth-child(1) input[type=checkbox]')[0]).checked,
                    SiSql: false,
                    IdAccion: parseInt(($(element).find('td:nth-child(1) input[type=checkbox]')[0]).value)
                };
                if (PrimeraVez) {
                    checks.push(Data);
                } else {
                    checks[((page - 1) * RegPag) + i] = Data;
                }
            });
        } else {
            if (TotalPage == w) {
                RegPag = RegPag + (Total - (w * RegPag));
            }
            if (PrimeraVez) {
                for (var j = 0; j < RegPag; j++) {
                    var Data = {
                        Check: false,
                        SiSql: true,
                        IdAccion: 0
                    };
                    checks.push(Data)
                }
            }
        }
    }

    DataAccion = checks;
}

function DataPagina() {
    var Modulo = {
        IdModulo: $("#PG_IdModulo").val()
    }

    var IdPaginaPadre = $('#PG_IdPaginaPadre').val();
    var Grupo = {
        IdsGrupo: $("#PG_IdGrupo").val() + ''
    }

    var D = {
        IdPagina: $("#P_IdPagina").val(),
        IdPaginaPadre: (IdPaginaPadre == '' ? 0 : IdPaginaPadre),
        Url: $("#PE_Url").val(),
        Visible: $("#PG_SiVisible").is(":checked"),
        Icono: $("#PG_Icono").val(),
        Nombre: $("#PG_NombrePagina").val(),
        Modulo: Modulo,
        Grupo: Grupo,
        Orden: $("#PG_IdOrden").val()
    }

    return D;
}

function DataPaginaPadre() {
    var Modulo = {
        IdModulo: $("#PG_IdModuloPadre").val()
    }

    var D = {
        IdPagina: $("#P_IdPaginaPadre").val(),
        Url: $("#PE_UrlPadre").val(),
        Visible: $("#PG_SiVisiblePadre").is(":checked"),
        Icono: $("#PG_IconoPadre").val(),
        Nombre: $("#PG_NombrePaginaPadre").val(),
        Modulo: Modulo,
        Orden: $("#PG_IdOrdenPadre").val()
    }

    return D;
}

function DataPaginaPadreEdit() {
    var Modulo = {
        IdModulo: $("#PG_IdModuloPadreEdit").val()
    }

    var D = {
        IdPagina: $("#P_IdPaginaPadreEdit").val(),
        Url: $("#PE_UrlPadreEdit").val(),
        Visible: $("#PG_SiVisiblePadreEdit").is(":checked"),
        Icono: $("#PG_IconoPadreEdit").val(),
        Nombre: $("#PG_NombrePaginaPadreEdit").val(),
        Modulo: Modulo,
        Orden: $("#PG_IdOrdenPadreEdit").val()
    }

    return D;
}

function DataCheckAccion() {
    var ListaPaginaAccion = [];
    var ListaData = DataAccion;

    var PageAct = $("#PagePreview").val();
    var Total = parseInt($("#Ac_RowCount").val());
    var RegPag = parseInt($("#Ac_RowsPerPage").val());
    var RegEnPag = RegPag;
    var TotalPage = 1;
    if ((Total / RegPag) > Math.round(Total / RegPag)) {
        TotalPage = Math.round(Total / RegPag) + 1;
    }
    else {
        TotalPage = Math.round(Total / RegPag);
    }

    for (var j = 1; j < TotalPage + 1; j++) {
        if (TotalPage == j) {
            RegEnPag = RegPag + (Total - (j * RegPag));
        }
        else {
            RegEnPag = RegPag;
        }

        if (j == PageAct) {
            $("#GrillaConsultaPaginaAccion tbody tr").each(function (i, element) {
                var check = ($(element).find('td:nth-child(1) input[type=checkbox]')[0]).checked;
                var IdAccion = parseInt(($(element).find('td:nth-child(1) input[type=checkbox]')[0]).value);
                var IdGrupo = $("#" + IdAccion).val();

                var Pagina = {
                    IdPagina: $("#P_IdPagina").val()
                }

                var Grupo = {
                    IdGrupo: IdGrupo
                }

                var Accion = {
                    IdAccion: IdAccion,
                    Grupo: Grupo
                }

                var Data = {
                    ChkAgregar: check,
                    Pagina: Pagina,
                    Accion: Accion
                };
                ListaPaginaAccion.push(Data);
            });
        } else {
            if (ListaData.length > 0) {
                for (var i = 0; i < RegEnPag; i++) {
                    var Check = ListaData[(j - 1) * RegPag + i].Check;
                    
                    var Accion = {
                        IdAccion: ListaData[(j - 1) * RegPag + i].IdAccion
                    }

                    var Pagina = {
                        IdPagina: $("#P_IdPagina").val()
                    }
                    
                    var Data = {
                        ChkAgregar: Check,
                        Pagina: Pagina,
                        Accion: Accion
                    };
                    ListaPaginaAccion.push(Data);
                }
            }
        }
    }
    return ListaPaginaAccion;
}

function HabilitarAcciones() {
    if ($("#PG_IdPaginaPadre").val() != '') {
        $("#Div_Acciones").attr("style", "display:'';");
    }
    else {
        $("#Div_Acciones").attr("style", "display:none;");
    }
}