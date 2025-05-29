using EstudiantesMateriasCrudApp.Domain.Interfaces;
using EstudiantesMateriasCrudApp.Domain.Models;
using EstudiantesMateriasCrudApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class MateriaRepos : IMateria
{
    private readonly ApplicationDbContext _context;

    public MateriaRepos(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Materia>> GetAllAsync()
    {
        return await _context.Materias.ToListAsync();
    }

    public async Task<Materia> GetByIdAsync(int id)
    {
        return await _context.Materias.FindAsync(id);
    }

    public async Task AddAsync(Materia materia)
    {
        await _context.Materias.AddAsync(materia);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Materia materia)
    {
        _context.Materias.Update(materia);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var materia = await GetByIdAsync(id);
        if (materia != null)
        {
            _context.Materias.Remove(materia);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> CodigoExisteAsync(string codigo, int? idExcluir = null)
    {
        return await _context.Materias.AnyAsync(m => m.Codigo == codigo && (!idExcluir.HasValue || m.Id != idExcluir));
    }
}
