using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Luminis.Models
{
    public class Especialidade
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da especialidade é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome da especialidade não pode exceder 50 caracteres.")]
        public string Nome { get; set; }

        public virtual ICollection<Psicologo> Psicologos { get; set; } 
    }
}