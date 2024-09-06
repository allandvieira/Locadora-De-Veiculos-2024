using System.ComponentModel.DataAnnotations;

namespace LocadoraDeVeiculos.WebApp.Models;

public class InserirFuncionarioViewModel
{
    [Required(ErrorMessage = "O nome do funcionário é obrigatório")]
    public string NomeCompleto { get; set; }

    [Required(ErrorMessage = "O nome de usuário é obrigatório")]
    public string NomeUsuario { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required, DataType(DataType.Password)]
    public string Senha { get; set; }

    [Display(Name = "Confirme a senha")]
    [DataType(DataType.Password), Compare("Senha", ErrorMessage = "As senhas não conferem")]
    public string ConfirmarSenha { get; set; }

    [DataType(DataType.Date)]
    public DateTime Admissao { get; set; }

    [DataType(DataType.Currency)]
    public decimal Salario { get; set; }

    public InserirFuncionarioViewModel()
    {
        Admissao = DateTime.Now;
    }
}

public class ListarFuncionarioViewModel
{
    public string Id { get; set; }
    public string NomeCompleto { get; set; }
    public string Email { get; set; }
    public DateTime Admissao { get; set; }
    public decimal Salario { get; set; }
}