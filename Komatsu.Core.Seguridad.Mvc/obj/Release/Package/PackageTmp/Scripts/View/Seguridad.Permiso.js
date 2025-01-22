/*function mostrarUsuariosPorRol() {
    var ParamUrl = $("#Url_ObtenerUsuarios").val();
    var IdRol = $("#IdRol").val();

    $("#box1Filter").val('');
    $("#box2Filter").val('');


    $.ajax({
        url: ParamUrl,
        data: { IdRol: IdRol },
        type: "post",
        cache: false, 
        success: function (data) {
            var combos = data.toString().split('<br />');
            $("#lstUsuarios2").html(combos[0]);
            $("#lstUsuarios").html(combos[1]);
        }
    });
}
*/

function BuscarAplicacionPorEmpresa() {
    var ParamUrl = $("#Url_AplicacionPorEmpresa").val();
    var IdEmpresa = $("#IdEmpresa").val();
    if (IdEmpresa == null || IdEmpresa == "") {
        IdEmpresa = 0;
    }
     $.ajax({
        url: ParamUrl,
        data: { IdEmpresa: IdEmpresa },
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#Div_Aplicacion").html(data);
            if (IdEmpresa == 0) {
                BuscarRolesPorAplicacion();
                LimpiarLstBox();
            }
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
       
        }
    });
}


function BuscarRolesPorAplicacion() {
    var ParamUrl = $("#Url_RolesPorAplicacion").val();
    var IdAplicacion = $("#IdAplicacion").val();
    if (IdAplicacion == null || IdAplicacion == "") {
        IdAplicacion = 0;
    }

    $.ajax({
        url: ParamUrl,
        data: { IdAplicacion: IdAplicacion },
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#Div_Rol").html(data);
            LimpiarLstBox();
            //if (IdAplicacion == 0) {
            //         LimpiarLstBox();
            //}
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

function guardarPerfil() {
    var ParamUrl = $("#Url_CrearPerfil").val();
    var IdRol = $("#IdRol").val();
    var Usuarios = $("#box2View option");
    var UsuariosFiltradosOcultos = $("#box2Storage option");
    UsuariosTotalesAsignados = new Array();
    

    if (IdRol == '') {
        scrollTo(0, 0);
        return false;
    }

    IdUsuarios = new Array();
 

    $(Usuarios).each(function (i) {
        IdUsuarios[i] = $(this).val();
    });
 
    IdUsuariosOcultos = new Array();
    $(UsuariosFiltradosOcultos).each(function (i) {
        IdUsuariosOcultos[i] = $(this).val();
    });

    UsuariosTotalesAsignados = IdUsuarios.concat(IdUsuariosOcultos);
    IdUsuarios = new Array();
    IdUsuarios = UsuariosTotalesAsignados;

    $.ajax({
        url: ParamUrl,
        data: {
            IdRol: IdRol,
            IdUsuarios: IdUsuarios.toString()
        },
        type: "post",
        success: function (data) {
            scrollTo(0, 0);
            var result = data.split("/");
            InicioJPopUpAlert(result[1], null);
        }
    });
}


function ClearAppRol() {

    /*var obj = document.getElementById("IdRol");
    var size = obj.options.length;

    for (i = size; i > 0; i--) {
        if (i != 0)
            obj.remove(i);
    }

    var obj1 = document.getElementById("IdAplicacion");
    var size1 = obj1.options.length;
    for (i = size1; i > 0; i--) {
        if (i != 0)
            obj1.remove(i);
    }*/

    var ParamUrl = $("#Url_AplicacionPorEmpresa").val();

    $.ajax({
        url: ParamUrl,
        data: { IdEmpresa: 0 },
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#Div_Aplicacion").html(data);
            if (IdEmpresa == 0) {
                BuscarRolesPorAplicacion();
                LimpiarLstBox();
            }
        }
    });

    var ParamUrl2 = $("#Url_RolesPorAplicacion").val();

    $.ajax({
        url: ParamUrl2,
        data: { IdAplicacion: 0 },
        type: "post",
        cache: false,
        success: function (data, textStatus, jqXHR) {
            $("#Div_Rol").html(data);
        }
    });

}

function LimpiarLstBox() {

    var ParamUrl = $("#Url_ObtenerUsuarios").val();
   
    $.configureBoxes();

    $("#box1Filter").val('');
    $("#box2Filter").val('');
    $('#box1Storage').empty();
    $('#box2Storage').empty();
    
    $("#IdRol option[value='']").attr("selected", true);
    $("#uniform-IdRol").find('span').first().html("- SELECCIONE -");
    
    $.ajax({
        url: ParamUrl,
        data: { IdRol: 0 },
        type: "post",
        success: function (data) {
            var combos = data.toString().split('<br />');
            //$("#lstUsuarios2").html(combos[0]);
            //$("#lstUsuarios").html(combos[1]);

            $("#box2View").html(combos[0]);
            $("#box1View").html(combos[1]);
        }
    });
 
}



function mostrarUsuariosPorRol() {


    var ParamUrl = $("#Url_ObtenerUsuarios").val();
    var IdRol = $("#IdRol").val();
    if (IdRol == "" || IdRol == null) {
        LimpiarLstBox();
    }
    else {
        $.configureBoxes();
        
        $("#box1Filter").val('');
        $("#box2Filter").val('');
        $('#box1Storage').empty();
        $('#box2Storage').empty();

        $.ajax({
            url: ParamUrl,
            data: { IdRol: IdRol },
            type: "post",
            success: function (data) {
                var combos = data.toString().split('<br />');
                $("#lstUsuarios2").html(combos[0]);
                $("#lstUsuarios").html(combos[1]);
            }
        });
    }
}
