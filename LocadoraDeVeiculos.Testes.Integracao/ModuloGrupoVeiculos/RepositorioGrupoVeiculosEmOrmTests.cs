using FizzWare.NBuilder;
using LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculos;
using LocadoraDeVeiculos.Testes.Integracao.Compartilhado;

namespace LocadoraDeVeiculos.Testes.Integracao.ModuloGrupoVeiculos;

[TestClass]
[TestCategory("Integração")]
public class RepositorioGrupoVeiculosEmOrmTests : RepositorioEmOrmTestsBase
{
    [TestMethod]
    public void Deve_Inserir_GrupoVeiculos()
    {
        var grupo = Builder<GrupoVeiculos>
            .CreateNew()
            .With(g => g.Id = 0)
            .With(g => g.EmpresaId = usuarioAutenticado.Id)
            .Build();

        repositorioGrupo.Inserir(grupo);

        var grupoSelecionado = repositorioGrupo.SelecionarPorId(grupo.Id);

        Assert.IsNotNull(grupoSelecionado);
        Assert.AreEqual(grupo, grupoSelecionado);
    }

    [TestMethod]
    public void Deve_Editar_GrupoVeiculos()
    {
        var grupo = Builder<GrupoVeiculos>
            .CreateNew()
            .With(g => g.Id = 0)
            .With(g => g.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        grupo.Nome = "Teste de Edição";
        repositorioGrupo.Editar(grupo);

        var grupoSelecionado = repositorioGrupo.SelecionarPorId(grupo.Id);

        Assert.IsNotNull(grupoSelecionado);
        Assert.AreEqual(grupo, grupoSelecionado);
    }

    [TestMethod]
    public void Deve_Excluir_GrupoVeiculos()
    {
        var grupo = Builder<GrupoVeiculos>
            .CreateNew()
            .With(g => g.Id = 0)
            .With(g => g.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        repositorioGrupo.Excluir(grupo);

        var grupoSelecionado = repositorioGrupo.SelecionarPorId(grupo.Id);

        var grupos = repositorioGrupo.SelecionarTodos();

        Assert.IsNull(grupoSelecionado);
        Assert.AreEqual(0, grupos.Count);
    }
}