using SupermercadoAPI.Data;
using Microsoft.EntityFrameworkCore;
using SupermercadoAPI.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SupermercadoAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// --- JWT: LECTURA SEGURA DE LA CLAVE SECRETA ---
// Lectura de la clave antes del bloque AddJwtBearer para evitar errores de null
var jwtSecretKey = builder.Configuration["JwtSettings:Secret"];

/* SERVICIO DE BASE DE DATOS */
var connectionString = "Server=localhost;Database=Supermercado;Uid=root;Pwd=;"; //URL de conexi�n a la base de datos MySQL

builder.Services.AddDbContext<AppDbContext>(options => //Servicio para la conexi�n a la base de datos MySQL
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
builder.Services.AddSwaggerGen();

/* SERVICIOS DE SEGURIDAD */
// Inyecci�n de dependencia para el servicio de manejo de contrase�as, no tiene estado por eso es singleton
builder.Services.AddSingleton<IpasswordHelper, PasswordHelper>();
// Inyecci�n de dependencia para el servicio de generaci�n de tokens
builder.Services.AddScoped<ITokenService, TokenService>();

/* AUTENTICACI�N Y AUTORIZACI�N CON JWT */
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; //Validar tokens JWT para autenticar solicitudes entrantes (accion predeterminada)
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; //Lo que pasaria si la autenticaci�n falla: 401 error (accion predeterminada)
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; //Esquema predeterminado para todas las operaciones de autenticaci�n (accion predeterminada)
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters() //Par�metros para validar el token JWT que ya estan definidos en el appsettings.json
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        // Clave secreta para firmar el token que se codifica en UTF8 por seguridad
        // Se usa la variable segura 'jwtSecretKey'
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)),
        ValidateLifetime = true // Agregado para validar la expiraci�n del token
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