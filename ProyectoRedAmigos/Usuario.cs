using System;

namespace ProyectoRedAmigos
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public int Id { get; set; } // Identificador numérico opcional

        public Usuario(string nombre)
        {
            this.Nombre = nombre;
            this.Id = 0;
        }

        public Usuario(string nombre, int id)
        {
            this.Nombre = nombre;
            this.Id = id;
        }

        public bool Igual(Usuario u)
        {
            return this.Nombre == u.Nombre;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}