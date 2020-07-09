using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CobrosVentaCreditoDatos
    {
        SFacturacionEntities modelDB = new SFacturacionEntities();
        ObjectParameter resultado = new ObjectParameter("resultado", typeof(bool));
        ObjectParameter cobroVentaCreditoID = new ObjectParameter("CobroVentaCreditoID", typeof(int));

        public Tuple<bool, int> InsertarCobroVentaCredito(CobrosVentasCredito cobrosVentasCredito)
        {
            modelDB.proc_InsertarCobroVentaCredito(cobroVentaCreditoID, cobrosVentasCredito.LineaCreditoVentaID, cobrosVentasCredito.FechaCobro, cobrosVentasCredito.Monto, cobrosVentasCredito.UserID, cobrosVentasCredito.Concepto,resultado);

            return Tuple.Create((bool)resultado.Value, (int)cobroVentaCreditoID.Value);
        }

        public ObjectResult<proc_CargarCobrosVentaCredito_Result> CargarCobrosVentaCredito(int lineaVentaCreditoID)
        {
            var result = modelDB.proc_CargarCobrosVentaCredito(lineaVentaCreditoID);

            return result;
        }
        public bool BorrarCobroVentaCredito(int cobroVentaCreditoID)
        {
            modelDB.proc_BorrarCobroVentaCredito(cobroVentaCreditoID, resultado);

            return (bool)resultado.Value;
        }

        public ObjectResult<proc_ComprobantePagoLineaCreditoVenta_Result> ComprobantePagoLineaCreditoVenta(int cobrosVentasCreditoID)
        {
            var result = modelDB.proc_ComprobantePagoLineaCreditoVenta(cobrosVentasCreditoID);

            return result;
        }              
    }
}
