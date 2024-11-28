using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsientoController : ControllerBase
    {
        // Lista de asientos accesible estáticamente
        public static List<Asiento> Asientos = new List<Asiento>();

        // Constructor estático para inicializar asientos
        static AsientoController()
        {
            InicializarAsientos();
        }

        // Método para inicializar los asientos
        private static void InicializarAsientos()
        {
            int numeroDeFunciones = 36; // Total de funciones
            int numeroAsientosPorFuncion = 64; // Total de asientos por función
            decimal precioBase = 6.00m; // Precio base de los asientos

            for (int funcionId = 1; funcionId <= numeroDeFunciones; funcionId++)
            {
                for (int i = 1; i <= numeroAsientosPorFuncion; i++)
                {
                    // Crear y añadir un asiento único para cada función
                    Asientos.Add(new Asiento(Asientos.Count + 1, i, true, precioBase, funcionId));
                }
            }
        }

        // Obtener todos los asientos de una función específica
        [HttpGet("Todos/{funcionId}")]
        public ActionResult<IEnumerable<object>> GetTodosAsientos(int funcionId)
        {
            var asientosFuncion = Asientos
                .Where(a => a.FuncionId == funcionId)
                .Select(a => new
                {
                    a.Numero,
                    a.Disponible,
                    Precio = $"{a.Precio} €" // Formatear precio con símbolo de euros
                })
                .ToList();

            if (!asientosFuncion.Any())
            {
                return NotFound($"No hay asientos registrados para la función con ID {funcionId}.");
            }

            return Ok(asientosFuncion);
        }

        // Método para elegir asientos
        [HttpPut("Elegir/{funcionId}")]
        public IActionResult ElegirAsientos(int funcionId, [FromBody] List<int> numerosAsientos)
        {
            var asientosNoDisponibles = new List<int>();

            foreach (var numero in numerosAsientos)
            {
                var asiento = Asientos.FirstOrDefault(a => a.FuncionId == funcionId && a.Numero == numero);

                if (asiento == null)
                {
                    return BadRequest($"El asiento {numero} no existe para la función con ID {funcionId}.");
                }

                if (!asiento.Disponible)
                {
                    asientosNoDisponibles.Add(numero);
                }
                else
                {
                    asiento.Disponible = false; // Reservar asiento
                }
            }

            if (asientosNoDisponibles.Any())
            {
                return BadRequest($"Los siguientes asientos no están disponibles: {string.Join(", ", asientosNoDisponibles)}");
            }

            return NoContent();
        }

        // Método para obtener un asiento específico
        [HttpGet("{funcionId}/{numeroAsiento}")]
        public ActionResult<object> GetAsiento(int funcionId, int numeroAsiento)
        {
            var asiento = Asientos.FirstOrDefault(a => a.FuncionId == funcionId && a.Numero == numeroAsiento);
            if (asiento == null)
            {
                return NotFound($"No se encontró el asiento {numeroAsiento} para la función con ID {funcionId}.");
            }

            return Ok(new
            {
                asiento.Numero,
                asiento.Disponible,
                asiento.Precio,
                asiento.FuncionId
            });
        }
    }
}
