namespace CapaPresentacion.Formularios
{
    partial class CuentasPorCobrar
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnConsultaFactura = new System.Windows.Forms.Button();
            this.dgvLineasCreditoVenta = new System.Windows.Forms.DataGridView();
            this.btnCerrar = new System.Windows.Forms.Label();
            this.btnRealizarCobro = new System.Windows.Forms.Button();
            this.btnHistorialCobros = new System.Windows.Forms.Button();
            this.txtFiltro = new System.Windows.Forms.TextBox();
            this.cbFiltro = new System.Windows.Forms.ComboBox();
            this.LineaCreditoVentaID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Completado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLineasCreditoVenta)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConsultaFactura
            // 
            this.btnConsultaFactura.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConsultaFactura.BackColor = System.Drawing.Color.ForestGreen;
            this.btnConsultaFactura.FlatAppearance.BorderSize = 0;
            this.btnConsultaFactura.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnConsultaFactura.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultaFactura.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsultaFactura.ForeColor = System.Drawing.Color.White;
            this.btnConsultaFactura.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsultaFactura.Location = new System.Drawing.Point(963, 129);
            this.btnConsultaFactura.Name = "btnConsultaFactura";
            this.btnConsultaFactura.Size = new System.Drawing.Size(180, 35);
            this.btnConsultaFactura.TabIndex = 46;
            this.btnConsultaFactura.Text = "Consultar Factura";
            this.btnConsultaFactura.UseVisualStyleBackColor = false;
            this.btnConsultaFactura.Click += new System.EventHandler(this.btnConsultaFactura_Click);
            // 
            // dgvLineasCreditoVenta
            // 
            this.dgvLineasCreditoVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLineasCreditoVenta.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvLineasCreditoVenta.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvLineasCreditoVenta.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvLineasCreditoVenta.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvLineasCreditoVenta.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSeaGreen;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLineasCreditoVenta.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLineasCreditoVenta.ColumnHeadersHeight = 30;
            this.dgvLineasCreditoVenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvLineasCreditoVenta.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LineaCreditoVentaID,
            this.Completado});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLineasCreditoVenta.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLineasCreditoVenta.EnableHeadersVisualStyles = false;
            this.dgvLineasCreditoVenta.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvLineasCreditoVenta.Location = new System.Drawing.Point(38, 67);
            this.dgvLineasCreditoVenta.MultiSelect = false;
            this.dgvLineasCreditoVenta.Name = "dgvLineasCreditoVenta";
            this.dgvLineasCreditoVenta.ReadOnly = true;
            this.dgvLineasCreditoVenta.RowHeadersVisible = false;
            this.dgvLineasCreditoVenta.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLineasCreditoVenta.Size = new System.Drawing.Size(895, 622);
            this.dgvLineasCreditoVenta.TabIndex = 45;
            // 
            // btnCerrar
            // 
            this.btnCerrar.AutoSize = true;
            this.btnCerrar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.DimGray;
            this.btnCerrar.Location = new System.Drawing.Point(10, 8);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(20, 20);
            this.btnCerrar.TabIndex = 44;
            this.btnCerrar.Text = "X";
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnRealizarCobro
            // 
            this.btnRealizarCobro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRealizarCobro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnRealizarCobro.FlatAppearance.BorderSize = 0;
            this.btnRealizarCobro.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnRealizarCobro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRealizarCobro.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRealizarCobro.ForeColor = System.Drawing.Color.White;
            this.btnRealizarCobro.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRealizarCobro.Location = new System.Drawing.Point(963, 67);
            this.btnRealizarCobro.Name = "btnRealizarCobro";
            this.btnRealizarCobro.Size = new System.Drawing.Size(180, 35);
            this.btnRealizarCobro.TabIndex = 43;
            this.btnRealizarCobro.Text = "Realizar Cobro";
            this.btnRealizarCobro.UseVisualStyleBackColor = false;
            this.btnRealizarCobro.Click += new System.EventHandler(this.btnRealizarCobro_Click);
            // 
            // btnHistorialCobros
            // 
            this.btnHistorialCobros.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHistorialCobros.BackColor = System.Drawing.Color.ForestGreen;
            this.btnHistorialCobros.FlatAppearance.BorderSize = 0;
            this.btnHistorialCobros.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnHistorialCobros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistorialCobros.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistorialCobros.ForeColor = System.Drawing.Color.White;
            this.btnHistorialCobros.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHistorialCobros.Location = new System.Drawing.Point(963, 190);
            this.btnHistorialCobros.Name = "btnHistorialCobros";
            this.btnHistorialCobros.Size = new System.Drawing.Size(180, 35);
            this.btnHistorialCobros.TabIndex = 47;
            this.btnHistorialCobros.Text = "Historial de Cobros";
            this.btnHistorialCobros.UseVisualStyleBackColor = false;
            this.btnHistorialCobros.Click += new System.EventHandler(this.btnHistorialCobros_Click);
            // 
            // txtFiltro
            // 
            this.txtFiltro.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFiltro.ForeColor = System.Drawing.Color.Gray;
            this.txtFiltro.Location = new System.Drawing.Point(560, 21);
            this.txtFiltro.Name = "txtFiltro";
            this.txtFiltro.Size = new System.Drawing.Size(213, 24);
            this.txtFiltro.TabIndex = 86;
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
            this.cbFiltro.Location = new System.Drawing.Point(772, 21);
            this.cbFiltro.Name = "cbFiltro";
            this.cbFiltro.Size = new System.Drawing.Size(161, 24);
            this.cbFiltro.TabIndex = 85;
            this.cbFiltro.Validating += new System.ComponentModel.CancelEventHandler(this.cbFiltro_Validating);
            // 
            // LineaCreditoVentaID
            // 
            this.LineaCreditoVentaID.DataPropertyName = "LineaCreditoVentaID";
            this.LineaCreditoVentaID.HeaderText = "ID";
            this.LineaCreditoVentaID.Name = "LineaCreditoVentaID";
            this.LineaCreditoVentaID.ReadOnly = true;
            this.LineaCreditoVentaID.Width = 44;
            // 
            // Completado
            // 
            this.Completado.DataPropertyName = "Completado";
            this.Completado.HeaderText = "Completado";
            this.Completado.Name = "Completado";
            this.Completado.ReadOnly = true;
            this.Completado.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Completado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Completado.ThreeState = true;
            this.Completado.Width = 116;
            // 
            // CuentasPorCobrar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1170, 700);
            this.Controls.Add(this.txtFiltro);
            this.Controls.Add(this.cbFiltro);
            this.Controls.Add(this.btnHistorialCobros);
            this.Controls.Add(this.btnConsultaFactura);
            this.Controls.Add(this.dgvLineasCreditoVenta);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnRealizarCobro);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CuentasPorCobrar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CXC";
            this.Load += new System.EventHandler(this.CuentasPorCobrar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLineasCreditoVenta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnConsultaFactura;
        private System.Windows.Forms.DataGridView dgvLineasCreditoVenta;
        private System.Windows.Forms.Label btnCerrar;
        private System.Windows.Forms.Button btnRealizarCobro;
        private System.Windows.Forms.Button btnHistorialCobros;
        private System.Windows.Forms.TextBox txtFiltro;
        private System.Windows.Forms.ComboBox cbFiltro;
        private System.Windows.Forms.DataGridViewTextBoxColumn LineaCreditoVentaID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Completado;
    }
}