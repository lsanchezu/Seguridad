var coperation = null;

$(function () {
    SetTotalRegistrosUsuario();
    InicioJPopUpConfirm_R("#dialog", 400, false, "Mensaje de Confirmación", Redireccionar);
});

function InicioJPopUpConfirm_R(selector, ancho, resize, titulo, actionfunction) {
    $(selector).dialog({
        autoOpen: false,
        width: ancho,
        modal: true,
        resizable: resize,
        hide: 'fade',
        show: 'fade',
        title: "Mensaje de Confirmación",
        buttons: [{
            text: "Si",
            "id": "btnOk",
            click: function () {
                if (actionfunction != null) {
                    actionfunction();
                }
                $(selector).dialog("close");
            },

        }, {
            text: "No",
            click: function () {
                $(selector).dialog("close");
            },
        }],
    });
}

function Redireccionar() {
    window.location = UrlAction.UrlPerfil
}

function toUpper(control) {

    if (/[a-z]/.test(control.value)) {
        control.value = control.value.toUpperCase();
    }
}

//Consultas!
function BuscarUsuario() {
    //url
    var ParamUrl = $("#Url_Consultar_Usuario").val();
    var NombreApellido = $("#NombreApellido").val();
    var Usuario = $("#UserName").val();
    var IdUsuarioTipo = $("#IdUsuarioTipo").val();
    var IdEmpresa = $("#IdEmpresa").val();
    var IdAplicacion = $("#IdAplicacion").val();
    var IdRol = $("#IdRol").val();
    //server request
    $.ajax({
        url: ParamUrl,
        data: {
            NombreApellido: NombreApellido,
            Usuario: Usuario,
            IdUsuarioTipo: IdUsuarioTipo,
            IdEmpresa: IdEmpresa,
            IdAplicacion: IdAplicacion,
            IdRol: IdRol
        },
        type: "get",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#Id_Consultar_Usuario_Grilla").html(data);
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            SetTotalRegistrosUsuario();
        }
    });
}

function SetTotalRegistrosUsuario() {

    $("#GrillaConsultaUsuario tfoot tr a, #GrillaConsultaUsuario thead tr a").on("click", function (e) {

        var NombreApellido = $("#NombreApellido").val();
        var Usuario = $("#UserName").val();
        var IdUsuarioTipo = $("#IdUsuarioTipo").val();
        var IdEmpresa = $("#IdEmpresa").val()
        var IdAplicacion = $("#IdAplicacion").val()
        var IdRol = $("#IdRol").val()

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
            "&NombreApellido=" + NombreApellido +
            "&Usuario=" + Usuario +
            "&IdUsuarioTipo=" + IdUsuarioTipo +
            "&IdEmpresa=" + IdEmpresa +
            "&IdAplicacion=" + IdAplicacion +
            "&IdRol=" + IdRol +

            "&sortdir=" + sortdir +
            "&sort=" + sort +
            "&page=" + page;
        $(this).attr('href', newurl);
    });

    $("#GrillaConsultaUsuario tfoot tr:last td").prepend("<a id='tfootPage'  class='total_registros'></a>");
    $("#tfootPage").html($('#FooterDesc').val());
}

function NuevoUsuario() {
    var ParamUrl = $("#Url_NuevoUsuario").val();

    $.ajax({
        url: ParamUrl,
        data: {},
        type: "get",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#Nuevo_Usuario").html(data);
            $("#UsuarioTercero").css('display', 'None');
            InicioJPopUpOpen('#Nuevo_Usuario');
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
        }
    });
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

function GuardarUsuario() {
    var valid = true;
    appendErrorMessage("#errorIdUsuario", "");
    appendErrorMessage("#errorIdNombre", "");
    appendErrorMessage("#errorIdUsuarioTipo2", "");
    appendErrorMessage("#errorIdEmpresa", "");
    appendErrorMessage("#errorIdEmail", "");
    appendErrorMessage("#errorIdDireccion", "");
    appendErrorMessage("#errorIdDNI", "");
    appendErrorMessage("#errorContrasena", "");
    appendErrorMessage("#RerrorContrasena", "");
    appendErrorMessage("#errorEmpresaTerceros", "");

    if ($("#IdUsuario").val() == "") {
        appendErrorMessage("#errorIdUsuario", "Requerido ", true);
        valid = false;
    } else
        appendErrorMessage("#errorIdUsuario", "");

    if ($("#IdNombre").val() == "") {
        appendErrorMessage("#errorIdNombre", "Requerido ", true);
        valid = false;
    } else
        appendErrorMessage("#errorIdNombre", "");

    if ($("#IdUsuarioTipo2").val() == "") {
        appendErrorMessage("#errorIdUsuarioTipo2", "Requerido ", true);
        valid = false;
    } else
        appendErrorMessage("#errorIdUsuarioTipo2", "");

    if ($("#IdEmpresa2").val() == "") {
        appendErrorMessage("#errorIdEmpresa", "Requerido ", true);
        valid = false;
    } else
        appendErrorMessage("#errorIdEmpresa", "");

    if ($("#IdEmail").val() == "") {
        appendErrorMessage("#errorIdEmail", "Requerido ", true);
        valid = false;
    } else
        appendErrorMessage("#errorIdEmail", "");

    if ($("#IdDireccion").val() == "") {
        appendErrorMessage("#errorIdDireccion", "Requerido ", true);
        valid = false;
    } else
        appendErrorMessage("#errorIdDireccion", "");

    if ($("#IdDNI").val() == "") {
        appendErrorMessage("#errorIdDNI", "Requerido ", true);
        valid = false;
    } else
        appendErrorMessage("#errorIdDNI", "");

    var Modo = "";
    var Contrasena2 = "";
    var IdDirectorio = $("#Directorio").val();
    var IdNormal = $("#Normal").val();

    if (document.getElementById('Directorio').checked == true) {
        Modo = IdDirectorio
        //        valid = true;
    } else if (document.getElementById('Normal').checked == true) {
        Contrasena2 = $("#Contrasena").val();
        Modo = IdNormal
        //        valid = true;
    } else if (document.getElementById('Externo').checked == true) {
        Contrasena2 = $("#Contrasena").val();
        Modo = $("#Externo").val();
    }
    else {
        valid = false;
    }

    var UserName2 = $("#IdUsuario").val();
    var Longitud = $("#Hdn_Longitud").val();
    var Diferencia = $("#Hdn_Diferencia").val();
    var NivelComplejidad = $("#Hdn_Complejidad").val();
    var EmpresaRelacionada = 0;

    if (Modo == "2" || Modo == "3") {
        if (Contrasena2.length < Longitud) {
            appendErrorMessage("#errorContrasena", "Mínimo de caracteres " + Longitud, true);
            valid = false;

        }
        else {
            if (Diferencia == 1) {
                if (Contrasena2 == UserName2) {
                    appendErrorMessage("#errorContrasena", "La contraseña debe ser diferente al usuario", true);
                    valid = false;
                } else {

                    if (NivelComplejidad == 1) {
                        if (SoloLetras(Contrasena2) == false) {
                            appendErrorMessage("#errorContrasena", "La contraseña debe contener solo letras", true);
                            valid = false;
                        } else {
                            appendErrorMessage("#errorContrasena", "");
                        }

                    } else if (NivelComplejidad == 2) {
                        if (soloLetrasYNum(Contrasena2) == -1) {
                            appendErrorMessage("#errorContrasena", "Nivel de seguridad baja: Contraseña debe contener letras y números", true);
                            valid = false;
                        } else {
                            appendErrorMessage("#errorContrasena", "");
                        }

                    } else {
                        appendErrorMessage("#errorContrasena", "");
                    }
                }

            } else {
                appendErrorMessage("#errorContrasena", "");
            }
        }

        if (valid)
            if ($("#RContrasena").val() != Contrasena2) {
                appendErrorMessage("#RerrorContrasena", "Las contraseñas no coinciden", true);
                valid = false;
            } else
                appendErrorMessage("#errorContrasena", "");

        if (Modo == "3") {
            if ($("#IdEmpresaRelacionada").val() == '' || $("#IdEmpresaRelacionada").val() == "0") {
                valid = false;
                appendErrorMessage("#errorEmpresaTerceros", "Requerido", true);
            }
            else {
                EmpresaRelacionada = $("#IdEmpresaRelacionada").val();
                appendErrorMessage("#errorEmpresaTerceros", "");
            }
        }
    }

    if (valid) {
        var ParamUrl = $("#Url_GuardarUsuario").val();
        var UserName = $("#IdUsuario").val();
        var CodEmpleado = "";
        var Nombre = $("#IdNombre").val();
        var TipoU = $("#IdUsuarioTipo2").val();
        var Empresa2 = $("#IdEmpresa2").val();
        var Sexo2 = $("#IdEstado2").val();
        var Email = $("#IdEmail").val();
        var Dire2 = $("#IdDireccion").val();
        var DNI2 = $("#IdDNI").val();
        var IdEstado = $("input:radio[name='r2']:checked").val();
        var IdEmpresaRelacionada = EmpresaRelacionada;

        $.ajax({
            url: ParamUrl,
            data: {
                IdUsuario: UserName,
                CodEmp: CodEmpleado,
                Nombres: Nombre,
                Tipo: TipoU,
                Empresa: Empresa2,
                Sexo: Sexo2,
                email: Email,
                dire: Dire2,
                dni: DNI2,
                ModoUsu: Modo,
                Contrasena: Contrasena2,
                IdEstado: IdEstado,
                IdEmpresaRelacionada: IdEmpresaRelacionada
            },
            type: "get",
            cache: false,
            success: function (data, textStatus, jqXHR) {
                var result = data.split("/");
                if (result[0] == 1) {

                    $('#buttonAlert').html(result[1]);
                    $("#dialogAlert").dialog({
                        autoOpen: false,
                        resizable: false,
                        closeOnEscape: false,
                        open: function (event, ui) { $(".ui-dialog-titlebar-close", this.parentNode).hide(); },
                        width: 400,
                        modal: true,
                        title: "Mensaje de Validación",
                        buttons: [{
                            id: "btnPopAceptar",
                            text: "Aceptar",
                            click: function () {
                                if (null != null)
                                    null();
                                $(this).dialog("close");
                                $("#Nuevo_Usuario").dialog("close");
                                InicioJPopUpConfirmOpen('#dialog');
                            }
                        }]

                    });
                    $('#dialogAlert').dialog('option', 'modal', true).dialog('open');

                } else {
                    alert(result[1]);
                }

            },
            error: function (jqXHR, status, error) {
            },
            complete: function () {
                BuscarUsuario();
            }
        });
    }

}


function SoloLetras(cadena) {
    if (cadena.match(/^[a-zA-Z]+$/)) {
        return true;
    }
    else {
        return false;
    }
}

function val_AZ(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 8) return true;
    //else if (tecla == e.keyCode || tecla == e.which) return true;
    patron = /[QWERTYUIOPASDFGHJKLÑZXCVBNMqwertyuiopñlkjhgfdsazxcvbnmáéíóúÁÉÍÓÚ\x00\s]/;
    te = String.fromCharCode(tecla);
    return patron.test(te);
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

function soloNumeros(e) {
    var keynum = window.event ? window.event.keyCode : e.which;
    if ((keynum == 8) || (keynum == 46))
        return true;

    return /\d/.test(String.fromCharCode(keynum));
}

function soloLetras2(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 32) return true;
    if (e.ctrlKey && tecla == 86) { return true; }
    if (e.ctrlKey && tecla == 67) { return true; }
    if (e.ctrlKey && tecla == 88) { return true; }

    patron = /[a-zA-Z]/;

    te = String.fromCharCode(tecla);
    return patron.test(te);
}

function BuscarAplicacionPorEmpresa() {
    var IdEmpresa = $("#IdEmpresa").val();
    if (IdEmpresa == null || IdEmpresa == "") {
        IdEmpresa = 0;
    }
    var ParamUrl = $("#Url_AplicacionPorEmpresa").val();


    $.ajax({
        url: ParamUrl,
        data: { IdEmpresa: IdEmpresa },
        type: "get",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#Div_Aplicacion").html(data);
            if (IdEmpresa == 0) {
                BuscarRolesPorAplicacion();
            }


        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            //            BuscarRolesPorAplicacion();
        }
    });
}

function BuscarRolesPorAplicacion() {
    var ParamUrl = $("#Url_RolesPorAplicacion").val();
    var IdAplicacion = $("#IdAplicacion").val();
    //      if (IdAplicacion == null || IdAplicacion == "") {
    //        IdAplicacion = 0;

    $.ajax({
        url: ParamUrl,
        data: { IdAplicacion: IdAplicacion },
        type: "get",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#Div_Rol").html(data);
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            //            if (!primeraVez) {
            //                $('#tree').aciTree('api').destroy();
            //                primeraVez = true;
            //            }
        }
    });
}

function ActivarDesactivarUsuario(IdUsuario, IdEstado, Mensaje) {
    $("#dialogConfirmacion p").text(Mensaje);
    $("#dialogConfirmacion").data('Opcion', 'Actualizar_Estado');
    $("#dialogConfirmacion").data('IdUsuario', IdUsuario);
    $("#dialogConfirmacion").data('IdEstado', IdEstado);
    $("#dialogConfirmacion").dialog('open');
}

function ActualizarEstado_Usuario(IdUsuario2, IdEstado) {
    var ParamUrl = $("#ActualizarEstado_Usuario").val();

    $.ajax({
        url: ParamUrl,
        data: { IdUsuario: IdUsuario2, IdEstado: IdEstado },
        type: "get",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#dialogResultado p").html(data.mensaje);
            $("#dialogResultado").dialog('open');
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            BuscarUsuario();
        }
    });
}

function EditarUsuario(IdUsuario2) {
    var ParamUrl = $("#Url_Editar_Usuario").val();

    $.ajax({
        url: ParamUrl,
        data: { IdUsuario: IdUsuario2 },
        type: "get",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#dialogGestionarUsuario").html(data);
            InicioJPopUpOpen('#dialogGestionarUsuario');
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
        }
    });
}

function VerRolesUsuario(IdUsuario2) {

    var ParamUrl = $("#Url_VerRoles_Usuario").val();

    $.ajax({
        url: ParamUrl,
        data: { IdUsuario: IdUsuario2 },
        type: "get",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#dialoGestionarRoles").html(data);
            InicioJPopUpOpen('#dialoGestionarRoles');
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
        }
    });
}


function DataUsuario() {
    var D = {
        IdUsuario: $("#IdUsuario1").val(),
        UserName: $("#IdUserName").val(),
        NombreApellido: $("#IdNombre2").val(),
        CodigoEmpresa: $("#IdEmpresa3").val(),
        IdUsuarioTipo: $("#IdUsuarioTipo3").val(),
        EmailCoorporativo: $("#IdEmail2").val(),
        Direccion: $("#IdDireccion2").val(),
        DNI: $("#IdDNI2").val(),
        CodEmp: "",
        FlagResetearContrasena: $("#ChkResetearContrasena").is(':checked') ? true : false
    }
    return D;
}

function Actualizar_Usuario() {
    var valid = true;
    appendErrorMessage("#errorIdUserName", "");
    appendErrorMessage("#errorIdNombre2", "");
    appendErrorMessage("#errorIdEmpresa2", "");
    appendErrorMessage("#errorIdUsuarioTipo", "");
    appendErrorMessage("#errorIdEmail2", "");
    appendErrorMessage("#errorIdDireccion2", "");
    appendErrorMessage("#errorIdDNI2", "");


    if ($("#IdUserName").val() == "") {
        appendErrorMessage("#errorIdUserName", "Requerido ", true);
        valid = false;
    } else
        appendErrorMessage("#errorIdUserName", "");


    if ($("#IdNombre2").val() == "") {
        appendErrorMessage("#errorIdNombre2", "Requerido ", true);
        valid = false;
    } else
        appendErrorMessage("#errorIdNombre2", "");

    if ($("#IdEmpresa3").val() == "") {
        appendErrorMessage("#errorIdEmpresa2", "Requerido ", true);
        valid = false;
    } else
        appendErrorMessage("#errorIdEmpresa2", "");

    if ($("#IdUsuarioTipo3").val() == "") {
        appendErrorMessage("#errorIdUsuarioTipo", "Requerido ", true);
        valid = false;
    } else
        appendErrorMessage("#errorIdUsuarioTipo", "");

    if ($("#IdEmail2").val() == "") {
        appendErrorMessage("#errorIdEmail2", "Requerido ", true);
        valid = false;
    } else
        appendErrorMessage("#errorIdEmail2", "");

    if ($("#IdDireccion2").val() == "") {
        appendErrorMessage("#errorIdDireccion2", "Requerido ", true);
        valid = false;
    } else
        appendErrorMessage("#errorIdDireccion2", "");

    if ($("#IdDNI2").val() == "") {
        appendErrorMessage("#errorIdDNI2", "Requerido ", true);
        valid = false;
    } else
        appendErrorMessage("#errorIdDNI2", "");
    if (valid) {
        var ParamUrl = $("#Url_Actualizar_Usuario").val();
        D = DataUsuario();

        $.ajax({
            url: ParamUrl,
            data: D,
            type: "post",
            cache: false,
            success: function (data, textStatus, jqXHR) {
                $("#dialogResultado p").html(data.mensaje);
                $("#dialogResultado").dialog('open');
                if (data.block) {
                    $("#dialogGestionarUsuario").dialog('close');
                } else {
                    $("#dialogResultado").dialog('open');
                }


            },
            error: function (jqXHR, status, error) {
                console.log(jqXHR + " - " + error);
            },
            complete: function () {
                BuscarUsuario();
            }
        });
    }
}

function Cancelar_Usuario() {
    $("#dialogGestionarUsuario").dialog('close');
}

function Cancelar_Roles() {
    $("#dialoGestionarRoles").dialog('close');
}

function CancelarUsuario() {
    $("#Nuevo_Usuario").dialog('close');
}

function GetSelectedItem2(Directorio, Normal) {
    $("#Contrasena").val('');
    $("#RContrasena").val('');
    $("#UsuarioTercero").css('display', 'none');
    $("input[name='rbtTerceros']").prop("checked", false);

    if (document.getElementById(Directorio).checked) {
        document.getElementById(Normal).checked = false;
        document.getElementById('Contrasena').disabled = true;
        document.getElementById('RContrasena').disabled = true;
    }
    else if (document.getElementById(Normal).checked) {
        document.getElementById(Directorio).checked = false;
        document.getElementById('Contrasena').disabled = false;
        document.getElementById('RContrasena').disabled = false;
    }
    else if (document.getElementById("Externo").checked) {
        document.getElementById(Directorio).checked = false;
        document.getElementById('Contrasena').disabled = false;
        document.getElementById('RContrasena').disabled = false;
        $("#UsuarioTercero").fadeIn();
        $("#Sede").prop("checked", true);
        ObtenerListaEmpresarelacionada();
    }
}

function ObtenerListaEmpresarelacionada() {
    var ParamUrl = $("#Url_ObtenerEmpresarelacionada").val();
    var tipoEmpresaRelacionada = $("input[name='rbtTerceros']:checked").val();

    $.ajax({
        url: ParamUrl,
        data: { tipoEmpresaRelacionada: tipoEmpresaRelacionada },
        type: "get",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $('#DivEmpresaRelacionada').html(data);
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () { }
    });
}

function GetArea(flag) {
    var ParamUrl = $("#Url_Get_Area").val();
    var IdUnidadOrganica = $('#IdUnidadOrganica').val();
    if (IdUnidadOrganica == "")
        IdUnidadOrganica = 0;

    $.ajax({
        url: ParamUrl,
        data: { IdUnidadOrganica: IdUnidadOrganica, flag: 0 },
        type: "get",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $('#Div_Area').html(data);
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () { }
    });
}

function GetAreaEdit() {
    var ParamUrl = $("#Url_Get_Area").val();
    var IdUnidadOrganica = $('#IdUnidadOrganicaEdit').val();
    if (IdUnidadOrganica == "")
        IdUnidadOrganica = 0;

    $.ajax({
        url: ParamUrl,
        data: { IdUnidadOrganica: IdUnidadOrganica, flag: 1 },
        type: "get",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $('#Div_AreaEdit').html(data);
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () { }
    });
}

function ValidExistContrasenaOperacion() {
    var url = $("#ValidExistContrasenaOperacion").val();
    var contrasenaoperacion = $("#IdCodEmpleado").val();
    $.ajax({
        url: url,
        data: { contrasenaoperacion: contrasenaoperacion.trim() },
        type: "GET",
        cache: false,
        success: function (data) {
            coperation = data;
            if (data == '0')
                alert('La contraseña de operación ya existe');
        }
    });
}
