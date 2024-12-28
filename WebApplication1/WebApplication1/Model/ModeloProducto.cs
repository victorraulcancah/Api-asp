namespace WebApplication1.Model
{
    public class ModeloProducto
    {
        public String CodigoProducto { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public float Costo { get; set; }
        public float Ganancia { get; set; }
        public float PrecioVenta { get; set; }
        public int CantidadStock { get; set; }
        public String EstadoProducto { get; set; }
        public String ProductoCodigoMarca { get; set; }
        public String ProductoCodigoCategoria { get; set; }
    }
}
