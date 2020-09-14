namespace CapaPresentacion.Formularios
{
    partial class CuentasPorPagar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CuentasPorPagar));
            this.btnHistorialPagos = new System.Windows.Forms.Button();
            this.btnConsultaFactura = new System.Windows.Forms.Button();
            this.dgvLineasCreditoCompra = new System.Windows.Forms.DataGridView();
            this.LineaCreditoCompraID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Proveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrdenCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FacturaCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MontoFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BalancePendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Completado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnCerrar = new System.Windows.Forms.Label();
            this.btnRealizarPago = new System.Windows.Forms.Button();
            this.txtFiltro = new System.Windows.Forms.TextBox();
            this.cbFiltro = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.btnRealizarBusqueda = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLineasCreditoCompra)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnHistorialPagos
            // 
            this.btnHistorialPagos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHistorialPagos.BackColor = System.Drawing.Color.ForestGreen;
            this.btnHistorialPagos.FlatAppearance.BorderSize = 0;
            this.btnHistorialPagos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnHistorialPagos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistorialPagos.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistorialPagos.ForeColor = System.Drawing.Color.White;
            this.btnHistorialPagos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHistorialPagos.Location = new System.Drawing.Point(905, 189);
            this.btnHistorialPagos.Name = "btnHistorialPagos";
            this.btnHistorialPagos.Size = new System.Drawing.Size(180, 35);
            this.btnHistorialPagos.TabIndex = 52;
            this.btnHistorialPagos.Text = "Historial de Pagos";
            this.btnHistorialPagos.UseVisualStyleBackColor = false;
            this.btnHistorialPagos.Click += new System.EventHandler(this.btnHistorialPagos_Click);
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
            this.btnConsultaFactura.Location = new System.Drawing.Point(905, 128);
            this.btnConsultaFactura.Name = "btnConsultaFactura";
            this.btnConsultaFactura.Size = new System.Drawing.Size(180, 35);
            this.btnConsultaFactura.TabIndex = 51;
            this.btnConsultaFactura.Text = "Consultar Factura";
            this.btnConsultaFactura.UseVisualStyleBackColor = false;
            this.btnConsultaFactura.Click += new System.EventHandler(this.btnConsultaFactura_Click);
            // 
            // dgvLineasCreditoCompra
            // 
            this.dgvLineasCreditoCompra.AllowUserToAddRows = false;
            this.dgvLineasCreditoCompra.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLineasCreditoCompra.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvLineasCreditoCompra.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvLineasCreditoCompra.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvLineasCreditoCompra.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvLineasCreditoCompra.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSeaGreen;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLineasCreditoCompra.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLineasCreditoCompra.ColumnHeadersHeight = 30;
            this.dgvLineasCreditoCompra.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvLineasCreditoCompra.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LineaCreditoCompraID,
            this.Proveedor,
            this.OrdenCompra,
            this.Fecha,
            this.FacturaCompra,
            this.MontoFactura,
            this.BalancePendiente,
            this.Completado});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLineasCreditoCompra.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLineasCreditoCompra.EnableHeadersVisualStyles = false;
            this.dgvLineasCreditoCompra.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvLineasCreditoCompra.Location = new System.Drawing.Point(40, 66);
            this.dgvLineasCreditoCompra.MultiSelect = false;
            this.dgvLineasCreditoCompra.Name = "dgvLineasCreditoCompra";
            this.dgvLineasCreditoCompra.ReadOnly = true;
            this.dgvLineasCreditoCompra.RowHeadersVisible = false;
            this.dgvLineasCreditoCompra.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLineasCreditoCompra.Size = new System.Drawing.Size(835, 524);
            this.dgvLineasCreditoCompra.TabIndex = 50;
            this.dgvLineasCreditoCompra.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLineasCreditoCompra_CellContentClick);
            this.dgvLineasCreditoCompra.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgvLineasCreditoCompra_Scroll);
            // 
            // LineaCreditoCompraID
            // 
            this.LineaCreditoCompraID.DataPropertyName = "LineaCreditoCompraID";
            this.LineaCreditoCompraID.HeaderText = "ID";
            this.LineaCreditoCompraID.Name = "LineaCreditoCompraID";
            this.LineaCreditoCompraID.ReadOnly = true;
            this.LineaCreditoCompraID.Width = 44;
            // 
            // Proveedor
            // 
            this.Proveedor.DataPropertyName = "Proveedor";
            this.Proveedor.HeaderText = "Proveedor";
            this.Proveedor.Name = "Proveedor";
            this.Proveedor.ReadOnly = true;
            this.Proveedor.Width = 98;
            // 
            // OrdenCompra
            // 
            this.OrdenCompra.DataPropertyName = "OrdenCompra";
            this.OrdenCompra.HeaderText = "OrdenCompra";
            this.OrdenCompra.Name = "OrdenCompra";
            this.OrdenCompra.ReadOnly = true;
            this.OrdenCompra.Width = 126;
            // 
            // Fecha
            // 
            this.Fecha.DataPropertyName = "Fecha";
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            this.Fecha.Width = 70;
            // 
            // FacturaCompra
            // 
            this.FacturaCompra.DataPropertyName = "FacturaCompra";
            this.FacturaCompra.HeaderText = "FacturaCompra";
            this.FacturaCompra.Name = "FacturaCompra";
            this.FacturaCompra.ReadOnly = true;
            this.FacturaCompra.Width = 135;
            // 
            // MontoFactura
            // 
            this.MontoFactura.DataPropertyName = "MontoFactura";
            this.MontoFactura.HeaderText = "MontoFactura";
            this.MontoFactura.Name = "MontoFactura";
            this.MontoFactura.ReadOnly = true;
            this.MontoFactura.Width = 122;
            // 
            // BalancePendiente
            // 
            this.BalancePendiente.DataPropertyName = "BalancePendiente";
            this.BalancePendiente.HeaderText = "BalancePendiente";
            this.BalancePendiente.Name = "BalancePendiente";
            this.BalancePendiente.ReadOnly = true;
            this.BalancePendiente.Width = 148;
            // 
            // Completado
            // 
            this.Completado.DataPropertyName = "Completado";
            this.Completado.HeaderText = "Completado";
            this.Completado.Name = "Completado";
            this.Completado.ReadOnly = true;
            this.Completado.Width = 97;
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
            this.btnCerrar.TabIndex = 49;
            this.btnCerrar.Text = "X";
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnRealizarPago
            // 
            this.btnRealizarPago.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRealizarPago.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnRealizarPago.FlatAppearance.BorderSize = 0;
            this.btnRealizarPago.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnRealizarPago.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRealizarPago.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRealizarPago.ForeColor = System.Drawing.Color.White;
            this.btnRealizarPago.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRealizarPago.Location = new System.Drawing.Point(905, 66);
            this.btnRealizarPago.Name = "btnRealizarPago";
            this.btnRealizarPago.Size = new System.Drawing.Size(180, 35);
            this.btnRealizarPago.TabIndex = 48;
            this.btnRealizarPago.Text = "Realizar Pago";
            this.btnRealizarPago.UseVisualStyleBackColor = false;
            this.btnRealizarPago.Click += new System.EventHandler(this.btnRealizarPago_Click);
            // 
            // txtFiltro
            // 
            this.txtFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFiltro.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFiltro.ForeColor = System.Drawing.Color.Gray;
            this.txtFiltro.Location = new System.Drawing.Point(468, 21);
            this.txtFiltro.Name = "txtFiltro";
            this.txtFiltro.Size = new System.Drawing.Size(213, 24);
            this.txtFiltro.TabIndex = 88;
            this.txtFiltro.Text = "Escriba para filtrar...";
            this.txtFiltro.Enter += new System.EventHandler(this.txtFiltro_Enter);
            this.txtFiltro.Leave += new System.EventHandler(this.txtFiltro_Leave);
            // 
            // cbFiltro
            // 
            this.cbFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbFiltro.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbFiltro.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbFiltro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFiltro.FormattingEnabled = true;
            this.cbFiltro.Location = new System.Drawing.Point(680, 21);
            this.cbFiltro.Name = "cbFiltro";
            this.cbFiltro.Size = new System.Drawing.Size(161, 24);
            this.cbFiltro.TabIndex = 87;
            this.cbFiltro.Validating += new System.ComponentModel.CancelEventHandler(this.cbFiltro_Validating);
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.label20);
            this.groupBox6.Controls.Add(this.label18);
            this.groupBox6.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold);
            this.groupBox6.Location = new System.Drawing.Point(905, 247);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(180, 87);
            this.groupBox6.TabIndex = 117;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "ATAJOS";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.label20.Location = new System.Drawing.Point(11, 24);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(134, 19);
            this.label20.TabIndex = 89;
            this.label20.Text = "F1 = Filtrar Clientes";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.label18.Location = new System.Drawing.Point(11, 52);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(168, 19);
            this.label18.TabIndex = 87;
            this.label18.Text = "F5 = Realizar Busqueda";
            // 
            // btnRealizarBusqueda
            // 
            this.btnRealizarBusqueda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRealizarBusqueda.BackColor = System.Drawing.SystemColors.Window;
            this.btnRealizarBusqueda.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRealizarBusqueda.FlatAppearance.BorderSize = 0;
            this.btnRealizarBusqueda.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnRealizarBusqueda.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRealizarBusqueda.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRealizarBusqueda.ForeColor = System.Drawing.Color.White;
            this.btnRealizarBusqueda.Image = ((System.Drawing.Image)(resources.GetObject("btnRealizarBusqueda.Image")));
            this.btnRealizarBusqueda.Location = new System.Drawing.Point(838, 21);
            this.btnRealizarBusqueda.Name = "btnRealizarBusqueda";
            this.btnRealizarBusqueda.Size = new System.Drawing.Size(37, 24);
            this.btnRealizarBusqueda.TabIndex = 116;
            this.btnRealizarBusqueda.UseVisualStyleBackColor = false;
            this.btnRealizarBusqueda.Click += new System.EventHandler(this.btnRealizarBusqueda_Click);
            // 
            // CuentasPorPagar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1110, 600);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.btnRealizarBusqueda);
            this.Controls.Add(this.txtFiltro);
            this.Controls.Add(this.cbFiltro);
            this.Controls.Add(this.btnHistorialPagos);
            this.Controls.Add(this.btnConsultaFactura);
            this.Controls.Add(this.dgvLineasCreditoCompra);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnRealizarPago);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CuentasPorPagar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CuentasPorPagar";
            this.Load += new System.EventHandler(this.CuentasPorPagar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLineasCreditoCompra)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnHistorialPagos;
        private System.Windows.Forms.Button btnConsultaFactura;
        private System.Windows.Forms.DataGridView dgvLineasCreditoCompra;
        private System.Windows.Forms.Label btnCerrar;
        private System.Windows.Forms.Button btnRealizarPago;
        private System.Windows.Forms.TextBox txtFiltro;
        private System.Windows.Forms.ComboBox cbFiltro;
        private System.Windows.Forms.DataGridViewTextBoxColumn LineaCreditoCompraID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Proveedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrdenCompra;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn FacturaCompra;
        private System.Windows.Forms.DataGridViewTextBoxColumn MontoFactura;
        private System.Windows.Forms.DataGridViewTextBoxColumn BalancePendiente;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Completado;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnRealizarBusqueda;
    }
}