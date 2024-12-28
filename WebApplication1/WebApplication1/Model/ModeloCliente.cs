namespace WebApplication1.Model
{
    public class ModeloCliente
    {
        public int CodigoCliente { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string? TipoCliente { get; set; }  // Asegúrate de que sea nullable si es opcional
        public string? Ruc { get; set; }
        public string? Direccion { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
