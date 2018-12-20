using CapaModulo.Bll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
namespace ServiceWin_FE
{
    public partial class Service_FE : ServiceBase
    {
        Timer tmservicio = null;
        private Int32 _valida_service = 0;

        /*genera qr*/
        Timer tmservicio_qr = null;
        private Int32 _valida_service_qr = 0;

        private Boolean autenticando_epos = false;

        public Service_FE()
        {
            //5000=5 segundos
            InitializeComponent();
            tmservicio = new Timer(1000);
            tmservicio.Elapsed += new ElapsedEventHandler(tmpServicio_Elapsed);

            /*impresion de codigo qr*/
            tmservicio_qr = new Timer(1000);
            tmservicio_qr.Elapsed += new ElapsedEventHandler(tmservicio_qr_Elapsed);
        }
        void tmservicio_qr_Elapsed(object sender, ElapsedEventArgs e)
        {
            //string varchivov = "c://valida_hash.txt";
            Int32 _valor = 0;
            try
            {

                //if (!(System.IO.File.Exists(varchivov)))
                if (_valida_service_qr == 0)
                {

                    _valor = 1;
                    _valida_service_qr = 1;
                    //TextWriter tw = new StreamWriter(varchivov, true);
                    //tw.WriteLine(DateTime.Now.ToString() + "====>>>ejecutando procesos");
                    //tw.Close();
                    string _error = "";
                    Basico.ejecuta_impresion_qr(ref _error);
                    //if (System.IO.File.Exists(varchivov))
                    //{
                    _valida_service_qr = 0;
                    //System.IO.File.Delete(varchivov);
                    //}
                }
                //****************************************************************************
            }
            catch
            {
                //if (System.IO.File.Exists(varchivov))
                //{
                _valida_service_qr = 0;
                //System.IO.File.Delete(varchivov);
                //}                
            }

            if (_valor == 1)
            {
                //if (System.IO.File.Exists(varchivov))
                //{
                _valida_service_qr = 0;
                //System.IO.File.Delete(varchivov);
                //}   
            }

        }
        void tmpServicio_Elapsed(object sender, ElapsedEventArgs e)
        {
            //string varchivov = "c://valida_hash.txt";
            Int32 _valor = 0;
            try
            {
               

                //if (!(System.IO.File.Exists(varchivov)))
                if (_valida_service == 0)
                {
                    #region<AUTENTICANDO INICIO DEL EPOS>
                    if (!autenticando_epos)
                    {
                        Basico autentication = new Basico();
                        string error = "";
                        Boolean install_epos = autentication.verifica_servicio_epos();

                        if (install_epos)
                        {
                            autentication.verifica_install_epos();
                            autentication.autenticando_epos_inicial(ref error, ref autenticando_epos);
                            //if (error.Length == 0) error = "sin ningun error";

                            //if (error.Length>0)
                            //{
                            //    TextWriter tw2 = new StreamWriter(@"D:\ERROR.txt", true);
                            //    tw2.WriteLine(error);
                            //    tw2.Flush();
                            //    tw2.Close();
                            //    tw2.Dispose();
                            //}
                            //autenticando_epos = true;
                        }


                    }
                    #endregion
                    _valor = 1;
                    _valida_service = 1;
                    //TextWriter tw = new StreamWriter(varchivov, true);
                    //tw.WriteLine(DateTime.Now.ToString() + "====>>>ejecutando procesos");
                    //tw.Close();

                    //TextWriter tw2 = new StreamWriter(@"D:\INTERFA\ERROR.txt", true);
                    //tw2.WriteLine("ejecutando");
                    //tw2.Flush();
                    //tw2.Close();
                    //tw2.Dispose();

                    string _error = "";
                    Basico._ejecuta_proceso(ref _error);
                    //if (System.IO.File.Exists(varchivov))
                    //{
                    _valida_service = 0;
                    //System.IO.File.Delete(varchivov);
                    //}
                }
                //****************************************************************************
            }
            catch(Exception exc)
            {
             

                //if (System.IO.File.Exists(varchivov))
                //{
                _valida_service = 0;
                //System.IO.File.Delete(varchivov);
                //}                
            }

            if (_valor == 1)
            {
                //if (System.IO.File.Exists(varchivov))
                //{
                _valida_service = 0;
                //System.IO.File.Delete(varchivov);
                //}   
            }

        }
        protected override void OnStart(string[] args)
        {          
            tmservicio.Start();
            tmservicio_qr.Start();
        }

        protected override void OnStop()
        {
            tmservicio.Stop();
            tmservicio_qr.Stop();
        }
    }
}
