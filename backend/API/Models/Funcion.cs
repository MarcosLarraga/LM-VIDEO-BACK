public class Funcion
{
    public int Id { get; set; }
    public int PeliculaId { get; set; }
    public int SalaId { get; set; }
    public DateOnly Fecha { get; set; } // Fecha separada
    public TimeOnly Hora { get; set; } // Hora separada

    public Funcion(int id, int peliculaId, int salaId, DateOnly fecha, TimeOnly hora)
    {
        Id = id;
        PeliculaId = peliculaId;
        SalaId = salaId;
        Fecha = fecha;
        Hora = hora;
    }
}
