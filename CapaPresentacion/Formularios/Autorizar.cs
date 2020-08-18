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
    public partial class Autorizar : Form
    {
        UsersNegocio usersNegocios = new UsersNegocio();
        User usersEntidad = new User();
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
        public Autorizar()
        {
            InitializeComponent();
        }

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Autorizar_Load(object sender, EventArgs e)
        {
            txtUsuario.Focus();
        }

        private void Validar()
        {
            usersEntidad.UserName = txtUsuario.Text;
            usersEntidad.UserPassword = txtPassword.Text;
            //se verifica el usuario
            var resultado = usersNegocios.ValidarUsuario(usersEntidad);
            respuesta = resultado.Item1;

            if (respuesta && resultado.Item2.UserLevel == "Administrador")
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuario incorecto || Usuario no es administrador, intente nuevamente...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtUsuario.Clear();
                txtUsuario.Focus();
            }

        }

        private void btnAutorizar_Click(object sender, EventArgs e)
        {
            try
            {
                Validar();                

            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: Verifique que la base de datos se encuentre en la ubicacion correcta.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    this.Close();
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }

        }
    }
}
