using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED2LAB1_CAMP1127922.DS
{
    public class GenNode<T> where T : IComparable<T>
    {
        public T data {  get; set; }
        public GenNode<T> left {  get; set; }
        public GenNode<T> right{  get; set; }
        public int height { get; set; } 

        public GenNode(T d) { 
            data = d;
            left = null;
            right= null;
            height = 0;
        }
    }
}
