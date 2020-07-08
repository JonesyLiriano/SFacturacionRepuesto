namespace CapaPresentacion.Formularios
{
    partial class OrdenCompra
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrdenCompra));
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.ProductoID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnidadMedida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Existencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CantMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ordenada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Recibida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BarraTitulo = new System.Windows.Forms.Panel();
            this.pbBarraMenu = new System.Windows.Forms.PictureBox();
            this.labelBarraMenu = new System.Windows.Forms.Label();
            this.iconcerrar = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbProveedor = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRecibirTodo = new System.Windows.Forms.Button();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnBuscarProd = new System.Windows.Forms.Button();
            this.btnFacturarOrdenCorte = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.BarraTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBarraMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconcerrar)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvProductos
            // 
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvProductos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvProductos.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvProductos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvProductos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSeaGreen;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProductos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvProductos.ColumnHeadersHeight = 30;
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvProductos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductoID,
            this.Descripcion,
            this.UnidadMedida,
            this.Existencia,
            this.CantMin,
            this.PrecioCompra,
            this.Ordenada,
            this.Recibida,
            this.Estatus});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProductos.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvProductos.EnableHeadersVisualStyles = false;
            this.dgvProductos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvProductos.Location = new System.Drawing.Point(49, 118);
            this.dgvProductos.MultiSelect = false;
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.RowHeadersVisible = false;
            this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.Size = new System.Drawing.Size(770, 447);
            this.dgvProductos.TabIndex = 115;
            this.dgvProductos.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvProductos_CellValidating);
            // 
            // ProductoID
            // 
            this.ProductoID.DataPropertyName = "ProductoID";
            this.ProductoID.HeaderText = "ID";
            this.ProductoID.Name = "ProductoID";
            this.ProductoID.Width = 41;
            // 
            // Descripcion
            // 
            this.Descripcion.DataPropertyName = "Descripcion";
            this.Descripcion.HeaderText = "Descripcion";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.Width = 86;
            // 
            // UnidadMedida
            // 
            this.UnidadMedida.DataPropertyName = "UnidadMedida";
            this.UnidadMedida.HeaderText = "UM";
            this.UnidadMedida.Name = "UnidadMedida";
            this.UnidadMedida.Width = 47;
            // 
            // Existencia
            // 
            this.Existencia.DataPropertyName = "Existencia";
            this.Existencia.HeaderText = "Existencia";
            this.Existencia.Name = "Existencia";
            this.Existencia.Width = 78;
            // 
            // CantMin
            // 
            this.CantMin.DataPropertyName = "CantMin";
            this.CantMin.HeaderText = "Cant Min";
            this.CantMin.Name = "CantMin";
            this.CantMin.Width = 67;
            // 
            // PrecioCompra
            // 
            this.PrecioCompra.DataPropertyName = "PrecioCompra";
            this.PrecioCompra.HeaderText = "Costo";
            this.PrecioCompra.Name = "PrecioCompra";
            this.PrecioCompra.Width = 57;
            // 
            // Ordenada
            // 
            this.Ordenada.DataPropertyName = "Ordenada";
            this.Ordenada.HeaderText = "Ordenada";
            this.Ordenada.Name = "Ordenada";
            this.Ordenada.Width = 77;
            // 
            // Recibida
            // 
            this.Recibida.DataPropertyName = "Recibida";
            this.Recibida.HeaderText = "Recibida";
            this.Recibida.Name = "Recibida";
            this.Recibida.Width = 72;
            // 
            // Estatus
            // 
            this.Estatus.DataPropertyName = "Estatus";
            this.Estatus.HeaderText = "Estatus";
            this.Estatus.Name = "Estatus";
            this.Estatus.Visible = false;
            this.Estatus.Width = 67;
            // 
            // BarraTitulo
            // 
            this.BarraTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(7)))), ((int)(((byte)(17)))));
            this.BarraTitulo.Controls.Add(this.pbBarraMenu);
            this.BarraTitulo.Controls.Add(this.labelBarraMenu);
            this.BarraTitulo.Controls.Add(this.iconcerrar);
            this.BarraTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.BarraTitulo.Location = new System.Drawing.Point(0, 0);
            this.BarraTitulo.Name = "BarraTitulo";
            this.BarraTitulo.Size = new System.Drawing.Size(1070, 36);
            this.BarraTitulo.TabIndex = 114;
            this.BarraTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BarraTitulo_MouseDown);
            // 
            // pbBarraMenu
            // 
            this.pbBarraMenu.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pbBarraMenu.ErrorImage")));
            this.pbBarraMenu.Image = ((System.Drawing.Image)(resources.GetObject("pbBarraMenu.Image")));
            this.pbBarraMenu.Location = new System.Drawing.Point(3, 3);
            this.pbBarraMenu.Name = "pbBarraMenu";
            this.pbBarraMenu.Size = new System.Drawing.Size(36, 30);
            this.pbBarraMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbBarraMenu.TabIndex = 6;
            this.pbBarraMenu.TabStop = false;
            // 
            // labelBarraMenu
            // 
            this.labelBarraMenu.AutoSize = true;
            this.labelBarraMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBarraMenu.ForeColor = System.Drawing.Color.White;
            this.labelBarraMenu.Location = new System.Drawing.Point(45, 9);
            this.labelBarraMenu.Name = "labelBarraMenu";
            this.labelBarraMenu.Size = new System.Drawing.Size(135, 20);
            this.labelBarraMenu.TabIndex = 2;
            this.labelBarraMenu.Text = "Orden de Compra";
            // 
            // iconcerrar
            // 
            this.iconcerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconcerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconcerrar.Image = ((System.Drawing.Image)(resources.GetObject("iconcerrar.Image")));
            this.iconcerrar.Location = new System.Drawing.Point(1040, 9);
            this.iconcerrar.Name = "iconcerrar";
            this.iconcerrar.Size = new System.Drawing.Size(18, 18);
            this.iconcerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconcerrar.TabIndex = 1;
            this.iconcerrar.TabStop = false;
            this.iconcerrar.Click += new System.EventHandler(this.iconcerrar_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbProveedor);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(49, 42);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(483, 70);
            this.groupBox4.TabIndex = 116;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Datos Proveedor";
            // 
            // cbProveedor
            // 
            this.cbProveedor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbProveedor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbProveedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProveedor.FormattingEnabled = true;
            this.cbProveedor.Location = new System.Drawing.Point(138, 26);
            this.cbProveedor.Name = "cbProveedor";
            this.cbProveedor.Size = new System.Drawing.Size(330, 26);
            this.cbProveedor.TabIndex = 118;
            this.cbProveedor.Validating += new System.ComponentModel.CancelEventHandler(this.cbProveedor_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(5, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 20);
            this.label2.TabIndex = 117;
            this.label2.Text = "Proveedor";
            // 
            // btnRecibirTodo
            // 
            this.btnRecibirTodo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRecibirTodo.BackColor = System.Drawing.Color.ForestGreen;
            this.btnRecibirTodo.FlatAppearance.BorderSize = 0;
            this.btnRecibirTodo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnRecibirTodo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecibirTodo.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecibirTodo.ForeColor = System.Drawing.Color.White;
            this.btnRecibirTodo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecibirTodo.Location = new System.Drawing.Point(859, 235);
            this.btnRecibirTodo.Name = "btnRecibirTodo";
            this.btnRecibirTodo.Size = new System.Drawing.Size(180, 35);
            this.btnRecibirTodo.TabIndex = 118;
            this.btnRecibirTodo.Text = "Recibir Todo";
            this.btnRecibirTodo.UseVisualStyleBackColor = false;
            this.btnRecibirTodo.Click += new System.EventHandler(this.btnRecibirTodo_Click);
            // 
            // btnQuitar
            // 
            this.btnQuitar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuitar.BackColor = System.Drawing.Color.Firebrick;
            this.btnQuitar.FlatAppearance.BorderSize = 0;
            this.btnQuitar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnQuitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuitar.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuitar.ForeColor = System.Drawing.Color.White;
            this.btnQuitar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuitar.Location = new System.Drawing.Point(859, 355);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(180, 35);
            this.btnQuitar.TabIndex = 122;
            this.btnQuitar.Text = "Quitar";
            this.btnQuitar.UseVisualStyleBackColor = false;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(859, 118);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(180, 35);
            this.btnGuardar.TabIndex = 120;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnBuscarProd
            // 
            this.btnBuscarProd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscarProd.BackColor = System.Drawing.Color.ForestGreen;
            this.btnBuscarProd.FlatAppearance.BorderSize = 0;
            this.btnBuscarProd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnBuscarProd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarProd.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarProd.ForeColor = System.Drawing.Color.White;
            this.btnBuscarProd.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarProd.Image")));
            this.btnBuscarProd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscarProd.Location = new System.Drawing.Point(859, 177);
            this.btnBuscarProd.Name = "btnBuscarProd";
            this.btnBuscarProd.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnBuscarProd.Size = new System.Drawing.Size(180, 35);
            this.btnBuscarProd.TabIndex = 123;
            this.btnBuscarProd.Text = "Buscar Producto";
            this.btnBuscarProd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscarProd.UseVisualStyleBackColor = false;
            this.btnBuscarProd.Click += new System.EventHandler(this.btnBuscarProd_Click);
            // 
            // btnFacturarOrdenCorte
            // 
            this.btnFacturarOrdenCorte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFacturarOrdenCorte.BackColor = System.Drawing.Color.ForestGreen;
            this.btnFacturarOrdenCorte.FlatAppearance.BorderSize = 0;
            this.btnFacturarOrdenCorte.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnFacturarOrdenCorte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFacturarOrdenCorte.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFacturarOrdenCorte.ForeColor = System.Drawing.Color.White;
            this.btnFacturarOrdenCorte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFacturarOrdenCorte.Location = new System.Drawing.Point(859, 297);
            this.btnFacturarOrdenCorte.Name = "btnFacturarOrdenCorte";
            this.btnFacturarOrdenCorte.Size = new System.Drawing.Size(180, 35);
            this.btnFacturarOrdenCorte.TabIndex = 124;
            this.btnFacturarOrdenCorte.Text = "Facturar Orden Corte";
            this.btnFacturarOrdenCorte.UseVisualStyleBackColor = false;
            this.btnFacturarOrdenCorte.Click += new System.EventHandler(this.btnFacturarOrdenCorte_Click);
            // 
            // OrdenCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1070, 577);
            this.Controls.Add(this.btnFacturarOrdenCorte);
            this.Controls.Add(this.btnBuscarProd);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnRecibirTodo);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.dgvProductos);
            this.Controls.Add(this.BarraTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OrdenCompra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.Load += new System.EventHandler(this.OrdenCompra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.BarraTitulo.ResumeLayout(false);
            this.BarraTitulo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBarraMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconcerrar)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.Panel BarraTitulo;
        private System.Windows.Forms.PictureBox pbBarraMenu;
        private System.Windows.Forms.Label labelBarraMenu;
        private System.Windows.Forms.PictureBox iconcerrar;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cbProveedor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRecibirTodo;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnBuscarProd;
        private System.Windows.Forms.Button btnFacturarOrdenCorte;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductoID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnidadMedida;
        private System.Windows.Forms.DataGridViewTextBoxColumn Existencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn CantMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioCompra;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ordenada;
        private System.Windows.Forms.DataGridViewTextBoxColumn Recibida;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estatus;
    }
}