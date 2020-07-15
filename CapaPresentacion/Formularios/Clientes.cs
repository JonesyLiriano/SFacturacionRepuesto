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
        List<proc_CargarTodosClientes_Result> proc_CargarTodosClientes_Results;
        Cliente clienteEntidad = new Cliente();
        bool resultado;

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
            CargarClientes();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {   if(dgvClientes.SelectedRows.Count > 0)
            {
                MantenimientoCliente frmMantCliente = new MantenimientoCliente(CargarParametrosCliente());
                frmMantCliente.Controls["btnAplicar"].Text = "Editar";
                frmMantCliente.ShowDialog();
                CargarClientes();
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
                proc_CargarTodosClientes_Results = clientesNegocio.CargarTodosClientes().ToList();
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
        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(txtFiltro.Text != "Escriba para filtrar...")
                {
                    switch (cbFiltro.SelectedItem.ToString())
                    {
                        case "ID":
                            dgvClientes.DataSource = proc_CargarTodosClientes_Results.Where(p => p.ClienteID.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Nombre":
                            dgvClientes.DataSource = proc_CargarTodosClientes_Results.Where(p => p.Nombre.ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Cedula o RNC":
                            dgvClientes.DataSource = proc_CargarTodosClientes_Results.Where(p => p.CedulaORnc.ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Direccion":
                            dgvClientes.DataSource = proc_CargarTodosClientes_Results.Where(p => p.Direccion.ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        default:
                            break;
                    }
                    OrdenarColumnasDGV();
                }                
            }
            catch (Exception exc)
            {
                MessageBox.Show("No se pudo completar la busqueda, intentelo de nuevo por favor.",
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
