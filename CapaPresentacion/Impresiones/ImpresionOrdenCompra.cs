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
    public partial class ImpresionOrdenCompra : Form
    {
        Printer printer = new Printer(Properties.Settings.Default.Impresora);
        OrdenesCompraNegocio ordenesCompraNegocio = new OrdenesCompraNegocio();
        List<proc_ComprobanteOrdenCompra_Result> proc_ComprobanteOrdenCompra_Results;
        ReportParameter[] parameters = new ReportParameter[6];
        int cantArticulos;
        decimal subtotal;
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        public ImpresionOrdenCompra(int cotizacionID)
        {
            InitializeComponent();
            proc_ComprobanteOrdenCompra_Results = ordenesCompraNegocio.CargarComprobanteOrdenCompra(cotizacionID).ToList();

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

        private void ImpresionOrdenCompra_Load(object sender, EventArgs e)
        {

        }

        private LocalReport CargarImpresionRV()
        {
            try
            {
                var dataSource = new ReportDataSource("DataSetComprobanteOrdenCompra", proc_ComprobanteOrdenCompra_Results);
                LocalReport rdlc = new LocalReport();
                rdlc.ReportPath = @"C:/SFacturacion/impresionOrdenCompra.rdlc";
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
                var dataSource = new ReportDataSource("DataSetComprobanteOrdenCompra", proc_ComprobanteOrdenCompra_Results);
                LocalReport rdlc = new LocalReport();
                rdlc.ReportPath = @"C:/SFacturacion/impresionTermicaOrdenCompra.rdlc";
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
                var dataSource = new ReportDataSource("DataSetComprobanteOrdenCompra", proc_ComprobanteOrdenCompra_Results);
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                reportViewer1.LocalReport.ReportPath = @"C:/SFacturacion/impresionOrdenCompra.rdlc";
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
                printer.AlignCenter();
                printer.DoubleWidth2();
                printer.BoldMode(Properties.Settings.Default.NombreEmpresa.ToUpper());
                printer.NormalWidth();
                printer.Append(Properties.Settings.Default.Direccion);
                printer.Append("TEL: " + Properties.Settings.Default.Telefono);
                printer.AlignLeft();
                printer.BoldMode(Properties.Settings.Default.RazonSocial.ToUpper());
                printer.Append("RNC: " + Properties.Settings.Default.CedulaORnc);
                printer.Append(proc_ComprobanteOrdenCompra_Results.First().FechaPedido.ToString());
                printer.Append("-----------------------------------------");
                printer.AlignCenter();
                printer.Append("ORDEN DE COMPRA");
                printer.AlignLeft();
                printer.Append("-----------------------------------------");
                printer.Append("DESCRIPCION/COMENTARIO       |VALOR");
                printer.Append("-----------------------------------------");
                foreach (var fila in proc_ComprobanteOrdenCompra_Results)
                {
                    AgregaArticuloOrdenCompra(fila.Descripcion, fila.Ordenada, Convert.ToDecimal(fila.PrecioCompra));
                    cantArticulos++;
                    subtotal += (Convert.ToDecimal(fila.Ordenada) * Convert.ToDecimal(fila.PrecioCompra));
                }
                printer.Append("-----------------------------------------");
                AgregarTotales("                   TOTAL : $ ", Convert.ToDecimal(subtotal));
                printer.Append("-----------------------------------------");
                printer.Append("CANTIDAD DE PRODUCTOS:" + " " + cantArticulos);
                printer.Append("-----------------------------------------");
                printer.Append("EL PRECIO DE LOS PRODUCTOS");
                printer.Append("PUEDE VARIAR.");
                printer.Append("-----------------------------------------");
                // controladorImpresoraMatricial.TextoIzquierda("COD. PROVEEDOR: " + proc_ComprobanteOrdenCompra_Results.First().ProveedorID);
                printer.Append("PROVEEDOR: " + proc_ComprobanteOrdenCompra_Results.First().NombreProveedor.ToUpper());
                printer.Append("COD. ORDEN COMPRA: " + proc_ComprobanteOrdenCompra_Results.First().OrdenCompraID.ToString());
                printer.Append("-----------------------------------------");
                printer.AlignCenter();
                printer.Append("SISTEMA REALIZADO POR JONESY LIRIANO");
                printer.Append("TEL/WSS: 809-222-3740");
                printer.Append("****GRACIAS POR SU VISITA****");

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

        public void AgregaArticuloOrdenCompra(string articulo, double cant, decimal precio)
        {
            if (cant.ToString().Length < 7 && precio.ToString().Length < 11)
            {
                string elemento, espacios = "";
                int nroEspacios = 0;
                decimal importe = 0;
                //Colocar cant y precio
                elemento = cant.ToString() + " " + "X" + " " + precio.ToString();



                //Colocar el precio total.
                importe = Convert.ToDecimal(cant) * (precio);
                nroEspacios = (29 - elemento.Length);
                espacios = "";
                for (int i = 0; i < nroEspacios; i++)
                {
                    espacios += " ";
                }
                elemento += espacios + importe.ToString();

                printer.Append(elemento);//Agregamos todo el elemento: nombre del articulo, cant, precio, importe.
                printer.Append(articulo);

            }
            else
            {
                printer.Append("Los valores ingresados para esta fila");
                printer.Append("superan las columnas soportdas por éste.");
                throw new Exception("Los valores ingresados para algunas filas del ticket\nsuperan las columnas soportdas por éste.");
            }
        }

        private void CargarImpresionMatricial()
        {
            try
            {
                cantArticulos = 0;
                subtotal = 0;

                ControladorImpresoraMatricial controladorImpresoraMatricial = new ControladorImpresoraMatricial();

                controladorImpresoraMatricial.TextoCentro(Properties.Settings.Default.NombreEmpresa.ToUpper());
                controladorImpresoraMatricial.TextoCentro(Properties.Settings.Default.Direccion);
                controladorImpresoraMatricial.TextoCentro("TEL: " + Properties.Settings.Default.Telefono);
                controladorImpresoraMatricial.TextoIzquierda(Properties.Settings.Default.RazonSocial.ToUpper());
                controladorImpresoraMatricial.TextoIzquierda("RNC: " + Properties.Settings.Default.CedulaORnc);
                controladorImpresoraMatricial.TextoIzquierda(proc_ComprobanteOrdenCompra_Results.First().FechaPedido.ToString());
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoCentro("ORDEN DE COMPRA");
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.EncabezadoOrdenCompra();
                controladorImpresoraMatricial.lineasGuio();
                foreach (var fila in proc_ComprobanteOrdenCompra_Results)
                {
                    controladorImpresoraMatricial.AgregaArticuloOrdenCompra(fila.Descripcion, fila.Ordenada,Convert.ToDecimal(fila.PrecioCompra));
                    cantArticulos++;
                    subtotal += (Convert.ToDecimal(fila.Ordenada) * Convert.ToDecimal(fila.PrecioCompra));
                }
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.AgregarTotales("                   TOTAL : $ ", Convert.ToDecimal(subtotal));
                controladorImpresoraMatricial.lineasGuio();                
                controladorImpresoraMatricial.TextoIzquierda("CANTIDAD DE PRODUCTOS:" + " " + cantArticulos);
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoIzquierda("EL PRECIO DE LOS PRODUCTOS");
                controladorImpresoraMatricial.TextoIzquierda("PUEDE VARIAR.");
                controladorImpresoraMatricial.lineasGuio();
                // controladorImpresoraMatricial.TextoIzquierda("COD. PROVEEDOR: " + proc_ComprobanteOrdenCompra_Results.First().ProveedorID);
                controladorImpresoraMatricial.TextoIzquierda("PROVEEDOR: " + proc_ComprobanteOrdenCompra_Results.First().NombreProveedor.ToUpper());
                controladorImpresoraMatricial.TextoIzquierda("COD. ORDEN COMPRA: " + proc_ComprobanteOrdenCompra_Results.First().OrdenCompraID.ToString());
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
