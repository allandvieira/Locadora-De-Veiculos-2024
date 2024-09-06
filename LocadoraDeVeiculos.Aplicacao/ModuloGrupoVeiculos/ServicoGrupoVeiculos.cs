using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculos;

namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoVeiculos;

public class ServicoGrupoVeiculos
{
    private readonly IRepositorioGrupoVeiculos repositorioGrupo;

    public ServicoGrupoVeiculos(IRepositorioGrupoVeiculos repositorioGrupo)
    {
        this.repositorioGrupo = repositorioGrupo;
    }

    public Result<GrupoVeiculos> Inserir(GrupoVeiculos grupo)
    {
        repositorioGrupo.Inserir(grupo);

        return Result.Ok(grupo);
    }

    public Result<GrupoVeiculos> Editar(GrupoVeiculos grupoAtualizado)
    {
        var grupo = repositorioGrupo.SelecionarPorId(grupoAtualizado.Id);

        if (grupo is null)
            return Result.Fail("O grupo não foi encontrado!");

        grupo.Nome = grupoAtualizado.Nome;

        repositorioGrupo.Editar(grupo);

        return Result.Ok(grupo);
    }

    public Result<GrupoVeiculos> Excluir(int grupoId)
    {
        var grupo = repositorioGrupo.SelecionarPorId(grupoId);

        if (grupo is null)
            return Result.Fail("O grupo não foi encontrado!");

        repositorioGrupo.Excluir(grupo);

        return Result.Ok(grupo);
    }

    public Result<GrupoVeiculos> SelecionarPorId(int grupoId)
    {
        var grupo = repositorioGrupo.SelecionarPorId(grupoId);

        if (grupo is null)
            return Result.Fail("O grupo não foi encontrado!");

        return Result.Ok(grupo);
    }

    public Result<List<GrupoVeiculos>> SelecionarTodos(int empresaId)
    {
        var grupos = repositorioGrupo.Filtrar(g => g.EmpresaId == empresaId);

        return Result.Ok(grupos);
    }
}