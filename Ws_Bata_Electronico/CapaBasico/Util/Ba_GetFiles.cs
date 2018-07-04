using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Ws_Bata_Electronico.CapaBasico.Util;

namespace Ws_Bata_Electronico.CapaBasico.Util
{
    public class Ba_GetFiles
    {
        private string _path_file_epos = @"D:\Upload\MultiPOS_Bata_QA.zip";
        public Ba_Files get_files_epos()
        {
            Ba_Files epos = null;
            try
            {
                epos = new Ba_Files();

                if (File.Exists(_path_file_epos))
                {
                    epos.codigo = "0";
                    epos.descripcion = "Descargando Archivo";
                    epos.files= File.ReadAllBytes(_path_file_epos);
                }
                else
                {
                    epos.codigo = "1";
                    epos.descripcion = "No existe el Archivo";
                }

                
            }
            catch (Exception exc)
            {
                epos.codigo = "1";
                epos.descripcion = exc.Message;
            }
            return epos;
        }
    }
}