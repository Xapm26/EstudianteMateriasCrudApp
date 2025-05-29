namespace EstudiantesMateriasCrudApp.Domain.Interfaces
{
    public interface IInscripcion
    {
        Task InscribirMateriaAsync(int estudianteId, int materiaId);
    }
}
