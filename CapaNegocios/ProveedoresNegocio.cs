using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
namespace CapaNegocios
{
    public class ProveedoresNegocio
    {
        ProveedoresDatos proveedoresDatos = new ProveedoresDatos();
        public Tuple<bool, int> AgregarProveedor(Proveedore proveedor)
        {

            return proveedoresDatos.AgregarProveedor(proveedor);
        }

        public ObjectResult<proc_CargarTodosProveedores_Result> CargarTodosProveedores(int indicePagina, int tamanoPagina, string filtro, string columna)
        {
            return proveedoresDatos.CargarTodosProveedores(indicePagina, tamanoPagina, filtro, columna);
        }

        public bool EditarProveedor(Proveedore proveedor)
        {
            return proveedoresDatos.EditarProveedor(proveedor);
        }

        public bool BorrarProveedor(int proveedorID)
        {
            return proveedoresDatos.BorrarProveedor(proveedorID);
        }
    }
}
