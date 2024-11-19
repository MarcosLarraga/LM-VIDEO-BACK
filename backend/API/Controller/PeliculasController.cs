using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculaController : ControllerBase
    {
        private static List<Pelicula> peliculas = new List<Pelicula>();

        // GET: api/pelicula
        [HttpGet]
        public ActionResult<IEnumerable<Pelicula>> GetPeliculas()
        {
            return Ok(peliculas);
        }

        // GET: api/pelicula/{id}
        [HttpGet("{id}")]
        public ActionResult<Pelicula> GetPelicula(int id)
        {
            var pelicula = peliculas.FirstOrDefault(p => p.Id == id);
            return pelicula != null ? Ok(pelicula) : NotFound($"No se encontró la película con ID {id}");
        }

        // POST: api/pelicula
        [HttpPost]
        public ActionResult<Pelicula> CreatePelicula(Pelicula pelicula)
        {
            if (peliculas.Any(p => p.Id == pelicula.Id))
            {
                return Conflict($"Ya existe una película con el ID {pelicula.Id}");
            }
            peliculas.Add(pelicula);
            return CreatedAtAction(nameof(GetPelicula), new { id = pelicula.Id }, pelicula);
        }

        // PUT: api/pelicula/{id}
        [HttpPut("{id}")]
        public IActionResult UpdatePelicula(int id, Pelicula updatedPelicula)
        {
            var pelicula = peliculas.FirstOrDefault(p => p.Id == id);
            if (pelicula == null)
            {
                return NotFound($"No se encontró la película con ID {id}");
            }
            pelicula.Titulo = updatedPelicula.Titulo;
            pelicula.Descripcion = updatedPelicula.Descripcion;
            pelicula.Duracion = updatedPelicula.Duracion;
            pelicula.FotoUrl = updatedPelicula.FotoUrl;
            return NoContent();
        }

        // DELETE: api/pelicula/{id}
        [HttpDelete("{id}")]
        public IActionResult DeletePelicula(int id)
        {
            var pelicula = peliculas.FirstOrDefault(p => p.Id == id);
            if (pelicula == null)
            {
                return NotFound($"No se encontró la película con ID {id}");
            }
            peliculas.Remove(pelicula);
            return NoContent();
        }

        // Método para inicializar datos de ejemplo
        public static void InicializarDatos()
        {
            peliculas.Add(new Pelicula(1, "Venom: El Último Baile", "Eddie y Venom están prófugos...", "1 hora 49 minutos", "https://image.tmdb.org/t/p/original/b0obWWCLRVRqRzlSK1LSGtADkLM.jpg"));
            peliculas.Add(new Pelicula(2, "The Wild Robot", "Un robot futurista llega a una isla desierta...", "1 hora 42 minutos", "https://image.tmdb.org/t/p/original/a0a7RC01aTa7pOnskgJb3mCD2Ba.jpg"));
            peliculas.Add(new Pelicula(3, "Gladiator II", "Años después de presenciar la muerte de Máximo...", "2 horas 28 minutos", "https://image.tmdb.org/t/p/original/vK1sK1B3WglfgVWPn6Xj4nNsw1q.jpg"));
            peliculas.Add(new Pelicula(4, "Terrifier 3", "La sobreviviente Sienna se ve acosada por visiones perturbadoras...", "2 horas 5 minutos", "https://image.tmdb.org/t/p/original/cZZ769OJGABhKkd5xnTRQz2Y2x0.jpg"));
            peliculas.Add(new Pelicula(5, "Red One", "Un agente de ELF debe asociarse con un rastreador para salvar la Navidad...", "2 horas 3 minutos", "https://image.tmdb.org/t/p/original/edgKt5Z7QcH2TZfaT7vXOrs36zt.jpg"));
            peliculas.Add(new Pelicula(6, "Smile 2", "La sensación pop global Skye Riley experimenta eventos aterradores...", "2 horas 7 minutos", "https://image.tmdb.org/t/p/original/yqfeWTE1RCl60E8V3SfFAgr0jj6.jpg"));

        }
    }
}
