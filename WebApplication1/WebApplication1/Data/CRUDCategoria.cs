using WebApplication1.Model;
using MySql.Data.MySqlClient;
using Dapper;

namespace WebApplication1.Data
{
    public class CRUDCategoria : ICategoria
    {
        private Configuracion _conexion;

        public CRUDCategoria(Configuracion conexion)
        {
            _conexion = conexion;
        }

        // Cambia a un método en vez de una propiedad
        private MySqlConnection Conectar()
        {
            return new MySqlConnection(_conexion.Conectar);
        }

        public async Task<IEnumerable<ModeloCategoria>> ListarCategoria()
        {
            try
            {
                using var bd = Conectar();
                await bd.OpenAsync();
                // Usar alias para garantizar que los nombres de las columnas coincidan con las propiedades del modelo
                string cad_sql = "SELECT codigo_categoria AS CodigoCategoria, nombre, descripcion FROM tb_categoria";
                var categorias = await bd.QueryAsync<ModeloCategoria>(cad_sql);

                if (!categorias.Any())
                {
                    Console.WriteLine("No se encontraron categorías en la base de datos.");
                }

                return categorias;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar categorías", ex);
            }
        }



        public async Task<ModeloCategoria> MostrarCategoria(int codigo)
        {
            using var bd = Conectar();
            await bd.OpenAsync();
            string sql = "SELECT * FROM tb_categoria WHERE codigo_categoria = @codigo";
            var categoria = await bd.QueryFirstOrDefaultAsync<ModeloCategoria>(sql, new { codigo });

            if (categoria == null)
            {
                throw new Exception($"Categoría con código {codigo} no encontrada.");
            }

            return categoria;
        }

        public async Task<bool> RegistrarCategoria(ModeloCategoria categoria)
        {
            using var bd = Conectar();
            await bd.OpenAsync();
            string sql = @"INSERT INTO tb_categoria (nombre, descripcion)
                           VALUES (@Nombre, @Descripcion)";
            int resultado = await bd.ExecuteAsync(sql, new
            {
                categoria.Nombre,
                categoria.Descripcion
            });
            return resultado > 0;
        }

        public async Task<bool> ActualizarCategoria(ModeloCategoria categoria)
        {
            using var bd = Conectar();
            await bd.OpenAsync();
            string sql = @"UPDATE tb_categoria 
                           SET nombre = @Nombre, descripcion = @Descripcion
                           WHERE codigo_categoria = @CodigoCategoria";
            int resultado = await bd.ExecuteAsync(sql, new
            {
                categoria.Nombre,
                categoria.Descripcion,
                categoria.CodigoCategoria
            });
            return resultado > 0;
        }

        public async Task<bool> EliminarCategoria(int codigo)
        {
            using var bd = Conectar();
            await bd.OpenAsync();
            string sql = "DELETE FROM tb_categoria WHERE codigo_categoria = @codigo";
            int resultado = await bd.ExecuteAsync(sql, new { codigo });
            return resultado > 0;
        }
    }
}
