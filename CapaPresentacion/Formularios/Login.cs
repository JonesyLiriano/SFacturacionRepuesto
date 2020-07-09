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
using CapaNegocios;
using CapaDatos;
using CapaPresentacion.Properties;
using System.Runtime.InteropServices;

namespace SFacturacion
{
    public partial class Login : Form
    {
        UsersNegocio usersNegocios = new UsersNegocio();
        User usersEntidad = new User();
        bool respuesta;
        public static string userLevel;
        public static int userID;
        public static string userName;
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtUsuario.Select();
            CargarRecuerdame();
        }

        private void CargarRecuerdame()
        {
            if(Settings.Default.Recuerdame)
            {
                checkboxRecuerdame.Checked = true;
                txtUsuario.Text = Settings.Default.Usuario;
                txtPassword.Text = Settings.Default.Password;
            }

        }
        private void iconminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

     
        private void ValidarRecuerdame()
        {
            if (checkboxRecuerdame.Checked)
            {
                Settings.Default.Usuario = txtUsuario.Text;
                Settings.Default.Password = txtPassword.Text;
                Settings.Default.Recuerdame = checkboxRecuerdame.Checked;

            } else
            {
                Settings.Default.Usuario = "";
                Settings.Default.Password = "";
                Settings.Default.Recuerdame = false;
            }
            Settings.Default.Save();
        }
        private void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                Validar();
                ValidarRecuerdame();

            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void Validar()
        {
            usersEntidad.UserName = txtUsuario.Text;
            usersEntidad.UserPassword = txtPassword.Text;
            //se verifica el usuario
            var resultado = usersNegocios.ValidarUsuario(usersEntidad);
            respuesta = resultado.Item1;

            if (respuesta)
            {
                this.ShowInTaskbar = false;
                this.Hide();
                userID = resultado.Item2.UserID;
                userLevel = resultado.Item2.UserLevel;
                userName = resultado.Item2.UserName;
                Principal menu = new Principal(userLevel);
                menu.Show();
            }
            else
            {
                MessageBox.Show("Usuario incorecto, intente nuevamente...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtUsuario.Clear();
                txtUsuario.Select();
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
