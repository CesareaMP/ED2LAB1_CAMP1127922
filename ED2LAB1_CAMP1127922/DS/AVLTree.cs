using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED2LAB1_CAMP1127922.DS
{
    public class AVLTree<T> where T : IComparable<T>
    {
        private GenNode<T> root;

        public AVLTree()
        {
            root = null;
        }

        // Obtener la altura de un nodo
        private int GetHeight(GenNode<T> node)
        {
            if (node == null)
                return -1;
            return node.height;
        }

        // Rotación simple a la derecha
        private GenNode<T> RotateRight(GenNode<T> y)
        {
            GenNode<T> x = y.left;
            GenNode<T> t2 = x.right;

            x.right = y;
            y.left = t2;

            y.height = Math.Max(GetHeight(y.left), GetHeight(y.right)) + 1;
            x.height = Math.Max(GetHeight(x.left), GetHeight(x.right)) + 1;

            return x;
        }

        // Rotación simple a la izquierda
        private GenNode<T> RotateLeft(GenNode<T> x)
        {
            GenNode<T> y = x.right;
            GenNode<T> t2 = y.left;

            y.left = x;
            x.right = t2;

            x.height = Math.Max(GetHeight(x.left), GetHeight(x.right)) + 1;
            y.height = Math.Max(GetHeight(y.left), GetHeight(y.right)) + 1;

            return y;
        }

        // Balanceo y rotaciones según factor de equilibrio
        private int GetBalance(GenNode<T> node)
        {
            if (node == null)
                return 0;
            return GetHeight(node.left) - GetHeight(node.right);
        }

        private GenNode<T> BalanceNode(GenNode<T> node)
        {
            int balance = GetBalance(node);

            // Casos de desequilibrio
            if (balance > 1) // Rotación derecha
            {
                if (GetBalance(node.left) >= 0)
                    return RotateRight(node);
                else
                {
                    node.left = RotateLeft(node.left);
                    return RotateRight(node);
                }
            }
            if (balance < -1) // Rotación izquierda
            {
                if (GetBalance(node.right) <= 0)
                    return RotateLeft(node);
                else
                {
                    node.right = RotateRight(node.right);
                    return RotateLeft(node);
                }
            }
            return node;
        }

        // Insertar un elemento en el árbol
        public void Insert(T data)
        {
            root = Insert(root, data);
        }

        private GenNode<T> Insert(GenNode<T> node, T data)
        {
            if (node == null)
                return new GenNode<T>(data);

            if (data.CompareTo(node.data) < 0)
                node.left = Insert(node.left, data);
            else if (data.CompareTo(node.data) > 0)
                node.right = Insert(node.right, data);
            else // No se permiten duplicados
                return node;

            node.height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));

            return BalanceNode(node);
        }
        // Buscar un elemento en el árbol
        public bool Contains(T data)
        {
            return Contains(root, data);
        }

        private bool Contains(GenNode<T> node, T data)
        {
            if (node == null)
                return false;

            int comparison = data.CompareTo(node.data);
            if (comparison < 0)
                return Contains(node.left, data);
            else if (comparison > 0)
                return Contains(node.right, data);
            else
                return true; // Encontrado
        }

        // Eliminar un elemento del árbol
        public void Remove(T data)
        {
            root = Remove(root, data);
        }

        private GenNode<T> Remove(GenNode<T> node, T data)
        {
            if (node == null)
                return null;

            int comparison = data.CompareTo(node.data);

            if (comparison < 0)
                node.left = Remove(node.left, data);
            else if (comparison > 0)
                node.right = Remove(node.right, data);
            else
            {
                // Nodo con un solo hijo o sin hijos
                if (node.left == null || node.right == null)
                {
                    GenNode<T> temp = null;
                    if (node.left == null)
                        temp = node.right;
                    else
                        temp = node.left;

                    // No hay hijos
                    if (temp == null)
                    {
                        temp = node;
                        node = null;
                    }
                    else // Un hijo
                        node = temp;

                    temp = null;
                }
                else
                {
                    // Nodo con dos hijos: obtener sucesor in-order (nodo más pequeño en el subárbol derecho)
                    GenNode<T> temp = MinValueNode(node.right);

                    // Copiar el contenido del sucesor
                    node.data = temp.data;

                    // Eliminar el sucesor
                    node.right = Remove(node.right, temp.data);
                }
            }

            // Si el árbol tenía solo un nodo
            if (node == null)
                return node;

            // Actualizar altura
            node.height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));

            // Rebalancear el nodo
            return BalanceNode(node);
        }

        private GenNode<T> MinValueNode(GenNode<T> node)
        {
            GenNode<T> current = node;
            while (current.left != null)
                current = current.left;
            return current;
        }
    
}

}
