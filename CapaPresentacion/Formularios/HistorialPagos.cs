using CapaDatos;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Formularios
{
    public partial class HistorialPagos : Form
    {
        List<proc_CargarPagosCompraCredito_Result> proc_CargarPagosCompraCredito_Results;
        PagosCompraCreditoNegocio pagosCompraCreditoNegocio = new PagosCompraCreditoNegocio();
        CultureInfo ci = new CultureInfo("en-us");
        private bool resultado;
        private int lineaCreditoCompraID;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        public HistorialPagos(int lineaCreditoCompraID, string proveedor, decimal montoPendiente, bool status)
        {
            InitializeComponent();
            this.lineaCreditoCompraID = lineaCreditoCompraID;
            CargarDataGridView();
            txtLineaCreditoCompraID.Text = this.lineaCreditoCompraID.ToString();
            txtProveedor.Text = proveedor;
            txtBalancePendiente.Text = montoPendiente.ToString("C", ci);
            
            if(status)
            {
                btnEliminar.Enabled = false;
            }
        }
        private void CargarDataGridView()
        {
            try
            {
                proc_CargarPagosCompraCredito_Results = pagosCompraCreditoNegocio.CargarPagosCompraCredito(lineaCreditoCompraID).ToList();
                dgvPagos.DataSource = proc_CargarPagosCompraCredito_Results;
                dgvPagos.Columns["Monto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvPagos.Refresh();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido cargar el historial de pagos, intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPagos.SelectedRows.Count > 0)
                {
                        DialogResult dialogResult = MessageBox.Show(string.Format("Esta seguro que desea eliminar el Pago {0}?", dgvPagos.CurrentRow.Cells["PagoCompraCreditoID"].Value), "Eliminar Pago", MessageBoxButtons.OKCancel);
                        if (dialogResult == DialogResult.OK)
                        {
                            resultado = pagosCompraCreditoNegocio.BorrarPagoCompraCredito(Convert.ToInt32(dgvPagos.CurrentRow.Cells["PagoCompraCreditoID"].Value));
                            CargarDataGridView();
                            ValidarBorrarPago(resultado);
                        }                   
                }
                else
                {
                    MessageBox.Show("Debe de seleccionar al menos un pago para eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("No se ha podido eliminar este pago, intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ValidarBorrarPago(bool result)
        {
            if (result)
            {
                MessageBox.Show(string.Format("El pago ha sido borrado correctamente en la base de datos."), "Pago Borrado Correctamente!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("El pago no pudo ser eliminado, favor de verificar los requerimientros", "Ha Ocurrido un error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
