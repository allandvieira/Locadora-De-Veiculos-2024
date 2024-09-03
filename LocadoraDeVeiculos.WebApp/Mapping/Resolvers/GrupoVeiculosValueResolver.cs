using AutoMapper;
using LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LocadoraDeVeiculos.WebApp.Mapping.Resolvers;

public class GrupoVeiculosValueResolver :
    IValueResolver<object, object, IEnumerable<SelectListItem>?>
{
    private readonly IRepositorioGrupoVeiculos repositorioGrupo;

    public GrupoVeiculosValueResolver(IRepositorioGrupoVeiculos repositorioGrupo)
    {
        this.repositorioGrupo = repositorioGrupo;
    }

    public IEnumerable<SelectListItem> Resolve(
        object source,
        object destination,
        IEnumerable<SelectListItem>? destMember,
        ResolutionContext context
    )
    {
        return repositorioGrupo
            .SelecionarTodos()
            .Select(g => new SelectListItem(g.Nome, g.Id.ToString()));
    }
}