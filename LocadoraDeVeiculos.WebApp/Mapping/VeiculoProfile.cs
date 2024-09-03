using AutoMapper;
using LocadoraDeVeiculos.Dominio.ModuloVeiculo;
using LocadoraDeVeiculos.WebApp.Mapping.Resolvers;
using LocadoraDeVeiculos.WebApp.Models;

namespace LocadoraDeVeiculos.WebApp.Mapping;

public class VeiculoProfile : Profile
{
    public VeiculoProfile()
    {
        CreateMap<InserirVeiculoViewModel, Veiculo>()
            .ForMember(dest => dest.Foto, opt => opt.MapFrom<FotoValueResolver>());

        CreateMap<EditarVeiculoViewModel, Veiculo>()
            .ForMember(dest => dest.Foto, opt => opt.MapFrom<FotoValueResolver>());

        CreateMap<Veiculo, ListarVeiculoViewModel>()
            .ForMember(
                dest => dest.GrupoVeiculos,
                opt => opt.MapFrom(src => src.GrupoVeiculos!.Nome)
            );

        CreateMap<Veiculo, DetalhesVeiculoViewModel>()
            .ForMember(dest => dest.GrupoVeiculos, opt => opt.MapFrom(src => src.GrupoVeiculos!.Nome));

        CreateMap<Veiculo, EditarVeiculoViewModel>()
            .ForMember(v => v.Foto, opt => opt.Ignore())
            .ForMember(v => v.GruposVeiculos, opt => opt.MapFrom<GrupoVeiculosValueResolver>());
    }
}