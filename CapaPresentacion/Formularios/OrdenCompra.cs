using CapaDatos;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Formularios
{
    public partial class OrdenCompra : Form
    {
        TiposPagosNegocio tiposPagosNegocio = new TiposPagosNegocio();
        ProveedoresNegocio proveedoresNegocio = new ProveedoresNegocio();
        OrdenesCompra ordenCompraEntidad = new OrdenesCompra();
        OrdenesCompraNegocio ordenesCompraNegocio = new OrdenesCompraNegocio();
        DetalleOrdenCompraNegocio detalleOrdenCompraNegocio = new DetalleOrdenCompraNegocio();
        DetalleOrdenesCompra detalleOrdenCompraEntidad = new DetalleOrdenesCompra();
        List<proc_CargarDetalleOrdenCompra_Result> proc_CargarDetalleOrdenCompra_Results;
        Producto productoEntidad = new Producto();
        ProductosNegocio productosNegocio = new ProductosNegocio();
        private int ordenCompraID;
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        public OrdenCompra()
        {
            InitializeComponent();
            CargarCbProveedores();
        }
        public OrdenCompra(string proveedor, int ordenCompraID ,bool status)
        {
            InitializeComponent();
            this.ordenCompraID = ordenCompraID;
            CargarCbProveedores();
            cbProveedor.SelectedItem = proveedor;
            cbProveedor.Enabled = false;

            if (status)
            {
                btnBuscarProd.Enabled = false;
                btnGuardar.Enabled = false;
                btnRecibirTodo.Enabled = false;
                btnQuitar.Enabled = false;
                btnFacturarOrdenCorte.Enabled = false;
            }
            CargarDataGridView();
        }



        private void iconcerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscarProd_Click(object sender, EventArgs e)
        {
            if (cbProveedor.SelectedIndex != 1)
            {
                ProductosPorProveedor productosPorProveedor = new ProductosPorProveedor(Convert.ToInt32(cbProveedor.SelectedValue));
                productosPorProveedor.ShowDialog();
                AgregarProductosDGV();
            }
            else
            {
                MessageBox.Show("Debe de seleccionar al menos un proveedor para buscar sus productos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        private void OrdenCompra_Load(object sender, EventArgs e)
        {
            InicializarDGV();
        }

        private void CargarCbProveedores()
        {
            cbProveedor.DisplayMember = "Nombre";
            cbProveedor.ValueMember = "ProveedorID";
            cbProveedor.DataSource = proveedoresNegocio.CargarTodosProveedores();
            cbProveedor.SelectedIndex = -1;
        }

        private void cbProveedor_Validating(object sender, CancelEventArgs e)
        {
            if (cbProveedor.SelectedIndex == -1 && cbProveedor.Items.Count > 0)
            {
                cbProveedor.Focus();
            }
        }
        private void CargarDataGridView()
        {
            if (dgvProductos.Rows.Count > 0)
            {
                dgvProductos.Rows.Clear();
            }
            proc_CargarDetalleOrdenCompra_Results = detalleOrdenCompraNegocio.CargarDetalleOrdenCompra(ordenCompraID).ToList();
            foreach (proc_CargarDetalleOrdenCompra_Result list in proc_CargarDetalleOrdenCompra_Results)
            {
                dgvProductos.Rows.Add(list.ProductoID, list.Descripcion, list.UnidadMedida, list.Existencia,
                    list.CantMin,list.PrecioCompra, list.Ordenada, list.Recibida);
            }
        }

        private void InicializarDGV()
        {
            dgvProductos.Columns["Recibida"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProductos.Columns["Ordenada"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProductos.Columns["Existencia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProductos.Columns["PrecioCompra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProductos.Columns["ProductoID"].ReadOnly = true;
            dgvProductos.Columns["Descripcion"].ReadOnly = true;
            dgvProductos.Columns["UnidadMedida"].ReadOnly = true;
            dgvProductos.Columns["Existencia"].ReadOnly = true;
            dgvProductos.Columns["CantMin"].ReadOnly = true;
            dgvProductos.Refresh();
        }

        private void AgregarProductosDGV()
        {
            try {
                if (ProductosPorProveedor.dtProductosMarcados.Rows.Count > 0)
                {
                    foreach (DataRow dtRow in ProductosPorProveedor.dtProductosMarcados.Rows)
                    {
                        bool existe = false;
                        foreach (DataGridViewRow dgvRow in dgvProductos.Rows)
                        {
                            if (dtRow["ProductoID"] == dgvRow.Cells["ProductoID"].Value)
                                existe = true;
                        }
                        if (!existe)
                        {
                            dgvProductos.Rows.Add(dtRow["ProductoID"], dtRow["Descripcion"],
                                dtRow["UnidadMedida"], dtRow["Existencia"], dtRow["CantMin"], Convert.ToDecimal(dtRow["PrecioCompra"]).ToString("F") ,Math.Abs((Convert.ToDouble(dtRow["CantMax"]) - Convert.ToDouble(dtRow["Existencia"]))), 0);

                        }
                    }                    
                }
                else
                {
                    MessageBox.Show("No se selecciono ningun producto para realizar la compra", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }                
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void btnRecibirTodo_Click(object sender, EventArgs e)
        {
            try
            {               
                if(dgvProductos.Rows.Count > 0)
                {
                    LlenarCamposCantidadRecibida();
                }                    
                else
                {
                    MessageBox.Show("No hay productos para recibir", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void LlenarCamposCantidadRecibida()
        {
            foreach (DataGridViewRow row in dgvProductos.Rows)
            {
                if (row.Cells["Recibida"].Value == null)
                    row.Cells["Recibida"].Value = row.Cells["Ordenada"].Value;
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProductos.SelectedRows.Count > 0)
                {
                    EliminarProducto();
                }                
                else
                {
                    MessageBox.Show("No hay productos para eliminar de la orden de compra", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void EliminarProducto()
        {        
            if (ordenCompraID > 0)
            {
                detalleOrdenCompraEntidad.OrdenCompraID = ordenCompraID;
                detalleOrdenCompraEntidad.ProductoID = Convert.ToInt32(dgvProductos.Rows[dgvProductos.CurrentRow.Index].Cells["ProductoID"].Value);
                detalleOrdenCompraNegocio.BorrarDetalleOrdenCompra(detalleOrdenCompraEntidad);                
            }
            dgvProductos.Rows.RemoveAt(dgvProductos.CurrentRow.Index);
            MessageBox.Show("Producto removido de la orden de compra correctamente.", "Producto Removido", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvProductos_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvProductos.Rows.Count > 0)
            {
                switch (e.ColumnIndex)
                {
                    case 5:
                        if (!ValidarCeldasNumero(dgvProductos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(),
                           dgvProductos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                        {
                            return;
                        }
                        break;
                    case 6:
                        if (!ValidarCeldasNumero(dgvProductos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(),
                           dgvProductos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                        {   
                            return;
                        }
                        dgvProductos.Rows[e.RowIndex].Cells["Ordenada"].Value = "";
                        break;
                    case 7:
                        if (!ValidarCeldasNumero(dgvProductos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(),
                           dgvProductos.Rows[e.RowIndex].Cells["Recibida"].Value.ToString()))
                        {
                            MessageBox.Show("La cantidad RECIBIDA no puede mayor a la cantidad ordenada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if(Convert.ToBoolean(dgvProductos.Rows[e.RowIndex].Cells["Estatus"].Value))
                        {
                            MessageBox.Show("No se puede modificar la cantidad recibida, ya se completo esta entrega.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        private bool ValidarCeldasNumero(string valor1, string valor2)
        {
            decimal valor1Convertido;
            decimal valor2Convertido;

            if (!decimal.TryParse(valor1, out valor1Convertido))
            {
                MessageBox.Show("Debe de ingresar un numero valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(!decimal.TryParse(valor2, out valor2Convertido))
            {
                MessageBox.Show("Debe de ingresar un numero valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (valor1Convertido  > valor2Convertido )
            {
                return false;
            }
            return true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if(dgvProductos.Rows.Count > 0 && cbProveedor.SelectedIndex != 0)
                {
                    if (ordenCompraID > 0)
                    {
                        ActualizarOrdenCompra();
                    }
                    else
                    {
                        CrearOrdenCompra();
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se puede guardar la orden de compra sin productos | Debe de elegir un proveedor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }
        private void CrearOrdenCompra()
        {
            ordenCompraEntidad.ProveedorID = Convert.ToInt32(cbProveedor.SelectedValue);
            ordenCompraEntidad.FechaPedido = DateTime.Now;
            ordenCompraEntidad.Estatus = false;

            var result = ordenesCompraNegocio.InsertarOrdenCompra(ordenCompraEntidad);

            if (result.Item1)
            {
                ordenCompraID = result.Item2;
                CreacionDetalleOrdenCompra();
                MessageBox.Show(string.Format("Orden de compra #{0} fue creada satifastoriamente", ordenCompraID), "Orden Creada", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Orden no pudo ser creada", "Error Orden", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void CreacionDetalleOrdenCompra()
        {
            foreach (DataGridViewRow row in dgvProductos.Rows)
            {
                detalleOrdenCompraEntidad.OrdenCompraID = ordenCompraID;
                detalleOrdenCompraEntidad.ProductoID = Convert.ToInt32(row.Cells["ProductoID"].Value);
                detalleOrdenCompraEntidad.CantidadOrdenada = Convert.ToDouble(row.Cells["Ordenada"].Value);
                detalleOrdenCompraEntidad.Precio = Convert.ToDecimal(row.Cells["PrecioCompra"].Value);
                detalleOrdenCompraEntidad.Estatus = false;

                detalleOrdenCompraNegocio.InsertarDetalleOrdenCompra(detalleOrdenCompraEntidad);
            }
        }

        private void ActualizarOrdenCompra()
        {
            foreach (DataGridViewRow row in dgvProductos.Rows)
            {
                detalleOrdenCompraEntidad.OrdenCompraID = ordenCompraID;
                detalleOrdenCompraEntidad.ProductoID = Convert.ToInt32(row.Cells["ProductoID"].Value);
                detalleOrdenCompraEntidad.CantidadOrdenada = Convert.ToDouble(row.Cells["Ordenada"].Value);
                detalleOrdenCompraEntidad.CantidadRecibida = Convert.ToDouble(row.Cells["Recibida"].Value);
                detalleOrdenCompraEntidad.Precio = Convert.ToDecimal(row.Cells["PrecioCompra"].Value);
                
                if(Convert.ToDouble(row.Cells["Ordenada"].Value) == Convert.ToDouble(row.Cells["Recibida"].Value))
                {
                    if (!detalleOrdenCompraEntidad.Estatus)
                    {
                        productoEntidad.ProductoID = Convert.ToInt32(row.Cells["ProductoID"].Value);
                        productoEntidad.Existencia = Convert.ToDouble(row.Cells["Recibida"].Value);
                        productosNegocio.ActualizarCantidadProductoPorID(productoEntidad);
                    }
                    detalleOrdenCompraEntidad.Estatus = true;
                                       
                }
                else
                {
                    detalleOrdenCompraEntidad.Estatus = false;

                }
                var result = detalleOrdenCompraNegocio.ActualizarDetalleOrdenCompra(detalleOrdenCompraEntidad);

                if (result)
                    MessageBox.Show(string.Format("Orden de Compra #{0} Actualizada Correctamente", ordenCompraID), "Actualizacion Correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("No se pudo actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }

        }

        private void btnFacturarOrdenCorte_Click(object sender, EventArgs e)
        {
            if (dgvProductos.Rows.Count > 0 && cbProveedor.SelectedIndex != 0 && ordenCompraID > 0)
            {
                if (ValidarProductosRecibidos())
                {
                    FacturarOrdenCompra facturarOrdenCompra = new FacturarOrdenCompra(ordenCompraID);
                    facturarOrdenCompra.ShowDialog();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("No se puede guardar la orden de compra sin productos | Debe de elegir un proveedor | La orden de compra debe de estar creada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private bool ValidarProductosRecibidos()
        {
            foreach (DataGridViewRow row in dgvProductos.Rows)
            {
                if(Convert.ToDecimal(row.Cells["Ordenada"].Value) < Convert.ToDecimal(row.Cells["Recibida"].Value))
                {
                    MessageBox.Show("Debe primero recibir todos los productos para poder facturar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

    }
}
