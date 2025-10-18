using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupermercadoAPI.Data;
using SupermercadoAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace SupermercadoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context; //Contexto de la base de datos para acceder a la tabla de usuarios
        private readonly IConfiguration _configuration; //Para acceder a la configuración de la aplicación (appsettings.json)

        public AuthController(AppDbContext context, IConfiguration configuration) //Inyección de dependencias del contexto de la base de datos y la configuración
        {
            _context = context;
            _configuration = configuration;
        }


    }
}
