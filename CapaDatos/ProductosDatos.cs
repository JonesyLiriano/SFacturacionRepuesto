using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;

namespace CapaDatos
{
    public class ProductosDatos
    {
        SFacturacionEntities modelDB = new SFacturacionEntities();
        ObjectParameter resultado = new ObjectParameter("resultado", typeof(bool));
        ObjectParameter productoID = new ObjectParameter("ProductoID", typeof(int));

        public Tuple<bool, int> AgregarProducto(Producto producto)
        {
            modelDB.proc_InsertarProducto(productoID,producto.Descripcion,producto.Existencia, producto.PrecioCompra, producto.PrecioVenta, producto.PrecioVentaMin, producto.ProveedorID, producto.Servicio, producto.ITBIS, producto.Descuento, producto.CantMin, producto.CantMax, producto.CodigoBarra, producto.UnidadMedida, producto.Referencia, producto.Marca, producto.Calidad, resultado);

            return Tuple.Create((bool)resultado.Value, (int)productoID.Value);
        }
        public ObjectResult<proc_CargarTodosProductos_Result> CargarTodosProductos()
        {
            var result = modelDB.proc_CargarTodosProductos();

            return result;
        }
        public ObjectResult<proc_CargarProductosExistBaja_Result> CargarProductosExistBaja()
        {
            var result = modelDB.proc_CargarProductosExistBaja();

            return result;
        }

        public ObjectResult<proc_CargarProductosExistBajaPorProveedor_Result> CargarProductosExistBajaPorProveedor(int proveedorID)
        {
            var result = modelDB.proc_CargarProductosExistBajaPorProveedor(proveedorID);

            return result;
        }

        public ObjectResult<proc_CargarProductosPorProveedor_Result> CargarProductosPorProveedor(int proveedorID)
        {
            var result = modelDB.proc_CargarProductosPorProveedor(proveedorID);

            return result;
        }

        public ObjectResult<proc_CargarProductosMasVendidos_Result> CargarProductosMasVendidos()
        {
            var result = modelDB.proc_CargarProductosMasVendidos();

            return result;
        }

        public bool EditarProducto(Producto producto)
        {
            modelDB.proc_ActualizarProducto(producto.ProductoID, producto.Descripcion, producto.Existencia, producto.PrecioCompra, producto.PrecioVenta, producto.PrecioVentaMin, producto.ProveedorID, producto.Servicio, producto.ITBIS, producto.Descuento, producto.CantMin, producto.CantMax, producto.CodigoBarra, producto.UnidadMedida, producto.Referencia, producto.Marca, producto.Calidad, resultado);
            return (bool)resultado.Value;
        }

        public bool BorrarProducto(int productoID)
        {
            modelDB.proc_BorrarProducto(productoID, resultado);
            return (bool)resultado.Value;
        }

        public ObjectResult<proc_BuscarProductosPorCodigoBarra_Result> BuscarProductosPorCodigoBarra(string codigoBarra)
        {
            var result = modelDB.proc_BuscarProductosPorCodigoBarra(codigoBarra);

            return result;
        }
        public bool ActualizarCantidadProducto(Producto producto)
        {
            modelDB.proc_ActualizarCantidadProducto(producto.CodigoBarra, producto.Existencia, resultado);

            return (bool)resultado.Value;
        }
        public bool ActualizarCantidadProductoPorID(Producto producto)
        {
            modelDB.proc_ActualizarCantidadProductoPorID(producto.ProductoID, producto.Existencia, resultado);

            return (bool)resultado.Value;
        }
    }
}
