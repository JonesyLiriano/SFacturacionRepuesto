//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CapaDatos
{
    using System;
    
    public partial class proc_ComprobanteOrdenCompra_Result
    {
        public int ProveedorID { get; set; }
        public string NombreProveedor { get; set; }
        public string CedulaORncProveedor { get; set; }
        public string DireccionProveedor { get; set; }
        public System.DateTime FechaPedido { get; set; }
        public int ProductoID { get; set; }
        public string Descripcion { get; set; }
        public string UnidadMedida { get; set; }
        public Nullable<double> Existencia { get; set; }
        public Nullable<double> CantMin { get; set; }
        public Nullable<decimal> PrecioCompra { get; set; }
        public double Ordenada { get; set; }
        public Nullable<double> Recibida { get; set; }
        public bool Estatus { get; set; }
        public int OrdenCompraID { get; set; }
        public string Referencia { get; set; }
        public string Marca { get; set; }
        public string Calidad { get; set; }
    }
}
