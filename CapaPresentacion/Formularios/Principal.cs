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
using CapaPresentacion.Formularios;

namespace SFacturacion
{
    public partial class Principal : Form
    {
        private Button btnActual;
        private Panel bordeIzquierdoBtn;
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        public Principal()
        {
            InitializeComponent();
            bordeIzquierdoBtn = new Panel();
            bordeIzquierdoBtn.Size = new Size(7, 60);
            MenuVertical.Controls.Add(bordeIzquierdoBtn);
        }      

        private void ActivarScrollBar()
        {
            MenuVertical.AutoScroll = false;
            MenuVertical.HorizontalScroll.Enabled = false;
            MenuVertical.HorizontalScroll.Visible = false;
            MenuVertical.HorizontalScroll.Maximum = 0;
            MenuVertical.AutoScroll = true;
        }

        private void DesactivarScrollBar()
        {
            MenuVertical.AutoScroll = false;            
        }

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                if (MenuVertical.Width == 250)
                {
                    MenuVertical.Width = 90;
                }
                else
                {
                    MenuVertical.Width = 250;
                }
            } else
            {
                if (MenuVertical.Width == 250)
                {
                    MenuVertical.Width = 70;
                }
                else
                {
                    MenuVertical.Width = 250;
                }
            }     
            SeleccionarButton(panelContenedor.Controls[0].Text);
        }

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iconmaximizar_Click(object sender, EventArgs e)
        {
            if (MenuVertical.Width == 90)
            {
                MenuVertical.Width = 70;
            }
            DesactivarScrollBar();
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;
            iconrestaurar.Visible = true;
            iconmaximizar.Visible = false;
        }

        private void iconrestaurar_Click(object sender, EventArgs e)
        {            
            ActivarScrollBar();
            this.WindowState = FormWindowState.Normal;
            iconrestaurar.Visible = false;
            iconmaximizar.Visible = true;
        }

        private void iconminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }       

        private void AbrirFormEnPanel<MiForm>() where MiForm : Form, new()
        {
            Form fh;
            fh = panelContenedor.Controls.OfType<MiForm>().FirstOrDefault();

            if (fh == null)
            {
                fh = new MiForm();
                fh.TopLevel = false;
                fh.Dock = DockStyle.Fill;
                this.panelContenedor.Controls.Add(fh);
                this.panelContenedor.Tag = fh;
                fh.Show();
                fh.BringToFront();
            }
            else
            {
                fh.Activate();
                fh.BringToFront();
                
            }

        }

        private void btnprod_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<Productos>();
            SeleccionarButton("Productos");
        }

        private void btnlogoInicio_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<InicioResumen>();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            ActivarScrollBar();
            btnlogoInicio_Click(null, e);

        }

        private void SeleccionarButton(string button)
        {
            switch (button.Trim())
            {
                case "Productos":
                    ActivarButton(btnProductos);
                    break;
                case "Ventas":
                    ActivarButton(btnVentas);
                    break;
                case "Clientes":
                    ActivarButton(btnClientes);
                    break;
                case "Proveedores":
                    ActivarButton(btnProveedores);
                    break;
                case "Compras":
                    ActivarButton(btnCompras);
                    break;
                case "Usuarios":
                    ActivarButton(btnUsuarios);
                    break;
                case "InicioResumen":
                    ActivarButton(btnPrincipal);
                    break;
                case "Configuraciones":
                    ActivarButton(btnConfiguraciones);
                    break;
                case "Cotizaciones":
                    ActivarButton(btnCotizaciones);
                    break;
                case "Facturas":
                    ActivarButton(btnFacturas);
                    break;
                default:
                    break;
            }
        }

        private void ActivarButton(object senderBtn)
        {
            if (senderBtn != null)
            {
                DesactivarButton();
                btnActual = (Button)senderBtn;
                btnActual.BackColor = Color.FromArgb(37, 36, 81);
                if (MenuVertical.Width == 250) 
                {
                    btnActual.TextAlign = ContentAlignment.MiddleCenter;
                    btnActual.ImageAlign = ContentAlignment.MiddleRight;
                    btnActual.TextImageRelation = TextImageRelation.TextBeforeImage;
                } else
                {
                    btnActual.TextAlign = ContentAlignment.MiddleCenter;
                    btnActual.ImageAlign = ContentAlignment.MiddleLeft;
                    btnActual.TextImageRelation = TextImageRelation.Overlay;
                }
               

                bordeIzquierdoBtn.BackColor = Color.White;
                bordeIzquierdoBtn.Location = new Point(0, btnActual.Location.Y);
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
            AbrirFormEnPanel<RegistrarVenta>();
            SeleccionarButton("Ventas");
            CerrarFormulariosCotizacionYFacturas();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<Clientes>();
            SeleccionarButton("Clientes");
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<Proveedores>();
            SeleccionarButton("Proveedores");
        }

        private void btnCompras_Click(object sender, EventArgs e)
        {
            SeleccionarButton("Compras");
        }
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<Usuarios>();
            SeleccionarButton("Usuarios");
        }

        private void panelContenedor_ControlRemoved(object sender, ControlEventArgs e)
        {
            SeleccionarButton(panelContenedor.Controls[0].Text);
        }

        private void btnPrincipal_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<InicioResumen>();
            SeleccionarButton("InicioResumen");
        }

        private void btnConfiguraciones_Click(object sender, EventArgs e)
        {

            AbrirFormEnPanel<Configuraciones>();
            SeleccionarButton("Configuraciones");
        }

        private void btnCotizaciones_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<Cotizaciones>();
            SeleccionarButton("Cotizaciones");
        }

        private void CerrarFormulariosCotizacionYFacturas()
        {
            foreach (Control item in panelContenedor.Controls)
            {
               if (item.Text == "Cotizaciones" || item.Text == "Facturas")
                {
                    panelContenedor.Controls.Remove(item);
                }
            }            
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<Facturas>();
            SeleccionarButton("Facturas");
        }
    }
}
