primeraVez = true;

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
            }
        },
//        beforeSend: function () {
//            if (IdEmpresa == "") {
//                $("#IdEmpresa").val() = 0;
//            }
//        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
            BuscarRolesPorAplicacion();
        }
    });
}


function BuscarRolesPorAplicacion() {
    var ParamUrl = $("#Url_RolesPorAplicacion").val();
    var IdAplicacion = $("#IdAplicacion").val();
    var IdEmpresa = $("#IdEmpresa").val();

    $.ajax({
        url: ParamUrl,
        data: { IdAplicacion: IdAplicacion },
        type: "post",
        cache: false,
        beforeSend: function () {

    
            },
        success: function (data, textStatus, jqXHR) {
            $("#Div_Roles").html(data);
        },
        error: function (jqXHR, status, error) {
        },
        complete: function () {
         
        }
    });
}

function CargarTreeView(IdRol) {
    var ParamUrl = $("#Url_TreeViewJson").val();
    var IdEmpresa = $("#IdEmpresa").val();

    if (primeraVez) {
        $('#tree').aciTree({
            ajax: {
                url: ParamUrl,
                data: { IdRol: IdRol, IdEmpresa: IdEmpresa },
                type: "post",
                cache: false
            },
            checkbox: true,
            checkboxName: 'checkbox1[]'
        });
    }
    else {
        $('#tree').aciTree('api').destroy({
            uid: 'user-defined',
            success: function () {
                $('#tree').aciTree({
                    ajax: {
                        url: ParamUrl,
                        data: { IdRol: IdRol, IdEmpresa: IdEmpresa },
                        type: "post",
                        cache: false
                    },
                    checkbox: true,
                    checkboxName: 'checkbox1[]'
                });
            },
            fail: function () {
                alert('Espere a que carge el arbol...vuelva a intentar');
                //alert('Error al seleccionar el rol!');
            }
        });
    }

    primeraVez = false;
}

function Enviar() {
    
    var treeApi = $('#tree').aciTree('api');
    var checked = treeApi.checkboxes(treeApi.children(null, true, true), true);

    var listaPermisoPerfil = [];
    checked.each(function () {
        
        var thisobject = $(this);
        var level = treeApi.level(thisobject);

        if (level == 2) {
            var parents = treeApi.path(thisobject, false);
            var Id = treeApi.getId(thisobject);
            var Label = treeApi.getLabel(thisobject);

            var permisoPerfil = {};
            permisoPerfil["IdEmpresa"] = $("#IdEmpresa").val();
            permisoPerfil["IdAplicacion"] = $("#IdAplicacion").val();
            permisoPerfil["IdAccion"] = Id;
            permisoPerfil["IdRol"] = $("#contenedor-grid-Rol input:checked").attr("id");

            parents.each(function () {
                var thisobject = $(this);
                var Id = treeApi.getId(thisobject);
                var Label = treeApi.getLabel(thisobject);
                var level = treeApi.level(thisobject);

                switch (level) {
                    case 0:
                        permisoPerfil["IdModulo"] = Id;
                        break;
                    case 1:
                        permisoPerfil["IdPagina"] = Id;
                        break;
                    default:
                }
            });
            listaPermisoPerfil.push(permisoPerfil);
        }
    });
    var ParamUrl = $("#Url_AsignarPerfil").val();

    $.ajax({
        url: ParamUrl,
        data: $.toJSON(listaPermisoPerfil),
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            alert(data);
        }
    });
}