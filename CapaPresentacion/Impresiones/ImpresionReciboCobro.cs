using CapaDatos;
using CapaNegocios;
using CapaPresentacion.Clases;
using ESC_POS_USB_NET.Printer;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Impresiones
{
    public partial class ImpresionReciboCobro : Form
    {
        Printer printer = new Printer(Properties.Settings.Default.Impresora);
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
                ControladorImpresoraGeneral.PrintToPrinter(CargarImpresionRV());
            }
            else if (Properties.Settings.Default.TipoImpresora == "Termica")
            {
                CargarImpresionTermica();
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

        private LocalReport CargarImpresionTermicaRV()
        {
            try
            {
                var dataSource = new ReportDataSource("DataSetReciboCobro", proc_ComprobantePagoLineaCreditoVenta_Results);
                LocalReport rdlc = new LocalReport();
                rdlc.ReportPath = @"C:/SFacturacion/impresionTermicaReciboCobro.rdlc";
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

        private void CargarImpresionTermica()
        {
            try
            {

                printer.AlignCenter();
                printer.DoubleWidth2();
                printer.BoldMode(Properties.Settings.Default.NombreEmpresa.ToUpper());
                printer.NormalWidth();
                printer.Append(Properties.Settings.Default.Direccion);
                printer.Append(Properties.Settings.Default.Telefono);
                printer.AlignLeft();
                printer.Append(Properties.Settings.Default.RazonSocial.ToUpper());
                printer.Append(Properties.Settings.Default.CedulaORnc);
                printer.Append("-----------------------------------------");
                printer.AlignCenter();
                printer.Append("COMPROBANTE DE PAGO");
                printer.AlignLeft();
                printer.Append("-----------------------------------------");
                printer.Append(proc_ComprobantePagoLineaCreditoVenta_Results.First().FechaCobro.ToString());
                printer.Append("COMPROBANTE: " + proc_ComprobantePagoLineaCreditoVenta_Results.First().CobroVentaCreditoID.ToString());
                printer.Append("FECHA: " + proc_ComprobantePagoLineaCreditoVenta_Results.First().FechaCobro.ToString());
                printer.Append("LINEA DE CREDITO: " + proc_ComprobantePagoLineaCreditoVenta_Results.First().LineaCreditoVentaID.ToString());
                printer.Append("FACTURA: " + proc_ComprobantePagoLineaCreditoVenta_Results.First().FacturaID.ToString());
                printer.Append("VALOR FACTURA: $ " + Convert.ToDecimal(proc_ComprobantePagoLineaCreditoVenta_Results.First().ValorFactura).ToString("0.00"));
                printer.Append("CONCEPTO: " + proc_ComprobantePagoLineaCreditoVenta_Results.First().Concepto);
                printer.Append("-----------------------------------------");
                AgregarTotales("    VALOR PENDIENTE : $ ", Convert.ToDecimal(proc_ComprobantePagoLineaCreditoVenta_Results.First().ValorPendiente));
                AgregarTotales("     PAGO REALIZADO : $ ", Convert.ToDecimal(proc_ComprobantePagoLineaCreditoVenta_Results.First().PagoRealizado));
                AgregarTotales("     MONTO RESTANTE : $ ", Convert.ToDecimal(proc_ComprobantePagoLineaCreditoVenta_Results.First().MontoRestante));
                printer.Append("-----------------------------------------");
                printer.AlignCenter();
                printer.Append(" ");
                printer.Append(" ");
                printer.Append("___________________________");
                printer.Append("FIRMA/CEDULA");
                printer.AlignLeft();
                printer.Append("-----------------------------------------");
                //  controladorImpresoraMatricial.TextoIzquierda("COD. CLIENTE: " + proc_ComprobantePagoLineaCreditoVenta_Results.First().ClienteID);
                printer.Append("CLIENTE: " + proc_ComprobantePagoLineaCreditoVenta_Results.First().Cliente.ToUpper());
                printer.Append("USUARIO: " + proc_ComprobantePagoLineaCreditoVenta_Results.First().Usuario.ToUpper());
                printer.Append("-----------------------------------------");
                printer.AlignCenter();
                printer.Append("SISTEMA REALIZADO POR JONESY LIRIANO");
                printer.Append("TEL/WSS: 809-222-3740");
                printer.Append("****GRACIAS POR SU PAGO***");

                printer.FullPaperCut();
                printer.PrintDocument();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido imprimir, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        public void AgregarTotales(string texto, decimal total)
        {
            int maxCar = 40, cortar;
            //Variables que usaremos
            string resumen, valor, textoCompleto, espacios = "";

            if (texto.Length > 29)//Si es mayor a 25 lo cortamos
            {
                cortar = texto.Length - 29;
                resumen = texto.Remove(29, cortar);
            }
            else
            { resumen = texto; }

            textoCompleto = resumen;
            valor = total.ToString("#,#.00");//Agregamos el total previo formateo.

            //Obtenemos el numero de espacios restantes para alinearlos a la derecha
            int nroEspacios = maxCar - (resumen.Length + valor.Length);
            //agregamos los espacios
            for (int i = 0; i < nroEspacios; i++)
            {
                espacios += " ";
            }
            textoCompleto += espacios + valor;
            printer.Append(textoCompleto);
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
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoCentro("COMPROBANTE DE PAGO");
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoIzquierda(proc_ComprobantePagoLineaCreditoVenta_Results.First().FechaCobro.ToString());
                controladorImpresoraMatricial.TextoIzquierda("COMPROBANTE: " + proc_ComprobantePagoLineaCreditoVenta_Results.First().CobroVentaCreditoID.ToString());
                controladorImpresoraMatricial.TextoIzquierda("FECHA: " + proc_ComprobantePagoLineaCreditoVenta_Results.First().FechaCobro.ToString());
                controladorImpresoraMatricial.TextoIzquierda("LINEA DE CREDITO: " + proc_ComprobantePagoLineaCreditoVenta_Results.First().LineaCreditoVentaID.ToString());
                controladorImpresoraMatricial.TextoIzquierda("FACTURA: " + proc_ComprobantePagoLineaCreditoVenta_Results.First().FacturaID.ToString());
                controladorImpresoraMatricial.TextoIzquierda("VALOR FACTURA: $ " + Convert.ToDecimal(proc_ComprobantePagoLineaCreditoVenta_Results.First().ValorFactura).ToString("0.00"));
                controladorImpresoraMatricial.TextoIzquierda("CONCEPTO: " + proc_ComprobantePagoLineaCreditoVenta_Results.First().Concepto);
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.AgregarTotales("    VALOR PENDIENTE : $ ", Convert.ToDecimal(proc_ComprobantePagoLineaCreditoVenta_Results.First().ValorPendiente));
                controladorImpresoraMatricial.AgregarTotales("     PAGO REALIZADO : $ ", Convert.ToDecimal(proc_ComprobantePagoLineaCreditoVenta_Results.First().PagoRealizado));
                controladorImpresoraMatricial.AgregarTotales("     MONTO RESTANTE : $ ", Convert.ToDecimal(proc_ComprobantePagoLineaCreditoVenta_Results.First().MontoRestante));
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoIzquierda(" ");
                controladorImpresoraMatricial.TextoIzquierda(" ");
                controladorImpresoraMatricial.TextoCentro("___________________________");
                controladorImpresoraMatricial.TextoCentro("FIRMA/CEDULA");
                controladorImpresoraMatricial.lineasGuio();
               //  controladorImpresoraMatricial.TextoIzquierda("COD. CLIENTE: " + proc_ComprobantePagoLineaCreditoVenta_Results.First().ClienteID);
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
