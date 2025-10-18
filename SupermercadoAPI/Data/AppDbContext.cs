using Microsoft.EntityFrameworkCore;
using SupermercadoAPI.Models;

namespace SupermercadoAPI.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext()
        {
        }
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options) { }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Inventarios> Inventarios { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }

        }
}
