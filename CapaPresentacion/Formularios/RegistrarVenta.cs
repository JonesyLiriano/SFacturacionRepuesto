﻿using CapaDatos;
using CapaNegocios;
using CapaPresentacion.Reportes;
using SFacturacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class RegistrarVenta : Form
    {
        public bool valida = false;
        private int contFila = 0;
        private decimal total = 0;
        private int facturaID, numFila, NCF, cotizacionID;
        CotizacionesNegocio cotizacionesNegocio = new CotizacionesNegocio();
        Cotizacione cotizacionEntidad = new Cotizacione();
        DetalleCotizacione detalleCotizacionEntidad = new DetalleCotizacione();
        DetalleCotizacionesNegocio detalleCotizacionesNegocio = new DetalleCotizacionesNegocio();
        ClientesNegocio clientesNegocio = new ClientesNegocio();
        ProductosNegocio productosNegocio = new ProductosNegocio();
        TiposPagosNegocio tiposPagosNegocio = new TiposPagosNegocio();
        DetalleFacturasNegocio detalleFacturasNegocio = new DetalleFacturasNegocio();
        FacturasNegocio facturasNegocio = new FacturasNegocio();
        Factura facturaEntidad = new Factura();
        DetalleFactura detalleFacturaEntidad = new DetalleFactura();
        Producto productoEntidad = new Producto();
        Producto productoCarritoEntidad = new Producto();
        TiposFacturaNegocio tiposFacturaNegocio = new TiposFacturaNegocio();
        LineasCreditoVenta lineasCreditoVentaEntidad = new LineasCreditoVenta();
        LineasCreditoVentasNegocio lineasCreditoVentasNegocio = new LineasCreditoVentasNegocio();
        List<proc_CargarCotizacionesActivas_Result> proc_CargarCotizacionesActivas_Results;
        List<proc_CargarProductosCotizacion_Result> proc_CargarProductosCotizacion_Results;
        private bool existe;
        private decimal descuentoProd, campoPrecio;
        private decimal itbisProd, itbisTotal,
            subTotal, descuentoTotal, descuentoCliente,
            campoITBIS, campoDescuento, descuentoClientePorciento;
        private double cantProdFinal;
        Thread hilo;
        CultureInfo ci = new CultureInfo("en-us");
        public static string codigoBarraProd;


        public RegistrarVenta()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RegistrarVenta_Load(object sender, EventArgs e)
        {
            try
            {
                CargarCBTipoPago();
                CargarCBTipoFactura();
                CargarCBClientes();
                CargarCBCotizaciones();
                this.AcceptButton = btnColocar;
                txtCodigoBarra.Select();
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void ColocarProdCarrito()
        {
            if (VerificarCantidadProducto())
            {
                LlevarProdCarrito();
                CalcularTotalFactura();
            }
        }

        private void txtCodigoBarra_TextChanged(object sender, EventArgs e)
        {
            try
            {
                BuscarDatosProducto();

            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void ActualizarCantProducto()
        {
            // Coge la cantidad y codigo de barra del carrito para actualizar cantidad de todos los productos
            if (dgvCarrito.Rows.Count != 0)
            {
                foreach (DataGridViewRow Fila in dgvCarrito.Rows)
                {
                    if(!Convert.ToBoolean(Fila.Cells["Servicio"].Value))
                    {   
                        productoEntidad.Existencia = Convert.ToDouble(Fila.Cells["Cantidad"].Value);
                        productoEntidad.CodigoBarra = Convert.ToString(Fila.Cells["CodigoBarra"].Value);
                        productosNegocio.ActualizarCantidadProducto(productoEntidad);
                    }                    
                }
            }
        }


        private bool VerificarCantidadProducto()
        {
            if (!string.IsNullOrEmpty(txtCodigoBarra.Text) && !string.IsNullOrEmpty(txtDescripcion.Text) && !string.IsNullOrEmpty(txtPrecio.Text) && !string.IsNullOrEmpty(txtCantidad.Text))
            {               
                if (productoCarritoEntidad != null)
                {
                    if (productoCarritoEntidad.Servicio)
                    {
                        return true;

                    } else if (ConfirmarCant())
                    {
                        return true;
                    }
                }
                else
                {
                    MessageBox.Show(string.Format("El Producto/Servicio con Codigo de Barra: {0} No Existe en almacen...", txtCodigoBarra.Text), "El Producto/Servicio No Existe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return false;
        }

        private bool ConfirmarCant()
        {

            cantProdFinal = Convert.ToDouble(txtCantidad.Text);
            if (dgvCarrito.Rows.Count != 0)
            {
                foreach (DataGridViewRow Fila in dgvCarrito.Rows)
                {
                    if (Convert.ToString(Fila.Cells["CodigoBarra"].Value) == txtCodigoBarra.Text)
                    {
                        cantProdFinal = Convert.ToDouble(Fila.Cells["Cantidad"].Value) + Convert.ToDouble(txtCantidad.Text);
                        break;
                    }
                }
            }
            return CantidadConfirmadaProd(cantProdFinal);
        }
        private bool CantidadConfirmadaProd(double cantProdFinal)
        {
            if (cantProdFinal > productoCarritoEntidad.Existencia)
            {
                MessageBox.Show(string.Format("Cantidad del producto/servicio: {0} no disponible en almacen...", txtDescripcion.Text), "Cantidad Insuficiente del Producto/Servicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }


        private void LlevarProdCarrito()
        {
            existe = false;
            numFila = 0;

            if (contFila == 0)
            {
                campoPrecio = Convert.ToDecimal(txtPrecio.Text) - (Convert.ToDecimal(txtPrecio.Text) * itbisProd);
                campoDescuento = campoPrecio * descuentoProd;
                campoITBIS = Convert.ToDecimal(txtPrecio.Text) * itbisProd;
                dgvCarrito.Rows.Add(productoCarritoEntidad.ProductoID, txtCodigoBarra.Text, txtDescripcion.Text, txtUnidadMedida.Text, txtCantidad.Text, 
                    campoPrecio.ToString("F"), campoITBIS.ToString("F"), campoDescuento.ToString("F"),
                    (Convert.ToDecimal(txtCantidad.Text) * (campoPrecio + campoITBIS - campoDescuento)).ToString("F") ,productoCarritoEntidad.Servicio);        
                
                contFila++;
            }
            else
            {
                foreach (DataGridViewRow Fila in dgvCarrito.Rows)
                {
                    if (Fila.Cells["CodigoBarra"].Value.ToString() == txtCodigoBarra.Text)
                    {
                        existe = true;
                        numFila = Fila.Index;
                    }
                }

                if (existe == true)
                {
                    dgvCarrito.Rows[numFila].Cells["Cantidad"].Value = Convert.ToDecimal(txtCantidad.Value) 
                        + Convert.ToDecimal(dgvCarrito.Rows[numFila].Cells["Cantidad"].Value);

                    dgvCarrito.Rows[numFila].Cells["Importe"].Value = (Convert.ToDecimal(dgvCarrito.Rows[numFila].Cells["Cantidad"].Value)
                        * (Convert.ToDecimal(dgvCarrito.Rows[numFila].Cells["Precio"].Value)
                        + Convert.ToDecimal(dgvCarrito.Rows[numFila].Cells["ITBIS"].Value)
                        - Convert.ToDecimal(dgvCarrito.Rows[numFila].Cells["Descuento"].Value))).ToString("F");
                }
                else
                {
                    campoPrecio = Convert.ToDecimal(txtPrecio.Text) - (Convert.ToDecimal(txtPrecio.Text) * itbisProd);
                    campoDescuento = campoPrecio * descuentoProd;
                    campoITBIS = Convert.ToDecimal(txtPrecio.Text) * itbisProd;
                    dgvCarrito.Rows.Add(productoCarritoEntidad.ProductoID, txtCodigoBarra.Text, txtDescripcion.Text, txtUnidadMedida.Text,txtCantidad.Text, 
                        campoPrecio.ToString("F"), campoITBIS.ToString("F"), campoDescuento.ToString("F"),
                        (Convert.ToDecimal(txtCantidad.Text) * (campoPrecio + campoITBIS - campoDescuento)).ToString("F"), productoCarritoEntidad.Servicio);

                    contFila++;
                }
            }
            dgvCarrito.Columns["Precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCarrito.Columns["ITBIS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCarrito.Columns["Descuento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCarrito.Columns["Importe"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCarrito.Refresh();
        }

        private void CalcularTotalFactura()
        {
            total = 0;
            itbisTotal = 0;
            descuentoTotal = 0;
            subTotal = 0;
            descuentoCliente = 0;

            foreach (DataGridViewRow Fila in dgvCarrito.Rows)
            {
                itbisTotal += Convert.ToDecimal(Fila.Cells["ITBIS"].Value) * Convert.ToDecimal(Fila.Cells["Cantidad"].Value);
                descuentoTotal += Convert.ToDecimal(Fila.Cells["Descuento"].Value) * Convert.ToDecimal(Fila.Cells["Cantidad"].Value);
                subTotal += Convert.ToDecimal(Fila.Cells["Precio"].Value) * Convert.ToDecimal(Fila.Cells["Cantidad"].Value);


            }

            if (string.IsNullOrEmpty(txtDescuentoCliente.Text))
            {
                descuentoClientePorciento = 0;
            }
            else
            {
                descuentoClientePorciento = (decimal.Parse(txtDescuentoCliente.Text)) / 100;
            }

            descuentoCliente = ((subTotal) * descuentoClientePorciento) + descuentoTotal;
            total = subTotal + itbisTotal - descuentoCliente;
            txtTotal.Text = total.ToString("C", ci);
            txtSubTotal.Text = subTotal.ToString("C", ci);
            txtITBIS.Text = itbisTotal.ToString("C", ci);
            txtDescuento.Text = descuentoCliente.ToString("C", ci);
            txtCantProd.Text = dgvCarrito.Rows.Count.ToString();
        }

        private void btnColocar_Click(object sender, EventArgs e)
        {
            try
            {
                ColocarProdCarrito();
                txtCodigoBarra.Clear();
                txtDescripcion.Clear();
                txtUnidadMedida.Clear();
                txtCantidad.Value = 1;
                txtPrecio.Clear();
                txtCodigoBarra.Select();
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }


        }

        private void RegistrarVenta_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (dgvCarrito.RowCount > 0)
                {
                    DialogResult dialogResult = MessageBox.Show("Esta Seguro que desea cerrar el formulario de ventas?", "Confirme para salir", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        e.Cancel = false;
                    }
                    else
                    {
                        e.Cancel = true;
                        txtCodigoBarra.Select();
                    }
                }
                else
                {
                    e.Cancel = false;

                }

            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (contFila > 0)
                {

                    dgvCarrito.Rows.RemoveAt(dgvCarrito.CurrentRow.Index);
                    total = 0;
                    subTotal = 0;
                    itbisTotal = 0;
                    descuentoTotal = 0;
                    CalcularTotalFactura();
                    contFila--;
                }
                txtCodigoBarra.Select();
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void dgvCarrito_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvCarrito.Rows.Count > 0)
                    dgvCarrito.Rows[dgvCarrito.CurrentRow.Index].Selected = true;

            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void txtPrecioVenta_Leave(object sender, EventArgs e)
        {
            decimal precioConvertido;
            if (decimal.TryParse(txtPrecio.Text, out precioConvertido))
            {
                try
                {
                    if (productoCarritoEntidad.PrecioVentaMin > precioConvertido)
                    {
                        MessageBox.Show("El precio de venta esta por debajo de lo permitido, favor de digitar otro precio", "Error en Precio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPrecio.Text = productoCarritoEntidad.PrecioVentaMin.ToString();
                    }
                }
                catch (Exception exc)
                {

                    MessageBox.Show("Error: " + exc.ToString(),
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Loggeator.EscribeEnArchivo(exc.ToString());
                }
            }
            else
            {
                MessageBox.Show("Debe de digitar un numero valido", "Error en Precio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPrecio.Text = productoCarritoEntidad.PrecioVenta.ToString();
            }
        }

        private void LimpiarFormulario()
        {            
                dgvCarrito.Rows.Clear();
                dgvCarrito.Refresh();
                total = 0;
                subTotal = 0;
                itbisTotal = 0;
                descuentoTotal = 0;
                txtCodigoBarra.Clear();
                txtDescripcion.Clear();
                txtUnidadMedida.Clear();
                txtCantidad.Value = 1;
                txtPrecio.Clear();
                txtCodigoBarra.Select();
                cbTipoFactura.SelectedIndex = 0;
                cbTipoPago.SelectedIndex = 0;
                txtRNC.Clear();
                txtRazonSocial.Clear();
                checkBoxClienteAnonimo.Checked = true;
                CalcularTotalFactura();
                contFila = 0;
                cbCotizacion.Enabled = true;
                cbCotizacion.SelectedIndex = -1;
                txtMontoCotizacion.Clear();
                txtFechaCotizacion.Clear();           

        }

        private void btnLimpiarCarrito_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Esta Seguro que desea limpiar el carrito?", "Confirme", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    LimpiarCarrito();
                    cbCotizacion.SelectedIndex = -1;
                    cbCotizacion.Enabled = true;
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }

        }
        private void LimpiarCarrito()
        {            
            dgvCarrito.Rows.Clear();
            dgvCarrito.Refresh();
            total = 0;
            subTotal = 0;
            itbisTotal = 0;
            descuentoTotal = 0;
            CalcularTotalFactura();
            txtCodigoBarra.Select();
            contFila = 0;
        }

        private void CargarCBTipoPago()
        {
            cbTipoPago.Items.Clear();
            cbTipoPago.DisplayMember = "TipoDePago";
            cbTipoPago.ValueMember = "TipoPagoID";
            cbTipoPago.DataSource = tiposPagosNegocio.CargarTodosTiposPagos();
            cbTipoPago.SelectedIndex = 0;
        }

        private void cbTipoPago_Validating(object sender, CancelEventArgs e)
        {
            if (cbTipoPago.SelectedIndex == -1)
            {
                cbTipoPago.Focus();
            }
        }

        private void cbClientes_Validating(object sender, CancelEventArgs e)
        {
            if (cbClientes.SelectedIndex == -1)
            {
                cbClientes.Focus();
            }
        }

        private void cbTipoFactura_Validating(object sender, CancelEventArgs e)
        {
            if (cbTipoFactura.SelectedIndex == -1)
            {
                cbTipoFactura.Focus();
            }
        }       

        private void btnCotizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (contFila != 0)
                {
                    DialogResult dialogResult = MessageBox.Show("Esta seguro que desea facturar los productos/servicios en el carrito?", "Confirme para facturar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        CrearCotizacion();
                        hilo = new Thread(() =>
                        {
                            ImpresionCotizacion impresionCotizacion = new ImpresionCotizacion(cotizacionID);
                            impresionCotizacion.Visible = false;
                            impresionCotizacion.ImprimirDirecto();
                        });
                        hilo.Start();
                        MessageBox.Show("Cotizacion realizada correctamente!", "Cotizacion generada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarFormulario();
                        CargarCBCotizaciones();
                    }
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void CrearCotizacion()
        {
            {
                if (checkBoxClienteAnonimo.Checked == true)
                {
                    cotizacionEntidad.ClienteID = 1;
                }
                else
                {
                    cotizacionEntidad.ClienteID = Convert.ToInt32(cbClientes.SelectedValue);
                }

                cotizacionEntidad.Fecha = DateTime.Now;
                cotizacionEntidad.UserID = Login.userID;
                cotizacionEntidad.DescuentoCliente = Convert.ToDecimal(txtDescuentoCliente.Text);

                var result = cotizacionesNegocio.InsertarCotizacion(cotizacionEntidad);
                if (result.Item1)
                {                    
                    cotizacionID = result.Item2;
                    CrearDetalleCotizacion();
                }
            }
        }
        public void CrearDetalleCotizacion()
        {
            foreach (DataGridViewRow fila in dgvCarrito.Rows)
            {
                detalleCotizacionEntidad.CotizacionID = cotizacionID;
                detalleCotizacionEntidad.ProductoID = Convert.ToInt32(fila.Cells["ProductoID"].Value);
                detalleCotizacionEntidad.Cantidad = Convert.ToDouble(fila.Cells["Cantidad"].Value);
                detalleCotizacionEntidad.Precio = Convert.ToDecimal(fila.Cells["Precio"].Value);
                detalleCotizacionEntidad.ITBIS = Convert.ToDecimal(fila.Cells["ITBIS"].Value);
                detalleCotizacionEntidad.Descuento = Convert.ToDecimal(fila.Cells["Descuento"].Value);

                detalleCotizacionesNegocio.InsertarDetalleCotizacion(detalleCotizacionEntidad);

            }
        }
        private void checkBoxClienteAnonimo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBoxClienteAnonimo.Checked == true)
                {
                    cbClientes.SelectedIndex = 0;
                    cbClientes.Enabled = false;
                    txtDescuentoCliente.Enabled = false;
                    txtDescuento.Text = "0";


                }
                else
                {                    
                    txtDescuentoCliente.Enabled = true;
                    cbClientes.Enabled = true;
                    cbClientes.SelectedIndex = 0;
                }

            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void cbCotizacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbCotizacion.SelectedIndex != -1)
                {
                    txtFechaCotizacion.Text = proc_CargarCotizacionesActivas_Results.Where(r => r.CotizacionID == Convert.ToInt32(cbCotizacion.SelectedValue))
                        .FirstOrDefault().Fecha.ToString();
                    txtMontoCotizacion.Text = proc_CargarCotizacionesActivas_Results.Where(r => r.CotizacionID == Convert.ToInt32(cbCotizacion.SelectedValue))
                        .FirstOrDefault().Valor.Value.ToString("C", ci);
                } 
                else
                {
                    txtFechaCotizacion.Clear();
                    txtMontoCotizacion.Clear();
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void btnCargarProd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbCotizacion.SelectedIndex != -1)
                {
                    DialogResult dialogResult = MessageBox.Show("Esta seguro que desea llevar los productos/servicios de la cotizacion al carrito?", "Confirme para cargar productos/servicios", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        LimpiarCarrito();
                        cbCotizacion.Enabled = false;
                        CargarProdCotizacionEnCarrito();
                        CalcularTotalFactura();


                    }
                } else
                {
                    MessageBox.Show("Debe de elegir un numero de cotizacion que se encuentre activo", "Cotizacion no encontrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }

        }

        private void CargarProdCotizacionEnCarrito()
        {
            proc_CargarProductosCotizacion_Results = cotizacionesNegocio.CargarProductosCotizacion(Convert.ToInt32(cbCotizacion.SelectedValue)).ToList();
           if(proc_CargarProductosCotizacion_Results.Count > 0)
            {
                foreach (proc_CargarProductosCotizacion_Result item in proc_CargarProductosCotizacion_Results)
                {
                    dgvCarrito.Rows.Add(item.ID, item.CodigoBarra, item.Descripcion, item.UnidadMedida, item.Cantidad, item.Precio, item.ITBIS,
                        item.Descuento, item.Importe, item.Servicio);
                    contFila++;
                }
            }
            dgvCarrito.Columns["Precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCarrito.Columns["ITBIS"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCarrito.Columns["Descuento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCarrito.Columns["Importe"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCarrito.Refresh();
        }
        private void btnBuscarProd_Click(object sender, EventArgs e)
        {
            try
            {
                codigoBarraProd = null;
                Productos formProductos = new Productos();
                formProductos.Controls["btnSeleccionar"].Visible = true;
                formProductos.Controls["btnNuevo"].Visible = false;
                formProductos.Controls["btnEliminar"].Visible = false;
                formProductos.Controls["btnEditar"].Visible = false;
                formProductos.AcceptButton = (IButtonControl)formProductos.Controls["btnSeleccionar"];
                formProductos.ShowDialog();
                if (codigoBarraProd != null)
                {
                    txtCodigoBarra.Text = codigoBarraProd;
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void cbCotizacion_Validating(object sender, CancelEventArgs e)
        {
            if (cbCotizacion.SelectedIndex == -1 && cbCotizacion.Items.Count > 0)
            {
                MessageBox.Show("Debe de elegir un numero de cotizacion que se encuentre activo", "Cotizacion no encontrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarCBCotizaciones()
        {
            proc_CargarCotizacionesActivas_Results = cotizacionesNegocio.CargarCotizacionesActivas().ToList();
            cbCotizacion.DataSource = null;
            cbCotizacion.DisplayMember = "CotizacionID";
            cbCotizacion.ValueMember = "CotizacionID";
            cbCotizacion.DataSource = proc_CargarCotizacionesActivas_Results;
            cbCotizacion.SelectedIndex = -1;
            
        }
        private void CargarCBClientes() 
        {
            cbClientes.DataSource = null;
            cbClientes.DisplayMember = "Nombre";
            cbClientes.ValueMember = "ClienteID";
            cbClientes.DataSource = clientesNegocio.CargarTodosClientes();
            cbClientes.SelectedIndex = 0;

        }
        private void CargarCBTipoFactura()
        {
            cbTipoFactura.DataSource = null;
            cbTipoFactura.DisplayMember = "TipoFactura";
            cbTipoFactura.ValueMember = "TipoFacturaID";
            cbTipoFactura.DataSource = tiposFacturaNegocio.CargarTodosTiposFactura();            
            cbTipoFactura.SelectedIndex = 0;
        }

        private void cbTipoFactura_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(cbTipoFactura.SelectedValue) == 1)
                {
                    txtRNC.Clear();
                    txtRazonSocial.Clear();
                    gbComprobante.Visible = false;
                    txtRazonSocial.Enabled = false;
                    txtRNC.Enabled = false;
                }
                else
                {
                    gbComprobante.Visible = true;
                    txtRazonSocial.Enabled = true;
                    txtRNC.Enabled = true;
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }

        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            try
            {
                if (contFila != 0)
                {
                    DialogResult dialogResult = MessageBox.Show("Esta seguro que desea facturar los productos/servicios en el carrito?", "Confirme para facturar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (Convert.ToInt32(cbTipoPago.SelectedValue) == 3 || Convert.ToInt32(cbTipoPago.SelectedValue) == 4)
                        {
                            if (clientesNegocio.VerificarCredito(Convert.ToInt32(cbClientes.SelectedValue), decimal.Parse(Regex.Replace(txtTotal.Text, @"[^\d.]", ""))))
                            {
                                VerificarTipoFactura();
                                CrearLineaCreditoVenta();

                            }
                            else
                            {
                                MessageBox.Show("Cliente no posee creditos suficientes para la compra...", "Creditos Insuficientes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            VerificarTipoFactura();
                        }
                    }
                    else
                    {
                        txtCodigoBarra.Select();
                    }
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }

        }
        private void CrearLineaCreditoVenta()
        {
            lineasCreditoVentaEntidad.FacturaID = facturaID;
            lineasCreditoVentaEntidad.Estatus = false;
            lineasCreditoVentasNegocio.InsertarLineaCreditoVenta(lineasCreditoVentaEntidad);
        }

        private void VerificarTipoFactura()
        {
            switch (cbTipoFactura.SelectedValue)
            {
                case 1:
                    CrearFacturaCFinal();                    
                    break;
                case 2:
                    CrearFacturaCFiscal();                                 
                    break;
                case 3:
                    CrearFacturaCGubernamental();
                    break;
                default:
                    break;
            }
            ImprimirFactura(facturaID);
            ActualizarCantProducto();
            MessageBox.Show("Venta realizada correctamente!", "Venta completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarFormulario();
        }

        private void ImprimirFactura(int facturaID)
        {
            hilo = new Thread(() =>
            {
                ImpresionFacturaVenta impresionFacturaVenta = new ImpresionFacturaVenta(facturaID);
                impresionFacturaVenta.Visible = false;
                impresionFacturaVenta.ImprimirDirecto();
            });
            hilo.Start();
        }
        private void CrearFacturaCGubernamental()
        {
            if (checkBoxClienteAnonimo.Checked == true)
            {
                facturaEntidad.ClienteID = 1;
            }
            else
            {
                facturaEntidad.ClienteID = Convert.ToInt32(cbClientes.SelectedValue);
            }

            NCF = Properties.Settings.Default.CGubernamental + 1;
            facturaEntidad.Fecha = DateTime.Now;
            facturaEntidad.TipoPagoID = Convert.ToInt32(cbTipoPago.SelectedValue);
            facturaEntidad.TipoFacturaID = Convert.ToInt32(cbTipoFactura.SelectedValue);
            facturaEntidad.NCF = "00000000B15" + NCF.ToString("D8");
            facturaEntidad.UserID = Login.userID;
            facturaEntidad.RNC = txtRNC.Text;
            facturaEntidad.Entidad = txtRazonSocial.Text;
            facturaEntidad.FechaVencimiento = Properties.Settings.Default.FechaVencimiento;
            facturaEntidad.DescuentoCliente = Convert.ToDecimal(txtDescuentoCliente.Text);
            
            if (Convert.ToInt32(cbCotizacion.SelectedValue) > 0 && cbCotizacion.Enabled == false)
            {
                facturaEntidad.CotizacionID = Convert.ToInt32(cbCotizacion.SelectedValue);
            }
            else
            {
                facturaEntidad.CotizacionID = null;
            }
            var result = facturasNegocio.InsertarFactura(facturaEntidad);
            if (result.Item1)
            {
                if (facturaEntidad.CotizacionID != null)
                {
                    cotizacionesNegocio.ActualizarEstatusCotizacion((int)facturaEntidad.CotizacionID);
                }
                Properties.Settings.Default.CGubernamental = NCF;
                Properties.Settings.Default.Save();
                facturaID = result.Item2;
                CrearDetalleFactura();
            }
        }
        private void CrearFacturaCFinal()
        {
            if (checkBoxClienteAnonimo.Checked == true)
            {
                facturaEntidad.ClienteID = 1;
            }
            else
            {
                facturaEntidad.ClienteID = Convert.ToInt32(cbClientes.SelectedValue);
            }
            NCF = Properties.Settings.Default.CFinal + 1;
            facturaEntidad.Fecha = DateTime.Now;
            facturaEntidad.TipoPagoID = Convert.ToInt32(cbTipoPago.SelectedValue);
            facturaEntidad.TipoFacturaID = Convert.ToInt32(cbTipoFactura.SelectedValue);
            facturaEntidad.NCF = "00000000B02" + NCF.ToString("D8");
            facturaEntidad.UserID = Login.userID;
            facturaEntidad.RNC = null;
            facturaEntidad.Entidad = null;
            facturaEntidad.FechaVencimiento = null;
            facturaEntidad.DescuentoCliente = Convert.ToDecimal(txtDescuentoCliente.Text);
           
            if (Convert.ToInt32(cbCotizacion.SelectedValue) > 0 && cbCotizacion.Enabled == false)
            {
                facturaEntidad.CotizacionID = Convert.ToInt32(cbCotizacion.SelectedValue);
            }
            else
            {
                facturaEntidad.CotizacionID = null;
            }            

            var result = facturasNegocio.InsertarFactura(facturaEntidad);
            if (result.Item1)
            {
                if (facturaEntidad.CotizacionID != null)
                {
                    cotizacionesNegocio.ActualizarEstatusCotizacion((int)facturaEntidad.CotizacionID);
                }
                Properties.Settings.Default.CFinal = NCF;
                Properties.Settings.Default.Save();
                facturaID = result.Item2;
                CrearDetalleFactura();
            }
        }

        private void CrearFacturaCFiscal()
        {
            if (checkBoxClienteAnonimo.Checked == true)
            {
                facturaEntidad.ClienteID = 1;
            }
            else
            {
                facturaEntidad.ClienteID = Convert.ToInt32(cbClientes.SelectedValue);
            }

            NCF = Properties.Settings.Default.CFiscal + 1;
            facturaEntidad.Fecha = DateTime.Now;
            facturaEntidad.TipoPagoID = Convert.ToInt32(cbTipoPago.SelectedValue);
            facturaEntidad.TipoFacturaID = Convert.ToInt32(cbTipoFactura.SelectedValue);
            facturaEntidad.NCF = "00000000B01" + NCF.ToString("D8");
            facturaEntidad.UserID = Login.userID;
            facturaEntidad.RNC = txtRNC.Text;
            facturaEntidad.Entidad = txtRazonSocial.Text;
            facturaEntidad.FechaVencimiento = Properties.Settings.Default.FechaVencimiento;
            facturaEntidad.DescuentoCliente = Convert.ToDecimal(txtDescuentoCliente.Text);

            if (Convert.ToInt32(cbCotizacion.SelectedValue) > 0 && cbCotizacion.Enabled == false)
            {
                facturaEntidad.CotizacionID = Convert.ToInt32(cbCotizacion.SelectedValue);
            }
            else
            {
                facturaEntidad.CotizacionID = null;
            }

            var result = facturasNegocio.InsertarFactura(facturaEntidad);
            if (result.Item1)
            {
                if (facturaEntidad.CotizacionID != null)
                {
                    cotizacionesNegocio.ActualizarEstatusCotizacion((int)facturaEntidad.CotizacionID);
                }
                Properties.Settings.Default.CFiscal = NCF;
                Properties.Settings.Default.Save();
                facturaID = result.Item2;
                CrearDetalleFactura();
            }
        }

        public void CrearDetalleFactura()
        {
            foreach (DataGridViewRow fila in dgvCarrito.Rows)
            {
                detalleFacturaEntidad.FacturaID = facturaID;
                detalleFacturaEntidad.ProductoID = Convert.ToInt32(fila.Cells["ProductoID"].Value);
                detalleFacturaEntidad.CantVen = Convert.ToDouble(fila.Cells["Cantidad"].Value);
                detalleFacturaEntidad.Precio = Convert.ToDecimal(fila.Cells["Precio"].Value);
                detalleFacturaEntidad.ITBIS = Convert.ToDecimal(fila.Cells["ITBIS"].Value);
                detalleFacturaEntidad.Descuento = Convert.ToDecimal(fila.Cells["Descuento"].Value);

                detalleFacturasNegocio.InsertarDetalleFactura(detalleFacturaEntidad);

            }
        }

        private void checkboxColocarAuto_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBoxColocarAut.Checked == true)
                {
                    this.AcceptButton = btnColocar;
                }
                else
                {
                    this.AcceptButton = null;
                }

            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }

        }

        private void BuscarDatosProducto()
        {
            if (string.IsNullOrEmpty(txtCodigoBarra.Text))
            {
                txtDescripcion.Clear();
                txtUnidadMedida.Clear();
                txtCantidad.Text = 1.ToString();
                txtPrecio.Clear();
            }
            else
            {  
                var result = productosNegocio.BuscarProductosPorCodigoBarra(txtCodigoBarra.Text).FirstOrDefault();
                if (result != null)
                {
                    productoCarritoEntidad.ProductoID = result.ProductoID;
                    productoCarritoEntidad.Servicio = result.Servicio;
                    productoCarritoEntidad.Descripcion = result.Descripcion;
                    productoCarritoEntidad.Existencia = result.Existencia;
                    productoCarritoEntidad.ProveedorID = result.ProveedorID;
                    productoCarritoEntidad.PrecioCompra = result.PrecioCompra;
                    productoCarritoEntidad.PrecioVenta = result.PrecioVenta;
                    productoCarritoEntidad.PrecioVentaMin = result.PrecioVentaMin;
                    productoCarritoEntidad.ITBIS = result.ITBIS;
                    productoCarritoEntidad.Descuento = result.Descuento;
                    productoCarritoEntidad.CantMin = result.CantMin;
                    productoCarritoEntidad.CantMax = result.CantMax;
                    productoCarritoEntidad.CodigoBarra = result.CodigoBarra;
                    productoCarritoEntidad.UnidadMedida = result.UnidadMedida;

                    txtDescripcion.Text = productoCarritoEntidad.Descripcion;
                    txtCantidad.Text = "1";
                    txtPrecio.Text = productoCarritoEntidad.PrecioVenta.ToString();
                    txtUnidadMedida.Text = productoCarritoEntidad.UnidadMedida.ToString();

                    if (productoCarritoEntidad.ITBIS)
                    {
                        itbisProd = Properties.Settings.Default.ITBIS / 100;
                    }
                    else
                    {
                        itbisProd = 0;
                    }

                    if (productoCarritoEntidad.Descuento > 0)
                    {
                        descuentoProd = (productoCarritoEntidad.Descuento) / 100;
                    }
                    else
                    {
                        descuentoProd = 0;
                    }
                }
                else
                {
                    txtDescripcion.Clear();
                    txtUnidadMedida.Clear();
                    txtCantidad.ResetText();
                    txtPrecio.Clear();
                }
            }

        }




    }
}