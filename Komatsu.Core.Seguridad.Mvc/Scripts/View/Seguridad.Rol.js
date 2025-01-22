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
function NuevoRol() {
    var ParamUrl = $("#Url_Registrar_Rol").val();

    $.ajax({
        url: ParamUrl,
        data: {},
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#dialogGestionarRol").html(data);
            InicioJPopUpOpen('#dialogGestionarRol');
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
        }
    });
}

function EditarRol(IdRol) {
    var ParamUrl = $("#Url_Editar_Rol").val();

    $.ajax({
        url: ParamUrl,
        data: { IdRol: IdRol },
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#dialogGestionarRol").html(data);
            InicioJPopUpOpen('#dialogGestionarRol');
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
        }
    });
}

function ActivarDesactivarRol(IdRol, IdEstado, Mensaje, SiAdmi) {
    $("#dialogConfirmacion p").text(Mensaje);
    $("#dialogConfirmacion").data('Opcion', 'Actualizar_Estado');
    $("#dialogConfirmacion").data('IdRol', IdRol);
    $("#dialogConfirmacion").data('IdEstado', IdEstado);
    $("#dialogConfirmacion").dialog('open');
}

function Insertar_Rol() {
    var ParamUrl = $("#Url_Insertar_Rol").val();
    D = DataRol();

    $.ajax({
        url: ParamUrl,
        data: D,
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            
            $("#dialogResultado p").html(String.format(data.mensaje, "", $("#GestIdAplicacion option:selected").text()));
            if (data.block) {
                $("#dialogGestionarRol").dialog('open');
                $("#dialogResultado").dialog('open');
            } else
                $("#dialogResultado").dialog('open');
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            BuscarRol();
        }
    });
}

function Actualizar_Rol() {
    var ParamUrl = $("#Url_Actualizar_Rol").val();
    D = DataRol();

    $.ajax({
        url: ParamUrl,
        data: D,
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#dialogResultado p").html(data.mensaje);
            if (data.block) {
                $("#dialogGestionarRol").dialog('open');
                $("#dialogResultado").dialog('open');
            }else
                $("#dialogResultado").dialog('open');
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            BuscarRol();
        }
    });
}

function ActualizarEstado_Rol(IdRol, IdEstado) {
    var ParamUrl = $("#Url_ActualizarEstado_Rol").val();

    $.ajax({
        url: ParamUrl,
        data: { IdRol: IdRol, IdEstado: IdEstado },
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#dialogResultado p").html(data.mensaje);
            $("#dialogResultado").dialog('open');
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            BuscarRol();
        }
    });
}

function VerFechas() {
    if ($("#SiRango").is(":checked")) {
        $("#Div_Fecha").attr("style", "display:'';");
    }
    else {
        $("#Div_Fecha").attr("style", "display:none;");
    }
}

function ValidarRol() {
    var valid = true;
    appendErrorMessage("#errorFechaInicio", "");
    appendErrorMessage("#errorFechaFin", "");
    appendErrorMessage("#errorGestIdAplicacion", "");
    
    if ($("#SiRango").is(":checked")) {
        if ($("#GestIdAplicacion").val() == 0) {
            appendErrorMessage("#errorGestIdAplicacion", "Seleccione la aplicación", true);
            valid = false;
        } else
            appendErrorMessage("#errorGestIdAplicacion", "");
        
        if (ValidarFecha("FechaInicio") != 0) {
            appendErrorMessage("#errorFechaInicio", "Ingrese una fecha inicio correcta", true);
            valid = false;
        }
        if (ValidarFecha("FechaFin") != 0) {
            appendErrorMessage("#errorFechaFin", "Ingrese una fecha fin correcta", true);
            valid = false;
        }

        if (valid) {
            if (Comparar_Fechas($("#FechaInicio").val(), $("#FechaFin").val())) {
                appendErrorMessage("#errorFechaInicio", "Fecha inicio mayor a final", true);
                appendErrorMessage("#errorFechaFin", "Fecha final menor a inicial", true);
                valid = false;
            }
        }
    }

    if ($("#GestIdAplicacion").val().trim() == '') {
        appendErrorMessage("#errorGestIdAplicacion", "Seleccione aplicación", true);
        valid = false;
    } else
        appendErrorMessage("#errorGestIdAplicacion", "");

    if ($("#GestNombreRol").val().trim() == '') {
        appendErrorMessage("#errorGestNombreRol", "Ingrese nombre", true);
        valid = false;
    } else
        appendErrorMessage("#errorGestNombreRol", "");

    if (valid) {
        $('#dialogGestionarRol').dialog('close');
        if ($("#IdRol").val() == '0') {
            Insertar_Rol();
        }
        else {
            Actualizar_Rol();
        }
    } 
}

function DataRol() {
    var D = {
        IdRol: $("#IdRol").val(),
        NombreRol: $("#GestNombreRol").val(),
        FechaInicio: $("#FechaInicio").val().substring(0, 10),
        FechaFin: $("#FechaFin").val().substring(0, 10),
        SiRango: ($("#SiRango").is(":checked") ? true : false),
        IdAplicacion: $("#GestIdAplicacion").val(),
        SiSuperAdmi: ($("#SiSuperAdmi").is(":checked") ? true : false)
    }
    return D;
}

function FechaFormat() {
    $(".datepicker").datepicker({
        showOtherMonths: true,
        autoSize: true,
        changeMonth: true,
        changeYear: true,
        appendText: '(dd/mm/yyyy)',
        dateFormat: 'dd/mm/yy'
    });

    $(".datepicker").mask("99/99/9999");

    $('.inlinedate').datepicker({
        inline: true,
        showOtherMonths: true
    });
}

//Consultas!
function BuscarRol() {
    //url
    var ParamUrl = $("#Url_Consultar_Rol").val();
    var IdAplicacion = $("#IdAplicacion").val()
    var NombreRol = $("#NombreRol").val();

        
    //server request
    $.ajax({
        url: ParamUrl,
        data: { IdAplicacion: IdAplicacion,
                NombreRol: NombreRol },
        type: "get",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#Id_Consultar_Rol_Grilla").html(data);
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            SetTotalRegistrosRol();
        }
    });
}

function SetTotalRegistrosRol() {
    $("#GrillaConsultaRol tfoot tr a, #GrillaConsultaRol thead tr a").on("click", function (e) {

        var IdAplicacion = $("#IdAplicacion").val()
        var NombreRol = $("#NombreRol").val();

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
            "&IdAplicacion=" + IdAplicacion +
            "&NombreRol=" + NombreRol +
            "&sortdir=" + sortdir +
            "&sort=" + sort +
            "&page=" + page;
        $(this).attr('href', newurl);
    });

    $("#GrillaConsultaRol tfoot tr:last td").prepend("<a id='tfootPage'  class='total_registros'></a>");
    $("#tfootPage").html($('#FooterDesc').val());
}

$(function () {
    //SetTotalRegistrosRol();
});