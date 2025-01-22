using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Komatsu.Core.Seguridad.Mvc.Helpers
{
    public class URL
    {
        public string ObtenerURL(string id)
        {
            var url = ConfigurationManager.AppSettings[id].ToString();

            return url;
        }
    }
}