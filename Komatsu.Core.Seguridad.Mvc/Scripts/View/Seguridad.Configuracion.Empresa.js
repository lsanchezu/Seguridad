//Transacciones
function NuevaEmpresa() {
    var ParamUrl = $("#Url_Registrar_Empresa").val();

    $.ajax({
        url: ParamUrl,
        data: {},
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#dialogGestionarEmpresa").html(data);
            InicioJPopUpOpen('#dialogGestionarEmpresa');
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
        }
    });
}

function EditarEmpresa(IdEmpresa) {
    var ParamUrl = $("#Url_Editar_Empresa").val();
    
    $.ajax({
        url: ParamUrl,
        data: { IdEmpresa: IdEmpresa },
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#dialogGestionarEmpresa").html(data);
            InicioJPopUpOpen('#dialogGestionarEmpresa');
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
        }
    });
}

function ActivarDesactivarEmpresa(IdEmpresa, IdEstado, Mensaje) {
    $("#dialogConfirmacion p").text(Mensaje);
    $("#dialogConfirmacion").data('Opcion', 'Actualizar_EstadoEmpresa');
    $("#dialogConfirmacion").data('IdEmpresa', IdEmpresa);
    $("#dialogConfirmacion").data('IdEstado', IdEstado);
    $("#dialogConfirmacion").dialog('open');
}

function Insertar_Empresa() {
    var ParamUrl = $("#Url_Insertar_Empresa").val();
    var D = DataEmpresa();

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
            BuscarEmpresa();
        }
    });
}

function Actualizar_Empresa() {
    var ParamUrl = $("#Url_Actualizar_Empresa").val();
    var D = DataEmpresa();

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
            BuscarEmpresa();
        }
    });
}

function DataEmpresa() {
    var D = {
        IdEmpresa: $("#E_IdEmpresa").val(),
        NombreEmpresa: $("#EG_NombreEmpresa").val(),
        Abreviatura: $("#EG_Abreviatura").val(),
        CodigoEmpresa: $("#EG_CodigoEmpresa").val(),
        ContentStyle: $("#EG_ContentStyle").val()
    }
    return D;
}

function ActualizarEstado_Empresa(IdEmpresa, IdEstado) {
    var ParamUrl = $("#Url_ActualizarEstado_Empresa").val();

    $.ajax({
        url: ParamUrl,
        data: { IdEmpresa: IdEmpresa, IdEstado: IdEstado },
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#dialogResultado p").html(data.mensaje);
            $("#dialogResultado").dialog('open');
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            BuscarEmpresa();
        }
    });
}

function ValidarEmpresa() {
    var valid = true;
    appendErrorMessage("#errorEG_NombreEmpresa", "");
    appendErrorMessage("#errorEG_Abreviatura", "");
    appendErrorMessage("#errorEG_CodigoEmpresa", "");
    appendErrorMessage("#errorEG_ContentStyle", "");
    
    if ($("#EG_NombreEmpresa").val().trim() == '') {
        appendErrorMessage("#errorEG_NombreEmpresa", "Ingrese nombre", true);
        valid = false;
    } else
        appendErrorMessage("#errorEG_NombreEmpresa", "");

    if ($("#EG_Abreviatura").val().trim() == '') {
        appendErrorMessage("#errorEG_Abreviatura", "Ingrese Abreviatura", true);
        valid = false;
    } else
        appendErrorMessage("#errorEG_Abreviatura", "");

    if ($("#EG_CodigoEmpresa").val().trim() == '') {
        appendErrorMessage("#errorEG_CodigoEmpresa", "Ingrese Código Empresa", true);
        valid = false;
    } else
        appendErrorMessage("#errorEG_CodigoEmpresa", "");

    if ($("#EG_ContentStyle").val().trim() == '') {
        appendErrorMessage("#errorEG_ContentStyle", "Ingrese Content-Style", true);
        valid = false;
    } else
        appendErrorMessage("#errorEG_ContentStyle", "");

    if (valid) {
        $('#dialogGestionarEmpresa').dialog('close');
        if ($('#E_IdEmpresa').val() == '0') {
            Insertar_Empresa();
        }
        else {
            Actualizar_Empresa();
        }
    } 
}

//Consultas!
function BuscarEmpresa() {
    //url
    var ParamUrl = $("#Url_Consultar_Empresa").val();
    var NombreEmpresa = $("#textEmpresaNombre").val();

    //server request
    $.ajax({
        url: ParamUrl,
        data: { NombreEmpresa: NombreEmpresa },
        type: "get",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#Id_Consultar_Empresa_Grilla").html(data);
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            SetTotalRegistrosEmpresa();
        }
    });
}

function SetTotalRegistrosEmpresa()
{
    $("#GrillaConsultaEmpresa tfoot tr a, #GrillaConsultaEmpresa thead tr a").on("click", function (e) {

        var NombreEmpresa = $("#textEmpresaNombre").val();
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

        var NombreEmpresa = $("#textEmpresaNombre").val();

        //adding filter and pagination parameters
        var newurl = "";
        newurl = queue +
            "&NombreEmpresa=" + NombreEmpresa +
            "&sortdir=" + sortdir +
            "&sort=" + sort +
            "&page=" + page;
        $(this).attr('href', newurl);
    });

    $("#GrillaConsultaEmpresa tfoot tr:last td").prepend("<a id='E_tfootPage'  class='total_registros'></a>");
    $("#E_tfootPage").html($('#E_FooterDesc').val());
}

$(function () {
    SetTotalRegistrosEmpresa();
});



function VerAplicacion(IdEmpresa){
    var ParamUrl = $("#Url_A_Empresa").val();

    $.ajax({
        url: ParamUrl,
        data: { IdEmpresa: IdEmpresa },
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