using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupermercadoAPI.Data; // Tu DbContext
using SupermercadoAPI.Dtos;
using SupermercadoAPI.Models;
using SupermercadoAPI.Services;
using AutoMapper;


namespace SupermercadoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /* Configuración e inyección de dependencias */
        private readonly AppDbContext _context; //Contexto de la base de datos para acceder a la tabla de usuarios
        private readonly IConfiguration _configuration; //Para acceder a la configuración de la aplicación (appsettings.json)
        private readonly ITokenService _tokenService; //Servicio para manejar la generación de tokens JWT
        private readonly IMapper _mapper; //Servicio para manejar el mapeo de objetos
        private readonly IpasswordHelper _passwordHelper; //Servicio para manejar el cifrado de contraseñas

        public AuthController(AppDbContext context, IConfiguration configuration, ITokenService tokenService, IMapper mapper, IpasswordHelper passwordHelper) //Inyección de dependencias del contexto de la base de datos y la configuración
        {
            _context = context;
            _configuration = configuration;
            _tokenService = tokenService;
            _mapper = mapper;
            _passwordHelper = passwordHelper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistroUsuarioDto dto) // Endpoint para registrar un nuevo usuario
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _context.Usuarios.AnyAsync(u => u.correo == dto.Correo)) 
            {
                return Conflict(new {message = "El correo ya esta registrado!"});
            }

            var usuario = _mapper.Map<Usuarios>(dto);

            usuario.contrasenaHash = _passwordHelper.HashPassword(dto.Contrasena);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return StatusCode(201, new {Message = "Usuario registrado exitosamente :)"});
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioDto dto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.correo == dto.correo);

            if (usuario == null || !_passwordHelper.VerifyPassword(usuario.contrasenaHash, dto.password))
            {
                return Unauthorized(new {message = "Credenciales inválidas"});
            }

            var token = _tokenService.GenerarToken(usuario);

            var responseDto = _mapper.Map<AuthResponseDto>(usuario);

            responseDto.Token = token;

            return Ok(responseDto);

        }

    }
        }

