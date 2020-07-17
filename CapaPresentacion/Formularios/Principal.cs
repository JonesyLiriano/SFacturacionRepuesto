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

        public Principal(string userLevel)
        {
            InitializeComponent();
            bordeIzquierdoBtn = new Panel();
            bordeIzquierdoBtn.Size = new Size(7, 60);
            MenuVertical.Controls.Add(bordeIzquierdoBtn);
            if (userLevel == "Usuario")
            {
                btnProductos.Enabled = false;
                btnClientes.Enabled = false;
                btnProveedores.Enabled = false;
                btnCompras.Enabled = false;
                btnUsuarios.Enabled = false;
                btnReportes.Enabled = false;
                btnCXP.Enabled = false;
                btnConfiguraciones.Enabled = false;
            }
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
                    MenuVertical.Width = 70;
                }
                else
                {
                    MenuVertical.Width = 250;
                }
            } else
            {
                if (MenuVertical.Width == 250)
                {
                    MenuVertical.Width = 60;
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
            DialogResult result = MessageBox.Show("Esta seguro que desea salir del programa?", "Confirmacion para salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();

            }
        }

        private void iconmaximizar_Click(object sender, EventArgs e)
        {
            if (MenuVertical.Width == 90)
            {
                MenuVertical.Width = 70;
            }
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
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

        private void AbrirFormEnPanel<MiForm>() where MiForm : Form, new()
        {
            try
            {
                if (panelContenedor.Controls.Count > 1)
                {
                    CerrarFormularioResumenSistemaYConfiguraciones();
                }
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
                    fh.BringToFront();

                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: Se ha intentado abrir un formulario pero este se cerro inesperadamente, intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
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
            SeleccionarButton("InicioResumen");

        }

        private void Principal_Load(object sender, EventArgs e)
        {
            ActivarScrollBar();
            btnlogoInicio_Click(null, e);
            lbCargoUsuario.Text = Login.userLevel.ToUpper();
            lbUsuario.Text = Login.userName.ToUpper();

        }

        private void SeleccionarButton(string button)
        {
            switch (button.Trim())
            {
                case "Productos":
                    ActivarButton(btnProductos);
                    break;
                case "TabsFormularioVentas":
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
                case "NotasCredito":
                    ActivarButton(btnNotasCredito);
                    break;
                case "CuentasPorCobrar":
                    ActivarButton(btnCXC);
                    break;
                case "CuentasPorPagar":
                    ActivarButton(btnCXP);
                    break;
                case "Reportes":
                    ActivarButton(btnReportes);
                    break;
                default:
                    break;
            }
        }

        private void ActivarButton(object senderBtn)
        {
            try
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
                    }
                    else
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
            catch (Exception exc)
            {

                MessageBox.Show("Error: No se ha podido activar el boton del formulario que se intento abrir, intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }

        }
        private void DesactivarButton()
        {
            try
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
            catch (Exception exc)
            {

                MessageBox.Show("Error: No se ha podido desactivar el boton en el menu lateral, intente de nuevo por favor.. " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }

        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            CerrarFormulariosAntesAbrirVentas();
            AbrirFormEnPanel<TabsFormularioVentas>();
            SeleccionarButton("TabsFormularioVentas");
            
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
            CerrarFormularioCuentasPorPagarYProductos();
            AbrirFormEnPanel<Compras>();
            SeleccionarButton("Compras");
            
        }
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<Usuarios>();
            SeleccionarButton("Usuarios");
        }

        private void panelContenedor_ControlRemoved(object sender, ControlEventArgs e)
        {
            if(panelContenedor.Controls.Count == 0)
            {
                btnlogoInicio_Click(null, e);
            }
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

        private void CerrarFormulariosAntesAbrirVentas()
        {
            foreach (Control item in panelContenedor.Controls)
            {
               if (item.Text == "Cotizaciones" || item.Text == "Facturas" || item.Text == "CuentasPorCobrar" || item.Text == "Productos")
                {
                    panelContenedor.Controls.Remove(item);
                }
            }            
        }
        private void CerrarFormularioCuentasPorPagarYProductos()
        {
            foreach (Control item in panelContenedor.Controls)
            {
                if (item.Text == "CuentasPorPagar" || item.Text == "Productos")
                {
                    panelContenedor.Controls.Remove(item);
                }
            }
        }
        private void CerrarFormularioCuentasPorCobrarYProductos()
        {
            foreach (Control item in panelContenedor.Controls)
            {
                if (item.Text == "CuentasPorCobrar" || item.Text == "Productos")
                {
                    panelContenedor.Controls.Remove(item);
                }
            }
        }


        private void CerrarFormularioResumenSistemaYConfiguraciones()
        {
            foreach (Control item in panelContenedor.Controls)
            {
                if (item.Text == "InicioResumen" || item.Text == "Configuraciones")
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

        private void btnNotasCredito_Click(object sender, EventArgs e)
        {
            CerrarFormularioCuentasPorCobrarYProductos();
            AbrirFormEnPanel<NotasCredito>();
            SeleccionarButton("NotasCredito");
            
        }

        private void btnCXC_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<CuentasPorCobrar>();
            SeleccionarButton("CuentasPorCobrar");
        }

        private void btnCXP_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<CuentasPorPagar>();
            SeleccionarButton("CuentasPorPagar");
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<Reportes>();
            SeleccionarButton("Reportes");
        }


    }
}
