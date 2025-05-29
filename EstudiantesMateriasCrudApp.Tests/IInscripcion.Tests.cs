namespace EstudiantesMateriasCrudApp.Domain.Interfaces
{
    public interface IInscripcionTests
    {
        Task InscribirMateriaAsync(int estudianteId, int materiaId);
    }
}
