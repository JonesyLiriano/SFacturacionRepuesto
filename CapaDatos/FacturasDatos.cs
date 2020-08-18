using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class FacturasDatos
    {
        SFacturacionEntities modelDB = new SFacturacionEntities();
        ObjectParameter resultado = new ObjectParameter("resultado", typeof(bool));
        ObjectParameter facturaID = new ObjectParameter("FacturaID", typeof(int));
        ObjectParameter fecha = new ObjectParameter("Fecha", typeof(DateTime));
        ObjectParameter descuentoCliente = new ObjectParameter("DescuentoCliente", typeof(decimal));
        

        public Tuple<bool, int> InsertarFactura(Factura factura)
        {
            modelDB.proc_InsertarFactura(facturaID, factura.ClienteID, factura.Fecha, factura.TipoPagoID, factura.TipoFacturaID,factura.NCF, factura.FechaVencimiento, factura.UserID, factura.RNC, factura.Entidad, factura.DescuentoCliente, factura.CotizacionID, resultado);

            return Tuple.Create((bool)resultado.Value, (int)facturaID.Value);
        }
       

        public ObjectResult<proc_CargarMontoFacturaNC_Result> CargarMontoFacturaNC(int facturaID)
        {
            var result = modelDB.proc_CargarMontoFacturaNC(facturaID);
            return result;
        }

        public ObjectResult<proc_CargarFacturasPendientes_Result> CargarFacturasPendiente(int clienteID)
        {
            var result = modelDB.proc_CargarFacturasPendientes(clienteID);
            return result;
        }


        public ObjectResult<proc_CargarProductosFactura_Result> CargarProductosFactura(int facturaID)
        {
            var result = modelDB.proc_CargarProductosFactura(facturaID);
            return result;
        }       

        public ObjectResult<proc_CargarTodasFacturas_Result> CargarTodasFacturas()
        {
            var result = modelDB.proc_CargarTodasFacturas();
            return result;
        }
       
        public ObjectResult<proc_ComprobanteFacturaVenta_Result> CargarComprobanteFacturaVenta(int facturaID)
        {
            var result = modelDB.proc_ComprobanteFacturaVenta(facturaID);
            return result;
        }

        public ObjectResult<proc_CargarFacturasPFecha_Result> CargarFacturasPFecha(DateTime fInicial, DateTime fFinal)
        {
            var result = modelDB.proc_CargarFacturasPFecha(fInicial, fFinal);
            return result;
        }
        public ObjectResult<proc_CargarFacturasCFinalPFecha_Result> CargarFacturasCFinalPFecha(DateTime fInicial, DateTime fFinal)
        {
            var result = modelDB.proc_CargarFacturasCFinalPFecha(fInicial, fFinal);
            return result;
        }
        public ObjectResult<proc_CargarFacturasCFiscalPFecha_Result> CargarFacturasCFiscalPFecha(DateTime fInicial, DateTime fFinal)
        {
            var result = modelDB.proc_CargarFacturasCFiscalPFecha(fInicial, fFinal);
            return result;
        }
        public ObjectResult<proc_CargarFacturasCGubernamentalPFecha_Result> CargarFacturasCGubernamentalPFecha(DateTime fInicial, DateTime fFinal)
        {
            var result = modelDB.proc_CargarFacturasCGubernamentalPFecha(fInicial, fFinal);
            return result;
        }
        public ObjectResult<proc_CargarFacturasRapidaPFecha_Result> CargarFacturasRapidaPFecha(DateTime fInicial, DateTime fFinal)
        {
            var result = modelDB.proc_CargarFacturasRapidaPFecha(fInicial, fFinal);
            return result;
        }
    }
}
