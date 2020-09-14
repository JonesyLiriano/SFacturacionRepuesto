using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocios
{
    public class LineasCreditoVentasNegocio
    {
        LineasCreditoVentasDatos lineasCreditoVentasDatos = new LineasCreditoVentasDatos();

        public Tuple<bool, int> InsertarLineaCreditoVenta(LineasCreditoVenta lineaCreditoVenta)
        {

            return lineasCreditoVentasDatos.InsertarLineaCreditoVenta(lineaCreditoVenta);
        }

        public ObjectResult<proc_CargarTodasLineasCreditoVentas_Result> CargarTodasLineasCreditoVentas(int indicePagina, int tamanoPagina, string filtro, string columna)
        {
           return lineasCreditoVentasDatos.CargarTodasLineasCreditoVentas(indicePagina, tamanoPagina, filtro, columna);
        }        
       
        public bool ActualizarLineaCreditoVenta(int lineaCreditoVentaID, bool estatus)
        {
            return lineasCreditoVentasDatos.ActualizarLineaCreditoVenta(lineaCreditoVentaID, estatus);
        }

        public int BuscarLineaDeCreditoVentaIDFactura(int facturaID)
        {
            return lineasCreditoVentasDatos.BuscarLineaDeCreditoVentaIDFactura(facturaID);
        }      
    }
}
