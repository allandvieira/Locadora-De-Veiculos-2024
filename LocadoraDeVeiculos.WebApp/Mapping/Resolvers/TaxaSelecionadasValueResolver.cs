using AutoMapper;
using LocadoraDeVeiculos.Dominio.ModuloLocacao;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using LocadoraDeVeiculos.WebApp.Models;

namespace LocadoraDeVeiculos.WebApp.Mapping.Resolvers;

public class TaxasSelecionadasValueResolver : IValueResolver<FormularioLocacaoViewModel, Locacao, List<Taxa>>
{
    private readonly IRepositorioTaxa repositorioTaxa;

    public TaxasSelecionadasValueResolver(IRepositorioTaxa repositorioTaxa)
    {
        this.repositorioTaxa = repositorioTaxa;
    }

    public List<Taxa> Resolve(
        FormularioLocacaoViewModel source,
        Locacao destination,
        List<Taxa> destMember,
        ResolutionContext context
    )
    {
        var idsTaxasSelecionadas = source.TaxasSelecionadas.ToList();

        return repositorioTaxa.SelecionarMuitos(idsTaxasSelecionadas);
    }
}