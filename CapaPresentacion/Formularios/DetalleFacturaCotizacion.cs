using CapaDatos;
using CapaNegocios;
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
    public partial class DetalleFacturaCotizacion : Form
    {
        CotizacionesNegocio cotizacionesNegocio = new CotizacionesNegocio();
        List<proc_ComprobanteCotizacion_Result> proc_ComprobanteCotizacion_Results;
        FacturasNegocio facturasNegocio = new FacturasNegocio();
        List<proc_ComprobanteFacturaVenta_Result> proc_ComprobanteFacturaVenta_Results;
        decimal itbis, descuento, total, subtotal, cantProd;
        int id;
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

        private void LlenarDataGridView(string detalle)
        {
            if(detalle == "Cotizacion")
            {
                CargarProdCotizacion();
                CargarTextBoxs();
            } else
            {
                CargarProdFactura();
                CargarTextBoxs();
            }

            dgvCarrito.Columns["Precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCarrito.Columns["ITBIS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCarrito.Columns["Descuento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCarrito.Columns["Importe"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCarrito.Refresh();
        }

        private void CargarProdCotizacion()
        {
            dgvCarrito.Rows.Clear();
            proc_ComprobanteCotizacion_Results = cotizacionesNegocio.CargarComprobanteCotizacion(id).ToList();
            if (proc_ComprobanteCotizacion_Results.Count > 0)
            {
                foreach (var item in proc_ComprobanteCotizacion_Results)
                {
                    dgvCarrito.Rows.Add(item.ProductoID, item.UnidadMedida, item.Descripcion, item.CantVen,
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
                    dgvCarrito.Rows.Add(item.ProductoID, item.UnidadMedida, item.Descripcion, item.CantVen,
                        item.Precio, item.ITBIS, item.Descuento, (item.Precio + item.ITBIS - item.Descuento));
                }
            }

                      
        }
        private void CargarTextBoxs()
        {
            itbis = 0;
            descuento = 0;
            subtotal = 0;
            cantProd = 0;

            foreach (DataGridViewRow row in dgvCarrito.Rows)
            {
                itbis += (Convert.ToDecimal(row.Cells["ITBIS1"].Value) * Convert.ToDecimal(row.Cells["CantVen"].Value));
                descuento += (Convert.ToDecimal(row.Cells["CantVen"].Value) * Convert.ToDecimal(row.Cells["Descuento1"].Value));
                subtotal += (Convert.ToDecimal(row.Cells["CantVen"].Value) * Convert.ToDecimal(row.Cells["Precio"].Value));
                total += (Convert.ToDecimal(row.Cells["Importe"].Value));
                cantProd++;
            }
            descuento += (proc_ComprobanteFacturaVenta_Results.FirstOrDefault().Descuento * (subtotal + itbis - descuento));           

            txtSubTotal.Text = subtotal.ToString("F");
            txtITBIS.Text = itbis.ToString("F");
            txtDescuento.Text = descuento.ToString("F");
            txtTotal.Text = (total - descuento).ToString("F");
            txtCantProd.Text = cantProd.ToString();
        }
    }
}
