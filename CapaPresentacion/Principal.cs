using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using CapaDatos;
using CapaPresentacion;

namespace SFacturacion
{
    public partial class Principal : Form
    {
        private Button btnActual;
        private Panel bordeIzquierdoBtn;

        public Principal()
        {
            InitializeComponent();
            bordeIzquierdoBtn = new Panel();
            bordeIzquierdoBtn.Size = new Size(7, 60);
            MenuVertical.Controls.Add(bordeIzquierdoBtn);

         
        }
        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (MenuVertical.Width == 250)
            {
                MenuVertical.Width = 70;
            }
            else
                MenuVertical.Width = 250;
        }

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconmaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            iconrestaurar.Visible = true;
            iconmaximizar.Visible = false;
        }

        private void iconrestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            iconrestaurar.Visible = false;
            iconmaximizar.Visible = true;
        }

        private void iconminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        public void AbrirFormEnPanel<MiForm>() where MiForm: Form, new()
        {
            Form fh;
            fh = panelContenedor.Controls.OfType<MiForm>().FirstOrDefault();

            if(fh == null)
            {
                fh = new MiForm();
                fh.TopLevel = false;
                fh.Dock = DockStyle.Fill;
                this.panelContenedor.Controls.Add(fh);
                this.panelContenedor.Tag = fh;
                fh.Show();
                fh.BringToFront();
            } else
            {
                fh.BringToFront();
            }
            
        }

        private void btnprod_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<Productos>();
            ActivarButton(sender);
        }

        private void btnlogoInicio_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<InicioResumen>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnlogoInicio_Click(null, e);
            
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            btnlogoInicio_Click(null, e);
        }


        private void ActivarButton(object senderBtn)
        {
            if(senderBtn != null)
            {
                DesactivarButton();
                btnActual = (Button)senderBtn;
                btnActual.BackColor = Color.FromArgb(37, 36, 81);
                btnActual.TextAlign = ContentAlignment.MiddleCenter;
                btnActual.ImageAlign = ContentAlignment.MiddleRight;
                btnActual.TextImageRelation = TextImageRelation.TextBeforeImage;

                bordeIzquierdoBtn.BackColor = Color.White;
                bordeIzquierdoBtn.Location = new Point(0,btnActual.Location.Y);
                bordeIzquierdoBtn.Visible = true;
                bordeIzquierdoBtn.BringToFront();

                labelBarraMenu.Text = btnActual.Text;
                pbBarraMenu.Image = btnActual.Image;

            }

        }

        private void DesactivarButton()
        {
            if (btnActual != null)
            {
                btnActual.BackColor = Color.FromArgb(32, 30, 45);
                btnActual.TextAlign = ContentAlignment.MiddleCenter;
                btnActual.ImageAlign = ContentAlignment.MiddleLeft;
                btnActual.TextImageRelation = TextImageRelation.Overlay;
                pbBarraMenu.Image = null;
                labelBarraMenu.Text = "";

            }

        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<Clientes>();
            ActivarButton(sender);
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<Clientes>();
            ActivarButton(sender);
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<Proveedores>();
            ActivarButton(sender);
        }

        private void btnCompras_Click(object sender, EventArgs e)
        {
            ActivarButton(sender);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<InicioResumen>();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<Usuarios>();
            ActivarButton(sender);
        }
    }
}
