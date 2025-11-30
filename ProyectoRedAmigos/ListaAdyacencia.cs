using System;

namespace ProyectoRedAmigos
{
    public class NodoAdyacencia
    {
        public int dato; // Índice del usuario destino
        public int peso; // Peso de la arista
        public NodoAdyacencia siguiente;

        public NodoAdyacencia(int d, int p = 1)
        {
            dato = d;
            peso = p;
            siguiente = null;
        }
    }

    public class ListaAdyacencia
    {
        private NodoAdyacencia[] tabla;

        public ListaAdyacencia(int n)
        {
            tabla = new NodoAdyacencia[n];
            for (int i = 0; i < n; i++)
                tabla[i] = null;
        }

        public void Inserta(int origen, int destino)
        {
            NodoAdyacencia nuevo = new NodoAdyacencia(destino);
            nuevo.siguiente = tabla[origen];
            tabla[origen] = nuevo;
        }

        public void InsertaConPeso(int origen, int destino, int peso)
        {
            NodoAdyacencia nuevo = new NodoAdyacencia(destino, peso);
            nuevo.siguiente = tabla[origen];
            tabla[origen] = nuevo;
        }

        public void Elimina(int origen, int destino)
        {
            if (tabla[origen] == null) return;

            if (tabla[origen].dato == destino)
            {
                tabla[origen] = tabla[origen].siguiente;
                return;
            }

            NodoAdyacencia actual = tabla[origen];
            while (actual.siguiente != null)
            {
                if (actual.siguiente.dato == destino)
                {
                    actual.siguiente = actual.siguiente.siguiente;
                    return;
                }
                actual = actual.siguiente;
            }
        }

        public NodoAdyacencia Primero(int v)
        {
            return tabla[v];
        }

        public NodoAdyacencia Siguiente(NodoAdyacencia n)
        {
            return n.siguiente;
        }
    }
}