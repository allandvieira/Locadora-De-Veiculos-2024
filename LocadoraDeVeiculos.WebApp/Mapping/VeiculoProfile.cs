using AutoMapper;
using LocadoraDeVeiculos.Dominio.ModuloVeiculo;
using LocadoraDeVeiculos.WebApp.Models;

namespace LocadoraDeVeiculos.WebApp.Mapping;

public class VeiculoProfile : Profile
{
    public VeiculoProfile()
    {
        CreateMap<InserirVeiculoViewModel, Veiculo>();
        CreateMap<EditarVeiculoViewModel, Veiculo>();

        CreateMap<Veiculo, ListarVeiculoViewModel>()
            .ForMember(
                dest => dest.GrupoVeiculos,
                opt => opt.MapFrom(src => src.GrupoVeiculos!.Nome)
            );

        CreateMap<Veiculo, DetalhesVeiculoViewModel>()
            .ForMember(
                dest => dest.GrupoVeiculos,
                opt => opt.MapFrom(src => src.GrupoVeiculos!.Nome)
            );

        CreateMap<Veiculo, EditarVeiculoViewModel>();
    }
}