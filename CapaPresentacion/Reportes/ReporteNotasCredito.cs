﻿using CapaDatos;
using CapaNegocios;
using Microsoft.Reporting.WinForms;
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

namespace CapaPresentacion.Reportes
{
    public partial class ReporteNotasCredito : Form
    {
        NotasDeCreditoNegocio notasDeCreditoNegocio = new NotasDeCreditoNegocio();
        List<proc_CargarNotasDeCreditoPFecha_Result> proc_CargarNotasDeCreditoPFecha_Results;
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
        public ReporteNotasCredito(DateTime fechaInicial, DateTime fechaFinal)
        {
            InitializeComponent();
            CargarParametros();
            CargarVistaPreviaRV(fechaInicial, fechaFinal);
        }

        private void CargarParametros()
        {
            parameters[0] = new ReportParameter("RazonSocial", Properties.Settings.Default.RazonSocial);
            parameters[1] = new ReportParameter("CedulaORNC", Properties.Settings.Default.CedulaORnc);
            parameters[2] = new ReportParameter("DireccionEmpresa", Properties.Settings.Default.Direccion);
            parameters[3] = new ReportParameter("TelefonoEmpresa", Properties.Settings.Default.Telefono);
            parameters[4] = new ReportParameter("EmailEmpresa", Properties.Settings.Default.Email);
            parameters[5] = new ReportParameter("Logo", Properties.Settings.Default.Logo);
            parameters[6] = new ReportParameter("Itbis", Properties.Settings.Default.ITBIS.ToString());
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

        private void CargarVistaPreviaRV(DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                proc_CargarNotasDeCreditoPFecha_Results = notasDeCreditoNegocio.CargarNotasDeCreditoPFecha(fechaInicial, fechaFinal).ToList();
                var dataSource = new ReportDataSource("DataSetTodasNotasCredito", proc_CargarNotasDeCreditoPFecha_Results);
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                reportViewer1.LocalReport.ReportPath = @"C:/SFacturacion/reporteNotasCredito.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(dataSource);
                reportViewer1.ZoomMode = ZoomMode.PageWidth;
                reportViewer1.LocalReport.EnableExternalImages = true;
                reportViewer1.LocalReport.SetParameters(parameters);
                reportViewer1.RefreshReport();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.ToString(),
                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());

            }

        }
    }
}