using AutoMapper;
using LocadoraDeVeiculos.Dominio.ModuloCombustivel;
using LocadoraDeVeiculos.WebApp.Models;

namespace LocadoraDeVeiculos.WebApp.Mapping;

public class ConfiguracaoCombustivelProfile : Profile
{
    public ConfiguracaoCombustivelProfile()
    {
        CreateMap<FormularioConfiguracaoCombustivelViewModel, ConfiguracaoCombustivel>();
        CreateMap<ConfiguracaoCombustivel, FormularioConfiguracaoCombustivelViewModel>();
    }
}