using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.ServiceProcess;
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

        private string ruc_global = "";
        private string tda_global = "";
        private void Config_Service_Load(object sender, EventArgs e)
        {
            loadinicio();
        }
        private void loadinicio()
        {
            limpiar_object();
            verifica_install_epos();
            //rdb_epos.Checked = true;
            //rdb_ec.Checked = true;
        }
        private void limpiar_object()
        {
            lblcodtda.Text = "";
        }
        private void verifica_install_epos()
        {
            Empresas_Lista emp = null;
            try
            {
                emp = new Empresas_Lista();
                var emp_fa = emp.Empresas_Bata();

                /*si el servicio esta instalado entonces verificamos el config de PAPERLESS*/
                if (verifica_servicio_epos())
                {
                    /*en este caso como tiene el servicio desactivamos la opcion wsdl*/
                    rdb_epos.Enabled = true;
                    rdb_wsdl.Enabled = false;
                    rdb_epos.Checked = true;
                    string _ruta_file_config_paperless = @"C:\Paperless\e-pos\configuracion\0.config";
                    /*si existe el archivo entonces leemos su config*/
                    if (File.Exists(_ruta_file_config_paperless))
                    {
                        StreamReader sr = new StreamReader(@_ruta_file_config_paperless, Encoding.Default);
                        string _formato_config = sr.ReadToEnd();
                        sr.Close();
                        _formato_config = _formato_config.Replace('\n',' ').Trim().TrimEnd();
                        string[] split = _formato_config.Split('\r');

                        if (split.Length>0)
                        {
                            string ruc_config = split[2].ToString(); 
                            string tienda_config= split[3].ToString();

                            Int32 index_ruc = ruc_config.IndexOf('=') + 1;
                            ruc_config = ruc_config.Substring(index_ruc, ruc_config.Length - index_ruc).Trim().TrimEnd(); 
                            
                            Int32 index_tienda= tienda_config.IndexOf('=') + 1;
                            tienda_config = tienda_config.Substring(index_tienda, tienda_config.Length - index_tienda).Trim().TrimEnd();

                            lblcodtda.Text = tienda_config;

                            tda_global = tienda_config;

                            /*verificar que la empresa exista*/
                            var str_existe = emp_fa.Where(b => b.ruc == ruc_config).ToList();

                            if (str_existe.Count()>0)
                            {
                                btnejecutar.Enabled = true;
                                ruc_global = str_existe[0].ruc;
                                if (Left(str_existe[0].nombre,1)=="E")
                                {                                    
                                    rdb_ec.Checked = true;
                                    rdb_tropi.Checked = false;
                                    rdb_ec.Enabled = true;
                                    rdb_tropi.Enabled = false;
                                }
                                else
                                {
                                    rdb_ec.Checked = false;
                                    rdb_tropi.Checked = true;
                                    rdb_ec.Enabled = false;
                                    rdb_tropi.Enabled = true;
                                }                              
                            }
                            else
                            {
                                btnejecutar.Enabled = false;
                                rdb_ec.Checked = false;
                                rdb_tropi.Checked = false;
                                rdb_ec.Enabled = false;
                                rdb_tropi.Enabled = false;
                                MessageBox.Show("El Numero de Ruc N° " + ruc_config + " Configurado en el E-Pos no existe en la configuracion Bata", "Aviso del sistema (Bata- Peru)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }

                        }                        
                    }
                    
                }
                else
                {
                    rdb_wsdl.Enabled = true;
                    rdb_epos.Enabled = false;
                    rdb_epos.Checked = false;
                    rdb_wsdl.Checked = true;
                    rdb_ec.Checked = true;
                    rdb_tropi.Enabled = true;
                }
            }
            catch 
            {
                
            }
        }

        private void activando_wsld()
        {
            desactivando_servicio_win();
            string empresa = (rdb_ec.Checked) ? "20101951872" : "20408990816";
            string met_fac = (rdb_epos.Checked) ? "1" : "0";
            string localhost = "localhost";
            update_config(empresa, met_fac, localhost);
            activando_servicio_win();
        }
        private void activando_epos(ref Boolean error_activando)
        {
            try
            {
                activando_servicio_epos(ref error_activando);
                Int32 contar_error_epos = 0;
                /*enviando porque la primera vez envia erro por unica vez en la conexion por eso envio 2 veces el metodo para que no 
                 haga ningun probleme en la facturacion*/
                autenticando_epos(ref error_activando,ref contar_error_epos);
                if (contar_error_epos==1)
                {
                    autenticando_epos(ref error_activando, ref contar_error_epos);
                }
            }
            catch 
            {

               
            }
        }
        /// <summary>
        /// autenthenticacion del epos
        /// </summary>
        /// <returns></returns>
        private string formato_epos_autentication()
        {
            string str = "";
            try
            {
                str = "@**@1\t0\t" + ruc_global  + "\t1\t" + tda_global +"*@@*";
            }
            catch 
            {
                
            }
            return str;
        }
        private void autenticando_epos(ref Boolean error_activando,ref Int32 contar_error_epos)
        {
            TcpClient clientSocket = null;
            string socket_host = "localhost";
            Int32 socket_puerto = 5500;
            try
            {
                clientSocket = new TcpClient();
                clientSocket.Connect(socket_host, socket_puerto);

                /*formatear formato de documento*/
                string _formato_doc = formato_epos_autentication();
                /**/

                byte[] outstream = Encoding.ASCII.GetBytes(_formato_doc);

                NetworkStream serverstream = clientSocket.GetStream();
                serverstream.Write(outstream, 0, outstream.Length);
                serverstream.Flush();

                byte[] instream = new byte[1024 * 1000];
                serverstream.Read(instream, 0, (int)clientSocket.ReceiveBufferSize);

                string return_data = Encoding.ASCII.GetString(instream);
                string[] split = return_data.Trim().Replace('\u0002',' ').Replace('\0',' ').Replace('\u0003',' ').Trim().TrimEnd().Split('\t');

                if (split[0]!="0")
                {
                    MessageBox.Show(split[1].ToString(), "Aviso del sistema (Bata- Peru)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    error_activando = true;
                }
            }
            catch (Exception exc)
            {
                contar_error_epos += 1;
                error_activando = true;
                if (contar_error_epos==2)
                    MessageBox.Show(exc.Message, "Aviso del sistema (Bata- Peru)", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }
        private void btnejecutar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
               
                if (rdb_wsdl.Checked)
                {
                    activando_wsld();
                }
                else
                {
                    Boolean valida_activacion = false;
                    desactivando_servicio_win();
                    activando_epos(ref valida_activacion);
                    if (valida_activacion)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Por favor vuelva a validar o valide como Administrador", "Aviso del sistema (Bata- Peru)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    activando_servicio_win();
                }               
                MessageBox.Show("El servicio se activo correctamente", "Aviso del sistema (Bata- Peru)", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {

               
            }
            Cursor.Current = Cursors.Default;
            this.Close();
        }
        private  void _espera_ejecuta(Int32 _segundos)
        {
            try
            {
                _segundos = _segundos * 1000;
                System.Threading.Thread.Sleep(_segundos);
            }
            catch
            {

            }
        }
        /// <summary>
        /// configuracion del epos desde codigo
        /// </summary>
        /// <param name="empresa"></param>
        /// <param name="e_pos"></param>
        /// <param name="localhost"></param>
        private void update_config(string empresa,string e_pos,string localhost)
        {
            try
            {
                System.Configuration.Configuration wConfig = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(new System.Configuration.ExeConfigurationFileMap { ExeConfigFilename = @"D:\INTERFA\FEPERU\bata_proceso\ServiceWin_FE.exe.config" }, System.Configuration.ConfigurationUserLevel.None);
                wConfig.AppSettings.Settings["epos"].Value = e_pos;
                wConfig.AppSettings.Settings["empresa"].Value = empresa;
                wConfig.AppSettings.Settings["socket_host"].Value = localhost;
                wConfig.Save(ConfigurationSaveMode.Modified);
            }
            catch 
            {
                
            }
        }
        /// <summary>
        /// verifica si el servicio epos esta instalado como servicio
        /// </summary>
        /// <returns></returns>
        private Boolean verifica_servicio_epos()
        {
            Boolean valida = false;
            try
            {
                ServiceController[] service;
                service = (ServiceController[])ServiceController.GetServices();
                for (Int32 s = 0; s < service.Length; ++s)
                {
                    string nameservicio = service[s].ServiceName;
                    if (nameservicio == "PPLJavaPOSPeru")
                    {
                        valida = true;
                        break;
                    }
                }
            }
            catch 
            {
                valida = false;                
            }
            return valida;
        }
        /// <summary>
        /// desactivando servicio FE
        /// </summary>
        private void desactivando_servicio_win()
        {
            try
            {
                ServiceController[] service;
                service = (ServiceController[])ServiceController.GetServices();
                for (Int32 s = 0; s < service.Length; ++s)
                {
                    string nameservicio = service[s].ServiceName;
                    if (nameservicio == "Service FE (Bata)")
                    {
                        //en este caso vamos activar el firewall para la tranferencia de ftp al server
                        //agregarfirewall(2);

                        string status = service[s].Status.ToString();
                        string DisplayName = service[s].DisplayName.ToString();
                        string ServiceType = service[s].ServiceType.ToString();
                        string MachineName = service[s].MachineName.ToString();

                        ServiceController servicio;
                        ServiceControllerStatus servStatus;
                        servicio = (ServiceController)service[s];
                        servicio.Refresh();
                        servStatus = servicio.Status;
                        if (Left(servStatus.ToString(), 1) == "R")
                        {
                            servicio.Stop();
                            servicio.Refresh();
                            Console.Write("El servicio se activo Correctamente");
                            return;
                        }
                    }
                }
            }
            catch
            {

            }
        }

        private void activando_servicio_epos(ref Boolean error_activando)
        {
            try
            {
                ServiceController[] service;
                service = (ServiceController[])ServiceController.GetServices();
                for (Int32 s = 0; s < service.Length; ++s)
                {
                    string nameservicio = service[s].ServiceName;
                    if (nameservicio == "PPLJavaPOSPeru")
                    {
                        //en este caso vamos activar el firewall para la tranferencia de ftp al server
                        //agregarfirewall(2);

                        string status = service[s].Status.ToString();
                        string DisplayName = service[s].DisplayName.ToString();
                        string ServiceType = service[s].ServiceType.ToString();
                        string MachineName = service[s].MachineName.ToString();

                        ServiceController servicio;
                        ServiceControllerStatus servStatus;
                        servicio = (ServiceController)service[s];
                        servicio.Refresh();
                        servStatus = servicio.Status;
                        if (Left(servStatus.ToString(), 1) != "R")
                        {
                            servicio.Start();
                            servicio.Refresh();
                            Console.Write("El servicio se activo Correctamente");
                            return;
                        }
                    }
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message, "Aviso del sistema (Bata- Peru)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                error_activando = true;
            }
        }

        /// <summary>
        /// activando servicio FE
        /// </summary>
        private void activando_servicio_win()
        {
            try
            {
                ServiceController[] service;
                service = (ServiceController[])ServiceController.GetServices();
                for (Int32 s = 0; s < service.Length; ++s)
                {
                    string nameservicio = service[s].ServiceName;
                    if (nameservicio == "Service FE (Bata)")
                    {
                        //en este caso vamos activar el firewall para la tranferencia de ftp al server
                        //agregarfirewall(2);

                        string status = service[s].Status.ToString();
                        string DisplayName = service[s].DisplayName.ToString();
                        string ServiceType = service[s].ServiceType.ToString();
                        string MachineName = service[s].MachineName.ToString();

                        ServiceController servicio;
                        ServiceControllerStatus servStatus;
                        servicio = (ServiceController)service[s];
                        servicio.Refresh();
                        servStatus = servicio.Status;
                        if (Left(servStatus.ToString(), 1) != "R")
                        {                            
                            servicio.Start();
                            servicio.Refresh();
                            Console.Write("El servicio se activo Correctamente");
                            return;
                        }
                    }
                }
            }
            catch
            {

            }
        }
        private string Left(string param, int length)
        {
            //we start at 0 since we want to get the characters starting from the
            //left and with the specified lenght and assign it to a variable
            string result = param.Substring(0, length);
            //return the result of the operation
            return result;
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            verifica_install_epos();
            MessageBox.Show("Configuraciones Actualizadas", "Aviso del sistema (Bata- Peru)", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Cursor.Current = Cursors.Default;
        }
    }
}
