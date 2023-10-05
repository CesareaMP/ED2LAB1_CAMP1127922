using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ED2LAB1_CAMP1127922
{
    internal class Aritmetica {
        //{
        //    CODIFICAR
        //    {
        //    Superior=distancia* limite de la letra superior + limite inferior anterior del mensaje
        //    Inferior = distancia * limite de la letra inferior + limite inferior anterior del menssje
        //    Respuesta = superior de la primera letra en la tabla de probabilidades* distancia + inferior anterior
        //    }

        //DECODIFICAR
        //    {www
        //    DECODIFICAR=(Codigo-limite inferior de la letra a la que corresponde)/probabilidad
        //    probabilidad = superior - inferior
        //    }
        private string contexto = "Adams Inc, Huel Group, Jacobson and Sons, Jaskolski - Moore, Carroll, Schroeder and Fay, Kassulke LLC, Fahey, Sauer and Erdman, Zemlak and Sons, Fay, Bradtke and Mayert, O'Keefe, Legros and Rowe, Harris, Murphy and Zemlak, Murray - Jast, Blanda - Robel, Conroy - Morissette, Cruickshank and Sons, Osinski - Kshlerin, Grant, Walter and Dicki, Kuhic, Walker and Gleason, O'Reilly, Marquardt and Koss, Grady Group, Franecki - Balistreri, Torphy Inc, Cartwright, Gottlieb and Schimmel, Terry - Marvin, Mitchell Inc, Barrows - Corwin, Bauch, Moen and Walter, Aufderhar - Flatley, Bernhard LLC, Hayes, Bahringer and Sipes, Trantow - Gislason, Nolan - Witting, Bogisich Group, Hudson, Pollich and Johnson, Bechtelar - Osinski, Treutel, O'Reilly and Schimmel, McGlynn - Kerluke, Conroy and Sons, Renner, Bergstrom and Daniel, Fisher - Corwin, Kub, Purdy and Hegmann, Towne, Medhurst and Gorczany, Franey - Emmerich, Stark - Volkman, Cartwright Group, Schaden, Wolf and Keeling, Waters LLC, Kuhic - Batz, Rohan, Medhurst and Goodwin, Labadie, Dare and Graham319703211684905712449882923847421125371279534927286033818255650";
        string[,] tabla;
        public Aritmetica() {
        tabla = Probtable(contexto);
        }
        
        public string Encode(string mensaje)
        {
            return Encode(contexto, mensaje);
        }
        public string Decode(string mensaje)
        {
            return Decode(mensaje, contexto);
        }
        private string Encode(string contexto, string mensaje)
        {
            decimal inf=0;
            decimal sup=0;            
            decimal ant_inf=0;
            decimal ant_sup=0;            
            string splitmessage = split15(mensaje);
            int splitcount = splitmessage.Split('|').Count()-1;
            string cadenacodigo = "";
            for (int j = 0; j < splitcount; j++)
            {
                string messagepos = splitmessage.Split('|')[j];
                string[] primero = FindLetter(tabla, messagepos[0]);
                inf = decimal.Parse(primero[1]);
                sup = decimal.Parse(primero[2]);
                for (int i = 1; i < messagepos.Length; i++)
                {
                    ant_inf = inf;
                    ant_sup = sup;
                    inf = ant_inf + (ant_sup - ant_inf) * decimal.Parse(FindLetter(tabla, messagepos[i])[1]);
                    sup = ant_inf + (ant_sup - ant_inf) * decimal.Parse(FindLetter(tabla, messagepos[i])[2]);
                }
                ant_inf = inf;
                ant_sup = sup;
                cadenacodigo += Convert.ToString(decimal.Parse(FindLetter(tabla, mensaje[0])[2]) * (ant_sup - ant_inf) + ant_inf + messagepos.Length) + "|";
            }
            
            return cadenacodigo;
        }
        
        public string Decode(string info, string contexto)
        {
            int splitcount = info.Split('|').Count() - 1;
            string decodificado = "";
            for (int j = 0; j < splitcount; j++)
            {
                string aux = info.Split('|')[j];
                decimal conversion = decimal.Parse(aux);
                int longitud = (int)Math.Floor(conversion);//15.871679192298956230059392070|1.6262766901027582477014602488|
                decimal codigo = conversion - longitud;               
                for (int i = 0; i < longitud; i++)
                {
                    string[] probs = FindInRange(tabla, codigo);
                    decodificado += probs[0];
                    decimal inf = decimal.Parse(probs[1]);
                    decimal sup = decimal.Parse(probs[2]);
                    codigo = (codigo - inf) / (sup - inf);
                }
            }
            return decodificado;
    }
        public string[] FindLetter(string[,] arr, char letra)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                if (Convert.ToChar(arr[i, 0]) == letra)
                {
                    string[] probs={
                        arr[i, 0],
                        arr[i,1],
                        arr[i,2]
                    };
                    return probs;               
                }
            }
            return null;
        }
        private Dictionary<char, decimal> GetDictionary(string contexto)
        {
            Dictionary<char, decimal> miDiccionario = new Dictionary<char, decimal>();

            foreach (char c in contexto)
            {
                if (!miDiccionario.ContainsKey(c))
                {
                    miDiccionario.Add(c, 1);
                }
                else
                {
                    miDiccionario[c] += 1;
                }
            }
            decimal suma = 0;
            foreach (var kvp in miDiccionario)
            {
                suma += kvp.Value;
            }

            // Calcular las probabilidades dividiendo cada valor por la suma total
            Dictionary<char, decimal> probabilidades = new Dictionary<char, decimal>();

            foreach (var kvp in miDiccionario)
            {
                decimal probabilidad = kvp.Value / suma;
                probabilidades[kvp.Key] = probabilidad;
            }
            return probabilidades;
        }

        private string[,] Probtable(string contexto)
        {
            Dictionary<char, decimal> D = GetDictionary(contexto);
            string[,] tabla = new string[D.Count, 3];

            decimal inf = 0;
            int i = 0;

            foreach (var kvp in D)
            {
                decimal sup = inf + kvp.Value;

                // Colocar los valores en la matriz
                tabla[i, 0] = kvp.Key.ToString();  // La clave
                tabla[i, 1] = inf.ToString();      // Límite Inferior (inf) 1/300000 - x
                tabla[i, 2] = sup.ToString();      // Límite Superior (sup) 1/10     - 1

                inf = sup;
                i++;
            }
            return tabla;
        }
        private string FindTableEntry(string[,] tabla, char c)
        {
            // Busca la entrada en la tabla correspondiente al carácter c
            for (int i = 0; i < tabla.GetLength(0); i++)
            {
                if (tabla[i, 0] == c.ToString())
                {
                    return tabla[i, 0] + "," + tabla[i, 1] + "," + tabla[i, 2];
                }
            }
            return null; // Carácter no encontrado en la tabla
        }

        private string[] FindInRange(string[,] arr,decimal range)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                if (range >= decimal.Parse(arr[i, 1]) && range <= decimal.Parse(arr[i, 2]))
                {
                    string[] probs ={
                        arr[i, 0],
                        arr[i,1],
                        arr[i,2]
                    };
                    return probs;
                }               
            }
            return null;
        }
        private string split15(string mensaje)
        {
            int longitudFragmento = 10;
            string fragmento = "";
            for (int i = 0; i < mensaje.Length; i += longitudFragmento)
            {
                int longitudRestante = mensaje.Length - i;
                int longitudActual = Math.Min(longitudFragmento, longitudRestante);
                fragmento += mensaje.Substring(i, longitudActual) + "|";
            }
            return fragmento;
        }

    }
}
