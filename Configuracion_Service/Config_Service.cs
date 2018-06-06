using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Configuracion_Service
{
    public partial class Config_Service : Form
    {
        public Config_Service()
        {
            InitializeComponent();
        }

        private void Config_Service_Load(object sender, EventArgs e)
        {
            loadinicio();
        }
        private void loadinicio()
        {
            rdb_epos.Checked = true;
            rdb_ec.Checked = true;
        }

        private void btnejecutar_Click(object sender, EventArgs e)
        {
            try
            {

                //System.Configuration.Configuration wConfig = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(new System.Configuration.ExeConfigurationFileMap { ExeConfigFilename = @"D:\Fuentes\FE_PAPERLESS\FE_BataPeru\CapaPresentacion\bin\Debug\CapaPresentacion.exe.config" }, System.Configuration.ConfigurationUserLevel.None);
                //wConfig.AppSettings.Settings["epos"].Value = "1";
                //wConfig.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception)
            {

               
            }
        }

    }
}
