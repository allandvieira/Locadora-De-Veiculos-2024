using AutoMapper;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using LocadoraDeVeiculos.WebApp.Mapping.Resolvers;
using LocadoraDeVeiculos.WebApp.Models;

namespace LocadoraDeVeiculos.WebApp.Mapping;

public class FuncionarioProfile : Profile
{
    public FuncionarioProfile()
    {
        CreateMap<InserirFuncionarioViewModel, Funcionario>()
            .ForMember(dest => dest.EmpresaId, opt => opt.MapFrom<EmpresaIdValueResolver>());

        CreateMap<Funcionario, ListarFuncionarioViewModel>();
    }
}