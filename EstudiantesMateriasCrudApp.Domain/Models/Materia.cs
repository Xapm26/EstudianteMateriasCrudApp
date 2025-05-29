using EstudiantesMateriasCrudApp.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace EstudiantesMateriasCrudApp.Domain.Models
{
    public class Materia
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El código es obligatorio")]
        [StringLength(20, ErrorMessage = "El código no puede tener más de 20 caracteres")]
        public string Codigo { get; set; }

        [Range(1, 10, ErrorMessage = "Los créditos deben estar entre 1 y 10")]
        public int Creditos { get; set; }

        public ICollection<Inscripcion>? Inscripciones { get; set; }
    }

}
