using CapaNegocios;
using System;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CapaPresentacion.Formularios
{
    public partial class ProductosADevolver : Form
    {
        FacturasNegocio facturasNegocio = new FacturasNegocio();
        public static DataTable dtProductosRecibidos;
        private int facturaID;       

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        public ProductosADevolver(int facturaID)
        {
            InitializeComponent();
            this.facturaID = facturaID;
        }

        private void dgvProductos_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvProductos.Rows.Count > 0)
            {
                switch (e.ColumnIndex)
                {
                    case 6:
                        if (!ValidarCeldasNumero(dgvProductos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(),
                           dgvProductos.Rows[e.RowIndex].Cells["CantVen"].Value.ToString()))
                        {
                            MessageBox.Show("La cantidad RECIBIDA no puede mayor a la cantidad VENDIDA", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        dgvProductos.Rows[e.RowIndex].Cells["Inventariada"].Value = "";
                        break;
                    case 7: 
                        if(!ValidarCeldasNumero(dgvProductos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), 
                           dgvProductos.Rows[e.RowIndex].Cells["Recibida"].Value.ToString()))
                        {   
                            MessageBox.Show("La cantidad INVENTARIADA no puede mayor a la cantidad recibida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;                            
                        }
                        break;                    
                    default:
                        break;
                }                
            }
        }

        private bool ValidarCeldasNumero(string valor1, string valor2)
        {
            decimal valor1Convertido;
            decimal valor2Convertido;

            if (!decimal.TryParse(valor1, out valor1Convertido))
            {
                MessageBox.Show("Debe de ingresar un numero valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(!decimal.TryParse(valor2, out valor2Convertido))
            {
                MessageBox.Show("Debe de ingresar un numero valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (valor1Convertido  > valor2Convertido )
            {
                return false;
            }
            return true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Esta seguro que deseas recibir estos productos ?", "Confirme", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                if (dgvProductos.Rows.Count > 0)
                {
                    dtProductosRecibidos.Rows.Clear();
                    foreach (DataGridViewRow row in dgvProductos.Rows)
                    {
                        if (Convert.ToDecimal(row.Cells["Recibida"].Value) > 0)
                            dtProductosRecibidos.Rows.Add(row);
                    }
                    this.Close();
                } else
                {
                    MessageBox.Show("Debe de haber por lo menos un producto para devolver", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void InicializarDataTableProdRecibidos()
        {
            dtProductosRecibidos = new DataTable();
            dtProductosRecibidos.Columns.Add("ProductoID", typeof(int));
            dtProductosRecibidos.Columns.Add("Descripcion", typeof(string));
            dtProductosRecibidos.Columns.Add("UnidadMedida", typeof(string));
            dtProductosRecibidos.Columns.Add("CantVen", typeof(double));
            dtProductosRecibidos.Columns.Add("Precio", typeof(decimal));
            dtProductosRecibidos.Columns.Add("PrecioSinITBIS", typeof(decimal));
            dtProductosRecibidos.Columns.Add("Recibida", typeof(double));
            dtProductosRecibidos.Columns.Add("Inventariada", typeof(double));
            dtProductosRecibidos.Columns.Add("Comentario", typeof(string));
        }

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ProductosADevolver_Load(object sender, EventArgs e)
        {
            CargarDGV();
            if (dtProductosRecibidos == null)
            {                
                InicializarDataTableProdRecibidos();
            }
            else
            {
                AgregarProdRecibidosAlDGV();
            }   
        }

        private void AgregarProdRecibidosAlDGV()
        {
            if (dgvProductos.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvProductos.Rows)
                {
                    foreach (DataRow dtRow in dtProductosRecibidos.Rows)
                    {
                        if (row.Cells["ProductoID"].Value.ToString() == dtRow["ProductoID"].ToString())
                        {
                            row.Cells["Recibido"].Value = dtRow["Recibido"].ToString();
                            row.Cells["Inventariada"].Value = dtRow["Inventariada"].ToString();
                            row.Cells["Comentario"].Value = dtRow["Comentario"].ToString();
                        }
                    }

                }
            }
        }
        private void CargarDGV()
        {
            var result = facturasNegocio.CargarProductosFactura(facturaID).ToList();
            if (result != null)
            {
                dgvProductos.DataSource = result;
                dgvProductos.Columns["CantVen"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvProductos.Columns["Precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvProductos.Columns["ProductoID"].ReadOnly = true;
                dgvProductos.Columns["Descripcion"].ReadOnly = true;
                dgvProductos.Columns["UnidadMedida"].ReadOnly = true;
                dgvProductos.Columns["Precio"].ReadOnly = true;
                dgvProductos.Columns["PrecioSinITBIS"].ReadOnly = true;
                dgvProductos.Refresh();
            }
        }
    }
}
