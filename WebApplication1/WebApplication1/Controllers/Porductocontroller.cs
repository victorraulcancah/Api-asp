using WebApplication1.Data;
using WebApplication1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : Controller
    {
        private IProducto _producto;

        public ProductoController(IProducto producto)
        {
            _producto = producto;
        }

        // Listar todos los productos
        [HttpGet]
        public async Task<IActionResult> ListarProducto()
        {
            var productos = await _producto.ListarProducto();
            return Ok(productos);
        }

        // Mostrar un producto por su código
        [HttpGet("{codigo}")]
        public async Task<IActionResult> MostrarProducto(String codigo)
        {
            
            return Ok(await _producto.MostrarProducto(codigo));
        }

        // Registrar un nuevo producto
        [HttpPost]
        public async Task<IActionResult> RegistrarProducto([FromBody] ModeloProducto producto)
        {
            if (producto == null)
                return BadRequest("Los datos del producto no pueden estar vacíos.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var registro = await _producto.RegistrarProducto(producto);

            return Created("Producto registrado", registro);
        }

        // Actualizar un producto
        [HttpPut]
        public async Task<IActionResult> ActualizarProducto([FromBody] ModeloProducto producto)
        {
            if (producto == null)
                return BadRequest("Los datos del producto no pueden estar vacíos.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var registro = await _producto.ActualizarProducto(producto);

            return Created("Producto actualizado", registro);
        }

        // Eliminar un producto
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> EliminarProducto(String codigo)
        {
            var registro = await _producto.EliminarProducto(codigo);

            return Created("Producto eliminado", registro);
        }
    }
}

