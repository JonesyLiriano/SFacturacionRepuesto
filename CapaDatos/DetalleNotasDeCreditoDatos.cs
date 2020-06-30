using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DetalleNotasDeCreditoDatos
    {
        SFacturacionEntities modelDB = new SFacturacionEntities();
        ObjectParameter resultado = new ObjectParameter("resultado", typeof(bool));
        ObjectParameter detalleNotaDeCreditoID = new ObjectParameter("DetalleNotaDeCreditoID", typeof(int));

        public Tuple<bool, int> AgregarDetalleNotaDeCredito(DetalleNotaDeCredito detalleNotaDeCredito)
        {
            modelDB.proc_InsertarDetalleNotaDeCredito(detalleNotaDeCreditoID, detalleNotaDeCredito.NotaDeCreditoID, detalleNotaDeCredito.ProductoID, detalleNotaDeCredito.CantDevuelta, detalleNotaDeCredito.CantInventariada, detalleNotaDeCredito.Precio, detalleNotaDeCredito.Comentario, resultado);

            return Tuple.Create((bool)resultado.Value, (int)detalleNotaDeCreditoID.Value);
        }

        public ObjectResult<proc_CargarDetalleNotaDeCredito_Result> CargarDetalleNotaDeCredito(int notaDeCreditoID)
        {
            var result = modelDB.proc_CargarDetalleNotaDeCredito(notaDeCreditoID);

            return result;
        }


        public ObjectResult<proc_ComprobanteNotaDeCredito_Result> CargarComprobanteNotaDeCredito(int notaDeCreditoID)
        {
            var result = modelDB.proc_ComprobanteNotaDeCredito(notaDeCreditoID);

            return result;
        }
    }
}
