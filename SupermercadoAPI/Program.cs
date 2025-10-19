using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SupermercadoAPI.Data;
using SupermercadoAPI.Mappings;
using SupermercadoAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// --- JWT: LECTURA SEGURA DE LA CLAVE SECRETA ---
// Lectura de la clave antes del bloque AddJwtBearer para evitar errores de null
var jwtSecretKey = builder.Configuration["JwtSettings:Secret"];

/* SERVICIO DE BASE DE DATOS */
var connectionString = "Server=localhost;Database=Supermercado;Uid=root;Pwd=;"; //URL de conexión a la base de datos MySQL

builder.Services.AddDbContext<AppDbContext>(options => //Servicio para la conexión a la base de datos MySQL
{
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure()
    );
});

/* AUTO MAPPER */
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly); //Registrar los servicios de mapeo para convertir entre DTOs y entidades

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => //Esto es para prubeas con el token
{
c.SwaggerDoc("v1", new OpenApiInfo { Title = "SupermercadoAPI", Version = "v1" });

// 1. Definición de la autenticación Bearer
c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "Ingresa 'Bearer ' [espacio] y luego tu token JWT. Ejemplo: 'Bearer 12345abcdef'",
});

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

/* SERVICIOS DE SEGURIDAD */
// Inyección de dependencia para el servicio de manejo de contraseñas, no tiene estado por eso es singleton
builder.Services.AddSingleton<IpasswordHelper, PasswordHelper>();
// Inyección de dependencia para el servicio de generación de tokens
builder.Services.AddScoped<ITokenService, TokenService>();

/* AUTENTICACIÓN Y AUTORIZACIÓN CON JWT */
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; //Validar tokens JWT para autenticar solicitudes entrantes (accion predeterminada)
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; //Lo que pasaria si la autenticación falla: 401 error (accion predeterminada)
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; //Esquema predeterminado para todas las operaciones de autenticación (accion predeterminada)
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters() //Parámetros para validar el token JWT que ya estan definidos en el appsettings.json
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        // Clave secreta para firmar el token que se codifica en UTF8 por seguridad
        // Se usa la variable segura 'jwtSecretKey'
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)),
        ValidateLifetime = true // Agregado para validar la expiración del token
    };
});

builder.Services.AddAuthorization(); //Habilitador final para proteger endpoints con autorizacion

var app = builder.Build();

/* MIDDLEWARES DEL PIPELINE*/
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();