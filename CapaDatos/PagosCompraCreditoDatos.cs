using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class PagosCompraCreditoDatos
    {
        SFacturacionEntities modelDB = new SFacturacionEntities();
        ObjectParameter resultado = new ObjectParameter("resultado", typeof(bool));
        ObjectParameter pagoCompraCreditoID = new ObjectParameter("PagoCompraCreditoID", typeof(int));

        public Tuple<bool, int> InsertarPagoCompraCredito(PagosComprasCredito pagoCompraCredito)
        {
            modelDB.proc_InsertarPagoCompraCredito(pagoCompraCreditoID, pagoCompraCredito.LineaCreditoCompraID, pagoCompraCredito.FechaPago, pagoCompraCredito.Monto, pagoCompraCredito.UserID, pagoCompraCredito.Concepto, resultado);

            return Tuple.Create((bool)resultado.Value, (int)pagoCompraCreditoID.Value);
        }

        public ObjectResult<proc_CargarPagosCompraCredito_Result> CargarPagosCompraCredito(int lineaCompraCreditoID)
        {
            var result = modelDB.proc_CargarPagosCompraCredito(lineaCompraCreditoID);

            return result;
        }
     
        public bool BorrarPagoCompraCredito(int pagoCompraCreditoID)
        {
            modelDB.proc_BorrarPagoCompraCredito(pagoCompraCreditoID, resultado);

            return (bool)resultado.Value;
        }
    }
}
