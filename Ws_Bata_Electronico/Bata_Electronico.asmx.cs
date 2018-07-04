using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Ws_Bata_Electronico.CapaBasico.Util;

namespace Ws_Bata_Electronico
{
    /// <summary>
    /// Descripción breve de Bata_Electronico
    /// </summary>
    [WebService(Namespace = "http://bataperu.com.pe/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class Bata_Electronico : System.Web.Services.WebService
    {

        public ValidateAcceso Authentication;
        Ba_WsConexion autentication_ws;
        [SoapHeader("Authentication", Required = true)]
        [WebMethod(Description = "Descarga de archivo e-pos")]
        public Ba_Files ws_descargar_epos()
        {
            autentication_ws = new Ba_WsConexion();
            Ba_Files epos = null;
            Ba_GetFiles get_epos = null;
            try
            {
                epos = new Ba_Files();
                /*valida acceso a web service descargar e-pos*/
                Boolean valida_ws = autentication_ws.ckeckAuthentication_ws("02", Authentication.Username, Authentication.Password);
                if (valida_ws)
                {
                    get_epos = new Ba_GetFiles();
                    epos = get_epos.get_files_epos();
                }
                else
                {
                    epos.codigo = "1";
                    epos.descripcion = "Conexión sin exito";
                }
            }
            catch(Exception exc)
            {
                epos.codigo = "1";
                epos.descripcion = exc.Message;
            }
            return epos;

        }

    }
    public class ValidateAcceso : SoapHeader
    {
        public string Username;
        public string Password;
    }
}
