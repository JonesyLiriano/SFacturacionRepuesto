using CapaDatos;
using CapaNegocios;
using CapaPresentacion.Impresiones;
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
            try
            {
                CargarFacturas();
                CargarCBFiltro();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido cargar las facturas, intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
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
                MessageBox.Show("Error: No se ha podido imprimir la factura, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }

        }
        private void CargarFacturas()
        {
            try
            {
                dgvFacturas.AutoGenerateColumns = false;
                proc_CargarTodasFacturas_Results = facturasNegocio.CargarTodasFacturas().ToList();
                dgvFacturas.DataSource = proc_CargarTodasFacturas_Results;
                OrdenarColumnasDGV();
                
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido cargar las facturas, intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        } 
        private void OrdenarColumnasDGV()
        {
            dgvFacturas.Columns["FacturaID"].DisplayIndex = 0;
            dgvFacturas.Columns["Cliente"].DisplayIndex = 1;
            dgvFacturas.Columns["DescuentoCliente"].DisplayIndex = 2;
            dgvFacturas.Columns["Fecha"].DisplayIndex = 3;
            dgvFacturas.Columns["TipoDePago"].DisplayIndex = 4;
            dgvFacturas.Columns["TipoFactura"].DisplayIndex = 5;
            dgvFacturas.Columns["Cotizacion"].DisplayIndex = 6;
            dgvFacturas.Columns["NCF"].DisplayIndex = 7;
            dgvFacturas.Columns["FechaVencimiento"].DisplayIndex = 8;
            dgvFacturas.Columns["Valor"].DisplayIndex = 9;
            dgvFacturas.Columns["Usuario"].DisplayIndex = 10;
            dgvFacturas.Columns["ClienteID"].DisplayIndex = 11;

            dgvFacturas.Columns["DescuentoCliente"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvFacturas.Columns["Valor"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvFacturas.Refresh();
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
                MessageBox.Show("Error: No se ha podido exportar la factura, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor.",
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
                MessageBox.Show("Ha ocurrido un error intentando ver el detalle de esta factura, intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void CargarCBFiltro()
        {
            cbFiltro.Items.Add("ID");
            cbFiltro.Items.Add("Cliente");
            cbFiltro.Items.Add("Fecha");
            cbFiltro.Items.Add("Tipo de Pago");
            cbFiltro.Items.Add("Tipo de Factura");
            cbFiltro.Items.Add("NCF");
            cbFiltro.Items.Add("Cotizacion");
            cbFiltro.SelectedIndex = 0;
        }
        private void txtFiltro_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFiltro.Text))
            {
                txtFiltro.Text = "Escriba para filtrar...";
                txtFiltro.ForeColor = Color.Gray;
            }
        }

        private void txtFiltro_Enter(object sender, EventArgs e)
        {
            if (txtFiltro.Text == "Escriba para filtrar...")
            {
                txtFiltro.Text = "";
                txtFiltro.ForeColor = Color.Black;
            }
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtFiltro.Text != "Escriba para filtrar...")
                {
                    switch (cbFiltro.SelectedItem.ToString())
                    {
                        case "ID":
                            dgvFacturas.DataSource = proc_CargarTodasFacturas_Results.Where(p => p.FacturaID.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Cliente":
                            dgvFacturas.DataSource = proc_CargarTodasFacturas_Results.Where(p => p.Cliente.ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Fecha":
                            dgvFacturas.DataSource = proc_CargarTodasFacturas_Results.Where(p => p.Fecha.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Tipo de Pago":
                            dgvFacturas.DataSource = proc_CargarTodasFacturas_Results.Where(p => p.TipoDePago.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Tipo de Factura":
                            dgvFacturas.DataSource = proc_CargarTodasFacturas_Results.Where(p => p.TipoFactura.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "NCF":
                            dgvFacturas.DataSource = proc_CargarTodasFacturas_Results.Where(p => p.NCF.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Cotizacion":
                            dgvFacturas.DataSource = proc_CargarTodasFacturas_Results.Where(p => p.Cotizacion.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        default:
                            break;
                    }
                }
                OrdenarColumnasDGV();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido realizar la busqueda, intente de nuevo por favor.",
                      "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void cbFiltro_Validating(object sender, CancelEventArgs e)
        {
            if (cbFiltro.SelectedIndex == -1 && cbFiltro.Items.Count > 0)
            {
                cbFiltro.Focus();
            }
        }

    }
}
