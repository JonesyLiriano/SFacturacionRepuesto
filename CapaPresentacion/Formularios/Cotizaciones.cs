﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaDatos;
using CapaNegocios;
using CapaPresentacion.Formularios;
using CapaPresentacion.Reportes;

namespace CapaPresentacion
{
    public partial class Cotizaciones : Form
    {
        CotizacionesNegocio cotizacionesNegocio = new CotizacionesNegocio();
        List<proc_CargarTodasCotizaciones_Result> proc_CargarTodasCotizaciones_Results;
        bool resultado;
        Thread hilo;
        public Cotizaciones()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCotizaciones.SelectedRows.Count > 0)
                {
                    hilo = new Thread(() =>
                    {
                        ImpresionCotizacion impresionCotizacion = new ImpresionCotizacion(Convert.ToInt32(dgvCotizaciones.CurrentRow.Cells["CotizacionID"].Value));
                        impresionCotizacion.Visible = false;
                        impresionCotizacion.ImprimirDirecto();
                    });
                    hilo.Start();
                    
                }
                else
                {
                    MessageBox.Show("Debe de seleccionar al menos una cotizacion para imprimir", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.ToString(),
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }

        }       
        private void CargarCotizaciones()
        {
            try
            {
                proc_CargarTodasCotizaciones_Results = cotizacionesNegocio.CargarTodasCotizaciones().ToList();
                dgvCotizaciones.DataSource = proc_CargarTodasCotizaciones_Results;
                dgvCotizaciones.Columns["Valor"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvCotizaciones.Refresh();
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
                if (dgvCotizaciones.SelectedRows.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show(string.Format("Esta seguro que desea eliminar la cotizacion {0}?", dgvCotizaciones.CurrentRow.Cells["CotizacionID"].Value), "Eliminar Cotizacion", MessageBoxButtons.OKCancel);
                    if (dialogResult == DialogResult.OK)
                    {
                        resultado = cotizacionesNegocio.BorrarCotizacion(Convert.ToInt32(dgvCotizaciones.CurrentRow.Cells["CotizacionID"].Value));
                        CargarCotizaciones();
                        ValidarBorrarCotizacion(resultado);
                    }
                }
                else
                {
                    MessageBox.Show("Debe de seleccionar al menos una cotizacion para eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }
        private void ValidarBorrarCotizacion(bool result)
        {
            if (result)
            {
                MessageBox.Show(string.Format("La cotizacion ha sido borrada correctamente en la base de datos."), "Cotizacion Borrada Correctamente!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("La Cotizacion no pudo ser eliminada, favor de verificar los requerimientros", "Ha Ocurrido un error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cotizaciones_Load(object sender, EventArgs e)
        {
            CargarCotizaciones();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCotizaciones.SelectedRows.Count > 0)
                {
                        ImpresionCotizacion impresionCotizacion = new ImpresionCotizacion(Convert.ToInt32(dgvCotizaciones.CurrentRow.Cells["CotizacionID"].Value));
                        impresionCotizacion.ImprimirConVistaPrevia();
                        impresionCotizacion.Show();
                }
                else
                {
                    MessageBox.Show("Debe de seleccionar al menos una cotizacion para imprimir", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.ToString(),
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void btnVerDetalles_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCotizaciones.SelectedRows.Count > 0)
                {
                    DetalleFacturaCotizacion detalleFacturaCotizacion 
                        = new DetalleFacturaCotizacion("Cotizacion", Convert.ToInt32(dgvCotizaciones.CurrentRow.Cells["CotizacionID"].Value));
                    detalleFacturaCotizacion.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Debe de seleccionar al menos una cotizacion para ver detalle", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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