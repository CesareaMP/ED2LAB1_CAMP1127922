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
        public int altura { get; set; }
        
        public Nodo(Person p) {                        
            persona = p;    
            izquierda =null; 
            derecha=null;
            altura = 0;
        }        
    }
}
