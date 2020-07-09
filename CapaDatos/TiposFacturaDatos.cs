using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class TiposFacturaDatos
    {
        SFacturacionEntities modelDB = new SFacturacionEntities();
        ObjectParameter resultado = new ObjectParameter("resultado", typeof(bool));
        ObjectParameter tipoFacturaID = new ObjectParameter("TipoFacturaID", typeof(int));

        public ObjectResult<proc_CargarTodosTiposFactura_Result> CargarTodosTiposFactura()
        {
            var result = modelDB.proc_CargarTodosTiposFactura();

            return result;
        }       
    }
}
