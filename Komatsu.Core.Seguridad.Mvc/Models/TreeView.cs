
using System.Runtime.Serialization;
namespace Komatsu.Core.Seguridad.Mvc.Models
{
    [DataContract]
    public class TreeView
    {
        public string id { get; set; }
        public string label { get; set; }
        public string inode { get; set; }
        public string open { get; set; }
        public string checkbox { get; set; }
        public string radio { get; set; }
        public TreeView branch { get; set; }
    }
}