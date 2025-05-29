using System.Collections.Generic;

namespace EstudiantesMateriasCrudApp.Domain.Models
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public string Correo { get; set; }

        public ICollection<Inscripcion>? Inscripciones { get; set; }
    }
}
