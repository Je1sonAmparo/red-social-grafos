    using System;

public class GrafoAmigos
{
    private Usuario[] usuarios;
    private int[,] matrizAdyacencia;
    private int contadorUsuarios;
    private int maxUsuarios;

    public GrafoAmigos(int max)
    {
        maxUsuarios = max;
        usuarios = new Usuario[maxUsuarios];
        matrizAdyacencia = new int[maxUsuarios, maxUsuarios];
        contadorUsuarios = 0;
    }

    public void AgregarUsuario(Usuario u)
    {
        if (contadorUsuarios < maxUsuarios)
        {
            usuarios[contadorUsuarios] = u;
            contadorUsuarios++;
        }
    }

    private int BuscarUsuario(Usuario u)
    {
        for (int i = 0; i < contadorUsuarios; i++)
        {
            if (usuarios[i].Nombre == u.Nombre)
                return i;
        }
        return -1;
    }

    public void AgregarAmistad(Usuario a, Usuario b)
    {
        int i = BuscarUsuario(a);
        int j = BuscarUsuario(b);

        if (i == -1 || j == -1)
        {
            Console.WriteLine("Uno de los usuarios no existe.");
            return;
        }

        matrizAdyacencia[i, j] = 1;
        matrizAdyacencia[j, i] = 1;
    }

    public void MostrarRed()
    {
        Console.WriteLine("=== MATRIZ DE ADYACENCIA ===\n");
        Console.Write("\t");

        for (int k = 0; k < contadorUsuarios; k++)
        {
            Console.Write(usuarios[k].Nombre[0] + "\t");
        }
        Console.WriteLine();

        for (int i = 0; i < contadorUsuarios; i++)
        {
            Console.Write($"{usuarios[i].Nombre + "\t");
            for (int j = 0; j < contadorUsuarios; j++)
            {
                Console.Write(matrizAdyacencia[i, j] + "\t");
            }
        Console.WriteLine();
    }


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
    public int[,] Warshall()
    {
        // Creamos una copia de la matriz para no modificar la original
        int[,] cierre = new int[contadorUsuarios, contadorUsuarios];

        for (int i = 0; i < contadorUsuarios; i++)
            for (int j = 0; j < contadorUsuarios; j++)
                cierre[i, j] = matrizAdyacencia[i, j];

        // Algoritmo de Warshall (cierre transitivo)
        for (int k = 0; k < contadorUsuarios; k++)
        {
            for (int i = 0; i < contadorUsuarios; i++)
            {
                for (int j = 0; j < contadorUsuarios; j++)
                {
                    if (cierre[i, k] == 1 && cierre[k, j] == 1)
                        cierre[i, j] = 1;
                }
            }
        }

        return cierre;
    }

    private int ObtenerIndice(Usuario u)
    {
        for (int i = 0; i < contadorUsuarios; i++)
        {
            if (usuarios[i].Nombre == u.Nombre)
                return i;
        }
        return -1;
    }
    public void MostrarWarshall()
    {
        var cierre = Warshall();

        Console.WriteLine("\n=== CIERRE TRANSITIVO (WARSHALL) ===\n");

        Console.Write("     ");
        for (int k = 0; k < contadorUsuarios; k++)
            Console.Write($"{usuarios[k].Nombre.Substring(0, 1)} ");
        Console.WriteLine();

        for (int i = 0; i < contadorUsuarios; i++)
        {
            Console.Write($"{usuarios[i].Nombre.Substring(0, 1)}:   ");
            for (int j = 0; j < contadorUsuarios; j++)
                Console.Write($"{cierre[i, j]} ");
            Console.WriteLine();
        }
    }

    public int[] Dijkstra(Usuario origen)
    {
        int start = ObtenerIndice(origen);
        if (start == -1)
            return null;

        int n = contadorUsuarios;
        int[] distancia = new int[n];
        bool[] visitado = new bool[n];

        // Inicializar distancias
        const int INF = 999999;
        for (int i = 0; i < n; i++)
            distancia[i] = INF;

        distancia[start] = 0;

        // Algoritmo de Dijkstra
        for (int c = 0; c < n - 1; c++)
        {
            // Seleccionar nodo no visitado con menor distancia
            int u = -1;
            int minDist = INF;

            for (int i = 0; i < n; i++)
            {
                if (!visitado[i] && distancia[i] < minDist)
                {
                    minDist = distancia[i];
                    u = i;
                }
            }

            if (u == -1) break;

            visitado[u] = true;

            // Relajación de vecinos
            for (int v = 0; v < n; v++)
            {
                if (matrizAdyacencia[u, v] > 0 && !visitado[v])
                {
                    int nuevoCosto = distancia[u] + matrizAdyacencia[u, v];
                    if (nuevoCosto < distancia[v])
                    {
                        distancia[v] = nuevoCosto;
                    }
                }
            }
        }

        return distancia;
    }

    public void MostrarDijkstra(Usuario origen)
    {
        int[] dist = Dijkstra(origen);
        if (dist == null)
        {
            Console.WriteLine("Usuario de origen no existe.");
            return;
        }

        Console.WriteLine($"\n=== DIJKSTRA DESDE {origen.Nombre} ===\n");

        for (int i = 0; i < contadorUsuarios; i++)
        {
            Console.WriteLine($"{origen.Nombre} → {usuarios[i].Nombre} = {dist[i]}");
        }
    }
}
