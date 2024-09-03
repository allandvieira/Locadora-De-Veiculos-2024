using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LocadoraDeVeiculos.WebApp.Models;

public class FormularioTaxaViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3, ErrorMessage = "O nome deve conter ao menos 3 caracteres")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O valor é obrigatório")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que 0")]
    public decimal Valor { get; set; }

    [Required(ErrorMessage = "O tipo de cobrança é obrigatório")]
    public TipoCobrancaEnum TipoCobranca { get; set; }

    public IEnumerable<SelectListItem>? TiposCobranca { get; set; }
}

public class InserirTaxaViewModel : FormularioTaxaViewModel
{
}

public class EditarTaxaViewModel : FormularioTaxaViewModel
{
    public int Id { get; set; }
}

public class ListarTaxaViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Valor { get; set; }
    public string TipoCobranca { get; set; }
}

public class DetalhesTaxaViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Valor { get; set; }
    public string TipoCobranca { get; set; }
}