using CapaDatos;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Formularios
{
    public partial class DetalleFacturaCotizacion : Form
    {
        CotizacionesNegocio cotizacionesNegocio = new CotizacionesNegocio();
        List<proc_ComprobanteCotizacion_Result> proc_ComprobanteCotizacion_Results;
        FacturasNegocio facturasNegocio = new FacturasNegocio();
        List<proc_ComprobanteFacturaVenta_Result> proc_ComprobanteFacturaVenta_Results;
        decimal itbisTotal, descuentoTotal, subtotal, cantProd;
        int id;
        CultureInfo ci = new CultureInfo("en-us");
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        public DetalleFacturaCotizacion(string detalle, int id)
        {
            InitializeComponent();
            this.id = id;
            LlenarDataGridView(detalle);
        }   

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DetalleFacturaCotizacion_Load(object sender, EventArgs e)
        {

        }

        private void LlenarDataGridView(string detalle)
        {
            try
            {
                if (detalle == "Cotizacion")
                {
                    CargarProdCotizacion();
                    CargarTextBoxs(detalle);
                }
                else
                {
                    CargarProdFactura();
                    CargarTextBoxs(detalle);
                }
                dgvCarrito.AutoGenerateColumns = false;
                dgvCarrito.Columns["Precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvCarrito.Columns["ITBIS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvCarrito.Columns["Descuento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvCarrito.Columns["Importe"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvCarrito.Refresh();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido llenar los detalles de esta factura o cotizacion, intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void CargarProdCotizacion()
        {
            dgvCarrito.Rows.Clear();
            proc_ComprobanteCotizacion_Results = cotizacionesNegocio.CargarComprobanteCotizacion(id).ToList();
            if (proc_ComprobanteCotizacion_Results.Count > 0)
            {
                foreach (var item in proc_ComprobanteCotizacion_Results)
                {
                    dgvCarrito.Rows.Add(item.ProductoID, item.Referencia, item.Descripcion, item.UnidadMedida, item.CantVen,
                        item.Precio, item.ITBIS, item.Descuento, (Convert.ToDecimal(item.CantVen) * (item.Precio + item.ITBIS - item.Descuento)).ToString("F"));
                }
            }
           
        }

        private void CargarProdFactura()
        {
            dgvCarrito.Rows.Clear();
            proc_ComprobanteFacturaVenta_Results = facturasNegocio.CargarComprobanteFacturaVenta(id).ToList();
            if (proc_ComprobanteFacturaVenta_Results.Count > 0)
            {
                foreach (var item in proc_ComprobanteFacturaVenta_Results)
                {
                    dgvCarrito.Rows.Add(item.ProductoID, item.Referencia, item.Descripcion, item.UnidadMedida, item.CantVen,
                        item.Precio, item.ITBIS, item.Descuento, (Convert.ToDecimal(item.CantVen) * (item.Precio + item.ITBIS - item.Descuento)).ToString("F"));
                }
            }          


        }
        private void CargarTextBoxs(string detalle)
        {
            itbisTotal = 0;
            descuentoTotal = 0;
            subtotal = 0;
            cantProd = 0;

            foreach (DataGridViewRow row in dgvCarrito.Rows)
            {
                itbisTotal += (Convert.ToDecimal(row.Cells["ITBIS"].Value) * Convert.ToDecimal(row.Cells["Cantidad"].Value));
                descuentoTotal += (Convert.ToDecimal(row.Cells["Cantidad"].Value) * Convert.ToDecimal(row.Cells["Descuento"].Value));
                subtotal += (Convert.ToDecimal(row.Cells["Cantidad"].Value) * Convert.ToDecimal(row.Cells["Precio"].Value));
                cantProd++;
            }
            if(detalle == "Cotizacion")
            {
                descuentoTotal += ((Convert.ToDecimal(proc_ComprobanteCotizacion_Results.FirstOrDefault().DescuentoCliente) / 100) * (subtotal));

            }
            else
            {
                descuentoTotal += ((Convert.ToDecimal(proc_ComprobanteFacturaVenta_Results.FirstOrDefault().DescuentoCliente) / 100) * (subtotal));

            }

            txtSubTotal.Text = subtotal.ToString("C", ci);
            txtITBIS.Text = itbisTotal.ToString("C", ci);
            txtDescuento.Text = descuentoTotal.ToString("C", ci);
            txtTotal.Text = (subtotal + itbisTotal - descuentoTotal).ToString("C", ci);
            txtCantProd.Text = cantProd.ToString();           
        }
    }
}
