using FizzWare.NBuilder;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Testes.Integracao.Compartilhado;

namespace LocadoraDeVeiculos.Testes.Integracao.ModuloCliente;

[TestClass]
[TestCategory("Integração")]
public class RepositorioClienteEmOrmTests : RepositorioEmOrmTestsBase
{
    [TestMethod]
    public void Deve_Inserir_Cliente()
    {
        var cliente = Builder<Cliente>
            .CreateNew()
            .With(c => c.Id = 0)
            .With(g => g.EmpresaId = usuarioAutenticado.Id)
            .Build();

        repositorioCliente.Inserir(cliente);

        var clienteSelecionado = repositorioCliente.SelecionarPorId(cliente.Id);

        Assert.IsNotNull(clienteSelecionado);
        Assert.AreEqual(cliente, clienteSelecionado);
    }

    [TestMethod]
    public void Deve_Editar_Cliente()
    {
        var cliente = Builder<Cliente>
            .CreateNew()
            .With(c => c.Id = 0)
            .With(g => g.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        cliente.Nome = "Nome Atualizado";
        cliente.Email = "novoemail@dominio.com";

        repositorioCliente.Editar(cliente);

        var clienteSelecionado = repositorioCliente.SelecionarPorId(cliente.Id);

        Assert.IsNotNull(clienteSelecionado);
        Assert.AreEqual(cliente, clienteSelecionado);
    }

    [TestMethod]
    public void Deve_Excluir_Cliente()
    {
        var cliente = Builder<Cliente>
            .CreateNew()
            .With(c => c.Id = 0)
            .With(g => g.EmpresaId = usuarioAutenticado.Id)
            .Persist();

        repositorioCliente.Excluir(cliente);

        var clienteSelecionado = repositorioCliente.SelecionarPorId(cliente.Id);

        var clientes = repositorioCliente.SelecionarTodos();

        Assert.IsNull(clienteSelecionado);
        Assert.AreEqual(0, clientes.Count);
    }
}