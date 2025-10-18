using System.ComponentModel.DataAnnotations;

namespace SupermercadoAPI.Models
{
    public class Usuarios
    {
        [Key]
        public int idUsuario { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string correo { get; set; }
        [Required]
        public string contrasenaHash { get; set; }
        [Required]
        public string rol { get; set; } //Puede ser administrador o empleado

        public Usuarios() { }

        public Usuarios(int idUsuario, string nombre, string correo, string contrasenaHash, string rol)
        {
            this.idUsuario = idUsuario;
            this.nombre = nombre;
            this.correo = correo;
            this.contrasenaHash = contrasenaHash;
            this.rol = rol;
        }
    }
}
