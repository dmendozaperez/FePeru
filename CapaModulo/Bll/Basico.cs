using Carvajal.FEPE.PreSC.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Drawing;
using System.ServiceModel.Configuration;
using System.ServiceProcess;

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
       

        private static string ruc_empresa = ConfigurationManager.AppSettings["empresa"].ToString(); //"20101951872";
        private static string ws_login= ConfigurationManager.AppSettings["ws_login"].ToString(); //"20101951872";
        private static string ws_pass = ConfigurationManager.AppSettings["ws_pass"].ToString(); //"20101951872";
        private static string socket_host = ConfigurationManager.AppSettings["socket_host"].ToString(); //"20101951872";
        private static Int32 socket_puerto =Convert.ToInt32(ConfigurationManager.AppSettings["socket_puerto"].ToString()); //"20101951872";

        private static Boolean metodo_epos = (ConfigurationManager.AppSettings["epos"].ToString() == "1") ? true : false;
        #endregion
        public static void _ejecuta_proceso(ref string _error)
        {
            _ruta_in_boleta = "D:\\INTERFA\\FEPERU\\IN\\Boletas";
            _ruta_in_factura = "D:\\INTERFA\\FEPERU\\IN\\Facturas";
            _ruta_in_credito = "D:\\INTERFA\\FEPERU\\IN\\Creditos";
            _ruta_in_debito = "D:\\INTERFA\\FEPERU\\IN\\Debitos";
            _ruta_in_retencion = "D:\\INTERFA\\FEPERU\\IN\\Retencion";

         

            string _carpeta_in = "";
           
            try
            {

                System.Configuration.Configuration wConfig = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(new System.Configuration.ExeConfigurationFileMap { ExeConfigFilename = @"D:\INTERFA\FEPERU\bata_proceso\ServiceWin_FE.exe.config" }, System.Configuration.ConfigurationUserLevel.None);

                string cod_qr = wConfig.AppSettings.Settings["gen_qr"].Value;
                /*GENERACION DE CODIGO QR Y FE=1 Y SOLO QR=2*/
                /*en este caso 2 no se procesa el codigo hash*/
                if (cod_qr == "2") return;

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
                _nombrearchivo_txt = Path.GetFileNameWithoutExtension(@_archivo);

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
                    string idtipo_doc = "";
                    //string ipsocket = host_socket; Int32 puerto = 5500;
                    //socket_host = "10.10.10.161";
                    clientSocket = new TcpClient();
                    clientSocket.Connect(socket_host,socket_puerto);

                    /*formatear formato de documento*/
                    _formato_doc = formato_e_pos(_formato_doc, _tipo_doc,ref idtipo_doc);
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
                        string num_doc = split[2].ToString();

                        #region<ENVIO DE CONFIRMACION>

                        string str_confirmacion = "@**@3\t0\t" + ruc_empresa + "\t1\t" +  idtipo_doc + "\t" + num_doc +  "*@@*";

                        outstream = Encoding.ASCII.GetBytes(str_confirmacion);

                        //NetworkStream serverstream1 = clientSocket.GetStream();
                        serverstream.Write(outstream, 0, outstream.Length);
                        serverstream.Flush();

                        instream = new byte[1024 * 1000];
                        serverstream.Read(instream, 0, (int)clientSocket.ReceiveBufferSize);
                        string return_confirmacion = Encoding.ASCII.GetString(instream);

                        string[] confirmacion= return_confirmacion.Trim().Split('\t');
                        estado_gen.codigo = confirmacion[0].ToString().Trim().Replace('\u0002', ' ').TrimEnd().TrimStart().Trim();

                        if (estado_gen.codigo!="0")
                        {
                            estado_gen.descripcion = confirmacion[1].ToString().Trim().Replace('\0', ' ').TrimEnd().TrimStart().Trim();
                            estado_gen.descripcion = estado_gen.descripcion.Replace(',', ' ');
                            estado_gen.descripcion = estado_gen.descripcion.Replace('\u0003', ' ').TrimEnd().TrimStart().Trim();
                        }

                        #endregion
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
        private static string formato_e_pos(string _str,string tdoc,ref string id_tipodoc)
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
                id_tipodoc = tipo_doc;
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
        #region<generacion e impresion de Codigo QR>
        public static void ejecuta_impresion_qr(ref string _error)
        {           
            string _ruta_in_boleta_qr = "D:\\INTERFA\\FEPERU\\IN\\Boletas\\QR";
            string _ruta_in_factura_qr = "D:\\INTERFA\\FEPERU\\IN\\Facturas\\QR";
            string _ruta_in_credito_qr = "D:\\INTERFA\\FEPERU\\IN\\creditos\\QR";
            string _ruta_in_debito_qr = "D:\\INTERFA\\FEPERU\\IN\\debitos\\QR";
            string _carpeta_in = "";
            try
            {
                if (!Directory.Exists(@_ruta_in_boleta_qr))
                {
                    Directory.CreateDirectory(@_ruta_in_boleta_qr);
                }
                if (!Directory.Exists(@_ruta_in_factura_qr))
                {
                    Directory.CreateDirectory(@_ruta_in_factura_qr);
                }
                if (!Directory.Exists(@_ruta_in_credito_qr))
                {
                    Directory.CreateDirectory(@_ruta_in_credito_qr);
                }
                if (!Directory.Exists(@_ruta_in_debito_qr))
                {
                    Directory.CreateDirectory(@_ruta_in_debito_qr);
                }

                string[] _rutas_in = { _ruta_in_boleta_qr, _ruta_in_factura_qr, _ruta_in_credito_qr, _ruta_in_debito_qr };

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

                    }

                    _carpeta_in = _rutas_in[i].ToString();
                    string[] _archivos_txt = Directory.GetFiles(@_carpeta_in, "*.txt");

                    if (_archivos_txt.Length > 0)
                    {
                        for (Int32 a = 0; a < _archivos_txt.Length; ++a)
                        {

                            string folder = _rutas_in[i].ToString(); string file = _archivos_txt[a].ToString();

                            _error = genimpr(folder, file);

                            //_error = "erroreds";

                            if (_error.Length == 0)
                            {
                                if (File.Exists(file))
                                {
                                    File.Delete(file);
                                }
                            }
                            else
                            {

                                string error_qr = folder + "\\error";

                                if (!Directory.Exists(error_qr))
                                {
                                    Directory.CreateDirectory(@error_qr);
                                }

                                string _nombrearchivo_qr = Path.GetFileNameWithoutExtension(@file);
                                string ruta_archivo_error = @error_qr + "//" + _nombrearchivo_qr + ".txt";

                                TextWriter tw = new StreamWriter(@ruta_archivo_error, true);
                                tw.WriteLine("1," + _error);
                                tw.Flush();
                                tw.Close();
                                tw.Dispose();
                            }
                            //}
                            //else
                            //{
                            //    TextWriter tw = new StreamWriter(@ruta_archivo_hash, true);
                            //    tw.WriteLine("1," + _error);
                            //    tw.Flush();
                            //    tw.Close();
                            //    tw.Dispose();

                            //}


                        }

                    }
                }


            }
            catch (Exception exc)
            {

                _error = exc.Message;
            }
        }
        private static string impresora_tda = "Ticket";
        private static byte[] generaQR(string str)
        {
            byte[] QR = null;
            try
            {
                GeneratorCdp genqr = new GeneratorCdp();

                QR = genqr.GetImageQrCodeFromString(str);

            }
            catch
            {
                QR = null;
            }
            return QR;
        }
        private static Image byteArrayToImage(byte[] bytesArr)
        {
            MemoryStream memstr = new MemoryStream(bytesArr);
            Image img = Image.FromStream(memstr);
            return img;
        }
        private static string[] caracteres =
      {
        "§","°",
        " ","á",
        "‚","é",
        "¡","í",
        "¢","ó",
        "£","ú",
        "µ","Á",
        " ","É",
        "Ö","Í",
        "à","Ó",
        "é","Ú",
        "¥","Ñ",
        "¤","ñ",
    };
        private static string ReemplazarCaracteresEspeciales(string origen)
        {
            string destino = "";
            List<string> listCaracteres = new List<string>();
            for (int i = 0; i < origen.Length; i++)
            {
                listCaracteres.Add(origen[i].ToString());
            }

            for (int i = 0; i < listCaracteres.Count; i++)
            {
                for (int j = 0; j < caracteres.Length; j = j + 2)
                {
                    if (listCaracteres[i] == caracteres[j])
                    {
                        listCaracteres[i] = listCaracteres[i].Replace(listCaracteres[i], caracteres[j + 1]);
                        j = caracteres.Length + 1;
                    }
                }
            }

            for (int i = 0; i < listCaracteres.Count; i++)
            {
                destino = destino + listCaracteres[i];
            }

            return destino;
        }
        private static void abrircajon()
        {
            try
            {
                RawPrinterHelper.SendStringToPrinter(impresora_tda, "\x1B" + "p" + "\x00" + "\x0F" + "\x96");
            }
            catch (Exception)
            {

                throw;
            }

        }
        private static string genimpr(string folder, string file)
        {
            string _error = "";
            try
            {
                Int32 n_impre = 0;
                Boolean tkregalo = false;
                Ticket tk = new Ticket();

                StreamReader sr = new StreamReader(@file, Encoding.Default);
                string _formato_doc = sr.ReadToEnd();
                sr.Close();

                /*verificar la cantidad de str array*/

                string[] str = Regex.Split(_formato_doc, "<td>");
                Byte[] qr = null;
                Image im = null;
                Bitmap bmp;
                if (str.Length > 1)
                {
                    string cadenaQR = str[1].ToString().Trim();
                    qr = generaQR(cadenaQR);
                    im = byteArrayToImage(qr);
                    bmp = new Bitmap(im, new Size(120, 120));                 
                    tk.HeaderImage = bmp;

                    impresora_tda = str[3].Trim();

                    foreach (string linea_array in str)
                    {
                        string cad = "";
                        /* string cad = ""*/                        
                        switch (n_impre)
                        {
                            /*si es varlor 0 entonces imprime el contenido*/
                            case 0:                                       
                            String[] str_linea= Regex.Split(str[0].ToString(), "\r");
                            foreach (string item_str in str_linea)
                            {
                                string cadena = item_str.PadLeft(42, ' ');
                                cadena= item_str.PadRight(42, ' ');
                                tk.AddHeaderLine(cadena);
                            }
                                tk.PrintTicket(impresora_tda);
                                break;
                            /*si es que se abre gaveta*/
                            case 2:
                                cad = ReemplazarCaracteresEspeciales(linea_array.Trim());

                                if (cad == "1")
                                {
                                    abrircajon();
                                }

                                break;
                            /*ticket de regalo*/
                            case 4:
                                tk = new Ticket();
                                str_linea = Regex.Split(str[4].ToString(), "\r");
                                Boolean tk_regalo = false;
                                foreach (string item_str in str_linea)
                                {
                                    string _str = item_str;
                                    _str = _str.Replace("\u001a", "");
                                    if (_str.TrimStart().TrimEnd().Trim().Length>0)
                                    { 
                                        string cadena = item_str.PadLeft(42, ' ');
                                        cadena = item_str.PadRight(42, ' ');
                                        tk.AddHeaderLine(cadena);
                                        tk_regalo = true;
                                    }
                                }
                                if (tk_regalo)
                                    tk.PrintTicket(impresora_tda);
                                break;
                        }
                        n_impre += 1;
                    }                  
                    

                }              


            }
            catch (Exception exc)
            {

                _error = exc.Message;
            }
            return _error;
        }
        #endregion

        #region<AUTENTICATION DE EPOS>
        private string ruc_global = "";
        private string tda_global = "";
        private void _espera_ejecuta(Int32 _segundos)
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
        public void autenticando_epos_inicial(ref String error,ref Boolean error_activando)
        {
            try
            {

               

                Int32 contar_error_epos = 0;
                //Boolean error_activando = false;
                autenticando_epos(ref error_activando, ref contar_error_epos,ref error);
                if (contar_error_epos == 1)
                {
                    _espera_ejecuta(10);
                    autenticando_epos(ref error_activando, ref contar_error_epos,ref error);
                }
            }
            catch(Exception exc) 
            {
                error = exc.Message;
            }
        }

        public Boolean verifica_servicio_epos()
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
        public void verifica_install_epos()
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
                 
                    string _ruta_file_config_paperless = @"C:\Paperless\e-pos\configuracion\0.config";
                    /*si existe el archivo entonces leemos su config*/
                    if (File.Exists(_ruta_file_config_paperless))
                    {
                        StreamReader sr = new StreamReader(@_ruta_file_config_paperless, Encoding.Default);
                        string _formato_config = sr.ReadToEnd();
                        sr.Close();
                        _formato_config = _formato_config.Replace('\n', ' ').Trim().TrimEnd();
                        string[] split = _formato_config.Split('\r');

                        if (split.Length > 0)
                        {
                            string ruc_config = split[2].ToString();
                            string tienda_config = split[3].ToString();

                            Int32 index_ruc = ruc_config.IndexOf('=') + 1;
                            ruc_config = ruc_config.Substring(index_ruc, ruc_config.Length - index_ruc).Trim().TrimEnd();

                            Int32 index_tienda = tienda_config.IndexOf('=') + 1;
                            tienda_config = tienda_config.Substring(index_tienda, tienda_config.Length - index_tienda).Trim().TrimEnd();
                            

                            tda_global = tienda_config;

                            /*verificar que la empresa exista*/
                            var str_existe = emp_fa.Where(b => b.ruc == ruc_config).ToList();

                            if (str_existe.Count() > 0)                                                           
                                ruc_global = str_existe[0].ruc;                               
                            
                            

                        }
                    }

                }                
                
            }
            catch
            {

            }
        }
        private string formato_epos_autentication()
        {
            string str = "";
            try
            {
                str = "@**@1\t0\t" + ruc_global + "\t1\t" + tda_global + "*@@*";
            }
            catch
            {

            }
            return str;
        }
        private void autenticando_epos(ref Boolean error_activando, ref Int32 contar_error_epos,ref  string error)
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
                string[] split = return_data.Trim().Replace('\u0002', ' ').Replace('\0', ' ').Replace('\u0003', ' ').Trim().TrimEnd().Split('\t');

                //error = split[0].ToString() + "==>" + split[1].ToString();

                if (split[0] != "0")
                {
                    //MessageBox.Show(split[1].ToString(), "Aviso del sistema (Bata- Peru)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    error_activando = true;
                }
            }
            catch (Exception exc)
            {
                
                contar_error_epos += 1;

                if (contar_error_epos == 2)
                {
                    error_activando = true;
                    //MessageBox.Show(exc.Message, "Aviso del sistema (Bata- Peru)", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                }
                throw;
            }
        }
        #endregion
    }
    public class Empresas_Lista
    {
        public IEnumerable<Empresas_FE> Empresas_Bata()
        {
            var lista = new List<Empresas_FE>();
            lista.Add(new Empresas_FE
            {
                nombre = "EMPRESAS COMERCIALES S.A. Y/O EMCOMER S.A.",
                //ruc = "201013232951872",
                ruc = "20101951872",
            });
            lista.Add(new Empresas_FE
            {
                nombre = "TROPICALZA S.A.C.",
                //ruc = "20408993230816",
                ruc = "20408990816",
            });
            return lista.ToList();
        }
    }
    public class Empresas_FE
    {
        public string nombre { get; set; }
        public string ruc { get; set; }
    }
}
