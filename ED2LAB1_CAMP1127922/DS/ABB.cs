using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ED2LAB1_CAMP1127922.DS
{
    internal class ABB
    {
        public Nodo raiz;
        public void Add(Person ndato)
        {
            raiz = Add(raiz, ndato);
        }
        public void Delete(Person persona)
        {
            raiz = Delete(raiz, persona.dpi);            
        }
        public void Patch(Person ndato)
        {
            Patch(raiz, ndato);
        }
        public Person SearchDpi(string dpi)
        {
            return SearchDpi(raiz, dpi);
        }
        public List<Person> SearchName(string nombre)
        {
           return RecorrerArbolYBuscar(raiz, nombre);
        }
        public List<string> PrintTree()
        {
            return PrintTree(raiz);
        }       
        private Nodo Add(Nodo nodo, Person nuevaPersona)
        {
            if (nodo == null)
            {
                // Si el nodo actual es nulo, crea un nuevo nodo con la nueva persona.
                return new Nodo(nuevaPersona);
            }

            // Compara el DPI de la nueva persona con el DPI del nodo actual.
            int comparacion = nuevaPersona.dpi.CompareTo(nodo.persona.dpi);

            if (comparacion < 0)
            {
                // El DPI de la nueva persona es menor, inserta en el subárbol izquierdo.
                nodo.izquierda = Add(nodo.izquierda, nuevaPersona);
            }
            else if (comparacion > 0)
            {
                // El DPI de la nueva persona es mayor, inserta en el subárbol derecho.
                nodo.derecha = Add(nodo.derecha, nuevaPersona);
            }
            else
            {
                // El DPI ya existe en el árbol (no se permite duplicados), no se hace nada.
                return nodo;
            }

            // Recalcula el factor de equilibrio y realiza las rotaciones si es necesario
            nodo.RecalcularFactorEquilibrio();
            return BalancearNodo(nodo);
        }

        public void Delete(string dpiBuscado)
        {
            raiz = Delete(raiz, dpiBuscado);
        }

        private Nodo Delete(Nodo nodo, string dpiBuscado)
        {
            if (nodo == null)
            {
                // El nodo no se encuentra en el árbol, no se hace nada.
                return nodo;
            }

            // Compara el DPI del nodo actual con el DPI buscado
            int comparacion = dpiBuscado.CompareTo(nodo.persona.dpi);

            if (comparacion < 0)
            {
                // El DPI deseado está en el subárbol izquierdo.
                nodo.izquierda = Delete(nodo.izquierda, dpiBuscado);
            }
            else if (comparacion > 0)
            {
                // El DPI deseado está en el subárbol derecho.
                nodo.derecha = Delete(nodo.derecha, dpiBuscado);
            }
            else
            {
                // Se encontró el nodo con el DPI deseado para eliminar.
                if (nodo.izquierda == null || nodo.derecha == null)
                {
                    // Si el nodo tiene 0 o 1 hijo, simplemente se elimina.
                    nodo = (nodo.izquierda != null) ? nodo.izquierda : nodo.derecha;
                }
                else
                {
                    // Si el nodo tiene 2 hijos, se busca el sucesor inmediato (nodo con el DPI más bajo en el subárbol derecho).
                    Nodo sucesor = EncontrarMinimo(nodo.derecha);
                    nodo.persona.dpi = sucesor.persona.dpi;
                    nodo.derecha = Delete(nodo.derecha, sucesor.persona.dpi);
                }
            }
            if (nodo != null)
            {
                nodo.RecalcularFactorEquilibrio();
            }
            // Aplica las rotaciones necesarias y retorna el nodo balanceado
            return BalancearNodo(nodo);
        }



        private void Patch(Nodo nodo, Person persona)
        {
            if (nodo != null)
            {
                if (persona.dpi.CompareTo(nodo.persona.dpi) == 0)
                {
                    nodo.persona.name = persona.name;
                    nodo.persona.datebirth = persona.datebirth;
                    nodo.persona.address = persona.address;
                    nodo.persona.companies = persona.companies;
                }
                else if (persona.dpi.CompareTo(nodo.persona.dpi) < 0)
                {
                    // El DPI deseado está en el subárbol izquierdo.
                    Patch(nodo.izquierda, persona);
                }
                else
                {
                    // El DPI deseado está en el subárbol derecho.
                    Patch(nodo.derecha, persona);
                }
            }
        }

        private Person SearchDpi(Nodo node, string dpi)
        {
            if (node == null)
            {
                return null; // El árbol está vacío o no se encontró el DPI.
            }

            if (dpi.CompareTo(node.persona.dpi) == 0)
            {
                // Encontramos el nodo con el DPI deseado.
                return node.persona;
            }
            else if (dpi.CompareTo(node.persona.dpi) < 0)
            {
                // El DPI deseado está en el subárbol izquierdo.
                return SearchDpi(node.izquierda, dpi);
            }
            else
            {
                // El DPI deseado está en el subárbol derecho.
                return SearchDpi(node.derecha, dpi);
            }
        }
        public List<Person> RecorrerArbolYBuscar(Nodo nodo, string nombreBuscado)
        {
            List<Person> personasEncontradas = new List<Person>();

            if (nodo != null)
            {
                personasEncontradas.AddRange(RecorrerArbolYBuscar(nodo.izquierda, nombreBuscado)); // Recorre el subárbol izquierdo

                // Compara el nombre del nodo actual con el nombre buscado
                if (nodo.persona.name == nombreBuscado)
                {
                    personasEncontradas.Add(nodo.persona); // Agrega la persona a la lista si coincide el nombre
                }

                personasEncontradas.AddRange(RecorrerArbolYBuscar(nodo.derecha, nombreBuscado)); // Recorre el subárbol derecho
            }

            return personasEncontradas;
        }
        private List<string> PrintTree(Nodo nodo)
        {
            List<string> Tree = new List<string>();
            if (nodo != null)
            {
                Tree.AddRange(PrintTree(nodo.izquierda)); // Recorre el subárbol izquierdo                
                    Tree.Add(JsonConvert.SerializeObject(nodo.persona, Formatting.None)); // Agrega la persona a la lista
                Tree.AddRange(PrintTree(nodo.derecha)); // Recorre el subárbol derecho
            }
            return Tree;
        }      

        private Nodo EncontrarMinimo(Nodo nodo)
        {
            while (nodo.izquierda != null)
            {
                nodo = nodo.izquierda;
            }
            return nodo;
        }
        private Nodo BalancearNodo(Nodo nodo)
        {
            // Recalcula el factor de equilibrio del nodo
            int factorEquilibrio = CalcularFactorEquilibrio(nodo);

            if (factorEquilibrio > 1)
            {
                // Desbalance a la izquierda
                if (CalcularFactorEquilibrio(nodo.izquierda) >= 0)
                {
                    // Rotación simple a la derecha (RR)
                    return RotacionRR(nodo);
                }
                else
                {
                    // Rotación doble izquierda-derecha (LR)
                    return RotacionLR(nodo);
                }
            }
            else if (factorEquilibrio < -1)
            {
                // Desbalance a la derecha
                if (CalcularFactorEquilibrio(nodo.derecha) <= 0)
                {
                    // Rotación simple a la izquierda (LL)
                    return RotacionLL(nodo);
                }
                else
                {
                    // Rotación doble derecha-izquierda (RL)
                    return RotacionRL(nodo);
                }
            }

            // No se necesita balanceo, retorna el nodo sin cambios
            return nodo;
        }


        private int CalcularFactorEquilibrio(Nodo nodo)
        {
            if (nodo == null)
            {
                return 0;
            }

            int alturaIzquierda = (nodo.izquierda != null) ? nodo.izquierda.FactorEquilibrio : 0;
            int alturaDerecha = (nodo.derecha != null) ? nodo.derecha.FactorEquilibrio : 0;

            return alturaIzquierda - alturaDerecha;
        }

        private Nodo RotacionLL(Nodo nodo)
        {
            if (nodo == null || nodo.izquierda == null || nodo.izquierda.derecha == null)
            {
                // No se puede realizar la rotación, retorna el nodo original
                return nodo;
            }

            Nodo nuevaRaiz = nodo.izquierda;
            nodo.izquierda = nuevaRaiz.derecha;
            nuevaRaiz.derecha = nodo;
            nodo.RecalcularFactorEquilibrio();
            nuevaRaiz.RecalcularFactorEquilibrio();
            return nuevaRaiz;
        }

        private Nodo RotacionRR(Nodo nodo)
        {
            if (nodo == null || nodo.izquierda == null)
            {
                // No se puede realizar la rotación, retorna el nodo original
                return nodo;
            }

            Nodo nuevaRaiz = nodo.izquierda;
            nodo.izquierda = nuevaRaiz.derecha;
            nuevaRaiz.derecha = nodo;
            nodo.RecalcularFactorEquilibrio();
            nuevaRaiz.RecalcularFactorEquilibrio();
            return nuevaRaiz;
        }

        private Nodo RotacionLR(Nodo nodo)
        {
            nodo.izquierda = RotacionRR(nodo.izquierda);
            return RotacionLL(nodo);
        }
        private Nodo RotacionRL(Nodo nodo)
        {
            nodo.derecha = RotacionLL(nodo.derecha);
            return RotacionRR(nodo);
        }

    }
}
