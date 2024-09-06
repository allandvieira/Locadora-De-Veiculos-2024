using AutoMapper;
using LocadoraDeVeiculos.Aplicacao.ModuloAutenticacao;
using LocadoraDeVeiculos.Aplicacao.ModuloGrupoVeiculos;
using LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculos;
using LocadoraDeVeiculos.WebApp.Controllers.Compartilhado;
using LocadoraDeVeiculos.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraDeVeiculos.WebApp.Controllers;

[Authorize(Roles = "Empresa,Funcionario")]
public class GrupoVeiculosController : WebControllerBase
{
    private readonly ServicoGrupoVeiculos servico;
    private readonly IMapper mapeador;

    public GrupoVeiculosController(
        ServicoAutenticacao servicoAuth,
        ServicoGrupoVeiculos servico,
        IMapper mapeador
    ) : base(servicoAuth)
    {
        this.servico = servico;
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

        var grupos = resultado.Value;

        var listarGruposVm =
            mapeador.Map<IEnumerable<ListarGrupoVeiculosViewModel>>(grupos);

        return View(listarGruposVm);
    }

    public IActionResult Inserir()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Inserir(InserirGrupoVeiculosViewModel inserirVm)
    {
        if (!ModelState.IsValid)
            return View(inserirVm);

        var grupo = mapeador.Map<GrupoVeiculos>(inserirVm);

        var resultado = servico.Inserir(grupo);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction(nameof(Listar));
        }

        ApresentarMensagemSucesso($"O registro ID [{grupo.Id}] foi inserido com sucesso!");

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

        var grupo = resultado.Value;

        var editarVm = mapeador.Map<EditarGrupoVeiculosViewModel>(grupo);

        return View(editarVm);
    }

    [HttpPost]
    public IActionResult Editar(EditarGrupoVeiculosViewModel editarVM)
    {
        if (!ModelState.IsValid)
            return View(editarVM);

        var grupo = mapeador.Map<GrupoVeiculos>(editarVM);

        var resultado = servico.Editar(grupo);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction(nameof(Listar));
        }

        ApresentarMensagemSucesso($"O registro ID [{grupo.Id}] foi editado com sucesso!");

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

        var grupo = resultado.Value;

        var detalhesVm = mapeador.Map<DetalhesGrupoVeiculosViewModel>(grupo);

        return View(detalhesVm);
    }

    [HttpPost]
    public IActionResult Excluir(DetalhesGrupoVeiculosViewModel detalhesVm)
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

        var grupo = resultado.Value;

        var detalhesVm = mapeador.Map<DetalhesGrupoVeiculosViewModel>(grupo);

        return View(detalhesVm);
    }
}