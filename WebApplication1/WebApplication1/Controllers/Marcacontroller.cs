using WebApplication1.Data;
using WebApplication1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;


namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Marcacontroller : Controller
    {
        private readonly IMarca _marca;
        public Marcacontroller(IMarca marca)
        {
            _marca = marca;
        }
        // Listar todas las marcas
        [HttpGet]
        public async Task<IActionResult> ListarMarca()
        {
            var marcas = await _marca.ListarMarca();
            return Ok(marcas);
        }

        // Mostrar una marca por su código
        [HttpGet("{codigo}")]
        public async Task<IActionResult> MostrarMarca(int codigo)
        {
            var marca = await _marca.MostrarMarca(codigo);
            if (marca == null)
            {
                return NotFound($"No se encontró la marca con código {codigo}");
            }
            return Ok(marca);
        }

        // Registrar una nueva marca
        [HttpPost]
        public async Task<IActionResult> RegistrarMarca([FromBody] ModeloMarca marca)
        {
            if (marca == null)
                return BadRequest("Los datos de la marca no pueden estar vacíos.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var registro = await _marca.RegistrarMarca(marca);

            if (!registro)
                return StatusCode(500, "Error al registrar la marca");

            return Created("Marca registrada", registro);
        }

        // Actualizar una marca
        [HttpPut]
        public async Task<IActionResult> ActualizarMarca([FromBody] ModeloMarca marca)
        {
            if (marca == null)
                return BadRequest("Los datos de la marca no pueden estar vacíos.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var registro = await _marca.ActualizarMarca(marca);

            if (!registro)
                return StatusCode(500, "Error al actualizar la marca");

            return Created("Marca actualizada", registro);
        }

        // Eliminar una marca
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> EliminarMarca(int codigo)
        {
            var registro = await _marca.EliminarMarca(codigo);

            if (!registro)
                return StatusCode(500, $"Error al eliminar la marca con código {codigo}");

            return Ok("Marca eliminada");
        }
    }
}
