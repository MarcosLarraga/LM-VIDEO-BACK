
public class Pago
{
    public int Id { get; set; }
    public int FuncionId { get; set; } 
    public int PeliculaId { get; set; } 
    public List<int> AsientosSeleccionados { get; set; } 
    public string Nombre { get; set; } 
    public string Apellido { get; set; } 
    public string Direccion { get; set; }
    public string CodigoPostal { get; set; }
    public string Ciudad { get; set; } 
    public string CorreoElectronico { get; set; } 
    public string Telefono { get; set; } 

    public Pago(int funcionId, int peliculaId, List<int> asientosSeleccionados, string nombre, string apellido, string direccion, string codigoPostal, string ciudad, string correoElectronico, string telefono)
    {
        FuncionId = funcionId;
        PeliculaId = peliculaId;
        AsientosSeleccionados = asientosSeleccionados;
        Nombre = nombre;
        Apellido = apellido;
        Direccion = direccion;
        CodigoPostal = codigoPostal;
        Ciudad = ciudad;
        CorreoElectronico = correoElectronico;
        Telefono = telefono;
    }
}

