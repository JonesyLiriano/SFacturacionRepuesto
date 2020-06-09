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

        public ObjectResult<proc_CargarTodasLineasCreditoVentas_Result> CargarTodasLineasCreditoVentas()
        {
            var result = modelDB.proc_CargarTodasLineasCreditoVentas();

            return result;
        }
        public ObjectResult<proc_CargarTodasLineasCreditoVentasCompletadas_Result> CargarTodasLineasCreditoVentasCompletadas()
        {
            var result = modelDB.proc_CargarTodasLineasCreditoVentasCompletadas();

            return result;
        }
        public ObjectResult<proc_CargarTodasLineasCreditoVentasPendientes_Result> CargarTodasLineasCreditoVentasPendientes()
        {
            var result = modelDB.proc_CargarTodasLineasCreditoVentasPendientes();

            return result;
        }
        public ObjectResult<proc_CargarTodasLineasCreditoVentasVencidas_Result> CargarTodasLineasCreditoVentasVencidas()
        {
            var result = modelDB.proc_CargarTodasLineasCreditoVentasVencidas();

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

        public ObjectResult<proc_CargarLineasCreditoVentasPendientesPFecha_Result> CargarLineasCreditoVentasPendientesPFecha(DateTime fInicial, DateTime fFinal)
        {
            var result = modelDB.proc_CargarLineasCreditoVentasPendientesPFecha(fInicial,fFinal);

            return result;
        }

        public ObjectResult<proc_CargarLineasCreditoVentasCompletadasPFecha_Result> CargarLineasCreditoVentasCompletadasPFecha(DateTime fInicial, DateTime fFinal)
        {
            var result = modelDB.proc_CargarLineasCreditoVentasCompletadasPFecha(fInicial, fFinal);

            return result;
        }

        public ObjectResult<proc_CargarLineasCreditoVentasVencidas30PFecha_Result> CargarLineasCreditoVentasVencidas30PFecha(DateTime fInicial, DateTime fFinal)
        {
            var result = modelDB.proc_CargarLineasCreditoVentasVencidas30PFecha(fInicial, fFinal);

            return result;
        }
    }
}
