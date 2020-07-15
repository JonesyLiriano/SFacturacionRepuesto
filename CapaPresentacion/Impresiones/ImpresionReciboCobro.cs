using CapaDatos;
using CapaNegocios;
using CapaPresentacion.Clases;
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

namespace CapaPresentacion.Impresiones
{
    public partial class ImpresionReciboCobro : Form
    {
        CobrosVentaCreditoNegocio cobrosVentaCreditoNegocio = new CobrosVentaCreditoNegocio();
        List<proc_ComprobantePagoLineaCreditoVenta_Result> proc_ComprobantePagoLineaCreditoVenta_Results;
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

        public ImpresionReciboCobro(int cobroVentaCreditoID)
        {
            InitializeComponent();
            proc_ComprobantePagoLineaCreditoVenta_Results = cobrosVentaCreditoNegocio.ComprobantePagoLineaCreditoVenta(cobroVentaCreditoID).ToList();

        }

        public void ImprimirDirecto()
        {
            try
            {
                ConfirmarTipoImpresora();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido imprimir, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor.",
                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        public void ImprimirConVistaPrevia()
        {
            try
            {
                CargarParametros();
                CargarVistaPreviaRV();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido imprimir, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor.",
                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void ConfirmarTipoImpresora()
        {
            if (Properties.Settings.Default.TipoImpresora == "Matricial")
            {
                CargarImpresionMatricial();
            }
            else if (Properties.Settings.Default.TipoImpresora == "Papel A4")
            {
                CargarParametros();
                ControladorImpresoraPapelA4 controladorImpresoraPapelA4 = new ControladorImpresoraPapelA4();
                controladorImpresoraPapelA4.Imprime(CargarImpresionRV());
            }
            this.Close();
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

        private LocalReport CargarImpresionRV()
        {
            try
            {
                var dataSource = new ReportDataSource("DataSetReciboCobro", proc_ComprobantePagoLineaCreditoVenta_Results);
                LocalReport rdlc = new LocalReport();
                rdlc.ReportPath = @"C:/SFacturacion/impresionReciboCobro.rdlc";
                rdlc.DataSources.Clear();
                rdlc.DataSources.Add(dataSource);
                rdlc.EnableExternalImages = true;
                rdlc.SetParameters(parameters);
                return rdlc;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido imprimir, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor.",
                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
                return null;
            }
        }
        private void CargarVistaPreviaRV()
        {
            try
            {
                var dataSource = new ReportDataSource("DataSetReciboCobro", proc_ComprobantePagoLineaCreditoVenta_Results);
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                reportViewer1.LocalReport.ReportPath = @"C:/SFacturacion/impresionReciboCobro.rdlc";
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
        private void CargarImpresionMatricial()
        {
            try
            {               
                ControladorImpresoraMatricial controladorImpresoraMatricial = new ControladorImpresoraMatricial();
        
                controladorImpresoraMatricial.TextoCentro(Properties.Settings.Default.NombreEmpresa.ToUpper());
                controladorImpresoraMatricial.TextoCentro(Properties.Settings.Default.Direccion);
                controladorImpresoraMatricial.TextoCentro(Properties.Settings.Default.Telefono);
                controladorImpresoraMatricial.TextoIzquierda(Properties.Settings.Default.RazonSocial.ToUpper());
                controladorImpresoraMatricial.TextoIzquierda(Properties.Settings.Default.CedulaORnc);
                controladorImpresoraMatricial.TextoCentro("COMPROBANTE DE PAGO");
                controladorImpresoraMatricial.TextoIzquierda(proc_ComprobantePagoLineaCreditoVenta_Results.First().FechaCobro.ToString());
                controladorImpresoraMatricial.TextoIzquierda("COMPROBANTE: " + proc_ComprobantePagoLineaCreditoVenta_Results.First().CobroVentaCreditoID.ToString());
                controladorImpresoraMatricial.TextoIzquierda("FECHA: " + proc_ComprobantePagoLineaCreditoVenta_Results.First().FechaCobro.ToString());
                controladorImpresoraMatricial.TextoIzquierda("LINEA DE CREDITO: " + proc_ComprobantePagoLineaCreditoVenta_Results.First().LineaCreditoVentaID.ToString());
                controladorImpresoraMatricial.TextoIzquierda("FACTURA: " + proc_ComprobantePagoLineaCreditoVenta_Results.First().FacturaID.ToString());
                controladorImpresoraMatricial.AgregarTotales("VALOR FACTURA: $ ", Convert.ToDecimal(proc_ComprobantePagoLineaCreditoVenta_Results.First().ValorFactura));
                controladorImpresoraMatricial.TextoIzquierda("CONCEPTO: " + proc_ComprobantePagoLineaCreditoVenta_Results.First().Concepto);
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.AgregarTotales("     VALOR PENDIENTE : $ ", Convert.ToDecimal(proc_ComprobantePagoLineaCreditoVenta_Results.First().ValorPendiente));
                controladorImpresoraMatricial.AgregarTotales("     PAGO REALIZADO : $ ", Convert.ToDecimal(proc_ComprobantePagoLineaCreditoVenta_Results.First().PagoRealizado));
                controladorImpresoraMatricial.AgregarTotales("     MONTO RESTANTE : $ ", Convert.ToDecimal(proc_ComprobantePagoLineaCreditoVenta_Results.First().MontoRestante));
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoIzquierda(" ");
                controladorImpresoraMatricial.TextoIzquierda(" ");
                controladorImpresoraMatricial.TextoCentro("___________________________");
                controladorImpresoraMatricial.TextoCentro("FIRMA/CEDULA");
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoIzquierda("COD. CLIENTE: " + proc_ComprobantePagoLineaCreditoVenta_Results.First().ClienteID);
                controladorImpresoraMatricial.TextoIzquierda("CLIENTE: " + proc_ComprobantePagoLineaCreditoVenta_Results.First().Cliente.ToUpper());
                controladorImpresoraMatricial.TextoDerecha("USUARIO: " + proc_ComprobantePagoLineaCreditoVenta_Results.First().Usuario.ToUpper());
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoCentro("SISTEMA REALIZADO POR JONESY LIRIANO");
                controladorImpresoraMatricial.TextoCentro("TEL/WSS: 809-222-3740");
                controladorImpresoraMatricial.TextoCentro("****GRACIAS POR SU PAGO***");
                controladorImpresoraMatricial.TextoIzquierda(" ");
                controladorImpresoraMatricial.TextoIzquierda(" ");
                controladorImpresoraMatricial.TextoIzquierda(" ");
                controladorImpresoraMatricial.TextoIzquierda(" ");
                controladorImpresoraMatricial.TextoIzquierda(" ");
                controladorImpresoraMatricial.TextoIzquierda(" ");
                controladorImpresoraMatricial.TextoIzquierda(" ");

                controladorImpresoraMatricial.ImprimirTicket();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido imprimir, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }
    }
}
