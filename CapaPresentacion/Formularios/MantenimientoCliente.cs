using CapaDatos;
using CapaNegocios;
using SFacturacion;
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

namespace CapaPresentacion
{
    public partial class MantenimientoCliente : Form
    {
     
        Cliente clienteEntidad = new Cliente();
        ClientesNegocio clientesNegocio = new ClientesNegocio();
        private bool respuesta;
        private int clienteID;
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        public MantenimientoCliente()
        {
            InitializeComponent();
        }
        public MantenimientoCliente(Cliente cliente)
        {
            InitializeComponent();
            LlenarTextBoxs(cliente);
        }
        

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (btnAplicar.Text == "Agregar")
            {
                AgregarCliente();

            } else if (btnAplicar.Text == "Editar")
            {
                EditarCliente();
            }
        }

        private void EditarCliente()
        {
            try
            {
                if (ValidarCampos())
                {
                    DialogResult dialogResult = MessageBox.Show("Esta seguro que desea editar este cliente?",
                        "Editar Cliente", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.OK)
                    {
                        clienteEntidad.ClienteID = Convert.ToInt32(txtID.Text);
                        clienteEntidad.Nombre = txtNombre.Text;
                        clienteEntidad.CedulaORnc = txtCedulaORnc.Text;
                        clienteEntidad.Direccion = txtDireccion.Text;
                        clienteEntidad.Contacto_1 = txtContacto1.Text;
                        clienteEntidad.Contacto_2 = txtContacto2.Text;
                        if (!string.IsNullOrEmpty(txtDescuento.Text))
                            clienteEntidad.Descuento = Convert.ToDouble(txtDescuento.Text);
                        else
                        {
                            clienteEntidad.Descuento = 0;
                        }
                        if (!string.IsNullOrEmpty(txtCredito.Text))
                            clienteEntidad.Credito = Convert.ToDouble(txtCredito.Text);
                        else
                        {
                            clienteEntidad.Credito = 0;
                        }

                        bool result = clientesNegocio.EditarCliente(clienteEntidad);
                        ValidarEditarCliente(result);
                        this.Close();
                    }
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: Cliente no pudo ser editado, verifique los campos e intente de nuevo por favor. ",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }

        }

        private void ValidarEditarCliente(bool result)
        {
            if (result)
            {
                MessageBox.Show(string.Format("El cliente ha sido editado correctamente en la base de datos."), "Cliente Editado Correctamente!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("El cliente no pudo ser editado, favor de verificar los requerimientros", "Ha Ocurrido un error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AgregarCliente()
        {
            try
            {
                if (ValidarCampos())
                {
                    DialogResult dialogResult = MessageBox.Show("Esta seguro que desea agregar nuevo cliente?",
                        "Nuevo Cliente", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.OK)
                    {
                        clienteEntidad.Nombre = txtNombre.Text;
                        clienteEntidad.CedulaORnc = txtCedulaORnc.Text;
                        clienteEntidad.Direccion = txtDireccion.Text;
                        clienteEntidad.Contacto_1 = txtContacto1.Text;
                        clienteEntidad.Contacto_2 = txtContacto2.Text;
                        clienteEntidad.Contacto_2 = txtContacto2.Text;

                        if (!string.IsNullOrEmpty(txtDescuento.Text))
                            clienteEntidad.Descuento = Convert.ToDouble(txtDescuento.Text);
                        else
                        {
                            clienteEntidad.Descuento = 0;
                        }
                        if (!string.IsNullOrEmpty(txtCredito.Text))
                            clienteEntidad.Credito = Convert.ToDouble(txtCredito.Text);
                        else
                        {
                            clienteEntidad.Credito = 0;
                        }
                        var result = clientesNegocio.AgregarCliente(clienteEntidad);

                        respuesta = result.Item1;
                        clienteID = result.Item2;
                        ValidarClienteAgregado(respuesta, clienteID);                        
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: Cliente no pudo ser agregado, verifique que el cliente no exista e intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }

        }

        private void ValidarClienteAgregado(bool respuesta, int clienteID)
        {
            if (respuesta)
            {

                MessageBox.Show(string.Format("El cliente ha sido agregado correctamente a la base de datos, con el numero de cliente:{0}", clienteID), "Cliente Agregado Correctamente!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("El cliente no pudo ser agregado a la base de datos, favor de verificar los requerimientros", "Ha Ocurrido un error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("El Campo Nombre no puede estar vacío");
                return false;
            }
            return true;
        }

        private void MantenimientoCliente_Load(object sender, EventArgs e)
        {
            txtNombre.Select();
        }

        private void LlenarTextBoxs(Cliente cliente)
        {
            txtID.Text = cliente.ClienteID.ToString();
            txtNombre.Text = cliente.Nombre;
            txtCedulaORnc.Text = cliente.CedulaORnc;
            txtDireccion.Text = cliente.Direccion;
            txtContacto1.Text = cliente.Contacto_1;
            txtContacto2.Text = cliente.Contacto_2;
            txtDescuento.Text = cliente.Descuento.ToString();
            txtCredito.Text = cliente.Credito.ToString();
        }

    }
}
