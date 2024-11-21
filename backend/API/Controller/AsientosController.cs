using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsientoController : ControllerBase
    {
        private static List<Asiento> asientos = new List<Asiento>();

        static AsientoController()
        {
            InicializarAsientos();
        }

        // Método para inicializar los 60 asientos
        private static void InicializarAsientos()
        {
            int numeroDeFunciones = 18; // Total de funciones
            int numeroAsientosPorFuncion = 64; // Total de asientos por función
            decimal precioBase = 50.00m; // Precio base de los asientos

            for (int funcionId = 1; funcionId <= numeroDeFunciones; funcionId++)
            {
                for (int i = 1; i <= numeroAsientosPorFuncion; i++)
                {
                    // Creamos un asiento único para cada función
                    asientos.Add(new Asiento(asientos.Count + 1, i, true, precioBase, funcionId));
                }
            }
        }

        [HttpGet("Todos/{funcionId}")]
        public ActionResult<IEnumerable<object>> GetTodosAsientos(int funcionId)
        {
            var asientosFuncion = asientos
                .Where(a => a.FuncionId == funcionId)
                .Select(a => new
                {
                    a.Numero,
                    a.Disponible, // Mantén el booleano original
                    a.Precio
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
                var asiento = asientos.FirstOrDefault(a => a.FuncionId == funcionId && a.Numero == numero);

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
    }
}
