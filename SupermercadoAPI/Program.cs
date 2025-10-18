using SupermercadoAPI.Data;
using Microsoft.EntityFrameworkCore;
using SupermercadoAPI.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SupermercadoAPI.Services;


var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddSwaggerGen();

/* AUTENTICACIÓN Y AUTORIZACIÓN CON JWT */
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; //Validar tokens JWT para autenticar solicitudes entrantes (accion predeterminada)
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; //Lo que pasaria si la autenticación falla: 401 error (accion predeterminada)
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; //Esquema predeterminado para todas las operaciones de autenticación (accion predeterminada)
}).
AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters() //Parámetros para validar el token JWT que ya estan definidos en el appsettings.json
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])) //Clave secreta para firmar el token que se codifica en UTF8 por seguridad
    };
});

builder.Services.AddSingleton<IpasswordHelper, PasswordHelper>(); //Inyección de dependencia para el servicio de manejo de contraseñas, no tiene estado por eso es singleton
builder.Services.AddScoped<ITokenService, TokenService>();

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
