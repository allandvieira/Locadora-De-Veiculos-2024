using FizzWare.NBuilder;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using LocadoraDeVeiculos.Testes.Integracao.Compartilhado;

namespace LocadoraDeVeiculos.Testes.Integracao.ModuloCondutor;

[TestClass]
[TestCategory("Integração")]
public class RepositorioCondutorEmOrmTests : RepositorioEmOrmTestsBase
{
    [TestMethod]
    public void Deve_Inserir_Condutor()
    {
        var cliente = Builder<Cliente>
            .CreateNew()
            .With(c => c.Id = 0)
            .Persist();

        var condutor = Builder<Condutor>
            .CreateNew()
            .With(c => c.Id = 0)
            .With(c => c.ClienteId = cliente.Id)
            .Build();

        repositorioCondutor.Inserir(condutor);

        var condutorSelecionado = repositorioCondutor.SelecionarPorId(condutor.Id);

        Assert.IsNotNull(condutorSelecionado);
        Assert.AreEqual(condutor, condutorSelecionado);
    }

    [TestMethod]
    public void Deve_Editar_Condutor()
    {
        var cliente = Builder<Cliente>
            .CreateNew()
            .With(c => c.Id = 0)
            .Persist();

        var condutor = Builder<Condutor>
            .CreateNew()
            .With(c => c.Id = 0)
            .With(c => c.ClienteId = cliente.Id)
            .Persist();

        condutor.Nome = "Nome Atualizado";
        condutor.Email = "novoemail@dominio.com";

        repositorioCondutor.Editar(condutor);

        var condutorSelecionado = repositorioCondutor.SelecionarPorId(condutor.Id);

        Assert.IsNotNull(condutorSelecionado);
        Assert.AreEqual(condutor, condutorSelecionado);
    }

    [TestMethod]
    public void Deve_Excluir_Condutor()
    {
        var cliente = Builder<Cliente>
            .CreateNew()
            .With(c => c.Id = 0)
            .Persist();

        var condutor = Builder<Condutor>
            .CreateNew()
            .With(c => c.Id = 0)
            .With(c => c.ClienteId = cliente.Id)
            .Persist();

        repositorioCondutor.Excluir(condutor);

        var condutorSelecionado = repositorioCondutor.SelecionarPorId(condutor.Id);

        var condutores = repositorioCondutor.SelecionarTodos();

        Assert.IsNull(condutorSelecionado);
        Assert.AreEqual(0, condutores.Count);
    }
}