using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SupermercadoAPI.Data;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace SupermercadoAPI.Data;
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext> //Implementación de la interfaz para crear el contexto de la base de datos en tiempo de diseño
                                                                                    //Esto es útil para ejecutar migraciones y otras tareas relacionadas con la base de datos durante el desarrollo
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        const string connectionString = "Server=localhost;Database=supermercado;Uid=root;Pwd=;";

        optionsBuilder.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString),
            mySqlOptions => mySqlOptions.EnableRetryOnFailure()
        );

        return new AppDbContext(optionsBuilder.Options);
    }
}