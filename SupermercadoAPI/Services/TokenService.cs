using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using SupermercadoAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SupermercadoAPI.Services
{
    public class TokenService: ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerarToken(Usuarios usuario)
        {
            var claims = new[] //Matriz que contendra el token con los datos del usuario
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.idUsuario.ToString()), //Claim es una afirmación sobre el usuario que se incluirá en el token
                new Claim(ClaimTypes.Role, usuario.rol)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"])); //Clave secreta para firmar el token
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //Credenciales de firma utilizando el algoritmo HmacSha256

            var expiryMinutes = _configuration.GetValue<int>("JwtSettings:ExpiryMinutes"); //Tiempo de expiración del token en minutos (lo saca del appsettings.json)
            var expirationTime = DateTime.UtcNow.AddMinutes(expiryMinutes); //Calcula la hora de expiración sumando los minutos actuales a la hora actual en UTC

            var token = new JwtSecurityToken( //!!Con todos los datos, ahora construye el token JWT!!
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: expirationTime,
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token); //Devuelve el token JWT como una cadena
        }
    }
}
