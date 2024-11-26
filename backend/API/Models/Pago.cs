public class Pago
{
    public int Id { get; set; }
    public string NombreCliente { get; set; } // Nombre del cliente
    public string CorreoCliente { get; set; } // Correo del cliente
    public Pelicula Pelicula { get; set; } // Referencia a la película
    public Funcion Funcion { get; set; } // Referencia a la función
    public List<Asiento> Asientos { get; set; } // Lista de asientos seleccionados
    public decimal MontoTotal { get; set; } // Precio total calculado
    public DateTime Fecha { get; set; } // Fecha del pago
    public string Metodo { get; set; } // Método de pago: "Tarjeta de Crédito", etc.
    public bool TransaccionExitosa { get; set; } // Resultado del pago

    // Constructor
    public Pago(int id, string nombreCliente, string correoCliente, Pelicula pelicula, Funcion funcion, 
                List<Asiento> asientos, decimal montoTotal, DateTime fecha, string metodo, bool transaccionExitosa)
    {
        Id = id;
        NombreCliente = nombreCliente;
        CorreoCliente = correoCliente;
        Pelicula = pelicula;
        Funcion = funcion;
        Asientos = asientos;
        MontoTotal = montoTotal;
        Fecha = fecha;
        Metodo = metodo;
        TransaccionExitosa = transaccionExitosa;
    }
}
