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
    using System.Collections.Generic;
    
    public partial class Movimiento
    {
        public int MovimientoID { get; set; }
        public int ProductoID { get; set; }
        public System.DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; }
        public Nullable<int> Referencia { get; set; }
        public decimal Existencia { get; set; }
        public decimal Cantidad { get; set; }
        public int UsuarioID { get; set; }
    }
}
