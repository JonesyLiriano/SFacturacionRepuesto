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
        BindingList<proc_CargarTodasFacturas_Result> proc_CargarTodasFacturas_Results;        
        Thread hilo;
        bool finalLista;
        int indicePagina, tamanoPagina;
        string filtro, columna;
        public Facturas()
        {
            InitializeComponent();
        }

        private void Facturas_Load(object sender, EventArgs e)
        {
            try
            {
                proc_CargarTodasFacturas_Results = new BindingList<proc_CargarTodasFacturas_Result>();
                indicePagina = 1;
                tamanoPagina = 50;
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
                        ImpresionFacturaVenta impresionFacturaVenta = new ImpresionFacturaVenta(Convert.ToInt32(dgvFacturas.CurrentRow.Cells["FacturaID"].Value),0,0,0,0);
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
                List<proc_CargarTodasFacturas_Result> lista = facturasNegocio.CargarTodasFacturas(indicePagina, tamanoPagina, filtro, columna).ToList();
                if (lista.Count < tamanoPagina)
                {
                    finalLista = true;
                }
                foreach (var item in lista)
                {
                    proc_CargarTodasFacturas_Results.Add(item);
                }
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
                        ImpresionFacturaVenta impresionFacturaVenta = new ImpresionFacturaVenta(Convert.ToInt32(dgvFacturas.CurrentRow.Cells["FacturaID"].Value),0,0,0,0);
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
                ResetearBusqueda();
                CargarFacturas();
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

        private void cbFiltro_Validating(object sender, CancelEventArgs e)
        {
            if (cbFiltro.SelectedIndex == -1 && cbFiltro.Items.Count > 0)
            {
                cbFiltro.Focus();
            }
        }

        private void btnRealizarBusqueda_Click(object sender, EventArgs e)
        {
            try
            {
                ResetearBusqueda();
                if (txtFiltro.Text != "Escriba para filtrar...")
                {
                    switch (cbFiltro.SelectedItem.ToString())
                    {
                        case "ID":
                            columna = "f.FacturaID";
                            filtro = txtFiltro.Text;
                            CargarFacturas();
                            break;
                        case "Cliente":
                            columna = "c.Nombre";
                            filtro = txtFiltro.Text;
                            CargarFacturas();
                            break;
                        case "Fecha":
                            columna = "f.Fecha";
                            filtro = txtFiltro.Text;
                            CargarFacturas();
                            break;
                        case "Tipo de Pago":
                            columna = "tp.TipoDePago";
                            filtro = txtFiltro.Text;
                            CargarFacturas();
                            break;
                        case "Tipo de Factura":
                            columna = "tf.TipoFactura";
                            filtro = txtFiltro.Text;
                            CargarFacturas();
                            break;
                        case "NCF":
                            columna = "f.NCF";
                            filtro = txtFiltro.Text;
                            CargarFacturas();
                            break;
                        case "Cotizacion":
                            columna = "f.CotizacionID";
                            filtro = txtFiltro.Text;
                            CargarFacturas();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    CargarCBFiltro();
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

        private void dgvFacturas_Scroll(object sender, ScrollEventArgs e)
        {
            if (!finalLista)
            {
                if ((e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement) && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
                {
                    int display = dgvFacturas.Rows.Count - dgvFacturas.DisplayedRowCount(false);
                    if (e.NewValue >= dgvFacturas.Rows.Count - GetDisplayedRowsCount())
                    {
                        indicePagina++;
                        CargarFacturas();
                    }
                }
            }

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F1:
                    txtFiltro.Focus();
                    return true;
                case Keys.F5:
                    btnRealizarBusqueda.PerformClick();
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private int GetDisplayedRowsCount()
        {
            int count = dgvFacturas.Rows[dgvFacturas.FirstDisplayedScrollingRowIndex].Height;
            count = dgvFacturas.Height / count;
            return count;
        }

        private void ResetearBusqueda()
        {
            proc_CargarTodasFacturas_Results.Clear();
            finalLista = false;
            indicePagina = 1;
            filtro = null;
            columna = null;
        }
    }
}
