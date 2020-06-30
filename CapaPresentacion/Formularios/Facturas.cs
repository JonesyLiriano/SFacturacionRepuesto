using CapaDatos;
using CapaNegocios;
using CapaPresentacion.Reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Formularios
{
    public partial class Facturas : Form
    {
        FacturasNegocio facturasNegocio = new FacturasNegocio();
        List<proc_CargarTodasFacturas_Result> proc_CargarTodasFacturas_Results;        
        Thread hilo;
        public Facturas()
        {
            InitializeComponent();
        }

        private void Facturas_Load(object sender, EventArgs e)
        {
            CargarFacturas();
        }       
       
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvFacturas.SelectedRows.Count > 0)
                {
                    hilo = new Thread(() =>
                    {
                        ImpresionFacturaVenta impresionFacturaVenta = new ImpresionFacturaVenta(Convert.ToInt32(dgvFacturas.CurrentRow.Cells["FacturaID"].Value));
                        impresionFacturaVenta.Visible = false;
                        impresionFacturaVenta.ImprimirDirecto();
                    });
                    hilo.Start();

                }
                else
                {
                    MessageBox.Show("Debe de seleccionar al menos una factura para imprimir", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.ToString(),
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }

        }
        private void CargarFacturas()
        {
            try
            {
                proc_CargarTodasFacturas_Results = facturasNegocio.CargarTodasFacturas().ToList();
                dgvFacturas.DataSource = proc_CargarTodasFacturas_Results;
                dgvFacturas.Columns["Valor"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvFacturas.Refresh();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.ToString(),
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        } 
        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvFacturas.SelectedRows.Count > 0)
                {                   
                        ImpresionFacturaVenta impresionFacturaVenta = new ImpresionFacturaVenta(Convert.ToInt32(dgvFacturas.CurrentRow.Cells["FacturaID"].Value));
                        impresionFacturaVenta.ImprimirConVistaPrevia();
                        impresionFacturaVenta.Show();                   
                }
                else
                {
                    MessageBox.Show("Debe de seleccionar al menos una factura para imprimir", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.ToString(),
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void btnVerDetalles_Click(object sender, EventArgs e)
        {
            try
            {               
               if (dgvFacturas.SelectedRows.Count > 0)
               {
                   DetalleFacturaCotizacion detalleFacturaCotizacion
                    = new DetalleFacturaCotizacion("Facturas", Convert.ToInt32(dgvFacturas.CurrentRow.Cells["FacturaID"].Value));
                    detalleFacturaCotizacion.ShowDialog();

               }
               else
               {
                    MessageBox.Show("Debe de seleccionar al menos una factura para ver detalle", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               }              
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
