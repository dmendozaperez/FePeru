using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppBata_WS_Electronico
{
    public partial class Form1 : Form
    {
        private bool simulateError = false;
        public Form1()
        {
            InitializeComponent();
            this.pictureBox.Image = Properties.Resources.Information;
        }
        private void _loading_procesos()
        {
            //_consulta_dni_ruc_loading(_dni_ruc);
            //_crear_actualiza_dbf_loading();
        }
        void progressReport(Object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is String)
            {
                this.labelProgress.Text = (String)e.UserState;
            }
        }
        void workerComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            // hide animation
            this.pictureBox.Image = null;
            // show result indication
            if (e.Cancelled)
            {
                this.labelProgress.Text = "Operation cancelled by the user!";
                this.pictureBox.Image = Properties.Resources.Warning;
            }
            else
            {
                if (e.Error != null)
                {
                    this.labelProgress.Text = "Operation failed: " + e.Error.Message;
                    this.pictureBox.Image = Properties.Resources.Error;
                }
                else
                {
                    this.labelProgress.Text = "Operation finished successfuly!";
                    this.pictureBox.Image = Properties.Resources.Information;
                }
            }
            // restore button states
            this.btndescargar.Enabled = true;
            this.btncancel.Enabled = false;
            //this.buttonError.Enabled = false;
        }
        private void btndescargar_Click(object sender, EventArgs e)
        {
            // show animated image
            this.pictureBox.Image = Properties.Resources.Animation;
            // change button states
            this.btndescargar.Enabled = false;
            this.btncancel.Enabled = true;
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork+= (obj, ea) => _loading_procesos();
            this.backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(progressReport);
            this.backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerComplete);
            this.backgroundWorker.RunWorkerAsync();

            //this.buttonError.Enabled = true;
            // start background operation
            //this.backgroundWorker.RunWorkerAsync();
            //try
            //{
            //    Bata_Util.ValidateAcceso header_user = new Bata_Util.ValidateAcceso();
            //    header_user.Username = "2B2C9CE8-E8FD-4B9E-BE01-2CFCD588A50E";
            //    header_user.Password = "74B574FC-E6BA-4FF3-BAD6-B4911C2B9FA8";

            //    Bata_Util.Bata_ElectronicoSoapClient bata_dow = new Bata_Util.Bata_ElectronicoSoapClient();

            //    var files = bata_dow.ws_descargar_epos(header_user);


            //}
            //catch (Exception exc)
            //{


            //}
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.backgroundWorker.CancelAsync();
        }
       
        private void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is String)
            {
                this.labelProgress.Text = (String)e.UserState;
            }
        }       

       

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {


            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                if (this.backgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                // report progress
                this.backgroundWorker.ReportProgress(-1, string.Format("Performing step {0}...", i + 1));
                // simulate operation step
                System.Threading.Thread.Sleep(rand.Next(100, 1000));
                if (this.simulateError)
                {
                    this.simulateError = false;
                    throw new Exception("Unexpected error!");
                }
            }
            Bata_Util.ValidateAcceso header_user = new Bata_Util.ValidateAcceso();
            header_user.Username = "2B2C9CE8-E8FD-4B9E-BE01-2CFCD588A50E";
            header_user.Password = "74B574FC-E6BA-4FF3-BAD6-B4911C2B9FA8";

            Bata_Util.Bata_ElectronicoSoapClient bata_dow = new Bata_Util.Bata_ElectronicoSoapClient();

            var files = bata_dow.ws_descargar_epos(header_user);

             string file_epos_default = @"C:\Paperless.zip";
             File.WriteAllBytes(file_epos_default, files.files);
        }
    }
}
