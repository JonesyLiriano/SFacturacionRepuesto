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
            CargarClientes();
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
                proc_CargarTodosClientes_Results = clientesNegocio.CargarTodosClientes().ToList();
                dgvClientes.DataSource = proc_CargarTodosClientes_Results;
                dgvClientes.Columns["Descuento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvClientes.Columns["Credito"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvClientes.Refresh();

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

                MessageBox.Show("Error: " + exc.ToString(),
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

    }
}
