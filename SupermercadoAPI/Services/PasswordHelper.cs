using BCrypt.Net;
namespace SupermercadoAPI.Services
{
    public class PasswordHelper: IpasswordHelper //Esta clase manejará la lógica de cifrado de contraseñas utilizando el algoritmo BCrypt
    {
        public string HashPassword(string password) //Método para cifrar una contraseña
        {
            return BCrypt.Net.BCrypt.HashPassword(password); //Devuelve la contraseña cifrada utilizando BCrypt
        }
        public bool VerifyPassword(string hashedPassword, string providedPassword) //Método para verificar si una contraseña proporcionada coincide con la contraseña cifrada
        {
            return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword); //Devuelve true si coinciden, false en caso contrario
        }
    }
}
