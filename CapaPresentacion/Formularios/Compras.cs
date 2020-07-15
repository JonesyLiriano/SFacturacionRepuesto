using CapaDatos;
using CapaNegocios;
using CapaPresentacion.Impresiones;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Formularios
{
    public partial class Compras : Form
    {
        List<proc_CargarTodasOrdenesCompra_Result> proc_CargarTodasOrdenesCompra_Results;
        OrdenesCompraNegocio ordenesCompraNegocio = new OrdenesCompraNegocio();
        Thread hilo;
        public Compras()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCompras.SelectedRows.Count > 0)
                {
                    OrdenCompra ordenCompra = new OrdenCompra(
                   dgvCompras.CurrentRow.Cells["Proveedor"].Value.ToString(),
                   Convert.ToInt32(dgvCompras.Rows[dgvCompras.CurrentRow.Index].Cells["OrdenCompraID"].Value),
                   Convert.ToBoolean(dgvCompras.Rows[dgvCompras.CurrentRow.Index].Cells["Completado"].Value)
                   );
                    ordenCompra.ShowDialog();
                    CargarDataGridView();
                }
                else
                {
                    MessageBox.Show("Debe de seleccionar una orden de compra para poder ver su detalle", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Ha ocurrido un error intentando ver el detalle de esta orden de compra, intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }

        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                OrdenCompra ordenCompra = new OrdenCompra();
                ordenCompra.ShowDialog();
                CargarDataGridView();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Ha ocurrido un error intentando registrar esta orden de compra, intente de nuevo por favor. ",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCompras.SelectedRows.Count > 0)
                {
                    if (Convert.ToBoolean(dgvCompras.Rows[dgvCompras.CurrentRow.Index].Cells["Completado"].Value))
                    {
                        DialogResult result = MessageBox.Show("Esta seguro que desea borrar la orden de compra?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)                        {

                            var resultado = ordenesCompraNegocio.BorrarOrdenCompra(Convert.ToInt32(dgvCompras.Rows[dgvCompras.CurrentRow.Index].Cells["OrdenCompraID"].Value));

                            if (resultado)
                            {
                                CargarDataGridView();
                                MessageBox.Show("Orden de compra ha sido borrada correctamente.", "Orden de Compra Eliminada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No se ha podido borrar la orden de compra", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                    }

                    else
                    {
                        MessageBox.Show("Esta orden de compra ya esta cerrada, ya no puede modificarse.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Debe de seleccionar una orden de compra para poder ver su detalle", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: Orden de compra no puede ser eliminado, posiblemente esta orden de compra ya esta relacionado con algun proveedor o factura de compra.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void Compras_Load(object sender, EventArgs e)
        {
            try
            {
                CargarDataGridView();
                CargarCBFiltro();
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: No se ha podido cargar las ordenes de compra, intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void CargarDataGridView()
        {
            dgvCompras.AutoGenerateColumns = false;
            proc_CargarTodasOrdenesCompra_Results = ordenesCompraNegocio.CargarTodasOrdenesCompra().ToList();
            dgvCompras.DataSource = proc_CargarTodasOrdenesCompra_Results;
            OrdenarColumnasDGV();
        }

        private void OrdenarColumnasDGV()
        {
            dgvCompras.Columns["OrdenCompraID"].DisplayIndex = 0;
            dgvCompras.Columns["Proveedor"].DisplayIndex = 1;
            dgvCompras.Columns["FechaPedido"].DisplayIndex = 2;
            dgvCompras.Columns["Completado"].DisplayIndex = 3;
            dgvCompras.Columns["FechaCompletado"].DisplayIndex = 4;
            dgvCompras.Columns["TipoDePago"].DisplayIndex = 5;
            dgvCompras.Columns["NCF"].DisplayIndex = 6;
            dgvCompras.Columns["FechaVencimiento"].DisplayIndex = 7;
            dgvCompras.Columns["FechaFactura"].DisplayIndex = 8;
            dgvCompras.Columns["ITBIS"].DisplayIndex = 9;
            dgvCompras.Columns["SubTotal"].DisplayIndex = 10;
            dgvCompras.Columns["Total"].DisplayIndex = 11;

            dgvCompras.Columns["ITBIS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCompras.Columns["SubTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCompras.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvCompras.Refresh();
        }
        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCompras.SelectedRows.Count > 0)
                {
                    ImpresionOrdenCompra impresionOrdenCompra = new ImpresionOrdenCompra(Convert.ToInt32(dgvCompras.CurrentRow.Cells["OrdenCompraID"].Value));
                    impresionOrdenCompra.ImprimirConVistaPrevia();
                    impresionOrdenCompra.Show();
                }
                else
                {
                    MessageBox.Show("Debe de seleccionar al menos una orden de compra para imprimir", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido exportar la orden de compra, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCompras.SelectedRows.Count > 0)
                {
                    hilo = new Thread(() =>
                    {
                        ImpresionOrdenCompra impresionOrdenCompra = new ImpresionOrdenCompra(Convert.ToInt32(dgvCompras.CurrentRow.Cells["OrdenCompraID"].Value));
                        impresionOrdenCompra.Visible = false;
                        impresionOrdenCompra.ImprimirDirecto();
                    });
                    hilo.Start();

                }
                else
                {
                    MessageBox.Show("Debe de seleccionar al menos una orden de compra para imprimir", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido imprimir la orden de compra, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }
        private void CargarCBFiltro()
        {
            cbFiltro.Items.Add("ID");
            cbFiltro.Items.Add("Proveedor");
            cbFiltro.Items.Add("Fecha Pedido");
            cbFiltro.Items.Add("Tipo de Pago");
            cbFiltro.Items.Add("NCF");
            cbFiltro.Items.Add("Completado");
            cbFiltro.SelectedIndex = 0;
        }
        private void txtFiltro_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFiltro.Text))
            {
                txtFiltro.Text = "Escriba para filtrar...";
                txtFiltro.ForeColor = Color.Gray;
            }
        }

        private void txtFiltro_Enter(object sender, EventArgs e)
        {
            if (txtFiltro.Text == "Escriba para filtrar...")
            {
                txtFiltro.Text = "";
                txtFiltro.ForeColor = Color.Black;
            }
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtFiltro.Text != "Escriba para filtrar...")
                {
                    switch (cbFiltro.SelectedItem.ToString())
                    {
                        case "ID":
                            dgvCompras.DataSource = proc_CargarTodasOrdenesCompra_Results.Where(p => p.OrdenCompraID.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Proveedor":
                            dgvCompras.DataSource = proc_CargarTodasOrdenesCompra_Results.Where(p => p.Proveedor.ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Tipo de Pago":
                            dgvCompras.DataSource = proc_CargarTodasOrdenesCompra_Results.Where(p => p.TipoDePago.ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Fecha Pedido":
                            dgvCompras.DataSource = proc_CargarTodasOrdenesCompra_Results.Where(p => p.FechaPedido.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "NCF":
                            dgvCompras.DataSource = proc_CargarTodasOrdenesCompra_Results.Where(p => p.NCF.ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Completado":
                            dgvCompras.DataSource = proc_CargarTodasOrdenesCompra_Results.Where(p => p.Completado.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        default:
                            break;
                    }
                }
                OrdenarColumnasDGV();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido realizar la busqueda, intente de nuevo por favor.",
                      "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void cbFiltro_Validating(object sender, CancelEventArgs e)
        {
            if (cbFiltro.SelectedIndex == -1 && cbFiltro.Items.Count > 0)
            {
                cbFiltro.Focus();
            }
        }
    }
}
