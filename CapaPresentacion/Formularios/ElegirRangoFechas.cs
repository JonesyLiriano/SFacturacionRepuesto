using CapaPresentacion.Reportes;
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

namespace CapaPresentacion.Formularios
{
    public partial class ElegirRangoFechas : Form
    {
        private string reporte;
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        public ElegirRangoFechas(string reporte)
        {
            InitializeComponent();
            this.reporte = reporte;
        }

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                ReporteTodasFacturas reporteTodasFacturas = new ReporteTodasFacturas();
                switch (reporte)
                {                   
                    case "Facturas Ventas":                        
                        reporteTodasFacturas.CargarTodasFacturas(dtPickerFechaInicial.Value, dtPickerFechaFinal.Value);
                        reporteTodasFacturas.Show();
              
                        break;
                    case "Facturas C Final":                        
                        reporteTodasFacturas.CargarTodasFacturasCFinal(dtPickerFechaInicial.Value, dtPickerFechaFinal.Value);
                        reporteTodasFacturas.Show();
                        break;
                    case "Facturas C Fiscal":                       
                        reporteTodasFacturas.CargarTodasFacturasCFiscal(dtPickerFechaInicial.Value, dtPickerFechaFinal.Value);
                        reporteTodasFacturas.Show();
                        break;
                    case "Facturas C Gubernamental":
                        reporteTodasFacturas.CargarTodasFacturasCGubernamental(dtPickerFechaInicial.Value, dtPickerFechaFinal.Value);
                        reporteTodasFacturas.Show();
                        break;
                    case "Facturas Compras":
                        ReporteFacturasCompra reporteFacturasCompra = new ReporteFacturasCompra(dtPickerFechaInicial.Value, dtPickerFechaFinal.Value);
                        reporteFacturasCompra.Show();
                        break;

                    case "Notas Credito":
                        ReporteNotasCredito reporteNotasCredito = new ReporteNotasCredito(dtPickerFechaInicial.Value, dtPickerFechaFinal.Value);
                        reporteNotasCredito.Show();
                        break;
                    default:
                        break;
                }
                this.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.ToString(),
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

       
    }
}
