using LocadoraDeVeiculos.Dominio.ModuloCondutor;

namespace LocadoraDeVeiculos.Testes.Unidade.ModuloCondutor;

[TestClass]
[TestCategory("Unidade")]
public class CondutorTests
{
    [TestMethod]
    public void Deve_Criar_Instancia_Valida()
    {
        var condutor = new Condutor
        (
            clienteId: 1,
            clienteCondutor: true,
            nome: "Maria Oliveira",
            email: "maria.oliveira@example.com",
            telefone: "(11) 98765-4321",
            cpf: "123.456.789-00",
            cnh: "12345678901",
            validadeCnh: DateTime.Today.AddYears(1)
        );

        var erros = condutor.Validar();

        Assert.AreEqual(0, erros.Count);
    }

    [TestMethod]
    public void Deve_Criar_Instancia_Com_Erro_Nome()
    {
        var condutor = new Condutor
        (
            clienteId: 1,
            clienteCondutor: true,
            nome: "",
            email: "maria.oliveira@example.com",
            telefone: "(11) 98765-4321",
            cpf: "123.456.789-00",
            cnh: "12345678901",
            validadeCnh: DateTime.Today.AddYears(1)
        );

        var erros = condutor.Validar();

        List<string> errosEsperados = new List<string>
        {
            "O nome é obrigatório"
        };

        Assert.AreEqual(errosEsperados.Count, erros.Count);
        CollectionAssert.AreEqual(errosEsperados, erros);
    }

    [TestMethod]
    public void Deve_Criar_Instancia_Com_Erro_Email()
    {
        var condutor = new Condutor
        (
            clienteId: 1,
            clienteCondutor: true,
            nome: "Maria Oliveira",
            email: "",
            telefone: "(11) 98765-4321",
            cpf: "123.456.789-00",
            cnh: "12345678901",
            validadeCnh: DateTime.Today.AddYears(1)
        );

        var erros = condutor.Validar();

        List<string> errosEsperados = new List<string>
        {
            "O email é obrigatório"
        };

        Assert.AreEqual(errosEsperados.Count, erros.Count);
        CollectionAssert.AreEqual(errosEsperados, erros);
    }

    [TestMethod]
    public void Deve_Criar_Instancia_Com_Erro_Telefone()
    {
        var condutor = new Condutor
        (
            clienteId: 1,
            clienteCondutor: true,
            nome: "Maria Oliveira",
            email: "maria.oliveira@example.com",
            telefone: "",
            cpf: "123.456.789-00",
            cnh: "12345678901",
            validadeCnh: DateTime.Today.AddYears(1)
        );

        var erros = condutor.Validar();

        List<string> errosEsperados = new List<string>
        {
            "O telefone é obrigatório"
        };

        Assert.AreEqual(errosEsperados.Count, erros.Count);
        CollectionAssert.AreEqual(errosEsperados, erros);
    }

    [TestMethod]
    public void Deve_Criar_Instancia_Com_Erro_CPF()
    {
        var condutor = new Condutor
        (
            clienteId: 1,
            clienteCondutor: true,
            nome: "Maria Oliveira",
            email: "maria.oliveira@example.com",
            telefone: "(11) 98765-4321",
            cpf: "",
            cnh: "12345678901",
            validadeCnh: DateTime.Today.AddYears(1)
        );

        var erros = condutor.Validar();

        List<string> errosEsperados = new List<string>
        {
            "O número do CPF é obrigatório"
        };

        Assert.AreEqual(errosEsperados.Count, erros.Count);
        CollectionAssert.AreEqual(errosEsperados, erros);
    }

    [TestMethod]
    public void Deve_Criar_Instancia_Com_Erro_CNH()
    {
        var condutor = new Condutor
        (
            clienteId: 1,
            clienteCondutor: true,
            nome: "Maria Oliveira",
            email: "maria.oliveira@example.com",
            telefone: "(11) 98765-4321",
            cpf: "123.456.789-00",
            cnh: "",
            validadeCnh: DateTime.Today.AddYears(1)
        );

        var erros = condutor.Validar();

        List<string> errosEsperados = new List<string>
        {
            "O número da CNH é obrigatório"
        };

        Assert.AreEqual(errosEsperados.Count, erros.Count);
        CollectionAssert.AreEqual(errosEsperados, erros);
    }

    [TestMethod]
    public void Deve_Criar_Instancia_Com_Erro_ValidadeCNH()
    {
        var condutor = new Condutor
        (
            clienteId: 1,
            clienteCondutor: true,
            nome: "Maria Oliveira",
            email: "maria.oliveira@example.com",
            telefone: "(11) 98765-4321",
            cpf: "123.456.789-00",
            cnh: "12345678901",
            validadeCnh: DateTime.Today.AddYears(-1)
        );

        var erros = condutor.Validar();

        List<string> errosEsperados = new List<string>
        {
            "A CNH está vencida"
        };

        Assert.AreEqual(errosEsperados.Count, erros.Count);
        CollectionAssert.AreEqual(errosEsperados, erros);
    }

    [TestMethod]
    public void Deve_Criar_Instancia_Com_Erros_Nome_Email_Telefone()
    {
        var condutor = new Condutor
        (
            clienteId: 1,
            clienteCondutor: true,
            nome: "",
            email: "",
            telefone: "",
            cpf: "123.456.789-00",
            cnh: "12345678901",
            validadeCnh: DateTime.Today.AddYears(1)
        );

        var erros = condutor.Validar();

        List<string> errosEsperados = new List<string>
        {
            "O nome é obrigatório",
            "O email é obrigatório",
            "O telefone é obrigatório"
        };

        Assert.AreEqual(errosEsperados.Count, erros.Count);
        CollectionAssert.AreEqual(errosEsperados, erros);
    }
}