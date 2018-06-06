namespace Configuracion_Service
{
    partial class Config_Service
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Config_Service));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdb_wsdl = new System.Windows.Forms.RadioButton();
            this.rdb_epos = new System.Windows.Forms.RadioButton();
            this.btnejecutar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdb_tropi = new System.Windows.Forms.RadioButton();
            this.rdb_ec = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.rdb_wsdl);
            this.groupBox1.Controls.Add(this.rdb_epos);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(476, 68);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Metodo de Generacion de Facturacion Electronica";
            // 
            // rdb_wsdl
            // 
            this.rdb_wsdl.AutoSize = true;
            this.rdb_wsdl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdb_wsdl.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdb_wsdl.Location = new System.Drawing.Point(310, 30);
            this.rdb_wsdl.Name = "rdb_wsdl";
            this.rdb_wsdl.Size = new System.Drawing.Size(74, 23);
            this.rdb_wsdl.TabIndex = 1;
            this.rdb_wsdl.TabStop = true;
            this.rdb_wsdl.Text = "WSDL";
            this.rdb_wsdl.UseVisualStyleBackColor = true;
            // 
            // rdb_epos
            // 
            this.rdb_epos.AutoSize = true;
            this.rdb_epos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdb_epos.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdb_epos.Location = new System.Drawing.Point(47, 30);
            this.rdb_epos.Name = "rdb_epos";
            this.rdb_epos.Size = new System.Drawing.Size(77, 23);
            this.rdb_epos.TabIndex = 0;
            this.rdb_epos.TabStop = true;
            this.rdb_epos.Text = "E-POS";
            this.rdb_epos.UseVisualStyleBackColor = true;
            // 
            // btnejecutar
            // 
            this.btnejecutar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnejecutar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnejecutar.Image = global::Configuracion_Service.Properties.Resources.b_active;
            this.btnejecutar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnejecutar.Location = new System.Drawing.Point(166, 163);
            this.btnejecutar.Name = "btnejecutar";
            this.btnejecutar.Size = new System.Drawing.Size(109, 31);
            this.btnejecutar.TabIndex = 2;
            this.btnejecutar.Text = "EJECUTAR";
            this.btnejecutar.UseVisualStyleBackColor = true;
            this.btnejecutar.Click += new System.EventHandler(this.btnejecutar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.rdb_tropi);
            this.groupBox2.Controls.Add(this.rdb_ec);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 80);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(476, 68);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Empresa";
            // 
            // rdb_tropi
            // 
            this.rdb_tropi.AutoSize = true;
            this.rdb_tropi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdb_tropi.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdb_tropi.ForeColor = System.Drawing.Color.Maroon;
            this.rdb_tropi.Location = new System.Drawing.Point(310, 30);
            this.rdb_tropi.Name = "rdb_tropi";
            this.rdb_tropi.Size = new System.Drawing.Size(134, 23);
            this.rdb_tropi.TabIndex = 1;
            this.rdb_tropi.TabStop = true;
            this.rdb_tropi.Text = "TROPICALZA";
            this.rdb_tropi.UseVisualStyleBackColor = true;
            // 
            // rdb_ec
            // 
            this.rdb_ec.AutoSize = true;
            this.rdb_ec.BackColor = System.Drawing.Color.Transparent;
            this.rdb_ec.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rdb_ec.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdb_ec.ForeColor = System.Drawing.Color.Maroon;
            this.rdb_ec.Location = new System.Drawing.Point(47, 30);
            this.rdb_ec.Name = "rdb_ec";
            this.rdb_ec.Size = new System.Drawing.Size(110, 23);
            this.rdb_ec.TabIndex = 0;
            this.rdb_ec.TabStop = true;
            this.rdb_ec.Text = "EMCOMER";
            this.rdb_ec.UseVisualStyleBackColor = false;
            // 
            // Config_Service
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(501, 209);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnejecutar);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Config_Service";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuracion de Servicio";
            this.Load += new System.EventHandler(this.Config_Service_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdb_wsdl;
        private System.Windows.Forms.RadioButton rdb_epos;
        private System.Windows.Forms.Button btnejecutar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdb_tropi;
        private System.Windows.Forms.RadioButton rdb_ec;
    }
}