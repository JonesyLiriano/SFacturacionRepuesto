using CapaDatos;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Formularios
{
    public partial class ProductosPorProveedor : Form
    {
        ProductosNegocio productosNegocio = new ProductosNegocio();
        ProveedoresNegocio proveedoresNegocio = new ProveedoresNegocio();
        List<proc_CargarProductosExistBajaPorProveedor_Result> proc_CargarProductosExistBajaPorProveedors;
        List<proc_CargarProductosPorProveedor_Result> proc_BuscarProductosPorProveedors;
        public static DataTable dtProductosMarcados;
        private int proveedorID;
        public ProductosPorProveedor(int proveedorID)
        {
            InitializeComponent();
            this.proveedorID = proveedorID;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InicializarDataTable()
        {
            dtProductosMarcados = new DataTable();
            dtProductosMarcados.Columns.Add("ProductoID", typeof(int));
            dtProductosMarcados.Columns.Add("Descripcion", typeof(string));
            dtProductosMarcados.Columns.Add("UnidadMedida", typeof(string));
            dtProductosMarcados.Columns.Add("Existencia", typeof(double));
            dtProductosMarcados.Columns.Add("PrecioCompra", typeof(decimal));
            dtProductosMarcados.Columns.Add("CantMin", typeof(double));
            dtProductosMarcados.Columns.Add("Ordenada", typeof(double));
            
        }

        private void CargarDataGridView()
        {
            if (checkBoxProdExistBaja.Checked == true)
            {
                proc_CargarProductosExistBajaPorProveedors = productosNegocio.CargarProductosExistBajaPorProveedor(proveedorID).ToList();
                dgvProductos.DataSource = proc_CargarProductosExistBajaPorProveedors;
            }
            else
            {
                proc_BuscarProductosPorProveedors = productosNegocio.CargarProductosPorProveedor(proveedorID).ToList();
                dgvProductos.DataSource = proc_BuscarProductosPorProveedors;
            }
            dgvProductos.Columns["CantMin"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProductos.Columns["CantMax"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProductos.Columns["Existencia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProductos.Columns["PrecioCompra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProductos.Columns["ProductoID"].ReadOnly = true;
            dgvProductos.Columns["Descripcion"].ReadOnly = true;
            dgvProductos.Columns["UnidadMedida"].ReadOnly = true;
            dgvProductos.Columns["Existencia"].ReadOnly = true;
            dgvProductos.Columns["PrecioCompra"].ReadOnly = true;
            dgvProductos.Columns["CantMin"].ReadOnly = true;
            dgvProductos.Columns["CantMax"].ReadOnly = true;
            dgvProductos.Refresh();
        }

        private void checkBoxProdExistBaja_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CargarDataGridView();

            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void ProductosPorProveedor_Load(object sender, EventArgs e)
        {
            InicializarDataTable();
            CargarDataGridView();
        }

        private void btnMarcarTodos_Click(object sender, EventArgs e)
        {
            try
            {
                if(dgvProductos.Rows.Count > 0)
                {
                    MarcarODesmarcarTodo(true);
                }
                else
                {
                    MessageBox.Show("No hay productos en el carrito para marcar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }                
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void btnDesmarcarTodos_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProductos.Rows.Count > 0)
                {
                    MarcarODesmarcarTodo(false);
                }
                else
                {
                    MessageBox.Show("No hay productos en el carrito para desmarcar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }                

            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void MarcarODesmarcarTodo(bool valor)
        {
            foreach (DataGridViewRow row in dgvProductos.Rows)
                row.Cells["Seleccionar"].Value = valor;

            dgvProductos.RefreshEdit();

        }

        private void ProductosMarcados()
        {
            dtProductosMarcados.Rows.Clear();
            foreach (DataGridViewRow rowMarcados in dgvProductos.Rows)
            {
                if (Convert.ToBoolean(rowMarcados.Cells["Seleccionar"].Value) == true)
                {
                    dtProductosMarcados.Rows.Add(
                          Convert.ToInt32(rowMarcados.Cells["ProductoID"].Value)
                        , Convert.ToString(rowMarcados.Cells["Descripcion"].Value)
                        , Convert.ToString(rowMarcados.Cells["UnidadMedida"].Value)
                        , Convert.ToString(rowMarcados.Cells["Existencia"].Value)
                        , Convert.ToDouble(rowMarcados.Cells["PrecioCompra"].Value)
                        , Convert.ToDouble(rowMarcados.Cells["CantMin"].Value)
                        , Convert.ToDouble(rowMarcados.Cells["CantMax"].Value));
                    }
            }

        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProductos.Rows.Count > 0)
                {
                    ProductosMarcados();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No hay productos en el carrito para seleccionar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
           
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }
    }
}
