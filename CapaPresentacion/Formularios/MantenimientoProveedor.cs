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

namespace CapaPresentacion
{
    public partial class MantenimientoProveedor : Form
    {
        ProveedoresNegocio proveedoresNegocio = new ProveedoresNegocio();
        Proveedore proveedorEntidad = new Proveedore();
        private bool respuesta;
        private int proveedorID;
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        public MantenimientoProveedor()
        {
            InitializeComponent();
        }

        public MantenimientoProveedor(Proveedore proveedor)
        {
            InitializeComponent();
            LlenarTextBoxs(proveedor);
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
                AgregarProveedor();

            }
            else if (btnAplicar.Text == "Editar")
            {
                EditarProveedor();
            }
        }

        private void AgregarProveedor()
        {
            try
            {
                if (ValidarCampos())
                {
                    DialogResult dialogResult = MessageBox.Show("Esta seguro que desea agregar" +
                        " nuevo proveedor a la base de datos?", "Nuevo Proveedor", MessageBoxButtons.OKCancel);
                    if (dialogResult == DialogResult.OK)
                    {
                        proveedorEntidad.Nombre = txtNombre.Text;
                        proveedorEntidad.CedulaORnc = txtCedulaORnc.Text;
                        proveedorEntidad.Direccion = txtDireccion.Text;
                        proveedorEntidad.Contacto_1 = txtContacto1.Text;
                        proveedorEntidad.Contacto_2 = txtContacto2.Text;
                        proveedorEntidad.DatoAdicional = txtDatoAdicional.Text;


                        var result = proveedoresNegocio.AgregarProveedor(proveedorEntidad);

                        respuesta = result.Item1;
                        proveedorID = result.Item2;
                        ValidarProveedorAgregado(respuesta, proveedorID);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: Proveedor no pudo ser agregado, verifique que el proveedor no exista e intente de nuevo por favor.",
                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());

            }

        }

        private void ValidarProveedorAgregado(bool respuesta, int proveedorID)
        {
            if (respuesta)
            {
                MessageBox.Show(string.Format("El proveedor ha sido agregado correctamente a la base de datos, con el numero de proveedor:{0}", proveedorID), "Proveedor Agregado Correctamente!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("El proveedor no pudo ser agregado a la base de datos, favor de verificar los requerimientros", "Ha Ocurrido un error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void EditarProveedor()
        {
            try
            {
                if (ValidarCampos())
                {
                    DialogResult dialogResult = MessageBox.Show("Esta seguro que desea editar este proveedor?",
                        "Editar Proveedor", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.OK)
                    {

                        proveedorEntidad.ProveedorID = Convert.ToInt32(txtID.Text);
                        proveedorEntidad.Nombre = txtNombre.Text;
                        proveedorEntidad.CedulaORnc = txtCedulaORnc.Text;
                        proveedorEntidad.Direccion = txtDireccion.Text;
                        proveedorEntidad.Contacto_1 = txtContacto1.Text;
                        proveedorEntidad.Contacto_2 = txtContacto2.Text;
                        proveedorEntidad.DatoAdicional = txtDatoAdicional.Text;

                        respuesta = proveedoresNegocio.EditarProveedor(proveedorEntidad);
                        ValidarEditarProveedor(respuesta);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: Proveedor no pudo ser editado, verifique los campos e intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }

        }

        private void ValidarEditarProveedor(bool result)
        {
            if (result)
            {
                MessageBox.Show(string.Format("El proveedor ha sido editado correctamente en la base de datos."), "Proveedor Editado Correctamente!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("El proveedor no pudo ser editado, favor de verificar los requerimientros", "Ha Ocurrido un error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("El Campo NOMBRE no puede estar vacio");
                return false;
            }
            return true;
        }

        private void LlenarTextBoxs(Proveedore proveedor)
        {
            txtID.Text = proveedor.ProveedorID.ToString();
            txtNombre.Text = proveedor.Nombre;
            txtCedulaORnc.Text = proveedor.CedulaORnc;
            txtDireccion.Text = proveedor.Direccion;
            txtContacto1.Text = proveedor.Contacto_1;
            txtContacto2.Text = proveedor.Contacto_2;
            txtDatoAdicional.Text = proveedor.DatoAdicional;
        }

        private void MantenimientoProveedor_Load(object sender, EventArgs e)
        {
            txtNombre.Select();
        }
               
    }
}

