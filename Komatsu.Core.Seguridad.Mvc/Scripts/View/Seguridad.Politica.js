function GuardarPolitica() {
    var valid = true;
    appendErrorMessage("#errorLongMin", "");
    appendErrorMessage("#errorVigencia", "");
    appendErrorMessage("#errorMaxIntentos", "");
    appendErrorMessage("#errorBloqUsuario", "");
    appendErrorMessage("#errorContHistoricas", "");

    if ($("#LongMinContrasena").val() == "") {
        appendErrorMessage("#errorLongMin", "Requerido ", true);
        valid = false;
    } else
        appendErrorMessage("#errorLongMin", "");

    if ($("#VigenciaContrasena").val() == "") {
        appendErrorMessage("#errorVigencia", "Requerido ", true);
        valid = false;
    } else
        appendErrorMessage("#errorVigencia", "");

    if ($("#MaxIntentos").val() == "") {
        appendErrorMessage("#errorMaxIntentos", "Requerido ", true);
        valid = false;
    } else
        appendErrorMessage("#errorMaxIntentos", "");

    if ($("#BloqUsuario").val() == "") {
        appendErrorMessage("#errorBloqUsuario", "Requerido ", true);
        valid = false;
    } else
        appendErrorMessage("#errorBloqUsuario", "");

    if ($("#ContHistoricas").val() == "") {
        appendErrorMessage("#errorContHistoricas", "Requerido ", true);
        valid = false;
    } else
        appendErrorMessage("#errorContHistoricas", "");

//    var DifUsuContra = 0;
//    if (document.getElementById('DifUsuContra').checked == true) {
//        DifUsuContra = 1;
//        valid = true;
//    } else {
//        DifUsuContra = 0;
//        valid = false;
    //    }

    var DifUsuContra = "";
    var IdSi = $("#Si").val();
    var IdNo = $("#No").val();

    if (document.getElementById('Si').checked == true) {
        DifUsuContra = IdSi
        valid = true;
    } else if (document.getElementById('No').checked == true) {
        DifUsuContra = IdNo
        valid = true;
    } else {
        valid = false;
    }

    var Nivel = "";
    var IdBaja = $("#Baja").val();
    var IdAlta = $("#Alta").val();

    if (document.getElementById('Baja').checked == true) {
        Nivel = IdBaja
        valid = true;
    } else if (document.getElementById('Alta').checked == true) {
        Nivel = IdAlta
        valid = true;
    } else {
        valid = false;
    }

    if (valid) {
        var ParamUrl = $("#Url_GuardarPolitica").val();
        var LongMinContrasena = $("#LongMinContrasena").val();
        var VigenciaContrasena = $("#VigenciaContrasena").val();
        var MaxIntentos = $("#MaxIntentos").val();
        var BloqUsuario = $("#BloqUsuario").val();
        var ContHistoricas = $("#ContHistoricas").val();

        $.ajax({
            url: ParamUrl,
            data: { Long: LongMinContrasena, Vigencia: VigenciaContrasena, Diferencia: DifUsuContra, NroMaximo: MaxIntentos, DiasBloq: BloqUsuario, Compleji: Nivel, CantContrHis: ContHistoricas },
            type: "get",
            cache: false,
            success: function (data, textStatus, jqXHR) {
                
                var result = data.split("/");
                if (result[0] == 1) {
                    alert(result[1]);
                } else
                    alert(result[1]);
            },
            error: function (jqXHR, status, error) {
            },
            complete: function () {
            }
        });
    }
}

function appendErrorMessage(selector, message, visible) {
    message = "<span>" + message + "</span>";
    $(selector).html(message);
    if (visible == true) {
        $(selector).css('display', '');
    }
    else {
        $(selector).css('display', 'none');
    }
}

function GetSelectedItem(Alta, Baja) {
    if (document.getElementById(Alta).checked) {
        document.getElementById(Baja).checked = false;
    }
    else if (document.getElementById(Baja).checked) {
        document.getElementById(Alta).checked = false;
    }
}