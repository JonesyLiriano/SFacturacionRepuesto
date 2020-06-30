using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class DetalleCotizacionesNegocio
    {
        DetalleCotizacionesDatos detalleCotizacionesDatos = new DetalleCotizacionesDatos();

        public Tuple<bool, int> InsertarDetalleCotizacion(DetalleCotizacione detalleCotizacion)
        {

            return detalleCotizacionesDatos.InsertarDetalleCotizacion(detalleCotizacion);
        }       
    }
}
