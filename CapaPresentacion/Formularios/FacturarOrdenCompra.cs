using CapaDatos;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Formularios
{
    public partial class FacturarOrdenCompra : Form
    {
        TiposPagosNegocio tiposPagosNegocio = new TiposPagosNegocio();
        OrdenesCompraNegocio ordenesCompraNegocio = new OrdenesCompraNegocio();
        FacturasCompra facturasCompraEntidad = new FacturasCompra();
        FacturaOrdenCompraNegocio facturaOrdenCompraNegocio = new FacturaOrdenCompraNegocio();
        LineasCreditoComprasNegocio lineasCreditoCompraNegocio = new LineasCreditoComprasNegocio();
        LineasCreditoCompra lineasCreditoCompraEntidad = new LineasCreditoCompra();
        private int ordenCompraID;
        private bool respuesta;

        public FacturarOrdenCompra(int ordenCompraID)
        {
            InitializeComponent();
            this.ordenCompraID = ordenCompraID;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CargarCBTipoPago()
        {
            cbTipoPago.Items.Clear();
            cbTipoPago.DisplayMember = "TipoDePago";
            cbTipoPago.ValueMember = "TipoPagoID";
            cbTipoPago.DataSource = tiposPagosNegocio.CargarTodosTiposPagos();
            cbTipoPago.SelectedIndex = -1;
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Esta seguro que desea cerrar y facturar la orden de compra?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    if(ValidarCampos())
                    {
                        CrearFacturaCompra();
                        CerrarOrdenCompra();
                        this.Close();
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

        private void CerrarOrdenCompra()
        {
            ordenesCompraNegocio.CerrarOrdenCompra(ordenCompraID);
        }
        private void CrearFacturaCompra()
        {
            facturasCompraEntidad.OrdenCompraID = ordenCompraID;
            facturasCompraEntidad.NCF = txtNCF.Text;
            facturasCompraEntidad.FechaVencimiento = dtPickerFechaVencimiento.Value;
            facturasCompraEntidad.FechaFactura = dtPickerFecha.Value;
            facturasCompraEntidad.TipoDePagoID = Convert.ToInt32(cbTipoPago.SelectedValue);
            facturasCompraEntidad.SubTotal = Convert.ToDecimal(txtSubTotal.Text);
            facturasCompraEntidad.ITBIS = Convert.ToDecimal(txtITBIS.Text);
            var result = facturaOrdenCompraNegocio.InsertarFacturaOrdenCompra(facturasCompraEntidad);
            respuesta = result.Item1;
            ValidarCreacionFactura(respuesta, result.Item2);


        }

        private void ValidarCreacionFactura(bool respuesta, int facturaCompraID)
        {
            if (respuesta)
            {
                CrearLineaCreditoCompra(facturaCompraID);
                MessageBox.Show(string.Format("La Factura ha sido agregada correctamente a la base de datos, con el numero de factura: {0}", facturaCompraID), "Factura Agregada Correctamente!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("La Factura no pudo ser agregada a la base de datos, favor de verificar los requerimientros", "Ha Ocurrido un error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CrearLineaCreditoCompra(int facturaCompraID)
        {
            lineasCreditoCompraEntidad.FacturaCompraID = facturaCompraID;
            lineasCreditoCompraEntidad.Estatus = false;
            lineasCreditoCompraNegocio.InsertarLineaCreditoCompra(lineasCreditoCompraEntidad);
        }


        private void FacturarOrdenCompra_Load(object sender, EventArgs e)
        {
            CargarCBTipoPago();
        }

        private bool ValidarCampos()
        {
            decimal numberoDecimal;
            if (string.IsNullOrEmpty(txtNCF.Text))
            {
                MessageBox.Show("El Campo NCF no puede estar vacío");
                return false;
            }
            if (cbTipoPago.SelectedIndex == -1)
            {
                MessageBox.Show("Debe de seleccionar un TIPO DE PAGO");
                return false;
            }
            if (string.IsNullOrEmpty(txtITBIS.Text) || !decimal.TryParse(txtITBIS.Text ,out numberoDecimal))
            {
                MessageBox.Show("El Campo ITBIS no puede estar vacío y debe ser un numero valido");
                return false;
            }
            if (string.IsNullOrEmpty(txtSubTotal.Text) || !decimal.TryParse(txtSubTotal.Text, out numberoDecimal))
            {
                MessageBox.Show("El Campo SUBTOTAL no puede estar vacío y debe ser un numero valido");
                return false;
            }
            return true;
        }

        private void cbTipoPago_Validating(object sender, CancelEventArgs e)
        {
            if (cbTipoPago.SelectedIndex == -1 && cbTipoPago.Items.Count > 0)
            {
                cbTipoPago.Focus();
            }
        }
    }
}
