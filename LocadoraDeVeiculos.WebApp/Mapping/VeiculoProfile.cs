using AutoMapper;
using LocadoraDeVeiculos.Dominio.ModuloVeiculo;
using LocadoraDeVeiculos.WebApp.Models;

namespace LocadoraDeVeiculos.WebApp.Mapping;

public class VeiculoProfile : Profile
{
    public VeiculoProfile()
    {
        CreateMap<InserirVeiculoViewModel, Veiculo>()
            .ForMember(dest => dest.Foto,
                opt => opt.MapFrom<FotoValueResolver>());

        CreateMap<EditarVeiculoViewModel, Veiculo>()
            .ForMember(dest => dest.Foto,
                opt => opt.MapFrom<FotoValueResolver>());

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

public class FotoValueResolver : IValueResolver<FormularioVeiculoViewModel, Veiculo, byte[]>
{
    public FotoValueResolver() { }

    public byte[] Resolve(
        FormularioVeiculoViewModel source,
        Veiculo destination,
        byte[] destMember,
        ResolutionContext context
    )
    {
        using (var memoryStream = new MemoryStream())
        {
            source.Foto.CopyTo(memoryStream);

            return memoryStream.ToArray();
        }
    }
}