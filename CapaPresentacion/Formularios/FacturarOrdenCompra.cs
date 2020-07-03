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
    public partial class FacturarOrdenCompra : Form
    {
        TiposPagosNegocio tiposPagosNegocio = new TiposPagosNegocio();
        OrdenesCompraNegocio ordenesCompraNegocio = new OrdenesCompraNegocio();
        FacturasCompra facturasCompraEntidad = new FacturasCompra();
        FacturaOrdenCompraNegocio facturaOrdenCompraNegocio = new FacturaOrdenCompraNegocio();

        public FacturarOrdenCompra()
        {
            InitializeComponent();
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
                    }
                    
                    //CerrarOrdenCompra(Convert.ToInt32(txtOrdenCompraID.Text));
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

        private void CrearFacturaCompra()
        {
            //facturasCompraEntidad.ProveedorID = Convert.ToInt32(txtCodProveedor.Text);
            //facturasCompraEntidad.OrdenCompraID = Convert.ToInt32(txtOrdenCompraID.Text);
            //facturasCompraEntidad.NCF = txtNCF.Text;
            //facturasCompraEntidad.FechaVencimientoSecuencia = dtpFechaVSecuencia.Value;
            //facturasCompraEntidad.FechaFactura = dtpFechaFactura.Value;
            //facturasCompraEntidad.TipoDePagoID = (int)char.GetNumericValue(cbTipoPago.Text[0]);
            //facturasCompraEntidad.SubTotal = Convert.ToDecimal(txtSubTotal.Text);
            //facturasCompraEntidad.ITBIS = Convert.ToDecimal(txtITBIS.Text);
            //var result = facturaOrdenCompraNegocio.InsertarFacturaOrdenCompra(facturasCompraEntidad);
            //respuesta = result.Item1;
            //facturaCompraID = result.Item2;
            //validarInsertFactura(respuesta, facturaCompraID);


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
            if (string.IsNullOrEmpty(txtITBIS.Text) && decimal.TryParse(txtITBIS.Text ,out numberoDecimal))
            {
                MessageBox.Show("El Campo ITBIS no puede estar vacío y debe ser un numero valido");
                return false;
            }
            if (string.IsNullOrEmpty(txtSubTotal.Text) && decimal.TryParse(txtSubTotal.Text, out numberoDecimal))
            {
                MessageBox.Show("El Campo SUBTOTAL no puede estar vacío y debe ser un numero valido");
                return false;
            }
            return true;
        }
    }
}
