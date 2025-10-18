namespace SupermercadoAPI.Services
{
    public interface IpasswordHelper //interfaz para el manejo de contraseñas, se inyectará en el controlador de autenticación
    {
        string HashPassword(string password);
        bool VerifyPassword(string hashedPassword, string providedPassword);
    }
}
