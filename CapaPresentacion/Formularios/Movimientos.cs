using CapaDatos;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Formularios
{
    public partial class Movimientos : Form
    {
        MovimientoNegocio movimientoNegocio = new MovimientoNegocio();
        List<proc_CargarMovimientos_Result> proc_CargarMovimientos_Results;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        public Movimientos(int productoID)
        {
            InitializeComponent();
            CargarMovimientos(productoID);
        }

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Movimientos_Load(object sender, EventArgs e)
        {
           
        }
        private void CargarMovimientos(int productoID)
        {
            try
            {
                dgvMovimientos.AutoGenerateColumns = false;
                proc_CargarMovimientos_Results = movimientoNegocio.CargarMovimientos(productoID).ToList();
                dgvMovimientos.DataSource = proc_CargarMovimientos_Results;
                OrdenarColumnasDGV();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido cargar los movimiento del producto correctamente, intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }
        private void OrdenarColumnasDGV()
        {
            dgvMovimientos.Columns["Producto"].DisplayIndex = 0;
            dgvMovimientos.Columns["Fecha"].DisplayIndex = 1;
            dgvMovimientos.Columns["TipoMovimiento"].DisplayIndex = 2;
            dgvMovimientos.Columns["Referencia"].DisplayIndex = 3;
            dgvMovimientos.Columns["Existencia"].DisplayIndex = 4;
            dgvMovimientos.Columns["Cantidad"].DisplayIndex = 5;
            dgvMovimientos.Columns["Usuario"].DisplayIndex = 6;

            dgvMovimientos.Columns["Existencia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvMovimientos.Columns["Cantidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
    }
}
