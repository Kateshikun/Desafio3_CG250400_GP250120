namespace SupermercadoAPI.Dtos
{
    public class LoginUsuarioDto //Datos de entrada que se obtendran desde el cliente para que un usuario se loguee
    {
        public string correo { get; set; } = string.Empty;

        public string password { get; set; } = string.Empty;
    }
}
