using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaController : ControllerBase
    {
        private static List<Sala> salas = new List<Sala>();

        // Constructor estático para inicializar los datos
        static SalaController()
        {
            InicializarDatos();
        }

        [HttpGet("{id}")]
        public ActionResult<Sala> GetSala(int id)
        {
            var sala = salas.FirstOrDefault(s => s.Id == id);
            if (sala == null)
            {
                return NotFound();
            }
            return Ok(sala);
        }

        private static void InicializarDatos()
        {
            salas.Add(new Sala(1, "Sala A"));
            salas.Add(new Sala(2, "Sala B"));
            salas.Add(new Sala(3, "Sala C"));
        }
    }
}
