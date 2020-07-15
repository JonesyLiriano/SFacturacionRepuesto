using CapaDatos;
using CapaNegocios;
using CapaPresentacion.Impresiones;
using SFacturacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Formularios
{
    public partial class RegistrarNotaCredito : Form
    {
        Thread hilo;
        Producto productoEntidad = new Producto();
        ProductosNegocio productosNegocio = new ProductosNegocio();
        ClientesNegocio clientesNegocio = new ClientesNegocio();
        FacturasNegocio facturasNegocio = new FacturasNegocio();
        NotasDeCreditoNegocio notasDeCreditoNegocio = new NotasDeCreditoNegocio();
        NotasDeCredito notasDeCreditoEntidad = new NotasDeCredito();
        DetalleNotaDeCredito detalleNotaDeCreditoEntidad = new DetalleNotaDeCredito();
        DetalleNotasDeCreditoNegocio detalleNotasDeCreditoNegocio = new DetalleNotasDeCreditoNegocio();
        CobrosVentaCreditoNegocio cobrosVentaCreditoNegocio = new CobrosVentaCreditoNegocio();
        CobrosVentasCredito cobrosVentasCreditoEntidad = new CobrosVentasCredito();
        LineasCreditoVentasNegocio lineasCreditoVentasNegocio = new LineasCreditoVentasNegocio();
        List<proc_CargarFacturasPendientes_Result> proc_CargarFacturasPendientes_Results;
        List<proc_CargarTodasFacturas_Result> proc_CargarTodasFacturas_Results;
        CultureInfo ci = new CultureInfo("en-us");
        private decimal valorFacturaTotal, valorFacturaTotalSinITBIS;
        private decimal? descuentoCliente;
        private int notaCreditoID, lineaCreditoVentaID;
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        public RegistrarNotaCredito()
        {
            InitializeComponent();
        }

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RegistrarNotaCredito_Load(object sender, EventArgs e)
        {
            CargarCBClientes();
            CargarCBFacturas();
        }
        private void CargarCBClientes()
        {
            cbClientes.DataSource = null;
            cbClientes.DisplayMember = "Nombre";
            cbClientes.ValueMember = "ClienteID";
            cbClientes.DataSource = clientesNegocio.CargarTodosClientes();
            cbClientes.SelectedIndex = -1;

        }
        private void CargarCBFacturas()
        {
            cbFacturas.DataSource = null;
            cbFacturas.DisplayMember = "FacturaID";
            cbFacturas.ValueMember = "FacturaID";
            proc_CargarTodasFacturas_Results = facturasNegocio.CargarTodasFacturas().ToList();
            cbFacturas.DataSource = proc_CargarTodasFacturas_Results;
            cbFacturas.SelectedIndex = -1;

        }
        private void CargarCBFacturas(int clienteID)
        {
            cbFacturas.DataSource = null;
            cbFacturas.DisplayMember = "FacturaID";
            cbFacturas.ValueMember = "FacturaID";
            proc_CargarTodasFacturas_Results = facturasNegocio.CargarTodasFacturas()
                .Where(f => f.ClienteID == clienteID).ToList();
            cbFacturas.DataSource = proc_CargarTodasFacturas_Results;
            cbFacturas.SelectedIndex = -1;

        }
        private void CargarCBFacturasAAplicar(int clienteID)
        {
            cbFacturasAAplicar.DataSource = null;
            cbFacturasAAplicar.DisplayMember = "FacturaID";
            cbFacturasAAplicar.ValueMember = "FacturaID";
            proc_CargarFacturasPendientes_Results = facturasNegocio.CargarFacturasPendientes(clienteID).ToList();
            cbFacturasAAplicar.DataSource = proc_CargarFacturasPendientes_Results;
            cbFacturasAAplicar.SelectedIndex = -1;

        }

        private void cbClientes_Validating(object sender, CancelEventArgs e)
        {
            if (cbClientes.SelectedIndex == -1)
            {
                cbClientes.Focus();
            }
        }

        private void cbFacturas_Validating(object sender, CancelEventArgs e)
        {
            if (cbFacturas.SelectedIndex == -1 && cbFacturas.Items.Count > 0)
            {
                cbFacturas.Focus();
            }

        }

        private void cbFacturasAAplicar_Validating(object sender, CancelEventArgs e)
        {
            if (cbFacturasAAplicar.SelectedIndex == -1 && cbFacturasAAplicar.Items.Count > 0)
            {
                cbFacturasAAplicar.Focus();
            }
        }      

        private void LimpiarTxtFacturaAAplicar()
        {
            txtBalancePendiente.Clear();
            txtMontoFactura.Clear();
        }
       
      
         private void CargarTxtFacturaAAplicar()
        {
            try
            {
               var result = facturasNegocio.CargarMontoFacturaNC(Convert.ToInt32(cbFacturasAAplicar.SelectedValue)).FirstOrDefault();
               if (result != null)
               {
                txtMontoFactura.Text = result.ValorFactura?.ToString("C", ci);
                txtBalancePendiente.Text = result.ValorPendiente?.ToString("C", ci);
               }
               else
               {
                    LimpiarTxtFacturaAAplicar();
               }                

            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido calcular el total de la nota de credito, favor de intentar nuevamente.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void ImprimirComprobanteNotaDeCredito()
        {
            try
            {
              hilo = new Thread(() =>
                {
                    ImpresionNotaCredito impresionCotizacion = new ImpresionNotaCredito(notaCreditoID);
                   impresionCotizacion.Visible = false;
                   impresionCotizacion.ImprimirDirecto();
                });
              hilo.Start();
               
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido imprimir la nota de credito, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }
        private bool CrearNotaDeCredito()
        {

            notasDeCreditoEntidad.FacturaID = Convert.ToInt32(cbFacturas.SelectedValue);
            notasDeCreditoEntidad.FacturaAplicarID = Convert.ToInt32(cbFacturasAAplicar.SelectedValue);
            notasDeCreditoEntidad.Fecha = System.DateTime.Now;
            notasDeCreditoEntidad.UserID = Login.userID;            
            notasDeCreditoEntidad.NCF = "B04" + (Properties.Settings.Default.NCredito + 1).ToString("D8");
            notasDeCreditoEntidad.FechaVencimiento = Properties.Settings.Default.FechaVencimiento;
            notasDeCreditoEntidad.ValorAplicado = Convert.ToDecimal(txtValorAplicarNotaCredito.Text);
            notasDeCreditoEntidad.ITBIS = checkBoxITBIS.Checked;

            var result = notasDeCreditoNegocio.AgregarNotaDeCredito(notasDeCreditoEntidad);

            if (result.Item1)
            {
                notaCreditoID = result.Item2;
                MessageBox.Show(string.Format("La nota de credito {0} fue creada exitosamente...", notaCreditoID), "Nota De Credito Creada Correctamente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CrearDetalleNotaDeCredito();
                Properties.Settings.Default.NCredito = (Properties.Settings.Default.NCredito + 1);
                Properties.Settings.Default.Save();
                return true;
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error en la crecion de la nota de credito...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }       
        private void CrearDetalleNotaDeCredito()
        {
            try
            {
                foreach (DataRow dtRow in ProductosADevolver.dtProductosRecibidos.Rows)
                {
                    detalleNotaDeCreditoEntidad.NotaDeCreditoID = notaCreditoID;
                    detalleNotaDeCreditoEntidad.ProductoID = Convert.ToInt32(dtRow["ProductoID"]);
                    detalleNotaDeCreditoEntidad.Precio = Convert.ToDecimal(dtRow["Precio"]);
                    detalleNotaDeCreditoEntidad.CantDevuelta = Convert.ToDouble(dtRow["Recibida"]);
                    detalleNotaDeCreditoEntidad.CantInventariada = Convert.ToDouble(dtRow["Inventariada"]);
                    detalleNotaDeCreditoEntidad.Comentario = Convert.ToString(dtRow["Comentario"]);

                    detalleNotasDeCreditoNegocio.AgregarDetalleNotaDeCredito(detalleNotaDeCreditoEntidad);
                    if(Convert.ToDouble(dtRow["Inventariada"]) > 0)
                    {
                        productoEntidad.ProductoID = Convert.ToInt32(dtRow["ProductoID"]);
                        productoEntidad.Existencia = Convert.ToDouble(dtRow["Inventariada"]);
                        productosNegocio.ActualizarCantidadProductoPorID(productoEntidad);
                    }                    
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: No se ha podido crear el detalle de la nota de credito, intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show(string.Format("Esta seguro que desea registrar esta nota de credito??"), "Registrar Nota de Credito", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    decimal valorAplicar;
                    if (ProductosADevolver.dtProductosRecibidos.Rows.Count > 0
                        && decimal.TryParse(txtValorAplicarNotaCredito.Text, out valorAplicar) && valorAplicar > 0 
                        && !(decimal.Parse(Regex.Replace(txtBalancePendiente.Text, @"[^\d.]", "")) < valorAplicar))
                    {
                        if (CrearNotaDeCredito())
                        {
                            AplicarNotaDeCredito(valorAplicar);
                            this.Close();
                        }                      
                    }
                    else
                    {
                        MessageBox.Show("Debe de haber productos recibidos para procesar la nota de credito | El valor a aplicar para la nota  de credito debe ser valido y mayor que 0 | El valor a aplicar no puede sobrepasar el valor pendiente de la factura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: No se ha podido guardar esta nota de credito, favor de verificar los campos e intentar nuevamente.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void AplicarNotaDeCredito(decimal valorAplicar)
        {
            try
            {
                lineaCreditoVentaID = lineasCreditoVentasNegocio.BuscarLineaDeCreditoVentaIDFactura(Convert.ToInt32(cbFacturasAAplicar.SelectedValue));
                cobrosVentasCreditoEntidad.LineaCreditoVentaID = lineaCreditoVentaID;
                cobrosVentasCreditoEntidad.FechaCobro = DateTime.Now;
                cobrosVentasCreditoEntidad.Monto = valorAplicar;
                cobrosVentasCreditoEntidad.UserID = Login.userID;
                cobrosVentasCreditoEntidad.Concepto = "Nota de credito";

                var result = cobrosVentaCreditoNegocio.InsertarCobroVentaCredito(cobrosVentasCreditoEntidad);

                if (result.Item1)
                {
                    if (decimal.Parse(Regex.Replace(txtBalancePendiente.Text, @"[^\d.]", "")) == valorAplicar)
                    {
                        lineasCreditoVentasNegocio.ActualizarLineaCreditoVenta(lineaCreditoVentaID, true);

                    }
                    ImprimirComprobanteNotaDeCredito();
                }
                else
                {
                    MessageBox.Show("Nota de Credito no pudo ser aplicada...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: La nota de credito no ha podido ser aplicada, verificar e intente de nuevo por favor",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void btnSeleccionarProd_Click(object sender, EventArgs e)
        {
            if (cbFacturas.SelectedIndex != -1)
            {
                ProductosADevolver productosADevolver = new ProductosADevolver(Convert.ToInt32(cbFacturas.SelectedValue));
                productosADevolver.ShowDialog();
                CargarDatosProductosRecibidos();
            } else
            {
                MessageBox.Show("Debe de seleccionar una factura valida para recibir los productos", "Factura no valida", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            ProductosADevolver.dtProductosRecibidos = null;
            cbFacturasAAplicar.SelectedIndex = -1;
            cbFacturasAAplicar.Enabled = true;
            cbFacturas.SelectedIndex = -1;
            cbFacturas.Enabled = true;
            LimpiarTxtFacturaAAplicar();
            txtCantProdDevueltos.Clear();
            txtFechaFactura.Clear();
            txtValorAplicarNotaCredito.Clear();
            txtValorNotaCredito.Clear();
            cbClientes.SelectedIndex = -1;
            cbClientes.Enabled = true;
        }

        private void checkBoxITBIS_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxITBIS.Checked)
            {
                txtValorAplicarNotaCredito.Text = valorFacturaTotal.ToString("F");
            }
            else
            {
                txtValorAplicarNotaCredito.Text = valorFacturaTotalSinITBIS.ToString("F");
            }
        }

        private void cbClientes_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbClientes.SelectedIndex != -1)
            {
                CargarCBFacturas(Convert.ToInt32(cbClientes.SelectedValue));
                CargarCBFacturasAAplicar(Convert.ToInt32(cbClientes.SelectedValue));

            }
            else
            {
                cbFacturasAAplicar.DataSource = null;
                CargarCBFacturas();
            }
            LimpiarTxtFacturaAAplicar();
        }

        private void cbFacturas_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbFacturas.SelectedIndex != -1)
            {
                cbClientes.SelectedValue = proc_CargarTodasFacturas_Results.Where(r => r.FacturaID == Convert.ToInt32(cbFacturas.SelectedValue))
                .FirstOrDefault().ClienteID;
                txtFechaFactura.Text = proc_CargarTodasFacturas_Results.Where(r => r.FacturaID == Convert.ToInt32(cbFacturas.SelectedValue))
                .FirstOrDefault().Fecha.ToString();
                descuentoCliente = proc_CargarTodasFacturas_Results.Where(r => r.FacturaID == Convert.ToInt32(cbFacturas.SelectedValue))
                .FirstOrDefault().DescuentoCliente;

                // Diferencia de días
                if ((12 * (Convert.ToDateTime(txtFechaFactura.Text).Year - DateTime.Today.Date.Year) + Convert.ToDateTime(txtFechaFactura.Text).Month - DateTime.Today.Date.Month) < 1)
                {
                    checkBoxITBIS.Checked = true;
                }
                else
                {
                    checkBoxITBIS.Checked = false;
                }
            }
            else
            {
                txtFechaFactura.Clear();
            }
        }

        private void RegistrarNotaCredito_FormClosing(object sender, FormClosingEventArgs e)
        {
            ProductosADevolver.dtProductosRecibidos = null;
        }

        private void cbFacturasAAplicar_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbFacturasAAplicar.SelectedIndex != -1)
            {
                CargarTxtFacturaAAplicar();
            }
        }

        private void CargarDatosProductosRecibidos()
        {
            try
            {
                valorFacturaTotal = 0;
                valorFacturaTotalSinITBIS = 0;
                if (ProductosADevolver.dtProductosRecibidos.Rows.Count > 0)
                {
                    cbFacturas.Enabled = false;
                    cbClientes.Enabled = false;
                    if (cbFacturasAAplicar.SelectedIndex != -1)
                    {
                        cbFacturasAAplicar.Enabled = false;
                    }
                    else
                    {
                        cbFacturasAAplicar.Enabled = true;
                    }
                       
                    txtCantProdDevueltos.Text = ProductosADevolver.dtProductosRecibidos.Rows.Count.ToString();

                    foreach (DataRow dtRow in ProductosADevolver.dtProductosRecibidos.Rows)
                    {
                        valorFacturaTotal += Convert.ToDecimal(dtRow["Recibida"]) * Convert.ToDecimal(dtRow["Precio"]);
                            valorFacturaTotalSinITBIS += Convert.ToDecimal(dtRow["Recibida"]) * Convert.ToDecimal(dtRow["PrecioSinITBIS"]);
                    }
                    txtValorNotaCredito.Text = (valorFacturaTotal - (valorFacturaTotal * (descuentoCliente / 100)))?.ToString("C", ci);

                    if (checkBoxITBIS.Checked)
                    {
                        txtValorAplicarNotaCredito.Text = valorFacturaTotal.ToString("F");
                    }
                    else
                    {
                        txtValorAplicarNotaCredito.Text = valorFacturaTotalSinITBIS.ToString("F");
                    }     
                    txtValorAplicarNotaCredito.Select();
                }
                else
                {
                    MessageBox.Show("No se recibieron productos para poder procesar la nota de credito", "No hay productos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
               
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: Se intento cargar los productos recibidos pero ha ocurrido un error, intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }
    }
}
