using System.ComponentModel.DataAnnotations;

namespace SupermercadoAPI.Dtos
{
    public class InventarioActualizacionDto
    {
        [Required(ErrorMessage = "La cantidad a actualizar es obligatoria.")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad debe ser un número positivo o cero.")]
        public int CantidadActual { get; set; }

    }
}
