using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LocadoraDeVeiculos.WebApp.Models;

public class SelecionarClienteViewModel
{
    [Required(ErrorMessage = "O cliente é obrigatório")]
    public int ClienteId { get; set; }
    public bool ClienteCondutor { get; set; }

    public IEnumerable<SelectListItem>? Clientes { get; set; }
}

public class FormularioCondutorViewModel
{
    [Required(ErrorMessage = "O cliente é obrigatório")]
    public int ClienteId { get; set; }
    public bool ClienteCondutor { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3, ErrorMessage = "O nome deve conter ao menos 3 caracteres")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress(ErrorMessage = "O email deve ser válido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O telefone é obrigatório")]
    [Phone(ErrorMessage = "O telefone deve ser válido")]
    public string Telefone { get; set; }

    [Required(ErrorMessage = "O CPF é obrigatório")]
    [MinLength(11, ErrorMessage = "O CPF deve conter ao menos 11 caracteres")]
    public string CPF { get; set; }

    [Required(ErrorMessage = "O número da CNH é obrigatório")]
    public string CNH { get; set; }

    [Required(ErrorMessage = "A validade da CNH é obrigatória")]
    [DataType(DataType.Date, ErrorMessage = "A validade da CNH deve ser uma data válida")]
    public DateTime ValidadeCNH { get; set; }
}

public class InserirCondutorViewModel : FormularioCondutorViewModel
{
}

public class ListarCondutorViewModel
{
    public int Id { get; set; }

    public string Cliente { get; set; }
    public bool ClienteCondutor { get; set; }

    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }

    public string CPF { get; set; }
    public string CNH { get; set; }
    public string ValidadeCNH { get; set; }
}

public class DetalhesCondutorViewModel
{
    public int Id { get; set; }

    public string Cliente { get; set; }
    public bool ClienteCondutor { get; set; }

    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }

    public string CPF { get; set; }
    public string CNH { get; set; }
    public string ValidadeCNH { get; set; }
}