using WebApplication1.Model;
namespace WebApplication1.Data
{
    public interface IMarca
    {
        Task<IEnumerable<ModeloMarca>> ListarMarca();
        Task<ModeloMarca> MostrarMarca(int codigo);
        Task<bool> RegistrarMarca(ModeloMarca marca);
        Task<bool> ActualizarMarca(ModeloMarca marca);
        Task<bool> EliminarMarca(int codigo);
    }
}
