using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class LineasCreditoVentasDatos
    {
        SFacturacionEntities modelDB = new SFacturacionEntities();
        ObjectParameter resultado = new ObjectParameter("resultado", typeof(bool));
        ObjectParameter lineaCreditoVentaID = new ObjectParameter("LineaCreditoVentaID", typeof(int));

        public Tuple<bool, int> InsertarLineaCreditoVenta(LineasCreditoVenta lineaCreditoVenta)
        {
            modelDB.proc_InsertarLineaCreditoVenta(lineaCreditoVentaID, lineaCreditoVenta.FacturaID, lineaCreditoVenta.Estatus, resultado);

            return Tuple.Create((bool)resultado.Value, (int)lineaCreditoVentaID.Value);
        }

        public ObjectResult<proc_CargarTodasLineasCreditoVentas_Result> CargarTodasLineasCreditoVentas(int indicePagina, int tamanoPagina, string filtro, string columna)
        {
            var result = modelDB.proc_CargarTodasLineasCreditoVentas(indicePagina, tamanoPagina, filtro, columna);

            return result;
        }
      
        
        public bool ActualizarLineaCreditoVenta(int lineaCreditoVentaID, bool estatus)
        {
            modelDB.proc_ActualizarLineaCreditoVenta(lineaCreditoVentaID, estatus, resultado);
            return (bool)resultado.Value;
        }

        public int BuscarLineaDeCreditoVentaIDFactura(int facturaID)
        {
            modelDB.proc_BuscarLineaDeCreditoVentaIDFactura(facturaID,lineaCreditoVentaID);

                return (int)lineaCreditoVentaID.Value;
        }      
    }
}
