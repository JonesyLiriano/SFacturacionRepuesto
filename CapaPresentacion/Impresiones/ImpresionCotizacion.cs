
using CapaDatos;
using CapaNegocios;
using CapaPresentacion.Clases;
using ESC_POS_USB_NET.Printer;
using Microsoft.Reporting.WinForms;
using Microsoft.ReportingServices.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Impresiones
{
    public partial class ImpresionCotizacion : Form
    {
        Printer printer = new Printer(Properties.Settings.Default.Impresora);
        CotizacionesNegocio cotizacionesNegocio = new CotizacionesNegocio();
        List<proc_ComprobanteCotizacion_Result> proc_ComprobanteCotizacion_Results;
        ReportParameter[] parameters = new ReportParameter[6];
        int cantArticulos;
        decimal subtotal, itbis, desc, descTotal;
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        public ImpresionCotizacion(int cotizacionID)
        {
            InitializeComponent();
            proc_ComprobanteCotizacion_Results = cotizacionesNegocio.CargarComprobanteCotizacion(cotizacionID).ToList();

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
                CargarParametros();
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

        private void ImpresionCotizacion_Load(object sender, EventArgs e)
        {

        }
        private LocalReport CargarImpresionTermicaRV()
        {
            try
            {
                var dataSource = new ReportDataSource("DataSetComprobanteCotizacion", proc_ComprobanteCotizacion_Results);
                LocalReport rdlc = new LocalReport();
                rdlc.ReportPath = @"C:/SFacturacion/impresionTermicaCotizacion.rdlc";
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

        private LocalReport CargarImpresionRV()
        {
            try
            {
                var dataSource = new ReportDataSource("DataSetComprobanteCotizacion", proc_ComprobanteCotizacion_Results);
                LocalReport rdlc = new LocalReport();
                rdlc.ReportPath = @"C:/SFacturacion/impresionCotizacion.rdlc";
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
                var dataSource = new ReportDataSource("DataSetComprobanteCotizacion", proc_ComprobanteCotizacion_Results);
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                reportViewer1.LocalReport.ReportPath = @"C:/SFacturacion/impresionCotizacion.rdlc";
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
                cantArticulos = 0;
                subtotal = 0;
                itbis = 0;
                desc = 0;
                descTotal = 0;
                
                printer.AlignCenter();
                printer.DoubleWidth2();
                printer.BoldMode(Properties.Settings.Default.NombreEmpresa.ToUpper());
                printer.NormalWidth();
                printer.Append(Properties.Settings.Default.Direccion);
                printer.Append("TEL: " + Properties.Settings.Default.Telefono);
                printer.AlignLeft();
                printer.BoldMode(Properties.Settings.Default.RazonSocial.ToUpper());
                printer.Append("RNC: " + Properties.Settings.Default.CedulaORnc);
                printer.Append(proc_ComprobanteCotizacion_Results.First().Fecha.ToString());
                printer.Append("-----------------------------------------");
                printer.AlignCenter();
                printer.Append("COTIZACION");
                printer.AlignLeft();
                printer.Append("-----------------------------------------");
                printer.Append("DESCRIPCION         |ITBIS     |VALOR");
                printer.Append("-----------------------------------------");
                foreach (var fila in proc_ComprobanteCotizacion_Results)
                {
                    AgregaArticulo(fila.Descripcion, fila.CantVen, fila.ITBIS, fila.Precio, fila.Descuento);
                    cantArticulos++;
                    subtotal += (Convert.ToDecimal(fila.CantVen) * (fila.Precio - fila.Descuento));
                    itbis += (Convert.ToDecimal(fila.CantVen) * Convert.ToDecimal(fila.ITBIS));
                    desc += (Convert.ToDecimal(fila.CantVen) * Convert.ToDecimal(fila.Descuento));
                }
                descTotal = Convert.ToDecimal((desc + ((proc_ComprobanteCotizacion_Results.First().DescuentoCliente / 100) * subtotal)));
                printer.Append("-----------------------------------------");
                AgregarTotales("                SUBTOTAL : $ ", subtotal);
                AgregarTotales("                   ITBIS : $ ", itbis);
                AgregarTotales("                   DESC. : $ ", descTotal);
                AgregarTotales("                   TOTAL : $ ", Convert.ToDecimal(subtotal + itbis - descTotal));
                printer.Append("-----------------------------------------");
                printer.Append("CANTIDAD DE PRODUCTOS/SERVICIOS:" + " " + cantArticulos);
                printer.Append("-----------------------------------------");
                printer.Append("LAS COTIZACIONES SOLAMENTE");
                printer.Append("SON VALIDAS POR 30 DIAS");
                printer.Append("-----------------------------------------");
                // controladorImpresoraMatricial.TextoIzquierda("COD. CLIENTE: " + proc_ComprobanteCotizacion_Results.First().ClienteID);
                printer.Append("CLIENTE: " + proc_ComprobanteCotizacion_Results.First().NombreCliente.ToUpper());
                printer.Append("COD. COTIZACION: " + proc_ComprobanteCotizacion_Results.First().CotizacionID.ToString());
                printer.Append("USUARIO: " + proc_ComprobanteCotizacion_Results.First().UserName.ToUpper());
                printer.Append("-----------------------------------------");
                printer.AlignCenter();
                printer.Append("SISTEMA REALIZADO POR JONESY LIRIANO");
                printer.Append("TEL/WSS: 809-222-3740");
                printer.Append("****GRACIAS POR SU VISITA****");

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

        public void AgregaArticulo(string articulo, double cant, decimal itbis, decimal precio, decimal descuento)
        {
            if (cant.ToString().Length < 7 && precio.ToString().Length < 11)
            {
                string elemento, espacios = "";
                int nroEspacios = 0;
                decimal importe = 0;
                //Colocar cant y precio
                elemento = cant.ToString() + " " + "X" + " " + precio.ToString();


                //Colocar el ITBIS a la derecha.
                nroEspacios = (20 - elemento.Length);
                espacios = "";
                for (int i = 0; i < nroEspacios; i++)
                {
                    espacios += " ";
                }
                elemento += espacios + itbis.ToString();
                //Colocar el precio total.
                importe = Convert.ToDecimal(cant) * (precio + itbis - descuento);
                nroEspacios = (31 - elemento.Length);
                espacios = "";
                for (int i = 0; i < nroEspacios; i++)
                {
                    espacios += " ";
                }
                elemento += espacios + importe.ToString();

                printer.Append(elemento);//Agregamos todo el elemento: nombre del articulo, cant, precio, importe.
                printer.Append(articulo);

            }
        }
            private void CargarImpresionMatricial()
        {
            try
            {
                cantArticulos = 0;
                subtotal = 0;
                itbis = 0;
                desc = 0;
                descTotal = 0;

                ControladorImpresoraMatricial controladorImpresoraMatricial = new ControladorImpresoraMatricial();
                
                controladorImpresoraMatricial.TextoCentro(Properties.Settings.Default.NombreEmpresa.ToUpper());
                controladorImpresoraMatricial.TextoCentro(Properties.Settings.Default.Direccion);
                controladorImpresoraMatricial.TextoCentro("TEL: " + Properties.Settings.Default.Telefono);
                controladorImpresoraMatricial.TextoIzquierda(Properties.Settings.Default.RazonSocial.ToUpper());
                controladorImpresoraMatricial.TextoIzquierda("RNC: " + Properties.Settings.Default.CedulaORnc);
                controladorImpresoraMatricial.TextoIzquierda(proc_ComprobanteCotizacion_Results.First().Fecha.ToString());
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoCentro("COTIZACION");
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.EncabezadoVenta();
                controladorImpresoraMatricial.lineasGuio();
                foreach (var fila in proc_ComprobanteCotizacion_Results)
                {
                    controladorImpresoraMatricial.AgregaArticulo(fila.Descripcion, fila.CantVen, fila.ITBIS, fila.Precio, fila.Descuento);
                    cantArticulos++;
                    subtotal += (Convert.ToDecimal(fila.CantVen) * (fila.Precio - fila.Descuento));
                    itbis += (Convert.ToDecimal(fila.CantVen) * Convert.ToDecimal(fila.ITBIS));
                    desc += (Convert.ToDecimal(fila.CantVen) * Convert.ToDecimal(fila.Descuento));
                }
                descTotal = Convert.ToDecimal((desc + ((proc_ComprobanteCotizacion_Results.First().DescuentoCliente / 100) * subtotal)));
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.AgregarTotales("                SUBTOTAL : $ ", subtotal);
                controladorImpresoraMatricial.AgregarTotales("                   ITBIS : $ ", itbis);
                controladorImpresoraMatricial.AgregarTotales("                   DESC. : $ ", descTotal);
                controladorImpresoraMatricial.AgregarTotales("                   TOTAL : $ ", Convert.ToDecimal(subtotal + itbis - descTotal));
                controladorImpresoraMatricial.lineasGuio();                
                controladorImpresoraMatricial.TextoIzquierda("CANTIDAD DE PRODUCTOS/SERVICIOS:" + " " + cantArticulos);
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoIzquierda("LAS COTIZACIONES SOLAMENTE");
                controladorImpresoraMatricial.TextoIzquierda("SON VALIDAS POR 30 DIAS");
                controladorImpresoraMatricial.lineasGuio();
               // controladorImpresoraMatricial.TextoIzquierda("COD. CLIENTE: " + proc_ComprobanteCotizacion_Results.First().ClienteID);
                controladorImpresoraMatricial.TextoIzquierda("CLIENTE: " + proc_ComprobanteCotizacion_Results.First().NombreCliente.ToUpper());
                controladorImpresoraMatricial.TextoIzquierda("COD. COTIZACION: " + proc_ComprobanteCotizacion_Results.First().CotizacionID.ToString());
                controladorImpresoraMatricial.TextoIzquierda("USUARIO: " + proc_ComprobanteCotizacion_Results.First().UserName.ToUpper());
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoCentro("SISTEMA REALIZADO POR JONESY LIRIANO");
                controladorImpresoraMatricial.TextoCentro("TEL/WSS: 809-222-3740");
                controladorImpresoraMatricial.TextoCentro("****GRACIAS POR SU VISITA****");
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
