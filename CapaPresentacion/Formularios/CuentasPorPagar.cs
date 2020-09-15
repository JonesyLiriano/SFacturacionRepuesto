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
        BindingList<proc_CargarTodasLineasCreditoCompras_Result> proc_CargarTodasLineasCreditoCompras_Results;
        bool finalLista;
        int indicePagina, tamanoPagina;
        string filtro, columna;
        public CuentasPorPagar()
        {
            InitializeComponent();
        }

        private void CuentasPorPagar_Load(object sender, EventArgs e)
        {
            try
            {
                proc_CargarTodasLineasCreditoCompras_Results = new BindingList<proc_CargarTodasLineasCreditoCompras_Result>();
                indicePagina = 1;
                tamanoPagina = 50;
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
            try
            {
                dgvLineasCreditoCompra.AutoGenerateColumns = false;
                List<proc_CargarTodasLineasCreditoCompras_Result> lista = lineasCreditoComprasNegocio.CargarTodasLineasCreditoCompras(indicePagina, tamanoPagina, filtro, columna).ToList();
                if (lista.Count < tamanoPagina)
                {
                    finalLista = true;
                }
                foreach (var item in lista)
                {
                    proc_CargarTodasLineasCreditoCompras_Results.Add(item);
                }
                dgvLineasCreditoCompra.DataSource = proc_CargarTodasLineasCreditoCompras_Results;

                OrdenarColumnasDGV();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido cargar las lineas de credito correctamente, intente de nuevo por favor. " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }

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
                        ResetearBusqueda();
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
                    ResetearBusqueda();
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
                ResetearBusqueda();
                CargarTodasLineasCreditoCompra();
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

        private void cbFiltro_Validating(object sender, CancelEventArgs e)
        {
            if (cbFiltro.SelectedIndex == -1 && cbFiltro.Items.Count > 0)
            {
                cbFiltro.Focus();
            }
        }

        private void btnRealizarBusqueda_Click(object sender, EventArgs e)
        {
            try
            {
                ResetearBusqueda();
                if (txtFiltro.Text != "Escriba para filtrar...")
                {
                    switch (cbFiltro.SelectedItem.ToString())
                    {
                        case "ID":
                            columna = "LineaCreditoVentaID";
                            filtro = txtFiltro.Text;
                            CargarTodasLineasCreditoCompra();
                            break;
                        case "Proveedor":
                            columna = "Proveedor";
                            filtro = txtFiltro.Text;
                            CargarTodasLineasCreditoCompra();
                            break;
                        case "Fecha":
                            columna = "Fecha";
                            filtro = txtFiltro.Text;
                            CargarTodasLineasCreditoCompra();
                            break;
                        case "Factura Compra":
                            columna = "FacturaCompra";
                            filtro = txtFiltro.Text;
                            CargarTodasLineasCreditoCompra();
                            break;
                        case "Completado":
                            columna = "Completado";
                            filtro = txtFiltro.Text;
                            CargarTodasLineasCreditoCompra();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    CargarCBFiltro();
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

        private void dgvLineasCreditoCompra_Scroll(object sender, ScrollEventArgs e)
        {
            if (!finalLista)
            {
                if ((e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement) && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
                {
                    int display = dgvLineasCreditoCompra.Rows.Count - dgvLineasCreditoCompra.DisplayedRowCount(false);
                    if (e.NewValue >= dgvLineasCreditoCompra.Rows.Count - GetDisplayedRowsCount())
                    {
                        indicePagina++;
                        CargarTodasLineasCreditoCompra();
                    }
                }
            }

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F1:
                    txtFiltro.Focus();
                    return true;
                case Keys.F5:
                    btnRealizarBusqueda.PerformClick();
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void dgvLineasCreditoCompra_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private int GetDisplayedRowsCount()
        {
            int count = dgvLineasCreditoCompra.Rows[dgvLineasCreditoCompra.FirstDisplayedScrollingRowIndex].Height;
            count = dgvLineasCreditoCompra.Height / count;
            return count;
        }

        private void ResetearBusqueda()
        {
            proc_CargarTodasLineasCreditoCompras_Results.Clear();
            finalLista = false;
            indicePagina = 1;
            filtro = null;
            columna = null;
        }
    }
}
