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
            for (int i = 1; i <= 60; i++)
            {
                asientos.Add(new Asiento(i, i, true, 50.00m, 1)); // Ejemplo: FuncionId = 1
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
                    Disponible = a.Disponible ? "Disponible" : "Ocupado",
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
