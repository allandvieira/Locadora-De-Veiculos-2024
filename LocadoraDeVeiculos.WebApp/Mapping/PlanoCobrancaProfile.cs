using AutoMapper;
using LocadoraDeVeiculos.Dominio.ModuloPlanoCobranca;
using LocadoraDeVeiculos.WebApp.Mapping.Resolvers;
using LocadoraDeVeiculos.WebApp.Models;

namespace LocadoraDeVeiculos.WebApp.Mapping;

public class PlanoCobrancaProfile : Profile
{
    public PlanoCobrancaProfile()
    {
        CreateMap<InserirPlanoCobrancaViewModel, PlanoCobranca>();
        CreateMap<EditarPlanoCobrancaViewModel, PlanoCobranca>();

        CreateMap<PlanoCobranca, ListarPlanoCobrancaViewModel>()
            .ForMember(
                dest => dest.GrupoVeiculos,
                opt => opt.MapFrom(src => src.GrupoVeiculos!.Nome));

        CreateMap<PlanoCobranca, DetalhesPlanoCobrancaViewModel>()
            .ForMember(
                dest => dest.GrupoVeiculos,
                opt => opt.MapFrom(src => src.GrupoVeiculos!.Nome));

        CreateMap<PlanoCobranca, EditarPlanoCobrancaViewModel>()
            .ForMember(dest => dest.GruposVeiculos, opt => opt.MapFrom<GrupoVeiculosValueResolver>());
    }
}