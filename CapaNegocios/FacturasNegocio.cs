using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocios
{
    public class FacturasNegocio
    {
        FacturasDatos facturasDatos = new FacturasDatos();

        public Tuple<bool, int> InsertarFactura(Factura factura)
        {

            return facturasDatos.InsertarFactura(factura);
        }

      

        public ObjectResult<proc_CargarMontoFacturaNC_Result> CargarMontoFacturaNC(int facturaID)
        {
            return facturasDatos.CargarMontoFacturaNC(facturaID);
        }

        public ObjectResult<proc_CargarFacturasPendientes_Result> CargarFacturasPendientes(int clienteID)
        {
            return facturasDatos.CargarFacturasPendiente(clienteID);
        }
               

        public ObjectResult<proc_CargarProductosFactura_Result> CargarProductosFactura(int facturaID)
        {
            return facturasDatos.CargarProductosFactura(facturaID);
        }

        public bool ConfirmarFacturaYCliente(int facturaID, int clienteID)
        {
            return facturasDatos.ConfirmarFacturaYCliente(facturaID, clienteID);
        }

        public Tuple<DateTime?, decimal?> BuscarFechaFacturaYDescuento(int facturaID)
        {
            return facturasDatos.BuscarFechaFacturaYDescuento(facturaID);
        }

        public bool BuscarFacturaPorID(int facturaID)
        {
            return facturasDatos.BuscarFacturaPorID(facturaID);
        }


            public ObjectResult<proc_CargarTodasFacturas_Result> CargarTodasFacturas()
        {
            return facturasDatos.CargarTodasFacturas();
        }

        public ObjectResult<proc_ComprobanteFacturaVenta_Result> CargarComprobanteFacturaVenta(int facturaID)
        {
            return facturasDatos.CargarComprobanteFacturaVenta(facturaID);
        }

        
        public ObjectResult<proc_CargarFacturasPFecha_Result> CargarFacturasPFecha(DateTime fInicial, DateTime fFinal)
        {
            return facturasDatos.CargarFacturasPFecha(fInicial, fFinal);
        }

        public ObjectResult<proc_CargarFacturasCFinalPFecha_Result> CargarFacturasCFinalPFecha(DateTime fInicial, DateTime fFinal)
        {
            return facturasDatos.CargarFacturasCFinalPFecha(fInicial, fFinal);
        }
        public ObjectResult<proc_CargarFacturasCFiscalPFecha_Result> CargarFacturasCFiscalPFecha(DateTime fInicial, DateTime fFinal)
        {
            return facturasDatos.CargarFacturasCFiscalPFecha(fInicial, fFinal);
        }
        public ObjectResult<proc_CargarFacturasCGubernamentalPFecha_Result> CargarFacturasCGubernamentalPFecha(DateTime fInicial, DateTime fFinal)
        {
            return facturasDatos.CargarFacturasCGubernamentalPFecha(fInicial, fFinal);
        }
    }
}
