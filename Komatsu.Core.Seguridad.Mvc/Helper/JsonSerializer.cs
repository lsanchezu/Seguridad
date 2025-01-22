using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;

namespace Komatsu.Core.Seguridad.Mvc.Helper
{
    internal static class JsonSerializer
    {
        ///
        /// Método extensor para serializar un string a JSON
        ///
        public static string SerializeToJson(this object objeto)
        {
            var jsonSerializer = new DataContractJsonSerializer(objeto.GetType());
            var ms = new MemoryStream();
            jsonSerializer.WriteObject(ms, objeto);
            var jsonResult = Encoding.Default.GetString(ms.ToArray());
            return jsonResult;
        }

        public static object SerializeToJsonObj(this object objeto)
        {

            var jsonSerializer = new DataContractJsonSerializer(objeto.GetType());
            var ms = new MemoryStream();
            jsonSerializer.WriteObject(ms, objeto);
            
            return ms.ToArray();
        }

        ///
        /// Método extensor para deserializar JSON cualquier objeto
        ///
        public static T DeserializeJsonTo<T>(string jsonSerializado)
        {
            var jss = new JavaScriptSerializer();
            T entidad = jss.Deserialize<T>(jsonSerializado);
            return entidad;
        }
    }
}
