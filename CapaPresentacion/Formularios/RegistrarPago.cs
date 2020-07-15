using CapaDatos;
using CapaNegocios;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Formularios
{
    public partial class RegistrarPago : Form
    {        

        PagosComprasCredito pagosComprasCreditoEntidad = new PagosComprasCredito();
        PagosCompraCreditoNegocio pagosCompraCreditoNegocio = new PagosCompraCreditoNegocio();
        LineasCreditoComprasNegocio lineasCreditoComprasNegocio = new LineasCreditoComprasNegocio();
        CultureInfo ci = new CultureInfo("en-us");
        decimal montoCobro;
        int pagoCompraCreditoID;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        public RegistrarPago(int lineaCreditoCompraID, string proveedor, decimal montoPendiente)
        {
            InitializeComponent();
            txtLineaCreditoCompraID.Text = lineaCreditoCompraID.ToString();
            txtProveedor.Text = proveedor;
            txtBalancePendiente.Text = montoPendiente.ToString("C", ci);
        }

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRealizarPago_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarTxtPago())
                {
                    RealizarPago();
                }
               
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: No se ha podido registrar el pago, verifique el monto a pagar e intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }


        private bool ValidarTxtPago()
        {

            if(string.IsNullOrEmpty(txtPago.Text) || !decimal.TryParse(txtPago.Text, out montoCobro) || montoCobro > decimal.Parse(Regex.Replace(txtBalancePendiente.Text, @"[^\d.]", "")))
            {
                MessageBox.Show("Favor de digitar un monto valido a pagar | El valor a pagar no puede sobrepasar el balance pendiente de la factura.", "Valor ingresado incorrecto.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }   
        private void RealizarPago()
        {
            pagosComprasCreditoEntidad.LineaCreditoCompraID = Convert.ToInt32(txtLineaCreditoCompraID.Text);
            pagosComprasCreditoEntidad.FechaPago = DateTime.Now;
            pagosComprasCreditoEntidad.Monto = montoCobro;
            pagosComprasCreditoEntidad.UserID = Login.userID;

            if (decimal.Parse(Regex.Replace(txtBalancePendiente.Text, @"[^\d.]", "")) == montoCobro)
            {
                pagosComprasCreditoEntidad.Concepto = "Saldo";
                lineasCreditoComprasNegocio.ActualizarLineaCreditoCompra(Convert.ToInt32(txtLineaCreditoCompraID.Text), true);

            }
            else
            {
                pagosComprasCreditoEntidad.Concepto = "Abono";
            }

            var result = pagosCompraCreditoNegocio.InsertarPagoCompraCredito(pagosComprasCreditoEntidad);
            pagoCompraCreditoID = result.Item2;
            ValidarPago(result.Item1);
            
        }

        private void ValidarPago(bool resultado)
        {
            if (resultado)
            {
                MessageBox.Show(string.Format("Pago  de codigo #{0} fue realizado correctamente", pagoCompraCreditoID), "Pago Aplicado Correctamente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Pago no fue aplicado", "Ha ocurrido un error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }       
    }
}
