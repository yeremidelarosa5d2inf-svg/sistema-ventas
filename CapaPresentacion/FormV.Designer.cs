namespace CapaPresentacion
{
    partial class FormV
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormV));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grpAgregar = new System.Windows.Forms.GroupBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.lbTotal = new System.Windows.Forms.Label();
            this.btnFacturar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxProducto = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxCliente = new System.Windows.Forms.ComboBox();
            this.grpEliminar = new System.Windows.Forms.GroupBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.txtVenta = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.dvgVenta = new System.Windows.Forms.DataGridView();
            this.vw_detalle_ventaTableAdapter1 = new CapaPresentacion.DataSetVentaTableAdapters.vw_detalle_ventaTableAdapter();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.grpAgregar.SuspendLayout();
            this.grpEliminar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgVenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Mongolian Baiti", 36F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(767, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(396, 77);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tabla Venta";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(122, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Cliente:";
            // 
            // grpAgregar
            // 
            this.grpAgregar.Controls.Add(this.txtTotal);
            this.grpAgregar.Controls.Add(this.lbTotal);
            this.grpAgregar.Controls.Add(this.btnFacturar);
            this.grpAgregar.Controls.Add(this.btnAgregar);
            this.grpAgregar.Controls.Add(this.txtCantidad);
            this.grpAgregar.Controls.Add(this.label4);
            this.grpAgregar.Controls.Add(this.cbxProducto);
            this.grpAgregar.Controls.Add(this.label3);
            this.grpAgregar.Controls.Add(this.cbxCliente);
            this.grpAgregar.Controls.Add(this.label2);
            this.grpAgregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.grpAgregar.Location = new System.Drawing.Point(26, 9);
            this.grpAgregar.Name = "grpAgregar";
            this.grpAgregar.Size = new System.Drawing.Size(538, 268);
            this.grpAgregar.TabIndex = 2;
            this.grpAgregar.TabStop = false;
            // 
            // txtTotal
            // 
            this.txtTotal.Enabled = false;
            this.txtTotal.Location = new System.Drawing.Point(245, 151);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(95, 26);
            this.txtTotal.TabIndex = 10;
            this.txtTotal.TextChanged += new System.EventHandler(this.txtTotal_TextChanged);
            // 
            // lbTotal
            // 
            this.lbTotal.AutoSize = true;
            this.lbTotal.Location = new System.Drawing.Point(140, 154);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Size = new System.Drawing.Size(54, 20);
            this.lbTotal.TabIndex = 9;
            this.lbTotal.Text = "Total:";
            this.lbTotal.Click += new System.EventHandler(this.label6_Click);
            // 
            // btnFacturar
            // 
            this.btnFacturar.BackColor = System.Drawing.Color.Purple;
            this.btnFacturar.Enabled = false;
            this.btnFacturar.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnFacturar.Location = new System.Drawing.Point(329, 204);
            this.btnFacturar.Name = "btnFacturar";
            this.btnFacturar.Size = new System.Drawing.Size(131, 35);
            this.btnFacturar.TabIndex = 8;
            this.btnFacturar.Text = "Facturar";
            this.btnFacturar.UseVisualStyleBackColor = false;
            this.btnFacturar.Click += new System.EventHandler(this.btnFacturar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnAgregar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAgregar.Location = new System.Drawing.Point(112, 204);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(131, 35);
            this.btnAgregar.TabIndex = 7;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(245, 103);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(169, 26);
            this.txtCantidad.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(122, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Cantidad:";
            // 
            // cbxProducto
            // 
            this.cbxProducto.FormattingEnabled = true;
            this.cbxProducto.Location = new System.Drawing.Point(231, 62);
            this.cbxProducto.Name = "cbxProducto";
            this.cbxProducto.Size = new System.Drawing.Size(174, 28);
            this.cbxProducto.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(122, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Producto:";
            // 
            // cbxCliente
            // 
            this.cbxCliente.FormattingEnabled = true;
            this.cbxCliente.Location = new System.Drawing.Point(231, 17);
            this.cbxCliente.Name = "cbxCliente";
            this.cbxCliente.Size = new System.Drawing.Size(174, 28);
            this.cbxCliente.TabIndex = 2;
            // 
            // grpEliminar
            // 
            this.grpEliminar.Controls.Add(this.btnEliminar);
            this.grpEliminar.Controls.Add(this.txtVenta);
            this.grpEliminar.Controls.Add(this.label5);
            this.grpEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.grpEliminar.Location = new System.Drawing.Point(26, 283);
            this.grpEliminar.Name = "grpEliminar";
            this.grpEliminar.Size = new System.Drawing.Size(538, 123);
            this.grpEliminar.TabIndex = 9;
            this.grpEliminar.TabStop = false;
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnEliminar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEliminar.Location = new System.Drawing.Point(209, 73);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(131, 35);
            this.btnEliminar.TabIndex = 7;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // txtVenta
            // 
            this.txtVenta.Location = new System.Drawing.Point(329, 31);
            this.txtVenta.Name = "txtVenta";
            this.txtVenta.Size = new System.Drawing.Size(169, 26);
            this.txtVenta.TabIndex = 6;
            this.txtVenta.TextChanged += new System.EventHandler(this.txtVenta_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(255, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "Seleccione o Ingrese la Venta:";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnLimpiar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLimpiar.Location = new System.Drawing.Point(433, 431);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(131, 35);
            this.btnLimpiar.TabIndex = 8;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // dvgVenta
            // 
            this.dvgVenta.AllowUserToAddRows = false;
            this.dvgVenta.AllowUserToDeleteRows = false;
            this.dvgVenta.AllowUserToResizeColumns = false;
            this.dvgVenta.AllowUserToResizeRows = false;
            this.dvgVenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgVenta.Location = new System.Drawing.Point(582, 92);
            this.dvgVenta.Name = "dvgVenta";
            this.dvgVenta.RowHeadersWidth = 62;
            this.dvgVenta.RowTemplate.Height = 28;
            this.dvgVenta.Size = new System.Drawing.Size(697, 311);
            this.dvgVenta.TabIndex = 11;
            // 
            // vw_detalle_ventaTableAdapter1
            // 
            this.vw_detalle_ventaTableAdapter1.ClearBeforeFill = true;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(685, 9);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(90, 77);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 12;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // FormV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1359, 478);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.dvgVenta);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.grpEliminar);
            this.Controls.Add(this.grpAgregar);
            this.Controls.Add(this.label1);
            this.Name = "FormV";
            this.Text = "FormV";
            this.Load += new System.EventHandler(this.FormV_Load);
            this.grpAgregar.ResumeLayout(false);
            this.grpAgregar.PerformLayout();
            this.grpEliminar.ResumeLayout(false);
            this.grpEliminar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgVenta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpAgregar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxProducto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxCliente;
        private System.Windows.Forms.Button btnFacturar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.GroupBox grpEliminar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.TextBox txtVenta;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.DataGridView dvgVenta;
        private DataSetVentaTableAdapters.vw_detalle_ventaTableAdapter vw_detalle_ventaTableAdapter1;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label lbTotal;
        private System.Windows.Forms.PictureBox pictureBox4;
    }
}