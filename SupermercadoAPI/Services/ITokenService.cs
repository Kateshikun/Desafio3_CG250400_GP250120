using SupermercadoAPI.Models;

namespace SupermercadoAPI.Services
{
    public interface ITokenService
    {
        string GenerarToken(Usuarios usuario);
    }
}
