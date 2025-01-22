using System;
using System.Web;
using System.Web.Security;
using Komatsu.Core.Seguridad.ServicioUsuario;
using System.DirectoryServices;
using System.Configuration;
using Komatsu.Core.Seguridad.Provider.Helper;

namespace Komatsu.Core.Seguridad.Provider
{
    public class KomatsuMembershipProvider : MembershipProvider
    {
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            try
            {
                Usuario oUsuario = null;
                int Modo = 0;

                using (ServicioUsuarioClient client = new ServicioUsuarioClient())
                {
                    Modo = client.ModoUsuario(username);
                }

                if (Modo == 1 || Modo == 0)
                {
                    DirectoryEntry entry = new DirectoryEntry(ConfigurationManager.AppSettings["PathDomain"], username, password);
                    //Object obj = entry.NativeObject;

                    using (ServicioUsuarioClient client = new ServicioUsuarioClient())
                    {
                        Usuario usuario = new Usuario
                        {
                            UserName = username,
                            Password = password
                        };

                        oUsuario = client.AuthenticateUser(usuario);
                    }
                }
                else if (Modo == 2) {

                    using (ServicioUsuarioClient client = new ServicioUsuarioClient())
                    {
                        Usuario usuario = new Usuario
                        {
                            UserName = username
                        };
                        oUsuario = client.AutenticarUsuario_Modo(usuario, password); 
                        //oUsuario = client.AutenticarUsuario_Modo(usuario, StringCipher.Encrypt(password)); 
                        
                    }
                }
                else if (Modo == 5)
                {
                    HttpContext.Current.Session["Modo"] = Modo;
                    return false;
                }

                if (Modo == 2) {
                    HttpContext.Current.Session["Modo"] = Modo;
                }

                if (oUsuario != null && oUsuario.IdUsuario > 0)
                {
                    DataContracts.Usuario DCUsuario = new DataContracts.Usuario()
                    {
                        IdUsuario = oUsuario.IdUsuario,
                        UserName = oUsuario.UserName,
                        NombreApellido = oUsuario.NombreApellido,
                        CodigoEmp = oUsuario.CodigoEmp,
                        EmailCoorporativo = oUsuario.EmailCoorporativo,
                        DNI = oUsuario.DNI,
                        Sexo = oUsuario.Sexo,
                        Empresa = new DataContracts.Empresa() 
                        {
                            CodigoEmpresa = oUsuario.Empresa.CodigoEmpresa,
                            ContentStyle = oUsuario.Empresa.ContentStyle
                        },
                        ListaRol = new DataContracts.ListaRol(),
                        IdSociedad = oUsuario.IdSociedad,
                        SociedadDescripcionCorta = oUsuario.SociedadDescripcionCorta,
                        IdAreaBU = oUsuario.IdAreaBU,
                        AreaBUDescripcionCorta = oUsuario.AreaBUDescripcionCorta,
                        IdCargo = oUsuario.IdCargo,
                        CargoDescripcionCorta = oUsuario.CargoDescripcionCorta,
                        IdComite = oUsuario.IdComite
                    };

                    foreach (var oRol in oUsuario.ListaRol)
                    {
                        var Rol = new DataContracts.Rol();
                        Rol.IdRol = oRol.IdRol;
                        Rol.Nombre = oRol.Nombre;
                        Rol.SiRango = oRol.SiRango;
                        Rol.SiSuperAdmi = oRol.SiSuperAdmi;
                        Rol.FechaInicio = oRol.FechaInicio;
                        Rol.FechaFin = oRol.FechaFin;
                        Rol.Aplicacion = new DataContracts.Aplicacion();
                        Rol.Aplicacion.IdAplicacion = oRol.Aplicacion.IdAplicacion;
                        DCUsuario.ListaRol.Add(Rol);
                    }

                    if (DCUsuario.IdUsuario != 0)
                    {
                        HttpContext.Current.Session["Usuario"] = DCUsuario;
                        HttpContext.Current.Session["IdUsuario"] = DCUsuario.IdUsuario;
                        HttpContext.Current.Session["Caduco"] = DCUsuario.IdComite;
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
