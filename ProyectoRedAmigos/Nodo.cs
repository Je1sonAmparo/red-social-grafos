using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Nodo: Usado en la implementación de la cola y la pila

namespace ProyectoRedAmigos
{
    public class Nodo
    {
        public int valor;
        public Nodo? siguiente;

        public Nodo(int valorDato)
        {
            valor = valorDato;
            siguiente = null;
        }
    }
}

