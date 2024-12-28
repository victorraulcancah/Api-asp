using WebApplication1.Model;
using MySql.Data.MySqlClient;
using Dapper;

namespace WebApplication1.Data
{
    public class CRUDServicio : IServicio
    {
        private Configuracion _conexion;

        public CRUDServicio(Configuracion conexion)
        {
            _conexion = conexion;
        }

        protected MySqlConnection Conectar()
        {
            return new MySqlConnection(_conexion.Conectar);
        }

        public async Task<IEnumerable<ModeloServicio>> ListarServicio()
        {
            try
            {
                using var bd = Conectar();
                await bd.OpenAsync(); // Abre la conexión a la base de datos

                // Usar alias para garantizar que los nombres de las columnas coincidan con las propiedades del modelo
                string cad_sql = @"SELECT 
                           codigo_servicio AS CodigoServicio, 
                           nombre, 
                           descripcion, 
                           costo, 
                           duracion_estimada AS DuracionEstimada, 
                           estado_servicio AS EstadoServicio 
                       FROM tb_servicio";

                var servicios = await bd.QueryAsync<ModeloServicio>(cad_sql);

                if (!servicios.Any())
                {
                    Console.WriteLine("No se encontraron servicios en la base de datos.");
                }

                return servicios;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar servicios", ex);
            }
        }


        public async Task<ModeloServicio> MostrarServicio(int codigo)
        {
            using var bd = Conectar();
            string cad_sql = "SELECT * FROM tb_servicio WHERE codigo_servicio = @codigo";
            return await bd.QueryFirstAsync<ModeloServicio>(cad_sql, new { codigo });
        }

        public async Task<bool> RegistrarServicio(ModeloServicio servicio)
        {
            using var bd = Conectar();
            string cad_sql = @"INSERT INTO tb_servicio (nombre, descripcion, costo, duracion_estimada, estado_servicio)
                               VALUES (@Nombre, @Descripcion, @Costo, @DuracionEstimada, @EstadoServicio)";

            int n = await bd.ExecuteAsync(cad_sql, new
            {
                servicio.Nombre,
                servicio.Descripcion,
                servicio.Costo,
                servicio.DuracionEstimada,
                servicio.EstadoServicio
            });

            return n > 0;
        }

        public async Task<bool> ActualizarServicio(ModeloServicio servicio)
        {
            using var bd = Conectar();
            string cad_sql = @"UPDATE tb_servicio 
                               SET nombre = @Nombre, 
                                   descripcion = @Descripcion, 
                                   costo = @Costo, 
                                   duracion_estimada = @DuracionEstimada, 
                                   estado_servicio = @EstadoServicio 
                               WHERE codigo_servicio = @CodigoServicio";

            int n = await bd.ExecuteAsync(cad_sql, new
            {
                servicio.Nombre,
                servicio.Descripcion,
                servicio.Costo,
                servicio.DuracionEstimada,
                servicio.EstadoServicio,
                servicio.CodigoServicio
            });

            return n > 0;
        }

        public async Task<bool> EliminarServicio(int codigo)
        {
            using var bd = Conectar();
            string cad_sql = "DELETE FROM tb_servicio WHERE codigo_servicio = @codigo";
            int n = await bd.ExecuteAsync(cad_sql, new { codigo });
            return n > 0;
        }
    }
}

