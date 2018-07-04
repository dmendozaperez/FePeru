using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ws_Bata_Electronico.CapaEntidad.Util
{
    public class Ent_Conexion
    {
        #region<CONEXION DE BASE DE DATOS>
        public static string conexion
        {
            //get { return "Server=10.10.10.208;Database=BdTienda;User ID=sa;Password=Bata2013;Trusted_Connection=False;"; }
            get { return "Server=posperu.bgr.pe;Database=BDPOS;User ID=pos_oracle;Password=Bata2018**;Trusted_Connection=False;"; }
        }
        #endregion
    }
}