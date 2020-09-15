using System;
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
using CapaPresentacion.Impresiones;

namespace CapaPresentacion
{
    public partial class Cotizaciones : Form
    {
        CotizacionesNegocio cotizacionesNegocio = new CotizacionesNegocio();
        BindingList<proc_CargarTodasCotizaciones_Result> proc_CargarTodasCotizaciones_Results;
        bool resultado, finalLista;
        Thread hilo;
        int indicePagina, tamanoPagina;
        string filtro, columna;
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
                MessageBox.Show("Error: No se ha podido imprimir la cotizacion, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }

        }       
        private void CargarCotizaciones()
        {
            try
            {               
                dgvCotizaciones.AutoGenerateColumns = false;
                List<proc_CargarTodasCotizaciones_Result> lista = cotizacionesNegocio.CargarTodasCotizaciones(indicePagina, tamanoPagina, filtro, columna).ToList();
                if (lista.Count < tamanoPagina)
                {
                    finalLista = true;
                }
                foreach (var item in lista)
                {
                    proc_CargarTodasCotizaciones_Results.Add(item);
                }
                dgvCotizaciones.DataSource = proc_CargarTodasCotizaciones_Results;

                OrdenarColumnasDGV();

            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido cargar las cotizaciones, intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void OrdenarColumnasDGV()
        {
            dgvCotizaciones.Columns["CotizacionID"].DisplayIndex = 0;
            dgvCotizaciones.Columns["Cliente"].DisplayIndex = 1;
            dgvCotizaciones.Columns["DescuentoCliente"].DisplayIndex = 2;
            dgvCotizaciones.Columns["Fecha"].DisplayIndex = 3;
            dgvCotizaciones.Columns["Factura"].DisplayIndex = 3;
            dgvCotizaciones.Columns["Valor"].DisplayIndex = 4;
            dgvCotizaciones.Columns["Usuario"].DisplayIndex = 5;

            dgvCotizaciones.Columns["DescuentoCliente"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCotizaciones.Columns["Valor"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCotizaciones.Refresh();
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

                MessageBox.Show("Error: Cotizacion no puede ser eliminado, posiblemente esta cotizacion ya esta relacionado con algun cliente o factura.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }
        private void ValidarBorrarCotizacion(bool result)
        {
            if (result)
            {
                ResetearBusqueda();
                CargarCotizaciones();
                MessageBox.Show(string.Format("La cotizacion ha sido borrada correctamente en la base de datos."), "Cotizacion Borrada Correctamente!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("La Cotizacion no pudo ser eliminada, favor de verificar los requerimientros", "Ha Ocurrido un error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cotizaciones_Load(object sender, EventArgs e)
        {
            proc_CargarTodasCotizaciones_Results = new BindingList<proc_CargarTodasCotizaciones_Result>();
            indicePagina = 1;
            tamanoPagina = 50;
            CargarCotizaciones();
            CargarCBFiltro();
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
                MessageBox.Show("Error: No se ha podido exportar la cotizacion, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor.",
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
                MessageBox.Show("Ha ocurrido un error intentando ver el detalle de esta cotizacion, intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }
        private void CargarCBFiltro()
        {
            cbFiltro.Items.Add("ID");
            cbFiltro.Items.Add("Cliente");
            cbFiltro.Items.Add("Fecha");
            cbFiltro.Items.Add("Factura");
            cbFiltro.SelectedIndex = 0;
        }
        private void txtFiltro_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFiltro.Text))
            {
                txtFiltro.Text = "Escriba para filtrar...";
                txtFiltro.ForeColor = Color.Gray;
                ResetearBusqueda();
                CargarCotizaciones();
            }
        }

        private void txtFiltro_Enter(object sender, EventArgs e)
        {
            if (txtFiltro.Text == "Escriba para filtrar...")
            {
                txtFiltro.Text = "";
                txtFiltro.ForeColor = Color.Black;
            }
        }
            

        private void cbFiltro_Validating(object sender, CancelEventArgs e)
        {
            if (cbFiltro.SelectedIndex == -1 && cbFiltro.Items.Count > 0)
            {
                cbFiltro.Focus();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F1:
                    txtFiltro.Focus();
                    return true;                
                case Keys.F5:
                    btnRealizarBusqueda.PerformClick();
                    return true;               
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }


        private void btnRealizarBusqueda_Click(object sender, EventArgs e)
        {
            try
            {
                ResetearBusqueda();
                if (txtFiltro.Text != "Escriba para filtrar...")
                {                    
                    switch (cbFiltro.SelectedItem.ToString())
                    {
                        case "ID":
                            columna = "CotizacionID";
                            filtro = txtFiltro.Text;
                            CargarCotizaciones();
                            break;
                        case "Cliente":
                            columna = "Cliente";
                            filtro = txtFiltro.Text;
                            CargarCotizaciones();
                            break;
                        case "Fecha":
                            columna = "Fecha";
                            filtro = txtFiltro.Text;
                            CargarCotizaciones();
                            break;
                        case "Factura":
                            columna = "Factura";
                            filtro = txtFiltro.Text;
                            CargarCotizaciones();
                            break;                     

                        default:
                            break;
                    }
                }
                else
                {
                    CargarCotizaciones();
                }
                OrdenarColumnasDGV();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido realizar la busqueda, intente de nuevo por favor.",
                      "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void dgvCotizaciones_Scroll(object sender, ScrollEventArgs e)
        {
            if (!finalLista)
            {
                if ((e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement) && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
                {
                    int display = dgvCotizaciones.Rows.Count - dgvCotizaciones.DisplayedRowCount(false);
                    if (e.NewValue >= dgvCotizaciones.Rows.Count - GetDisplayedRowsCount())
                    {
                        indicePagina++;
                        CargarCotizaciones();
                    }
                }
            }
        }
      
        private int GetDisplayedRowsCount()
        {
            int count = dgvCotizaciones.Rows[dgvCotizaciones.FirstDisplayedScrollingRowIndex].Height;
            count = dgvCotizaciones.Height / count;
            return count;
        }

        private void ResetearBusqueda()
        {
            proc_CargarTodasCotizaciones_Results.Clear();
            finalLista = false;
            indicePagina = 1;
            filtro = null;
            columna = null;
        }

    }

}
