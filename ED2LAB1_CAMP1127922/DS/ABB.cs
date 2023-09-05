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

        private Nodo Add(Nodo nodo, Person persona)
        {
            if (nodo == null)
                return new Nodo(persona);

            if (persona.name.CompareTo(nodo.nombre) < 0)
            {
                nodo.izquierda = Add(nodo.izquierda, persona);
            }
            else if (persona.name.CompareTo(nodo.nombre) > 0)
            {
                nodo.derecha = Add(nodo.derecha, persona);
            }
            else if (persona.name.CompareTo(nodo.nombre) == 0)
            {
                nodo.AddTolist(persona);
            }

            // Recalcula el factor de equilibrio del nodo actual
            nodo.RecalcularFactorEquilibrio();

            // Realiza las rotaciones si es necesario para mantener el equilibrio
            return BalancearNodo(nodo);
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

                // Recalcula el factor de equilibrio del nodo actual
                nodo.RecalcularFactorEquilibrio();

                // Realiza las rotaciones si es necesario para mantener el equilibrio
                return BalancearNodo(nodo);
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

        private Nodo RotacionDerecha(Nodo nodo)
        {
            Nodo nuevoPadre = nodo.izquierda;
            nodo.izquierda = nuevoPadre.derecha;
            nuevoPadre.derecha = nodo;
            nodo.RecalcularFactorEquilibrio();
            nuevoPadre.RecalcularFactorEquilibrio();
            return nuevoPadre;
        }
        private Nodo RotacionIzquierda(Nodo nodo)
        {
            Nodo nuevoPadre = nodo.derecha;
            nodo.derecha = nuevoPadre.izquierda;
            nuevoPadre.izquierda = nodo;
            nodo.RecalcularFactorEquilibrio();
            nuevoPadre.RecalcularFactorEquilibrio();
            return nuevoPadre;
        }
        private Nodo RotacionDobleDerechaIzquierda(Nodo nodo)
        {
            nodo.derecha = RotacionDerecha(nodo.derecha);
            return RotacionIzquierda(nodo);
        }

        private Nodo RotacionDobleIzquierdaDerecha(Nodo nodo)
        {
            nodo.izquierda = RotacionIzquierda(nodo.izquierda);
            return RotacionDerecha(nodo);
        }
        private Nodo BalancearNodo(Nodo nodo)
        {
            // Recalcular el factor de equilibrio del nodo
            nodo.RecalcularFactorEquilibrio();

            // Verificar el factor de equilibrio del nodo
            if (nodo.FactorEquilibrio > 1)
            {
                // El subárbol izquierdo es más alto
                if (nodo.izquierda.FactorEquilibrio >= 0)
                {
                    // Rotación simple a la derecha (RR)
                    return RotacionDerecha(nodo);
                }
                else
                {
                    // Rotación doble izquierda-derecha (LR)
                    return RotacionDobleIzquierdaDerecha(nodo);
                }
            }
            else if (nodo.FactorEquilibrio < -1)
            {
                // El subárbol derecho es más alto
                if (nodo.derecha.FactorEquilibrio <= 0)
                {
                    // Rotación simple a la izquierda (LL)
                    return RotacionIzquierda(nodo);
                }
                else
                {
                    // Rotación doble derecha-izquierda (RL)
                    return RotacionDobleDerechaIzquierda(nodo);
                }
            }

            // Si el factor de equilibrio está en el rango [-1, 1], el nodo está balanceado
            return nodo;
        }

    }
}
