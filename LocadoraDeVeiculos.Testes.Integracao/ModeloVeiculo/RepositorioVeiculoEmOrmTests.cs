using FizzWare.NBuilder;
using LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculos;
using LocadoraDeVeiculos.Dominio.ModuloVeiculo;
using LocadoraDeVeiculos.Infra.Orm.Compartilhado;
using LocadoraDeVeiculos.Infra.Orm.ModuloGrupoVeiculos;
using LocadoraDeVeiculos.Infra.Orm.ModuloVeiculo;

namespace LocadoraDeVeiculos.Testes.Integracao.ModuloVeiculo;

[TestClass]
[TestCategory("Integração")]
public class RepositorioVeiculoEmOrmTests
{
    private LocadoraDbContext dbContext;
    private RepositorioVeiculoEmOrm repositorio;
    private RepositorioGrupoVeiculosEmOrm repositorioGrupos;

    [TestInitialize]
    public void Inicializar()
    {
        dbContext = new LocadoraDbContext();

        dbContext.Veiculos.RemoveRange(dbContext.Veiculos);
        dbContext.GruposVeiculos.RemoveRange(dbContext.GruposVeiculos);

        repositorio = new RepositorioVeiculoEmOrm(dbContext);
        repositorioGrupos = new RepositorioGrupoVeiculosEmOrm(dbContext);

        BuilderSetup.SetCreatePersistenceMethod<Veiculo>(repositorio.Inserir);
        BuilderSetup.SetCreatePersistenceMethod<GrupoVeiculos>(repositorioGrupos.Inserir);
    }

    [TestMethod]
    public void Deve_Inserir_Veiculo()
    {
        var grupo = Builder<GrupoVeiculos>
            .CreateNew()
            .With(g => g.Id = 0)
            .Persist();

        var veiculo = Builder<Veiculo>
            .CreateNew()
            .With(v => v.Id = 0)
            .With(v => v.GrupoVeiculosId = grupo.Id)
            .Persist();

        var veiculoSelecionado = repositorio.SelecionarPorId(veiculo.Id);

        Assert.IsNotNull(veiculoSelecionado);
        Assert.AreEqual(veiculo, veiculoSelecionado);
    }

    [TestMethod]
    public void Deve_Editar_Veiculo()
    {
        var grupo = Builder<GrupoVeiculos>
            .CreateNew()
            .With(g => g.Id = 0)
            .Persist();

        var veiculo = Builder<Veiculo>
            .CreateNew()
            .With(v => v.Id = 0)
            .With(v => v.GrupoVeiculosId = grupo.Id)
            .Persist();

        veiculo.Modelo = "Novo Modelo";

        repositorio.Editar(veiculo);

        var veiculoSelecionado = repositorio.SelecionarPorId(veiculo.Id);

        Assert.IsNotNull(veiculoSelecionado);
        Assert.AreEqual(veiculo, veiculoSelecionado);
    }

    [TestMethod]
    public void Deve_Excluir_Veiculo()
    {
        var grupo = Builder<GrupoVeiculos>
            .CreateNew()
            .With(g => g.Id = 0)
            .Persist();

        var veiculo = Builder<Veiculo>
            .CreateNew()
            .With(v => v.Id = 0)
            .With(v => v.GrupoVeiculosId = grupo.Id)
            .Persist();

        repositorio.Excluir(veiculo);

        var veiculoSelecionado = repositorio.SelecionarPorId(veiculo.Id);

        var veiculos = repositorio.SelecionarTodos();

        Assert.IsNull(veiculoSelecionado);
        Assert.AreEqual(0, veiculos.Count);
    }
}