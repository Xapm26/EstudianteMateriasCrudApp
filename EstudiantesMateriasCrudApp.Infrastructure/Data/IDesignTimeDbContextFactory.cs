using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using EstudiantesMateriasCrudApp.Infrastructure.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // Validación de la conexión BD
        optionsBuilder.UseSqlServer("Server=XIME\\SQLEXPRESS;Database=EstudianteMateriasDB;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;",
            b => b.MigrationsAssembly("EstudiantesMateriasCrudApp.Infrastructure"));

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
