﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;

namespace CapaDatos
{
    public class ProveedoresDatos
    {
        SFacturacionEntities modelDB = new SFacturacionEntities();
        ObjectParameter resultado = new ObjectParameter("resultado", typeof(bool));
        ObjectParameter proveedorID = new ObjectParameter("ProveedorID", typeof(int));

        public Tuple<bool, int> AgregarProveedor(Proveedore proveedor)
        {
            modelDB.proc_InsertarProveedor(proveedorID, proveedor.Nombre, proveedor.CedulaORnc, proveedor.Direccion, proveedor.Contacto_1, proveedor.Contacto_2, proveedor.DatoAdicional, resultado);

            return Tuple.Create((bool)resultado.Value, (int)proveedorID.Value);
        }

        public ObjectResult<proc_CargarTodosProveedores_Result> CargarTodosProveedores(int indicePagina, int tamanoPagina, string filtro, string columna)
        {
            var result = modelDB.proc_CargarTodosProveedores(indicePagina, tamanoPagina, filtro, columna);

            return result;
        }

        public bool EditarProveedor(Proveedore proveedor)
        {
            modelDB.proc_ActualizarProveedor(proveedor.ProveedorID, proveedor.Nombre, proveedor.CedulaORnc, proveedor.Direccion, proveedor.Contacto_1, proveedor.Contacto_2, proveedor.DatoAdicional, resultado);
            return (bool)resultado.Value;
        }

        public bool BorrarProveedor(int proveedorID)
        {
            modelDB.proc_BorrarProveedor(proveedorID, resultado);
            return (bool)resultado.Value;
        }
    }
}
