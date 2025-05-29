using EstudiantesMateriasCrudApp.Domain.Models;

namespace EstudiantesMateriasCrudApp.Domain.Interfaces
{
    public interface IMateria
    {
        Task<IEnumerable<Materia>> GetAllAsync();
        Task<Materia?> GetByIdAsync(int id);
        Task AddAsync(Materia materia);
        Task UpdateAsync(Materia materia);
        Task DeleteAsync(int id);

        // Validamos si existe una materia con ese código, excluyendo opcionalmente un id
        Task<bool> CodigoExisteAsync(string codigo, int? idExcluir = null);
    }
}
