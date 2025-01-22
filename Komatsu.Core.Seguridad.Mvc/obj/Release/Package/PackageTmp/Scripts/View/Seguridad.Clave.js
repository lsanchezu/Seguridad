
//var valid = true;
//appendErrorMessage("#errorIdUsuario", "");
//appendErrorMessage("#errorIdCodEmpleado", "");
//appendErrorMessage("#errorIdNombre", "");

//var Contrasena2 = $("#IdClave").val();

//if (Contrasena2.length < Longitud) {
//    appendErrorMessage("#errorContrasena", "Mínimo de caracteres " + Longitud, true);
//    valid = false;
//}

$(function () {
    $("#IdClave").keyup(function (event) {
        if (event.keyCode == 13) {
            var ParamUrl = $("#Url_ValidarClave").val();
            var Clave = $("#IdClave").val();
            appendErrorMessage("#errorIdClave", "");
            appendErrorMessage("#errorIdClave2", "");

            $.ajax({
                url: ParamUrl,
                data: { Contrasena: Clave },
                type: "get",
                cache: false,
                success: function (data, textStatus, jqXHR) {
                    var result = data.split("/");
                    if (result[0] == 2) {
                        appendErrorMessage("#errorIdClave", "Clave Incorrecta", true);
                        document.getElementById('IdNuevo').disabled = true;
                        document.getElementById('IdNuevo2').disabled = true;
                    } else
                        appendErrorMessage("#errorIdClave2", "Correcto", true);
                        document.getElementById('IdNuevo').disabled = false;
                        document.getElementById('IdNuevo2').disabled = false;
                },
                error: function (jqXHR, status, error) {
                },
                complete: function () {

                }
            });
        }
    });
});

function CambiarClave() {
    var valid = true;
    appendErrorMessage("#errorIdClave", "");
    appendErrorMessage("#errorIdNuevo", "");
    appendErrorMessage("#errorIdNuevo2", "");

    if ($("#IdClave").val() == "") {
        appendErrorMessage("#errorIdClave", "Requerido ", true);
        valid = false;
    } else
        appendErrorMessage("#errorIdClave", "");

    if ($("#IdNuevo").val() == "") {
        appendErrorMessage("#errorIdNuevo", "Requerido ", true);
        valid = false;
    } else
        appendErrorMessage("#errorIdNuevo", "");

    if ($("#IdNuevo2").val() == "") {
        appendErrorMessage("#errorIdNuevo2", "Requerido ", true);
        valid = false;
    } else
        appendErrorMessage("#errorIdNuevo2", "");

    var ContrasenaN = $("#IdNuevo").val();
    var Contrasena2 = $("#IdNuevo2").val();
    var Longitud = $("#Hdn_Longitud").val();
    var NivelComplejidad = $("#Hdn_Complejidad").val();

    if (ContrasenaN.length < Longitud) {
        appendErrorMessage("#errorIdNuevo", "Mínimo de caracteres " + Longitud, true);
        valid = false;

    } else {
        if (ContrasenaN != Contrasena2) {
            appendErrorMessage("#errorIdNuevo2", "Las contraseñas no coiciden", true);
            valid = false;
        } else {
            if (NivelComplejidad == 1) {
                if (SoloLetras(ContrasenaN) == false) {
                    appendErrorMessage("#errorIdNuevo", "La contraseña debe contener solo letras", true);
                    valid = false;
                } else {
                    appendErrorMessage("#errorIdNuevo", "");
                }

            } else if (NivelComplejidad == 2) {
                if (soloLetrasYNum(ContrasenaN) == -1) {
                    appendErrorMessage("#errorIdNuevo", "Nivel de seguridad baja: Contraseña debe contener letras y números", true);
                    valid = false;
                } else {
                    appendErrorMessage("#errorIdNuevo", "");
                }

            } else {
                appendErrorMessage("#errorIdNuevo", "");
            }
        }

    }
    if (valid) {
        var ParamUrl = $("#Url_CambiarClave").val();

        $.ajax({
            url: ParamUrl,
            data: { Contrasena: ContrasenaN },
            type: "get",
            cache: false,
            success: function (data, textStatus, jqXHR) {
                var result = data.split("/");
                if (result[0] == 1) {
                    Limpiar();
                    alert(result[1]);
                }else if (result[0] == 2) {
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

function Limpiar() {
    $("#IdClave").val('');
    $("#IdNuevo").val('');
    $("#IdNuevo2").val('');
    appendErrorMessage("#errorIdClave", "");
    appendErrorMessage("#errorIdNuevo", "");
    appendErrorMessage("#errorIdNuevo2", "");
    appendErrorMessage("#errorIdClave2", "");
}
function SoloLetras(cadena) {
    if (cadena.match(/^[a-zA-Z]+$/)) {
        return true;
    }
    else {
        return false;
    }
}

function soloLetrasYNum(cadena) {
    var numeros = "0123456789";
    var letras = "abcdefghyjklmnñopqrstuvwxyz";
    var cont = 0;
    var cont2 = 0;

    cadena = cadena.toLowerCase();
    for (i = 0; i < cadena.length; i++) {
        if (letras.indexOf(cadena.charAt(i), 0) != -1) {
            cont = cont + 1;
        }
    }

    if (cont == 0) {
        return -1;
    } else {
        for (i = 0; i < cadena.length; i++) {
            if (numeros.indexOf(cadena.charAt(i), 0) != -1) {
                cont2 = cont2 + 1;
            }
        }
    }

    if (cont2 == 0) {
        return -1;
    } else {
        return 0;
    }
}