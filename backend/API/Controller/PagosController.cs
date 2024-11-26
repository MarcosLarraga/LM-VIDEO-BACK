using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private static List<Pago> pagos = new List<Pago>();

        // POST: Crear un nuevo pago
        [HttpPost]
        public ActionResult<Pago> CreatePago([FromBody] Pago nuevoPago)
        {
            nuevoPago.Id = pagos.Any() ? pagos.Max(p => p.Id) + 1 : 1; // Generar un nuevo ID
            nuevoPago.Fecha = DateTime.Now; // Asignar la fecha actual
            pagos.Add(nuevoPago);
            return CreatedAtAction(nameof(GetPago), new { id = nuevoPago.Id }, nuevoPago);
        }

        // GET: Obtener detalles de un pago por ID
        [HttpGet("{id}")]
        public ActionResult<Pago> GetPago(int id)
        {
            var pago = pagos.FirstOrDefault(p => p.Id == id);
            if (pago == null)
            {
                return NotFound($"No se encontró el pago con ID {id}");
            }
            return Ok(pago);
        }

        // GET: Obtener todos los pagos
        [HttpGet]
        public ActionResult<IEnumerable<Pago>> GetPagos()
        {
            return Ok(pagos);
        }

        // PUT: Actualizar el estado de un pago
        [HttpPut("{id}")]
        public IActionResult UpdatePago(int id, [FromBody] Pago updatedPago)
        {
            var pago = pagos.FirstOrDefault(p => p.Id == id);
            if (pago == null)
            {
                return NotFound($"No se encontró el pago con ID {id}");
            }

            // Actualizar los datos del pago
            pago.NombreCliente = updatedPago.NombreCliente;
            pago.CorreoCliente = updatedPago.CorreoCliente;
            pago.Metodo = updatedPago.Metodo;
            pago.TransaccionExitosa = updatedPago.TransaccionExitosa;

            return NoContent();
        }
    }
}
