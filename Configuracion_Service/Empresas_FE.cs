using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Configuracion_Service
{
    public class Empresas_FE
    {
        public string nombre { get; set; }
        public string ruc { get; set; } 
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

}
