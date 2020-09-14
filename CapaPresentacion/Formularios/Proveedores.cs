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

namespace CapaPresentacion
{
    public partial class Proveedores : Form
    {
        ProveedoresNegocio proveedoresNegocio = new ProveedoresNegocio();
        BindingList<proc_CargarTodosProveedores_Result> proc_CargarTodosProveedores_Results;
        Proveedore proveedorEntidad = new Proveedore();
        bool resultado, finalLista;
        int indicePagina, tamanoPagina;
        string filtro, columna;
        public Proveedores()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            MantenimientoProveedor frmMantProveedor = new MantenimientoProveedor();
            frmMantProveedor.Controls["btnAplicar"].Text = "Agregar";
            frmMantProveedor.ShowDialog();
            CargarProveedores();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProveedores.SelectedRows.Count > 0)
            {
                MantenimientoProveedor frmMantProveedor = new MantenimientoProveedor(CargarParametrosProveedor());
                frmMantProveedor.Controls["btnAplicar"].Text = "Editar";
                frmMantProveedor.ShowDialog();
                CargarProveedores();
            }
            else
            {
                MessageBox.Show("Debe de seleccionar al menos un proveedor para editar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Proveedores_Load(object sender, EventArgs e)
        {
            try
            {
                proc_CargarTodosProveedores_Results = new BindingList<proc_CargarTodosProveedores_Result>();
                indicePagina = 1;
                tamanoPagina = 50;
                CargarProveedores();
                CargarCBFiltro();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido cargar los proveedores correctamente., intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private Proveedore CargarParametrosProveedor()
        {
            proveedorEntidad.ProveedorID = Convert.ToInt32(dgvProveedores.CurrentRow.Cells["ProveedorID"].Value);
            proveedorEntidad.Nombre = dgvProveedores.CurrentRow.Cells["Nombre"].Value.ToString();
            proveedorEntidad.CedulaORnc = dgvProveedores.CurrentRow.Cells["CedulaORnc"].Value.ToString();
            proveedorEntidad.Direccion = dgvProveedores.CurrentRow.Cells["Direccion"].Value.ToString();
            proveedorEntidad.Contacto_1 = dgvProveedores.CurrentRow.Cells["Contacto_1"].Value.ToString();
            proveedorEntidad.Contacto_2 = dgvProveedores.CurrentRow.Cells["Contacto_2"].Value.ToString();
            proveedorEntidad.DatoAdicional = dgvProveedores.CurrentRow.Cells["DatoAdicional"].Value.ToString();
            return proveedorEntidad;
        }

        private void CargarProveedores()
        {
            try
            {
                dgvProveedores.AutoGenerateColumns = false;
                List<proc_CargarTodosProveedores_Result> lista = proveedoresNegocio.CargarTodosProveedores(indicePagina, tamanoPagina, filtro, columna).ToList();
                if (lista.Count < tamanoPagina)
                {
                    finalLista = true;
                }
                foreach (var item in lista)
                {
                    proc_CargarTodosProveedores_Results.Add(item);
                }
                dgvProveedores.DataSource = proc_CargarTodosProveedores_Results;

                OrdenarColumnasDGV();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido cargar los proveedores correctamente., intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void OrdenarColumnasDGV()
        {
            dgvProveedores.Columns["ProveedorID"].DisplayIndex = 0;
            dgvProveedores.Columns["Nombre"].DisplayIndex = 1;
            dgvProveedores.Columns["CedulaORnc"].DisplayIndex = 2;
            dgvProveedores.Columns["Direccion"].DisplayIndex = 3;
            dgvProveedores.Columns["Contacto_1"].DisplayIndex = 4;
            dgvProveedores.Columns["Contacto_2"].DisplayIndex = 5;
            dgvProveedores.Columns["DatoAdicional"].DisplayIndex = 6;
            dgvProveedores.Refresh();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProveedores.SelectedRows.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show(string.Format("Esta seguro que desea eliminar el proveedor {0}?", dgvProveedores.CurrentRow.Cells["Nombre"].Value), "Eliminar Proveedor", MessageBoxButtons.OKCancel);
                    if (dialogResult == DialogResult.OK)
                    {
                        resultado = proveedoresNegocio.BorrarProveedor(Convert.ToInt32(dgvProveedores.CurrentRow.Cells["ProveedorID"].Value));
                        ValidarBorrarProveedor(resultado);
                    }
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: No se ha podido eliminar este proveedor, posiblemente este proveedor ya este relacionado con alguna factura de compra o producto.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }
        private void ValidarBorrarProveedor(bool result)
        {
            if (result)
            {
                MessageBox.Show(string.Format("El proveedor ha sido borrado correctamente en la base de datos."), "Proveedor Borrado Correctamente!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("El proveedor no pudo ser borrado, favor de verificar los requerimientros", "Ha Ocurrido un error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarCBFiltro()
        {
            cbFiltro.Items.Add("ID");
            cbFiltro.Items.Add("Nombre");
            cbFiltro.Items.Add("Cedula o RNC");
            cbFiltro.Items.Add("Direccion");
            cbFiltro.SelectedIndex = 0;
        }
        private void txtFiltro_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFiltro.Text))
            {
                txtFiltro.Text = "Escriba para filtrar...";
                txtFiltro.ForeColor = Color.Gray;
                ResetearBusqueda();
                CargarProveedores();
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
                            columna = "ClienteID";
                            filtro = txtFiltro.Text;
                            CargarProveedores();
                            break;
                        case "Nombre":
                            columna = "Nombre";
                            filtro = txtFiltro.Text;
                            CargarProveedores();
                            break;
                        case "Cedula o RNC":
                            columna = "CedulaORnc";
                            filtro = txtFiltro.Text;
                            CargarProveedores();
                            break;
                        case "Direccion":
                            columna = "Direccion";
                            filtro = txtFiltro.Text;
                            CargarProveedores();
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

        private void dgvProveedores_Scroll(object sender, ScrollEventArgs e)
        {
            if (!finalLista)
            {
                if ((e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement) && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
                {
                    int display = dgvProveedores.Rows.Count - dgvProveedores.DisplayedRowCount(false);
                    if (e.NewValue >= dgvProveedores.Rows.Count - GetDisplayedRowsCount())
                    {
                        indicePagina++;
                        CargarProveedores();
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
            int count = dgvProveedores.Rows[dgvProveedores.FirstDisplayedScrollingRowIndex].Height;
            count = dgvProveedores.Height / count;
            return count;
        }

        private void ResetearBusqueda()
        {
            proc_CargarTodosProveedores_Results.Clear();
            finalLista = false;
            indicePagina = 1;
            filtro = null;
            columna = null;
        }
    }
}
