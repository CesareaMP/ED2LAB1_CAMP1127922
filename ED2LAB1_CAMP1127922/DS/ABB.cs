using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ED2LAB1_CAMP1127922.DS
{
    public class ABB
    {
        public Nodo raiz;
        public void Add(Person ndato)
        {
            raiz = Add(raiz, ndato);
        }
        public void Delete(Person ndato)
        {
            raiz = Delete(raiz, ndato);
        }
        public void Patch(Person ndato)
        {
            raiz=Patch(raiz, ndato);
        }

        private Nodo Add (Nodo node, Person persona)
        {
            if (node == null) return new Nodo(persona);
            else if (persona.name.CompareTo(node.nombre) < 0) node.izquierda = Add(node.izquierda, persona);
            else if (persona.name.CompareTo(node.nombre) > 0) node.derecha = Add(node.derecha, persona);
            else if (persona.name.CompareTo(node.nombre) == 0) node.AddTolist(persona);
            return node;
        }
       private Nodo Delete(Nodo node, Person persona)
        {
            if (persona.name.CompareTo(node.nombre) == 0)
            {
                node.Deletefrom(persona);                
                return node;       
            }
            else if (persona.name.CompareTo(node.nombre) < 0) node.izquierda = Delete(node.izquierda, persona);
            else if (persona.name.CompareTo(node.nombre) > 0) node.derecha = Delete(node.derecha, persona);
            return node;
        }
        private Nodo Patch(Nodo node, Person persona)
        {

            if (node == null) return new Nodo(persona);
            else if (persona.name.CompareTo(node.nombre) < 0) node.izquierda = Add(node.izquierda, persona);
            else if (persona.name.CompareTo(node.nombre) > 0) node.derecha = Add(node.derecha, persona);
            else if (persona.name.CompareTo(node.nombre) == 0) node.PatchData(persona);
            return node;
        }
    
        private bool eq(string a, string b)
        {
            if (a == b) return true;
            else return false;
        }
    }
}
