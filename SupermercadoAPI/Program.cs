using SupermercadoAPI.Data;
using Microsoft.EntityFrameworkCore;
using SupermercadoAPI.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;


var builder = WebApplication.CreateBuilder(args);

var connectionString = "Server=localhost;Database=Supermercado;Uid=root;Pwd=;"; //URL de conexión a la base de datos MySQL

builder.Services.AddDbContext<AppDbContext>(options => //Servicio para la conexión a la base de datos MySQL
{
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure()
    );
});

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly); //Registrar los servicios de mapeo para convertir entre DTOs y entidades
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
