namespace CapaPresentacion.Formularios
{
    partial class TabsFormularioVentas
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabsFormularioVentas));
            this.tabControlVentas = new System.Windows.Forms.TabControl();
            this.tabAgregar = new System.Windows.Forms.TabPage();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabControlVentas.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlVentas
            // 
            this.tabControlVentas.Controls.Add(this.tabAgregar);
            this.tabControlVentas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlVentas.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControlVentas.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.tabControlVentas.ImageList = this.imageList1;
            this.tabControlVentas.Location = new System.Drawing.Point(0, 0);
            this.tabControlVentas.Margin = new System.Windows.Forms.Padding(0);
            this.tabControlVentas.Name = "tabControlVentas";
            this.tabControlVentas.Padding = new System.Drawing.Point(0, 0);
            this.tabControlVentas.SelectedIndex = 0;
            this.tabControlVentas.Size = new System.Drawing.Size(1170, 700);
            this.tabControlVentas.TabIndex = 0;
            this.tabControlVentas.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControlVentas_DrawItem);
            this.tabControlVentas.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.tabControlVentas_ControlRemoved);
            this.tabControlVentas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabControlVentas_MouseClick);
            // 
            // tabAgregar
            // 
            this.tabAgregar.ImageIndex = 1;
            this.tabAgregar.Location = new System.Drawing.Point(4, 29);
            this.tabAgregar.Name = "tabAgregar";
            this.tabAgregar.Padding = new System.Windows.Forms.Padding(3);
            this.tabAgregar.Size = new System.Drawing.Size(1162, 667);
            this.tabAgregar.TabIndex = 0;
            this.tabAgregar.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "lengueta.png");
            this.imageList1.Images.SetKeyName(1, "lengueta (1).png");
            // 
            // TabsFormularioVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1170, 700);
            this.Controls.Add(this.tabControlVentas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TabsFormularioVentas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TabsFormularioVentas";
            this.Load += new System.EventHandler(this.TabsFormularioVentas_Load);
            this.tabControlVentas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlVentas;
        private System.Windows.Forms.TabPage tabAgregar;
        private System.Windows.Forms.ImageList imageList1;
    }
}