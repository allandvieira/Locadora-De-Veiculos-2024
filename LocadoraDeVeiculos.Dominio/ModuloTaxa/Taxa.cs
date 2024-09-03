using LocadoraDeVeiculos.Dominio.Compartilhado;
using System.ComponentModel.DataAnnotations;

namespace LocadoraDeVeiculos.Dominio.ModuloTaxa;

public enum TipoCobrancaEnum
{
    [Display(Name = "Diária")]
    Diaria,
    Fixa
}

public class Taxa : EntidadeBase
{
    public string Nome { get; set; }
    public decimal Valor { get; set; }
    public TipoCobrancaEnum TipoCobranca { get; set; }

    protected Taxa() { }

    public Taxa(string nome, decimal valor, TipoCobrancaEnum tipoCobranca) : this()
    {
        Nome = nome;
        Valor = valor;
        TipoCobranca = tipoCobranca;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (Nome.Length < 3)
            erros.Add("O nome precisa conter ao menos 3 caracteres");

        if (Valor < 1.0m)
            erros.Add("O valor precisa ser ao menos 1");

        return erros;
    }
}