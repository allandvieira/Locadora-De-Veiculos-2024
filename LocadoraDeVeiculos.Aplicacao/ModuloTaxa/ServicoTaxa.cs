using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;

namespace LocadoraDeVeiculos.Aplicacao.ModuloTaxa;

public class ServicoTaxa
{
    private readonly IRepositorioTaxa repositorioTaxa;

    public ServicoTaxa(IRepositorioTaxa repositorioTaxa)
    {
        this.repositorioTaxa = repositorioTaxa;
    }

    public Result<Taxa> Inserir(Taxa taxa)
    {
        var errosValidacao = taxa.Validar();

        if (errosValidacao.Count > 0)
            return Result.Fail(errosValidacao);

        repositorioTaxa.Inserir(taxa);

        return Result.Ok(taxa);
    }

    public Result<Taxa> Editar(Taxa taxaAtualizada)
    {
        var taxa = repositorioTaxa.SelecionarPorId(taxaAtualizada.Id);

        if (taxa is null)
            return Result.Fail("A taxa não foi encontrada!");

        var errosValidacao = taxaAtualizada.Validar();

        if (errosValidacao.Count > 0)
            return Result.Fail(errosValidacao);

        taxa.Nome = taxaAtualizada.Nome;
        taxa.Valor = taxaAtualizada.Valor;
        taxa.TipoCobranca = taxaAtualizada.TipoCobranca;

        repositorioTaxa.Editar(taxa);

        return Result.Ok(taxa);
    }

    public Result<Taxa> Excluir(int taxaId)
    {
        var taxa = repositorioTaxa.SelecionarPorId(taxaId);

        if (taxa is null)
            return Result.Fail("A taxa não foi encontrada!");

        repositorioTaxa.Excluir(taxa);

        return Result.Ok(taxa);
    }

    public Result<Taxa> SelecionarPorId(int taxaId)
    {
        var taxa = repositorioTaxa.SelecionarPorId(taxaId);

        if (taxa is null)
            return Result.Fail("A taxa não foi encontrada!");

        return Result.Ok(taxa);
    }

    public Result<List<Taxa>> SelecionarTodos()
    {
        var taxas = repositorioTaxa.SelecionarTodos();

        return Result.Ok(taxas);
    }
}