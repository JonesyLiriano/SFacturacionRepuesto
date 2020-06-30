﻿using CapaDatos;
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
        List<proc_CargarTodosUsers_Result> proc_CargarTodosUsers_Results;
        User usuarioEntidad = new User();
        bool resultado;

        public Usuarios()
        {
            InitializeComponent();
        }

        private void Usuarios_Activated(object sender, EventArgs e)
        {
          
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
            CargarUsuarios();
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
                proc_CargarTodosUsers_Results = usuariosNegocio.CargarTodosUsers().ToList();
                dgvUsuarios.DataSource = proc_CargarTodosUsers_Results;
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

                MessageBox.Show("Error: " + exc.ToString(),
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
    }
}