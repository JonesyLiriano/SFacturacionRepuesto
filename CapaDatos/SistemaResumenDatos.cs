﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class SistemaResumenDatos
    {
        SFacturacionEntities modelDB = new SFacturacionEntities();
        ObjectParameter gananciaFContDia = new ObjectParameter("GananciaFContDia", typeof(decimal));
        ObjectParameter gananciaFContSemana = new ObjectParameter("GananciaFContSemana", typeof(decimal));
        ObjectParameter gananciaFContMes = new ObjectParameter("GananciaFContMes", typeof(decimal));
        ObjectParameter gananciaFCredDia = new ObjectParameter("GananciaFCredDia", typeof(decimal));
        ObjectParameter gananciaFCredSemana = new ObjectParameter("GananciaFCredSemana", typeof(decimal));
        ObjectParameter gananciaFCredtMes = new ObjectParameter("GananciaFCredMes", typeof(decimal));
        public ObjectResult<proc_ResumenSistema_Result> ResumenSistema()
        {
            return modelDB.proc_ResumenSistema();
        }

        public void RealizarCopiadeSeguridad()
        {
            modelDB.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "proc_RealizarBackUp");

        }
        public Tuple<decimal, decimal, decimal, decimal, decimal, decimal> CalcularGanancias(decimal itbis)
        {
            modelDB.proc_CalcularGanancias(gananciaFContDia, gananciaFContSemana, gananciaFContMes, gananciaFCredDia, gananciaFCredSemana, gananciaFCredtMes, itbis);

            return Tuple.Create(
                    gananciaFContDia.Value == DBNull.Value ? Convert.ToDecimal(0.00) : Convert.ToDecimal(gananciaFContDia.Value),
                    gananciaFContSemana.Value == DBNull.Value ? Convert.ToDecimal(0.00) : Convert.ToDecimal(gananciaFContSemana.Value),
                    gananciaFContMes.Value == DBNull.Value ? Convert.ToDecimal(0.00) : Convert.ToDecimal(gananciaFContMes.Value),
                    gananciaFCredDia.Value == DBNull.Value ? Convert.ToDecimal(0.00) : Convert.ToDecimal(gananciaFCredDia.Value),
                    gananciaFCredSemana.Value == DBNull.Value ? Convert.ToDecimal(0.00) : Convert.ToDecimal(gananciaFCredSemana.Value),
                    gananciaFCredtMes.Value == DBNull.Value ? Convert.ToDecimal(0.00) : Convert.ToDecimal(gananciaFCredtMes.Value));
        }
    }
}
