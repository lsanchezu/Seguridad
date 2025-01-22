﻿using System;
using System.Net;
using System.Security.Principal;

namespace Komatsu.Core.Seguridad.Mvc.AppCode
{

        [Serializable()]
        public class CustomReportCredentials : Microsoft.Reporting.WebForms.IReportServerCredentials
        {

            private string _UserName;
            private string _PassWord;
            private string _DomainName;

            public CustomReportCredentials(string UserName, string PassWord, string DomainName)
            {
                _UserName = UserName;
                _PassWord = PassWord;
                _DomainName = DomainName;
            }

            public WindowsIdentity ImpersonationUser
            {
                get
                {
                    return null;
                }
            }

            public ICredentials NetworkCredentials
            {
                get
                {
                    return new NetworkCredential(_UserName, _PassWord, _DomainName);
                }
            }

            public bool GetFormsCredentials(out Cookie authCookie, out string user, out string password, out string authority)
            {
                authCookie = null;
                user = password = authority = null;
                return false;
            }

        }
    }
