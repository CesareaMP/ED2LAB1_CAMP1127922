using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED2LAB1_CAMP1127922.ENC
{
    internal class Crypt
    {
        public string Encrypt(string mensaje, string clave)
        {
            List<int> permutacion = ObtainPerm(clave);            
            return Permutation(permutacion,mensaje);
        }
        public string Decrypt(string mensaje, string clave)
        {
            List<int> permutacion = ObtainPerm(clave);
            return InversePermutation(permutacion, mensaje);
        }
        private List<int> ObtainPerm(string clave)
        {
            int longitudDeseada = 8;

            // Paso 1: Dividir la frase en subcadenas de 8 caracteres (completando con espacios)
            List<string> subcadenas = new List<string>();
            for (int i = 0; i < clave.Length; i += longitudDeseada)
            {
                string subcadena = clave.Substring(i, Math.Min(longitudDeseada, clave.Length - i));
                if (subcadena.Length < longitudDeseada)
                {
                    subcadena += clave.Substring(0,longitudDeseada-subcadena.Length);
                }
                subcadenas.Add(subcadena);
            }

            // Paso 2: Crear una tupla con el índice y el valor ASCII de cada subcadena
            List<(int, int)> tuplaSubcadenas = subcadenas
            .Select((subcadena, index) => (index, GetASCIISum(subcadena)))
            .ToList();
            tuplaSubcadenas = tuplaSubcadenas.OrderBy(t => t.Item2).ToList();
            List<int> permu = tuplaSubcadenas
            .Select(tupla => tupla.Item1)
            .ToList();
            return permu;
        }

        private string Permutation(List<int> permu, string mensaje)
        {
            string mensajepermutado = "";
            int longitudpermu = permu.Count();

            for (int i = 0; i < mensaje.Length; i += longitudpermu)
            {
                string subcadena = mensaje.Substring(i, Math.Min(permu.Count(), mensaje.Length - i));

                if (subcadena.Length==permu.Count())
                {
                    foreach (int t in permu)
                    {
                        if (t >= 0 && t < subcadena.Length)
                        {
                            mensajepermutado += subcadena[t];
                        }
                    }
                }
                else
                {
                    List<int> subpermu = new List<int>();
                    for (int t = 0; t < subcadena.Count(); t++)
                    {
                        if (permu[t]<subcadena.Length)
                        {
                            subpermu.Add(permu[t]);
                        }
                    }
                    foreach (int t in permu)
                    {
                        if (t >= 0 && t < subcadena.Length)
                        {
                            mensajepermutado += subcadena[t];
                        }
                    }
                }                
            }

            return mensajepermutado;
        }

        private string InversePermutation(List<int> permu, string mensaje)
        {
            string mensajeDespermutado = "";
            int longitudpermu = permu.Count();

            for (int i = 0; i < mensaje.Length; i += longitudpermu)
            {
                string subcadena = mensaje.Substring(i, Math.Min(longitudpermu, mensaje.Length - i));

                if (subcadena.Length == permu.Count())
                {
                    for (int t = 0; t < longitudpermu; t++)
                    {
                        int indexon = permu.IndexOf(t);
                        mensajeDespermutado += subcadena[indexon];
                    }
                }
                else
                {
                    List<int> subpermu = new List<int>();
                    for (int t = 0; t < permu.Count(); t++)
                    {
                        if (permu[t] < subcadena.Length)
                        {
                            subpermu.Add(permu[t]);
                        }
                    }
                    for (int t = 0; t < subpermu.Count(); t++)
                    {
                        int indexon = subpermu.IndexOf(t);
                        mensajeDespermutado += subcadena[indexon];
                    }

                }

            }

            return mensajeDespermutado;
        }


        private int GetASCIISum(string input)
        {
            int suma = 0;
            foreach (char c in input)
            {
                suma += (int)c;
            }
            return suma;
        }
    }
}
