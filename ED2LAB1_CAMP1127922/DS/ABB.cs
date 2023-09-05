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
            raiz = Patch(raiz, ndato);
        }
        public List<Person> Search(string nombre)
        {
            return Search(raiz, nombre);
        }
  

        private Nodo Add (Nodo node, Person persona)
        {
            if (node == null) return new Nodo(persona);
            else if (persona.name.CompareTo(node.nombre) < 0) node.izquierda = Add(node.izquierda, persona);
            else if (persona.name.CompareTo(node.nombre) > 0) node.derecha = Add(node.derecha, persona);
            else if (persona.name.CompareTo(node.nombre) == 0) node.AddTolist(persona);
            return node;
        }
        private Nodo Delete(Nodo nodo, Person persona)
        {
            if (nodo != null)
            {
                if (persona.name.CompareTo(nodo.nombre) == 0)
                {
                    nodo.Deletefrom(persona);

                    if (nodo.persona.Count == 0)
                    {
                        // Si el nodo se queda sin datos, eliminar el nodo y buscar reemplazo
                        if (nodo.izquierda == null)
                            return nodo.derecha;
                        else if (nodo.derecha == null)
                            return nodo.izquierda;

                        Nodo reemplazo = EncontrarMinimo(nodo.derecha);
                        nodo.nombre = reemplazo.nombre;
                        nodo.persona = reemplazo.persona;
                        nodo.derecha = Delete(nodo.derecha, reemplazo.persona[0]);
                    }
                }
                else if (persona.name.CompareTo(nodo.nombre) < 0)
                    nodo.izquierda = Delete(nodo.izquierda, persona);
                else if (persona.name.CompareTo(nodo.nombre) > 0)
                    nodo.derecha = Delete(nodo.derecha, persona);
            }
            return nodo;
        }
        private Nodo Patch(Nodo node, Person persona)
        {

            if (node == null) return new Nodo(persona);
            else if (persona.name.CompareTo(node.nombre) < 0) node.izquierda = Patch(node.izquierda, persona);
            else if (persona.name.CompareTo(node.nombre) > 0) node.derecha = Patch(node.derecha, persona);
            else if (persona.name.CompareTo(node.nombre) == 0) node.PatchData(persona);
            return node;
        }
    
        private bool eq(string a, string b)
        {
            if (a == b) return true;
            else return false;
        }
        public List<Person> Search(Nodo node, string nombre)
        {
            if (node!=null)
            {
                if (nombre == node.nombre)
                {
                    return node.persona;
                }
                else if (nombre.CompareTo(node.nombre) > 0) return Search(node.derecha, nombre);
                else if (nombre.CompareTo(node.nombre) < 0) return Search(node.izquierda, nombre);
            }
            return null;
        }
        private Nodo EncontrarMinimo(Nodo nodo)
        {
            while (nodo.izquierda != null)
            {
                nodo = nodo.izquierda;
            }
            return nodo;
        }


    }
}
