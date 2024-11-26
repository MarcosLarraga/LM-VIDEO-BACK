
public class Pago
{
    public int Id { get; set; } // ID único del pago

    // Relación con la función, película y asientos seleccionados
    public int FuncionId { get; set; } // ID de la función seleccionada
    public int PeliculaId { get; set; } // ID de la película asociada
    public List<int> AsientosSeleccionados { get; set; } // Lista de IDs o números de asientos seleccionados

    // Información del formulario del cliente
    public string Nombre { get; set; } // Nombre del cliente
    public string Apellido { get; set; } // Apellido del cliente
    public string Direccion { get; set; } // Dirección del cliente
    public string CodigoPostal { get; set; } // Código postal
    public string Ciudad { get; set; } // Ciudad del cliente
    public string CorreoElectronico { get; set; } // Email del cliente
    public string Telefono { get; set; } // Teléfono del cliente

    // Constructor para inicializar los campos
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

