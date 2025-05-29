using EstudiantesMateriasCrudApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EstudiantesMateriasCrudApp.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración del Estudiante
            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Documento).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Correo).IsRequired().HasMaxLength(100);
            });

            // Configuración de la Materia
            modelBuilder.Entity<Materia>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(m => m.Codigo).IsRequired().HasMaxLength(10);
                entity.Property(m => m.Creditos).IsRequired();
            });

            // Configuración de la Inscripcion
            modelBuilder.Entity<Inscripcion>(entity =>
            {
                entity.HasKey(i => i.Id);
                entity.HasOne(i => i.Estudiante)
                      .WithMany(e => e.Inscripciones)
                      .HasForeignKey(i => i.EstudianteId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(i => i.Materia)
                      .WithMany(m => m.Inscripciones)
                      .HasForeignKey(i => i.MateriaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
