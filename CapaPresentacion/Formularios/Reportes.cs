using CapaPresentacion.Reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Formularios
{
    public partial class Reportes : Form
    {
        public Reportes()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Reportes_Load(object sender, EventArgs e)
        {

        }

        private void pictureBoxClientes_Click(object sender, EventArgs e)
        {
            try
            {
                ReporteClientes reporteClientes = new ReporteClientes();
                reporteClientes.Show();                          
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido exportar los clientes, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void pictureBoxProveedores_Click(object sender, EventArgs e)
        {
            try
            {
                ReporteProveedores reporteProveedores = new ReporteProveedores();
                reporteProveedores.Show();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido exportar los proveedores, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void pictureBoxProductos_Click(object sender, EventArgs e)
        {
            try
            {
                ReporteProductos reporteProductos = new ReporteProductos();
                reporteProductos.Show();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido exportar los productos, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void pictureBoxGanancias_Click(object sender, EventArgs e)
        {
            try
            {
                ReporteGanancias reporteGanancias = new ReporteGanancias();
                reporteGanancias.Show();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido exportar las ganancias, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void pictureBoxFacturasCompra_Click(object sender, EventArgs e)
        {
            try
            {
                ElegirRangoFechas elegirRangoFechas = new ElegirRangoFechas("Facturas Compras");
                elegirRangoFechas.ShowDialog();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido exportar las facturas de compras, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void pictureBoxNotasCredito_Click(object sender, EventArgs e)
        {
            try
            {
                ElegirRangoFechas elegirRangoFechas = new ElegirRangoFechas("Notas Credito");
                elegirRangoFechas.ShowDialog();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido exportar las notas de credito, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void pictureBoxFacturasCFinal_Click(object sender, EventArgs e)
        {
            try
            {
                ElegirRangoFechas elegirRangoFechas = new ElegirRangoFechas("Facturas C Final");
                elegirRangoFechas.ShowDialog();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido exportar las facturas, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void pictureBoxFacturasCFiscal_Click(object sender, EventArgs e)
        {
            try
            {
                ElegirRangoFechas elegirRangoFechas = new ElegirRangoFechas("Facturas C Fiscal");
                elegirRangoFechas.ShowDialog();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido exportar las facturas, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void pictureBoxFacturasCGubernamental_Click(object sender, EventArgs e)
        {
            try
            {
                ElegirRangoFechas elegirRangoFechas = new ElegirRangoFechas("Facturas C Gubernamental");
                elegirRangoFechas.ShowDialog();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido exportar las facturas, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void pictureBoxFacturas_Click(object sender, EventArgs e)
        { 
            try
            {
                ElegirRangoFechas elegirRangoFechas = new ElegirRangoFechas("Facturas Ventas");
                elegirRangoFechas.ShowDialog();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido exportar las facturas, verifique si las configuraciones del sistema estan correctas e intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }
    }
}
