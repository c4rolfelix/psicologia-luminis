using System.ComponentModel.DataAnnotations;

public class EsqueceuSenhaViewModel
{
    [Required(ErrorMessage = "Informe seu CPF")]
    [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "CPF inválido")]
    public string CPF { get; set; }
}
