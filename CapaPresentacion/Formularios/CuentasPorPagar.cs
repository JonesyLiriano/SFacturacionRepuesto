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
    public partial class CuentasPorPagar : Form
    {
        public static decimal montoPendiente;
        LineasCreditoComprasNegocio lineasCreditoComprasNegocio = new LineasCreditoComprasNegocio();
        List<proc_CargarTodasLineasCreditoCompras_Result> proc_CargarTodasLineasCreditoCompras_Results;
        public CuentasPorPagar()
        {
            InitializeComponent();
        }

        private void CuentasPorPagar_Load(object sender, EventArgs e)
        {
            try
            {
                CargarTodasLineasCreditoCompra();
                CargarCBFiltro();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido cargar las linea de credito correctamente, intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void CargarTodasLineasCreditoCompra()
        {
            dgvLineasCreditoCompra.AutoGenerateColumns = false;
            proc_CargarTodasLineasCreditoCompras_Results = lineasCreditoComprasNegocio.CargarTodasLineasCreditoCompras().ToList();
            dgvLineasCreditoCompra.DataSource = proc_CargarTodasLineasCreditoCompras_Results;
            OrdenarColumnasDGV();
            
        }

        private void OrdenarColumnasDGV()
        {
            dgvLineasCreditoCompra.Columns["LineaCreditoCompraID"].DisplayIndex = 0;
            dgvLineasCreditoCompra.Columns["Proveedor"].DisplayIndex = 1;
            dgvLineasCreditoCompra.Columns["OrdenCompra"].DisplayIndex = 2;
            dgvLineasCreditoCompra.Columns["Fecha"].DisplayIndex = 3;
            dgvLineasCreditoCompra.Columns["FacturaCompra"].DisplayIndex = 4;
            dgvLineasCreditoCompra.Columns["MontoFactura"].DisplayIndex = 5;
            dgvLineasCreditoCompra.Columns["BalancePendiente"].DisplayIndex = 6;
            dgvLineasCreditoCompra.Columns["Completado"].DisplayIndex = 7;

            dgvLineasCreditoCompra.Columns["BalancePendiente"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLineasCreditoCompra.Columns["MontoFactura"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLineasCreditoCompra.Refresh();

        }
        private void btnConsultaFactura_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLineasCreditoCompra.SelectedRows.Count > 0)
                {
                    OrdenCompra ordenCompra = new OrdenCompra(
                   dgvLineasCreditoCompra.CurrentRow.Cells["Proveedor"].Value.ToString(),
                   Convert.ToInt32(dgvLineasCreditoCompra.Rows[dgvLineasCreditoCompra.CurrentRow.Index].Cells["OrdenCompra"].Value),
                   true);
                    ordenCompra.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Debe de seleccionar una orden de compra para poder ver su detalle", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido consultar la factura de esta linea de credito, intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void btnRealizarPago_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLineasCreditoCompra.SelectedRows.Count > 0)
                {
                    if (!Convert.ToBoolean(dgvLineasCreditoCompra.Rows[dgvLineasCreditoCompra.CurrentRow.Index].Cells["Completado"].Value))
                    {
                        RegistrarPago registrarPago = new RegistrarPago(
                            Convert.ToInt32(dgvLineasCreditoCompra.CurrentRow.Cells["LineaCreditoCompraID"].Value)
                        , dgvLineasCreditoCompra.CurrentRow.Cells["Proveedor"].Value.ToString()
                        , Convert.ToDecimal(dgvLineasCreditoCompra.CurrentRow.Cells["BalancePendiente"].Value));
                        registrarPago.ShowDialog();
                        CargarTodasLineasCreditoCompra();

                    }
                    else
                    {
                        MessageBox.Show("La linea de credito compra ya esta saldada.", "Linea Saldada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Debe de seleccionar la linea de credito compra a la que desea realizar el pago.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: No se ha podido realizar el pago de esta linea de credito, intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void btnHistorialPagos_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLineasCreditoCompra.SelectedRows.Count > 0)
                {
                    HistorialPagos historialPagos = new HistorialPagos(
                        Convert.ToInt32(dgvLineasCreditoCompra.CurrentRow.Cells["LineaCreditoCompraID"].Value)
                        , dgvLineasCreditoCompra.CurrentRow.Cells["Proveedor"].Value.ToString()
                        , Convert.ToDecimal(dgvLineasCreditoCompra.CurrentRow.Cells["BalancePendiente"].Value)
                        , Convert.ToBoolean(dgvLineasCreditoCompra.CurrentRow.Cells["Completado"].Value));
                    historialPagos.ShowDialog();
                    CargarTodasLineasCreditoCompra();
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Ha ocurrido un error intentando abrir el historial de cobro, intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CargarCBFiltro()
        {
            cbFiltro.Items.Add("ID");
            cbFiltro.Items.Add("Proveedor");
            cbFiltro.Items.Add("Fecha");
            cbFiltro.Items.Add("Factura Compra");
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
                            dgvLineasCreditoCompra.DataSource = proc_CargarTodasLineasCreditoCompras_Results.Where(p => p.LineaCreditoCompraID.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Proveedor":
                            dgvLineasCreditoCompra.DataSource = proc_CargarTodasLineasCreditoCompras_Results.Where(p => p.Proveedor.ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Fecha":
                            dgvLineasCreditoCompra.DataSource = proc_CargarTodasLineasCreditoCompras_Results.Where(p => p.Fecha.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Factura Compra":
                            dgvLineasCreditoCompra.DataSource = proc_CargarTodasLineasCreditoCompras_Results.Where(p => p.FacturaCompra.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Completado":
                            dgvLineasCreditoCompra.DataSource = proc_CargarTodasLineasCreditoCompras_Results.Where(p => p.Completado.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
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
