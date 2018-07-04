namespace Configuracion_Service
{
    partial class DescargarEpos
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnok = new System.Windows.Forms.Button();
            this.lblprogress = new System.Windows.Forms.Label();
            this.ptb_load = new System.Windows.Forms.PictureBox();
            this.trabajo = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptb_load)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.PowderBlue;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnok);
            this.panel1.Location = new System.Drawing.Point(0, 68);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(303, 56);
            this.panel1.TabIndex = 0;
            // 
            // btnok
            // 
            this.btnok.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnok.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnok.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnok.Image = global::Configuracion_Service.Properties.Resources.b_active;
            this.btnok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnok.Location = new System.Drawing.Point(98, 12);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(88, 26);
            this.btnok.TabIndex = 1;
            this.btnok.Text = "OK";
            this.btnok.UseVisualStyleBackColor = true;
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // lblprogress
            // 
            this.lblprogress.AutoSize = true;
            this.lblprogress.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblprogress.Location = new System.Drawing.Point(90, 23);
            this.lblprogress.Name = "lblprogress";
            this.lblprogress.Size = new System.Drawing.Size(41, 13);
            this.lblprogress.TabIndex = 5;
            this.lblprogress.Text = "Ready!";
            // 
            // ptb_load
            // 
            this.ptb_load.Location = new System.Drawing.Point(38, 3);
            this.ptb_load.Name = "ptb_load";
            this.ptb_load.Size = new System.Drawing.Size(84, 61);
            this.ptb_load.TabIndex = 3;
            this.ptb_load.TabStop = false;
            // 
            // trabajo
            // 
            this.trabajo.WorkerReportsProgress = true;
            this.trabajo.WorkerSupportsCancellation = true;
            // 
            // DescargarEpos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(302, 120);
            this.Controls.Add(this.lblprogress);
            this.Controls.Add(this.ptb_load);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DescargarEpos";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Descargando E-Pos";
            this.Load += new System.EventHandler(this.DescargarEpos_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ptb_load)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox ptb_load;
        private System.Windows.Forms.Label lblprogress;
        private System.Windows.Forms.Button btnok;
        private System.ComponentModel.BackgroundWorker trabajo;
    }
}