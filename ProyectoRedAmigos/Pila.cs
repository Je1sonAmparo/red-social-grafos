using System;
using System.Collections.Generic;

namespace ProyectoRedAmigos
{
    // Wrapper simple sobre Stack de .NET
    public class Pila
    {
        private Stack<NodoPila> pilaInterna;

        public class NodoPila
        {
            public int valor;
            public NodoPila(int v) { valor = v; }
        }

        public Pila()
        {
            pilaInterna = new Stack<NodoPila>();
        }

        public void Push(int x)
        {
            pilaInterna.Push(new NodoPila(x));
        }

        public NodoPila Pop()
        {
            if (pilaInterna.Count == 0) return null;
            return pilaInterna.Pop();
        }

        public bool Vacia() { return pilaInterna.Count == 0; }
    }
}