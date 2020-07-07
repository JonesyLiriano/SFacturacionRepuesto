using CapaPresentacion.Reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Formularios
{
    public partial class ElegirRangoFechas : Form
    {
        private string reporte;
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
                        reporteTodasFacturas.CargarTodasFacturas(dtPickerFechaInicial.Value, dtPickerFechaInicial.Value);
                        reporteTodasFacturas.Show();
              
                        break;
                    case "Facturas C Final":                        
                        reporteTodasFacturas.CargarTodasFacturasCFinal(dtPickerFechaInicial.Value, dtPickerFechaInicial.Value);
                        reporteTodasFacturas.Show();
                        break;
                    case "Facturas C Fiscal":                       
                        reporteTodasFacturas.CargarTodasFacturasCFiscal(dtPickerFechaInicial.Value, dtPickerFechaInicial.Value);
                        reporteTodasFacturas.Show();
                        break;
                    case "Facturas C Gubernamental":
                        reporteTodasFacturas.CargarTodasFacturasCGubernamental(dtPickerFechaInicial.Value, dtPickerFechaInicial.Value);
                        reporteTodasFacturas.Show();
                        break;
                    case "Facturas Compras":
                        ReporteFacturasCompra reporteFacturasCompra = new ReporteFacturasCompra(dtPickerFechaInicial.Value, dtPickerFechaInicial.Value);
                        reporteFacturasCompra.Show();
                        break;

                    case "Notas Credito":
                        ReporteNotasCredito reporteNotasCredito = new ReporteNotasCredito(dtPickerFechaInicial.Value, dtPickerFechaInicial.Value);
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
