﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocios
{
    public class SistemaResumenNegocio
    {
        SistemaResumenDatos resumenDatos = new SistemaResumenDatos();
        public ObjectResult<proc_ResumenSistema_Result> ResumenSistema()
        {
            return resumenDatos.ResumenSistema();
        }

        public void RealizarCopiadeSeguridad()
        {
            resumenDatos.RealizarCopiadeSeguridad();
        }

        public Tuple<decimal, decimal, decimal, decimal, decimal, decimal> CalcularGanancias(decimal itbis)
        {
            return resumenDatos.CalcularGanancias(itbis);
        }
    }
}
