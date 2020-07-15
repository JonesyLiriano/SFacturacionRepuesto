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
    public partial class NotasCredito : Form
    {
        NotasDeCreditoNegocio notasDeCreditoNegocio = new NotasDeCreditoNegocio();
        List<proc_CargarTodasNotasDeCredito_Result> proc_CargarTodasNotasDeCredito_Results;
        Thread hilo;
        public NotasCredito()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                RegistrarNotaCredito registrarNotaCredito = new RegistrarNotaCredito();
                registrarNotaCredito.ShowDialog();
                CargarNotasDeCredito();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Ha ocurrido un error intentando registrar esta nota de credito, intente de nuevo por favor. ",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void CargarNotasDeCredito()
        {
            try
            {
                dgvNotasCredito.AutoGenerateColumns = false;
                proc_CargarTodasNotasDeCredito_Results = notasDeCreditoNegocio.CargarTodasNotasDeCredito().ToList();
                dgvNotasCredito.DataSource = proc_CargarTodasNotasDeCredito_Results;
                OrdenarColumnasDGV();               
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido cargar las notas de credito, intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void OrdenarColumnasDGV() 
        {
            dgvNotasCredito.Columns["NotaDeCreditoID"].DisplayIndex = 0;
            dgvNotasCredito.Columns["Cliente"].DisplayIndex = 1;
            dgvNotasCredito.Columns["Fecha"].DisplayIndex = 2;
            dgvNotasCredito.Columns["Factura"].DisplayIndex = 3;
            dgvNotasCredito.Columns["FacturaAplicada"].DisplayIndex = 4;
            dgvNotasCredito.Columns["NCF"].DisplayIndex = 5;
            dgvNotasCredito.Columns["FechaVencimiento"].DisplayIndex = 6;
            dgvNotasCredito.Columns["ITBIS"].DisplayIndex = 7;
            dgvNotasCredito.Columns["Monto"].DisplayIndex = 8;
            dgvNotasCredito.Columns["MontoAplicado"].DisplayIndex = 9;

            dgvNotasCredito.Columns["Monto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvNotasCredito.Columns["MontoAplicado"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvNotasCredito.Refresh();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvNotasCredito.SelectedRows.Count > 0)
                {
                    ImpresionNotaCredito impresionNotaCredito = new ImpresionNotaCredito(Convert.ToInt32(dgvNotasCredito.CurrentRow.Cells["NotaDeCreditoID"].Value));
                    impresionNotaCredito.ImprimirConVistaPrevia();
                    impresionNotaCredito.Show();
                }
                else
                {
                    MessageBox.Show("Debe de seleccionar al menos una nota de credito para imprimir", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido exportar la nota de creidto, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvNotasCredito.SelectedRows.Count > 0)
                {
                    hilo = new Thread(() =>
                    {
                        ImpresionNotaCredito impresionNotaCredito = new ImpresionNotaCredito(Convert.ToInt32(dgvNotasCredito.CurrentRow.Cells["NotaDeCreditoID"].Value));
                        impresionNotaCredito.Visible = false;
                        impresionNotaCredito.ImprimirDirecto();
                    });
                    hilo.Start();

                }
                else
                {
                    MessageBox.Show("Debe de seleccionar al menos una nota de credito para imprimir", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido imprimir la nota de credito, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }

        }
        private void NotasCredito_Load(object sender, EventArgs e)
        {
            try
            {
                CargarNotasDeCredito();
                CargarCBFiltro();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido cargar las notas de credito correctamente, intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void btnVerDetalles_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvNotasCredito.SelectedRows.Count > 0)
                {
                    DetalleNotaCredito detalleNotaCredito
                        = new DetalleNotaCredito(Convert.ToInt32(dgvNotasCredito.CurrentRow.Cells["NotaDeCreditoID"].Value));
                    detalleNotaCredito.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Debe de seleccionar al menos una cotizacion para ver detalle", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Ha ocurrido un error intentando ver el detalle de esta nota de credito, intente de nuevo por favor.",
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
            cbFiltro.Items.Add("Factura Aplicada");
            cbFiltro.Items.Add("NCF");
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
                            dgvNotasCredito.DataSource = proc_CargarTodasNotasDeCredito_Results.Where(p => p.NotaDeCreditoID.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Cliente":
                            dgvNotasCredito.DataSource = proc_CargarTodasNotasDeCredito_Results.Where(p => p.Cliente.ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Fecha":
                            dgvNotasCredito.DataSource = proc_CargarTodasNotasDeCredito_Results.Where(p => p.Fecha.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Factura":
                            dgvNotasCredito.DataSource = proc_CargarTodasNotasDeCredito_Results.Where(p => p.Factura.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Factura Aplicada":
                            dgvNotasCredito.DataSource = proc_CargarTodasNotasDeCredito_Results.Where(p => p.FacturaAplicada.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "NCF":
                            dgvNotasCredito.DataSource = proc_CargarTodasNotasDeCredito_Results.Where(p => p.NCF.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
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
