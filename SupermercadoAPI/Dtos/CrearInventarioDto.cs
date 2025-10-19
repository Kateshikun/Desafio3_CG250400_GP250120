using System.ComponentModel.DataAnnotations;

namespace SupermercadoAPI.Dtos
{
    public class CrearInventarioDto
    {
        [Required(ErrorMessage = "El ID del producto es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del producto debe ser válido.")]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "La cantidad inicial es obligatoria.")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad debe ser un número positivo o cero.")]
        public int CantidadActual { get; set; }
    }
}
