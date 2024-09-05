using AutoMapper;
using LocadoraDeVeiculos.Aplicacao.ModuloVeiculo;
using LocadoraDeVeiculos.Dominio.ModuloLocacao;
using LocadoraDeVeiculos.WebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LocadoraDeVeiculos.WebApp.Mapping.Resolvers;

public class VeiculosValueResolver : IValueResolver<Locacao, FormularioLocacaoViewModel, IEnumerable<SelectListItem>?>
{
    private readonly ServicoVeiculo _servicoVeiculo;

    public VeiculosValueResolver(ServicoVeiculo servicoVeiculo)
    {
        _servicoVeiculo = servicoVeiculo;
    }

    public IEnumerable<SelectListItem>? Resolve(Locacao source, FormularioLocacaoViewModel destination, IEnumerable<SelectListItem>? destMember,
        ResolutionContext context)
    {
        if (destination is RealizarDevolucaoViewModel or ConfirmarAberturaLocacaoViewModel or ConfirmarDevolucaoLocacaoViewModel)
        {
            var veiculoSelecionado = _servicoVeiculo.SelecionarPorId(source.VeiculoId).Value;

            return [new SelectListItem(veiculoSelecionado!.Modelo, veiculoSelecionado.Id.ToString())];
        }

        return _servicoVeiculo
            .SelecionarTodos()
            .Value
            .Select(v => new SelectListItem(v.Modelo, v.Id.ToString()));
    }
}