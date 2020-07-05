using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class LineasCreditoComprasNegocio
    {
        LineasCreditoComprasDatos lineasCreditoVentasDatos = new LineasCreditoComprasDatos();

        public Tuple<bool, int> InsertarLineaCreditoCompra(LineasCreditoCompra lineaCreditoCompra)
        {
            return lineasCreditoVentasDatos.InsertarLineaCreditoCompra(lineaCreditoCompra);
        }

        public ObjectResult<proc_CargarTodasLineasCreditoCompras_Result> CargarTodasLineasCreditoCompras()
        {
            return lineasCreditoVentasDatos.CargarTodasLineasCreditoCompras();
        }
        public bool ActualizarLineaCreditoVenta(int lineaCreditoCompraID, bool estatus)
        {
            return lineasCreditoVentasDatos.ActualizarLineaCreditoCompra(lineaCreditoCompraID, estatus);
        }

        //public int BuscarLineaDeCreditoVentaIDFactura(int facturaID)
        //{
        //    return lineasCreditoVentasDatos.BuscarLineaDeCreditoVentaIDFactura(facturaID);
        //}
    }
}
