using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class PagosCompraCreditoNegocio
    {
        PagosCompraCreditoDatos pagosCompraCreditoDatos = new PagosCompraCreditoDatos();
        public Tuple<bool, int> InsertarPagoCompraCredito(PagosComprasCredito pagoCompraCredito)
        {
            return pagosCompraCreditoDatos.InsertarPagoCompraCredito(pagoCompraCredito);
        }

        public ObjectResult<proc_CargarPagosCompraCredito_Result> CargarPagosCompraCredito(int lineaCompraCreditoID)
        {   
            return pagosCompraCreditoDatos.CargarPagosCompraCredito(lineaCompraCreditoID);
        }
        public bool BorrarPagoCompraCredito(int pagoCompraCreditoID)
        {
            return pagosCompraCreditoDatos.BorrarPagoCompraCredito(pagoCompraCreditoID);
        }
    }
}
