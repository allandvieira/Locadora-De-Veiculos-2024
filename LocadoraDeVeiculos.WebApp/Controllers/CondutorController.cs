using AutoMapper;
using LocadoraDeVeiculos.Aplicacao.ModuloAutenticacao;
using LocadoraDeVeiculos.Aplicacao.ModuloCliente;
using LocadoraDeVeiculos.Aplicacao.ModuloCondutor;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using LocadoraDeVeiculos.WebApp.Controllers.Compartilhado;
using LocadoraDeVeiculos.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LocadoraDeVeiculos.WebApp.Controllers;

[Authorize(Roles = "Empresa,Funcionario")]
public class CondutorController : WebControllerBase
{
    private readonly ServicoCondutor servico;
    private readonly ServicoCliente servicoCliente;
    private readonly IMapper mapeador;

    public CondutorController(
        ServicoAutenticacao servicoAuth,
        ServicoCondutor servico,
        ServicoCliente servicoCliente,
        IMapper mapeador) : base(servicoAuth)
    {
        this.servico = servico;
        this.servicoCliente = servicoCliente;
        this.mapeador = mapeador;
    }

    public IActionResult Listar()
    {
        var resultado = servico.SelecionarTodos(EmpresaId.GetValueOrDefault());

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction("Index", "Home");
        }

        var condutores = resultado.Value;

        var listarCondutoresVm = mapeador.Map<IEnumerable<ListarCondutorViewModel>>(condutores);

        return View(listarCondutoresVm);
    }

    public IActionResult SelecionarCliente()
    {
        var clientesResult = servicoCliente.SelecionarTodos(EmpresaId.GetValueOrDefault());

        if (clientesResult.IsFailed)
            return RedirectToAction("Index", "Home");

        var clientes = clientesResult.Value;

        var selecionarVm = new SelecionarClienteViewModel()
        {
            Clientes = clientes.Select(c => new SelectListItem(c.Nome, c.Id.ToString()))
        };

        return View(selecionarVm);
    }

    [HttpPost]
    public IActionResult SelecionarCliente(SelecionarClienteViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        int clienteId = viewModel.ClienteId;
        bool clienteCondutor = viewModel.ClienteCondutor;

        return RedirectToAction("PreencherCondutor", new { clienteId, clienteCondutor });
    }

    public IActionResult PreencherCondutor(int clienteId, bool clienteCondutor)
    {
        var clienteResult = servicoCliente.SelecionarPorId(clienteId);

        if (clienteResult.IsFailed)
            return RedirectToAction("SelecionarCliente");

        var cliente = clienteResult.Value;

        var viewModel = new FormularioCondutorViewModel();

        if (clienteCondutor)
        {
            viewModel.ClienteId = clienteId;
            viewModel.ClienteCondutor = clienteCondutor;
            viewModel.Nome = cliente.Nome;
            viewModel.Email = cliente.Email;
            viewModel.Telefone = cliente.Telefone;
            viewModel.CPF = cliente.NumeroDocumento;
        }

        ViewBag.ClienteSelecionado = cliente.Nome;

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult PreencherCondutor(FormularioCondutorViewModel inserirVm)
    {
        if (!ModelState.IsValid)
            return View(inserirVm);

        var condutor = mapeador.Map<Condutor>(inserirVm);

        var resultado = servico.Inserir(condutor);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction(nameof(Listar));
        }

        ApresentarMensagemSucesso($"O registro ID [{condutor.Id}] foi inserido com sucesso!");

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

        var condutor = resultado.Value;

        var detalhesVm = mapeador.Map<DetalhesCondutorViewModel>(condutor);

        return View(detalhesVm);
    }

    [HttpPost]
    public IActionResult Excluir(DetalhesCondutorViewModel detalhesVm)
    {
        var resultado = servico.Excluir(detalhesVm.Id);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction(nameof(Listar));
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

        var condutor = resultado.Value;

        var detalhesVm = mapeador.Map<DetalhesCondutorViewModel>(condutor);

        return View(detalhesVm);
    }
}