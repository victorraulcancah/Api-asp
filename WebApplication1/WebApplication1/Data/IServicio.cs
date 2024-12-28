using WebApplication1.Model;

namespace WebApplication1.Data
{
    public interface IServicio
    {
        Task<IEnumerable<ModeloServicio>> ListarServicio();
        Task<ModeloServicio> MostrarServicio(int codigo);
        Task<bool> RegistrarServicio(ModeloServicio servicio);
        Task<bool> ActualizarServicio(ModeloServicio servicio);
        Task<bool> EliminarServicio(int codigo);
    }
}
