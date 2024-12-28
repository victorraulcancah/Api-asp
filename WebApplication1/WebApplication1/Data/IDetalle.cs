using WebApplication1.Model;
namespace WebApplication1.Data
{
    public interface IDetalle
    {
        Task<IEnumerable<ModeloDetalle>> ListarDetalles();
        Task<ModeloDetalle> MostrarDetalle(int codigo);
        Task<bool> RegistrarDetalle(ModeloDetalle detalle);
        Task<bool> ActualizarDetalle(ModeloDetalle detalle);
        Task<bool> EliminarDetalle(int codigo);
    }
}
