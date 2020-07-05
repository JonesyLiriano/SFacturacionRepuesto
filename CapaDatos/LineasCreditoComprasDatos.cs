using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class LineasCreditoComprasDatos
    {
        SFacturacionEntities modelDB = new SFacturacionEntities();
        ObjectParameter resultado = new ObjectParameter("resultado", typeof(bool));
        ObjectParameter lineaCreditoCompraID = new ObjectParameter("LineaCreditoCompraID", typeof(int));

        public Tuple<bool, int> InsertarLineaCreditoCompra(LineasCreditoCompra lineaCreditoCompra)
        {
            modelDB.proc_InsertarLineaCreditoCompra(lineaCreditoCompraID, lineaCreditoCompra.FacturaCompraID, lineaCreditoCompra.Estatus, resultado);

            return Tuple.Create((bool)resultado.Value, (int)lineaCreditoCompraID.Value);
        }
        
        public ObjectResult<proc_CargarTodasLineasCreditoCompras_Result> CargarTodasLineasCreditoCompras()
        {
            var result = modelDB.proc_CargarTodasLineasCreditoCompras();

            return result;
        }
        public bool ActualizarLineaCreditoCompra(int lineaCreditoCompraID, bool estatus)
        {
            modelDB.proc_ActualizarLineaCreditoCompra(lineaCreditoCompraID, estatus, resultado);
            return (bool)resultado.Value;
        }

        //public int BuscarLineaDeCreditoVentaIDFactura(int ordenCompraID)
        //{
        //    modelDB.proc_BuscarLineaDeCreditoCompraIDOrdenCompra(ordenCompraID, lineaCreditoCompraID);

        //    return (int)lineaCreditoCompraID.Value;
        //}
    }
}
