using System.ComponentModel.DataAnnotations;

namespace LocadoraDeVeiculos.WebApp.Models;

public class InserirGrupoVeiculosViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3, ErrorMessage = "O nome deve conter ao menos 3 caracteres")]
    public string Nome { get; set; }
}

public class EditarGrupoVeiculosViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3, ErrorMessage = "O nome deve conter ao menos 3 caracteres")]
    public string Nome { get; set; }
}

public class ListarGrupoVeiculosViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
}

public class DetalhesGrupoVeiculosViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
}