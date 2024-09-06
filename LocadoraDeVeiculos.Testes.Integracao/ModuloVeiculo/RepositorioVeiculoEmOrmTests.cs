using FizzWare.NBuilder;
using LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculos;
using LocadoraDeVeiculos.Dominio.ModuloVeiculo;
using LocadoraDeVeiculos.Testes.Integracao.Compartilhado;

namespace LocadoraDeVeiculos.Testes.Integracao.ModuloVeiculo;

[TestClass]
[TestCategory("Integração")]
public class RepositorioVeiculoEmOrmTests : RepositorioEmOrmTestsBase
{
    [TestMethod]
    public void Deve_Inserir_Veiculo()
    {
        var grupo = Builder<GrupoVeiculos>
            .CreateNew()
            .With(g => g.Id = 0)
            .With(g => g.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        var veiculo = Builder<Veiculo>
            .CreateNew()
            .With(v => v.Id = 0)
            .With(v => v.GrupoVeiculosId = grupo.Id)
            .With(g => g.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        var veiculoSelecionado = repositorioVeiculo.SelecionarPorId(veiculo.Id);

        Assert.IsNotNull(veiculoSelecionado);
        Assert.AreEqual(veiculo, veiculoSelecionado);
    }

    [TestMethod]
    public void Deve_Editar_Veiculo()
    {
        var grupo = Builder<GrupoVeiculos>
            .CreateNew()
            .With(g => g.Id = 0)
            .With(g => g.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        var veiculo = Builder<Veiculo>
            .CreateNew()
            .With(v => v.Id = 0)
            .With(v => v.GrupoVeiculosId = grupo.Id)
            .With(g => g.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        veiculo.Modelo = "Novo Modelo";

        repositorioVeiculo.Editar(veiculo);

        var veiculoSelecionado = repositorioVeiculo.SelecionarPorId(veiculo.Id);

        Assert.IsNotNull(veiculoSelecionado);
        Assert.AreEqual(veiculo, veiculoSelecionado);
    }

    [TestMethod]
    public void Deve_Excluir_Veiculo()
    {
        var grupo = Builder<GrupoVeiculos>
            .CreateNew()
            .With(g => g.Id = 0)
            .With(g => g.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        var veiculo = Builder<Veiculo>
            .CreateNew()
            .With(v => v.Id = 0)
            .With(v => v.GrupoVeiculosId = grupo.Id)
            .With(g => g.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        repositorioVeiculo.Excluir(veiculo);

        var veiculoSelecionado = repositorioVeiculo.SelecionarPorId(veiculo.Id);

        var veiculos = repositorioVeiculo.SelecionarTodos();

        Assert.IsNull(veiculoSelecionado);
        Assert.AreEqual(0, veiculos.Count);
    }
}