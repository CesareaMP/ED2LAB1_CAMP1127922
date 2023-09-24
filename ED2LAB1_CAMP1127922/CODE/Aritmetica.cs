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
        private string contexto = "Parker Inc, Conn - Huels, Hickle Ziemann and Legros, Nolan LLC, McDermott Cummerata and Thompson, Welch - Shields, O'Hara and Sons, Parisian Gleichner and Collins, Moore and Sons, Gottlieb - Sporer, Schiller Fadel and Gislason, Rowe LLC, Tremblay Inc, Schuppe D'Amore and Hilpert, Dickinson - Nikolaus, Vandervort - Weimann, Jenkins - Douglas, Lind Inc, Connelly - Swaniawski, Block and Sons, Emard LLC, Daugherty Group, Hermiston Lakin and Jacobi, Swift - Volkman, Wunsch LLC, Witting - Becker, Jones Grady and Breitenberg, Ziemann - Borer, Upton LLC, Barrows and Sons, Maggio LLC, Daniel - Franey, Stehr - Langosh, Gaylord Schiller and Murray, Pollich and Sons, Schuster Olson and Doyle, Turner LLC, Jacobs - Farrell, Lakin - Altenwerth, Mante - Lesch, Kub and Sons, Mayer Block and Gaylord, Pagac - Bosco, Wisozk - Strosin, Cassin Kreiger and McKenzie, Corkery - Rosenbaum, Marvin - Legros, Gislason Group, Mertz Casper and Hirthe, Ziemann and Sons, 69041530678076137807492837072658546614092675243164251099723466904153067807613780749283707265854661409267524316425109972346";
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
            string[,] tabla = Probtable(contexto);
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
                string[,] tablaprobs = Probtable(contexto);
                for (int i = 0; i < longitud; i++)
                {
                    string[] probs = FindInRange(tablaprobs, codigo);
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
            int longitudFragmento = 15;
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
