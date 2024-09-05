using FizzWare.NBuilder;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCombustivel;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculos;
using LocadoraDeVeiculos.Dominio.ModuloLocacao;
using LocadoraDeVeiculos.Dominio.ModuloPlanoCobranca;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using LocadoraDeVeiculos.Dominio.ModuloVeiculo;
using LocadoraDeVeiculos.Infra.Orm.Compartilhado;
using LocadoraDeVeiculos.Infra.Orm.ModuloCliente;
using LocadoraDeVeiculos.Infra.Orm.ModuloCombustivel;
using LocadoraDeVeiculos.Infra.Orm.ModuloCondutor;
using LocadoraDeVeiculos.Infra.Orm.ModuloGrupoVeiculos;
using LocadoraDeVeiculos.Infra.Orm.ModuloLocacao;
using LocadoraDeVeiculos.Infra.Orm.ModuloPlanoCobranca;
using LocadoraDeVeiculos.Infra.Orm.ModuloTaxa;
using LocadoraDeVeiculos.Infra.Orm.ModuloVeiculo;

namespace LocadoraDeVeiculos.Testes.Integracao.Compartilhado;

public abstract class RepositorioEmOrmTestsBase
{
    protected LocadoraDbContext dbContext;

    protected RepositorioLocacaoEmOrm repositorioLocacao;
    protected RepositorioConfiguracaoCombustivelEmOrm repositorioCombustivel;
    protected RepositorioTaxaEmOrm repositorioTaxa;
    protected RepositorioClienteEmOrm repositorioCliente;
    protected RepositorioCondutorEmOrm repositorioCondutor;
    protected RepositorioVeiculoEmOrm repositorioVeiculo;
    protected RepositorioGrupoVeiculosEmOrm repositorioGrupo;
    protected RepositorioPlanoCobrancaEmOrm repositorioPlano;

    [TestInitialize]
    public void Inicializar()
    {
        dbContext = new LocadoraDbContext();

        dbContext.Locacoes.RemoveRange(dbContext.Locacoes);
        dbContext.ConfiguracoesCombustiveis.RemoveRange(dbContext.ConfiguracoesCombustiveis);
        dbContext.Taxas.RemoveRange(dbContext.Taxas);
        dbContext.PlanosCobranca.RemoveRange(dbContext.PlanosCobranca);
        dbContext.Condutores.RemoveRange(dbContext.Condutores);
        dbContext.Clientes.RemoveRange(dbContext.Clientes);
        dbContext.Veiculos.RemoveRange(dbContext.Veiculos);
        dbContext.GruposVeiculos.RemoveRange(dbContext.GruposVeiculos);

        dbContext.SaveChanges();

        repositorioTaxa = new RepositorioTaxaEmOrm(dbContext);
        repositorioPlano = new RepositorioPlanoCobrancaEmOrm(dbContext);
        repositorioCliente = new RepositorioClienteEmOrm(dbContext);
        repositorioCondutor = new RepositorioCondutorEmOrm(dbContext);
        repositorioVeiculo = new RepositorioVeiculoEmOrm(dbContext);
        repositorioGrupo = new RepositorioGrupoVeiculosEmOrm(dbContext);
        repositorioLocacao = new RepositorioLocacaoEmOrm(dbContext);
        repositorioCombustivel = new RepositorioConfiguracaoCombustivelEmOrm(dbContext);

        BuilderSetup.SetCreatePersistenceMethod<Locacao>(repositorioLocacao.Inserir);
        BuilderSetup.SetCreatePersistenceMethod<ConfiguracaoCombustivel>(repositorioCombustivel.GravarConfiguracao);
        BuilderSetup.SetCreatePersistenceMethod<Taxa>(repositorioTaxa.Inserir);
        BuilderSetup.SetCreatePersistenceMethod<PlanoCobranca>(repositorioPlano.Inserir);
        BuilderSetup.SetCreatePersistenceMethod<Condutor>(repositorioCondutor.Inserir);
        BuilderSetup.SetCreatePersistenceMethod<Cliente>(repositorioCliente.Inserir);
        BuilderSetup.SetCreatePersistenceMethod<Veiculo>(repositorioVeiculo.Inserir);
        BuilderSetup.SetCreatePersistenceMethod<GrupoVeiculos>(repositorioGrupo.Inserir);
    }
}