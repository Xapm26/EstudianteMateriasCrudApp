using EstudiantesMateriasCrudApp.Domain.Interfaces;
using EstudiantesMateriasCrudApp.Domain.Models;

public class MateriaService : IMateriaService
{
    private readonly IMateria _materiaRepository;

    public MateriaService(IMateria materiaRepository)
    {
        _materiaRepository = materiaRepository;
    }

    public async Task<List<Materia>> ObtenerTodasAsync()
    {
        var materias = await _materiaRepository.GetAllAsync();
        return materias.ToList();  
    }

    public async Task<Materia> ObtenerPorIdAsync(int id)
    {
        return await _materiaRepository.GetByIdAsync(id);
    }

    public async Task<bool> CrearMateriaAsync(Materia materia)
    {
        if (await _materiaRepository.CodigoExisteAsync(materia.Codigo))
            return false;

        await _materiaRepository.AddAsync(materia);
        return true;
    }

    public async Task<bool> ActualizarMateriaAsync(Materia materia)
    {
        if (await _materiaRepository.CodigoExisteAsync(materia.Codigo, materia.Id))
            return false;

        await _materiaRepository.UpdateAsync(materia);
        return true;
    }

    public async Task EliminarMateriaAsync(int id)
    {
        await _materiaRepository.DeleteAsync(id);
    }
}
