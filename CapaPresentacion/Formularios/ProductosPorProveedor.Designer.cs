namespace CapaPresentacion.Formularios
{
    partial class ProductosPorProveedor
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnDesmarcarTodos = new System.Windows.Forms.Button();
            this.btnMarcarTodos = new System.Windows.Forms.Button();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.Seleccionar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ProductoID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnidadMedida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCerrar = new System.Windows.Forms.Label();
            this.btnSeleccionar = new System.Windows.Forms.Button();
            this.checkBoxProdExistBaja = new System.Windows.Forms.CheckBox();
            this.txtFiltro = new System.Windows.Forms.TextBox();
            this.cbFiltro = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDesmarcarTodos
            // 
            this.btnDesmarcarTodos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDesmarcarTodos.BackColor = System.Drawing.Color.Firebrick;
            this.btnDesmarcarTodos.FlatAppearance.BorderSize = 0;
            this.btnDesmarcarTodos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnDesmarcarTodos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDesmarcarTodos.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDesmarcarTodos.ForeColor = System.Drawing.Color.White;
            this.btnDesmarcarTodos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDesmarcarTodos.Location = new System.Drawing.Point(967, 239);
            this.btnDesmarcarTodos.Name = "btnDesmarcarTodos";
            this.btnDesmarcarTodos.Size = new System.Drawing.Size(180, 35);
            this.btnDesmarcarTodos.TabIndex = 47;
            this.btnDesmarcarTodos.Text = "Desmarcar Todos";
            this.btnDesmarcarTodos.UseVisualStyleBackColor = false;
            this.btnDesmarcarTodos.Click += new System.EventHandler(this.btnDesmarcarTodos_Click);
            // 
            // btnMarcarTodos
            // 
            this.btnMarcarTodos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMarcarTodos.BackColor = System.Drawing.Color.ForestGreen;
            this.btnMarcarTodos.FlatAppearance.BorderSize = 0;
            this.btnMarcarTodos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnMarcarTodos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMarcarTodos.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMarcarTodos.ForeColor = System.Drawing.Color.White;
            this.btnMarcarTodos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMarcarTodos.Location = new System.Drawing.Point(967, 180);
            this.btnMarcarTodos.Name = "btnMarcarTodos";
            this.btnMarcarTodos.Size = new System.Drawing.Size(180, 35);
            this.btnMarcarTodos.TabIndex = 46;
            this.btnMarcarTodos.Text = "Marcar Todos";
            this.btnMarcarTodos.UseVisualStyleBackColor = false;
            this.btnMarcarTodos.Click += new System.EventHandler(this.btnMarcarTodos_Click);
            // 
            // dgvProductos
            // 
            this.dgvProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvProductos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvProductos.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvProductos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvProductos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.LightSeaGreen;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProductos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvProductos.ColumnHeadersHeight = 30;
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvProductos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Seleccionar,
            this.ProductoID,
            this.UnidadMedida});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProductos.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvProductos.EnableHeadersVisualStyles = false;
            this.dgvProductos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvProductos.Location = new System.Drawing.Point(40, 66);
            this.dgvProductos.MultiSelect = false;
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.RowHeadersVisible = false;
            this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.Size = new System.Drawing.Size(895, 622);
            this.dgvProductos.TabIndex = 45;
            // 
            // Seleccionar
            // 
            this.Seleccionar.HeaderText = "";
            this.Seleccionar.Name = "Seleccionar";
            this.Seleccionar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Seleccionar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Seleccionar.Width = 20;
            // 
            // ProductoID
            // 
            this.ProductoID.DataPropertyName = "ProductoID";
            this.ProductoID.HeaderText = "ID";
            this.ProductoID.Name = "ProductoID";
            this.ProductoID.Width = 44;
            // 
            // UnidadMedida
            // 
            this.UnidadMedida.DataPropertyName = "UnidadMedida";
            this.UnidadMedida.HeaderText = "UM";
            this.UnidadMedida.Name = "UnidadMedida";
            this.UnidadMedida.Width = 50;
            // 
            // btnCerrar
            // 
            this.btnCerrar.AutoSize = true;
            this.btnCerrar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.DimGray;
            this.btnCerrar.Location = new System.Drawing.Point(12, 9);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(20, 20);
            this.btnCerrar.TabIndex = 44;
            this.btnCerrar.Text = "X";
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSeleccionar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnSeleccionar.FlatAppearance.BorderSize = 0;
            this.btnSeleccionar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnSeleccionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeleccionar.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeleccionar.ForeColor = System.Drawing.Color.White;
            this.btnSeleccionar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSeleccionar.Location = new System.Drawing.Point(967, 119);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(180, 35);
            this.btnSeleccionar.TabIndex = 43;
            this.btnSeleccionar.Text = "Seleccionar";
            this.btnSeleccionar.UseVisualStyleBackColor = false;
            this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click);
            // 
            // checkBoxProdExistBaja
            // 
            this.checkBoxProdExistBaja.AutoSize = true;
            this.checkBoxProdExistBaja.Checked = true;
            this.checkBoxProdExistBaja.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxProdExistBaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.checkBoxProdExistBaja.Location = new System.Drawing.Point(962, 66);
            this.checkBoxProdExistBaja.Name = "checkBoxProdExistBaja";
            this.checkBoxProdExistBaja.Size = new System.Drawing.Size(196, 22);
            this.checkBoxProdExistBaja.TabIndex = 93;
            this.checkBoxProdExistBaja.Text = "Prod. con Existencia Baja";
            this.checkBoxProdExistBaja.UseVisualStyleBackColor = true;
            this.checkBoxProdExistBaja.CheckedChanged += new System.EventHandler(this.checkBoxProdExistBaja_CheckedChanged);
            // 
            // txtFiltro
            // 
            this.txtFiltro.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFiltro.ForeColor = System.Drawing.Color.Gray;
            this.txtFiltro.Location = new System.Drawing.Point(562, 21);
            this.txtFiltro.Name = "txtFiltro";
            this.txtFiltro.Size = new System.Drawing.Size(213, 24);
            this.txtFiltro.TabIndex = 95;
            this.txtFiltro.Text = "Escriba para filtrar...";
            this.txtFiltro.TextChanged += new System.EventHandler(this.txtFiltro_TextChanged);
            this.txtFiltro.Enter += new System.EventHandler(this.txtFiltro_Enter);
            this.txtFiltro.Leave += new System.EventHandler(this.txtFiltro_Leave);
            // 
            // cbFiltro
            // 
            this.cbFiltro.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbFiltro.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbFiltro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFiltro.FormattingEnabled = true;
            this.cbFiltro.Location = new System.Drawing.Point(774, 21);
            this.cbFiltro.Name = "cbFiltro";
            this.cbFiltro.Size = new System.Drawing.Size(161, 24);
            this.cbFiltro.TabIndex = 94;
            this.cbFiltro.Validating += new System.ComponentModel.CancelEventHandler(this.cbFiltro_Validating);
            // 
            // ProductosPorProveedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 700);
            this.Controls.Add(this.txtFiltro);
            this.Controls.Add(this.cbFiltro);
            this.Controls.Add(this.checkBoxProdExistBaja);
            this.Controls.Add(this.btnDesmarcarTodos);
            this.Controls.Add(this.btnMarcarTodos);
            this.Controls.Add(this.dgvProductos);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnSeleccionar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProductosPorProveedor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProductosPorProveedor";
            this.Load += new System.EventHandler(this.ProductosPorProveedor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDesmarcarTodos;
        private System.Windows.Forms.Button btnMarcarTodos;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.Label btnCerrar;
        private System.Windows.Forms.Button btnSeleccionar;
        private System.Windows.Forms.CheckBox checkBoxProdExistBaja;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Seleccionar;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductoID;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnidadMedida;
        private System.Windows.Forms.TextBox txtFiltro;
        private System.Windows.Forms.ComboBox cbFiltro;
    }
}