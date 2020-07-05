using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class SistemaResumenDatos
    {
        SFacturacionEntities modelDB = new SFacturacionEntities();

        public ObjectResult<proc_ResumenSistema_Result> ResumenSistema()
        {
            return modelDB.proc_ResumenSistema();
        }
    }
}
