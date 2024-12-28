using WebApplication1.Data;
using WebApplication1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Clientecontroller : Controller
    {
        private ICliente _cliente;
        public Clientecontroller(ICliente cliente)
        {
            _cliente = cliente;
        }



        [HttpGet]
        public async Task<IActionResult> ListarCliente()
        {
            return Ok(await _cliente.ListarCliente());
        }



        [HttpGet("{codigo}")]
        public async Task<IActionResult> MostarCliente(String codigo)
        {
            return Ok(await _cliente.MostarCliente(codigo));
        }



        [HttpPost]
        public async Task<IActionResult> RegistrarCliente([FromBody] ModeloCliente cliente)
        {
            if (cliente == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var registro = await _cliente.RegistrarCliente(cliente);

            return Created("Producto registrado",registro);
            
        }
        [HttpPut]
        public async Task<IActionResult> ActualizarCliente([FromBody] ModeloCliente cliente)
        {
            if(cliente == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var registro = await _cliente.ActualizarCliente(cliente);

            return Created("Cliente actualizado", registro);
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarCliente(String codigo)
        {
            var registro = await _cliente.EliminarCliente(codigo);
            return Created("Cliente Eliminado", registro);
        }

    }
}
