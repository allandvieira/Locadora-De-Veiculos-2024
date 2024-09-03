using AutoMapper;
using LocadoraDeVeiculos.Aplicacao.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.WebApp.Controllers.Compartilhado;
using LocadoraDeVeiculos.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraDeVeiculos.WebApp.Controllers;

public class ClienteController : WebControllerBase
{
    private readonly ServicoCliente servico;
    private readonly IMapper mapeador;

    public ClienteController(ServicoCliente servico, IMapper mapeador)
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

        var clientes = resultado.Value;

        var listarTaxasVm = mapeador.Map<IEnumerable<ListarClienteViewModel>>(clientes);

        return View(listarTaxasVm);
    }

    public IActionResult Inserir()
    {
        return View(new InserirClienteViewModel());
    }

    [HttpPost]
    public IActionResult Inserir(InserirClienteViewModel inserirVm)
    {
        if (!ModelState.IsValid)
            return View(inserirVm);

        var cliente = mapeador.Map<Cliente>(inserirVm);

        var resultado = servico.Inserir(cliente);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return View(inserirVm);
        }

        ApresentarMensagemSucesso($"O cliente ID [{cliente.Id}] foi inserido com sucesso!");

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

        var cliente = resultado.Value;

        var editarVm = mapeador.Map<EditarClienteViewModel>(cliente);

        return View(editarVm);
    }

    [HttpPost]
    public IActionResult Editar(EditarClienteViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        var cliente = mapeador.Map<Cliente>(editarVm);

        var resultado = servico.Editar(cliente);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return View(editarVm);
        }

        ApresentarMensagemSucesso($"O cliente ID [{cliente.Id}] foi editado com sucesso!");

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

        var cliente = resultado.Value;

        var detalhesVm = mapeador.Map<DetalhesClienteViewModel>(cliente);

        return View(detalhesVm);
    }

    [HttpPost]
    public IActionResult Excluir(DetalhesClienteViewModel detalhesVm)
    {
        var resultado = servico.Excluir(detalhesVm.Id);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return View(detalhesVm);
        }

        ApresentarMensagemSucesso($"O cliente ID [{detalhesVm.Id}] foi excluído com sucesso!");

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

        var cliente = resultado.Value;

        var detalhesVm = mapeador.Map<DetalhesClienteViewModel>(cliente);

        return View(detalhesVm);
    }
}