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
    public partial class RegistrarCobro : Form
    {
        Thread hilo;
        CobrosVentasCredito cobrosVentasCreditoEntidad = new CobrosVentasCredito();
        CobrosVentaCreditoNegocio cobrosVentaCreditoNegocio = new CobrosVentaCreditoNegocio();
        LineasCreditoVentasNegocio lineasCreditoVentasNegocio = new LineasCreditoVentasNegocio();
        CultureInfo ci = new CultureInfo("en-us");
        decimal montoCobro;
        int cobroVentaCreditoID;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        public RegistrarCobro(int lineaCreditoVentaID, string cliente, decimal montoPendiente)
        {
            InitializeComponent();
            txtLineaCreditoVentaID.Text = lineaCreditoVentaID.ToString();
            txtCliente.Text = cliente;
            txtBalancePendiente.Text = montoPendiente.ToString("C", ci);
        }

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRealizarCobro_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarTxtCobro())
                {
                    RealizarCobro();
                }
               
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: No se ha podido registrar el cobro, verifique el monto a cobrar e intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }


        private bool ValidarTxtCobro()
        {

            if(string.IsNullOrEmpty(txtCobro.Text) || !decimal.TryParse(txtCobro.Text, out montoCobro) || montoCobro > decimal.Parse(Regex.Replace(txtBalancePendiente.Text, @"[^\d.]", "")))
            {
                MessageBox.Show("Favor de digitar un monto valido a cobrar | El valor a cobrar no puede sobrepasar el balance pendiente.", "Valor ingresado incorrecto.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }   
        private void RealizarCobro()
        {
            cobrosVentasCreditoEntidad.LineaCreditoVentaID = Convert.ToInt32(txtLineaCreditoVentaID.Text);
            cobrosVentasCreditoEntidad.FechaCobro = DateTime.Now;
            cobrosVentasCreditoEntidad.Monto = montoCobro;
            cobrosVentasCreditoEntidad.UserID = Login.userID;

            if (decimal.Parse(Regex.Replace(txtBalancePendiente.Text, @"[^\d.]", "")) == montoCobro)
            {
                cobrosVentasCreditoEntidad.Concepto = "Saldo";
                lineasCreditoVentasNegocio.ActualizarLineaCreditoVenta(Convert.ToInt32(txtLineaCreditoVentaID.Text), true);

            }
            else
            {
                cobrosVentasCreditoEntidad.Concepto = "Abono";
            }

            var result = cobrosVentaCreditoNegocio.InsertarCobroVentaCredito(cobrosVentasCreditoEntidad);
            cobroVentaCreditoID = result.Item2;
            ValidarCobro(result.Item1);
            
        }

        private void ValidarCobro(bool resultado)
        {
            if (resultado)
            {
                ImprimirComprobanteCobro();
                MessageBox.Show(string.Format("Cobro  de codigo #{0} fue realizado correctamente", cobroVentaCreditoID), "Cobro Aplicado Correctamente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Cobro no fue aplicado", "Ha ocurrido un error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImprimirComprobanteCobro()
        {
            hilo = new Thread(() => 
            {
                ImpresionReciboCobro impresionReciboCobro = new ImpresionReciboCobro(cobroVentaCreditoID);
                impresionReciboCobro.Visible = false;
                impresionReciboCobro.ImprimirDirecto();
            });             
            hilo.Start();
        }
    }
}
