﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
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
        private int cantidad;
        ReportParameter[] parameters = new ReportParameter[5];

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        public ImpresionEtiquetaProducto(string descripcionProd, string codigoBarra, int cantidad, string precioVenta, string referencia)
        {
            InitializeComponent();
            CargarParametros(descripcionProd, codigoBarra, precioVenta, referencia);
            this.cantidad = cantidad;
        }

        private void ImpresionEtiquetaProducto_Load(object sender, EventArgs e)
        {

        }

        public void ImprimirLabel()
        {
            for (int i = 0; i < cantidad; i++)
            {
                ControladorImpresoraPapelA4 controladorImpresoraPapelA4 = new ControladorImpresoraPapelA4();
                controladorImpresoraPapelA4.Imprime(CargarImpresionRV());
            }            
            this.Close();
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

                using (var b = new Bitmap(CodigoBarra.Encode(TYPE.CODE128, codigoBarra, Color.Black, Color.White)))
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
                MessageBox.Show("Error: " + exc.ToString(),
                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
                return null;
            }
        }

        private void CargarParametros(string descripcionProd, string codigoBarra, string precioVenta, string referencia)
        {
            try
            {
                parameters[0] = new ReportParameter("NombreEmpresa", Properties.Settings.Default.NombreEmpresa);
                parameters[1] = new ReportParameter("CodigoBarra", GenerarCodigoBarra(codigoBarra));
                parameters[2] = new ReportParameter("DescripcionProducto", descripcionProd);
                parameters[3] = new ReportParameter("PrecioVenta", precioVenta);
                parameters[4] = new ReportParameter("Referencia", referencia);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.ToString(),
                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
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
                MessageBox.Show("Error: " + exc.ToString(),
                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
                return null;
            }
        }       
    }
}
