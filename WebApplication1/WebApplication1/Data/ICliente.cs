using WebApplication1.Model;
namespace WebApplication1.Data
{
    public interface ICliente
    {
        Task<IEnumerable<ModeloCliente>> ListarCliente();
        Task<ModeloCliente> MostarCliente(string codigo); // Usamos string para el identificador si es texto
        Task<bool> RegistrarCliente(ModeloCliente cliente); // Se corregió "Registar" a "Registrar"
        Task<bool> ActualizarCliente(ModeloCliente cliente);
        Task<bool> EliminarCliente(string codigo);

    }
}
