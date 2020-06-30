using CapaDatos;
using CapaNegocios;
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
    public partial class CuentasPorCobrar : Form
    {       
        public static decimal montoPendiente;
        LineasCreditoVentasNegocio lineasCreditoVentasNegocio = new LineasCreditoVentasNegocio();
        List<proc_CargarTodasLineasCreditoVentas_Result> proc_CargarTodasLineasCreditoVentas_Results;
        public CuentasPorCobrar()
        {
            InitializeComponent();
        }

        private void CuentasPorCobrar_Load(object sender, EventArgs e)
        {
            try
            {
                dgvLineasCreditoVenta.Columns["BalancePendiente"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvLineasCreditoVenta.Columns["MontoFactura"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                CargarTodasLineasCreditoVenta();
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }   
      
        private void CargarTodasLineasCreditoVenta()
        {
            proc_CargarTodasLineasCreditoVentas_Results = lineasCreditoVentasNegocio.CargarTodasLineasCreditoVentas().ToList();
            dgvLineasCreditoVenta.DataSource = proc_CargarTodasLineasCreditoVentas_Results;

        }
        private void btnRealizarCobro_Click(object sender, EventArgs e)
        {
            try
            {     
                if (dgvLineasCreditoVenta.SelectedRows.Count > 0)
                {
                    if (Convert.ToBoolean(dgvLineasCreditoVenta.Rows[dgvLineasCreditoVenta.CurrentRow.Index].Cells["Completada"].Value))
                    {
                        RegistrarCobro registrarCobro = new RegistrarCobro(
                            Convert.ToInt32(dgvLineasCreditoVenta.CurrentRow.Cells["LineaCreditoVentaID"].Value)
                        , dgvLineasCreditoVenta.CurrentRow.Cells["Cliente"].Value.ToString()
                        , Convert.ToDecimal(dgvLineasCreditoVenta.CurrentRow.Cells["BalancePendiente"].Value));
                        registrarCobro.ShowDialog();
                        CargarTodasLineasCreditoVenta();

                    }
                    else
                    {
                        MessageBox.Show("La linea de credito venta ya esta saldada.", "Linea Saldada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Debe de seleccionar la linea de credito venta a la que desea realizar el cobro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }


        }

        private void btnHistorialCobros_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLineasCreditoVenta.SelectedRows.Count > 0)
                {
                    HistorialCobros historialCobros = new HistorialCobros(
                        Convert.ToInt32(dgvLineasCreditoVenta.CurrentRow.Cells["LineaCreditoVentaID"].Value)
                        , dgvLineasCreditoVenta.CurrentRow.Cells["Cliente"].Value.ToString()
                        , Convert.ToDecimal(dgvLineasCreditoVenta.CurrentRow.Cells["BalancePendiente"].Value)
                        , Convert.ToBoolean(dgvLineasCreditoVenta.CurrentRow.Cells["Status"].Value));
                    historialCobros.ShowDialog();
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }

        }  

        private void btnConsultaFactura_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLineasCreditoVenta.SelectedRows.Count > 0)
                {
                    DetalleFacturaCotizacion detalleFacturaCotizacion
                    = new DetalleFacturaCotizacion("Facturas", Convert.ToInt32(dgvLineasCreditoVenta.Rows[dgvLineasCreditoVenta.CurrentRow.Index].Cells["Factura"].Value));
                    detalleFacturaCotizacion.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Debe de seleccionar por lo menos una linea de credito para ver el detalle de la factura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
