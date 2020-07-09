using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CotizacionesNegocio
    {
        CotizacionesDatos cotizacionDatos = new CotizacionesDatos();

        public Tuple<bool, int> InsertarCotizacion(Cotizacione cotizacion)
        {

            return cotizacionDatos.InsertarCotizacion(cotizacion);
        }

        public bool BorrarCotizacion(int cotizacionID)
        {
            return cotizacionDatos.BorrarCotizacion(cotizacionID);
        }

        public bool ActualizarEstatusCotizacion(int cotizacionID)
        {
            return cotizacionDatos.ActualizarEstatusCotizacion(cotizacionID);
        }


        public ObjectResult<proc_CargarTodasCotizaciones_Result> CargarTodasCotizaciones()
        {
            return cotizacionDatos.CargarTodasCotizaciones();
        }

        public ObjectResult<proc_CargarProductosCotizacion_Result> CargarProductosCotizacion(int cotizacionID)
        {
            return cotizacionDatos.CargarProductosCotizacion(cotizacionID);
        }

        public ObjectResult<proc_CargarCotizacionesActivas_Result> CargarCotizacionesActivas()
        {
            return cotizacionDatos.CargarCotizacionesActivas();
        }

        public ObjectResult<proc_ComprobanteCotizacion_Result> CargarComprobanteCotizacion(int cotizacionID)
        {
            return cotizacionDatos.CargarComprobanteCotizacion(cotizacionID);
        }
    }
}
