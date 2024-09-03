using AutoMapper;
using LocadoraDeVeiculos.Aplicacao.ModuloTaxa;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using LocadoraDeVeiculos.WebApp.Controllers.Compartilhado;
using LocadoraDeVeiculos.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraDeVeiculos.WebApp.Controllers;

public class TaxaController : WebControllerBase
{
    private readonly ServicoTaxa servico;
    private readonly IMapper mapeador;

    public TaxaController(ServicoTaxa servico, IMapper mapeador)
    {
        this.servico = servico;
        this.mapeador = mapeador;
    }

    public IActionResult Listar()
    {
        var resultado = servico.SelecionarTodos();

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction("Index", "Home");
        }

        var taxas = resultado.Value;

        var listarTaxasVm = mapeador.Map<IEnumerable<ListarTaxaViewModel>>(taxas);

        return View(listarTaxasVm);
    }

    public IActionResult Inserir()
    {
        return View(new InserirTaxaViewModel());
    }

    [HttpPost]
    public IActionResult Inserir(InserirTaxaViewModel inserirVm)
    {
        if (!ModelState.IsValid)
            return View(inserirVm);

        var taxa = mapeador.Map<Taxa>(inserirVm);

        var resultado = servico.Inserir(taxa);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction(nameof(Listar));
        }

        ApresentarMensagemSucesso($"O registro ID [{taxa.Id}] foi inserido com sucesso!");

        return RedirectToAction(nameof(Listar));
    }

    public IActionResult Editar(int id)
    {
        var resultado = servico.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction(nameof(Listar));
        }

        var taxa = resultado.Value;

        var editarVm = mapeador.Map<EditarTaxaViewModel>(taxa);

        return View(editarVm);
    }

    [HttpPost]
    public IActionResult Editar(EditarTaxaViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        var taxa = mapeador.Map<Taxa>(editarVm);

        var resultado = servico.Editar(taxa);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction(nameof(Listar));
        }

        ApresentarMensagemSucesso($"O registro ID [{taxa.Id}] foi editado com sucesso!");

        return RedirectToAction(nameof(Listar));
    }

    public IActionResult Excluir(int id)
    {
        var resultado = servico.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction(nameof(Listar));
        }

        var taxa = resultado.Value;

        var detalhesVm = mapeador.Map<DetalhesTaxaViewModel>(taxa);

        return View(detalhesVm);
    }

    [HttpPost]
    public IActionResult Excluir(DetalhesTaxaViewModel detalhesVm)
    {
        var resultado = servico.Excluir(detalhesVm.Id);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return View(detalhesVm);
        }

        ApresentarMensagemSucesso($"O registro ID [{detalhesVm.Id}] foi excluído com sucesso!");

        return RedirectToAction(nameof(Listar));
    }

    public IActionResult Detalhes(int id)
    {
        var resultado = servico.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());
            return RedirectToAction(nameof(Listar));
        }

        var taxa = resultado.Value;

        var detalhesVm = mapeador.Map<DetalhesTaxaViewModel>(taxa);

        return View(detalhesVm);
    }
}