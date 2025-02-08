using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Models;

namespace RegistroTecnicos.DAL
{
    public class Contexto: DbContext
    {
        public Contexto(DbContextOptions<Contexto> options): base (options) { }

        public DbSet<Tecnicos> Tecnicos { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<Ciudades> Ciudades { get; set; }
        public DbSet<Sistemas> Sistemas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar la propiedad SueldoHora de la entidad Tecnicos
            // para que los valores se almacenen correctamente y evitar truncamiento en la DB 
            modelBuilder.Entity<Tecnicos>()
                .Property(t => t.SueldoHora)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Tickets>()
                .HasOne(t => t.clientes)
                .WithMany()
                .HasForeignKey(t => t.ClienteId)
                .OnDelete(DeleteBehavior.NoAction); //Confurar que deje en null si se elimina el cliente

            modelBuilder.Entity<Tickets>()
                .HasOne(t => t.tecnicos)
                .WithMany()
                .HasForeignKey(t => t.TecnicoId)
                .OnDelete(DeleteBehavior.NoAction); //Confurar que deje en null si se elimina el Tecnico 
        }

    }
}
