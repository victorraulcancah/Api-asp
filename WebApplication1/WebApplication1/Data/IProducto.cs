using WebApplication1.Model;

namespace WebApplication1.Data
{
    public interface IProducto
    {
        Task<IEnumerable<ModeloProducto>> ListarProducto(); // Para obtener todos los productos
        Task<ModeloProducto> MostrarProducto(String codigo); // Mostrar un producto por su código
        Task<bool> RegistrarProducto(ModeloProducto producto); // Registrar un nuevo producto
        Task<bool> ActualizarProducto(ModeloProducto producto); // Actualizar un producto existente
        Task<bool> EliminarProducto(String codigo); // Eliminar un producto por su código
    }
}
