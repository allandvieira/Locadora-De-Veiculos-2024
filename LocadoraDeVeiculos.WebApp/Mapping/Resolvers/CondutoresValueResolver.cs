using AutoMapper;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using LocadoraDeVeiculos.Dominio.ModuloLocacao;
using LocadoraDeVeiculos.WebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LocadoraDeVeiculos.WebApp.Mapping.Resolvers;

public class CondutoresValueResolver : IValueResolver<Locacao, FormularioLocacaoViewModel, IEnumerable<SelectListItem>?>
{
    private readonly IRepositorioCondutor repositorioCondutor;

    public CondutoresValueResolver(IRepositorioCondutor repositorioCondutor)
    {
        this.repositorioCondutor = repositorioCondutor;
    }

    public IEnumerable<SelectListItem> Resolve(Locacao source, FormularioLocacaoViewModel destination, IEnumerable<SelectListItem>? destMember,
        ResolutionContext context)
    {
        if (destination is RealizarDevolucaoViewModel or
            ConfirmarAberturaLocacaoViewModel or
            ConfirmarDevolucaoLocacaoViewModel)
        {
            var condutorSelecionado = repositorioCondutor.SelecionarPorId(source.CondutorId);

            return [new SelectListItem(condutorSelecionado!.Nome, condutorSelecionado.Id.ToString())];
        }

        return repositorioCondutor
            .SelecionarTodos()
            .Select(c => new SelectListItem(c.Nome, c.Id.ToString()));
    }
}