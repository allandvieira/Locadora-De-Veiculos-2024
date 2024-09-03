using FizzWare.NBuilder;
using LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculos;
using LocadoraDeVeiculos.Infra.Orm.Compartilhado;
using LocadoraDeVeiculos.Infra.Orm.ModuloGrupoVeiculos;

namespace LocadoraDeVeiculos.Testes.Integracao.ModuloGrupoVeiculos;

[TestClass]
[TestCategory("Integração")]
public class RepositorioGrupoVeiculosEmOrmTests
{
    private LocadoraDbContext dbContext;
    private RepositorioGrupoVeiculosEmOrm repositorio;

    [TestInitialize]
    public void Inicializar()
    {
        dbContext = new LocadoraDbContext();

        dbContext.GruposVeiculos.RemoveRange(dbContext.GruposVeiculos);

        repositorio = new RepositorioGrupoVeiculosEmOrm(dbContext);

        BuilderSetup.SetCreatePersistenceMethod<GrupoVeiculos>(repositorio.Inserir);
    }

    [TestMethod]
    public void Deve_Inserir_GrupoVeiculos()
    {
        var grupo = Builder<GrupoVeiculos>
            .CreateNew()
            .With(c => c.Id = 0)
            .Persist();

        var grupoSelecionado = repositorio.SelecionarPorId(grupo.Id);

        Assert.IsNotNull(grupoSelecionado);
        Assert.AreEqual(grupo, grupoSelecionado);
    }

    [TestMethod]
    public void Deve_Editar_GrupoVeiculos()
    {
        var grupo = Builder<GrupoVeiculos>
            .CreateNew()
            .With(c => c.Id = 0)
            .Persist();

        grupo.Nome = "Teste de Edição";
        repositorio.Editar(grupo);

        var grupoSelecionado = repositorio.SelecionarPorId(grupo.Id);

        Assert.IsNotNull(grupoSelecionado);
        Assert.AreEqual(grupo, grupoSelecionado);
    }

    [TestMethod]
    public void Deve_Excluir_GrupoVeiculos()
    {
        var grupo = Builder<GrupoVeiculos>
            .CreateNew()
            .With(c => c.Id = 0)
            .Persist();

        repositorio.Excluir(grupo);

        var grupoSelecionado = repositorio.SelecionarPorId(grupo.Id);

        var grupos = repositorio.SelecionarTodos();

        Assert.IsNull(grupoSelecionado);
        Assert.AreEqual(0, grupos.Count);
    }
}