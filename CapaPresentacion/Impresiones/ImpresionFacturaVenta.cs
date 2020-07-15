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
    public partial class ImpresionFacturaVenta : Form
    {
        FacturasNegocio facturasNegocio = new FacturasNegocio();
        List<proc_ComprobanteFacturaVenta_Result> proc_ComprobanteFacturaVenta_Results;
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
        public ImpresionFacturaVenta(int facturaID)
        {
            InitializeComponent();
            proc_ComprobanteFacturaVenta_Results = facturasNegocio.CargarComprobanteFacturaVenta(facturaID).ToList();
        }

        private void ImpresionFacturaVenta_Load(object sender, EventArgs e)
        {

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
                switch (proc_ComprobanteFacturaVenta_Results.First().TipoFactura)
                {
                    case "Consumidor Final":
                        CargarImpresionMatricialCFinal();
                        break;
                    case "Comprobante Fiscal":
                        CargarImpresionMatricialCFiscal();
                        break;
                    case "Comprobante Gubernamental":
                        CargarImpresionMatricialCGubernamental();
                        break;
                    default:
                        break;
                }
                
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

        private LocalReport CargarImpresionRV()
        {
            try
            {
                var dataSource = new ReportDataSource("DataSetComprobanteFacturaVenta", proc_ComprobanteFacturaVenta_Results);
                LocalReport rdlc = new LocalReport();
                rdlc.ReportPath = @"C:/SFacturacion/impresionFacturaVenta.rdlc";
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
                var dataSource = new ReportDataSource("DataSetComprobanteFacturaVenta", proc_ComprobanteFacturaVenta_Results);
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                reportViewer1.LocalReport.ReportPath = @"C:/SFacturacion/impresionFacturaVenta.rdlc";
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

  
        private void iconcerrar_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void CargarImpresionMatricialCFiscal()
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
                controladorImpresoraMatricial.TextoCentro("COMPROBANTE AUTORIZADO POR DGII");
                controladorImpresoraMatricial.TextoIzquierda(proc_ComprobanteFacturaVenta_Results.First().Fecha.ToString());
                controladorImpresoraMatricial.TextoIzquierda("NCF:" + " " + proc_ComprobanteFacturaVenta_Results.First().NCF.ToString());
                controladorImpresoraMatricial.TextoIzquierda("FECHA VENCIMIENTO: " + proc_ComprobanteFacturaVenta_Results.First().FechaVencimiento?.ToString("dd/MM/yyyy"));
                controladorImpresoraMatricial.TextoIzquierda("RNC: " + proc_ComprobanteFacturaVenta_Results.First().RNC.ToString());
                controladorImpresoraMatricial.TextoIzquierda(proc_ComprobanteFacturaVenta_Results.First().Entidad);
                controladorImpresoraMatricial.TextoIzquierda("TIPO DE PAGO:" + " " + proc_ComprobanteFacturaVenta_Results.First().TipoDePago);
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoCentro("FACTURA PARA CREDITO FISCAL");
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.EncabezadoVenta();
                controladorImpresoraMatricial.lineasGuio();
                foreach (var fila in proc_ComprobanteFacturaVenta_Results)
                {
                    controladorImpresoraMatricial.AgregaArticulo(fila.Descripcion, fila.CantVen, fila.ITBIS, fila.Precio, fila.Descuento);
                    cantArticulos++;
                    subtotal += (Convert.ToDecimal(fila.CantVen) * (fila.Precio - fila.Descuento));
                    itbis += (Convert.ToDecimal(fila.CantVen) * Convert.ToDecimal(fila.ITBIS));
                    desc += (Convert.ToDecimal(fila.CantVen) * Convert.ToDecimal(fila.Descuento));
                }
                descTotal = Convert.ToDecimal((desc + ((proc_ComprobanteFacturaVenta_Results.First().DescuentoCliente / 100) * subtotal)));
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoIzquierda("CANTIDAD DE PRODUCTOS/SERVICIOS:" + " " + cantArticulos);
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoIzquierda("LAS DEVOLUCIONES SE HACEN");
                controladorImpresoraMatricial.TextoIzquierda("CON CREDITO, NO SE DEVUELVE DINERO");
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.AgregarTotales("                SUBTOTAL : $ ", subtotal);
                controladorImpresoraMatricial.AgregarTotales("                   ITBIS : $ ", itbis);
                controladorImpresoraMatricial.AgregarTotales("                   DESC. : $ ", descTotal);
                controladorImpresoraMatricial.AgregarTotales("                   TOTAL : $ ", Convert.ToDecimal(subtotal + itbis - descTotal));
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoIzquierda(" ");
                controladorImpresoraMatricial.TextoIzquierda(" ");
                controladorImpresoraMatricial.TextoCentro("___________________________");
                controladorImpresoraMatricial.TextoCentro("FIRMA/CEDULA");
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoIzquierda("COD. CLIENTE: " + proc_ComprobanteFacturaVenta_Results.First().ClienteID);
                controladorImpresoraMatricial.TextoIzquierda("CLIENTE: " + proc_ComprobanteFacturaVenta_Results.First().NombreCliente.ToUpper());
                controladorImpresoraMatricial.TextoIzquierda("COD. FACTURA: " + proc_ComprobanteFacturaVenta_Results.First().FacturaID.ToString());
                controladorImpresoraMatricial.TextoIzquierda("USUARIO: " + proc_ComprobanteFacturaVenta_Results.First().UserName.ToString().ToUpper());
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
        private void CargarImpresionMatricialCFinal()
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
                controladorImpresoraMatricial.TextoCentro("COMPROBANTE AUTORIZADO POR DGII");
                controladorImpresoraMatricial.TextoIzquierda(proc_ComprobanteFacturaVenta_Results.First().Fecha.ToString());
                controladorImpresoraMatricial.TextoIzquierda("FECHA VENCIMIENTO: " + proc_ComprobanteFacturaVenta_Results.First().FechaVencimiento?.ToString("dd/MM/yyyy"));
                controladorImpresoraMatricial.TextoIzquierda("NCF:" + " " + proc_ComprobanteFacturaVenta_Results.First().NCF.ToString());
                controladorImpresoraMatricial.TextoIzquierda("TIPO DE PAGO:" + " " + proc_ComprobanteFacturaVenta_Results.First().TipoDePago);
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoCentro("FACTURA PARA CONSUMIDOR FINAL");
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.EncabezadoVenta();
                controladorImpresoraMatricial.lineasGuio();
                foreach (var fila in proc_ComprobanteFacturaVenta_Results)
                {
                    controladorImpresoraMatricial.AgregaArticulo(fila.Descripcion, fila.CantVen, fila.ITBIS, fila.Precio, fila.Descuento);
                    cantArticulos++;
                    subtotal += (Convert.ToDecimal(fila.CantVen) * (fila.Precio - fila.Descuento));
                    itbis += (Convert.ToDecimal(fila.CantVen) * Convert.ToDecimal(fila.ITBIS));
                    desc += (Convert.ToDecimal(fila.CantVen) * Convert.ToDecimal(fila.Descuento));
                }
                descTotal = Convert.ToDecimal((desc + ((proc_ComprobanteFacturaVenta_Results.First().DescuentoCliente / 100) * subtotal)));
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoIzquierda("CANTIDAD DE PRODUCTOS/SERVICIOS:" + " " + cantArticulos);
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoIzquierda("LAS DEVOLUCIONES SE HACEN");
                controladorImpresoraMatricial.TextoIzquierda("CON CREDITO, NO SE DEVUELVE DINERO");
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.AgregarTotales("                SUBTOTAL : $ ", subtotal);
                controladorImpresoraMatricial.AgregarTotales("                   ITBIS : $ ", itbis);
                controladorImpresoraMatricial.AgregarTotales("                   DESC. : $ ", descTotal);
                controladorImpresoraMatricial.AgregarTotales("                   TOTAL : $ ", Convert.ToDecimal(subtotal + itbis - descTotal));
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoIzquierda(" ");
                controladorImpresoraMatricial.TextoIzquierda(" ");
                controladorImpresoraMatricial.TextoCentro("___________________________");
                controladorImpresoraMatricial.TextoCentro("FIRMA/CEDULA");
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoIzquierda("COD. CLIENTE: " + proc_ComprobanteFacturaVenta_Results.First().ClienteID);
                controladorImpresoraMatricial.TextoIzquierda("CLIENTE: " + proc_ComprobanteFacturaVenta_Results.First().NombreCliente.ToUpper());
                controladorImpresoraMatricial.TextoIzquierda("COD. FACTURA: " + proc_ComprobanteFacturaVenta_Results.First().FacturaID.ToString());
                controladorImpresoraMatricial.TextoIzquierda("USUARIO: " + proc_ComprobanteFacturaVenta_Results.First().UserName.ToString().ToUpper());
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoCentro("SISTEMA REALIZADO POR LIRIANO");
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
        private void CargarImpresionMatricialCGubernamental()
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
                controladorImpresoraMatricial.TextoCentro("COMPROBANTE AUTORIZADO POR DGII");
                controladorImpresoraMatricial.TextoIzquierda(proc_ComprobanteFacturaVenta_Results.First().Fecha.ToString());
                controladorImpresoraMatricial.TextoIzquierda("NCF:" + " " + proc_ComprobanteFacturaVenta_Results.First().NCF.ToString());
                controladorImpresoraMatricial.TextoIzquierda("FECHA VENCIMIENTO: " + proc_ComprobanteFacturaVenta_Results.First().FechaVencimiento?.ToString("dd/MM/yyyy"));
                controladorImpresoraMatricial.TextoIzquierda("RNC: " + proc_ComprobanteFacturaVenta_Results.First().RNC.ToString());
                controladorImpresoraMatricial.TextoIzquierda(proc_ComprobanteFacturaVenta_Results.First().Entidad);
                controladorImpresoraMatricial.TextoIzquierda("TIPO DE PAGO:" + " " + proc_ComprobanteFacturaVenta_Results.First().TipoDePago);
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoCentro("FACTURA GUBERNAMENTAL");
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.EncabezadoVenta();
                controladorImpresoraMatricial.lineasGuio();
                foreach (var fila in proc_ComprobanteFacturaVenta_Results)
                {
                    controladorImpresoraMatricial.AgregaArticulo(fila.Descripcion, fila.CantVen, fila.ITBIS, fila.Precio, fila.Descuento);
                    cantArticulos++;
                    subtotal += (Convert.ToDecimal(fila.CantVen) * (fila.Precio - fila.Descuento));
                    itbis += (Convert.ToDecimal(fila.CantVen) * Convert.ToDecimal(fila.ITBIS));
                    desc += (Convert.ToDecimal(fila.CantVen) * Convert.ToDecimal(fila.Descuento));
                }
                descTotal = Convert.ToDecimal((desc + ((proc_ComprobanteFacturaVenta_Results.First().DescuentoCliente / 100) * subtotal)));
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoIzquierda("CANTIDAD DE PRODUCTOS/SERVICIOS:" + " " + cantArticulos);
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoIzquierda("LAS DEVOLUCIONES SE HACEN");
                controladorImpresoraMatricial.TextoIzquierda("CON CREDITO, NO SE DEVUELVE DINERO");
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.AgregarTotales("                SUBTOTAL : $ ", subtotal);
                controladorImpresoraMatricial.AgregarTotales("                   ITBIS : $ ", itbis);
                controladorImpresoraMatricial.AgregarTotales("                   DESC. : $ ", descTotal);
                controladorImpresoraMatricial.AgregarTotales("                   TOTAL : $ ", Convert.ToDecimal(subtotal + itbis - descTotal));
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoIzquierda(" ");
                controladorImpresoraMatricial.TextoIzquierda(" ");
                controladorImpresoraMatricial.TextoCentro("___________________________");
                controladorImpresoraMatricial.TextoCentro("FIRMA/CEDULA");
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoIzquierda("COD. CLIENTE: " + proc_ComprobanteFacturaVenta_Results.First().ClienteID);
                controladorImpresoraMatricial.TextoIzquierda("CLIENTE: " + proc_ComprobanteFacturaVenta_Results.First().NombreCliente.ToUpper());
                controladorImpresoraMatricial.TextoIzquierda("COD. FACTURA: " + proc_ComprobanteFacturaVenta_Results.First().FacturaID.ToString());
                controladorImpresoraMatricial.TextoIzquierda("USUARIO: " + proc_ComprobanteFacturaVenta_Results.First().UserName.ToString().ToUpper());
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoCentro("SISTEMA REALIZADO POR LIRIANO");
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
