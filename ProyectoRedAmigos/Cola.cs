using System;
using System.Collections.Generic;

namespace ProyectoRedAmigos
{
    // Wrapper simple sobre Queue de .NET
    public class Cola
    {
        private Queue<NodoCola> colaInterna;

        public class NodoCola
        {
            public int valor;
            public NodoCola(int v) { valor = v; }
        }

        public Cola()
        {
            colaInterna = new Queue<NodoCola>();
        }

        public void push(int x)
        {
            colaInterna.Enqueue(new NodoCola(x));
        }

        public NodoCola pop()
        {
            if (colaInterna.Count == 0) return null;
            return colaInterna.Dequeue();
        }
        
        public bool Vacia() { return colaInterna.Count == 0; }
    }
}