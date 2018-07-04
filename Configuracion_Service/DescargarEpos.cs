
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Configuracion_Service
{
    public partial class DescargarEpos : Form
    {
        private string file_epos_default = @"C:\Paperless.zip";
        private string file_config_paperless = @"C:\Paperless\e-pos\configuracion\0.config";
        private string _tienda = "";string _empresa_ruc = "";
        private string _path_default = @"D:\POS";
        private  string _conexion
        {          
            get { return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _path_default + ";Extended Properties=dBASE IV;"; }
        }
        /// <summary>
        /// captura el codigo de tienda
        /// </summary>
        private  void _parametros_tda()
        {

            OleDbConnection cn = null;
            OleDbCommand cmd = null;
            OleDbDataAdapter da = null;
            DataTable dt = null;
            string sqlquery = "";
            try
            {
                #region<CODIGO DE TIENDA>
                cn = new OleDbConnection(_conexion);               
                sqlquery = "select C_sucu   from FPCTRL02";

                cmd = new OleDbCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                da = new OleDbDataAdapter(cmd);
                dt = new DataTable();               
                da.Fill(dt);
                if (dt != null)
                    _tienda = dt.Rows[0]["C_sucu"].ToString();
                #endregion
                #region<RUC DE EMPRESA>
                sqlquery = "select E_nruc   from FEMPRESA";
                cmd = new OleDbCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                da = new OleDbDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                if (dt != null)
                    _empresa_ruc = dt.Rows[0]["E_nruc"].ToString();
                #endregion
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso del sistema (Bata- Peru)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ejecutar_bat_paperless()
        {
            try
            {
               Process.Start(@"C:\Paperless\e-pos\InstallPPLJPOS-NT.bat");
            }
            catch (Exception exc)
            {

                MessageBox.Show(exc.Message, "Aviso del sistema (Bata- Peru)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void genera_config_paperless()
        {
            try
            {
                if (_tienda.Length>0 && _empresa_ruc.Length>0)
                {
                    StringBuilder str = null;
                    string str_cadena = "";
                    string codificado = "codificado=false";
                    string doctypes = "jpos.doctypes=1,3,7,8";
                    string companies = "jpos.companies=" + _empresa_ruc;
                    string tienda = "jpos.tienda.id=" + _tienda;
                    string id = "jpos.pos.id=0";
                    str = new StringBuilder();

                    str.Append(codificado);
                    str.Append("\r\n");
                    str.Append(doctypes);
                    str.Append("\r\n");
                    str.Append(companies);
                    str.Append("\r\n");
                    str.Append(tienda);
                    str.Append("\r\n");                   
                    str.Append(id);

                    str_cadena = str.ToString();

                    if (File.Exists(@file_config_paperless)) File.Delete(@file_config_paperless);
                    File.WriteAllText(@file_config_paperless, str_cadena);

                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso del sistema (Bata- Peru)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DescargarEpos()
        {
            InitializeComponent();
            ptb_load.Image = Properties.Resources.Information;
        }

        private void DescargarEpos_Load(object sender, EventArgs e)
        {
            ptb_load.Image = Properties.Resources.Animation;
            btnok.Enabled = false;
            trabajo.WorkerReportsProgress = true;
            //trabajo.DoWork += (obj, ea) => _loading_procesos();
            trabajo.DoWork += new DoWorkEventHandler(loading_procesos);
            trabajo.ProgressChanged += new ProgressChangedEventHandler(progressReport);
            trabajo.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerComplete);
            trabajo.RunWorkerAsync();
        }

        private void btnok_Click(object sender, EventArgs e)
        {           
            this.Close();
        }
         void loading_procesos(Object sender, DoWorkEventArgs e)
        {

            try
            {
                trabajo.ReportProgress(-90, string.Format("Descargando desde servidor", 90));
                Bata_Util.ValidateAcceso header_user = new Bata_Util.ValidateAcceso();
                header_user.Username = "2B2C9CE8-E8FD-4B9E-BE01-2CFCD588A50E";
                header_user.Password = "74B574FC-E6BA-4FF3-BAD6-B4911C2B9FA8";

                Bata_Util.Bata_ElectronicoSoapClient bata_dow = new Bata_Util.Bata_ElectronicoSoapClient();

                if (!File.Exists(@file_config_paperless))
                { 
                    var files = bata_dow.ws_descargar_epos(header_user);

                    if (files.files!=null)
                    {
                        trabajo.ReportProgress(-90, string.Format("Descomprimiento Archivo...", 90));
                        File.WriteAllBytes(file_epos_default, files.files);
                        descomprimir(file_epos_default, @"C:\");
                    }
                }


            }
            catch (Exception)
            {

                
            }
           
        }
        void progressReport(Object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is String)
            {
                lblprogress.Text = (String)e.UserState;
            }
        }
        void workerComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            // hide animation
            ptb_load.Image = null;
            // show result indication
            if (e.Cancelled)
            {
                lblprogress.Text = "Operation cancelled by the user!";
                ptb_load.Image = Properties.Resources.Warning;
            }
            else
            {
                if (e.Error != null)
                {
                    lblprogress.Text = "Operation failed: " + e.Error.Message;
                    ptb_load.Image = Properties.Resources.Error;
                }
                else
                {
                    _parametros_tda();
                    genera_config_paperless();
                    ejecutar_bat_paperless();
                    lblprogress.Text = "La descarga se realizo satisfactoriamente!";
                    ptb_load.Image = Properties.Resources.Information;
                }
            }

            btnok.Enabled = true;
            
        }
        private string descomprimir(string _rutazip, string _destino)
        {
            string _error = "";
            try
            {
                ICSharpCode.SharpZipLib.Zip.FastZip fZip = new ICSharpCode.SharpZipLib.Zip.FastZip();
                fZip.ExtractZip(@_rutazip, @_destino, "");
            }
            catch (Exception exc)
            {
                _error = exc.Message;
            }
            return _error;
        }


    }
}
