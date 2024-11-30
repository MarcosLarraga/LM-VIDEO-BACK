public class Asiento
{
    public int Id { get; set; } // Identificador único del asiento
    public int Numero { get; set; } // Número del asiento
    public bool Disponible { get; set; } // Estado de disponibilidad
    public int FuncionId { get; set; } // Relación con la función

    // Constructor
    public Asiento(int id, int numero, bool disponible, int funcionId)
    {
        Id = id;
        Numero = numero;
        Disponible = disponible;
        FuncionId = funcionId;
    }
}
