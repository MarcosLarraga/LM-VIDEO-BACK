public class Sala
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public List<Funcion> Funciones { get; set; } = new List<Funcion>(); // Lista de funciones en la sala

    public Sala(int id, string nombre)
    {
        Id = id;
        Nombre = nombre;
    }
}
