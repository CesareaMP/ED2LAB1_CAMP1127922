using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED2LAB1_CAMP1127922.CMP
{
    internal class LZW
    {
        public (string compr, Dictionary<string, int> dictio) COMPRESS(string mensaje)
        {
            Dictionary<string, int> init = new Dictionary<string, int>();
            for (int i = 0; i < mensaje.Length; i++)
            {
                string current = Convert.ToString(mensaje[i]);
                if (!init.ContainsKey(current))
                {
                    init.Add(current, init.Count);
                }
            }
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
            return (salida,init);
        }

        public string DECOMPRESS(string compress, Dictionary<string, int> init)
        {
            string original = "";
            int total = compress.Split(',').Count()-1;
            int parte;
            for (int i = 0; i < total; i++)
            {
                parte = int.Parse(compress.Split(',')[i]);
                original += init.FirstOrDefault(x => x.Value == parte).Key;
            }
            return original;
        }
    }
}
