using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        public static List<Pago> pagos = new List<Pago>();
        private const decimal precioAsiento = 6.00m;

        [HttpPost]
        public IActionResult RegistrarPago([FromBody] Pago nuevoPago)
        {
            // Validar si los datos de pago son correctos
            if (nuevoPago == null || nuevoPago.AsientosSeleccionados == null || !nuevoPago.AsientosSeleccionados.Any())
            {
                return BadRequest("El pago debe contener datos válidos y al menos un asiento seleccionado.");
            }

            // Verificar disponibilidad de asientos
            foreach (var asientoNumero in nuevoPago.AsientosSeleccionados)
            {
                var asiento = AsientoController.Asientos
                    .FirstOrDefault(a => a.Numero == asientoNumero && a.FuncionId == nuevoPago.FuncionId);

                if (asiento == null)
                {
                    return BadRequest($"El asiento {asientoNumero} no existe para la función con ID {nuevoPago.FuncionId}.");
                }

                if (!asiento.Disponible)
                {
                    return BadRequest($"El asiento {asientoNumero} no está disponible.");
                }

                // Reservar asiento
                asiento.Disponible = false;
            }

            // Calcular el precio total basado en el número de asientos seleccionados
            decimal precioTotal = nuevoPago.AsientosSeleccionados.Count * precioAsiento;

            nuevoPago.Id = pagos.Any() ? pagos.Max(p => p.Id) + 1 : 1;

            pagos.Add(nuevoPago);

            return CreatedAtAction(nameof(ObtenerPago), new { id = nuevoPago.Id }, new
            {
                nuevoPago.Id,
                nuevoPago.FuncionId,
                nuevoPago.PeliculaId,
                AsientosSeleccionados = nuevoPago.AsientosSeleccionados,
                PrecioTotal = precioTotal, 
                Cliente = new
                {
                    nuevoPago.Nombre,
                    nuevoPago.Apellido,
                    nuevoPago.Direccion,
                    nuevoPago.CodigoPostal,
                    nuevoPago.Ciudad,
                    nuevoPago.CorreoElectronico,
                    nuevoPago.Telefono
                }
            });
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

            // Calcular el precio total basado en los asientos seleccionados
            decimal precioTotal = pago.AsientosSeleccionados.Count * precioAsiento;

            return Ok(new
            {
                pago.Id,
                pago.FuncionId,
                pago.PeliculaId,
                AsientosSeleccionados = pago.AsientosSeleccionados,
                PrecioTotal = precioTotal, 
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
