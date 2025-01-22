//Transacciones
function NuevaAplicacion() {
    var ParamUrl = $("#Url_Registrar_Aplicacion").val();

    $.ajax({
        url: ParamUrl,
        data: {},
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#dialogGestionarAplicacion").html(data);
            InicioJPopUpOpen('#dialogGestionarAplicacion');
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
        }
    });
}

function EditarAplicacion(IdAplicacion) {
    var ParamUrl = $("#Url_Editar_Aplicacion").val();

    $.ajax({
        url: ParamUrl,
        data: { IdAplicacion: IdAplicacion },
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#dialogGestionarAplicacion").html(data);
            InicioJPopUpOpen('#dialogGestionarAplicacion');
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
        }
    });
}

function ActivarDesactivarAplicacion(IdAplicacion, IdEstado, Mensaje) {
    $("#dialogConfirmacion p").text(Mensaje);
    $("#dialogConfirmacion").data('Opcion', 'Actualizar_EstadoAplicacion');
    $("#dialogConfirmacion").data('IdAplicacion', IdAplicacion);
    $("#dialogConfirmacion").data('IdEstado', IdEstado);
    $("#dialogConfirmacion").dialog('open');
}

function Insertar_Aplicacion() {
    var ParamUrl = $("#Url_Insertar_Aplicacion").val();
    D = DataAplicacion();

    $.ajax({
        url: ParamUrl,
        data: D,
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            if (data.identificador = -1)
                alert(data.mensaje);
            else {
                $("#dialogResultado p").html(data.mensaje);
                $("#dialogResultado").dialog('open');
            }
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            BuscarAplicacion();
        }
    });
}

function Actualizar_Aplicacion() {
    var ParamUrl = $("#Url_Actualizar_Aplicacion").val();
    
    D = DataAplicacion();

    $.ajax({
        url: ParamUrl,
        data: D,
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#dialogResultado p").html(data.mensaje);
            $("#dialogResultado").dialog('open');
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            BuscarAplicacion();
        }
    });
}

function DataAplicacion() {
    var D = {
        IdAplicacion: $("#A_IdAplicacion").val(),
        IdsEmpresa: ($("#AG_IdEmpresa").val() == null ? "" : $("#AG_IdEmpresa").val()) + '',
        Url: $("#AG_Url").val(),
        NombreAplicacion: $("#AG_NombreAplicacion0").val(),
        DescripcionAplicacion: $("#AG_DescripcionAplicacion0").val()
    }
    return D;
}

function ActualizarEstado_Aplicacion(IdAplicacion, IdEstado) {
    var ParamUrl = $("#Url_ActualizarEstado_Aplicacion").val();

    $.ajax({
        url: ParamUrl,
        data: { IdAplicacion: IdAplicacion, IdEstado: IdEstado },
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#dialogResultado p").html(data.mensaje);
            $("#dialogResultado").dialog('open');
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            BuscarAplicacion();
        }
    });
}

function ValidarAplicacion() {
    var valid = true;
    appendErrorMessage("#errorAG_IdEmpresa", "");
    appendErrorMessage("#errorAG_Url", "");
    appendErrorMessage("#errorAG_NombreAplicacion0", "");
    appendErrorMessage("#errorAG_DescripcionAplicacion0", "");

    if ($("#AG_IdEmpresa").val() == '' || $("#AG_IdEmpresa").val() == null) {
        appendErrorMessage("#errorAG_IdEmpresa", "Seleccione empresa", true);
        valid = false;
    } else
        appendErrorMessage("#errorAG_IdEmpresa", "");

    if ($("#AG_Url").val().trim() == '') {
        appendErrorMessage("#errorAG_Url", "Ingrese url", true);
        valid = false;
    } else
        appendErrorMessage("#errorAG_Url", "");

    if ($("#AG_NombreAplicacion0").val().trim() == '') {
        appendErrorMessage("#errorAG_NombreAplicacion0", "Ingrese nombre", true);
        valid = false;
    } else
        appendErrorMessage("#errorAG_NombreAplicacion0", "");

    if ($("#AG_DescripcionAplicacion0").val().trim() == '') {
        appendErrorMessage("#errorAG_DescripcionAplicacion0", "Ingrese descripción", true);
        valid = false;
    } else
        appendErrorMessage("#errorAG_DescripcionAplicacion0", "");

    if (valid) {
        $('#dialogGestionarAplicacion').dialog('close');
        if ($('#A_IdAplicacion').val() == '0') {            
            Insertar_Aplicacion();
        }
        else {
            Actualizar_Aplicacion();
        }
    }
}

//Consultas!
function BuscarAplicacion() {
    //url
    var ParamUrl = $("#Url_Consultar_Aplicacion").val();
    var IdsEmpresa = ($("#A_IdEmpresa").val() == null ? "" : $("#A_IdEmpresa").val()) + '';
    var NombreAplicacion = $("#A_NombreAplicacion").val();
    var DescripcionAplicacion = $("#A_DescripcionAplicacion").val();

    //server request
    $.ajax({
        url: ParamUrl,
        data: {
            IdsEmpresa: IdsEmpresa,
            NombreAplicacion: NombreAplicacion,
            DescripcionAplicacion: DescripcionAplicacion
        },
        type: "get",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#Id_Consultar_Aplicacion_Grilla").html(data);
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            SetTotalRegistrosAplicacion();
        }
    });
}

function SetTotalRegistrosAplicacion() {
    $("#GrillaConsultaAplicacion tfoot tr a, #GrillaConsultaAplicacion thead tr a").on("click", function (e) {

        var IdsEmpresa = ($("#A_IdEmpresa").val() == null ? "" : $("#A_IdEmpresa").val()) + '';
        var NombreAplicacion = $("#A_NombreAplicacion").val();
        var DescripcionAplicacion = $("#A_DescripcionAplicacion").val();

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

        var IdEmpresa = $("#A_IdEmpresa").val();
        var NombreAplicacion = $("#A_NombreAplicacion").val();
        var DescripcionAplicacion = $("#A_DescripcionAplicacion").val();

        //adding filter and pagination parameters
        var newurl = "";
        newurl = queue +
            "&IdEmpresa=" + IdEmpresa +
            "&NombreAplicacion=" + NombreAplicacion +
            "&DescripcionAplicacion=" + DescripcionAplicacion +
            "&sortdir=" + sortdir +
            "&sort=" + sort +
            "&page=" + page;
        $(this).attr('href', newurl);
    });

    $("#GrillaConsultaAplicacion tfoot tr:last td").prepend("<a id='A_tfootPage'  class='total_registros'></a>");
    $("#A_tfootPage").html($('#A_FooterDesc').val());
}

$(function () {
    SetTotalRegistrosAplicacion();
});

function VerModulo(IdEmpresa, IdAplicacion) {
    var ParamUrl = $("#Url_M_Empresa").val();

    $.ajax({
        url: ParamUrl,
        data: { IdEmpresa: IdEmpresa },
        type: "get",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#M_Div_Empresa").html(data);
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            $("#lnkModulo").click();
            $('#M_NombreModulo').val('');
            $('#M_DescripcionModulo').val('');
            BuscarAplicacion();
        }
    });

    ParamUrl = $("#Url_M_Aplicacion").val();

    $.ajax({
        url: ParamUrl,
        data: { IdAplicacion: IdAplicacion, IdEmpresa: IdEmpresa },
        type: "get",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#M_Div_Aplicacion").html(data);
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            BuscarModulo();
        }
    });
}

function RefreshAplicacion() {
    var ParamUrl = $("#Url_A_Empresa").val();
    
    $.ajax({
        url: ParamUrl,
        data: { IdEmpresa: 0 },
        type: "get",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#A_Div_Empresa").html(data);
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            $("#lnkAplicacion").click();
            $('#A_NombreAplicacion').val('');
            $('#A_DescripcionAplicacion').val('');
            BuscarAplicacion();
        }
    });
}

function RefreshModulo() {
    var ParamUrl = $("#Url_M_Empresa").val();

    $.ajax({
        url: ParamUrl,
        data: { IdEmpresa: 0 },
        type: "get",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#M_Div_Empresa").html(data);
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            $("#lnkModulo").click();
            $('#M_NombreModulo').val('');
            $('#M_DescripcionModulo').val('');
            BuscarAplicacion();
        }
    });
}

function RefreshPagina() {
    var ParamUrl = $("#Url_P_Empresa").val();

    $.ajax({
        url: ParamUrl,
        data: { IdEmpresa: 0 },
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
}