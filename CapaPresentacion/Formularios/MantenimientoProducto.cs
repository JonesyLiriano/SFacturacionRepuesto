using CapaDatos;
using CapaNegocios;
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

namespace CapaPresentacion
{
    public partial class MantenimientoProducto : Form
    {
        ProductosNegocio productosNegocio = new ProductosNegocio();
        ProveedoresNegocio proveedoresNegocio = new ProveedoresNegocio();
        Producto productoEntidad = new Producto();
        private bool respuesta;
        private int productoID;
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        public MantenimientoProducto()
        {
            InitializeComponent();
            CargarCbProveedores();
        }

        public MantenimientoProducto(Producto producto)
        {
            InitializeComponent();
            CargarCbProveedores();
            LlenarTextBoxs(producto);
        }

       
        private void iconcerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MantenimientoProducto_Load(object sender, EventArgs e)
        {            
            txtCodigoBarra.Select();
        }

        private void CargarCbProveedores()
        {
            cbProveedor.DisplayMember = "Nombre";
            cbProveedor.ValueMember = "ProveedorID";
            cbProveedor.DataSource = proveedoresNegocio.CargarTodosProveedores();
            cbProveedor.SelectedIndex = -1;

        }
        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (btnAplicar.Text == "Agregar")
            {
                AgregarProducto();

            }
            else if (btnAplicar.Text == "Editar")
            {
                EditarProducto();
            }
        }

        private void AgregarProducto()
        {
            try
            {
                if (ValidarCampos())
                {
                    DialogResult dialogResult = MessageBox.Show("Esta seguro que desea agregar" +
                        " nuevo producto/servicio a la base de datos?", "Nuevo Producto/Servicio", MessageBoxButtons.OKCancel);
                    if (dialogResult == DialogResult.OK)
                    {                        
                        productoEntidad.Servicio = checkboxServicio.Checked;
                        productoEntidad.Descripcion = txtDescripcion.Text;
                        productoEntidad.ProveedorID = Convert.ToInt32(cbProveedor.SelectedValue) > 0 ? (int?)Convert.ToInt32(cbProveedor.SelectedValue) : null;
                        productoEntidad.Existencia = !string.IsNullOrEmpty(txtExistencia.Text) ? (double?)Convert.ToDouble(txtExistencia.Text) : null;                        
                        productoEntidad.PrecioCompra = !string.IsNullOrEmpty(txtPrecioCompra.Text) ? (decimal?)Convert.ToDouble(txtPrecioCompra.Text) : null;
                        productoEntidad.PrecioVenta = Convert.ToDecimal(txtPrecioVenta.Text);
                        productoEntidad.PrecioVentaMin = Convert.ToDecimal(txtPrecioVentaMin.Text);
                        productoEntidad.ITBIS = checkboxITBIS.Checked;
                        productoEntidad.Descuento = Convert.ToDecimal(txtDescuento.Text);
                        productoEntidad.CantMin = !string.IsNullOrEmpty(txtCantMin.Text) ? (double?)Convert.ToDouble(txtCantMin.Text) : null;
                        productoEntidad.CantMax = !string.IsNullOrEmpty(txtCantMax.Text) ? (double?)Convert.ToDouble(txtCantMax.Text) : null;
                        productoEntidad.CodigoBarra = txtCodigoBarra.Text;
                        productoEntidad.UnidadMedida = txtUnidadMedida.Text.ToUpper();
                        productoEntidad.Referencia = txtReferencia.Text;
                        productoEntidad.Marca = txtMarca.Text;
                        productoEntidad.Calidad = txtCalidad.Text;

                        var result = productosNegocio.AgregarProducto(productoEntidad);

                        respuesta = result.Item1;
                        productoID = result.Item2;
                        ValidarProductoAgregado(respuesta, productoID);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: Producto no pudo ser agregado, verifique que el codigo de barra no exista e intente de nuevo por favor.",
                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());

            }

        }

        private void ValidarProductoAgregado(bool respuesta, int productoID)
        {
            if (respuesta)
            {
                MessageBox.Show(string.Format("El proveedor/servicio ha sido agregado correctamente a la base de datos, con el codigo: {0}", productoID), "Producto/Servicio Agregado Correctamente!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("El producto/servicio no pudo ser agregado a la base de datos, favor de verificar los requerimientros", "Ha Ocurrido un error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void EditarProducto()
        {
            try
            {
                if (ValidarCampos())
                {
                    DialogResult dialogResult = MessageBox.Show("Esta seguro que desea editar este producto/servicio?",
                        "Editar Producto/Servicio", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.OK)
                    {
                        
                        productoEntidad.ProductoID = Convert.ToInt32(txtID.Text);
                        productoEntidad.Servicio = checkboxServicio.Checked;
                        productoEntidad.Descripcion = txtDescripcion.Text;
                        productoEntidad.ProveedorID = Convert.ToInt32(cbProveedor.SelectedValue) > 0 ? (int?)Convert.ToInt32(cbProveedor.SelectedValue) : null;
                        productoEntidad.Existencia = !string.IsNullOrEmpty(txtExistencia.Text) ? (double?)Convert.ToDouble(txtExistencia.Text) : null;
                        productoEntidad.PrecioCompra = !string.IsNullOrEmpty(txtPrecioCompra.Text) ? (decimal?)Convert.ToDouble(txtPrecioCompra.Text) : null;
                        productoEntidad.PrecioVenta = Convert.ToDecimal(txtPrecioVenta.Text);
                        productoEntidad.PrecioVentaMin = Convert.ToDecimal(txtPrecioVentaMin.Text);
                        productoEntidad.ITBIS = checkboxITBIS.Checked;
                        productoEntidad.Descuento = Convert.ToDecimal(txtDescuento.Text);
                        productoEntidad.CantMin = !string.IsNullOrEmpty(txtCantMin.Text) ? (double?)Convert.ToDouble(txtCantMin.Text) : null;
                        productoEntidad.CantMax = !string.IsNullOrEmpty(txtCantMax.Text) ? (double?)Convert.ToDouble(txtCantMax.Text) : null;
                        productoEntidad.CodigoBarra = txtCodigoBarra.Text;
                        productoEntidad.UnidadMedida = txtUnidadMedida.Text.ToUpper();
                        productoEntidad.Referencia = txtReferencia.Text;
                        productoEntidad.Marca = txtMarca.Text;
                        productoEntidad.Calidad = txtCalidad.Text;

                        respuesta = productosNegocio.EditarProducto(productoEntidad);
                        ValidarEditarProducto(respuesta);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: Producto no pudo ser editado, verifique que los campos e intente de nuevo por favor.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }

        }

        private void ValidarEditarProducto(bool result)
        {
            if (result)
            {
                MessageBox.Show(string.Format("El producto/servicio ha sido editado correctamente en la base de datos."), "Producto/Servicio Editado Correctamente!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("El producto/servicio no pudo ser editado, favor de verificar los requerimientros", "Ha Ocurrido un error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            double comprobadorDouble;
            decimal comprobadorDecimal;


            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                MessageBox.Show("El Campo DESCRIPCION no puede estar vacio");
                return false;
            }
            if (string.IsNullOrEmpty(txtDescuento.Text) || !decimal.TryParse(txtDescuento.Text, out comprobadorDecimal))
            {
                MessageBox.Show("El Campo DESCUENTO no puede estar vacio o tiene que ser un numero valido.");
                return false;
            }            
            if (string.IsNullOrEmpty(txtPrecioVenta.Text) || !decimal.TryParse(txtPrecioVenta.Text, out comprobadorDecimal))
            {
                MessageBox.Show("El Campo PRECIO VENTA no puede estar vacio o tiene que ser un numero valido.");
                return false;
            }
            if (string.IsNullOrEmpty(txtPrecioVentaMin.Text) || !decimal.TryParse(txtPrecioVentaMin.Text, out comprobadorDecimal))
            {
                MessageBox.Show("El Campo PRECIO VENTA MIN no puede estar vacio o tiene que ser un numero valido.");
                return false;
            }
            if (string.IsNullOrEmpty(txtCodigoBarra.Text))
            {
                MessageBox.Show("El Campo CODIGO BARRA no puede estar vacio.");
                return false;
            }
            if (string.IsNullOrEmpty(txtUnidadMedida.Text))
            {
                MessageBox.Show("El Campo UNIDAD MEDIDA no puede estar vacio.");
                return false;
            }
            if (!checkboxServicio.Checked)
            {
                if (string.IsNullOrEmpty(txtCantMax.Text) || !double.TryParse(txtCantMax.Text, out comprobadorDouble))
                {
                    MessageBox.Show("El Campo CANT MAXIMA no puede estar vacio o tiene que ser un numero valido.");
                    return false;
                }
                if (string.IsNullOrEmpty(txtCantMin.Text) || !double.TryParse(txtCantMin.Text, out comprobadorDouble))
                {
                    MessageBox.Show("El Campo CANT MINIMA no puede estar vacio o tiene que ser un numero valido.");
                    return false;
                }               
                if (string.IsNullOrEmpty(txtPrecioCompra.Text) || !decimal.TryParse(txtPrecioCompra.Text, out comprobadorDecimal))
                {
                    MessageBox.Show("El Campo PRECIO COMPRA no puede estar vacio o tiene que ser un numero valido.");
                    return false;
                }
                if (string.IsNullOrEmpty(txtExistencia.Text) || !double.TryParse(txtExistencia.Text, out comprobadorDouble))
                {
                    MessageBox.Show("El Campo EXISTENCIA no puede estar vacio o tiene que ser un numero valido.");
                    return false;
                }
                if (string.IsNullOrEmpty(txtReferencia.Text))
                {
                    MessageBox.Show("El Campo REFERENCIA no puede estar vacio.");
                    return false;
                }
                if (string.IsNullOrEmpty(txtMarca.Text))
                {
                    MessageBox.Show("El Campo MARCA no puede estar vacio.");
                    return false;
                }
                if (string.IsNullOrEmpty(txtCalidad.Text))
                {
                    MessageBox.Show("El Campo CALIDAD no puede estar vacio.");
                    return false;
                }

            }
            return true;
        }

        private void LlenarTextBoxs(Producto producto)
        {
            txtID.Text = producto.ProductoID.ToString();            
            txtDescripcion.Text = producto.Descripcion; 
            txtExistencia.Text = producto.Existencia.ToString();
            cbProveedor.SelectedValue = producto.ProveedorID;
            txtPrecioCompra.Text = producto.PrecioCompra.ToString();
            txtPrecioVenta.Text = producto.PrecioVenta.ToString();
            txtPrecioVentaMin.Text = producto.PrecioVentaMin.ToString();
            checkboxITBIS.Checked = producto.ITBIS;
            txtDescuento.Text = producto.Descuento.ToString();
            txtCantMin.Text = producto.CantMin.ToString();
            txtCantMax.Text = producto.CantMax.ToString();
            txtCodigoBarra.Text = producto.CodigoBarra.ToString();
            txtUnidadMedida.Text = producto.UnidadMedida.ToString();
            txtReferencia.Text = producto.Referencia;
            txtMarca.Text = producto.Marca;
            txtCalidad.Text = producto.Calidad;
            checkboxServicio.Checked = producto.Servicio;
        }

        private void checkboxServicio_CheckedChanged(object sender, EventArgs e)
        {
            if(checkboxServicio.Checked)
            {
                txtExistencia.Clear();
                txtExistencia.Enabled = false;
                cbProveedor.SelectedIndex = -1;
                cbProveedor.Enabled = false;
                txtPrecioCompra.Clear();
                txtPrecioCompra.Enabled = false;
                txtCantMin.Clear();
                txtCantMin.Enabled = false;
                txtCantMax.Clear();
                txtCantMax.Enabled = false;
                txtReferencia.Clear();
                txtReferencia.Enabled = false;
                txtMarca.Clear();
                txtMarca.Enabled = false;
                txtCalidad.Clear();
                txtCalidad.Enabled = false;

            } else
            {
                txtExistencia.Enabled = true;
                cbProveedor.Enabled = true;
                txtPrecioCompra.Enabled = true;
                txtCantMin.Enabled = true;
                txtCantMax.Enabled = true;
                txtReferencia.Enabled = true;
                txtMarca.Enabled = true;
                txtCalidad.Enabled = true;
            }
        }

        private void cbProveedor_Validating(object sender, CancelEventArgs e)
        {
            if (cbProveedor.SelectedIndex == -1 && cbProveedor.Items.Count > 0)
            {
                cbProveedor.Focus();
            }
        }
    }
}
