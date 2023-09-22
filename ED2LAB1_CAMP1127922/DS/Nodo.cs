using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED2LAB1_CAMP1127922.DS
{
    public class Nodo
    {
        public Person persona { get; set; }
        public Nodo izquierda { get; set; }
        public Nodo derecha { get; set; }
        public int FactorEquilibrio { get; set; }
        
        public Nodo(Person p) {                        
            persona = p;    
            izquierda =null; 
            derecha=null;
            FactorEquilibrio = 0;
        }
        public void RecalcularFactorEquilibrio()
        {
            int alturaIzquierda = (izquierda != null) ? Altura(izquierda) : -1;
            int alturaDerecha = (derecha != null) ? Altura(derecha) : -1;
            FactorEquilibrio = alturaIzquierda - alturaDerecha;
        }

        private int Altura(Nodo nodo)
        {
            if (nodo == null)
                return -1;

            int alturaIzquierda = Altura(nodo.izquierda);
            int alturaDerecha = Altura(nodo.derecha);

            return Math.Max(alturaIzquierda, alturaDerecha) + 1;
        }


    }
}
