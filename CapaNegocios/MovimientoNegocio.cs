using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class MovimientoNegocio
    {
        MovimientoDatos movimientoDatos = new MovimientoDatos();
        public Tuple<bool, int> AgregarMovimiento(Movimiento movimiento)
        {

            return movimientoDatos.AgregarMovimiento(movimiento);
        }

        public ObjectResult<proc_CargarMovimientos_Result> CargarMovimientos(int productoID)
        {
            return movimientoDatos.CargarMovimientos(productoID);
        }
    }
}
