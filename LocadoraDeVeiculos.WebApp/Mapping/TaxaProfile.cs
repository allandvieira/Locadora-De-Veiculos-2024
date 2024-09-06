using AutoMapper;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using LocadoraDeVeiculos.WebApp.Mapping.Resolvers;
using LocadoraDeVeiculos.WebApp.Models;

namespace LocadoraDeVeiculos.WebApp.Mapping;

public class TaxaProfile : Profile
{
    public TaxaProfile()
    {
        CreateMap<InserirTaxaViewModel, Taxa>()
            .ForMember(dest => dest.EmpresaId, opt => opt.MapFrom<EmpresaIdValueResolver>());

        CreateMap<EditarTaxaViewModel, Taxa>();

        CreateMap<Taxa, ListarTaxaViewModel>()
            .ForMember(
                dest => dest.TipoCobranca,
                opt => opt.MapFrom(x => x.TipoCobranca.ToString())
            );

        CreateMap<Taxa, DetalhesTaxaViewModel>()
            .ForMember(
                dest => dest.TipoCobranca,
                opt => opt.MapFrom(x => x.TipoCobranca.ToString())
            );

        CreateMap<Taxa, EditarTaxaViewModel>();
    }
}