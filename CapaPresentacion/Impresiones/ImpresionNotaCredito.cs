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
    public partial class ImpresionNotaCredito : Form
    {
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
                ControladorImpresoraPapelA4 controladorImpresoraPapelA4 = new ControladorImpresoraPapelA4();
                controladorImpresoraPapelA4.Imprime(CargarImpresionRV());
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
                controladorImpresoraMatricial.TextoCentro("NOTA DE CREDITO");
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
                controladorImpresoraMatricial.TextoIzquierda("CANTIDAD DE ARTICULOS DEVUELTOS:" + " " + cantArticulos);
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.AgregarTotales("                   ITBIS : $ ", proc_ComprobanteNotaDeCredito_Results.First().ITBIS ? Convert.ToDecimal((proc_ComprobanteNotaDeCredito_Results.First().PrecioTotal * (Properties.Settings.Default.ITBIS / 100))) :
                    Convert.ToDecimal(0.00));
                controladorImpresoraMatricial.AgregarTotales("          VALOR APLICADO : $ ", Convert.ToDecimal(proc_ComprobanteNotaDeCredito_Results.First().PrecioTotal));
                controladorImpresoraMatricial.lineasGuio();
                controladorImpresoraMatricial.TextoIzquierda("COD.CLIENTE: " + proc_ComprobanteNotaDeCredito_Results.First().ClienteID.ToString());
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
