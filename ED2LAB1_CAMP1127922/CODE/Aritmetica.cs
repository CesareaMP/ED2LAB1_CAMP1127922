using System.Collections.Generic;

namespace ED2LAB1_CAMP1127922
{
    internal class Aritmetica
    {
        private string contexto = "ParkerIncConn-HuelsHickleZiemannandLegrosNolanLLCMcDermottCummerataandThompsonWelch-ShieldsO'HaraandSonsParisianGleichnerandCollinsMooreandSonsGottlieb-SporerSchillerFadelandGislasonRoweLLCTremblayIncSchuppeD'AmoreandHilpertDickinson-NikolausVandervort-WeimannJenkins-DouglasLindIncConnelly-SwaniawskiBlockandSonsEmardLLCDaughertyGroupHermistonLakinandJacobiSwift-VolkmanWunschLLCWitting-BeckerJonesGradyandBreitenbergZiemann-BorerUptonLLCBarrowsandSonsMaggioLLCDaniel-FraneyStehr-LangoshGaylordSchillerandMurrayPollichandSonsSchusterOlsonandDoyleTurnerLLCJacobs-FarrellLakin-AltenwerthMante-LeschKubandSonsMayerBlockandGaylordPagac-BoscoWisozk-StrosinCassinKreigerandMcKenzieCorkery-RosenbaumMarvin-LegrosGislasonGroupMertzCasperandHirtheZiemannandSons6904153067807613780749283707265854661409267524316425109972346";
        public double Encode(string mensaje)
        {
            return Encode(contexto, mensaje);
        }
        public string Decode(string mensaje)
        {
            return Decode(mensaje, contexto);
        }
        private double Encode(string contexto, string mensaje)
        {
            
        }

        public string Decode(string mensaje, string contexto)
        {
            Dictionary<char, double> probabilidadDiccionario = GetDictionary(contexto);
            string[,] tabla = Probtable(probabilidadDiccionario);

            double valor = 0;
            double rango = 1;
            string mensajeDecodificado = "";

            foreach (char c in mensaje)
            {
                for (int i = 0; i < tabla.GetLength(0); i++)
                {
                    char caracter = tabla[i, 0][0];
                    double inf = double.Parse(tabla[i, 1]);
                    double sup = double.Parse(tabla[i, 2]);

                    double nuevoRango = rango * (sup - inf);
                    if (valor >= inf * rango && valor < sup * rango)
                    {
                        mensajeDecodificado += caracter;
                        valor = (valor - inf * rango) / (sup - inf);
                        rango = nuevoRango;
                        break;
                    }
                }
            }

            return mensajeDecodificado;
        }


        private Dictionary<char, double> GetDictionary(string contexto)
        {
            Dictionary<char, double> miDiccionario = new Dictionary<char, double>();

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
            double suma = 0;
            foreach (var kvp in miDiccionario)
            {
                suma += kvp.Value;
            }

            // Calcular las probabilidades dividiendo cada valor por la suma total
            Dictionary<char, double> probabilidades = new Dictionary<char, double>();

            foreach (var kvp in miDiccionario)
            {
                double probabilidad = kvp.Value / suma;
                probabilidades[kvp.Key] = probabilidad;
            }
            return probabilidades;
        }

        private string[,] Probtable(Dictionary<char, double> D)
        {
            string[,] tabla = new string[D.Count, 3];
            double inf = 0;
            int i = 0;

            foreach (var kvp in D)
            {
                double sup = inf + kvp.Value;

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
    }
}
