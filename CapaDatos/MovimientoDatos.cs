using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class MovimientoDatos
    {
        SFacturacionEntities modelDB = new SFacturacionEntities();
        ObjectParameter resultado = new ObjectParameter("resultado", typeof(bool));
        ObjectParameter movimientoID = new ObjectParameter("MovimientoID", typeof(int));


        public Tuple<bool, int> AgregarMovimiento(Movimiento movimiento)
        {
            modelDB.proc_InsertarMovimiento(movimientoID, movimiento.ProductoID, movimiento.Fecha, movimiento.TipoMovimiento,
                movimiento.Referencia, movimiento.Cantidad, movimiento.UsuarioID, resultado);

            return Tuple.Create((bool)resultado.Value, (int)movimientoID.Value);
        }

        public ObjectResult<proc_CargarMovimientos_Result> CargarMovimientos(int productoID)
        {
            return modelDB.proc_CargarMovimientos(productoID);
        }

    }
}
