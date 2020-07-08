namespace CapaPresentacion.Formularios
{
    partial class Facturas
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
            this.btnVerDetalles = new System.Windows.Forms.Button();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.dgvFacturas = new System.Windows.Forms.DataGridView();
            this.FacturaID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClienteID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCerrar = new System.Windows.Forms.Label();
            this.txtFiltro = new System.Windows.Forms.TextBox();
            this.cbFiltro = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas)).BeginInit();
            this.SuspendLayout();
            // 
            // btnVerDetalles
            // 
            this.btnVerDetalles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVerDetalles.BackColor = System.Drawing.Color.ForestGreen;
            this.btnVerDetalles.FlatAppearance.BorderSize = 0;
            this.btnVerDetalles.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnVerDetalles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerDetalles.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerDetalles.ForeColor = System.Drawing.Color.White;
            this.btnVerDetalles.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVerDetalles.Location = new System.Drawing.Point(968, 189);
            this.btnVerDetalles.Name = "btnVerDetalles";
            this.btnVerDetalles.Size = new System.Drawing.Size(180, 35);
            this.btnVerDetalles.TabIndex = 66;
            this.btnVerDetalles.Text = "Ver Detalles";
            this.btnVerDetalles.UseVisualStyleBackColor = false;
            this.btnVerDetalles.Click += new System.EventHandler(this.btnVerDetalles_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnExportar.FlatAppearance.BorderSize = 0;
            this.btnExportar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportar.ForeColor = System.Drawing.Color.White;
            this.btnExportar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportar.Location = new System.Drawing.Point(968, 66);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(180, 35);
            this.btnExportar.TabIndex = 65;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = false;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImprimir.BackColor = System.Drawing.Color.ForestGreen;
            this.btnImprimir.FlatAppearance.BorderSize = 0;
            this.btnImprimir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.ForeColor = System.Drawing.Color.White;
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(968, 126);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(180, 35);
            this.btnImprimir.TabIndex = 64;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // dgvFacturas
            // 
            this.dgvFacturas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFacturas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvFacturas.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvFacturas.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvFacturas.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvFacturas.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSeaGreen;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFacturas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFacturas.ColumnHeadersHeight = 30;
            this.dgvFacturas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvFacturas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FacturaID,
            this.ClienteID});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFacturas.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvFacturas.EnableHeadersVisualStyles = false;
            this.dgvFacturas.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dgvFacturas.Location = new System.Drawing.Point(45, 66);
            this.dgvFacturas.MultiSelect = false;
            this.dgvFacturas.Name = "dgvFacturas";
            this.dgvFacturas.ReadOnly = true;
            this.dgvFacturas.RowHeadersVisible = false;
            this.dgvFacturas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFacturas.Size = new System.Drawing.Size(898, 625);
            this.dgvFacturas.TabIndex = 62;
            // 
            // FacturaID
            // 
            this.FacturaID.DataPropertyName = "FacturaID";
            this.FacturaID.HeaderText = "ID";
            this.FacturaID.Name = "FacturaID";
            this.FacturaID.ReadOnly = true;
            this.FacturaID.Width = 44;
            // 
            // ClienteID
            // 
            this.ClienteID.DataPropertyName = "ClienteID";
            this.ClienteID.HeaderText = "ClienteID";
            this.ClienteID.Name = "ClienteID";
            this.ClienteID.ReadOnly = true;
            this.ClienteID.Visible = false;
            this.ClienteID.Width = 92;
            // 
            // btnCerrar
            // 
            this.btnCerrar.AutoSize = true;
            this.btnCerrar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.DimGray;
            this.btnCerrar.Location = new System.Drawing.Point(17, 10);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(20, 20);
            this.btnCerrar.TabIndex = 61;
            this.btnCerrar.Text = "X";
            // 
            // txtFiltro
            // 
            this.txtFiltro.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFiltro.ForeColor = System.Drawing.Color.Gray;
            this.txtFiltro.Location = new System.Drawing.Point(570, 21);
            this.txtFiltro.Name = "txtFiltro";
            this.txtFiltro.Size = new System.Drawing.Size(213, 24);
            this.txtFiltro.TabIndex = 90;
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
            this.cbFiltro.Location = new System.Drawing.Point(782, 21);
            this.cbFiltro.Name = "cbFiltro";
            this.cbFiltro.Size = new System.Drawing.Size(161, 24);
            this.cbFiltro.TabIndex = 89;
            this.cbFiltro.Validating += new System.ComponentModel.CancelEventHandler(this.cbFiltro_Validating);
            // 
            // Facturas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1170, 700);
            this.Controls.Add(this.txtFiltro);
            this.Controls.Add(this.cbFiltro);
            this.Controls.Add(this.btnVerDetalles);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.dgvFacturas);
            this.Controls.Add(this.btnCerrar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Facturas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Facturas";
            this.Load += new System.EventHandler(this.Facturas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnVerDetalles;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.DataGridView dgvFacturas;
        private System.Windows.Forms.Label btnCerrar;
        private System.Windows.Forms.DataGridViewTextBoxColumn FacturaID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClienteID;
        private System.Windows.Forms.TextBox txtFiltro;
        private System.Windows.Forms.ComboBox cbFiltro;
    }
}