using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace CapaPresentacion.Formularios
{
    public partial class TabsFormularioVentas : Form
    {
        public TabsFormularioVentas()
        {
            InitializeComponent();
            AgregarFormVentas();
        }

        private void TabsFormularioVentas_Load(object sender, EventArgs e)
        {
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            tabControlVentas.Controls.Remove(tabControlVentas.SelectedTab);
        }

        private void tabControlVentas_DrawItem(object sender, DrawItemEventArgs e)
        {
            if(e.Index != 0)
            {
                e.Graphics.DrawString("x", e.Font, Brushes.Black, e.Bounds.Right - 15, e.Bounds.Top + 1);
                
            } 
            else
            {
                e.Graphics.DrawImage(imageList1.Images[0], e.Bounds.Left + 18, e.Bounds.Top + 6);
            }
            e.Graphics.DrawString(this.tabControlVentas.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left, e.Bounds.Top + 4);
            e.DrawFocusRectangle();
        }   
        private void tabControlVentas_MouseClick(object sender, MouseEventArgs e)
        {
            if(tabControlVentas.SelectedTab != tabAgregar)
            {
                //Looping through the controls.
                for (int i = 0; i < this.tabControlVentas.TabPages.Count; i++)
                {
                    Rectangle r = tabControlVentas.GetTabRect(i);
                    //Getting the position of the "x" mark.
                    Rectangle closeButton = new Rectangle(r.Right - 15, r.Top + 7, 9, 7);
                    if (closeButton.Contains(e.Location))
                    {
                        if (MessageBox.Show("Esta seguro que desea cerrar esta pestaña?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            this.tabControlVentas.TabPages.RemoveAt(i);
                            if(this.tabControlVentas.TabPages.Count > 1)
                            {
                                this.tabControlVentas.SelectedIndex = 1;
                            }                            
                            break;
                        }
                    }
                }
            } 
            else
            {                
             AgregarFormVentas();
            }
        }

        private void tabControlVentas_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (tabControlVentas.Controls.Count == 1)
            {
                this.Close();
            }
        }
        private void AgregarFormVentas()
        {
            if(tabControlVentas.Controls.Count > 1)
            {
                NombreTabRegistrarVenta nombreTabsDialog = new NombreTabRegistrarVenta();
                // Show testDialog as a modal dialog and determine if DialogResult = OK.
                if (nombreTabsDialog.ShowDialog(this) == DialogResult.OK)
                {
                    AgregarTabs(nombreTabsDialog.nombre);
                }
                nombreTabsDialog.Dispose();

            } 
            else
            {
                AgregarTabs("Tab");
            }    
        }

        private void AgregarTabs(string nombre)
        {
            TabPage tabPage = new TabPage(nombre);
            tabPage.Margin = new Padding(0);
            tabPage.Padding = new Padding(0);
            RegistrarVenta registrarVenta = new RegistrarVenta();
            registrarVenta.TopLevel = false;
            registrarVenta.Dock = DockStyle.Fill;
            tabPage.Controls.Add(registrarVenta);
            tabControlVentas.Controls.Add(tabPage);
            registrarVenta.Show();
            tabControlVentas.SelectedTab = tabPage;
        }
    }
}
