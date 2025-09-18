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
        public string? SenhaHash { get; set; }

        [StringLength(700, ErrorMessage = "A biografia não pode exceder 700 caracteres.")]
        [DataType(DataType.MultilineText)]
        public string? Biografia { get; set; }

        public string? FotoUrl { get; set; }

        public string WhatsApp { get; set; }

        [Url(ErrorMessage = "A URL do WhatsApp precisa ser um endereço válido.")]
        [Display(Name = "Link de Agendamento do WhatsApp")]
        public string? WhatsAppUrl { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(14, ErrorMessage = "O CPF deve ter o formato 000.000.000-00", MinimumLength = 14)]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "O CPF deve ter o formato 000.000.000-00.")]
        [Display(Name = "CPF")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        public bool EmDestaque { get; set; } = false;

        public virtual ICollection<Especialidade> Especialidades { get; set; }

        public bool Ativo { get; set; } = false;

        public int GetIdade()
        {
            var today = DateTime.Today;
            var age = today.Year - DataNascimento.Year;
            if (DataNascimento.Date > today.AddYears(-age)) age--;
            return age;
        }

        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}