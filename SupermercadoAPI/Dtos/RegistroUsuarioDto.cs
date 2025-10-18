using System.ComponentModel.DataAnnotations;

namespace SupermercadoAPI.Dtos
{
    public class RegistroUsuarioDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
        [StringLength(255)]
        public string Correo { get; set; } 

        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string Contrasena { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio.")]
        public string Rol { get; set; }
    }
}
