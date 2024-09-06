using AutoMapper;
using LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculos;
using LocadoraDeVeiculos.WebApp.Mapping.Resolvers;
using LocadoraDeVeiculos.WebApp.Models;

namespace LocadoraDeVeiculos.WebApp.Mapping;

public class GrupoVeiculosProfile : Profile
{
    public GrupoVeiculosProfile()
    {
        CreateMap<InserirGrupoVeiculosViewModel, GrupoVeiculos>()
            .ForMember(dest => dest.EmpresaId, opt => opt.MapFrom<EmpresaIdValueResolver>());

        CreateMap<EditarGrupoVeiculosViewModel, GrupoVeiculos>();

        CreateMap<GrupoVeiculos, ListarGrupoVeiculosViewModel>();
        CreateMap<GrupoVeiculos, DetalhesGrupoVeiculosViewModel>();
        CreateMap<GrupoVeiculos, EditarGrupoVeiculosViewModel>();
    }
}