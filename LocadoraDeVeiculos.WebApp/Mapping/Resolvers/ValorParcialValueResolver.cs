using AutoMapper;
using LocadoraDeVeiculos.Aplicacao.ModuloPlanoCobranca;
using LocadoraDeVeiculos.Aplicacao.ModuloVeiculo;
using LocadoraDeVeiculos.Dominio.ModuloLocacao;
using LocadoraDeVeiculos.WebApp.Models;

namespace LocadoraDeVeiculos.WebApp.Mapping.Resolvers;

public class ValorParcialValueResolver : IValueResolver<Locacao, ConfirmarAberturaLocacaoViewModel, decimal>
{
    private readonly ServicoVeiculo servicoVeiculo;
    private readonly ServicoPlanoCobranca servicoPlano;

    public ValorParcialValueResolver(ServicoVeiculo servicoVeiculo, ServicoPlanoCobranca servicoPlano)
    {
        this.servicoVeiculo = servicoVeiculo;
        this.servicoPlano = servicoPlano;
    }

    public decimal Resolve(
        Locacao source,
        ConfirmarAberturaLocacaoViewModel destination,
        decimal destMember,
        ResolutionContext context
    )
    {
        var veiculo = servicoVeiculo.SelecionarPorId(source.VeiculoId).Value;

        var planoSelecionado = servicoPlano.SelecionarPorIdGrupoVeiculos(veiculo.GrupoVeiculosId).Value;

        return source.CalcularValorParcial(planoSelecionado);
    }
}