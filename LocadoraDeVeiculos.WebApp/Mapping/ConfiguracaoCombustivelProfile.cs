using AutoMapper;
using LocadoraDeVeiculos.Dominio.ModuloCombustivel;
using LocadoraDeVeiculos.WebApp.Mapping.Resolvers;
using LocadoraDeVeiculos.WebApp.Models;

namespace LocadoraDeVeiculos.WebApp.Mapping;

public class ConfiguracaoCombustivelProfile : Profile
{
    public ConfiguracaoCombustivelProfile()
    {
        CreateMap<FormularioConfiguracaoCombustivelViewModel, ConfiguracaoCombustivel>()
            .ForMember(dest => dest.EmpresaId, opt => opt.MapFrom<EmpresaIdValueResolver>());

        CreateMap<ConfiguracaoCombustivel, FormularioConfiguracaoCombustivelViewModel>();
    }
}