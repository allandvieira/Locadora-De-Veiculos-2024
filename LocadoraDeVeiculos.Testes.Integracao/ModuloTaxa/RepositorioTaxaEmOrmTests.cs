using FizzWare.NBuilder;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using LocadoraDeVeiculos.Infra.Orm.Compartilhado;
using LocadoraDeVeiculos.Infra.Orm.ModuloTaxa;

namespace LocadoraDeVeiculos.Testes.Integracao.ModuloTaxa;

[TestClass]
[TestCategory("Integração")]
public class RepositorioTaxaEmOrmTests
{
    private LocadoraDbContext dbContext;
    private RepositorioTaxaEmOrm repositorio;

    [TestInitialize]
    public void Inicializar()
    {
        dbContext = new LocadoraDbContext();

        dbContext.Taxas.RemoveRange(dbContext.Taxas);

        repositorio = new RepositorioTaxaEmOrm(dbContext);

        BuilderSetup.SetCreatePersistenceMethod<Taxa>(repositorio.Inserir);
    }

    [TestMethod]
    public void Deve_Inserir_Taxa()
    {
        var taxa = Builder<Taxa>
            .CreateNew()
            .With(t => t.Id = 0)
            .Build();

        repositorio.Inserir(taxa);

        var taxaSelecionada = repositorio.SelecionarPorId(taxa.Id);

        Assert.IsNotNull(taxaSelecionada);
        Assert.AreEqual(taxa, taxaSelecionada);
    }

    [TestMethod]
    public void Deve_Editar_Taxa()
    {
        var taxa = Builder<Taxa>
            .CreateNew()
            .With(t => t.Id = 0)
            .Persist();

        taxa.Nome = "Taxa Atualizada";
        taxa.Valor = 100.0m;

        repositorio.Editar(taxa);

        var taxaSelecionada = repositorio.SelecionarPorId(taxa.Id);

        Assert.IsNotNull(taxaSelecionada);
        Assert.AreEqual(taxa, taxaSelecionada);
    }

    [TestMethod]
    public void Deve_Excluir_Taxa()
    {
        var taxa = Builder<Taxa>
            .CreateNew()
            .With(t => t.Id = 0)
            .Persist();

        repositorio.Excluir(taxa);

        var taxaSelecionada = repositorio.SelecionarPorId(taxa.Id);

        var taxas = repositorio.SelecionarTodos();

        Assert.IsNull(taxaSelecionada);
        Assert.AreEqual(0, taxas.Count);
    }
}