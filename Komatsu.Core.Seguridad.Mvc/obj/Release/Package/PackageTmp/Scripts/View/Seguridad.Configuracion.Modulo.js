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
//Transacciones
function NuevoModulo() {
    var ParamUrl = $("#Url_Registrar_Modulo").val();

    $.ajax({
        url: ParamUrl,
        data: {},
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#dialogGestionarModulo").html(data);
            InicioJPopUpOpen('#dialogGestionarModulo');
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
        }
    });
}

function EditarModulo(IdModulo) {
    var ParamUrl = $("#Url_Editar_Modulo").val();

    $.ajax({
        url: ParamUrl,
        data: { IdModulo: IdModulo },
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#dialogGestionarModulo").html(data);
            InicioJPopUpOpen('#dialogGestionarModulo');
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
        }
    });
}

function ActivarDesactivarModulo(IdModulo, IdEstado, Mensaje) {
    $("#dialogConfirmacion p").text(Mensaje);
    $("#dialogConfirmacion").data('Opcion', 'Actualizar_EstadoModulo');
    $("#dialogConfirmacion").data('IdModulo', IdModulo);
    $("#dialogConfirmacion").data('IdEstado', IdEstado);
    $("#dialogConfirmacion").dialog('open');
}

function Insertar_Modulo() {
    var ParamUrl = $("#Url_Insertar_Modulo").val();
    var IdAplicacion = $("#MG_IdAplicacion").val();
    var NombreModulo = $("#MG_NombreModulo0").val();
    var DescripcionModulo = $("#MG_DescripcionModulo0").val();

    $.ajax({
        url: ParamUrl,
        data: { IdAplicacion: IdAplicacion, NombreModulo: NombreModulo, DescripcionModulo: DescripcionModulo },
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#dialogResultado p").html(String.format(data.mensaje, "",$("#MG_IdAplicacion option:selected").text()));
            $("#dialogResultado").dialog('open');
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            BuscarModulo();
        }
    });
}

function Actualizar_Modulo() {
    var ParamUrl = $("#Url_Actualizar_Modulo").val();
    var IdModulo = $("#M_IdModulo").val();
    var IdAplicacion = $("#MG_IdAplicacion").val();
    var NombreModulo = $("#MG_NombreModulo0").val();
    var DescripcionModulo = $("#MG_DescripcionModulo0").val();

    $.ajax({
        url: ParamUrl,
        data: { IdModulo: IdModulo, IdAplicacion: IdAplicacion, NombreModulo: NombreModulo, DescripcionModulo: DescripcionModulo },
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#dialogResultado p").html(data.mensaje);
            $("#dialogResultado").dialog('open');
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            BuscarModulo();
        }
    });
}

function ActualizarEstado_Modulo(IdModulo, IdEstado) {
    var ParamUrl = $("#Url_ActualizarEstado_Modulo").val();

    $.ajax({
        url: ParamUrl,
        data: { IdModulo: IdModulo, IdEstado: IdEstado },
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#dialogResultado p").html(data.mensaje);
            $("#dialogResultado").dialog('open');
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            BuscarModulo();
        }
    });
}

function ValidarModulo() {
    var valid = true;
    appendErrorMessage("#errorMG_IdAplicacion", "");
    appendErrorMessage("#errorMG_NombreModulo0", "");
    appendErrorMessage("#errorMG_DescripcionModulo0", "");

    if ($("#MG_IdAplicacion").val() == 0) {
        appendErrorMessage("#errorMG_IdAplicacion", "Seleccione aplicación", true);
        valid = false;
    } else
        appendErrorMessage("#errorMG_IdEmpresa", "");

    if ($("#MG_NombreModulo0").val().trim() == '') {
        appendErrorMessage("#errorMG_NombreModulo0", "Ingrese nombre", true);
        valid = false;
    } else
        appendErrorMessage("#errorMG_NombreAplicacion0", "");

    if ($("#MG_DescripcionModulo0").val().trim() == '') {
        appendErrorMessage("#errorMG_DescripcionModulo0", "Ingrese descripción", true);
        valid = false;
    } else
        appendErrorMessage("#errorMG_DescripcionModulo0", "");

    if (valid) {
        $('#dialogGestionarModulo').dialog('close');
        if ($('#M_IdModulo').val() == '0') {
            Insertar_Modulo();
        }
        else {
            Actualizar_Modulo();
        }
    }
}

//Consultas!
function BuscarModulo() {
    //url
    var ParamUrl = $("#Url_Consultar_Modulo").val();
    var IdEmpresa = $("#M_IdEmpresa").val();
    var IdAplicacion = $("#M_IdAplicacion").val();
    var NombreModulo = $("#M_NombreModulo").val();
    var DescripcionModulo = $("#M_DescripcionModulo").val();

    //server request
    $.ajax({
        url: ParamUrl,
        data: {
            IdEmpresa: IdEmpresa,
            IdAplicacion: IdAplicacion,
            NombreModulo: NombreModulo,
            DescripcionModulo: DescripcionModulo
        },
        type: "get",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#Id_Consultar_Modulo_Grilla").html(data);
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            SetTotalRegistrosModulo();
        }
    });
}

function SetTotalRegistrosModulo() {
    $("#GrillaConsultaModulo tfoot tr a, #GrillaConsultaModulo thead tr a").on("click", function (e) {

        var IdEmpresa = $("#M_IdEmpresa").val();
        var IdAplicacion = $("#M_IdAplicacion").val();
        var NombreModulo = $("#M_NombreModulo").val();
        var DescripcionModulo = $("#M_DescripcionModulo").val();

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

        var IdEmpresa = $("#M_IdEmpresa").val();
        var IdAplicacion = $("#M_IdAplicacion").val();
        var NombreModulo = $("#M_NombreModulo").val();
        var DescripcionModulo = $("#M_DescripcionModulo").val();

        //adding filter and pagination parameters
        var newurl = "";
        newurl = queue +
            "&IdEmpresa=" + IdEmpresa +
            "&IdAplicacion=" + IdAplicacion +
            "&NombreModulo=" + NombreModulo +
            "&DescripcionModulo=" + DescripcionModulo + 
            "&sortdir=" + sortdir +
            "&sort=" + sort +
            "&page=" + page;
        $(this).attr('href', newurl);
    });

    $("#GrillaConsultaModulo tfoot tr:last td").prepend("<a id='M_tfootPage'  class='total_registros'></a>");
    $("#M_tfootPage").html($('#M_FooterDesc').val());
}

$(function () {
    SetTotalRegistrosModulo();
});

function M_BuscarAplicacionPorEmpresa() {
    var ParamUrl = $("#Url_M_AplicacionPorEmpresa").val();
    var IdEmpresa = $("#M_IdEmpresa").val();

    $.ajax({
        url: ParamUrl,
        data: { IdEmpresa: IdEmpresa },
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#M_Div_Aplicacion").html(data);
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
        }
    });
}