using FizzWare.NBuilder;
using LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculos;
using LocadoraDeVeiculos.Dominio.ModuloPlanoCobranca;
using LocadoraDeVeiculos.Testes.Integracao.Compartilhado;

namespace LocadoraDeVeiculos.Testes.Integracao.ModuloPlanoCobranca;

[TestClass]
[TestCategory("Integração")]
public class RepositorioPlanoCobrancaEmOrmTests : RepositorioEmOrmTestsBase
{

    [TestMethod]
    public void Deve_Inserir_PlanoCobranca()
    {
        var grupo = Builder<GrupoVeiculos>
            .CreateNew()
            .With(g => g.Id = 0)
            .With(g => g.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        var planoCobranca = Builder<PlanoCobranca>
            .CreateNew()
            .With(p => p.Id = 0)
            .With(p => p.GrupoVeiculosId = grupo.Id)
            .With(g => g.EmpresaId = usuarioAutenticado.Id)
            .Build();

        repositorioPlano.Inserir(planoCobranca);

        var planoCobrancaSelecionado = repositorioPlano.SelecionarPorId(planoCobranca.Id);

        Assert.IsNotNull(planoCobrancaSelecionado);
        Assert.AreEqual(planoCobranca, planoCobrancaSelecionado);
    }

    [TestMethod]
    public void Deve_Editar_PlanoCobranca()
    {
        var grupo = Builder<GrupoVeiculos>
            .CreateNew()
            .With(g => g.Id = 0)
            .With(g => g.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        var planoCobranca = Builder<PlanoCobranca>
            .CreateNew()
            .With(p => p.Id = 0)
            .With(p => p.GrupoVeiculosId = grupo.Id)
            .With(g => g.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        planoCobranca.PrecoDiarioPlanoDiario = 200.0m;

        repositorioPlano.Editar(planoCobranca);

        var planoCobrancaSelecionado = repositorioPlano.SelecionarPorId(planoCobranca.Id);

        Assert.IsNotNull(planoCobrancaSelecionado);
        Assert.AreEqual(planoCobranca, planoCobrancaSelecionado);
    }

    [TestMethod]
    public void Deve_Excluir_PlanoCobranca()
    {
        var grupo = Builder<GrupoVeiculos>
            .CreateNew()
            .With(g => g.Id = 0)
            .With(g => g.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        var planoCobranca = Builder<PlanoCobranca>
            .CreateNew()
            .With(p => p.Id = 0)
            .With(p => p.GrupoVeiculosId = grupo.Id)
            .With(g => g.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        repositorioPlano.Excluir(planoCobranca);

        var planoCobrancaSelecionado = repositorioPlano.SelecionarPorId(planoCobranca.Id);

        var planosCobranca = repositorioPlano.SelecionarTodos();

        Assert.IsNull(planoCobrancaSelecionado);
        Assert.AreEqual(0, planosCobranca.Count);
    }
}