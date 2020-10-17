using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BarcodeLib;
using CapaPresentacion.Clases;
using Microsoft.Reporting.WinForms;

namespace CapaPresentacion.Impresiones
{
    public partial class ImpresionEtiquetaProducto : Form
    {
        ReportParameter[] parameters = new ReportParameter[7];

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        public ImpresionEtiquetaProducto()
        {
            InitializeComponent();       
        }

        private void ImpresionEtiquetaProducto_Load(object sender, EventArgs e)
        {

        }       

        private void iconminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void iconrestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            iconrestaurar.Visible = false;
            iconmaximizar.Visible = true;
        }

        private void iconmaximizar_Click(object sender, EventArgs e)
        {
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;
            iconrestaurar.Visible = true;
            iconmaximizar.Visible = false;
        }

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string GenerarCodigoBarra(string codigoBarra)
        {
            try
            {
                Barcode CodigoBarra = new Barcode();
                CodigoBarra.IncludeLabel = true;
                CodigoBarra.BarWidth = 1;
                CodigoBarra.Width = 150;
                CodigoBarra.Height = 49;

                using (var b = new Bitmap(CodigoBarra.Encode(TYPE.CODE128, codigoBarra)))
                {
                    using (var ms = new MemoryStream())
                    {
                        b.Save(ms, ImageFormat.Jpeg);
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido imprimir, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor." + exc.ToString(),
                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
                return null;
            }
        }
        public string PadBoth(string source, int length)
        {
            int spaces = length - source.Length;
            int padLeft = spaces / 2 + source.Length;
            return source.PadLeft(padLeft).PadRight(length);

        }
        public void ImprimirLabel(string descripcionProd, string codigoBarra, int cantidad, string precioVenta, string referencia,
            string precioCompra)
        {
            try
            {              
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine();
                    sb.AppendLine("N");
                    sb.AppendLine("q304");
                    sb.AppendLine("Q203");
                    sb.AppendLine("D7");
                    //AStartPoint, VerticalStartPoint, Rotation, Font Selection, Horizontal Multiplier (expands Text Horizontally), Vertical Multiplier(expands Text Vertically), Reverse Image, Data
                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A15,10,0,3,1,1,N,\"{0}\"", PadBoth(Properties.Settings.Default.NombreEmpresa, 20)));
                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A15,35,0,2,1,1,N,\"{0}\"", PadBoth(Properties.Settings.Default.TelefonoEtiqueta, 24)));
                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A15,60,0,2,1,1,N,\"{0}\"", descripcionProd.Length < 24 ? descripcionProd : descripcionProd.Substring(0, 24)));
                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A15,80,0,2,1,1,N,\"{0}\"", referencia.Length < 24 ? referencia : referencia.Substring(0, 24)));
                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "A15,105,0,4,1,1,N,\"{0}\"", ConvertirPrecioNumeroALetras(precioCompra) + " / " + ConvertirPrecioNumeroALetras(precioVenta)));
                    //BStartPoint, VerticalStartPoint, Rotation, BarSelection(Code 39), Barcode Width, Wide Barcode Width, Barcode Height, Print Human Readable (B=Yes), Data
                    sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "B15,130,0,1,2,2,50,B,\"{0}\"", codigoBarra));
                    sb.AppendLine($"P{cantidad}");
                   
                    RawPrinterHelper.SendStringToPrinter(Properties.Settings.Default.ImpresoraTermica, sb.ToString());                
                    this.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido imprimir, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor.",
                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }

        }

        private string ConvertirPrecioNumeroALetras(string precio)
        {
            string precioLetras = "";
            for (int i = 0; i < precio.Length; i++)
            {                              
                 precioLetras = String.Concat(precioLetras, BaseNumeroALetra(precio.Substring(i, 1)));
            }
            return precioLetras;
        }

        private string BaseNumeroALetra(string numero)
        {
            string palabra = Properties.Settings.Default.CodigoLetras;
            switch (numero)
            {
                case "0":
                    return palabra.Substring(9,1);
                case "1":
                    return palabra.Substring(0, 1);
                case "2":
                    return palabra.Substring(1, 1);
                case "3":
                    return palabra.Substring(2, 1);
                case "4":
                    return palabra.Substring(3, 1);
                case "5":
                    return palabra.Substring(4, 1);
                case "6":
                    return palabra.Substring(5, 1);
                case "7":
                    return palabra.Substring(6, 1);
                case "8":
                    return palabra.Substring(7, 1);
                case "9":
                    return palabra.Substring(8, 1);
                default:
                    return "";
            }
        }
        private LocalReport CargarImpresionRV()
        {
            try
            {
                LocalReport rdlc = new LocalReport();
                rdlc.ReportPath = @"C:/SFacturacion/impresionEtiquetaProducto.rdlc";
                rdlc.DataSources.Clear();
                rdlc.EnableExternalImages = true;
                rdlc.SetParameters(parameters);
                return rdlc;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido imprimir, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor.",
                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
                return null;
            }
        }       
    }
}
