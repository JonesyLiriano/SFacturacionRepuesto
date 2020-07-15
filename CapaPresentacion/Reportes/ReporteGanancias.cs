using CapaDatos;
using CapaNegocios;
using Microsoft.Reporting.WinForms;
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

namespace CapaPresentacion.Reportes
{
    public partial class ReporteGanancias : Form
    {
        SistemaResumenNegocio sistemaResumenNegocio = new SistemaResumenNegocio();
        ReportParameter[] parameters = new ReportParameter[15];
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        public ReporteGanancias()
        {
            InitializeComponent();
            CargarParametros();
            CargarVistaPreviaRV();
        }

        private void CargarParametros()
        {
            var result = sistemaResumenNegocio.CalcularGanancias(Convert.ToDecimal(Properties.Settings.Default.ITBIS));            
            parameters[0] = new ReportParameter("RazonSocial", Properties.Settings.Default.RazonSocial);
            parameters[1] = new ReportParameter("CedulaORNC", Properties.Settings.Default.CedulaORnc);
            parameters[2] = new ReportParameter("DireccionEmpresa", Properties.Settings.Default.Direccion);
            parameters[3] = new ReportParameter("TelefonoEmpresa", Properties.Settings.Default.Telefono);
            parameters[4] = new ReportParameter("EmailEmpresa", Properties.Settings.Default.Email);
            parameters[5] = new ReportParameter("Logo", Properties.Settings.Default.Logo);
            parameters[6] = new ReportParameter("GananciasContDia", result.Item1.ToString());
            parameters[7] = new ReportParameter("GananciasContSemana", result.Item2.ToString());
            parameters[8] = new ReportParameter("GananciasContMes", result.Item3.ToString());
            parameters[9] = new ReportParameter("GananciasCredDia", result.Item4.ToString());
            parameters[10] = new ReportParameter("GananciasCredSemana", result.Item5.ToString());
            parameters[11] = new ReportParameter("GananciasCredMes", result.Item6.ToString());
            parameters[12] = new ReportParameter("FDia", DateTime.Today.Date.ToString("dd/MM/yyyy"));
            parameters[13] = new ReportParameter("FSemana", DateTime.Today.AddDays(-7).ToString("dd/MM/yyyy"));
            parameters[14] = new ReportParameter("FMes", DateTime.Today.AddDays(-30).ToString("dd/MM/yyyy"));
        }

        private void iconminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void iconrestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            iconrestaurar.Visible = false;
            iconmaximizar.Visible = true;
        }

        private void iconmaximizar_Click(object sender, EventArgs e)
        {
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;
            iconrestaurar.Visible = true;
            iconmaximizar.Visible = false;
        }

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CargarVistaPreviaRV()
        {
            try
            {
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                reportViewer1.LocalReport.ReportPath = @"C:/SFacturacion/reporteGanancias.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.ZoomMode = ZoomMode.PageWidth;
                reportViewer1.LocalReport.EnableExternalImages = true;
                reportViewer1.LocalReport.SetParameters(parameters);
                reportViewer1.RefreshReport();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido exportar, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor.",
                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());

            }

        }

        private void ReporteGanancias_Load(object sender, EventArgs e)
        {

        }
    }
}
