public class Asiento
{
    public int Id { get; set; } // Identificador único del asiento
    public int Numero { get; set; } // Número del asiento
    public bool Disponible { get; set; } // Estado de disponibilidad
    public decimal Precio { get; set; } // Precio del asiento
    public int FuncionId { get; set; } // Relación con la función

    // Constructor
    public Asiento(int id, int numero, bool disponible, decimal precio, int funcionId)
    {
        Id = id;
        Numero = numero;
        Disponible = disponible;
        Precio = precio;
        FuncionId = funcionId;
    }
}
