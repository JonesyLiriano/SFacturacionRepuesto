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

namespace SFacturacion
{
    public partial class Login : Form
    {
        UsersNegocio usersNegocios = new UsersNegocio();
        User usersEntidad = new User();
        bool respuesta;
        public static int userLevel;
        public static int userID;

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
            if (Settings.Default.Recuerdame)
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
        }
        private void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                Me
                Validar();
                ValidarRecuerdame();

            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                loggeator.EscribeEnArchivo(exc.ToString());
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
                userID = resultado.Item2.UserID;
                userLevel = resultado.Item2.UserLevel;
                Principal menu = new Principal();
                this.ShowInTaskbar = false;
                this.Hide();
                menu.Show();
            }
            else
            {
                MessageBox.Show("Usuario incorecto, intente nuevamente...", "Error", MessageBoxButtons.OK);
                txtPassword.Clear();
                txtUsuario.Clear();
                txtUsuario.Select();
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
