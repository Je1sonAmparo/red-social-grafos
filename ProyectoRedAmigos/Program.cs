class Program
{
    static void Main()
    {
        GrafoAmigos red = new GrafoAmigos(10);

        var juan = new Usuario("Juan");
        var maria = new Usuario("María");
        var pedro = new Usuario("Pedro");
        var ana = new Usuario("Ana");

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
