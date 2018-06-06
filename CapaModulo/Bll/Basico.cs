using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace CapaModulo.Bll
{
    public class Basico
    {
        #region <REGION VARIABLES DE CONFIGURACION>
        private static string _ruta_in_boleta { set; get; }
        private static string _ruta_in_factura { set; get; }
        private static string _ruta_in_credito { set; get; }
        private static string _ruta_in_debito { set; get; }
        private static string _ruta_in_retencion { get; set; }

        //configuracion por defecto de carvajal
        private static string _ruta_carvajal_xml { set; get; }
        private static string _ruta_carvajal_mapa { set; get; }
        private static string _ruta_carvajal_esquemas { set; get; }
        private static string _ruta_carvajal_certificado { set; get; }

        private static string ruc_empresa = ConfigurationManager.AppSettings["empresa"].ToString(); //"20101951872";
        private static string ws_login= ConfigurationManager.AppSettings["ws_login"].ToString(); //"20101951872";
        private static string ws_pass = ConfigurationManager.AppSettings["ws_pass"].ToString(); //"20101951872";


        private static Boolean metodo_epos = (ConfigurationManager.AppSettings["epos"].ToString() == "1") ? true : false;
        #endregion
        public static void _ejecuta_proceso(ref string _error)
        {
            _ruta_in_boleta = "D:\\INTERFA\\FEPERU\\IN\\Boletas";
            _ruta_in_factura = "D:\\INTERFA\\FEPERU\\IN\\Facturas";
            _ruta_in_credito = "D:\\INTERFA\\FEPERU\\IN\\Creditos";
            _ruta_in_debito = "D:\\INTERFA\\FEPERU\\IN\\Debitos";
            _ruta_in_retencion = "D:\\INTERFA\\FEPERU\\IN\\Retencion";

            _ruta_carvajal_xml = "D:\\INTERFA\\FEPERU\\XML";
            _ruta_carvajal_mapa = "D:\\INTERFA\\FEPERU\\bata_proceso\\Mapas";
            _ruta_carvajal_esquemas = "D:\\INTERFA\\FEPERU\\bata_proceso\\Esquemas";
            _ruta_carvajal_certificado = "D:\\INTERFA\\FEPERU\\bata_proceso\\Certificado";

            string _carpeta_in = "";
           
            try
            {

                if (!Directory.Exists(@_ruta_in_boleta))
                {
                    Directory.CreateDirectory(@_ruta_in_boleta);
                }
                if (!Directory.Exists(@_ruta_in_factura))
                {
                    Directory.CreateDirectory(@_ruta_in_factura);
                }
                if (!Directory.Exists(@_ruta_in_credito))
                {
                    Directory.CreateDirectory(@_ruta_in_credito);
                }
                if (!Directory.Exists(@_ruta_in_debito))
                {
                    Directory.CreateDirectory(@_ruta_in_debito);
                }
                if (!Directory.Exists(@_ruta_in_retencion))
                {
                    Directory.CreateDirectory(@_ruta_in_retencion);
                }

                //***************************
                //configuracion por defecto de app config

                if (!Directory.Exists(@_ruta_carvajal_xml))
                {
                    Directory.CreateDirectory(@_ruta_carvajal_xml);
                }
                if (!Directory.Exists(@_ruta_carvajal_mapa))
                {
                    Directory.CreateDirectory(@_ruta_carvajal_mapa);
                }
                if (!Directory.Exists(@_ruta_carvajal_esquemas))
                {
                    Directory.CreateDirectory(@_ruta_carvajal_esquemas);
                }
                if (!Directory.Exists(@_ruta_carvajal_certificado))
                {
                    Directory.CreateDirectory(@_ruta_carvajal_certificado);
                }

                //ahora recorrer las carpetas in y verificas cuales son los archivo para la generacion de hash
                //verificar archivo
                string[] _rutas_in = { _ruta_in_boleta, _ruta_in_factura, _ruta_in_credito, _ruta_in_debito, _ruta_in_retencion };

                //***********************************
                for (Int32 i = 0; i < _rutas_in.Length; ++i)
                {
                    string _tipo_doc = "";

                    switch (i)
                    {
                        case 0:
                            _tipo_doc = "BO";
                            break;
                        case 1:
                            _tipo_doc = "FA";
                            break;
                        case 2:
                            _tipo_doc = "NC";
                            break;
                        case 3:
                            _tipo_doc = "ND";
                            break;
                        case 4:
                            _tipo_doc = "RE";
                            break;
                    }

                    _carpeta_in = _rutas_in[i].ToString();
                    string[] _archivos_txt = Directory.GetFiles(@_carpeta_in, "*.txt");

                    if (_archivos_txt.Length > 0)
                    {
                        for (Int32 a = 0; a < _archivos_txt.Length; ++a)
                        {

                            string ruta_archivo_hash = "";
                            string ruta_archivo_externo = "";
                            Int32 _ingreso = 0;
                            string codigo_hash = _retornar_codigo_hash(ref _error, ref ruta_archivo_hash, ref ruta_archivo_externo, ref _ingreso, _archivos_txt[a].ToString(), _tipo_doc, _rutas_in[i].ToString());
                            if (_ingreso == 0) return;
                            if (System.IO.File.Exists(@ruta_archivo_hash))
                            {
                                System.IO.File.Delete(@ruta_archivo_hash);
                            }

                            if (_error.Length == 0 && codigo_hash.Length > 0)
                            {
                                TextWriter tw = new StreamWriter(@ruta_archivo_hash, true);
                                tw.WriteLine("0," + codigo_hash);
                                tw.Flush();
                                tw.Close();
                                tw.Dispose();

                                //borrar archivo generado por externo
                                if (System.IO.File.Exists(@ruta_archivo_externo))
                                {
                                    System.IO.File.Delete(@ruta_archivo_externo);
                                }

                            }
                            else
                            {
                                TextWriter tw = new StreamWriter(@ruta_archivo_hash, true);
                                tw.WriteLine("1," + _error);
                                tw.Flush();
                                tw.Close();
                                tw.Dispose();

                                //en este paso vamos a usar el wf , para enviar el error al servidor con el archivo
                                //string _nombrearchivo_txt = System.IO.Path.GetFileNameWithoutExtension(@ruta_archivo_externo);
                                //string _tienda = _nombrearchivo_txt.Substring(1, 3);
                                //byte[] _archivo_bytes_txt = File.ReadAllBytes(@ruta_archivo_externo);
                                //ServiceBata.ws_bataSoapClient _error_genera_hash = new ServiceBata.ws_bataSoapClient();

                                //string _error_service_txt = _error_genera_hash.ws_control_error(_archivo_bytes_txt, _tienda, _nombrearchivo_txt, _error);

                                //if (_error_service_txt == "1")
                                //{
                                //    File.Delete(@ruta_archivo_externo);
                                //}


                            }



                        }

                    }
                }
              
            }
            catch (Exception exc)
            {
                _error = "CATCH DE ERROR" + exc.Message;
                TextWriter tw = new StreamWriter(@"D:\INTERFA\CARVAJAL\XML\error.txt", true);
                tw.WriteLine(_error);
                tw.Flush();
                tw.Close();
                tw.Dispose();
            }
        }
        public static string _retornar_codigo_hash(ref string _error, ref string _ruta_archivo, ref string ruta_archivo_externo, ref Int32 _ingreso, string _archivo, string _tipo_doc, string _carpeta_in)
        {
            //_ruta_in_boleta = "D:\\INTERFA\\FEPERU\\IN\\Boletas";
            //_ruta_in_factura = "D:\\INTERFA\\FEPERU\\IN\\Facturas";
            //_ruta_in_credito = "D:\\INTERFA\\FEPERU\\IN\\creditos";
            //_ruta_in_debito = "D:\\INTERFA\\FEPERU\\IN\\debitos";

            //_ruta_carvajal_xml = "D:\\INTERFA\\FEPERU\\XML";
            //_ruta_carvajal_mapa = "D:\\INTERFA\\FEPERU\\bata_proceso\\Mapas";
            //_ruta_carvajal_esquemas = "D:\\INTERFA\\FEPERU\\bata_proceso\\Esquemas";
            //_ruta_carvajal_certificado = "D:\\INTERFA\\FEPERU\\bata_proceso\\Certificado";

            string _valida_error = "";
            string _codigo_hash_return = "";
            //string _carpeta_in = "";
            string _nombrearchivo_txt = "";
            //string _archivo = "";
            Int32 _valida = 0;
            FEBata.OnlinePortTypeClient gen_fe = null;
            EstadoGenera estado_gen = null;
            TcpClient clientSocket = null;
            try
            {
               
                //CAPTURAR DESDE LA RUTA EL ARCHIVO DEL FORMTO
                StreamReader sr = new StreamReader(@_archivo, Encoding.Default);
                string _formato_doc = sr.ReadToEnd();
                sr.Close();


                //_formato_doc = "111";
                //CERRAR LA INSTNACIA
                _nombrearchivo_txt = System.IO.Path.GetFileNameWithoutExtension(@_archivo);

                //instanciar la dll externa
                string _codigo_hash = "";
                _codigo_hash_return = "";

                _ingreso = 1;/*variable de inicializacion*/


                #region<METODO WEB SERVICE>
                if (!metodo_epos)
                {
                    //string ruc = ruc_empresa ; string login = "admin_ws"; string clave = "abc123"; string str = "xxx"; 
                    Int32 tipofoliacion = 1;

                    /*0 = ID asignado 1 = URL del XML 2 = URL del PDF 3 = Estado en la SUNAT 4 = Folio Asignado (Serie-Correlativo) 5 = Bytes del PDF en Base64
                      6 = PDF417 (Cadena de texto a imprimir en el PDF 417) 7 = HASH (Cadena de texto)*/                
               
                    gen_fe = new FEBata.OnlinePortTypeClient();
                    string consulta = gen_fe.OnlineGeneration(ruc_empresa, ws_login, ws_pass, _formato_doc, tipofoliacion, 7);                
                    var doc = XDocument.Parse(consulta);
                    var result = from factura in doc.Descendants("Respuesta")
                                 select new
                                 {
                                     Codigo = factura.Element("Codigo").Value,
                                     Mensaje = factura.Element("Mensaje").Value,                                
                                 };
                    estado_gen = new EstadoGenera();
                    foreach (var item in result)
                    {
                        estado_gen.codigo = item.Codigo;
                        estado_gen.descripcion = item.Mensaje.Replace(',',' ');
                        estado_gen.descripcion = estado_gen.descripcion.Replace('\n',' ').Trim().TrimEnd();
                    }

                    /*si el retorno es cero entonces retorno ok*/              
                    if (estado_gen.codigo == "0")
                    {                   
                        _codigo_hash = estado_gen.descripcion;
                    }
               
                    if (_codigo_hash.Length > 0)
                    {
                        _codigo_hash_return = _codigo_hash;
                        _valida = 1;                 
                    }
                    else
                    {
                        _codigo_hash_return = estado_gen.descripcion;
                    }
                }
                #endregion

                #region<METODO E-POS>
                if (metodo_epos)
                {                    
                    string ipsocket = "10.10.10.66"; Int32 puerto = 5500;
                    clientSocket = new TcpClient();
                    clientSocket.Connect(ipsocket, puerto);

                    /*formatear formato de documento*/
                    _formato_doc = formato_e_pos(_formato_doc, _tipo_doc);
                    /**/

                    byte[] outstream = Encoding.ASCII.GetBytes(_formato_doc);

                    NetworkStream serverstream = clientSocket.GetStream();
                    serverstream.Write(outstream, 0, outstream.Length);
                    serverstream.Flush();

                    byte[] instream = new byte[1024 * 1000];
                    serverstream.Read(instream, 0, (int)clientSocket.ReceiveBufferSize);

                    string return_data = Encoding.ASCII.GetString(instream);

                    string[] split = return_data.Trim().Split('\t');

                    estado_gen = new EstadoGenera();
                    /*errores de generacion*/
                    if (split.Length == 2)
                    {
                        estado_gen.codigo = split[0].ToString().TrimEnd().TrimStart().Trim();
                        estado_gen.descripcion = split[1].ToString().Trim().Replace('\0', ' ').TrimEnd().TrimStart().Trim();
                        estado_gen.descripcion = estado_gen.descripcion.Replace(',', ' ').TrimEnd().TrimStart().Trim();


                    }
                    /*se genero exitosamente*/
                    if (split.Length==5)
                    {
                        estado_gen.codigo = split[0].ToString().Trim().Replace('\u0002', ' ').TrimEnd().TrimStart().Trim();
                        estado_gen.descripcion = split[4].ToString().Trim().Replace('\0', ' ').TrimEnd().TrimStart().Trim();
                        estado_gen.descripcion = estado_gen.descripcion.Replace(',', ' ');
                        estado_gen.descripcion = estado_gen.descripcion.Replace('\u0003', ' ').TrimEnd().TrimStart().Trim(); 
                    }

                    /*si el retorno es cero entonces retorno ok*/
                    if (estado_gen.codigo == "0")
                    {
                        _codigo_hash = estado_gen.descripcion;
                    }

                    if (_codigo_hash.Length > 0)
                    {
                        _codigo_hash_return = _codigo_hash;
                        _valida = 1;
                    }
                    else
                    {
                        _codigo_hash_return = estado_gen.descripcion;
                    }

                }
                #endregion
            }
            catch (Exception exc)
            {
                _valida_error = exc.Message;
            }

            if (_valida_error.Length==0)
            {
                if (estado_gen!=null)
                {
                    if  (estado_gen.codigo!="0") _valida_error = estado_gen.descripcion;
                }
            }


            ruta_archivo_externo = _carpeta_in;
            string _hash = _carpeta_in + "\\hash";
            //verificar que la carpeta hash exista
            if (_ingreso == 1)
            {
                if (!Directory.Exists(@_hash))
                {
                    Directory.CreateDirectory(@_hash);
                }
            }
            string _ruta_archivo_txt = _hash + "\\" + _nombrearchivo_txt + ".txt";
            _ruta_archivo = _ruta_archivo_txt;
            _error = _valida_error;
            ruta_archivo_externo = _archivo;        
            return _codigo_hash_return;
        }
        
        private static string formato_e_pos(string _str,string tdoc)
        {
            string str_new_format = "";
            try
            {

                string tipo_doc = "";
                switch(tdoc)
                {
                    case "BO":
                        tipo_doc = "03";
                        break;
                    case "FA":
                        tipo_doc = "01";
                        break;
                    case "NC":
                        tipo_doc = "07";
                        break;
                    case "ND":
                        tipo_doc = "08";
                        break;
                    case "RE":
                        tipo_doc = "20";
                        break;

                }

                string str_primer_line = "@**@2\t0\t" + ruc_empresa +"\t1\t" + tipo_doc + "\t\n";
                string str_fin_line ="*@@*";

                str_new_format = str_primer_line + _str + str_fin_line;

            }
            catch
            {
                str_new_format = "";                
            }
            return str_new_format;
        }
        

    }
}
