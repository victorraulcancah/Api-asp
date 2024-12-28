using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioController : ControllerBase
    {
        private readonly IServicio _servicio;

        public ServicioController(IServicio servicio)
        {
            _servicio = servicio;
        }

        // Listar todos los servicios
        [HttpGet]
        public async Task<IActionResult> ListarServicios()
        {
            var servicios = await _servicio.ListarServicio();
            return Ok(servicios);
        }

        // Mostrar un servicio por su código
        [HttpGet("{codigo}")]
        public async Task<IActionResult> MostrarServicio(int codigo)
        {
            var servicio = await _servicio.MostrarServicio(codigo);
            if (servicio == null)
            {
                return NotFound($"No se encontró el servicio con código {codigo}");
            }
            return Ok(servicio);
        }

        // Registrar un nuevo servicio
        [HttpPost]
        public async Task<IActionResult> RegistrarServicio([FromBody] ModeloServicio servicio)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var registro = await _servicio.RegistrarServicio(servicio);
            if (!registro)
                return StatusCode(500, "Error al registrar el servicio");

            return CreatedAtAction(nameof(MostrarServicio), new { codigo = servicio.CodigoServicio }, servicio);
        }

        // Actualizar un servicio
        [HttpPut]
        public async Task<IActionResult> ActualizarServicio([FromBody] ModeloServicio servicio)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var registro = await _servicio.ActualizarServicio(servicio);
            if (!registro)
                return StatusCode(500, "Error al actualizar el servicio");

            return NoContent();
        }

        // Eliminar un servicio
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> EliminarServicio(int codigo)
        {
            var registro = await _servicio.EliminarServicio(codigo);
            if (!registro)
                return StatusCode(500, $"Error al eliminar el servicio con código {codigo}");

            return NoContent();
        }
    }
}

