using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED2LAB1_CAMP1127922.CMP
{
    internal class LZW
    {
        private Dictionary<string, int> init = new Dictionary<string, int>();
        private void CFD(string mensaje)
        {
            for (int i = 0; i < mensaje.Length; i++)
            {
                string current = Convert.ToString(mensaje[i]);
                if (!init.ContainsKey(current))
                {
                    init.Add(current, init.Count);
                }
            }
        }
        public string COMPRESS(string mensaje)
        {
            CFD(mensaje);
            string w = null;
            string k = "";
            string wk = "";
            string salida = "";
            for (int i = 0; i < mensaje.Length; i++)
            {
                k = Convert.ToString(mensaje[i]);
                if (w==null)
                {
                    wk = k;
                }
                else
                {                    
                    wk = w + k;
                }
                if (init.ContainsKey(wk))
                {
                    w = wk;
                }
                else
                {                   
                    salida += init[w]+",";
                    init.Add(wk, init.Count);
                    w = k;
                }
            }
            salida += init[w] + ",";
            return salida;
        }

        public string DECOMPRESS(string compress)
        {
            return "";
        }
    }

}
