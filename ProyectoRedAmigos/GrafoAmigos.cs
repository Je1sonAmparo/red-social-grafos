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

    private int ObtenerIndice(Usuario u)
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
        int i = ObtenerIndice(a);
        int j = ObtenerIndice(b);

        if (i != -1 && j != -1)
        {
            matrizAdyacencia[i, j] = 1;
            matrizAdyacencia[j, i] = 1; // Amistad bidireccional
        }
    }

    public void MostrarRed()
    {
        Console.WriteLine("=== MATRIZ DE ADYACENCIA ===\n");

        Console.Write("     ");
        for (int k = 0; k < contadorUsuarios; k++)
        {
            Console.Write($"{usuarios[k].Nombre.Substring(0, 1)} ");
        }
        Console.WriteLine();

        for (int i = 0; i < contadorUsuarios; i++)
        {
            Console.Write($"{usuarios[i].Nombre.Substring(0, 1)}:   ");
            for (int j = 0; j < contadorUsuarios; j++)
            {
                Console.Write($"{matrizAdyacencia[i, j]} ");
            }
            Console.WriteLine();
        }
    }
}