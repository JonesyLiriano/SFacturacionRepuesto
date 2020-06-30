using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DetalleCotizacionesDatos
    {
        SFacturacionEntities modelDB = new SFacturacionEntities();
        ObjectParameter resultado = new ObjectParameter("resultado", typeof(bool));
        ObjectParameter detalleCotizacionID = new ObjectParameter("DetalleCotizacionID", typeof(int));

        public Tuple<bool, int> InsertarDetalleCotizacion(DetalleCotizacione detalleCotizacion)
        {
            modelDB.proc_InsertarDetalleCotizacion(detalleCotizacionID, detalleCotizacion.CotizacionID, detalleCotizacion.ProductoID, detalleCotizacion.Cantidad, detalleCotizacion.Precio, detalleCotizacion.ITBIS, detalleCotizacion.Descuento, resultado);

            return Tuple.Create((bool)resultado.Value, (int)detalleCotizacionID.Value);
        }        
    }
}
