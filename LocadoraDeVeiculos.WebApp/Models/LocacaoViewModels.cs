using LocadoraDeVeiculos.Dominio.ModuloLocacao;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LocadoraDeVeiculos.WebApp.Models;

public class FormularioLocacaoViewModel
{
    [Required(ErrorMessage = "O veículo é obrigatório")]
    public int VeiculoId { get; set; }

    [Required(ErrorMessage = "O condutor é obrigatório")]
    public int CondutorId { get; set; }

    [Required(ErrorMessage = "O tipo de plano é obrigatório")]
    public TipoPlanoCobrancaEnum TipoPlano { get; set; }

    [Required(ErrorMessage = "O marcador de combustível é obrigatório")]
    public MarcadorCombustivelEnum MarcadorCombustivel { get; set; }

    [Required(ErrorMessage = "A quilometragem percorrida é obrigatória")]
    [Range(0, int.MaxValue, ErrorMessage = "A quilometragem percorrida deve ser maior ou igual a 0")]
    public int QuilometragemPercorrida { get; set; }

    [Required(ErrorMessage = "A data da locação é obrigatória")]
    [DataType(DataType.Date)]
    public DateTime DataLocacao { get; set; }

    [Required(ErrorMessage = "A data prevista de devolução é obrigatória")]
    [DataType(DataType.Date)]
    public DateTime DevolucaoPrevista { get; set; }

    public IEnumerable<int> TaxasSelecionadas { get; set; }

    public IEnumerable<SelectListItem>? Veiculos { get; set; }
    public IEnumerable<SelectListItem>? Condutores { get; set; }
    public IEnumerable<SelectListItem>? Taxas { get; set; }

    public FormularioLocacaoViewModel()
    {
        DataLocacao = DateTime.Now;
        DevolucaoPrevista = DateTime.Now.AddDays(1);
        MarcadorCombustivel = MarcadorCombustivelEnum.Completo;
    }
}

public class InserirLocacaoViewModel : FormularioLocacaoViewModel
{
}

public class EditarLocacaoViewModel : FormularioLocacaoViewModel
{
    public int Id { get; set; }
}

public class RealizarDevolucaoViewModel : FormularioLocacaoViewModel
{
    public int Id { get; set; }
}

public class ConfirmarAberturaLocacaoViewModel : FormularioLocacaoViewModel
{
    public decimal ValorParcial { get; set; }
}

public class ConfirmarDevolucaoLocacaoViewModel : FormularioLocacaoViewModel
{
    public int Id { get; set; }
    public decimal ValorTotal { get; set; }
}

public class ListarLocacaoViewModel
{
    public int Id { get; set; }
    public string Veiculo { get; set; }
    public string Condutor { get; set; }
    public string TipoPlano { get; set; }
    public int QuilometragemPercorrida { get; set; }
    public DateTime DataLocacao { get; set; }
    public DateTime DevolucaoPrevista { get; set; }
    public DateTime? DataDevolucao { get; set; }
}

public class DetalhesLocacaoViewModel
{
    public int Id { get; set; }
    public string Veiculo { get; set; }
    public string Condutor { get; set; }
    public string TipoPlano { get; set; }
    public string MarcadorCombustivel { get; set; }
    public int QuilometragemPercorrida { get; set; }
    public DateTime DataLocacao { get; set; }
    public DateTime DevolucaoPrevista { get; set; }
    public DateTime? DataDevolucao { get; set; }
}