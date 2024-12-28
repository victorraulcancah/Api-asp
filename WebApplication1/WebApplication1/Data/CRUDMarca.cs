using WebApplication1.Model;
using MySql.Data.MySqlClient;
using Dapper;

namespace WebApplication1.Data
{
    public class CRUDMarca : IMarca
    {
        private Configuracion _conexion;

        public CRUDMarca(Configuracion conexion)
        {
            _conexion = conexion;
        }

        protected MySqlConnection Conectar()
        {
            return new MySqlConnection(_conexion.Conectar);
        }
        public async Task<IEnumerable<ModeloMarca>> ListarMarca()
        {
            try
            {
                using var bd = Conectar();
                await bd.OpenAsync(); // Abre la conexión a la base de datos

                // Usar alias para garantizar que los nombres de las columnas coincidan con las propiedades del modelo
                string cad_sql = @"SELECT 
                           codigo_marca AS CodigoMarca, 
                           nombre, 
                           pais_origen AS PaisOrigen, 
                           descripcion 
                       FROM tb_marca";

                var marcas = await bd.QueryAsync<ModeloMarca>(cad_sql);

                if (!marcas.Any())
                {
                    Console.WriteLine("No se encontraron marcas en la base de datos.");
                }

                return marcas;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar marcas", ex);
            }
        }


        public async Task<ModeloMarca> MostrarMarca(int codigo)
        {
            using var bd = Conectar();
            string cad_sql = "SELECT * FROM tb_marca WHERE codigo_marca = @codigo";
            return await bd.QueryFirstAsync<ModeloMarca>(cad_sql, new { codigo });

        }

        public async Task<bool> RegistrarMarca(ModeloMarca marca)
        {
            using var bd = Conectar();
            string cad_sql = @"INSERT INTO tb_marca (nombre, pais_origen, descripcion)
                               VALUES (@Nombre, @PaisOrigen, @Descripcion)"; // Se agregó pais_origen
            var resultado = await bd.ExecuteAsync(cad_sql, new { marca.Nombre, marca.PaisOrigen, marca.Descripcion });
            return resultado > 0;
        }

        public async Task<bool> ActualizarMarca(ModeloMarca marca)
        {
            using var bd = Conectar();
            string cad_sql = @"UPDATE tb_marca 
                               SET nombre = @Nombre, pais_origen = @PaisOrigen, descripcion = @Descripcion 
                               WHERE codigo_marca = @CodigoMarca"; // Se agregó pais_origen
            var resultado = await bd.ExecuteAsync(cad_sql, new
            {
                marca.Nombre,
                marca.PaisOrigen,
                marca.Descripcion,
                marca.CodigoMarca
            });
            return resultado > 0;
        }

        public async Task<bool> EliminarMarca(int codigo)
        {
            using var bd = Conectar();
            string cad_sql = "DELETE FROM tb_marca WHERE codigo_marca = @codigo";
            var resultado = await bd.ExecuteAsync(cad_sql, new { codigo });
            return resultado > 0;
        }
    }
}
