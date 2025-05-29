using EstudiantesMateriasCrudApp.Domain.Interfaces;
using EstudiantesMateriasCrudApp.Domain.Models;
using EstudiantesMateriasCrudApp.Infrastructure.Data; 
using Microsoft.EntityFrameworkCore;

public class InscripcionService : IInscripcionService
{
    private readonly ApplicationDbContext _context;

    public InscripcionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> InscribirMateriaAsync(int estudianteId, int materiaId)
    {
       
        bool yaInscrito = await _context.Inscripciones
            .AnyAsync(i => i.EstudianteId == estudianteId && i.MateriaId == materiaId);
        if (yaInscrito)
            return false;

        
        var materia = await _context.Materias.FindAsync(materiaId);
        if (materia == null)
            return false; 

        // Se calcula el total de créditos que ya tiene inscrito el estudiante
        int totalCreditos = await _context.Inscripciones
            .Where(i => i.EstudianteId == estudianteId)
            .Include(i => i.Materia)
            .SumAsync(i => i.Materia.Creditos);

       
        int totalCreditosPropuesto = totalCreditos + materia.Creditos;

        // Validamos si excede el límite de 12 créditos
        if (totalCreditosPropuesto > 12)
        {
            return false; // Si excede, no permite la inscripción
        }

        // Si no excede, agrega la inscripción
        var inscripcion = new Inscripcion
        {
            EstudianteId = estudianteId,
            MateriaId = materiaId
        };

        _context.Inscripciones.Add(inscripcion);
        await _context.SaveChangesAsync();

        return true;
    }

}
