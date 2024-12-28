using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Model;
namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleController : ControllerBase
    {
        private readonly IDetalle _detalle;

        public DetalleController(IDetalle detalle)
        {
            _detalle = detalle;
        }

        // Listar todos los detalles
        [HttpGet]
        public async Task<IActionResult> ListarDetalles()
        {
            var detalles = await _detalle.ListarDetalles();
            return Ok(detalles);
        }

        // Mostrar un detalle por su código
        [HttpGet("{codigo}")]
        public async Task<IActionResult> MostrarDetalle(int codigo)
        {
            var detalle = await _detalle.MostrarDetalle(codigo);
            if (detalle == null)
            {
                return NotFound($"No se encontró el detalle con código {codigo}");
            }
            return Ok(detalle);
        }

        // Registrar un nuevo detalle
        [HttpPost]
        public async Task<IActionResult> RegistrarDetalle([FromBody] ModeloDetalle detalle)
        {
            if (detalle == null)
                return BadRequest("Los datos del detalle no pueden estar vacíos.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var registro = await _detalle.RegistrarDetalle(detalle);
            if (!registro)
                return StatusCode(500, "Error al registrar el detalle");

            return CreatedAtAction(nameof(MostrarDetalle), new { codigo = detalle.CodigoDetalle }, detalle);
        }

        // Actualizar un detalle
        [HttpPut]
        public async Task<IActionResult> ActualizarDetalle([FromBody] ModeloDetalle detalle)
        {
            if (detalle == null)
                return BadRequest("Los datos del detalle no pueden estar vacíos.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var registro = await _detalle.ActualizarDetalle(detalle);
            if (!registro)
                return StatusCode(500, "Error al actualizar el detalle");

            return NoContent();
        }

        // Eliminar un detalle
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> EliminarDetalle(int codigo)
        {
            var registro = await _detalle.EliminarDetalle(codigo);
            if (!registro)
                return StatusCode(500, $"Error al eliminar el detalle con código {codigo}");

            return NoContent();
        }
    }
}
