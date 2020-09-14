using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocios
{
    public class NotasDeCreditoNegocio
    {
        NotasDeCreditoDatos notasDeCreditoDatos = new NotasDeCreditoDatos();

        public Tuple<bool, int> AgregarNotaDeCredito(NotasDeCredito notasDeCredito)
        {

            return notasDeCreditoDatos.AgregarNotaDeCredito(notasDeCredito);
        }

        public ObjectResult<proc_CargarTodasNotasDeCredito_Result> CargarTodasNotasDeCredito(int indicePagina, int tamanoPagina, string filtro, string columna)
        {
            return notasDeCreditoDatos.CargarTodasNotasDeCredito(indicePagina, tamanoPagina, filtro, columna);
        }

        public ObjectResult<proc_ComprobanteNotaDeCredito_Result> CargarComprobanteNotaDeCredito(int notaCreditoID)
        {
            return notasDeCreditoDatos.CargarComprobanteNotaDeCredito(notaCreditoID);
        }

        public ObjectResult<proc_CargarNotasDeCreditoPFecha_Result> CargarNotasDeCreditoPFecha(DateTime fInicial, DateTime fFinal)
        {
            return notasDeCreditoDatos.CargarNotasDeCreditoPFecha(fInicial, fFinal);
        }
    }
}
