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
	/// Data Contract Class - AplicacionPaginacion
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "http://komatsu.core.seguridad.model/2013/dckomatsuseguridad", Name = "AplicacionPaginacion")]
	public partial class AplicacionPaginacion 
	{
		private Paginacion paginacion;
		private ListaAplicacion listaAplicacion;
		
		[WcfSerialization::DataMember(Name = "Paginacion", IsRequired = false, Order = 0)]
		public Paginacion Paginacion
		{
		  get { return paginacion; }
		  set { paginacion = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "ListaAplicacion", IsRequired = false, Order = 1)]
		public ListaAplicacion ListaAplicacion
		{
		  get { return listaAplicacion; }
		  set { listaAplicacion = value; }
		}				
	}
}

