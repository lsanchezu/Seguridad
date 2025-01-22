//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using WcfSerialization = global::System.Runtime.Serialization;

namespace Komatsu.Core.Seguridad.DataContracts
{
	/// <summary>
	/// Data Contract Class - Pagina
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "http://komatsu.core.seguridad.model/2013/dckomatsuseguridad", Name = "Pagina")]
	public partial class Pagina 
	{
		private int idPagina;
		private int idPaginaPadre;
		private string url;
		private string nombre;
		private string icono;
		private int nivel;
		private int orden;
		private bool visible;
		private Modulo modulo;
		private Komatsu.Core.Seguridad.DataContracts.Grupo grupo;
		private Estado estado;
        private int idAgrupacion;
        private int idTamanoMenu;
        private string descripcionAgrupacion;
		
		[WcfSerialization::DataMember(Name = "IdPagina", IsRequired = false, Order = 0)]
		public int IdPagina
		{
		  get { return idPagina; }
		  set { idPagina = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "IdPaginaPadre", IsRequired = false, Order = 1)]
		public int IdPaginaPadre
		{
		  get { return idPaginaPadre; }
		  set { idPaginaPadre = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Url", IsRequired = false, Order = 2)]
		public string Url
		{
		  get { return url; }
		  set { url = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Nombre", IsRequired = false, Order = 3)]
		public string Nombre
		{
		  get { return nombre; }
		  set { nombre = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Icono", IsRequired = false, Order = 4)]
		public string Icono
		{
		  get { return icono; }
		  set { icono = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Nivel", IsRequired = false, Order = 5)]
		public int Nivel
		{
		  get { return nivel; }
		  set { nivel = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Orden", IsRequired = false, Order = 6)]
		public int Orden
		{
		  get { return orden; }
		  set { orden = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Visible", IsRequired = false, Order = 7)]
		public bool Visible
		{
		  get { return visible; }
		  set { visible = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Modulo", IsRequired = false, Order = 8)]
		public Modulo Modulo
		{
		  get { return modulo; }
		  set { modulo = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Grupo", IsRequired = false, Order = 9)]
		public Komatsu.Core.Seguridad.DataContracts.Grupo Grupo
		{
		  get { return grupo; }
		  set { grupo = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Estado", IsRequired = false, Order = 10)]
		public Estado Estado
		{
		  get { return estado; }
		  set { estado = value; }
		}

        [WcfSerialization::DataMember(Name = "IdAgrupacion", IsRequired = false, Order = 11)]
        public int IdAgrupacion
        {
            get { return idAgrupacion; }
            set { idAgrupacion = value; }
        }
        [WcfSerialization::DataMember(Name = "IdTamanoMenu", IsRequired = false, Order = 12)]
        public int IdTamanoMenu
        {
            get { return idTamanoMenu; }
            set { idTamanoMenu = value; }
        }

        [WcfSerialization::DataMember(Name = "DescripcionAgrupacion", IsRequired = false, Order = 13)]
        public string DescripcionAgrupacion
        {
            get { return descripcionAgrupacion; }
            set { descripcionAgrupacion = value; }
        }

        
	}
}

