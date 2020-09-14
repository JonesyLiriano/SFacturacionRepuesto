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
        BindingList<proc_CargarMovimientos_Result> proc_CargarMovimientos_Results;
        bool finalLista;
        int indicePagina, tamanoPagina, productoID;

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
            this.productoID = productoID;
            CargarMovimientos();
        }

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Movimientos_Load(object sender, EventArgs e)
        {
            proc_CargarMovimientos_Results = new BindingList<proc_CargarMovimientos_Result>();
            indicePagina = 1;
            tamanoPagina = 50;
        }
        private void CargarMovimientos()
        {
            try
            {
                dgvMovimientos.AutoGenerateColumns = false;
                List<proc_CargarMovimientos_Result> lista = movimientoNegocio.CargarMovimientos(productoID, indicePagina, tamanoPagina).ToList();
                if (lista.Count < tamanoPagina)
                {
                    finalLista = true;
                }
                foreach (var item in lista)
                {
                    proc_CargarMovimientos_Results.Add(item);
                }
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

        private void dgvMovimientos_Scroll(object sender, ScrollEventArgs e)
        {

            if (!finalLista)
            {
                if ((e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement) && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
                {
                    int display = dgvMovimientos.Rows.Count - dgvMovimientos.DisplayedRowCount(false);
                    if (e.NewValue >= dgvMovimientos.Rows.Count - GetDisplayedRowsCount())
                    {
                        indicePagina++;
                        CargarMovimientos();
                    }
                }
            }

        }
        private int GetDisplayedRowsCount()
        {
            int count = dgvMovimientos.Rows[dgvMovimientos.FirstDisplayedScrollingRowIndex].Height;
            count = dgvMovimientos.Height / count;
            return count;
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
