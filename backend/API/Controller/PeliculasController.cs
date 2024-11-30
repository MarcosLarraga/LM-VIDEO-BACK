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


        [HttpGet]
        public ActionResult<IEnumerable<Pelicula>> GetPeliculas()
        {
            return Ok(peliculas);
        }


        [HttpGet("{id}")]
        public ActionResult<Pelicula> GetPelicula(int id)
        {
            var pelicula = peliculas.FirstOrDefault(p => p.Id == id);
            return pelicula != null ? Ok(pelicula) : NotFound($"No se encontró la película con ID {id}");
        }

        public static void InicializarDatos()
        {
            peliculas.Add(new Pelicula(1, "Venom: El Último Baile", "Eddie y Venom están prófugos. Perseguidos por sus dos mundos y con la red cerrándose sobre ellos, el dúo se ve obligado a tomar una decisión devastadora.", "1 hora 49 minutos", "https://image.tmdb.org/t/p/original/b0obWWCLRVRqRzlSK1LSGtADkLM.jpg"));
            peliculas.Add(new Pelicula(2, "The Wild Robot", "“The Wild Robot” sigue a Rozzum 7134 (“Roz” para abreviar), un robot futurista que llega a una isla desierta. Comienza una historia de supervivencia y descubrimiento.", "1 hora 42 minutos", "https://image.tmdb.org/t/p/original/a0a7RC01aTa7pOnskgJb3mCD2Ba.jpg"));
            peliculas.Add(new Pelicula(3, "Gladiator II", "Años después de presenciar la muerte del venerado héroe Máximo a manos de su tío, Lucius (Paul Mescal) se ve obligado a ingresar al Coliseo después de que su casa es conquistada.", "2 horas 28 minutos", "https://image.tmdb.org/t/p/original/vK1sK1B3WglfgVWPn6Xj4nNsw1q.jpg"));
            peliculas.Add(new Pelicula(4, "Terrifier 3", "Tras los impactantes acontecimientos de Terrifier 2, la sobreviviente Sienna se ve acosada por visiones perturbadoras y comienza a darse cuenta de que no hay forma de escapar de su pasado ni de superar a Art el Payaso.", "2 horas 5 minutos", "https://image.tmdb.org/t/p/original/cZZ769OJGABhKkd5xnTRQz2Y2x0.jpg"));
            peliculas.Add(new Pelicula(5, "Red One", "Después de que un villano secuestra a Santa Claus del Polo Norte, un agente de ELF debe asociarse con el rastreador más experimentado del mundo para encontrar a Santa Claus y salvar la Navidad.", "2 horas 3 minutos", "https://image.tmdb.org/t/p/original/edgKt5Z7QcH2TZfaT7vXOrs36zt.jpg"));
            peliculas.Add(new Pelicula(6, "Smile 2", "A punto de embarcarse en una nueva gira mundial, la sensación pop global Skye Riley (Naomi Scott) comienza a experimentar eventos cada vez más aterradores e inexplicables.", "2 horas 7 minutos", "https://image.tmdb.org/t/p/original/yqfeWTE1RCl60E8V3SfFAgr0jj6.jpg"));

        }
    }
}
