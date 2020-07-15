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
    public partial class MantenimientoUsuario : Form
    {
        UsersNegocio usersNegocio = new UsersNegocio();
        User userEntidad = new User();
        int usuarioID;
        bool respuesta;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        public MantenimientoUsuario()
        {
            InitializeComponent();
            CargarComboBox();
        }

        public MantenimientoUsuario(User usuario)
        {
            InitializeComponent();
            CargarComboBox();
            LlenarTextBoxs(usuario);
        }

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MantenimientoUsuario_Load(object sender, EventArgs e)
        {            
            txtUsuario.Select();
        }

        private void CargarComboBox()
        {
            cbNivel.Items.Add("Usuario");
            cbNivel.Items.Add("Administrador");
            cbNivel.SelectedIndex = 0;            
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (btnAplicar.Text == "Agregar")
            {
                AgregarUsuario();

            }
            else if (btnAplicar.Text == "Editar")
            {
                EditarUsuario();
            }
        }

        private void AgregarUsuario()
        {
            try
            {

                if (ValidarCampos())
                {
                    DialogResult dialogResult = MessageBox.Show("Esta seguro que desea agregar usuario?",
                        "Agregar Usuario", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.OK)
                    {

                        userEntidad.UserName = txtUsuario.Text;
                        userEntidad.UserPassword = txtPassword.Text;
                        userEntidad.UserLevel = cbNivel.SelectedItem.ToString();
                        var result = usersNegocio.AgregarUser(userEntidad);

                        respuesta = result.Item1;
                        usuarioID = result.Item2;
                        ValidarUsuarioAgregado(respuesta, usuarioID);

                    }
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: Usuario no pudo ser agregado, verifique que el usuario no exista e intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void ValidarUsuarioAgregado(bool respuesta, int usuarioID)
        {
            if (respuesta)
            {
                MessageBox.Show(string.Format("El usuario ha sido agregado correctamente a la base de datos, con el numero de usuario:{0}", usuarioID), "Usuario Agregado Correctamente!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("El usuario no pudo ser agregado a la base de datos, favor de verificar los requerimientros", "Ha Ocurrido un error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void EditarUsuario()
        {
            try
            {
                if (ValidarCampos())
                {
                    DialogResult dialogResult = MessageBox.Show("Esta seguro que desea editar usuario?",
                        "Editar Usuario", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.OK)
                    {

                        userEntidad.UserID = Convert.ToInt32(txtID.Text);
                        userEntidad.UserName = txtUsuario.Text;
                        userEntidad.UserPassword = txtPassword.Text;
                        userEntidad.UserLevel = cbNivel.SelectedItem.ToString();
                        respuesta = usersNegocio.EditarUser(userEntidad);
                        ValidarEditarUsuario(respuesta);

                    }
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: Usuario no pudo ser editado, verifique los campos e intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }
        private void ValidarEditarUsuario(bool result)
        {
            if (result)
            {
                MessageBox.Show(string.Format("El usuario ha sido editado correctamente en la base de datos."), "Usuario Editado Correctamente!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("El usuario no pudo ser editado, favor de verificar los requerimientros", "Ha Ocurrido un error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LlenarTextBoxs(User usuario)
        {
            txtID.Text = usuario.UserID.ToString();
            txtUsuario.Text = usuario.UserName;
            txtPassword.Text = usuario.UserPassword;
            cbNivel.SelectedItem = usuario.UserLevel;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("El Campo Usuario Y Password no pueden estar vacio");
                return false;
            }
            return true;
        }

        private void cbNivel_Validating(object sender, CancelEventArgs e)
        {
            if (cbNivel.SelectedIndex == -1 && cbNivel.Items.Count > 0)
            {
                cbNivel.Focus();
            }
        }
    }
}
