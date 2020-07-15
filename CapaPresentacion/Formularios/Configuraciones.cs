using CapaNegocios;
using CapaPresentacion.Clases;
using Microsoft.ReportingServices.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Formularios
{
    public partial class Configuraciones : Form
    {
        SistemaResumenNegocio sistemaResumenNegocio = new SistemaResumenNegocio();
        public Configuraciones()
        {
            InitializeComponent();
        }

        private void Configuraciones_Load(object sender, EventArgs e)
        {
            try
            {
                CargarCbTipoImpresora();
                CargarTextBox();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido cargar las configuraciones correctamente, intente de nuevo por favor. " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void CargarCbTipoImpresora()
        {
            cbTipoImpresora.Items.Add("Papel A4");
            cbTipoImpresora.Items.Add("Matricial");
            cbTipoImpresora.SelectedIndex = 0;
        }
        private void btnCargarLogo_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg;)|*.jpg; *.jpeg;";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pbLogo.Image = new Bitmap(open.FileName);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Esta seguro que desea aplicar los cambios", "Cambios de configuracion", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    AplicarCambios();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido guardar las configuraciones, verifique los campos e intente nuevamente por favor. " + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }
        private void AplicarCambios()
        {

            Properties.Settings.Default.ITBIS = decimal.Parse(txtITBIS.Text);
            Properties.Settings.Default.CFiscal = Convert.ToInt32(txtSFacturaFiscal.Text);
            Properties.Settings.Default.CFinal = Convert.ToInt32(txtSFacturaFinal.Text);
            Properties.Settings.Default.CGubernamental = Convert.ToInt32(txtSFacturaGubernamental.Text);
            Properties.Settings.Default.FechaVencimiento = Convert.ToDateTime(dtFechaVencimiento.Text);
            Properties.Settings.Default.NCredito = Convert.ToInt32(txtSNCredito.Text);
            Properties.Settings.Default.NombreEmpresa = txtNombreEmpresa.Text;
            Properties.Settings.Default.Direccion = txtDireccion.Text;
            Properties.Settings.Default.Telefono = txtTelefono.Text;
            Properties.Settings.Default.RazonSocial = txtRazonSocial.Text;
            Properties.Settings.Default.CedulaORnc = txtRNCoCedula.Text;
            Properties.Settings.Default.Impresora = txtNombreImpresora.Text;
            Properties.Settings.Default.TipoImpresora = cbTipoImpresora.SelectedItem.ToString();
            Properties.Settings.Default.Email = txtEmail.Text;
            Properties.Settings.Default.ImpresoraTermica = txtImpresoraTermica.Text;
            if (pbLogo.Image != null)
            {
                using (var b = new Bitmap(pbLogo.Image))
                {
                    using (var ms = new MemoryStream())
                    {
                        b.Save(ms, ImageFormat.Jpeg);
                        Properties.Settings.Default.Logo = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            Properties.Settings.Default.Save();
            MessageBox.Show("Los cambios se aplicaron correctamente.", "Cambios Aplicados", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void CargarTextBox()
        {
            try
            {
                txtITBIS.Text = Properties.Settings.Default.ITBIS.ToString();
                txtSFacturaFiscal.Text = Properties.Settings.Default.CFiscal.ToString();
                txtSFacturaFinal.Text = Properties.Settings.Default.CFinal.ToString();
                txtSFacturaGubernamental.Text = Properties.Settings.Default.CGubernamental.ToString();
                dtFechaVencimiento.Text = Properties.Settings.Default.FechaVencimiento.ToString();
                txtSNCredito.Text = Properties.Settings.Default.NCredito.ToString();
                txtNombreEmpresa.Text = Properties.Settings.Default.NombreEmpresa.ToString();
                txtDireccion.Text = Properties.Settings.Default.Direccion.ToString();
                txtTelefono.Text = Properties.Settings.Default.Telefono.ToString();
                txtRazonSocial.Text = Properties.Settings.Default.RazonSocial.ToString();
                txtRNCoCedula.Text = Properties.Settings.Default.CedulaORnc.ToString();
                txtNombreImpresora.Text = Properties.Settings.Default.Impresora.ToString();
                cbTipoImpresora.SelectedItem = Properties.Settings.Default.TipoImpresora.ToString();
                txtEmail.Text = Properties.Settings.Default.Email.ToString();
                txtImpresoraTermica.Text = Properties.Settings.Default.ImpresoraTermica.ToString();
                if (Properties.Settings.Default.Logo != "")
                {
                    byte[] imageBytes = Convert.FromBase64String(Properties.Settings.Default.Logo);
                    using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                    {
                        pbLogo.Image = Image.FromStream(ms, true);

                    }
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido cargar las configuraciones correctamente, intente de nuevo por favor." + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbTipoImpresora_Validating(object sender, CancelEventArgs e)
        {
            if (cbTipoImpresora.SelectedIndex == -1 && cbTipoImpresora.Items.Count > 0)
            {
                cbTipoImpresora.Focus();
            }
        }

        private void btnSubirBackupNube_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Esta seguro que desea subir el backup de los datos a la nube?", "Generar Backup", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    RealizarCopiaDeSeguridad();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido subir el backup a la nube, verifique que tenga internet e intente de nuevo por favor." + exc.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private void RealizarCopiaDeSeguridad()
        {
            if (File.Exists(@"C:\SFacturacion\Backup\BackUpBaseDatos.bak"))
            {
                File.Delete(@"C:\SFacturacion\Backup\BackUpBaseDatos.bak");
            }
            sistemaResumenNegocio.RealizarCopiadeSeguridad();
            ComprimirCopiaDeSeguridad();
            SubirBackupAGoogleDrive();
        }

        private void SubirBackupAGoogleDrive()
        {
            ControladorMegaApi controladorMegaApi = new ControladorMegaApi();
            controladorMegaApi.UploadFileToMega();
            MessageBox.Show("Copia de Seguridad fue realizada con exito", "Copia de Seguridad Completada", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void ComprimirCopiaDeSeguridad()
        {
            if (File.Exists(@"C:\SFacturacion\BD\BackUpBaseDatos"
            + Properties.Settings.Default.NombreEmpresa.Replace(" ", "") + ".zip"))
            {
                File.Delete(@"C:\SFacturacion\BD\BackUpBaseDatos"
                + Properties.Settings.Default.NombreEmpresa.Replace(" ", "") + ".zip");
            }

            ZipFile.CreateFromDirectory(@"C:\SFacturacion\Backup", @"C:\SFacturacion\BD\BackUpBaseDatos"
             + Properties.Settings.Default.NombreEmpresa.Replace(" ", "") + ".zip");
        }
    }
}
