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
                MessageBox.Show("Error: " + exc.ToString(),
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void CargarNotasDeCredito()
        {
            try
            {
                proc_CargarTodasNotasDeCredito_Results = notasDeCreditoNegocio.CargarTodasNotasDeCredito().ToList();
                dgvNotasCredito.DataSource = proc_CargarTodasNotasDeCredito_Results;

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
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.ToString(),
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
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
                MessageBox.Show("Error: " + exc.ToString(),
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
                MessageBox.Show("Error: " + exc.ToString(),
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }

        }
        private void NotasCredito_Load(object sender, EventArgs e)
        {
            CargarNotasDeCredito();
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
                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
