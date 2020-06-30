namespace CapaPresentacion.Formularios
{
    partial class RegistrarCobro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistrarCobro));
            this.BarraTitulo = new System.Windows.Forms.Panel();
            this.pbBarraMenu = new System.Windows.Forms.PictureBox();
            this.labelBarraMenu = new System.Windows.Forms.Label();
            this.iconcerrar = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLineaCreditoVentaID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBalancePendiente = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCobro = new System.Windows.Forms.TextBox();
            this.btnRealizarCobro = new System.Windows.Forms.Button();
            this.BarraTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBarraMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconcerrar)).BeginInit();
            this.groupBox4.SuspendLayout();
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
            this.BarraTitulo.Size = new System.Drawing.Size(395, 36);
            this.BarraTitulo.TabIndex = 6;
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
            this.labelBarraMenu.Size = new System.Drawing.Size(121, 20);
            this.labelBarraMenu.TabIndex = 2;
            this.labelBarraMenu.Text = "Registrar Cobro";
            // 
            // iconcerrar
            // 
            this.iconcerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconcerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconcerrar.Image = ((System.Drawing.Image)(resources.GetObject("iconcerrar.Image")));
            this.iconcerrar.Location = new System.Drawing.Point(365, 9);
            this.iconcerrar.Name = "iconcerrar";
            this.iconcerrar.Size = new System.Drawing.Size(18, 18);
            this.iconcerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconcerrar.TabIndex = 1;
            this.iconcerrar.TabStop = false;
            this.iconcerrar.Click += new System.EventHandler(this.iconcerrar_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.txtLineaCreditoVentaID);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.txtBalancePendiente);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.txtCliente);
            this.groupBox4.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(12, 42);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(371, 160);
            this.groupBox4.TabIndex = 118;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Linea de credito";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(18, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 20);
            this.label2.TabIndex = 40;
            this.label2.Text = "ID";
            // 
            // txtLineaCreditoVentaID
            // 
            this.txtLineaCreditoVentaID.BackColor = System.Drawing.SystemColors.Info;
            this.txtLineaCreditoVentaID.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLineaCreditoVentaID.Location = new System.Drawing.Point(64, 33);
            this.txtLineaCreditoVentaID.Name = "txtLineaCreditoVentaID";
            this.txtLineaCreditoVentaID.ReadOnly = true;
            this.txtLineaCreditoVentaID.Size = new System.Drawing.Size(103, 24);
            this.txtLineaCreditoVentaID.TabIndex = 39;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(18, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 20);
            this.label1.TabIndex = 38;
            this.label1.Text = "Balance Pendiente";
            // 
            // txtBalancePendiente
            // 
            this.txtBalancePendiente.BackColor = System.Drawing.SystemColors.Info;
            this.txtBalancePendiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBalancePendiente.Location = new System.Drawing.Point(173, 117);
            this.txtBalancePendiente.Name = "txtBalancePendiente";
            this.txtBalancePendiente.ReadOnly = true;
            this.txtBalancePendiente.Size = new System.Drawing.Size(174, 24);
            this.txtBalancePendiente.TabIndex = 37;
            this.txtBalancePendiente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label8.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(18, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 20);
            this.label8.TabIndex = 36;
            this.label8.Text = "Cliente";
            // 
            // txtCliente
            // 
            this.txtCliente.BackColor = System.Drawing.SystemColors.Info;
            this.txtCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCliente.Location = new System.Drawing.Point(85, 74);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.ReadOnly = true;
            this.txtCliente.Size = new System.Drawing.Size(262, 24);
            this.txtCliente.TabIndex = 35;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(30, 239);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 20);
            this.label3.TabIndex = 42;
            this.label3.Text = "Monto a Cobrar";
            // 
            // txtCobro
            // 
            this.txtCobro.BackColor = System.Drawing.Color.White;
            this.txtCobro.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCobro.Location = new System.Drawing.Point(164, 237);
            this.txtCobro.Name = "txtCobro";
            this.txtCobro.Size = new System.Drawing.Size(195, 24);
            this.txtCobro.TabIndex = 41;
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
            this.btnRealizarCobro.Location = new System.Drawing.Point(108, 283);
            this.btnRealizarCobro.Name = "btnRealizarCobro";
            this.btnRealizarCobro.Size = new System.Drawing.Size(180, 35);
            this.btnRealizarCobro.TabIndex = 119;
            this.btnRealizarCobro.Text = "Realizar Cobro";
            this.btnRealizarCobro.UseVisualStyleBackColor = false;
            this.btnRealizarCobro.Click += new System.EventHandler(this.btnRealizarCobro_Click);
            // 
            // RegistrarCobro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 330);
            this.Controls.Add(this.btnRealizarCobro);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.txtCobro);
            this.Controls.Add(this.BarraTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RegistrarCobro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RegistrarCobro";
            this.BarraTitulo.ResumeLayout(false);
            this.BarraTitulo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBarraMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconcerrar)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel BarraTitulo;
        private System.Windows.Forms.PictureBox pbBarraMenu;
        private System.Windows.Forms.Label labelBarraMenu;
        private System.Windows.Forms.PictureBox iconcerrar;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLineaCreditoVentaID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBalancePendiente;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCobro;
        private System.Windows.Forms.Button btnRealizarCobro;
    }
}