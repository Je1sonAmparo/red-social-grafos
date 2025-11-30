using System;
using System.Collections.Generic;

namespace ProyectoRedAmigos
{
    public class GrafoAmigos
    {
        private int maxUsuarios;
        private int contadorUsuarios;
        private Usuario[] usuarios;
        private int[,] matrizAdyacencia;
        private ListaAdyacencia listaAdyacencia;

        public GrafoAmigos(int max)
        {
            maxUsuarios = max;
            contadorUsuarios = 0;
            usuarios = new Usuario[maxUsuarios];
            matrizAdyacencia = new int[maxUsuarios, maxUsuarios];

            for (int i = 0; i < maxUsuarios; i++)
                for (int j = 0; j < maxUsuarios; j++)
                    matrizAdyacencia[i, j] = 0;

            listaAdyacencia = new ListaAdyacencia(maxUsuarios);
        }

        // --- MÉTODOS BÁSICOS DE GESTIÓN (Source 1) ---

        public int ObtenerIndice(string nombre)
        {
            int i;
            bool encontrado = false;
            for (i = 0; (i < contadorUsuarios) && !encontrado;)
            {
                encontrado = usuarios[i].Igual(new Usuario(nombre));
                if (!encontrado) i++;
            }
            return (i < contadorUsuarios) ? i : -1;
        }

        public int ObtenerIndice(Usuario u)
        {
            return ObtenerIndice(u.Nombre);
        }

        public void AgregarUsuario(string nombre)
        {
            bool esta = ObtenerIndice(nombre) >= 0;
            if (!esta && contadorUsuarios < maxUsuarios)
            {
                Usuario u = new Usuario(nombre, contadorUsuarios);
                usuarios[contadorUsuarios] = u;
                contadorUsuarios++;
            }
        }

        public void AgregarUsuario(Usuario u)
        {
            AgregarUsuario(u.Nombre);
        }

        public void BorrarUsuario(Usuario u)
        {
            int indiceBorrar = ObtenerIndice(u);

            if (indiceBorrar == -1)
            {
                Console.WriteLine($"Error: El usuario {u.Nombre} no existe.");
                return;
            }

            // 1. Eliminar referencias en la Lista de Adyacencia de TODOS los demás vértices
            for (int i = 0; i < contadorUsuarios; i++)
            {
                // El usuario 'i' ya no puede ser amigo del usuario a borrar (indiceBorrar)
                listaAdyacencia.Elimina(i, indiceBorrar);
            }

            // 2. Desplazar el Array de Usuarios y la Matriz de Adyacencia
            for (int i = indiceBorrar; i < contadorUsuarios - 1; i++)
            {
                // Desplaza el objeto Usuario
                usuarios[i] = usuarios[i + 1];

                // *** Desplazar la Matriz de Adyacencia ***
                // Desplaza la FILA (conectividad saliente)
                for (int j = 0; j < contadorUsuarios; j++)
                {
                    matrizAdyacencia[i, j] = matrizAdyacencia[i + 1, j];
                }

                // Desplaza la COLUMNA (conectividad entrante)
                for (int j = 0; j < contadorUsuarios; j++)
                {
                    matrizAdyacencia[j, i] = matrizAdyacencia[j, i + 1];
                }
            }

            // 3. Actualizar contadores y el último elemento
            contadorUsuarios--;
            usuarios[contadorUsuarios] = null;

            Console.WriteLine($"Usuario {u.Nombre} y todas sus amistades han sido eliminados de la red.");
        }

        // --- GESTIÓN DE ARCOS / AMISTADES (Unificado) ---

        public void NuevoArco(int va, int vb)
        {
            if (va < 0 || vb < 0 || va > contadorUsuarios || vb > contadorUsuarios)
                throw new Exception("Usuario no existe");

            matrizAdyacencia[va, vb] = 1;
            listaAdyacencia.Inserta(va, vb);
        }

        public bool EsAdyacente(Usuario u, Usuario v)
        {
            int i = ObtenerIndice(u);
            int j = ObtenerIndice(v);

            if (i == -1 || j == -1) return false;

            if (matrizAdyacencia[i, j] > 0) return true;

            return false;
        }

        public void BorrarArco(Usuario a, Usuario b)
        {
            int i = ObtenerIndice(a);
            int j = ObtenerIndice(b);

            if (i == -1 || j == -1)
            {
                Console.WriteLine($"Error: Uno o ambos usuarios no existen ({a.Nombre} o {b.Nombre}).");
                return;
            }

            // En un grafo de amistad (no dirigido), borramos la conexión en ambos sentidos.
            matrizAdyacencia[i, j] = 0; 
            matrizAdyacencia[j, i] = 0; 

            listaAdyacencia.Elimina(i, j); 
            listaAdyacencia.Elimina(j, i); 

            Console.WriteLine($"Conexión de amistad eliminada entre {a.Nombre} y {b.Nombre}.");
        }

        // OPCIÓN 1: Para grafos Dirigidos 
        public void AgregarDirigida(Usuario origen, Usuario destino)
        {
            int i = ObtenerIndice(origen);
            int j = ObtenerIndice(destino);

            if (i != -1 && j != -1)
            {
                NuevoArco(i, j); 
            }
            else
            {
                Console.WriteLine($"Error: No se pudo conectar {origen.Nombre} con {destino.Nombre}");
            }
        }

        // OPCIÓN 2: Para grafos No Dirigidos (Alias para AgregarAmistad)
        public void AgregarNoDirigida(Usuario a, Usuario b)
        {
            AgregarAmistad(a, b);
        }

        // Integración del método AgregarAmistad de los otros archivos
        public void AgregarAmistad(Usuario a, Usuario b)
        {
            int i = ObtenerIndice(a);
            int j = ObtenerIndice(b);

            if (i != -1 && j != -1)
            {
                // Actualiza Matriz y Lista (usando la lógica completa de Source 1)
                NuevoArco(i, j); // A -> B
                NuevoArco(j, i); // B -> A
            }
            else
            {
                Console.WriteLine("Uno de los usuarios no existe.");
            }
        }

        // OPCIÓN 3: Grafo no dirigido Valorado
        public void AgregarAmistadValorada(Usuario a, Usuario b, int nivelAmistad)
        {
            int i = ObtenerIndice(a);
            int j = ObtenerIndice(b);

            if (i != -1 && j != -1)
            {
                matrizAdyacencia[i, j] = nivelAmistad;
                matrizAdyacencia[j, i] = nivelAmistad; 

                listaAdyacencia.InsertaConPeso(i, j, nivelAmistad);
                listaAdyacencia.InsertaConPeso(j, i, nivelAmistad); 
            }
        }

        // --- MÉTODOS DE VISUALIZACIÓN ---

        public void MostrarListaConPesos()
        {
            Console.WriteLine("\n=== AMISTADES Y SU INTENSIDAD ===");
            for (int i = 0; i < contadorUsuarios; i++)
            {
                Console.Write($"{usuarios[i].Nombre} tiene de amigos a: ");
                NodoAdyacencia actual = listaAdyacencia.Primero(i);

                while (actual != null)
                {
                    int indiceAmigo = actual.dato;
                    int pesoAmistad = actual.peso; 

                    Console.Write($"[{usuarios[indiceAmigo].Nombre} (Nivel: {pesoAmistad})] ");

                    actual = listaAdyacencia.Siguiente(actual);
                }
                Console.WriteLine();
            }
        }

        public void MostrarMatriz()
        {
            Console.WriteLine("Matriz de Adyacencia:");
            Console.Write("\t");

            for (int i = 0; i < contadorUsuarios; i++)
                Console.Write(usuarios[i].Nombre[0] + "\t");

            Console.WriteLine();

            for (int i = 0; i < contadorUsuarios; i++)
            {
                Console.Write(usuarios[i].Nombre + "\t");

                for (int j = 0; j < contadorUsuarios; j++)
                    Console.Write(matrizAdyacencia[i, j] + "\t");

                Console.WriteLine();
            }
        }

        public void MostrarRed() 
        {
            Console.WriteLine("\n=== MATRIZ DE ADYACENCIA ===\n");
            Console.Write("      ");
            for (int k = 0; k < contadorUsuarios; k++)
                Console.Write($"{usuarios[k].Nombre.Substring(0, 1)} ");
            Console.WriteLine();

            for (int i = 0; i < contadorUsuarios; i++)
            {
                Console.Write($"{usuarios[i].Nombre.Substring(0, 1)}:   ");
                for (int j = 0; j < contadorUsuarios; j++)
                    Console.Write($"{matrizAdyacencia[i, j]} ");
                Console.WriteLine();
            }
        }

        public void MostrarListaAdyacencia()
        {
            Console.WriteLine("\n=== LISTA DE ADYACENCIA ===\n");
            for (int i = 0; i < contadorUsuarios; i++)
            {
                Console.Write($"{usuarios[i].Nombre} -> ");
                bool tieneAmigos = false;
                for (int j = 0; j < contadorUsuarios; j++)
                {
                    if (matrizAdyacencia[i, j] >= 1)
                    {
                        Console.Write($"{usuarios[j].Nombre}, ");
                        tieneAmigos = true;
                    }
                }
                if (!tieneAmigos) Console.Write("Sin amigos registrados");
                Console.WriteLine();
            }
        }

        // --- RECORRIDOS (BFS / DFS) ---

        public void RecorridoAnchura(string nombreInicio)
        {
            int inicio = ObtenerIndice(nombreInicio);
            if (inicio == -1) return;

            bool[] visitado = new bool[contadorUsuarios];
            Cola colaBFS = new Cola();

            visitado[inicio] = true;
            colaBFS.push(inicio);

            Console.Write("Anchura : ");

            while (true)
            {
                var nodo = colaBFS.pop();
                if (nodo == null) break;

                int v = nodo.valor;
                Console.Write(usuarios[v].Nombre + " ");

                NodoAdyacencia? p = listaAdyacencia.Primero(v);
                while (p != null)
                {
                    int u = p.dato;
                    if (!visitado[u])
                    {
                        visitado[u] = true;
                        colaBFS.push(u);
                    }
                    p = listaAdyacencia.Siguiente(p);
                }
            }
            Console.WriteLine();
        }

        public void RecorridoProfundidad(string nombreInicio)
        {
            int inicio = ObtenerIndice(nombreInicio);
            if (inicio == -1) return;

            bool[] visitado = new bool[contadorUsuarios];
            Pila pilaDFS = new Pila();

            visitado[inicio] = true;
            pilaDFS.Push(inicio);

            Console.Write("Profundidad : ");

            while (!pilaDFS.Vacia())
            {
                var nodo = pilaDFS.Pop();
                if (nodo == null) break;

                int v = nodo.valor;
                Console.Write(usuarios[v].Nombre + " ");

                NodoAdyacencia? p = listaAdyacencia.Primero(v); 
                while (p != null)
                {
                    int u = p.dato;
                    if (!visitado[u])
                    {
                        visitado[u] = true;
                        pilaDFS.Push(u);
                    }
                    p = listaAdyacencia.Siguiente(p);
                }
            }
            Console.WriteLine();
        }

        // --- ALGORITMOS AVANZADOS (Integrados) ---

        public void Floyd()
        {
            int n = contadorUsuarios;
            int[,] dist = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                        dist[i, j] = 0;
                    else if (matrizAdyacencia[i, j] == 0)
                        dist[i, j] = 9999;
                    else
                        dist[i, j] = matrizAdyacencia[i, j];
                }
            }

            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (dist[i, j] > dist[i, k] + dist[k, j])
                        {
                            dist[i, j] = dist[i, k] + dist[k, j];
                        }
                    }
                }
            }

            Console.WriteLine("\n=== MATRIZ DE CAMINOS MÍNIMOS (FLOYD) ===");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(dist[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public void Prim()
        {
            int n = contadorUsuarios;
            int[] visitado = new int[n];
            int costoTotal = 0;

            visitado[0] = 1;

            Console.WriteLine("\n=== ÁRBOL DE EXPANSIÓN MÍNIMA (PRIM) ===");

            for (int k = 0; k < n - 1; k++)
            {
                int min = 9999;
                int x = 0, y = 0;

                for (int i = 0; i < n; i++)
                {
                    if (visitado[i] == 1)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (visitado[j] == 0 && matrizAdyacencia[i, j] != 0)
                            {
                                if (matrizAdyacencia[i, j] < min)
                                {
                                    min = matrizAdyacencia[i, j];
                                    x = i;
                                    y = j;
                                }
                            }
                        }
                    }
                }

                Console.WriteLine($"{usuarios[x].Nombre} - {usuarios[y].Nombre} (costo {min})");

                costoTotal += min;
                visitado[y] = 1;
            }

            Console.WriteLine($"Costo total: {costoTotal}");
        }

        public void Kruskal()
        {
            int n = contadorUsuarios;
            int[,] aristas = new int[n * n, 3];
            int contador = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (matrizAdyacencia[i, j] != 0)
                    {
                        aristas[contador, 0] = i;
                        aristas[contador, 1] = j;
                        aristas[contador, 2] = matrizAdyacencia[i, j];
                        contador++;
                    }
                }
            }

            for (int i = 0; i < contador - 1; i++)
            {
                for (int j = 0; j < contador - i - 1; j++)
                {
                    if (aristas[j, 2] > aristas[j + 1, 2])
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            int temp = aristas[j, k];
                            aristas[j, k] = aristas[j + 1, k];
                            aristas[j + 1, k] = temp;
                        }
                    }
                }
            }

            int[] padre = new int[n];
            for (int i = 0; i < n; i++) padre[i] = i;

            Console.WriteLine("\n=== ÁRBOL DE EXPANSIÓN MÍNIMA (KRUSKAL) ===");

            int costoTotal = 0;

            for (int i = 0; i < contador; i++)
            {
                int a = aristas[i, 0];
                int b = aristas[i, 1];
                int c = aristas[i, 2];

                int pa = Encontrar(padre, a);
                int pb = Encontrar(padre, b);

                if (pa != pb)
                {
                    Console.WriteLine($"{usuarios[a].Nombre} - {usuarios[b].Nombre} (costo {c})");
                    costoTotal += c;
                    padre[pa] = pb;
                }
            }

            Console.WriteLine($"Costo total: {costoTotal}");
        }

        private int Encontrar(int[] padre, int i)
        {
            while (padre[i] != i)
                i = padre[i];
            return i;
        }

        public void ComponentesConexas()
        {
            bool[] visitado = new bool[contadorUsuarios];
            int componente = 1;

            Console.WriteLine("\n=== COMPONENTES CONEXAS ===");

            for (int i = 0; i < contadorUsuarios; i++)
            {
                if (!visitado[i])
                {
                    Console.Write("Componente " + componente + ": ");
                    DFS_Marcar(i, visitado);
                    componente++;
                    Console.WriteLine();
                }
            }
        }

        private void DFS_Marcar(int nodo, bool[] visitado)
        {
            visitado[nodo] = true;
            Console.Write(usuarios[nodo].Nombre + " ");

            for (int i = 0; i < contadorUsuarios; i++)
            {
                if (matrizAdyacencia[nodo, i] == 1 && !visitado[i])
                    DFS_Marcar(i, visitado);
            }
        }

        public void ComponentesFuertementeConexas()
        {
            Console.WriteLine("\n=== COMPONENTES FUERTEMENTE CONEXAS ===");

            bool[] visitado = new bool[contadorUsuarios];
            List<int> ordenFinalizacion = new List<int>();

            for (int i = 0; i < contadorUsuarios; i++)
                if (!visitado[i])
                    Kosaraju_DFS1(i, visitado, ordenFinalizacion);

            int[,] transpuesta = Transponer();

            Array.Fill(visitado, false);
            int comp = 1;

            for (int idx = ordenFinalizacion.Count - 1; idx >= 0; idx--)
            {
                int nodo = ordenFinalizacion[idx];
                if (!visitado[nodo])
                {
                    Console.Write("Componente " + comp + ": ");
                    Kosaraju_DFS2(nodo, visitado, transpuesta);
                    comp++;
                    Console.WriteLine();
                }
            }
        }

        private void Kosaraju_DFS1(int nodo, bool[] visitado, List<int> ordenFinalizacion)
        {
            visitado[nodo] = true;
            for (int i = 0; i < contadorUsuarios; i++)
            {
                if (matrizAdyacencia[nodo, i] == 1 && !visitado[i])
                    Kosaraju_DFS1(i, visitado, ordenFinalizacion);
            }

            ordenFinalizacion.Add(nodo);
        }

        private void Kosaraju_DFS2(int nodo, bool[] visitado, int[,] trans)
        {
            visitado[nodo] = true;
            Console.Write(usuarios[nodo].Nombre + " ");
            for (int i = 0; i < contadorUsuarios; i++)
            {
                if (trans[nodo, i] == 1 && !visitado[i])
                    Kosaraju_DFS2(i, visitado, trans);
            }
        }

        private int[,] Transponer()
        {
            int[,] t = new int[contadorUsuarios, contadorUsuarios];
            for (int i = 0; i < contadorUsuarios; i++)
                for (int j = 0; j < contadorUsuarios; j++)
                    t[j, i] = matrizAdyacencia[i, j];
            return t;
        }

        public void MatrizDeCaminos()
        {
            Console.WriteLine("\n=== MATRIZ DE CAMINOS ===");

            int n = contadorUsuarios;
            int[,] caminos = new int[n, n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    caminos[i, j] = matrizAdyacencia[i, j];

            for (int k = 0; k < n; k++)
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        if (caminos[i, j] == 0 && caminos[i, k] == 1 && caminos[k, j] == 1)
                            caminos[i, j] = 1;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write(caminos[i, j] + " ");
                Console.WriteLine();
            }
        }

        public void CierreTransitivo()
        {
            Console.WriteLine("\n=== CIERRE TRANSITIVO ===");

            int n = contadorUsuarios;
            int[,] cierre = new int[n, n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    cierre[i, j] = matrizAdyacencia[i, j];

            for (int k = 0; k < n; k++)
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        if (cierre[i, j] == 0 && cierre[i, k] == 1 && cierre[k, j] == 1)
                            cierre[i, j] = 1;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write(cierre[i, j] + " ");
                Console.WriteLine();
            }
        }

        public void OrdenTopologico()
        {
            Console.WriteLine("\n=== ORDENACIÓN TOPOLÓGICA (DFS) ===");

            bool[] visitado = new bool[contadorUsuarios];
            List<int> orden = new List<int>();

            for (int i = 0; i < contadorUsuarios; i++)
                if (!visitado[i])
                    OrdenTopologico_DFS(i, visitado, orden);

            for (int idx = orden.Count - 1; idx >= 0; idx--)
                Console.Write(usuarios[orden[idx]].Nombre + " ");
            Console.WriteLine();
        }

        private void OrdenTopologico_DFS(int nodo, bool[] visitado, List<int> orden)
        {
            visitado[nodo] = true;

            for (int i = 0; i < contadorUsuarios; i++)
            {
                if (matrizAdyacencia[nodo, i] == 1 && !visitado[i])
                    OrdenTopologico_DFS(i, visitado, orden);
            }

            orden.Add(nodo);
        }
    }
}