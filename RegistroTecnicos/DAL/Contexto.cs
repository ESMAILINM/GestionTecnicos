using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.DAL
{
    public class Contexto: DbContext
    {
        public Contexto(DbContextOptions<Contexto> options): base (options) { }

        public DbSet<Tecnicos> Tecnicos { get; set; }
        public DbSet<Clientes> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar la propiedad SueldoHora de la entidad Tecnicos
            // para que los valores se almacenen correctamente y evitar truncamiento en la DB 
            modelBuilder.Entity<Tecnicos>()
                .Property(t => t.SueldoHora)
                .HasColumnType("decimal(18,2)"); 
        }
    }
}
