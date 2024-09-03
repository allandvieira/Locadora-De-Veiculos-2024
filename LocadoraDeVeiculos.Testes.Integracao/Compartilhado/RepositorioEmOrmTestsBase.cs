using FizzWare.NBuilder;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculos;
using LocadoraDeVeiculos.Dominio.ModuloPlanoCobranca;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using LocadoraDeVeiculos.Dominio.ModuloVeiculo;
using LocadoraDeVeiculos.Infra.Orm.Compartilhado;
using LocadoraDeVeiculos.Infra.Orm.ModuloCliente;
using LocadoraDeVeiculos.Infra.Orm.ModuloGrupoVeiculos;
using LocadoraDeVeiculos.Infra.Orm.ModuloPlanoCobranca;
using LocadoraDeVeiculos.Infra.Orm.ModuloTaxa;
using LocadoraDeVeiculos.Infra.Orm.ModuloVeiculo;

namespace LocadoraDeVeiculos.Testes.Integracao.Compartilhado;

public abstract class RepositorioEmOrmTestsBase
{
    protected LocadoraDbContext dbContext;
    protected RepositorioTaxaEmOrm repositorioTaxa;
    protected RepositorioClienteEmOrm repositorioCliente;
    protected RepositorioVeiculoEmOrm repositorioVeiculo;
    protected RepositorioGrupoVeiculosEmOrm repositorioGrupo;
    protected RepositorioPlanoCobrancaEmOrm repositorioPlano;

    [TestInitialize]
    public void Inicializar()
    {
        dbContext = new LocadoraDbContext();

        dbContext.Taxas.RemoveRange(dbContext.Taxas);
        dbContext.PlanosCobranca.RemoveRange(dbContext.PlanosCobranca);
        dbContext.Clientes.RemoveRange(dbContext.Clientes);
        dbContext.Veiculos.RemoveRange(dbContext.Veiculos);
        dbContext.GruposVeiculos.RemoveRange(dbContext.GruposVeiculos);

        dbContext.SaveChanges();

        repositorioTaxa = new RepositorioTaxaEmOrm(dbContext);
        repositorioPlano = new RepositorioPlanoCobrancaEmOrm(dbContext);
        repositorioCliente = new RepositorioClienteEmOrm(dbContext);
        repositorioVeiculo = new RepositorioVeiculoEmOrm(dbContext);
        repositorioGrupo = new RepositorioGrupoVeiculosEmOrm(dbContext);

        BuilderSetup.SetCreatePersistenceMethod<Taxa>(repositorioTaxa.Inserir);
        BuilderSetup.SetCreatePersistenceMethod<PlanoCobranca>(repositorioPlano.Inserir);
        BuilderSetup.SetCreatePersistenceMethod<Cliente>(repositorioCliente.Inserir);
        BuilderSetup.SetCreatePersistenceMethod<Veiculo>(repositorioVeiculo.Inserir);
        BuilderSetup.SetCreatePersistenceMethod<GrupoVeiculos>(repositorioGrupo.Inserir);
    }
}