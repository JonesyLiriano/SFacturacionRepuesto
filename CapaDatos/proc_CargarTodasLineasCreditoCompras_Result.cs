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
    
    public partial class proc_CargarTodasLineasCreditoCompras_Result
    {
        public Nullable<long> RowNumber { get; set; }
        public int LineaCreditoCompraID { get; set; }
        public string Proveedor { get; set; }
        public int OrdenCompra { get; set; }
        public System.DateTime Fecha { get; set; }
        public int FacturaCompra { get; set; }
        public Nullable<decimal> MontoFactura { get; set; }
        public Nullable<decimal> BalancePendiente { get; set; }
        public bool Completado { get; set; }
    }
}
