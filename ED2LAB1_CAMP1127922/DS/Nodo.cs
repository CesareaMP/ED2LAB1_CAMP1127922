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
        public string nombre { get; set; }
        public List<Person> persona = new List<Person>();
        public Nodo izquierda { get; set; }
        public Nodo derecha { get; set; }
        public int FactorEquilibrio { get; set; }
        public Nodo(Person p) {
            nombre=p.name;
            persona.Add(p);          
            izquierda=null; 
            derecha=null;
            FactorEquilibrio = 0;
        }
        public void AddTolist(Person p)
        {
            persona.Add(p);
            persona.Sort((person1, person2) => person1.dpi.CompareTo(person2.dpi));
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

        public void Deletefrom(Person p)
        {
            persona.RemoveAll(person => person.dpi == p.dpi);                  
        }
        public void PatchData(Person p)
        {
            int cantidad = persona.Count();

            for (int i = 0; i < cantidad; i++) { 
                Person aux = persona[i];
            if (aux.dpi == p.dpi)
                {
                    aux.datebirth = p.datebirth;
                    aux.address= p.address;
                    Deletefrom(p);
                    AddTolist(aux);
                    i = cantidad;
                }
            }
        }


    }
}
