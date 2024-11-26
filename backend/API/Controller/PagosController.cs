using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;


namespace CineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        // Lista estática para almacenar pagos en memoria
        public static List<Pago> pagos = new List<Pago>();

        // POST: api/pagos
        [HttpPost]
        public IActionResult RegistrarPago([FromBody] Pago nuevoPago)
        {
            if (nuevoPago == null || nuevoPago.AsientosSeleccionados == null || !nuevoPago.AsientosSeleccionados.Any())
            {
                return BadRequest("El pago debe contener datos válidos y al menos un asiento seleccionado.");
            }

            // Asignar un ID único al pago
            nuevoPago.Id = pagos.Any() ? pagos.Max(p => p.Id) + 1 : 1;

            // Agregar el nuevo pago a la lista
            pagos.Add(nuevoPago);

            // Retornar el pago registrado
            return CreatedAtAction(nameof(ObtenerPago), new { id = nuevoPago.Id }, nuevoPago);
        }

        // GET: api/pagos/{id}
        [HttpGet("{id}")]
        public IActionResult ObtenerPago(int id)
        {
            // Buscar el pago por ID
            var pago = pagos.FirstOrDefault(p => p.Id == id);
            if (pago == null)
            {
                return NotFound($"No se encontró el pago con ID {id}.");
            }

            // Retornar los detalles del pago
            return Ok(new
            {
                pago.Id,
                pago.FuncionId,
                pago.PeliculaId,
                AsientosSeleccionados = pago.AsientosSeleccionados,
                Cliente = new
                {
                    pago.Nombre,
                    pago.Apellido,
                    pago.Direccion,
                    pago.CodigoPostal,
                    pago.Ciudad,
                    pago.CorreoElectronico,
                    pago.Telefono
                }
            });
        }
    }
}
