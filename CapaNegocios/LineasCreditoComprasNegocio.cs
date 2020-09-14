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
        LineasCreditoComprasDatos lineasCreditoComprasDatos = new LineasCreditoComprasDatos();

        public Tuple<bool, int> InsertarLineaCreditoCompra(LineasCreditoCompra lineaCreditoCompra)
        {
            return lineasCreditoComprasDatos.InsertarLineaCreditoCompra(lineaCreditoCompra);
        }

        public ObjectResult<proc_CargarTodasLineasCreditoCompras_Result> CargarTodasLineasCreditoCompras(int indicePagina, int tamanoPagina, string filtro, string columna)
        {
            return lineasCreditoComprasDatos.CargarTodasLineasCreditoCompras(indicePagina, tamanoPagina, filtro, columna);
        }
        public bool ActualizarLineaCreditoCompra(int lineaCreditoCompraID, bool estatus)
        {
            return lineasCreditoComprasDatos.ActualizarLineaCreditoCompra(lineaCreditoCompraID, estatus);
        }
    }
}
