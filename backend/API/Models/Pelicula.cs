public class Pelicula
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public string Duracion { get; set; }
    public string FotoUrl { get; set; }

    public Pelicula(int id, string titulo, string descripcion, string duracion, string fotoUrl)
    {
        Id = id;
        Titulo = titulo;
        Descripcion = descripcion;
        Duracion = duracion;
        FotoUrl = fotoUrl;
    }
}
