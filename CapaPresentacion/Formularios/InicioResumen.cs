﻿using CapaPresentacion;
using CapaDatos;
using CapaNegocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SFacturacion
{
    public partial class InicioResumen : Form
    {
        ProductosNegocio productosNegocio = new ProductosNegocio();
        SistemaResumenNegocio sistemaResumenNegocio = new SistemaResumenNegocio();
        List<proc_ResumenSistema_Result> proc_ResumenSistema_Results;
        List<proc_CargarProductosMasVendidos_Result> proc_CargarProductosMasVendidos_Results;
        BindingList<proc_CargarProductosExistBaja_Result> proc_CargarProductosExistBaja_Results;
        bool finalLista;
        int indicePagina, tamanoPagina;
        public InicioResumen()
        {
            InitializeComponent();
        }

        private void InicioResumen_Load(object sender, EventArgs e)
        {
            proc_CargarProductosExistBaja_Results = new BindingList<proc_CargarProductosExistBaja_Result>();
            indicePagina = 1;
            tamanoPagina = 50;
            InicicializarLabelSistemaResumen();
            CargarProductoExistenciaBajaDGV();
            CargarProductosMasVendidosChart();
        }

        private void CargarProductosMasVendidosChart()
        {
            proc_CargarProductosMasVendidos_Results = productosNegocio.CargarProductosMasVendidos().ToList();
            chartProductosMasVendidos.Series["Series1"].IsValueShownAsLabel = true;
            foreach (proc_CargarProductosMasVendidos_Result producto in proc_CargarProductosMasVendidos_Results)
            {
                chartProductosMasVendidos.Series["Series1"].Points.AddXY(producto.Descripcion, producto.CantidadVendida);
            }           

        }

        private void CargarProductoExistenciaBajaDGV()
        {
            try
            {
                dgvProductos.AutoGenerateColumns = false;
                List<proc_CargarProductosExistBaja_Result> lista = productosNegocio.CargarProductosExistBaja(indicePagina, tamanoPagina).ToList();
                if (lista.Count < tamanoPagina)
                {
                    finalLista = true;
                }
                foreach (var item in lista)
                {
                    proc_CargarProductosExistBaja_Results.Add(item);
                }
                dgvProductos.DataSource = proc_CargarProductosExistBaja_Results;

                OrdenarColumnasDGV();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido cargar todos los usuarios correctamente, intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }


        private void OrdenarColumnasDGV()
        {           
            dgvProductos.Columns["ProductoID"].DisplayIndex = 0;
            dgvProductos.Columns["Referencia"].DisplayIndex = 1;
            dgvProductos.Columns["CodigoBarra"].DisplayIndex = 2;
            dgvProductos.Columns["Descripcion"].DisplayIndex = 3;
            dgvProductos.Columns["Proveedor"].DisplayIndex = 4;
            dgvProductos.Columns["UnidadMedida"].DisplayIndex = 5;
            dgvProductos.Columns["Existencia"].DisplayIndex = 6;
            dgvProductos.Columns["CantMin"].DisplayIndex = 7;
            dgvProductos.Columns["CantMax"].DisplayIndex = 8;

            dgvProductos.Columns["Existencia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProductos.Columns["CantMin"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProductos.Columns["CantMax"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
        private void InicicializarLabelSistemaResumen()
        {
            try
            {
                proc_ResumenSistema_Results = sistemaResumenNegocio.ResumenSistema().ToList();
                labelClientes.Text = proc_ResumenSistema_Results.FirstOrDefault().Clientes.ToString();
                labelProveedores.Text = proc_ResumenSistema_Results.FirstOrDefault().Proveedores.ToString();
                labelProductos.Text = proc_ResumenSistema_Results.FirstOrDefault().Productos.ToString();
                labelVentas.Text = proc_ResumenSistema_Results.FirstOrDefault().Ventas.ToString();
                labelCompras.Text = proc_ResumenSistema_Results.FirstOrDefault().Compras.ToString();
                labelCotizaciones.Text = proc_ResumenSistema_Results.FirstOrDefault().Cotizaciones.ToString();
                labelNotasCredito.Text = proc_ResumenSistema_Results.FirstOrDefault().NotasCreditos.ToString();
                labelCXC.Text = proc_ResumenSistema_Results.FirstOrDefault().CXC.ToString();
                labelCXP.Text = proc_ResumenSistema_Results.FirstOrDefault().CXP.ToString();
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: El resumen del sistema no pudo ser cargado correctamente, intente de nuevo por favor." + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void dgvProductos_Scroll(object sender, ScrollEventArgs e)
        {
            if (!finalLista)
            {
                if ((e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement) && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
                {
                    int display = dgvProductos.Rows.Count - dgvProductos.DisplayedRowCount(false);
                    if (e.NewValue >= dgvProductos.Rows.Count - GetDisplayedRowsCount())
                    {
                        indicePagina++;
                        CargarProductoExistenciaBajaDGV();
                    }
                }
            }

        }
        private int GetDisplayedRowsCount()
        {
            int count = dgvProductos.Rows[dgvProductos.FirstDisplayedScrollingRowIndex].Height;
            count = dgvProductos.Height / count;
            return count;
        }
    }
}
