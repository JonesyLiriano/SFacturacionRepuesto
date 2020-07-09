using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocios
{
    public class CobrosVentaCreditoNegocio
    {
        CobrosVentaCreditoDatos cobrosVentaCreditoDatos = new CobrosVentaCreditoDatos();

        public Tuple<bool, int> InsertarCobroVentaCredito(CobrosVentasCredito cobrosVentasCredito)
        {

            return cobrosVentaCreditoDatos.InsertarCobroVentaCredito(cobrosVentasCredito);
        }

        public ObjectResult<proc_CargarCobrosVentaCredito_Result> CargarCobrosVentaCredito(int lineaCreditoVentaID)
        {
            return cobrosVentaCreditoDatos.CargarCobrosVentaCredito(lineaCreditoVentaID);
        }

        public ObjectResult<proc_ComprobantePagoLineaCreditoVenta_Result> ComprobantePagoLineaCreditoVenta(int cobrosVentasCreditoID)
        {
            return cobrosVentaCreditoDatos.ComprobantePagoLineaCreditoVenta(cobrosVentasCreditoID);
        }
        public bool BorrarCobroVentaCredito(int cobroVentaCreditoID)
        {
            return cobrosVentaCreditoDatos.BorrarCobroVentaCredito(cobroVentaCreditoID);          
        }
    }
}
