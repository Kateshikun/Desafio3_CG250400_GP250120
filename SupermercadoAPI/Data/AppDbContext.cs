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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define la relación uno-a-uno entre Inventarios y Productos
            modelBuilder.Entity<Inventarios>()
                // 1. Un Inventario tiene un Producto (HasOne)
                .HasOne(i => i.producto)
                // 2. Ese Producto tiene un Inventario (WithOne)
                .WithOne(p => p.Inventario)
                // 3. La clave foránea se encuentra en la tabla Inventarios (HasForeignKey)
                //    Y se mapea a la propiedad 'productoId' (asumiendo que ese es el FK en Inventarios)
                .HasForeignKey<Inventarios>(i => i.idProducto)
                // 4. Se establece la eliminación en cascada si se borra el producto
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

    }
}
