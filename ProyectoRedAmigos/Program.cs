using System;
// IMPORTANTE: Esta línea permite ver las clases
using ProyectoRedAmigos;
class Program
{
            static void MostrarGrafoDirigido()
        {
            GrafoAmigos red = new GrafoAmigos(10);

            Usuario juan = new Usuario("Juan");
            Usuario maria = new Usuario("María");
            Usuario pedro = new Usuario("Pedro");
            Usuario ana = new Usuario("Ana");

            red.AgregarUsuario(juan);
            red.AgregarUsuario(maria);
            red.AgregarUsuario(pedro);
            red.AgregarUsuario(ana);

            red.AgregarDirigida(juan, maria);
            red.AgregarDirigida(maria, juan);

            red.AgregarDirigida(maria, ana);
            red.AgregarDirigida(ana, maria);

            red.AgregarDirigida(pedro, juan);

            red.MostrarListaAdyacencia();
        }

        static void MostrarGrafoNoDirigido()
        {
            GrafoAmigos red = new GrafoAmigos(10);

            Usuario juan = new Usuario("Juan");
            Usuario maria = new Usuario("María");
            Usuario pedro = new Usuario("Pedro");
            Usuario ana = new Usuario("Ana");

            red.AgregarUsuario(juan);
            red.AgregarUsuario(maria);
            red.AgregarUsuario(pedro);
            red.AgregarUsuario(ana);

            red.AgregarNoDirigida(juan, maria);
            red.AgregarNoDirigida(maria, ana);
            red.AgregarNoDirigida(pedro, juan);

            red.MostrarListaAdyacencia();
        }

        static void MostrarGrafoNoDirigidoConPeso()
        {
            Console.WriteLine("--- Ejecutando Grafo No Dirigido con Pesos ---");
            GrafoAmigos red = new GrafoAmigos(10);

            Usuario juan = new Usuario("Juan");
            Usuario maria = new Usuario("María");
            Usuario pedro = new Usuario("Pedro");
            Usuario ana = new Usuario("Ana");

            red.AgregarUsuario(juan);
            red.AgregarUsuario(maria);
            red.AgregarUsuario(pedro);
            red.AgregarUsuario(ana);

            red.AgregarAmistadValorada(juan, maria, 10);

            red.AgregarAmistadValorada(maria, ana, 5);

            red.AgregarAmistadValorada(pedro, juan, 1);

            red.MostrarListaConPesos();
        }


        static void DemostrarCaminoYLongitud()
        {
            Console.WriteLine("\n--- DEMOSTRACIÓN DE CAMINO NO VALORADO ---");
            // Usamos la configuración del Grafo Dirigido
            GrafoAmigos red = new GrafoAmigos(10);
            Usuario juan = new Usuario("Juan");
            Usuario maria = new Usuario("María");
            Usuario pedro = new Usuario("Pedro");
            Usuario ana = new Usuario("Ana");
            red.AgregarUsuario(juan);
            red.AgregarUsuario(maria);
            red.AgregarUsuario(pedro);
            red.AgregarUsuario(ana);

            red.AgregarDirigida(juan, maria);
            red.AgregarDirigida(maria, ana); // Única conexión para el camino de ejemplo
            red.AgregarDirigida(pedro, juan);

            // Camino de ejemplo: Pedro -> Juan -> María -> Ana
            string[] camino = { "Pedro", "Juan", "María", "Ana" };
            int longitud = camino.Length - 1; // Número de pasos

            Console.WriteLine($"Camino encontrado: ({string.Join(" -> ", camino)})");
            Console.WriteLine($"Longitud del Camino: {longitud} pasos.");

            // El camino (Pedro -> Juan -> María -> Ana) es un Camino Simple.
        }

        static void DemostrarLongitudConPeso()
        {
            Console.WriteLine("\n--- DEMOSTRACIÓN DE LONGITUD CON PESO ---");
            GrafoAmigos red = new GrafoAmigos(10);
            Usuario juan = new Usuario("Juan");
            Usuario maria = new Usuario("María");
            Usuario pedro = new Usuario("Pedro");
            Usuario ana = new Usuario("Ana");
            red.AgregarUsuario(juan);
            red.AgregarUsuario(maria);
            red.AgregarUsuario(pedro);
            red.AgregarUsuario(ana);

            // Pesos reales de tu ejemplo
            int pesoPJ = 1;  // Pedro -> Juan
            int pesoJM = 10; // Juan -> María

            red.AgregarAmistadValorada(pedro, juan, pesoPJ);
            red.AgregarAmistadValorada(juan, maria, pesoJM);

            // Simulación del camino: Pedro -> Juan -> María
            int longitudTotal = pesoPJ + pesoJM;

            Console.WriteLine($"Camino: Pedro -> Juan (Peso {pesoPJ}) -> María (Peso {pesoJM})");
            Console.WriteLine($"Longitud del Camino (Costo): {longitudTotal} (1 + 10).");
        }


        static void DemostrarCiclo()
        {
            Console.WriteLine("\n--- 6. DEMOSTRACIÓN DE CICLO ---");
            GrafoAmigos red = new GrafoAmigos(10);
            Usuario juan = new Usuario("Juan");
            Usuario maria = new Usuario("María");
            Usuario pedro = new Usuario("Pedro");
            Usuario ana = new Usuario("Ana"); // Incluimos a Ana, pero la dejamos fuera del ciclo.

            red.AgregarUsuario(juan); red.AgregarUsuario(maria);
            red.AgregarUsuario(pedro); red.AgregarUsuario(ana);

            // Creamos un ciclo simple de 3 nodos usando conexiones dirigidas:
            red.AgregarDirigida(juan, maria);  // 1. Juan le envía a María
            red.AgregarDirigida(maria, pedro); // 2. María le envía a Pedro
            red.AgregarDirigida(pedro, juan);  // 3. Pedro le envía a Juan (Cierra el ciclo)

            red.MostrarListaAdyacencia();

            Console.WriteLine("Ciclo identificado: Juan -> María -> Pedro -> Juan.");
            Console.WriteLine("PROPIEDADES:");
            Console.WriteLine("  - Comienzo y final en el mismo nodo: Sí (Juan).");
            Console.WriteLine("  - Longitud: 3 pasos.");
            Console.WriteLine("  - Nodos internos repetidos: No. Por lo tanto, es un ciclo simple.");
            Console.WriteLine("  - El nodo 'Ana' no está en el ciclo, pero forma parte del grafo.");
        }

        static void DemostrarGrafoConexo()
        {
            Console.WriteLine("\n--- 7. A) GRAFO CONEXO (No Dirigido) ---");
            GrafoAmigos red = new GrafoAmigos(10);
            Usuario juan = new Usuario("Juan");
            Usuario maria = new Usuario("María");
            Usuario pedro = new Usuario("Pedro");
            Usuario ana = new Usuario("Ana");
            red.AgregarUsuario(juan); red.AgregarUsuario(maria);
            red.AgregarUsuario(pedro); red.AgregarUsuario(ana);

            // Conexiones mínimas (3 aristas) para unir los 4 nodos en una cadena.
            red.AgregarNoDirigida(juan, maria); // Juan <-> María
            red.AgregarNoDirigida(maria, ana);  // María <-> Ana
            red.AgregarNoDirigida(ana, pedro);  // Ana <-> Pedro

            red.MostrarListaAdyacencia();
            Console.WriteLine("RESULTADO: Es conexo. Puedes ir de Pedro a Juan por el camino: Pedro <-> Ana <-> María <-> Juan.");
        }

        static void DemostrarGrafoFuertementeConexo()
        {
            Console.WriteLine("\n--- 7. B) GRAFO FUERTEMENTE CONEXO (Dirigido) ---");
            GrafoAmigos red = new GrafoAmigos(10);
            Usuario juan = new Usuario("Juan");
            Usuario maria = new Usuario("María");
            Usuario pedro = new Usuario("Pedro");
            Usuario ana = new Usuario("Ana");
            red.AgregarUsuario(juan); red.AgregarUsuario(maria);
            red.AgregarUsuario(pedro); red.AgregarUsuario(ana);

            // Creamos un ciclo: Juan → María → Ana → Pedro → Juan
            red.AgregarDirigida(juan, maria); // J -> M
            red.AgregarDirigida(maria, ana);  // M -> A
            red.AgregarDirigida(ana, pedro);  // A -> P
            red.AgregarDirigida(pedro, juan); // P -> J (Cierra el ciclo, asegurando la vuelta a J)

            red.MostrarListaAdyacencia();
            Console.WriteLine("RESULTADO: Es fuertemente conexo. Si empiezas en cualquier usuario, puedes llegar a todos los demás y volver a tu inicio siguiendo la dirección de las flechas.");
        }

        static void DemostrarGrafoCompleto()
        {
            Console.WriteLine("\n--- 8. GRAFO COMPLETO (No Dirigido) ---");
            GrafoAmigos red = new GrafoAmigos(10);
            Usuario juan = new Usuario("Juan");
            Usuario maria = new Usuario("María");
            Usuario pedro = new Usuario("Pedro");
            Usuario ana = new Usuario("Ana");
            red.AgregarUsuario(juan); red.AgregarUsuario(maria);
            red.AgregarUsuario(pedro); red.AgregarUsuario(ana);

            // 6 conexiones bidireccionales necesarias
            // Conexiones de Juan:
            red.AgregarNoDirigida(juan, maria);
            red.AgregarNoDirigida(juan, pedro);
            red.AgregarNoDirigida(juan, ana);
            // Conexiones restantes de María:
            red.AgregarNoDirigida(maria, pedro);
            red.AgregarNoDirigida(maria, ana);
            // Conexiones restantes de Pedro:
            red.AgregarNoDirigida(pedro, ana);

            red.MostrarListaAdyacencia();
            Console.WriteLine("RESULTADO: Es completo. Cada usuario tiene Grado 3 (está conectado con sus 3 compañeros).");
        }


        static void MostrarListaAdyacencia()
        {

            GrafoAmigos red = new GrafoAmigos(10);
            Usuario juan = new Usuario("Juan");
            Usuario maria = new Usuario("María");
            Usuario pedro = new Usuario("Pedro");
            Usuario ana = new Usuario("Ana");

            red.AgregarUsuario(juan);
            red.AgregarUsuario(maria);
            red.AgregarUsuario(pedro);
            red.AgregarUsuario(ana);

            red.AgregarDirigida(juan, maria);
            red.AgregarDirigida(maria, juan);

            red.AgregarDirigida(maria, ana);
            red.AgregarDirigida(ana, maria);

            red.AgregarDirigida(pedro, juan);

            red.MostrarRed();

            Console.WriteLine();
            red.RecorridoAnchura("Juan");
            red.RecorridoProfundidad("Juan");
        }
    static void Main()
    {
        GrafoAmigos red = new GrafoAmigos(10);

        Usuario juan = new Usuario("Juan");
        Usuario maria = new Usuario("María");
        Usuario pedro = new Usuario("Pedro");
        Usuario ana = new Usuario("Ana");

        red.AgregarUsuario(juan);
        red.AgregarUsuario(maria);
        red.AgregarUsuario(pedro);
        red.AgregarUsuario(ana);

        red.AgregarAmistad(juan, maria);
        red.AgregarAmistad(maria, ana);
        red.AgregarAmistad(pedro, juan);

        red.MostrarRed();

        red.Floyd();
        red.Prim();
        red.Kruskal();
    }
}
