namespace WebApplication1.Model
{
    public class ModeloDetalle
    {
        public int CodigoDetalle { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public int CodigoPedido { get; set; }
        public int CodigoProducto { get; set; }
        public int CodigoServicio { get; set; }
    }
}
