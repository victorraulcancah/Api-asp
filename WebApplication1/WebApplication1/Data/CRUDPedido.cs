using WebApplication1.Model;
using MySql.Data.MySqlClient;
using Dapper;

namespace WebApplication1.Data
{
    public class CRUDPedido : IPedido
    {
        private Configuracion _conexion;

        public CRUDPedido(Configuracion conexion)
        {
            _conexion = conexion;
        }

        protected MySqlConnection Conectar()
        {
            return new MySqlConnection(_conexion.Conectar);
        }
        public async Task<IEnumerable<ModeloPedido>> ListarPedidos()
        {
            try
            {
                using var bd = Conectar();
                await bd.OpenAsync(); // Abre la conexión a la base de datos

                // Usar alias para garantizar que los nombres de las columnas coincidan con las propiedades del modelo
                string cad_sql = @"SELECT 
                           codigo_pedido AS CodigoPedido, 
                           fecha, 
                           estado, 
                           tipo_pedido AS TipoPedido, 
                           pedido_codigo_cliente AS PedidoCodigoCliente, 
                           total_pedido AS TotalPedido 
                       FROM tb_pedido";

                var pedidos = await bd.QueryAsync<ModeloPedido>(cad_sql);

                if (!pedidos.Any())
                {
                    Console.WriteLine("No se encontraron pedidos en la base de datos.");
                }

                return pedidos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar pedidos", ex);
            }
        }


        public async Task<ModeloPedido> MostrarPedido(int codigo)
        {
            using var bd = Conectar();
            string cad_sql = "SELECT * FROM tb_pedido WHERE codigo_pedido = @codigo";
            return await bd.QueryFirstAsync<ModeloPedido>(cad_sql, new { codigo });
        }

        public async Task<bool> RegistrarPedido(ModeloPedido pedido)
        {
            using var bd = Conectar();
            string cad_sql = @"INSERT INTO tb_pedido (fecha, estado, tipo_pedido, pedido_codigo_cliente, total_pedido)
                               VALUES (@Fecha, @Estado, @TipoPedido, @PedidoCodigoCliente, @TotalPedido)";
            var resultado = await bd.ExecuteAsync(cad_sql, new
            {
                pedido.Fecha,
                pedido.Estado,
                pedido.TipoPedido,
                pedido.PedidoCodigoCliente,
                pedido.TotalPedido
            });
            return resultado > 0;
        }

        public async Task<bool> ActualizarPedido(ModeloPedido pedido)
        {
            using var bd = Conectar();
            string cad_sql = @"UPDATE tb_pedido 
                               SET fecha = @Fecha, estado = @Estado, tipo_pedido = @TipoPedido, total_pedido = @TotalPedido 
                               WHERE codigo_pedido = @CodigoPedido";
            var resultado = await bd.ExecuteAsync(cad_sql, new
            {
                pedido.Fecha,
                pedido.Estado,
                pedido.TipoPedido,
                pedido.TotalPedido,
                pedido.CodigoPedido
            });
            return resultado > 0;
        }

        public async Task<bool> EliminarPedido(int codigo)
        {
            using var bd = Conectar();
            string cad_sql = "DELETE FROM tb_pedido WHERE codigo_pedido = @codigo";
            var resultado = await bd.ExecuteAsync(cad_sql, new { codigo });
            return resultado > 0;
        }
    }
}
