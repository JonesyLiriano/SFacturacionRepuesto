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
    public partial class CuentasPorCobrar : Form
    {       
        public static decimal montoPendiente;
        LineasCreditoVentasNegocio lineasCreditoVentasNegocio = new LineasCreditoVentasNegocio();
        BindingList<proc_CargarTodasLineasCreditoVentas_Result> proc_CargarTodasLineasCreditoVentas_Results;
        bool finalLista;
        int indicePagina, tamanoPagina;
        string filtro, columna;
        public CuentasPorCobrar()
        {
            InitializeComponent();
        }

        private void CuentasPorCobrar_Load(object sender, EventArgs e)
        {
            try
            {
                proc_CargarTodasLineasCreditoVentas_Results = new BindingList<proc_CargarTodasLineasCreditoVentas_Result>();
                indicePagina = 1;
                tamanoPagina = 50;
                CargarTodasLineasCreditoVenta();
                CargarCBFiltro();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido cargar las lineas de credito correctamente, intente de nuevo por favor. " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }   
      
        private void CargarTodasLineasCreditoVenta()
        {
            try
            {
                dgvLineasCreditoVenta.AutoGenerateColumns = false;
                List<proc_CargarTodasLineasCreditoVentas_Result> lista = lineasCreditoVentasNegocio.CargarTodasLineasCreditoVentas(indicePagina, tamanoPagina, filtro, columna).ToList();
                if (lista.Count < tamanoPagina)
                {
                    finalLista = true;
                }
                foreach (var item in lista)
                {
                    proc_CargarTodasLineasCreditoVentas_Results.Add(item);
                }
                dgvLineasCreditoVenta.DataSource = proc_CargarTodasLineasCreditoVentas_Results;

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
            dgvLineasCreditoVenta.Columns["LineaCreditoVentaID"].DisplayIndex = 0;
            dgvLineasCreditoVenta.Columns["Cliente"].DisplayIndex = 1;
            dgvLineasCreditoVenta.Columns["Factura"].DisplayIndex = 2;
            dgvLineasCreditoVenta.Columns["Fecha"].DisplayIndex = 3;
            dgvLineasCreditoVenta.Columns["MontoFactura"].DisplayIndex = 4;
            dgvLineasCreditoVenta.Columns["BalancePendiente"].DisplayIndex = 5;
            dgvLineasCreditoVenta.Columns["Completado"].DisplayIndex = 6;

            dgvLineasCreditoVenta.Columns["BalancePendiente"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLineasCreditoVenta.Columns["MontoFactura"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLineasCreditoVenta.Refresh();

        }
        private void btnRealizarCobro_Click(object sender, EventArgs e)
        {
            try
            {     
                if (dgvLineasCreditoVenta.SelectedRows.Count > 0)
                {
                    if (!Convert.ToBoolean(dgvLineasCreditoVenta.Rows[dgvLineasCreditoVenta.CurrentRow.Index].Cells["Completado"].Value))
                    {
                        RegistrarCobro registrarCobro = new RegistrarCobro(
                            Convert.ToInt32(dgvLineasCreditoVenta.CurrentRow.Cells["LineaCreditoVentaID"].Value)
                        , dgvLineasCreditoVenta.CurrentRow.Cells["Cliente"].Value.ToString()
                        , Convert.ToDecimal(dgvLineasCreditoVenta.CurrentRow.Cells["BalancePendiente"].Value));
                        registrarCobro.ShowDialog();
                        ResetearBusqueda();
                        CargarTodasLineasCreditoVenta();

                    }
                    else
                    {
                        MessageBox.Show("La linea de credito venta ya esta saldada.", "Linea Saldada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Debe de seleccionar la linea de credito venta a la que desea realizar el cobro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: No se ha podido realizar el cobro de esta linea de credito, intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }

        }

        private void btnHistorialCobros_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLineasCreditoVenta.SelectedRows.Count > 0)
                {
                    HistorialCobros historialCobros = new HistorialCobros(
                        Convert.ToInt32(dgvLineasCreditoVenta.CurrentRow.Cells["LineaCreditoVentaID"].Value)
                        , dgvLineasCreditoVenta.CurrentRow.Cells["Cliente"].Value.ToString()
                        , Convert.ToDecimal(dgvLineasCreditoVenta.CurrentRow.Cells["BalancePendiente"].Value)
                        , Convert.ToBoolean(dgvLineasCreditoVenta.CurrentRow.Cells["Completado"].Value));
                    historialCobros.ShowDialog();
                    ResetearBusqueda();
                    CargarTodasLineasCreditoVenta();
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Ha ocurrido un error intentando abrir el historial de cobro, intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }

        }  

        private void btnConsultaFactura_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLineasCreditoVenta.SelectedRows.Count > 0)
                {
                    DetalleFacturaCotizacion detalleFacturaCotizacion
                    = new DetalleFacturaCotizacion("Facturas", Convert.ToInt32(dgvLineasCreditoVenta.Rows[dgvLineasCreditoVenta.CurrentRow.Index].Cells["Factura"].Value));
                    detalleFacturaCotizacion.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Debe de seleccionar por lo menos una linea de credito para ver el detalle de la factura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido consultar la factura de esta linea de credito, intente de nuevo por favor.",
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
            cbFiltro.Items.Add("Cliente");
            cbFiltro.Items.Add("Fecha");
            cbFiltro.Items.Add("Factura");
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
                CargarTodasLineasCreditoVenta();
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
                            CargarTodasLineasCreditoVenta();
                            break;
                        case "Cliente":
                            columna = "Cliente";
                            filtro = txtFiltro.Text;
                            CargarTodasLineasCreditoVenta();
                            break;
                        case "Fecha":
                            columna = "Fecha";
                            filtro = txtFiltro.Text;
                            CargarTodasLineasCreditoVenta();
                            break;
                        case "Factura":
                            columna = "Factura";
                            filtro = txtFiltro.Text;
                            CargarTodasLineasCreditoVenta();
                            break;
                        case "Completado":
                            columna = "Completado";
                            filtro = txtFiltro.Text;
                            CargarTodasLineasCreditoVenta();
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

        private void dgvLineasCreditoVenta_Scroll(object sender, ScrollEventArgs e)
        {
            if (!finalLista)
            {
                if ((e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement) && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
                {
                    int display = dgvLineasCreditoVenta.Rows.Count - dgvLineasCreditoVenta.DisplayedRowCount(false);
                    if (e.NewValue >= dgvLineasCreditoVenta.Rows.Count - GetDisplayedRowsCount())
                    {
                        indicePagina++;
                        CargarTodasLineasCreditoVenta();
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

        private int GetDisplayedRowsCount()
        {
            int count = dgvLineasCreditoVenta.Rows[dgvLineasCreditoVenta.FirstDisplayedScrollingRowIndex].Height;
            count = dgvLineasCreditoVenta.Height / count;
            return count;
        }

        private void ResetearBusqueda()
        {
            proc_CargarTodasLineasCreditoVentas_Results.Clear();
            finalLista = false;
            indicePagina = 1;
            filtro = null;
            columna = null;
        }
    }
}
