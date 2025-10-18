using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupermercadoAPI.Models
{
    public class Inventarios
    {
        [Key]
        public int idInventario { get; set; }
        [ForeignKey("idProducto")]
        public int idProducto { get; set; }
        [Required]
        public int CantidadActual { get; set; }

        [RegularExpression(@"^\d{2}/\d{2}/\d{4}\s\d{2}:\d{2}:\d{2}$")] 
        public DateTime fechaActualizacion { get; set; } = DateTime.Now;
        public Productos? producto { get; set; }
        public Inventarios() { }
        public Inventarios(int idInventario, int idProducto, DateTime fechaActualizacion, int CantidadActual)
        {
            this.idInventario = idInventario;
            this.idProducto = idProducto;
            this.CantidadActual = CantidadActual;
            this.fechaActualizacion = fechaActualizacion;
        }
    }
}
