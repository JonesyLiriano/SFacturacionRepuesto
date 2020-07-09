using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class TiposFacturaNegocio
    {
        TiposFacturaDatos tiposFacturaDatos = new TiposFacturaDatos();

        public ObjectResult<proc_CargarTodosTiposFactura_Result> CargarTodosTiposFactura()
        {
            return tiposFacturaDatos.CargarTodosTiposFactura();
        }     
    }
}
