using WebApplication1.Data;
using WebApplication1.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private ICategoria _categoria;

        public CategoriaController(ICategoria categoria)
        {
            _categoria = categoria;
        }
        [HttpGet]
        public async Task<IActionResult> ListarCategoria()
        {
            var categorias = await _categoria.ListarCategoria();

            // Depuración
            Console.WriteLine("Categorías recuperadas: " + JsonConvert.SerializeObject(categorias));

            if (!categorias.Any())
            {
                return NotFound("No se encontraron categorías.");
            }

            return Ok(categorias);
        }


        [HttpGet("{codigo}")]
        public async Task<IActionResult> MostrarCategoria(int codigo)
        {
            try
            {
                var categoria = await _categoria.MostrarCategoria(codigo);
                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarCategoria([FromBody] ModeloCategoria categoria)
        {
            if (categoria == null)
                return BadRequest("Los datos de la categoría no pueden estar vacíos.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var registro = await _categoria.RegistrarCategoria(categoria);
            if (!registro)
                return StatusCode(500, "Error al registrar la categoría");

            return Ok("Categoría registrada con éxito");
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarCategoria([FromBody] ModeloCategoria categoria)
        {
            if (categoria == null)
                return BadRequest("Los datos de la categoría no pueden estar vacíos.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var registro = await _categoria.ActualizarCategoria(categoria);
            if (!registro)
                return StatusCode(500, "Error al actualizar la categoría");

            return Ok("Categoría actualizada con éxito");
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> EliminarCategoria(int codigo)
        {
            var registro = await _categoria.EliminarCategoria(codigo);
            if (!registro)
                return StatusCode(500, $"Error al eliminar la categoría con código {codigo}");

            return Ok("Categoría eliminada con éxito");
        }
    }
}
