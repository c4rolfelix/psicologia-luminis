using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; 

namespace Luminis.Models
{
    public class Psicologo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O sobrenome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O sobrenome não pode exceder 100 caracteres.")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "O CRP é obrigatório.")]
        [StringLength(10, ErrorMessage = "O CRP não pode exceder 10 caracteres.")]
        public string CRP { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string SenhaHash { get; set; }

        [StringLength(500, ErrorMessage = "A biografia não pode exceder 500 caracteres.")]
        [DataType(DataType.MultilineText)]
        public string? Biografia { get; set; } 

        public string? FotoUrl { get; set; }

        public string WhatsApp { get; set; }

        public virtual ICollection<Especialidade> Especialidades { get; set; } 

        public bool Ativo { get; set; } = false;

        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}