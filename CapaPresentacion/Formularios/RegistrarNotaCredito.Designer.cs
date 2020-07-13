namespace CapaPresentacion.Formularios
{
    partial class RegistrarNotaCredito
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistrarNotaCredito));
            this.BarraTitulo = new System.Windows.Forms.Panel();
            this.pbBarraMenu = new System.Windows.Forms.PictureBox();
            this.labelBarraMenu = new System.Windows.Forms.Label();
            this.iconcerrar = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbFacturasAAplicar = new System.Windows.Forms.ComboBox();
            this.txtMontoFactura = new System.Windows.Forms.TextBox();
            this.txtFechaFactura = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBalancePendiente = new System.Windows.Forms.TextBox();
            this.cbFacturas = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbClientes = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnSeleccionarProd = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxITBIS = new System.Windows.Forms.CheckBox();
            this.txtCantProdDevueltos = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtValorNotaCredito = new System.Windows.Forms.TextBox();
            this.txtValorAplicarNotaCredito = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.BarraTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBarraMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconcerrar)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
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
            this.BarraTitulo.Size = new System.Drawing.Size(745, 36);
            this.BarraTitulo.TabIndex = 7;
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
            this.labelBarraMenu.Size = new System.Drawing.Size(193, 20);
            this.labelBarraMenu.TabIndex = 2;
            this.labelBarraMenu.Text = "Registrar  Nota de Credito";
            // 
            // iconcerrar
            // 
            this.iconcerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconcerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconcerrar.Image = ((System.Drawing.Image)(resources.GetObject("iconcerrar.Image")));
            this.iconcerrar.Location = new System.Drawing.Point(715, 9);
            this.iconcerrar.Name = "iconcerrar";
            this.iconcerrar.Size = new System.Drawing.Size(18, 18);
            this.iconcerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconcerrar.TabIndex = 1;
            this.iconcerrar.TabStop = false;
            this.iconcerrar.Click += new System.EventHandler(this.iconcerrar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbFacturasAAplicar);
            this.groupBox1.Controls.Add(this.txtMontoFactura);
            this.groupBox1.Controls.Add(this.txtFechaFactura);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtBalancePendiente);
            this.groupBox1.Controls.Add(this.cbFacturas);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbClientes);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(721, 160);
            this.groupBox1.TabIndex = 82;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Factura";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label6.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(428, 115);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 20);
            this.label6.TabIndex = 88;
            this.label6.Text = "Balance Pendiente";
            // 
            // cbFacturasAAplicar
            // 
            this.cbFacturasAAplicar.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbFacturasAAplicar.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbFacturasAAplicar.BackColor = System.Drawing.SystemColors.Window;
            this.cbFacturasAAplicar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFacturasAAplicar.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cbFacturasAAplicar.FormattingEnabled = true;
            this.cbFacturasAAplicar.Location = new System.Drawing.Point(585, 30);
            this.cbFacturasAAplicar.Name = "cbFacturasAAplicar";
            this.cbFacturasAAplicar.Size = new System.Drawing.Size(127, 26);
            this.cbFacturasAAplicar.TabIndex = 82;
            this.cbFacturasAAplicar.SelectionChangeCommitted += new System.EventHandler(this.cbFacturasAAplicar_SelectionChangeCommitted);
            this.cbFacturasAAplicar.Validating += new System.ComponentModel.CancelEventHandler(this.cbFacturasAAplicar_Validating);
            // 
            // txtMontoFactura
            // 
            this.txtMontoFactura.BackColor = System.Drawing.SystemColors.Info;
            this.txtMontoFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMontoFactura.Location = new System.Drawing.Point(585, 71);
            this.txtMontoFactura.Name = "txtMontoFactura";
            this.txtMontoFactura.ReadOnly = true;
            this.txtMontoFactura.Size = new System.Drawing.Size(130, 24);
            this.txtMontoFactura.TabIndex = 85;
            this.txtMontoFactura.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtFechaFactura
            // 
            this.txtFechaFactura.BackColor = System.Drawing.SystemColors.Info;
            this.txtFechaFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFechaFactura.Location = new System.Drawing.Point(156, 109);
            this.txtFechaFactura.Name = "txtFechaFactura";
            this.txtFechaFactura.ReadOnly = true;
            this.txtFechaFactura.Size = new System.Drawing.Size(256, 24);
            this.txtFechaFactura.TabIndex = 83;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(431, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 20);
            this.label3.TabIndex = 81;
            this.label3.Text = "Factura a aplicar";
            // 
            // txtBalancePendiente
            // 
            this.txtBalancePendiente.BackColor = System.Drawing.SystemColors.Info;
            this.txtBalancePendiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBalancePendiente.Location = new System.Drawing.Point(585, 113);
            this.txtBalancePendiente.Name = "txtBalancePendiente";
            this.txtBalancePendiente.ReadOnly = true;
            this.txtBalancePendiente.Size = new System.Drawing.Size(130, 24);
            this.txtBalancePendiente.TabIndex = 87;
            this.txtBalancePendiente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cbFacturas
            // 
            this.cbFacturas.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbFacturas.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbFacturas.BackColor = System.Drawing.SystemColors.Window;
            this.cbFacturas.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFacturas.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cbFacturas.FormattingEnabled = true;
            this.cbFacturas.Location = new System.Drawing.Point(156, 71);
            this.cbFacturas.Name = "cbFacturas";
            this.cbFacturas.Size = new System.Drawing.Size(127, 26);
            this.cbFacturas.TabIndex = 80;
            this.cbFacturas.SelectionChangeCommitted += new System.EventHandler(this.cbFacturas_SelectionChangeCommitted);
            this.cbFacturas.Validating += new System.ComponentModel.CancelEventHandler(this.cbFacturas_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(16, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 20);
            this.label4.TabIndex = 84;
            this.label4.Text = "Fecha Factura";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(16, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 79;
            this.label1.Text = "Factura";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label5.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(431, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 20);
            this.label5.TabIndex = 86;
            this.label5.Text = "Monto";
            // 
            // cbClientes
            // 
            this.cbClientes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbClientes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbClientes.BackColor = System.Drawing.SystemColors.Window;
            this.cbClientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbClientes.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cbClientes.FormattingEnabled = true;
            this.cbClientes.Location = new System.Drawing.Point(156, 30);
            this.cbClientes.Name = "cbClientes";
            this.cbClientes.Size = new System.Drawing.Size(256, 26);
            this.cbClientes.TabIndex = 78;
            this.cbClientes.SelectionChangeCommitted += new System.EventHandler(this.cbClientes_SelectionChangeCommitted);
            this.cbClientes.Validating += new System.ComponentModel.CancelEventHandler(this.cbClientes_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(16, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 20);
            this.label2.TabIndex = 77;
            this.label2.Text = "Cliente";
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
            this.btnGuardar.Location = new System.Drawing.Point(465, 292);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(180, 35);
            this.btnGuardar.TabIndex = 120;
            this.btnGuardar.Text = "Registrar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnSeleccionarProd
            // 
            this.btnSeleccionarProd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSeleccionarProd.BackColor = System.Drawing.Color.ForestGreen;
            this.btnSeleccionarProd.FlatAppearance.BorderSize = 0;
            this.btnSeleccionarProd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnSeleccionarProd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeleccionarProd.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeleccionarProd.ForeColor = System.Drawing.Color.White;
            this.btnSeleccionarProd.Image = ((System.Drawing.Image)(resources.GetObject("btnSeleccionarProd.Image")));
            this.btnSeleccionarProd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSeleccionarProd.Location = new System.Drawing.Point(479, 214);
            this.btnSeleccionarProd.Name = "btnSeleccionarProd";
            this.btnSeleccionarProd.Size = new System.Drawing.Size(142, 50);
            this.btnSeleccionarProd.TabIndex = 121;
            this.btnSeleccionarProd.Text = "Seleccionar Productos";
            this.btnSeleccionarProd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSeleccionarProd.UseVisualStyleBackColor = false;
            this.btnSeleccionarProd.Click += new System.EventHandler(this.btnSeleccionarProd_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxITBIS);
            this.groupBox3.Controls.Add(this.txtCantProdDevueltos);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtValorNotaCredito);
            this.groupBox3.Controls.Add(this.txtValorAplicarNotaCredito);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 208);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(343, 192);
            this.groupBox3.TabIndex = 89;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Datos Nota de Credito";
            // 
            // checkBoxITBIS
            // 
            this.checkBoxITBIS.AutoSize = true;
            this.checkBoxITBIS.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxITBIS.Checked = true;
            this.checkBoxITBIS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxITBIS.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.checkBoxITBIS.Location = new System.Drawing.Point(20, 71);
            this.checkBoxITBIS.Name = "checkBoxITBIS";
            this.checkBoxITBIS.Size = new System.Drawing.Size(70, 22);
            this.checkBoxITBIS.TabIndex = 122;
            this.checkBoxITBIS.Text = "ITBIS?";
            this.checkBoxITBIS.UseVisualStyleBackColor = true;
            this.checkBoxITBIS.CheckedChanged += new System.EventHandler(this.checkBoxITBIS_CheckedChanged);
            // 
            // txtCantProdDevueltos
            // 
            this.txtCantProdDevueltos.BackColor = System.Drawing.SystemColors.Info;
            this.txtCantProdDevueltos.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantProdDevueltos.Location = new System.Drawing.Point(204, 32);
            this.txtCantProdDevueltos.Name = "txtCantProdDevueltos";
            this.txtCantProdDevueltos.ReadOnly = true;
            this.txtCantProdDevueltos.Size = new System.Drawing.Size(103, 24);
            this.txtCantProdDevueltos.TabIndex = 89;
            this.txtCantProdDevueltos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label10.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(17, 34);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(161, 20);
            this.label10.TabIndex = 90;
            this.label10.Text = "Productos Devueltos";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label7.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(16, 152);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(117, 20);
            this.label7.TabIndex = 88;
            this.label7.Text = "Valor a aplicar";
            // 
            // txtValorNotaCredito
            // 
            this.txtValorNotaCredito.BackColor = System.Drawing.SystemColors.Info;
            this.txtValorNotaCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorNotaCredito.Location = new System.Drawing.Point(180, 108);
            this.txtValorNotaCredito.Name = "txtValorNotaCredito";
            this.txtValorNotaCredito.ReadOnly = true;
            this.txtValorNotaCredito.Size = new System.Drawing.Size(157, 24);
            this.txtValorNotaCredito.TabIndex = 85;
            this.txtValorNotaCredito.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtValorAplicarNotaCredito
            // 
            this.txtValorAplicarNotaCredito.BackColor = System.Drawing.Color.White;
            this.txtValorAplicarNotaCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorAplicarNotaCredito.Location = new System.Drawing.Point(180, 150);
            this.txtValorAplicarNotaCredito.Name = "txtValorAplicarNotaCredito";
            this.txtValorAplicarNotaCredito.Size = new System.Drawing.Size(157, 24);
            this.txtValorAplicarNotaCredito.TabIndex = 87;
            this.txtValorAplicarNotaCredito.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label9.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(16, 110);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 20);
            this.label9.TabIndex = 86;
            this.label9.Text = "Valor";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLimpiar.BackColor = System.Drawing.Color.Firebrick;
            this.btnLimpiar.FlatAppearance.BorderSize = 0;
            this.btnLimpiar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Image = ((System.Drawing.Image)(resources.GetObject("btnLimpiar.Image")));
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(465, 360);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnLimpiar.Size = new System.Drawing.Size(180, 35);
            this.btnLimpiar.TabIndex = 122;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // RegistrarNotaCredito
            // 
            this.AcceptButton = this.btnGuardar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(745, 421);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnSeleccionarProd);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BarraTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RegistrarNotaCredito";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RegistrarNotaCredito";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RegistrarNotaCredito_FormClosing);
            this.Load += new System.EventHandler(this.RegistrarNotaCredito_Load);
            this.BarraTitulo.ResumeLayout(false);
            this.BarraTitulo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBarraMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconcerrar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel BarraTitulo;
        private System.Windows.Forms.PictureBox pbBarraMenu;
        private System.Windows.Forms.Label labelBarraMenu;
        private System.Windows.Forms.PictureBox iconcerrar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbFacturasAAplicar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbFacturas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbClientes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMontoFactura;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFechaFactura;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBalancePendiente;
        private System.Windows.Forms.Button btnSeleccionarProd;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtCantProdDevueltos;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtValorNotaCredito;
        private System.Windows.Forms.TextBox txtValorAplicarNotaCredito;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox checkBoxITBIS;
        private System.Windows.Forms.Button btnLimpiar;
    }
}