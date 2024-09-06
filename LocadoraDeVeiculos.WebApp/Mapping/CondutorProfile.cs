using AutoMapper;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using LocadoraDeVeiculos.WebApp.Mapping.Resolvers;
using LocadoraDeVeiculos.WebApp.Models;

namespace LocadoraDeVeiculos.WebApp.Mapping;

public class CondutorProfile : Profile
{
    public CondutorProfile()
    {
        CreateMap<FormularioCondutorViewModel, Condutor>()
            .ForMember(dest => dest.EmpresaId, opt => opt.MapFrom<EmpresaIdValueResolver>());

        CreateMap<Condutor, ListarCondutorViewModel>()
            .ForMember(dest => dest.Cliente, opt => opt.MapFrom(c => c.Cliente!.Nome))
            .ForMember(dest => dest.ValidadeCNH, opt => opt.MapFrom(c => c.ValidadeCNH.ToShortDateString()));

        CreateMap<Condutor, DetalhesCondutorViewModel>()
            .ForMember(dest => dest.Cliente, opt => opt.MapFrom(c => c.Cliente!.Nome))
            .ForMember(dest => dest.ValidadeCNH, opt => opt.MapFrom(c => c.ValidadeCNH.ToShortDateString()));
    }
}