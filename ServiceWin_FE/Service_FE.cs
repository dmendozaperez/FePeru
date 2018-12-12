using CapaModulo.Bll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

                    _valor = 1;
                    _valida_service = 1;
                    //TextWriter tw = new StreamWriter(varchivov, true);
                    //tw.WriteLine(DateTime.Now.ToString() + "====>>>ejecutando procesos");
                    //tw.Close();
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
            catch
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
