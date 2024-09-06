using AutoMapper;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.WebApp.Mapping.Resolvers;
using LocadoraDeVeiculos.WebApp.Models;

namespace LocadoraDeVeiculos.WebApp.Mapping;

public class ClienteProfile : Profile
{
    public ClienteProfile()
    {
        CreateMap<InserirClienteViewModel, Cliente>()
            .ForMember(dest => dest.EmpresaId, opt => opt.MapFrom<EmpresaIdValueResolver>());

        CreateMap<EditarClienteViewModel, Cliente>();

        CreateMap<Cliente, ListarClienteViewModel>()
            .ForMember(
                dest => dest.TipoCadastro,
                opt => opt.MapFrom(x => x.TipoCadastro.ToString())
            );

        CreateMap<Cliente, DetalhesClienteViewModel>()
            .ForMember(
                dest => dest.TipoCadastro,
                opt => opt.MapFrom(x => x.TipoCadastro.ToString())
            );

        CreateMap<Cliente, EditarClienteViewModel>();
    }
}