using CapaDatos;
using CapaNegocios;
using CapaPresentacion;
using CapaPresentacion.Formularios;
using CapaPresentacion.Impresiones;
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

namespace SFacturacion
{
    
    public partial class Productos : Form
    {
        ProductosNegocio productosNegocio = new ProductosNegocio();
        List<proc_CargarTodosProductos_Result> proc_CargarTodosProductos_Results;
        Producto productoEntidad = new Producto();
        bool resultado;

        public Productos()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            try
            {
                CargarProductos();
                CargarCBFiltro();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido cargar los productos, intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private Producto CargarParametrosProducto()
        {
            productoEntidad.ProductoID = Convert.ToInt32(dgvProductos.CurrentRow.Cells["ProductoID"].Value);
            productoEntidad.Servicio = Convert.ToBoolean(dgvProductos.CurrentRow.Cells["Servicio"].Value.ToString());
            productoEntidad.Descripcion = dgvProductos.CurrentRow.Cells["Descripcion"].Value.ToString();
            productoEntidad.ProveedorID = dgvProductos.CurrentRow.Cells["ProveedorID"].Value == null ? -1 : (int?)Convert.ToInt32(dgvProductos.CurrentRow.Cells["ProveedorID"].Value.ToString());
            productoEntidad.Existencia = dgvProductos.CurrentRow.Cells["Existencia"].Value  == null ? null : (double?)Convert.ToDouble(dgvProductos.CurrentRow.Cells["Existencia"].Value.ToString());
            productoEntidad.ITBIS = Convert.ToBoolean(dgvProductos.CurrentRow.Cells["ITBIS"].Value.ToString());
            productoEntidad.PrecioCompra = dgvProductos.CurrentRow.Cells["PrecioCompra"].Value == null ? null : (decimal?)Convert.ToDecimal(dgvProductos.CurrentRow.Cells["PrecioCompra"].Value.ToString());
            productoEntidad.PrecioVenta = Convert.ToDecimal(dgvProductos.CurrentRow.Cells["PrecioVenta"].Value.ToString());
            productoEntidad.PrecioVentaMin = Convert.ToDecimal(dgvProductos.CurrentRow.Cells["PrecioVentaMin"].Value.ToString());            
            productoEntidad.Descuento = Convert.ToDecimal(dgvProductos.CurrentRow.Cells["Descuento"].Value.ToString());
            productoEntidad.CantMin = dgvProductos.CurrentRow.Cells["CantMin"].Value == null ? null : (double?)Convert.ToDouble(dgvProductos.CurrentRow.Cells["CantMin"].Value.ToString());
            productoEntidad.CantMax = dgvProductos.CurrentRow.Cells["CantMax"].Value == null ? null : (double?)Convert.ToDouble(dgvProductos.CurrentRow.Cells["CantMax"].Value.ToString());
            productoEntidad.CodigoBarra = dgvProductos.CurrentRow.Cells["CodigoBarra"].Value.ToString();
            productoEntidad.UnidadMedida = dgvProductos.CurrentRow.Cells["UnidadMedida"].Value.ToString();
            productoEntidad.Referencia = dgvProductos.CurrentRow.Cells["Referencia"].Value.ToString();
            productoEntidad.Marca = dgvProductos.CurrentRow.Cells["Marca"].Value.ToString();
            productoEntidad.Calidad = dgvProductos.CurrentRow.Cells["Calidad"].Value.ToString();

            return productoEntidad;
        }

        private void CargarProductos()
        {
            try
            {
                dgvProductos.AutoGenerateColumns = false;
                proc_CargarTodosProductos_Results = productosNegocio.CargarTodosProductos().ToList();
                dgvProductos.DataSource = proc_CargarTodosProductos_Results;
                OrdenarColumnasDGV();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido cargar los productos correctamente, intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void OrdenarColumnasDGV()
        {
            dgvProductos.Columns["ProductoID"].DisplayIndex = 0;
            dgvProductos.Columns["Servicio"].DisplayIndex = 1;
            dgvProductos.Columns["CodigoBarra"].DisplayIndex = 2;
            dgvProductos.Columns["Referencia"].DisplayIndex = 3;
            dgvProductos.Columns["Descripcion"].DisplayIndex = 4;
            dgvProductos.Columns["Marca"].DisplayIndex = 5;
            dgvProductos.Columns["Calidad"].DisplayIndex = 6;            
            dgvProductos.Columns["UnidadMedida"].DisplayIndex = 7;            
            dgvProductos.Columns["Existencia"].DisplayIndex = 8;
            dgvProductos.Columns["PrecioVenta"].DisplayIndex = 9;
            dgvProductos.Columns["PrecioVentaMin"].DisplayIndex = 10;
            dgvProductos.Columns["PrecioCompra"].DisplayIndex = 11; 
            dgvProductos.Columns["ITBIS"].DisplayIndex = 12;
            dgvProductos.Columns["Descuento"].DisplayIndex = 13;
            dgvProductos.Columns["CantMin"].DisplayIndex = 14;
            dgvProductos.Columns["CantMax"].DisplayIndex = 15;
            dgvProductos.Columns["Proveedor"].DisplayIndex = 16;
            dgvProductos.Columns["ProveedorID"].DisplayIndex = 17;

            dgvProductos.Columns["Existencia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProductos.Columns["PrecioCompra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProductos.Columns["PrecioVenta"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProductos.Columns["PrecioVentaMin"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProductos.Columns["Descuento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProductos.Columns["CantMin"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProductos.Columns["CantMax"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProductos.Refresh();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProductos.SelectedRows.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show(string.Format("Esta seguro que desea eliminar el producto/servicio {0}?", dgvProductos.CurrentRow.Cells["Descripcion"].Value), "Eliminar Producto/Servicio", MessageBoxButtons.OKCancel);
                    if (dialogResult == DialogResult.OK)
                    {
                        resultado = productosNegocio.BorrarProducto(Convert.ToInt32(dgvProductos.CurrentRow.Cells["ProductoID"].Value));
                        ValidarBorrarProducto(resultado);
                    }
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: Producto no puede ser eliminado, posiblemente ya este relacionado con alguna factura, cotizacion, nota de credito u orden de compra.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }
        private void ValidarBorrarProducto(bool result)
        {
            if (result)
            {
                MessageBox.Show(string.Format("El producto/servicio ha sido borrado correctamente en la base de datos."), "Producto/Servicio Borrado Correctamente!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("El producto/servicio no pudo ser borrado, favor de verificar los requerimientros", "Ha Ocurrido un error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            MantenimientoProducto frmMantProducto = new MantenimientoProducto();
            frmMantProducto.Controls["btnAplicar"].Text = "Agregar";
            frmMantProducto.ShowDialog();
            CargarProductos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                MantenimientoProducto frmMantProducto = new MantenimientoProducto(CargarParametrosProducto());
                frmMantProducto.Controls["btnAplicar"].Text = "Editar";
                frmMantProducto.ShowDialog();
                CargarProductos();
            }
            else
            {
                MessageBox.Show("Debe de seleccionar al menos un producto para editar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
           if (dgvProductos.SelectedRows.Count > 0)
            {
                RegistrarVenta.codigoBarraProd = dgvProductos.CurrentRow.Cells["CodigoBarra"].Value.ToString();
                this.Close();
            }
        }

        private void btnImprimirEtiqueta_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProductos.SelectedRows.Count > 0)
                {
                    if(!Convert.ToBoolean(dgvProductos.CurrentRow.Cells["Servicio"].Value))
                    {
                        CantidadEtiquetasImprimir cantidadEtiquetasImprimir = new CantidadEtiquetasImprimir(dgvProductos.CurrentRow.Cells["Descripcion"].Value.ToString(),
                        dgvProductos.CurrentRow.Cells["CodigoBarra"].Value.ToString(), Convert.ToInt32(dgvProductos.CurrentRow.Cells["PrecioVenta"].Value).ToString(), dgvProductos.CurrentRow.Cells["Referencia"].Value.ToString(),
                        Convert.ToInt32(dgvProductos.CurrentRow.Cells["PrecioCompra"].Value).ToString());
                        cantidadEtiquetasImprimir.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("No se le puede imprimir etiquetas a los servicios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                } 
                else
                {
                    MessageBox.Show("Debe de seleccionar al menos un producto para imprimir etiqueta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido imprimir la etiqueta de este producto, verifique el producto e intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void CargarCBFiltro()
        {
            cbFiltro.Items.Add("ID");
            cbFiltro.Items.Add("Servicio");
            cbFiltro.Items.Add("Codigo de Barra");
            cbFiltro.Items.Add("Referencia");
            cbFiltro.Items.Add("Descripcion");
            cbFiltro.Items.Add("Marca");
            cbFiltro.Items.Add("Calidad");
            cbFiltro.Items.Add("Unidad de Medida");
            cbFiltro.Items.Add("Proveedor");
            cbFiltro.SelectedIndex = 4;
        }
        private void txtFiltro_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFiltro.Text))
            {
                txtFiltro.Text = "Escriba para filtrar...";
                txtFiltro.ForeColor = Color.Gray;
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

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtFiltro.Text != "Escriba para filtrar...")
                {
                    switch (cbFiltro.SelectedItem.ToString())
                    {
                        case "ID":
                            dgvProductos.DataSource = proc_CargarTodosProductos_Results.Where(p => p.ProductoID.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Servicio":
                            dgvProductos.DataSource = proc_CargarTodosProductos_Results.Where(p => p.Servicio.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Codigo de Barra":
                            dgvProductos.DataSource = proc_CargarTodosProductos_Results.Where(p => p.CodigoBarra.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Descripcion":
                            dgvProductos.DataSource = proc_CargarTodosProductos_Results.Where(p => p.Descripcion.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Unidad de Medida":
                            dgvProductos.DataSource = proc_CargarTodosProductos_Results.Where(p => p.UnidadMedida.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Proveedor":
                            dgvProductos.DataSource = proc_CargarTodosProductos_Results.Where(p => p.Proveedor.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Referencia":
                            dgvProductos.DataSource = proc_CargarTodosProductos_Results.Where(p => p.Referencia.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Marca":
                            dgvProductos.DataSource = proc_CargarTodosProductos_Results.Where(p => p.Marca.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;
                        case "Calidad":
                            dgvProductos.DataSource = proc_CargarTodosProductos_Results.Where(p => p.Calidad.ToString().ToLower().Contains(txtFiltro.Text.ToLower())).ToList();
                            break;

                        default:
                            break;
                    }
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

        private void cbFiltro_Validating(object sender, CancelEventArgs e)
        {
            if (cbFiltro.SelectedIndex == -1 && cbFiltro.Items.Count > 0)
            {
                cbFiltro.Focus();
            }
        }
    }
}
