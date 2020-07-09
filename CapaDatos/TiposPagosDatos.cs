using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class TiposPagosDatos
    {
        SFacturacionEntities modelDB = new SFacturacionEntities();
        ObjectParameter resultado = new ObjectParameter("resultado", typeof(bool));
        ObjectParameter tipoPagoID = new ObjectParameter("TipoPagoID", typeof(int));

        public ObjectResult<proc_CargarTodosTiposPagos_Result> CargarTodosTiposPagos()
        {
            var result = modelDB.proc_CargarTodosTiposPagos();
        
            return result;
        }
    }
}
