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
            int balance = FactorBalance(nodo);

            if (balance > 1 && nuevaPersona.dpi.CompareTo(nodo.izquierda.persona.dpi) < 0)
                return RotacionDerecha(nodo);

            if (balance < -1 && nuevaPersona.dpi.CompareTo(nodo.derecha.persona.dpi) > 0)
                return RotacionIzquierda(nodo);

            if (balance > 1 && nuevaPersona.dpi.CompareTo(nodo.izquierda.persona.dpi) > 0)
            {
                nodo.izquierda = RotacionIzquierda(nodo.izquierda);
                return RotacionDerecha(nodo);
            }

            if (balance < -1 && nuevaPersona.dpi.CompareTo(nodo.derecha.persona.dpi) < 0)
            {
                nodo.derecha = RotacionDerecha(nodo.derecha);
                return RotacionIzquierda(nodo);
            }

            return nodo;
        }

        private int Altura(Nodo nodo)
        {
            if (nodo == null) { return 0; }


            return nodo.altura;
        }
        //obtener el balance
        private int FactorBalance(Nodo nodo)
        {
            if (nodo == null)
                return 0;

            return Altura(nodo.izquierda) - Altura(nodo.derecha);
        }

        public void Delete(string dpiBuscado)
        {
            raiz = Delete(raiz, dpiBuscado);
        }

        private Nodo Delete(Nodo nodo, string dpiBuscado)
        {
            if (nodo == null)
            {
                return nodo;
            }

            // Realiza la eliminación recursiva como en un árbol binario de búsqueda
            if (dpiBuscado.CompareTo(nodo.persona.dpi) < 0)
            {
                nodo.izquierda = Delete(nodo.izquierda, dpiBuscado);
            }
            else if (dpiBuscado.CompareTo(nodo.persona.dpi) > 0)
            {
                nodo.derecha = Delete(nodo.derecha, dpiBuscado);
            }
            else
            {
                // Nodo encontrado, manejar los tres casos

                // Caso 1: El nodo a eliminar es una hoja
                if (nodo.izquierda == null && nodo.derecha == null)
                {
                    nodo = null;
                }
                // Caso 2: El nodo a eliminar tiene un solo hijo
                else if (nodo.izquierda == null)
                {
                    nodo = nodo.derecha;
                }
                else if (nodo.derecha == null)
                {
                    nodo = nodo.izquierda;
                }
                // Caso 3: El nodo a eliminar tiene dos hijos
                else
                {
                    // Encuentra el sucesor inorden (el nodo más pequeño en el subárbol derecho)
                    Nodo sucesor = EncontrarSucesor(nodo.derecha);

                    // Copia los datos del sucesor al nodo actual
                    nodo.persona = sucesor.persona;

                    // Elimina el sucesor
                    nodo.derecha = Delete(nodo.derecha, sucesor.persona.dpi);
                }
            }

            if (nodo == null)
            {
                return nodo;
            }
            // Actualizar altura y realizar rotaciones si es necesario
            nodo.altura = 1 + Math.Max(Altura(nodo.izquierda), Altura(nodo.derecha));
            int balance = FactorBalance(nodo);

            if (balance > 1 && FactorBalance(nodo.izquierda) >= 0)
                return RotacionDerecha(nodo);

            if (balance > 1 && FactorBalance(nodo.izquierda) < 0)
            {
                nodo.izquierda = RotacionIzquierda(nodo.izquierda);
                return RotacionDerecha(nodo);
            }

            if (balance < -1 && FactorBalance(nodo.derecha) <= 0)
                return RotacionIzquierda(nodo);

            if (balance < -1 && FactorBalance(nodo.derecha) > 0)
            {
                nodo.derecha = RotacionDerecha(nodo.derecha);
                return RotacionIzquierda(nodo);
            }

            return nodo;
        }

        private Nodo EncontrarSucesor(Nodo nodo)
        {
            Nodo actual = nodo;
            while (actual.izquierda != null)
            {
                actual = actual.izquierda;
            }
            return actual;
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
        private Nodo RotacionIzquierda(Nodo nodo)
        {
            Nodo nododerecha = nodo.derecha;
            Nodo nodoizquierda = nododerecha.izquierda;

            nododerecha.izquierda = nodo;
            nodo.derecha = nodoizquierda;

            nodo.altura = 1 + Math.Max(Altura(nodo.izquierda), Altura(nodo.derecha));
            nododerecha.altura = 1 + Math.Max(Altura(nododerecha.izquierda), Altura(nododerecha.derecha));

            return nododerecha;
        }
        private Nodo RotacionDerecha(Nodo nodo)
        {
            Nodo nodoizquierda = nodo.izquierda;
            Nodo nododerecha = nodoizquierda.derecha;

            nodoizquierda.derecha = nodo;
            nodo.izquierda = nododerecha;

            nodo.altura = 1 + Math.Max(Altura(nodo.izquierda), Altura(nodo.derecha));
            nodoizquierda.altura = 1 + Math.Max(Altura(nodoizquierda.izquierda), Altura(nodoizquierda.derecha));

            return nodoizquierda;
        }

    }
}
