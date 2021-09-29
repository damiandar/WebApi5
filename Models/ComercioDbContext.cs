using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace ProyRepositorio.Models
{
    public class ComercioDbContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public ComercioDbContext(){

        }
        public ComercioDbContext(DbContextOptions<ComercioDbContext> options) :base(options) {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder){

        }
    }
}