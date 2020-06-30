using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CotizacionesDatos
    {
        SFacturacionEntities modelDB = new SFacturacionEntities();
        ObjectParameter resultado = new ObjectParameter("resultado", typeof(bool));
        ObjectParameter cotizacionID = new ObjectParameter("CotizacionID", typeof(int));

        public Tuple<bool, int> InsertarCotizacion(Cotizacione cotizacion)
        {
            modelDB.proc_InsertarCotizacion(cotizacionID, cotizacion.ClienteID, cotizacion.Fecha, cotizacion.UserID, cotizacion.Facturada, cotizacion.DescuentoCliente, resultado);

            return Tuple.Create((bool)resultado.Value, (int)cotizacionID.Value);
        }        

        public bool BorrarCotizacion(int cotizacionID)
        {
            modelDB.proc_BorrarCotizacion(cotizacionID, resultado);
            return (bool)resultado.Value;
        }

        public bool ActualizarEstatusCotizacion(int cotizacionID)
        {
            modelDB.proc_ActualizarEstatusCotizacion(cotizacionID, resultado);
            return (bool)resultado.Value;
        }
        public ObjectResult<proc_CargarTodasCotizaciones_Result> CargarTodasCotizaciones()
        {
            var result = modelDB.proc_CargarTodasCotizaciones();
            return result;
        }

        public ObjectResult<proc_CargarCotizacionesActivas_Result> CargarCotizacionesActivas()
        {
            var result = modelDB.proc_CargarCotizacionesActivas();
            return result;
        }

        public ObjectResult<proc_ComprobanteCotizacion_Result> CargarComprobanteCotizacion(int cotizacionID)
        {
            var result = modelDB.proc_ComprobanteCotizacion(cotizacionID);
            return result;
        }

        public ObjectResult<proc_CargarProductosCotizacion_Result> CargarProductosCotizacion(int cotizacionID)
        {
            var result = modelDB.proc_CargarProductosCotizacion(cotizacionID);
            return result;
        }


        public ObjectResult<proc_CargarCotizacionesPFecha_Result> CargarCotizacionesPFecha(DateTime fInicial, DateTime fFinal)
        {
            var result = modelDB.proc_CargarCotizacionesPFecha(fInicial, fFinal);
            return result;
        }

    }
}
