using WebApplication1.Model;
using MySql.Data.MySqlClient;
using Dapper;

namespace WebApplication1.Data
{
    public class CRUDProducto : IProducto
    {
        private Configuracion _conexion;

        public CRUDProducto(Configuracion conexion)
        {
            _conexion = conexion;
        }

        public MySqlConnection Conectar()
        {
            return new MySqlConnection(_conexion.Conectar);
        }

        public async Task<IEnumerable<ModeloProducto>> ListarProducto()
        {
            try
            {
                using var bd = Conectar();
                await bd.OpenAsync(); // Abre la conexión a la base de datos

                // Usar alias para garantizar que los nombres de las columnas coincidan con las propiedades del modelo
                string cad_sql = @"SELECT 
                           codigo_producto AS CodigoProducto, 
                           nombre, 
                           descripcion, 
                           costo, 
                           ganancia, 
                           precio_venta AS PrecioVenta, 
                           cantidad_stock AS CantidadStock, 
                           estado_producto AS EstadoProducto, 
                           producto_codigo_marca AS ProductoCodigoMarca, 
                           producto_codigo_categoria AS ProductoCodigoCategoria 
                       FROM tb_producto";

                var productos = await bd.QueryAsync<ModeloProducto>(cad_sql);

                if (!productos.Any())
                {
                    Console.WriteLine("No se encontraron productos en la base de datos.");
                }

                return productos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar productos", ex);
            }
        }


        // Show a product by code
        public async Task<ModeloProducto> MostrarProducto(string codigo)
        {
            using var bd = Conectar();
            string cad_sql = "SELECT * FROM tb_producto WHERE codigo_producto = @codigo";
            return await bd.QueryFirstOrDefaultAsync<ModeloProducto>(cad_sql, new { codigo });
        }

        // Register a new product
        public async Task<bool> RegistrarProducto(ModeloProducto producto)
        {
            using var bd = Conectar();
            string cad_sql = @"
                INSERT INTO tb_producto (nombre, descripcion, costo, ganancia, precio_venta, cantidad_stock, estado_producto, producto_codigo_marca, producto_codigo_categoria)
                VALUES (@Nombre, @Descripcion, @Costo, @Ganancia, @PrecioVenta, @CantidadStock, @EstadoProducto, @ProductoCodigoMarca, @ProductoCodigoCategoria)";

            var resultado = await bd.ExecuteAsync(cad_sql, new
            {
                producto.Nombre,
                producto.Descripcion,
                producto.Costo,
                producto.Ganancia,
                producto.PrecioVenta,
                producto.CantidadStock,
                producto.EstadoProducto,
                producto.ProductoCodigoMarca,
                producto.ProductoCodigoCategoria
            });
            return resultado > 0; // Return true if one or more rows were affected
        }

        // Update a product
        public async Task<bool> ActualizarProducto(ModeloProducto producto)
        {
            using var bd = Conectar();
            string cad_sql = @"
                UPDATE tb_producto 
                SET nombre = @Nombre, 
                    descripcion = @Descripcion, 
                    costo = @Costo, 
                    ganancia = @Ganancia, 
                    precio_venta = @PrecioVenta, 
                    cantidad_stock = @CantidadStock, 
                    estado_producto = @EstadoProducto, 
                    producto_codigo_marca = @ProductoCodigoMarca, 
                    producto_codigo_categoria = @ProductoCodigoCategoria
                WHERE codigo_producto = @CodigoProducto";

            var resultado = await bd.ExecuteAsync(cad_sql, new
            {
                producto.Nombre,
                producto.Descripcion,
                producto.Costo,
                producto.Ganancia,
                producto.PrecioVenta,
                producto.CantidadStock,
                producto.EstadoProducto,
                producto.ProductoCodigoMarca,
                producto.ProductoCodigoCategoria,
                producto.CodigoProducto
            });
            return resultado > 0; // Return true if one or more rows were affected
        }

        // Delete a product
        public async Task<bool> EliminarProducto(string codigo)
        {
            using var bd = Conectar();
            string cad_sql = "DELETE FROM tb_producto WHERE codigo_producto = @codigo";
            var resultado = await bd.ExecuteAsync(cad_sql, new { codigo });
            return resultado > 0; // Return true if one or more rows were affected
        }
    }
}
