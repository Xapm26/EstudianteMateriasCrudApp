using EstudiantesMateriasCrudApp.Domain.Interfaces;
using EstudiantesMateriasCrudApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EstudiantesMateriasCrudApp.Infrastructure.Data
{
    public class EstudianteRepos: IEstudiante
    {
        private readonly ApplicationDbContext _context;

        public EstudianteRepos(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Estudiante>> GetAllAsync()
        {
            return await _context.Estudiantes.ToListAsync();
        }

        public async Task<Estudiante> GetByIdAsync(int id)
        {
            return await _context.Estudiantes.FindAsync(id);
        }

        public async Task AddAsync(Estudiante estudiante)
        {
            Console.WriteLine("Se intenta agregar un estudiante...");
            _context.Estudiantes.Add(estudiante);
            var result = await _context.SaveChangesAsync();
            Console.WriteLine($"Resultado SaveChangesAsync: {result}");
        
        }

        public async Task UpdateAsync(Estudiante estudiante)
        {
            _context.Estudiantes.Update(estudiante);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante != null)
            {
                _context.Estudiantes.Remove(estudiante);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DocumentoExisteAsync(string documento, int? idExcluir = null)
        {
            return await _context.Estudiantes
                .AnyAsync(e => e.Documento == documento && (!idExcluir.HasValue || e.Id != idExcluir.Value));
        }
    }
}
