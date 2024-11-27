using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionController : ControllerBase
    {
        private static List<Funcion> funciones = new List<Funcion>();

        static FuncionController()
        {
            if (funciones.Count == 0)
            {
                InicializarDatos();
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Funcion>> GetFunciones()
        {
            return Ok(funciones);
        }

        [HttpGet("{id}")]
        public ActionResult<Funcion> GetFuncion(int id)
        {
            var funcion = funciones.FirstOrDefault(f => f.Id == id);
            if (funcion == null)
            {
                return NotFound($"La función con ID {id} no fue encontrada.");
            }
            return Ok(funcion);
        }

        [HttpGet("pelicula/{peliculaId}")]
        public ActionResult<IEnumerable<Funcion>> GetFuncionesPorPelicula(int peliculaId)
        {
            var funcionesPorPelicula = funciones.Where(f => f.PeliculaId == peliculaId).ToList();

            if (!funcionesPorPelicula.Any())
            {
                return NotFound($"No se encontraron funciones para la película con ID {peliculaId}.");
            }

            return Ok(funcionesPorPelicula);
        }

        [HttpGet("pelicula/{peliculaId}/fecha/{fecha}")]
        public ActionResult<IEnumerable<Funcion>> GetFuncionesPorPeliculaYFecha(int peliculaId, string fecha)
        {
            if (!DateOnly.TryParse(fecha, out DateOnly fechaParsed))
            {
                return BadRequest("El formato de la fecha es incorrecto.");
            }

            var funcionesPorFecha = funciones
                .Where(f => f.PeliculaId == peliculaId && f.Fecha == fechaParsed)
                .ToList();

            if (!funcionesPorFecha.Any())
            {
                return NotFound($"No se encontraron funciones para la película con ID {peliculaId} en la fecha {fechaParsed.ToShortDateString()}.");
            }

            return Ok(funcionesPorFecha);
        }

        [HttpPost]
        public ActionResult<Funcion> CreateFuncion(Funcion funcion)
        {
            funcion.Id = funciones.Any() ? funciones.Max(f => f.Id) + 1 : 1; // Asignar ID único
            funciones.Add(funcion);
            return CreatedAtAction(nameof(GetFuncion), new { id = funcion.Id }, funcion);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateFuncion(int id, Funcion updatedFuncion)
        {
            var funcion = funciones.FirstOrDefault(f => f.Id == id);
            if (funcion == null)
            {
                return NotFound($"La función con ID {id} no fue encontrada.");
            }

            funcion.PeliculaId = updatedFuncion.PeliculaId;
            funcion.SalaId = updatedFuncion.SalaId;
            funcion.Fecha = updatedFuncion.Fecha;
            funcion.Hora = updatedFuncion.Hora;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFuncion(int id)
        {
            var funcion = funciones.FirstOrDefault(f => f.Id == id);
            if (funcion == null)
            {
                return NotFound($"La función con ID {id} no fue encontrada.");
            }
            funciones.Remove(funcion);
            return NoContent();
        }

        private static void InicializarDatos()
        {
            // Fecha base para las funciones
            DateOnly fechaBase = DateOnly.FromDateTime(DateTime.Today);

            // Funciones para la película 1
            funciones.Add(new Funcion(1, 1, 1, fechaBase, new TimeOnly(10, 0))); // Hoy
            funciones.Add(new Funcion(2, 1, 1, fechaBase, new TimeOnly(13, 0))); // Hoy
            funciones.Add(new Funcion(3, 1, 2, fechaBase.AddDays(1), new TimeOnly(16, 0))); // Mañana
            funciones.Add(new Funcion(4, 1, 2, fechaBase.AddDays(1), new TimeOnly(18, 0))); // Mañana
            funciones.Add(new Funcion(5, 1, 3, fechaBase.AddDays(2), new TimeOnly(20, 0))); // Pasado mañana
            funciones.Add(new Funcion(6, 1, 3, fechaBase.AddDays(2), new TimeOnly(22, 0))); // Pasado mañana

            // Funciones para la película 2
            funciones.Add(new Funcion(7, 2, 1, fechaBase, new TimeOnly(11, 0))); // Hoy
            funciones.Add(new Funcion(8, 2, 1, fechaBase, new TimeOnly(14, 0))); // Hoy
            funciones.Add(new Funcion(9, 2, 2, fechaBase.AddDays(1), new TimeOnly(17, 0))); // Mañana
            funciones.Add(new Funcion(10, 2, 2, fechaBase.AddDays(1), new TimeOnly(19, 0))); // Mañana
            funciones.Add(new Funcion(11, 2, 3, fechaBase.AddDays(2), new TimeOnly(21, 0))); // Pasado mañana
            funciones.Add(new Funcion(12, 2, 3, fechaBase.AddDays(2), new TimeOnly(23, 0))); // Pasado mañana

            // Funciones para la película 3
            funciones.Add(new Funcion(13, 3, 1, fechaBase, new TimeOnly(12, 0))); // Hoy
            funciones.Add(new Funcion(14, 3, 2, fechaBase, new TimeOnly(15, 0))); // Hoy
            funciones.Add(new Funcion(15, 3, 2, fechaBase.AddDays(1), new TimeOnly(18, 0))); // Mañana
            funciones.Add(new Funcion(16, 3, 3, fechaBase.AddDays(1), new TimeOnly(20, 0))); // Mañana
            funciones.Add(new Funcion(17, 3, 3, fechaBase.AddDays(2), new TimeOnly(22, 0))); // Pasado mañana
            funciones.Add(new Funcion(18, 3, 1, fechaBase.AddDays(2), new TimeOnly(23, 0))); // Pasado mañana

            // Funciones para la película 4
            funciones.Add(new Funcion(19, 4, 2, fechaBase, new TimeOnly(13, 0))); // Hoy
            funciones.Add(new Funcion(20, 4, 2, fechaBase, new TimeOnly(16, 0))); // Hoy
            funciones.Add(new Funcion(21, 4, 3, fechaBase.AddDays(1), new TimeOnly(19, 0))); // Mañana
            funciones.Add(new Funcion(22, 4, 3, fechaBase.AddDays(1), new TimeOnly(21, 0))); // Mañana
            funciones.Add(new Funcion(23, 4, 1, fechaBase.AddDays(2), new TimeOnly(22, 0))); // Pasado mañana
            funciones.Add(new Funcion(24, 4, 1, fechaBase.AddDays(2), new TimeOnly(23, 30))); // Pasado mañana

            // Funciones para la película 5
            funciones.Add(new Funcion(25, 5, 1, fechaBase, new TimeOnly(14, 0))); // Hoy
            funciones.Add(new Funcion(26, 5, 1, fechaBase, new TimeOnly(17, 0))); // Hoy
            funciones.Add(new Funcion(27, 5, 2, fechaBase.AddDays(1), new TimeOnly(19, 0))); // Mañana
            funciones.Add(new Funcion(28, 5, 2, fechaBase.AddDays(1), new TimeOnly(21, 0))); // Mañana
            funciones.Add(new Funcion(29, 5, 3, fechaBase.AddDays(2), new TimeOnly(22, 0))); // Pasado mañana
            funciones.Add(new Funcion(30, 5, 3, fechaBase.AddDays(2), new TimeOnly(23, 59))); // Pasado mañana

            // Funciones para la película 6
            funciones.Add(new Funcion(31, 6, 2, fechaBase, new TimeOnly(15, 0))); // Hoy
            funciones.Add(new Funcion(32, 6, 2, fechaBase, new TimeOnly(18, 0))); // Hoy
            funciones.Add(new Funcion(33, 6, 3, fechaBase.AddDays(1), new TimeOnly(20, 0))); // Mañana
            funciones.Add(new Funcion(34, 6, 3, fechaBase.AddDays(1), new TimeOnly(22, 0))); // Mañana
            funciones.Add(new Funcion(35, 6, 1, fechaBase.AddDays(2), new TimeOnly(23, 0))); // Pasado mañana
            funciones.Add(new Funcion(36, 6, 1, fechaBase.AddDays(2), new TimeOnly(23, 59))); // Pasado mañana
        }

    }
}
