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
    public partial class ImpresionNotaCredito : Form
    {
        Printer printer = new Printer(Properties.Settings.Default.Impresora);
        NotasDeCreditoNegocio notaDeCreditoNegocio = new NotasDeCreditoNegocio();
        List<proc_ComprobanteNotaDeCredito_Result> proc_ComprobanteNotaDeCredito_Results;
        ReportParameter[] parameters = new ReportParameter[7];
        int cantArticulos;
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        public ImpresionNotaCredito(int notaCreditoID)
        {
            InitializeComponent();
            proc_ComprobanteNotaDeCredito_Results = notaDeCreditoNegocio.CargarComprobanteNotaDeCredito(notaCreditoID).ToList();

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

            parameters[0] = new ReportParameter("ITBISActual", Properties.Settings.Default.ITBIS.ToString());
            parameters[1] = new ReportParameter("RazonSocial", Properties.Settings.Default.RazonSocial);
            parameters[2] = new ReportParameter("CedulaORNC", Properties.Settings.Default.CedulaORnc);
            parameters[3] = new ReportParameter("DireccionEmpresa", Properties.Settings.Default.Direccion);
            parameters[4] = new ReportParameter("TelefonoEmpresa", Properties.Settings.Default.Telefono);
            parameters[5] = new ReportParameter("EmailEmpresa", Properties.Settings.Default.Email);
            parameters[6] = new ReportParameter("Logo", Properties.Settings.Default.Logo);
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
                var dataSource = new ReportDataSource("DataSetComprobanteNotaCredito", proc_ComprobanteNotaDeCredito_Results);
                LocalReport rdlc = new LocalReport();
                rdlc.ReportPath = @"C:/SFacturacion/impresionNotaCredito.rdlc";
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
                var dataSource = new ReportDataSource("DataSetComprobanteNotaCredito", proc_ComprobanteNotaDeCredito_Results);
                LocalReport rdlc = new LocalReport();
                rdlc.ReportPath = @"C:/SFacturacion/impresionTermicaNotaCredito.rdlc";
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
                var dataSource = new ReportDataSource("DataSetComprobanteNotaCredito", proc_ComprobanteNotaDeCredito_Results);
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                reportViewer1.LocalReport.ReportPath = @"C:/SFacturacion/impresionNotaCredito.rdlc";
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

                printer.AlignCenter();
                printer.DoubleWidth2();
                printer.BoldMode(Properties.Settings.Default.NombreEmpresa.ToUpper());
                printer.NormalWidth();
                printer.Append(Properties.Settings.Default.Direccion.ToUpper());
                printer.Append(Properties.Settings.Default.Telefono);
                printer.AlignLeft();
                printer.BoldMode(Properties.Settings.Default.RazonSocial.ToUpper());
                printer.Append(Properties.Settings.Default.CedulaORnc);
                printer.Append("-----------------------------------------");
                printer.AlignCenter();
                printer.Append("NOTA DE CREDITO");
                printer.AlignLeft();
                printer.Append("-----------------------------------------");
                printer.Append(proc_ComprobanteNotaDeCredito_Results.First().Fecha.ToString());
                printer.Append("NCF:" + " " + proc_ComprobanteNotaDeCredito_Results.First().NCF.ToString());
                printer.Append("NCF AFECTADO:" + " " + proc_ComprobanteNotaDeCredito_Results.First().NCFAfectado);
                printer.Append("FECHA VENCIMIENTO: " + proc_ComprobanteNotaDeCredito_Results.First().FechaVencimiento.ToString("dd/MM/yyyy"));
                printer.Append("-----------------------------------------");
                printer.Append("DESCRIPCION/COMENTARIO       |VALOR");
                printer.Append("-----------------------------------------");
                foreach (var fila in proc_ComprobanteNotaDeCredito_Results)
                {
                    AgregaArticuloNC(fila.Descripcion, fila.CantDevuelta, proc_ComprobanteNotaDeCredito_Results.First().ITBIS ? fila.Precio :
                        Convert.ToDecimal(fila.Precio - (fila.Precio * (Properties.Settings.Default.ITBIS / 100))), fila.Comentario);
                    cantArticulos++;

                }
                printer.Append("-----------------------------------------");
                AgregarTotales("                   ITBIS : $ ", proc_ComprobanteNotaDeCredito_Results.First().ITBIS ? Convert.ToDecimal((proc_ComprobanteNotaDeCredito_Results.First().PrecioTotal * (Properties.Settings.Default.ITBIS / 100))) :
                Convert.ToDecimal(0.00));
                AgregarTotales("          VALOR APLICADO : $ ", Convert.ToDecimal(proc_ComprobanteNotaDeCredito_Results.First().PrecioTotal));
                printer.Append("-----------------------------------------");
                printer.Append("CANTIDAD DE ARTICULOS DEVUELTOS:" + " " + cantArticulos);
                printer.Append("-----------------------------------------");
                // controladorImpresoraMatricial.TextoIzquierda("COD.CLIENTE: " + proc_ComprobanteNotaDeCredito_Results.First().ClienteID.ToString());
                printer.Append("CLIENTE: " + proc_ComprobanteNotaDeCredito_Results.First().NombreCliente.ToUpper());
                printer.Append("COD. NOTA DE CREDITO: " + proc_ComprobanteNotaDeCredito_Results.First().NotaDeCreditoID);
                printer.Append("USUARIO: " + proc_ComprobanteNotaDeCredito_Results.First().UserName.ToUpper());
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

        public void AgregaArticuloNC(string articulo, double cant, decimal? precio, string comentario)
        {
            if (cant.ToString().Length < 7 && precio.ToString().Length < 11)
            {
                string elemento, espacios = "";
                int nroEspacios = 0;
                decimal importe = 0;
                //Colocar cant y precio
                elemento = cant.ToString() + " " + "X" + " " + precio.ToString();

                //Colocar el precio total.
                importe = Convert.ToDecimal(cant) * Convert.ToDecimal(precio);
                nroEspacios = (29 - elemento.Length);
                espacios = "";
                for (int i = 0; i < nroEspacios; i++)
                {
                    espacios += " ";
                }
                elemento += espacios + importe.ToString();

                printer.Append(elemento);//Agregamos todo el elemento: nombre del articulo, comentario,cant, precio, importe.
                printer.Append(articulo);
                printer.Append(comentario);


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

                ControladorImpresoraMatricial controladorImpresoraMatricial = new ControladorImpresoraMatricial();

                controladorImpresoraMatricial.TextoCentro(Properties.Settings.Default.NombreEmpresa.ToUpper());
                controladorImpresoraMatricial.TextoCentro(Properties.Settings.Default.Direccion.ToUpper());
                controladorImpresoraMatricial.TextoCentro(Properties.Settings.Default.Telefono);
                controladorImpresoraMatricial.TextoIzquierda(Properties.Settings.Default.RazonSocial.ToUpper());
                controladorImpresoraMatricial.TextoIzquierda(Properties.Settings.Default.CedulaORnc);
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoCentro("NOTA DE CREDITO");
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoIzquierda(proc_ComprobanteNotaDeCredito_Results.First().Fecha.ToString());
                controladorImpresoraMatricial.TextoIzquierda("NCF:" + " " + proc_ComprobanteNotaDeCredito_Results.First().NCF.ToString());
                controladorImpresoraMatricial.TextoIzquierda("NCF AFECTADO:" + " " + proc_ComprobanteNotaDeCredito_Results.First().NCFAfectado);
                controladorImpresoraMatricial.TextoIzquierda("FECHA VENCIMIENTO: " + proc_ComprobanteNotaDeCredito_Results.First().FechaVencimiento.ToString("dd/MM/yyyy"));
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.EncabezadoNC();
                controladorImpresoraMatricial.lineasGuio();
                foreach (var fila in proc_ComprobanteNotaDeCredito_Results)
                {
                    controladorImpresoraMatricial.AgregaArticuloNC(fila.Descripcion, fila.CantDevuelta, proc_ComprobanteNotaDeCredito_Results.First().ITBIS ? fila.Precio :
                        Convert.ToDecimal(fila.Precio - (fila.Precio * (Properties.Settings.Default.ITBIS / 100))), fila.Comentario);
                    cantArticulos++;

                }
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.AgregarTotales("                   ITBIS : $ ", proc_ComprobanteNotaDeCredito_Results.First().ITBIS ? Convert.ToDecimal((proc_ComprobanteNotaDeCredito_Results.First().PrecioTotal * (Properties.Settings.Default.ITBIS / 100))) :
                Convert.ToDecimal(0.00));
                controladorImpresoraMatricial.AgregarTotales("          VALOR APLICADO : $ ", Convert.ToDecimal(proc_ComprobanteNotaDeCredito_Results.First().PrecioTotal));
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoIzquierda("CANTIDAD DE ARTICULOS DEVUELTOS:" + " " + cantArticulos);
                controladorImpresoraMatricial.lineasGuio();
                // controladorImpresoraMatricial.TextoIzquierda("COD.CLIENTE: " + proc_ComprobanteNotaDeCredito_Results.First().ClienteID.ToString());
                controladorImpresoraMatricial.TextoIzquierda("CLIENTE: " + proc_ComprobanteNotaDeCredito_Results.First().NombreCliente.ToUpper());
                controladorImpresoraMatricial.TextoIzquierda("COD. NOTA DE CREDITO: " + proc_ComprobanteNotaDeCredito_Results.First().NotaDeCreditoID);
                controladorImpresoraMatricial.TextoIzquierda("USUARIO: " + proc_ComprobanteNotaDeCredito_Results.First().UserName.ToUpper());
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
