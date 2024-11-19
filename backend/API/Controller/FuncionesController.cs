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
            funcion.Horario = updatedFuncion.Horario;

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
            // Inicializar datos de funciones
            funciones.Add(new Funcion(1, 1, 1, DateTime.Today.AddHours(10)));
            funciones.Add(new Funcion(2, 2, 1, DateTime.Today.AddHours(13)));
            funciones.Add(new Funcion(3, 3, 1, DateTime.Today.AddHours(16)));
            funciones.Add(new Funcion(4, 4, 1, DateTime.Today.AddHours(19)));
            funciones.Add(new Funcion(5, 5, 1, DateTime.Today.AddHours(21)));
            funciones.Add(new Funcion(6, 6, 1, DateTime.Today.AddHours(23)));

            funciones.Add(new Funcion(7, 1, 2, DateTime.Today.AddHours(11)));
            funciones.Add(new Funcion(8, 2, 2, DateTime.Today.AddHours(14)));
            funciones.Add(new Funcion(9, 3, 2, DateTime.Today.AddHours(17)));
            funciones.Add(new Funcion(10, 4, 2, DateTime.Today.AddHours(20)));
            funciones.Add(new Funcion(11, 5, 2, DateTime.Today.AddHours(22)));
            funciones.Add(new Funcion(12, 6, 2, DateTime.Today.AddHours(24)));

            funciones.Add(new Funcion(13, 1, 3, DateTime.Today.AddHours(10)));
            funciones.Add(new Funcion(14, 2, 3, DateTime.Today.AddHours(13)));
            funciones.Add(new Funcion(15, 3, 3, DateTime.Today.AddHours(16)));
            funciones.Add(new Funcion(16, 4, 3, DateTime.Today.AddHours(19)));
            funciones.Add(new Funcion(17, 5, 3, DateTime.Today.AddHours(21)));
            funciones.Add(new Funcion(18, 6, 3, DateTime.Today.AddHours(23)));

        }
    }
}
