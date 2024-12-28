using WebApplication1.Model;
using MySql.Data.MySqlClient;
using Dapper;

namespace WebApplication1.Data
{
    public class CRUDCliente : ICliente
    {
        private Configuracion _conexion;
        public CRUDCliente(Configuracion conexion) 
        {
            _conexion = conexion;
        }
        protected MySqlConnection Conectar() 
        {
            return new MySqlConnection(_conexion.Conectar);
        }
        public async Task<IEnumerable<ModeloCliente>> ListarCliente()
        {
            try
            {
                using var bd = Conectar();
                await bd.OpenAsync();  // Asegúrate de abrir la conexión a la base de datos

                // Usar alias para que coincidan los nombres de las columnas con las propiedades del modelo
                string cad_sql = @"SELECT codigo_cliente AS CodigoCliente, 
                                  nombre, 
                                  correo, 
                                  telefono, 
                                  tipo_cliente AS TipoCliente, 
                                  ruc, 
                                  direccion, 
                                  fecha_registro AS FechaRegistro 
                           FROM tb_cliente";
                var clientes = await bd.QueryAsync<ModeloCliente>(cad_sql);

                // Verificar si no se encontraron clientes en la base de datos
                if (!clientes.Any())
                {
                    Console.WriteLine("No se encontraron clientes en la base de datos.");
                }

                return clientes;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar clientes", ex);
            }
        }

        public async Task<ModeloCliente> MostarCliente(string codigo)
        {
            var bd = Conectar();
            String cad_sql = @"select * from tb_cliente where codigo_cliente = @cod";
            return await bd.QueryFirstAsync<ModeloCliente>(cad_sql, new { cod = codigo});
        }
        public async Task<bool> RegistrarCliente(ModeloCliente cliente)
        {
            // Conectar a la base de datos
            var bd = Conectar();

            // Definir la cadena SQL para el insert
            string cad_sql = @"INSERT INTO tb_cliente (codigo_cliente, nombre, correo, telefono, tipo_cliente, ruc, direccion, fecha_registro) 
                       VALUES (@codigo_cliente, @nombre, @correo, @telefono, @tipo_cliente, @ruc, @direccion, @fecha_registro)";

            // Ejecutar la consulta con Dapper
            int n = await bd.ExecuteAsync(cad_sql, new
            {
                codigo_cliente = cliente.CodigoCliente,
                nombre = cliente.Nombre,
                correo = cliente.Correo,
                telefono = cliente.Telefono,
                tipo_cliente = cliente.TipoCliente,
                ruc = cliente.Ruc,
                direccion = cliente.Direccion,
                fecha_registro = cliente.FechaRegistro
            });

            // Retornar true si se afectó al menos una fila, lo que indica éxito
            return n > 0;
        }
        public async Task<bool> ActualizarCliente(ModeloCliente cliente)
        {
            var bd = Conectar();
            string cad_sql = @"UPDATE tb_cliente 
                           SET nombre = @nombre, 
                               correo = @correo, 
                               telefono = @telefono, 
                               tipo_cliente = @tipo_cliente, 
                               ruc = @ruc, 
                               direccion = @direccion, 
                               fecha_registro = @fecha_registro 
                           WHERE codigo_cliente = @codigo_cliente";

            int n = await bd.ExecuteAsync(cad_sql, new
            {
                codigo_cliente = cliente.CodigoCliente,
                nombre = cliente.Nombre,
                correo = cliente.Correo,
                telefono = cliente.Telefono,
                tipo_cliente = cliente.TipoCliente,
                ruc = cliente.Ruc,
                direccion = cliente.Direccion,
                fecha_registro = cliente.FechaRegistro
            });

            return n > 0;
        }

        public async Task<bool> EliminarCliente(String codigo)
        {
            var bd = Conectar();

            // Definir la cadena SQL para el DELETE
            String cad_sql = @"DELETE FROM tb_cliente WHERE codigo_cliente = @codigo_cliente";

            // Ejecutar la consulta con Dapper
            int n = await bd.ExecuteAsync(cad_sql, new
            {
                codigo_cliente = codigo
            });

            // Retornar true si se afectó al menos una fila, lo que indica éxito
            return n > 0;
        }
    }
}
