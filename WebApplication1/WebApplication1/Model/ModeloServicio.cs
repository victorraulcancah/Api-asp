namespace WebApplication1.Model
{
    public class ModeloServicio
    {
        public int CodigoServicio { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public float Costo { get; set; }
        public int DuracionEstimada { get; set; }  // Duración estimada en minutos
        public string? EstadoServicio { get; set; }
    }
}
