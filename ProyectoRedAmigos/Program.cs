class Program
{
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
