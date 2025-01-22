using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Komatsu.Core.Seguridad.DataContracts;
using System.Net.Security;

namespace Komatsu.Core.Seguridad.ServiceContracts
{
    [ServiceContract]
    public interface IServicioRol
    {
        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioRol/ListarRolPaginacion", ProtectionLevel = ProtectionLevel.None)]
        RolPaginacion ListarRolPaginacion(Rol rol, Paginacion paginacion);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioRol/ListarRol", ProtectionLevel = ProtectionLevel.None)]
        ListaRol ListarRol();

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioRol/Buscar_Rol", ProtectionLevel = ProtectionLevel.None)]
        Rol Buscar_Rol(Rol rol);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioRol/Insertar_Rol", ProtectionLevel = ProtectionLevel.None)]
        int Insertar_Rol(Rol rol);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioRol/Actualizar_Rol", ProtectionLevel = ProtectionLevel.None)]
        int Actualizar_Rol(Rol rol);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioRol/ActualizarEstado_Rol", ProtectionLevel = ProtectionLevel.None)]
        int ActualizarEstado_Rol(Rol rol);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioRol/RolesPorAplicacion", ProtectionLevel = ProtectionLevel.None)]
        ListaRol RolesPorAplicacion(Aplicacion aplicacion);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioRol/BuscarRolesPorUsuario", ProtectionLevel = ProtectionLevel.None)]
        ListaRol BuscarRolesPorUsuario(Usuario usuario);

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioRol/ListarAplicacion", ProtectionLevel = ProtectionLevel.None)]
        ListaAplicacion ListarAplicacion();

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioRol/ListarOperacion", ProtectionLevel = ProtectionLevel.None)]
        ListaOperacion ListarOperacion();

        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://Komatsu.Core.Seguridad.Model.ServiceContract/2013/KomatsuCoreServicioRol/ListarOperacionesPorRol", ProtectionLevel = ProtectionLevel.None)]
        ListaOperacion ListarOperacionesPorRol(int idRol);

    }
}
