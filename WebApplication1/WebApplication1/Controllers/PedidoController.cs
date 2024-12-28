using WebApplication1.Data;
using WebApplication1.Model;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedido _pedido;

        public PedidoController(IPedido pedido)
        {
            _pedido = pedido;
        }

        // Listar todos los pedidos
        [HttpGet]
        public async Task<IActionResult> ListarPedidos()
        {
            var pedidos = await _pedido.ListarPedidos();
            return Ok(pedidos);
        }

        // Mostrar un pedido por su código
        [HttpGet("{codigo}")]
        public async Task<IActionResult> MostrarPedido(int codigo)
        {
            var pedido = await _pedido.MostrarPedido(codigo);
            if (pedido == null)
            {
                return NotFound($"No se encontró el pedido con código {codigo}");
            }
            return Ok(pedido);
        }

        // Registrar un nuevo pedido
        [HttpPost]
        public async Task<IActionResult> RegistrarPedido([FromBody] ModeloPedido pedido)
        {
            if (pedido == null)
                return BadRequest("Los datos del pedido no pueden estar vacíos.");

            var resultado = await _pedido.RegistrarPedido(pedido);
            if (!resultado)
                return StatusCode(500, "Error al registrar el pedido");

            return CreatedAtAction(nameof(MostrarPedido), new { codigo = pedido.CodigoPedido }, pedido);
        }

        // Actualizar un pedido
        [HttpPut]
        public async Task<IActionResult> ActualizarPedido([FromBody] ModeloPedido pedido)
        {
            if (pedido == null)
                return BadRequest("Los datos del pedido no pueden estar vacíos.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var registro = await _pedido.ActualizarPedido(pedido);
            if (!registro)
                return StatusCode(500, "Error al actualizar el pedido");

            return NoContent();
        }

        // Eliminar un pedido
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> EliminarPedido(int codigo)
        {
            var registro = await _pedido.EliminarPedido(codigo);
            if (!registro)
                return StatusCode(500, $"Error al eliminar el pedido con código {codigo}");

            return NoContent();
        }
    }
}
