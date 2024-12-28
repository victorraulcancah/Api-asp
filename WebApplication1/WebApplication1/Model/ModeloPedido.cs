using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Model
{
    public class ModeloPedido
    {
        [Required]
        public int CodigoPedido { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required]
        [StringLength(50)]
        public string Estado { get; set; }

        [Required]
        [StringLength(50)]
        public string TipoPedido { get; set; }

        [Required]
        public int PedidoCodigoCliente { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El total del pedido debe ser mayor que cero.")]
        public float TotalPedido { get; set; }
    }
}

