using CapaDatos;
using CapaNegocios;
using CapaPresentacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SFacturacion
{
    public partial class Clientes : Form
    {
        ClientesNegocio clientesNegocio = new ClientesNegocio();
        BindingList<proc_CargarTodosClientes_Result> proc_CargarTodosClientes_Results;
        Cliente clienteEntidad = new Cliente();
        bool resultado, finalLista;
        int indicePagina, tamanoPagina;
        string filtro, columna;

        public Clientes()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            MantenimientoCliente frmMantCliente = new MantenimientoCliente();
            frmMantCliente.Controls["btnAplicar"].Text = "Agregar";
            frmMantCliente.ShowDialog();
            ResetearBusqueda();
            CargarClientes();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {   if(dgvClientes.SelectedRows.Count > 0)
            {
                MantenimientoCliente frmMantCliente = new MantenimientoCliente(CargarParametrosCliente());
                frmMantCliente.Controls["btnAplicar"].Text = "Editar";
                frmMantCliente.ShowDialog();
                ResetearBusqueda();
                CargarClientes(); ;
            }
            else
            {
                MessageBox.Show("Debe de seleccionar al menos un cliente para editar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            try
            {
                proc_CargarTodosClientes_Results = new BindingList<proc_CargarTodosClientes_Result>();
                indicePagina = 1;
                tamanoPagina = 50;
                CargarClientes();
                CargarCBFiltro();
            }
            catch (Exception exc)
            {

                MessageBox.Show("Ha ocurrido un error intentando cargar los clientes, intente abrir esta pestaña de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }       
        private Cliente CargarParametrosCliente()
        {
            clienteEntidad.ClienteID = Convert.ToInt32(dgvClientes.CurrentRow.Cells["ClienteID"].Value);
            clienteEntidad.Nombre = dgvClientes.CurrentRow.Cells["Nombre"].Value.ToString();
            clienteEntidad.CedulaORnc = dgvClientes.CurrentRow.Cells["CedulaORnc"].Value.ToString();
            clienteEntidad.Direccion = dgvClientes.CurrentRow.Cells["Direccion"].Value.ToString();
            clienteEntidad.Contacto_1 = dgvClientes.CurrentRow.Cells["Contacto_1"].Value.ToString();
            clienteEntidad.Contacto_2 = dgvClientes.CurrentRow.Cells["Contacto_2"].Value.ToString();
            clienteEntidad.Descuento = Convert.ToDouble(dgvClientes.CurrentRow.Cells["Descuento"].Value);
            clienteEntidad.Credito = Convert.ToDouble(dgvClientes.CurrentRow.Cells["Credito"].Value);
            return clienteEntidad;
        }

        private void CargarClientes()
        {
            try
            {
                dgvClientes.AutoGenerateColumns = false;
                List<proc_CargarTodosClientes_Result> lista = clientesNegocio.CargarTodosClientes(indicePagina, tamanoPagina, filtro, columna).ToList();
                if (lista.Count < tamanoPagina)
                {
                    finalLista = true;
                }
                foreach (var item in lista)
                {
                    proc_CargarTodosClientes_Results.Add(item);
                }
                dgvClientes.DataSource = proc_CargarTodosClientes_Results;

                OrdenarColumnasDGV();



            }
            catch (Exception exc)
            {
                MessageBox.Show("Ha ocurrido un error intentando cargar los clientes, intente abrir esta pestaña de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if(dgvClientes.SelectedRows.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show(string.Format("Esta seguro que desea eliminar el cliente {0}?", dgvClientes.CurrentRow.Cells["Nombre"].Value), "Eliminar Cliente", MessageBoxButtons.OKCancel);
                    if (dialogResult == DialogResult.OK)
                    {
                        resultado = clientesNegocio.BorrarCliente(Convert.ToInt32(dgvClientes.CurrentRow.Cells["ClienteID"].Value));
                        ValidarBorrarCliente(resultado);
                    }                        
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Cliente no puede ser eliminado, posiblemente este cliente ya esta relacionado con alguna factura, cotizacion o nota de credito.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }
        private void ValidarBorrarCliente(bool result)
        {
            if (result)
            {
                ResetearBusqueda();
                CargarClientes();
                MessageBox.Show(string.Format("El cliente ha sido borrado correctamente en la base de datos."), "Cliente Borrado Correctamente!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("El cliente no pudo ser borrado, favor de verificar los requerimientros", "Ha Ocurrido un error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                CargarClientes();
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

        private void OrdenarColumnasDGV()
        {
            dgvClientes.Columns["Descuento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvClientes.Columns["Credito"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvClientes.Refresh();
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
                            CargarClientes();
                            break;
                        case "Nombre":
                            columna = "Nombre";
                            filtro = txtFiltro.Text;
                            CargarClientes();
                            break;
                        case "Cedula o RNC":
                            columna = "CedulaORnc";
                            filtro = txtFiltro.Text;
                            CargarClientes();
                            break;
                        case "Direccion":
                            columna = "Direccion";
                            filtro = txtFiltro.Text;
                            CargarClientes();
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    CargarClientes();
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

        private void dgvClientes_Scroll(object sender, ScrollEventArgs e)
        {
            if (!finalLista)
            {
                if ((e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement) && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
                {
                    int display = dgvClientes.Rows.Count - dgvClientes.DisplayedRowCount(false);
                    if (e.NewValue >= dgvClientes.Rows.Count - GetDisplayedRowsCount())
                    {
                        indicePagina++;
                        CargarClientes();
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
            int count = dgvClientes.Rows[dgvClientes.FirstDisplayedScrollingRowIndex].Height;
            count = dgvClientes.Height / count;
            return count;
        }

        private void ResetearBusqueda()
        {
            proc_CargarTodosClientes_Results.Clear();
            finalLista = false;
            indicePagina = 1;
            filtro = null;
            columna = null;
        }
    }
}
