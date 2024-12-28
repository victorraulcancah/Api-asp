using Dapper;
using MySql.Data.MySqlClient;
using WebApplication1.Model;

namespace WebApplication1.Data
{
    public class CRUDDetalle : IDetalle
    {
        private readonly Configuracion _conexion;
        public CRUDDetalle(Configuracion conexion)
        {
            _conexion = conexion;
        }

        protected MySqlConnection Conectar()
        {
            return new MySqlConnection(_conexion.Conectar);
        }

        // Listar todos los detalles
        public async Task<IEnumerable<ModeloDetalle>> ListarDetalles()
        {
            try
            {
                using var db = Conectar();
                await db.OpenAsync(); // Abre la conexión a la base de datos

                // Usar alias para garantizar que los nombres de las columnas coincidan con las propiedades del modelo
                string sql = @"SELECT 
                           codigo_detalle AS CodigoDetalle, 
                           cantidad, 
                           subtotal, 
                           total, 
                           detalle_codigo_pedido AS DetalleCodigoPedido, 
                           detalle_codigo_producto AS DetalleCodigoProducto, 
                           detalle_codigo_servicio AS DetalleCodigoServicio 
                       FROM tb_detalle";

                var detalles = await db.QueryAsync<ModeloDetalle>(sql);

                if (!detalles.Any())
                {
                    Console.WriteLine("No se encontraron detalles en la base de datos.");
                }

                return detalles;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar detalles", ex);
            }
        }


        // Mostrar un detalle por su código
        public async Task<ModeloDetalle> MostrarDetalle(int codigo)
        {
            var db = Conectar();
            string sql = "SELECT * FROM tb_detalle WHERE codigo_detalle = @Codigo";
            return await db.QueryFirstAsync<ModeloDetalle>(sql, new { Codigo = codigo });
        }

        // Registrar un nuevo detalle
        public async Task<bool> RegistrarDetalle(ModeloDetalle detalle)
        {
            var query = @"INSERT INTO tb_detalle (cantidad, subtotal, total, detalle_codigo_pedido, detalle_codigo_producto, detalle_codigo_servicio)
                  VALUES (@Cantidad, @Subtotal, @Total, @CodigoPedido, @CodigoProducto, @CodigoServicio)";
            using (var connection = new MySqlConnection(_conexion.Conectar))
            {
                var result = await connection.ExecuteAsync(query, new
                {
                    Cantidad = detalle.Cantidad,
                    Subtotal = detalle.Subtotal,
                    Total = detalle.Total,
                    CodigoPedido = detalle.CodigoPedido,
                    CodigoProducto = detalle.CodigoProducto,
                    CodigoServicio = detalle.CodigoServicio
                });
                return result > 0;
            }
        }


        // Actualizar un detalle
        public async Task<bool> ActualizarDetalle(ModeloDetalle detalle)
        {
            var db = Conectar();
            string sql = @"UPDATE tb_detalle 
                           SET cantidad = @Cantidad, subtotal = @Subtotal, total = @Total, 
                               codigo_pedido = @CodigoPedido, codigo_producto = @CodigoProducto, codigo_servicio = @CodigoServicio
                           WHERE codigo_detalle = @CodigoDetalle";
            var result = await db.ExecuteAsync(sql, detalle);
            return result > 0;
        }

        // Eliminar un detalle
        public async Task<bool> EliminarDetalle(int codigo)
        {
            var db = Conectar();
            string sql = "DELETE FROM tb_detalle WHERE codigo_detalle = @Codigo";
            var result = await db.ExecuteAsync(sql, new { Codigo = codigo });
            return result > 0;
        }
    }
}

