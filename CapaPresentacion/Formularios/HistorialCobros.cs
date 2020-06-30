using CapaDatos;
using CapaNegocios;
using CapaPresentacion.Impresiones;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Formularios
{
    public partial class HistorialCobros : Form
    {
        List<proc_CargarCobrosVentaCredito_Result> proc_CargarCobrosVentaCredito_Results;
        CobrosVentaCreditoNegocio cobrosVentaCreditoNegocio = new CobrosVentaCreditoNegocio();
        CultureInfo ci = new CultureInfo("en-us");
        bool status, resultado;
        int lineaCreditoVentaID;
        Thread hilo;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        public HistorialCobros( int lineaCreditoVentaID, string cliente, decimal montoPendiente, bool status)
        {
            InitializeComponent();
            this.lineaCreditoVentaID = lineaCreditoVentaID;
            CargarDataGridView();            
            txtLineaCreditoVentaID.Text = this.lineaCreditoVentaID.ToString();
            txtCliente.Text = cliente;
            txtBalancePendiente.Text = montoPendiente.ToString("C", ci);
            this.status = status;
        }

        private void CargarDataGridView()
        {
            try
            {                
                proc_CargarCobrosVentaCredito_Results = cobrosVentaCreditoNegocio.CargarCobrosVentaCredito(lineaCreditoVentaID).ToList();
                dgvCobros.DataSource = proc_CargarCobrosVentaCredito_Results;
                dgvCobros.Columns["Monto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvCobros.Refresh();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }
        
        private void iconcerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }       

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCobros.SelectedRows.Count > 0)
                {
                    hilo = new Thread(() =>
                    {
                        ImpresionReciboCobro impresionReciboCobro = new ImpresionReciboCobro(Convert.ToInt32(dgvCobros.CurrentRow.Cells["CobroVentaCreditoID"].Value));
                        impresionReciboCobro.Visible = false;
                        impresionReciboCobro.ImprimirDirecto();
                    });
                    hilo.Start();

                }
                else
                {
                    MessageBox.Show("Debe de seleccionar al menos un cobro para imprimir", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                if (dgvCobros.SelectedRows.Count > 0)
                {
                    ImpresionReciboCobro impresionReciboCobro = new ImpresionReciboCobro(Convert.ToInt32(dgvCobros.CurrentRow.Cells["CobroVentaCreditoID"].Value));
                    impresionReciboCobro.ImprimirConVistaPrevia();
                    impresionReciboCobro.Show();
                }
                else
                {
                    MessageBox.Show("Debe de seleccionar al menos un cobro para imprimir", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.ToString(),
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCobros.SelectedRows.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show(string.Format("Esta seguro que desea eliminar el cobro {0}?", dgvCobros.CurrentRow.Cells["CobroVentaCreditoID"].Value), "Eliminar Cobro", MessageBoxButtons.OKCancel);
                    if (dialogResult == DialogResult.OK)
                    {
                        resultado = cobrosVentaCreditoNegocio.BorrarCobroVentaCredito(Convert.ToInt32(dgvCobros.CurrentRow.Cells["CobroVentaCreditoID"].Value));
                        CargarDataGridView();
                        ValidarBorrarCobro(resultado);
                    }
                }
                else
                {
                    MessageBox.Show("Debe de seleccionar al menos un cobro para eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }
        private void ValidarBorrarCobro(bool result)
        {
            if (result)
            {
                MessageBox.Show(string.Format("La cotizacion ha sido borrada correctamente en la base de datos."), "Cotizacion Borrada Correctamente!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("La Cotizacion no pudo ser eliminada, favor de verificar los requerimientros", "Ha Ocurrido un error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
