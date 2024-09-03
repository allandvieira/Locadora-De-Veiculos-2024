using LocadoraDeVeiculos.Dominio.ModuloCliente;

namespace LocadoraDeVeiculos.Testes.Unidade.ModuloCliente;

[TestClass]
[TestCategory("Unidade")]
public class ClienteTests
{
    [TestMethod]
    public void Deve_Criar_Instancia_Valida()
    {
        var cliente = new Cliente
        (
            "João Silva",
            "joao.silva@example.com",
            "(11) 98765-4321",
            TipoCadastroClienteEnum.CPF,
            "123.456.789-00",
            "São Paulo",
            "SP",
            "Centro",
            "Rua A",
            "123"
        );

        var erros = cliente.Validar();

        Assert.AreEqual(0, erros.Count);
    }

    [TestMethod]
    public void Deve_Criar_Instancia_Com_Erro_Nome()
    {
        var cliente = new Cliente
        (
            "",
            "joao.silva@example.com",
            "(11) 98765-4321",
            TipoCadastroClienteEnum.CPF,
            "123.456.789-00",
            "São Paulo",
            "SP",
            "Centro",
            "Rua A",
            "123"
        );

        var erros = cliente.Validar();

        List<string> errosEsperados =
        [
            "O nome é obrigatório"
        ];

        Assert.AreEqual(errosEsperados.Count, erros.Count);
        CollectionAssert.AreEqual(errosEsperados, erros);
    }

    [TestMethod]
    public void Deve_Criar_Instancia_Com_Erro_Email()
    {
        var cliente = new Cliente
        (
            "João Silva",
            "",
            "(11) 98765-4321",
            TipoCadastroClienteEnum.CPF,
            "123.456.789-00",
            "São Paulo",
            "SP",
            "Centro",
            "Rua A",
            "123"
        );

        var erros = cliente.Validar();

        List<string> errosEsperados =
        [
            "O email é obrigatório"
        ];

        Assert.AreEqual(errosEsperados.Count, erros.Count);
        CollectionAssert.AreEqual(errosEsperados, erros);
    }

    [TestMethod]
    public void Deve_Criar_Instancia_Com_Erro_Telefone()
    {
        var cliente = new Cliente
        (
            "João Silva",
            "joao.silva@example.com",
            "",
            TipoCadastroClienteEnum.CPF,
            "123.456.789-00",
            "São Paulo",
            "SP",
            "Centro",
            "Rua A",
            "123"
        );

        var erros = cliente.Validar();

        List<string> errosEsperados =
        [
            "O telefone é obrigatório"
        ];

        Assert.AreEqual(errosEsperados.Count, erros.Count);
        CollectionAssert.AreEqual(errosEsperados, erros);
    }

    [TestMethod]
    public void Deve_Criar_Instancia_Com_Erros_Nome_Email_Telefone()
    {
        var cliente = new Cliente
        (
            "",
            "",
            "",
            TipoCadastroClienteEnum.CPF,
            "123.456.789-00",
            "São Paulo",
            "SP",
            "Centro",
            "Rua A",
            "123"
        );

        var erros = cliente.Validar();

        List<string> errosEsperados =
        [
            "O nome é obrigatório",
            "O email é obrigatório",
            "O telefone é obrigatório"
        ];

        Assert.AreEqual(errosEsperados.Count, erros.Count);
        CollectionAssert.AreEqual(errosEsperados, erros);
    }
}