using WebApplication1.Model;

namespace WebApplication1.Data
{
    public interface ICategoria
    {
        Task<IEnumerable<ModeloCategoria>> ListarCategoria();
        Task<ModeloCategoria> MostrarCategoria(int codigo);
        Task<bool> RegistrarCategoria(ModeloCategoria categoria);
        Task<bool> ActualizarCategoria(ModeloCategoria categoria);
        Task<bool> EliminarCategoria(int codigo);
    }
}
