using WebApplication1.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace WebApplication1.Data
{
    public interface IPedido
    {
        Task<IEnumerable<ModeloPedido>> ListarPedidos();
        Task<ModeloPedido> MostrarPedido(int codigo);
        Task<bool> RegistrarPedido(ModeloPedido pedido);
        Task<bool> ActualizarPedido(ModeloPedido pedido);
        Task<bool> EliminarPedido(int codigo);
    }
}
