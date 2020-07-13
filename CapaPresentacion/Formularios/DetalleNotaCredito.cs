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
    public partial class DetalleNotaCredito : Form
    {
        NotasDeCreditoNegocio notasDeCreditoNegocio = new NotasDeCreditoNegocio();
        List<proc_ComprobanteNotaDeCredito_Result> proc_ComprobanteNotaDeCredito_Results;
        int notaCreditoID;
        CultureInfo ci = new CultureInfo("en-us");

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        public DetalleNotaCredito(int notaCreditoID)
        {
            InitializeComponent();
            this.notaCreditoID = notaCreditoID;
            LlenarDataGridView();
            CargarTextBoxs();
        }

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LlenarDataGridView()
        {
            try
            {
                dgvCarrito.Rows.Clear();
                proc_ComprobanteNotaDeCredito_Results = notasDeCreditoNegocio.CargarComprobanteNotaDeCredito(notaCreditoID).ToList();
                if (proc_ComprobanteNotaDeCredito_Results.Count > 0)
                {
                    foreach (var item in proc_ComprobanteNotaDeCredito_Results)
                    {
                        dgvCarrito.Rows.Add(item.ProductoID, item.Referencia, item.Descripcion, item.UnidadMedida, item.CantDevuelta,
                            item.Precio, (Convert.ToDecimal(item.CantDevuelta) * (item.Precio))?.ToString("F"),item.Comentario);
                    }
                }
                dgvCarrito.AutoGenerateColumns = false;
                dgvCarrito.Columns["Precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvCarrito.Columns["CantDevuelta"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvCarrito.Columns["Importe"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvCarrito.Refresh();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void CargarTextBoxs()
        {
            try
            {              
               if(proc_ComprobanteNotaDeCredito_Results.FirstOrDefault().ITBIS)
                {
                    txtITBIS.Text = (proc_ComprobanteNotaDeCredito_Results.FirstOrDefault().PrecioTotal * (Properties.Settings.Default.ITBIS / 100))?.ToString("C", ci);
                }
               else
                {
                    txtITBIS.Text = (0).ToString("C", ci);
                }                  
                
                txtTotal.Text = proc_ComprobanteNotaDeCredito_Results.FirstOrDefault().PrecioTotal?.ToString("C", ci);
                txtCantProd.Text = proc_ComprobanteNotaDeCredito_Results.Count.ToString();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void DetalleNotaCredito_Load(object sender, EventArgs e)
        {

        }
    }
}
