using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupermercadoAPI.Models
{
    public class Productos
    {
        [Key]
        public int idProducto { get; set; }
        [MaxLength(150)]
        public string nombre { get; set; }
        [Range(0, 999999.99)]
        [Required]
        public decimal precio { get; set; }
        [ForeignKey("idCategoria")]
        public int idCategoria { get; set; }

        [ForeignKey("idCategoria")]
        public Categorias? categoria { get; set; }
        public Inventarios? Inventario { get; set; }
        public Productos() { }
        public Productos(int idProducto, string nombre, decimal precio, int idCategoria, Categorias categoria, Inventarios Inventario)
        {
            this.idProducto = idProducto;
            this.nombre = nombre;
            this.precio = precio;
            this.idCategoria = idCategoria;
            this.categoria = categoria;
            this.Inventario = Inventario;
        }
    }
}
