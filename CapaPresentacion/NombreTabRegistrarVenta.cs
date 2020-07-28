using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class NombreTabRegistrarVenta : Form
    {
        public string nombre;
        public NombreTabRegistrarVenta()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {            
            nombre = txtNombre.Text;
        }

        private void iconcerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NombreTabRegistrarVenta_Load(object sender, EventArgs e)
        {
            this.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.txtNombre.Select();
        }
    }
}
