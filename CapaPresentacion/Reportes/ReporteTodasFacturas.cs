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
    public partial class ReporteTodasFacturas : Form
    {
        FacturasNegocio facturasNegocio = new FacturasNegocio();
        List<proc_CargarFacturasPFecha_Result> proc_CargarFacturasPFecha_Results;
        List<proc_CargarFacturasCFinalPFecha_Result> proc_CargarFacturasCFinalPFecha_Results;
        List<proc_CargarFacturasCFiscalPFecha_Result> proc_CargarFacturasCFiscalPFecha_Results;
        List<proc_CargarFacturasCGubernamentalPFecha_Result> proc_CargarFacturasCGubernamentalPFecha_Results;
        ReportParameter[] parameters = new ReportParameter[6];
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        public ReporteTodasFacturas()
        {
            InitializeComponent();
            CargarParametros();            
        }

        private void CargarParametros()
        {
            parameters[0] = new ReportParameter("RazonSocial", Properties.Settings.Default.RazonSocial);
            parameters[1] = new ReportParameter("CedulaORNC", Properties.Settings.Default.CedulaORnc);
            parameters[2] = new ReportParameter("DireccionEmpresa", Properties.Settings.Default.Direccion);
            parameters[3] = new ReportParameter("TelefonoEmpresa", Properties.Settings.Default.Telefono);
            parameters[4] = new ReportParameter("EmailEmpresa", Properties.Settings.Default.Email);
            parameters[5] = new ReportParameter("Logo", Properties.Settings.Default.Logo);
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

        public void CargarTodasFacturas(DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                proc_CargarFacturasPFecha_Results = facturasNegocio.CargarFacturasPFecha(fechaInicial, fechaFinal).ToList();
                var dataSource = new ReportDataSource("DataSetTodasFacturas", proc_CargarFacturasPFecha_Results);
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                reportViewer1.LocalReport.ReportPath = @"C:/SFacturacion/reporteTodasFacturas.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(dataSource);
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
        public void CargarTodasFacturasCFinal(DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                proc_CargarFacturasCFinalPFecha_Results = facturasNegocio.CargarFacturasCFinalPFecha(fechaInicial, fechaFinal).ToList();
                var dataSource = new ReportDataSource("DataSetTodasFacturas", proc_CargarFacturasCFinalPFecha_Results);
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                reportViewer1.LocalReport.ReportPath = @"C:/SFacturacion/reporteTodasFacturas.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(dataSource);
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
        public void CargarTodasFacturasCFiscal(DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                proc_CargarFacturasCFiscalPFecha_Results = facturasNegocio.CargarFacturasCFiscalPFecha(fechaInicial, fechaFinal).ToList();
                var dataSource = new ReportDataSource("DataSetTodasFacturas", proc_CargarFacturasCFiscalPFecha_Results);
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                reportViewer1.LocalReport.ReportPath = @"C:/SFacturacion/reporteTodasFacturas.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(dataSource);
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
        public void CargarTodasFacturasCGubernamental(DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                proc_CargarFacturasCGubernamentalPFecha_Results = facturasNegocio.CargarFacturasCGubernamentalPFecha(fechaInicial, fechaFinal).ToList();
                var dataSource = new ReportDataSource("DataSetTodasFacturas", proc_CargarFacturasCGubernamentalPFecha_Results);
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                reportViewer1.LocalReport.ReportPath = @"C:/SFacturacion/reporteTodasFacturas.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(dataSource);
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
        private void ReporteTodasFacturas_Load(object sender, EventArgs e)
        {

        }

    }
}
