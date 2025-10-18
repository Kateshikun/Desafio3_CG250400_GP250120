namespace SupermercadoAPI.Dtos
{
    public class AuthResponseDto //Datos de salida que se enviaran al cliente cuando un usuario se loguee correctamente
                              //Esto sirve para enviar el token JWT y otra información relevante al cliente, para que el cliente tenga tiempo limite para estar en la app y para que vea su informacion
    {
        public string Token { get; set; } = string.Empty;
        public int IdUsuario { get; set; }
        public string Rol { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}
