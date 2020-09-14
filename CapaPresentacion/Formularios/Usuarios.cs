using CapaDatos;
using CapaNegocios;
using SFacturacion;
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
    public partial class Usuarios : Form
    {
        UsersNegocio usuariosNegocio = new UsersNegocio();
        BindingList<proc_CargarTodosUsers_Result> proc_CargarTodosUsers_Results;
        User usuarioEntidad = new User();
        bool resultado, finalLista;
        int indicePagina, tamanoPagina;
        string filtro, columna;

        public Usuarios()
        {
            InitializeComponent();
        }
             

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            MantenimientoUsuario frmMantUsuario = new MantenimientoUsuario();
            frmMantUsuario.Controls["btnAplicar"].Text = "Agregar";
            frmMantUsuario.ShowDialog();
            CargarUsuarios();

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                MantenimientoUsuario frmMantUsuario = new MantenimientoUsuario(CargarParametrosUsuario());
                frmMantUsuario.Controls["btnAplicar"].Text = "Editar";
                frmMantUsuario.ShowDialog();
                CargarUsuarios();
            }
            else
            {
                MessageBox.Show("Debe de seleccionar al menos un usuario para editar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            try
            {
                proc_CargarTodosUsers_Results = new BindingList<proc_CargarTodosUsers_Result>();
                indicePagina = 1;
                tamanoPagina = 50;
                CargarUsuarios();
                CargarCBFiltro();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: No se ha podido cargar todos los usuarios correctamente, intente de nuevo por favor.",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }

        private User CargarParametrosUsuario()
        {
            usuarioEntidad.UserID = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["UserID"].Value);
            usuarioEntidad.UserName = dgvUsuarios.CurrentRow.Cells["UserName"].Value.ToString();
            usuarioEntidad.UserPassword = dgvUsuarios.CurrentRow.Cells["UserPassword"].Value.ToString();
            usuarioEntidad.UserLevel = dgvUsuarios.CurrentRow.Cells["UserLevel"].Value.ToString();
            return usuarioEntidad;
        }

        private void CargarUsuarios()
        {
            try
            {
                dgvUsuarios.AutoGenerateColumns = false;
                List<proc_CargarTodosUsers_Result> lista = usuariosNegocio.CargarTodosUsers(indicePagina, tamanoPagina, filtro, columna).ToList();
                if (lista.Count < tamanoPagina)
                {
                    finalLista = true;
                }
                foreach (var item in lista)
                {
                    proc_CargarTodosUsers_Results.Add(item);
                }
                dgvUsuarios.DataSource = proc_CargarTodosUsers_Results;

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
            dgvUsuarios.Columns["UserID"].DisplayIndex = 0;
            dgvUsuarios.Columns["UserName"].DisplayIndex = 1;
            dgvUsuarios.Columns["UserPassword"].DisplayIndex = 2;
            dgvUsuarios.Columns["UserLevel"].DisplayIndex = 3;
            dgvUsuarios.Refresh();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsuarios.SelectedRows.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show(string.Format("Esta seguro que desea eliminar el usuario {0}?", dgvUsuarios.CurrentRow.Cells["UserName"].Value), "Eliminar Usuario", MessageBoxButtons.OKCancel);
                    if (dialogResult == DialogResult.OK)
                    {
                        resultado = usuariosNegocio.BorrarUser(Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["UserID"].Value));
                        ValidarBorrarUsuario(resultado);
                    }
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show("Error: Usuario no puede ser eliminado, posiblemente este usuario ya este relacionado con alguna factura, factura de compra, cotizacion, etc...",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }
        public void ValidarBorrarUsuario(bool result)
        {
            if (result)
            {
                MessageBox.Show(string.Format("El usuario ha sido borrado correctamente en la base de datos."), "Usuario Borrado Correctamente!", MessageBoxButtons.OK, MessageBoxIcon.Information);           
            }
            else
            {
                MessageBox.Show("El usuario no pudo ser borrado, favor de verificar los requerimientros", "Ha Ocurrido un error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarCBFiltro()
        {
            cbFiltro.Items.Add("ID");
            cbFiltro.Items.Add("Usuario");
            cbFiltro.Items.Add("Nivel");
            cbFiltro.SelectedIndex = 0;
        }
        private void txtFiltro_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFiltro.Text))
            {
                txtFiltro.Text = "Escriba para filtrar...";
                txtFiltro.ForeColor = Color.Gray;
                ResetearBusqueda();
                CargarUsuarios();
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
                            columna = "UserID";
                            filtro = txtFiltro.Text;
                            CargarUsuarios();
                            break;
                        case "Usuario":
                            columna = "UserName";
                            filtro = txtFiltro.Text;
                            CargarUsuarios();
                            break;
                        case "Nivel":
                            columna = "UserLevel";
                            filtro = txtFiltro.Text;
                            CargarUsuarios();
                            break;      
                        default:
                            break;
                    }
                }
                else
                {
                    CargarCBFiltro();
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

        private void dgvUsuarios_Scroll(object sender, ScrollEventArgs e)
        {
            if (!finalLista)
            {
                if ((e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.LargeIncrement) && e.ScrollOrientation == ScrollOrientation.VerticalScroll)
                {
                    int display = dgvUsuarios.Rows.Count - dgvUsuarios.DisplayedRowCount(false);
                    if (e.NewValue >= dgvUsuarios.Rows.Count - GetDisplayedRowsCount())
                    {
                        indicePagina++;
                        CargarUsuarios();
                    }
                }
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

        private int GetDisplayedRowsCount()
        {
            int count = dgvUsuarios.Rows[dgvUsuarios.FirstDisplayedScrollingRowIndex].Height;
            count = dgvUsuarios.Height / count;
            return count;
        }

        private void ResetearBusqueda()
        {
            proc_CargarTodosUsers_Results.Clear();
            finalLista = false;
            indicePagina = 1;
            filtro = null;
            columna = null;
        }
    }
}
