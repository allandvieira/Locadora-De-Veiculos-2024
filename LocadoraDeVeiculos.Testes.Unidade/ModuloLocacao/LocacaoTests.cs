using LocadoraDeVeiculos.Dominio.ModuloLocacao;

namespace LocadoraDeVeiculos.Testes.Unidade.ModuloLocacao;

[TestClass]
[TestCategory("Unidade")]
public class LocacaoTests
{
    [TestMethod]
    public void Deve_Criar_Instancia_Valida()
    {
        // Arrange
        var locacao = new Locacao(
            veiculoId: 1,
            condutorId: 1,
            configuracaoCombustivelId: 1,
            planoCobranca: TipoPlanoCobrancaEnum.Diario,
            dataLocacao: DateTime.Now,
            devolucaoPrevista: DateTime.Now.AddDays(3)
        );

        // Act
        var erros = locacao.Validar();

        // Assert
        Assert.AreEqual(0, erros.Count);
    }

    [TestMethod]
    public void Deve_Criar_Instancia_Com_Erro()
    {
        // Arrange
        var locacao = new Locacao(
            veiculoId: 0,
            condutorId: 0,
            configuracaoCombustivelId: 1,
            planoCobranca: TipoPlanoCobrancaEnum.Diario,
            dataLocacao: DateTime.MinValue,
            devolucaoPrevista: DateTime.MinValue
        );

        // Act
        var erros = locacao.Validar();

        // Assert
        List<string> errosEsperados =
        [
            "O condutor é obrigatório",
            "O veículo é obrigatório",
            "Selecione a data da locação",
            "Selecione a data prevista da entrega"
        ];

        Assert.AreEqual(errosEsperados.Count, erros.Count);
        CollectionAssert.AreEqual(errosEsperados, erros);
    }

    [TestMethod]
    public void Deve_Retornar_Erro_Quando_DevolucaoPrevista_Eh_Menor_Que_DataLocacao()
    {
        // Arrange
        var locacao = new Locacao(
            veiculoId: 1,
            condutorId: 1,
            configuracaoCombustivelId: 1,
            planoCobranca: TipoPlanoCobrancaEnum.Diario,
            dataLocacao: DateTime.Now,
            devolucaoPrevista: DateTime.Now.AddDays(-1)
        );

        // Act
        var erros = locacao.Validar();

        // Assert
        List<string> errosEsperados =
        [
            "A data prevista da entrega não pode ser menor que data da locação"
        ];

        Assert.AreEqual(errosEsperados.Count, erros.Count);
        CollectionAssert.AreEqual(errosEsperados, erros);
    }

    [TestMethod]
    public void Deve_Retornar_True_Se_TemMulta()
    {
        // Arrange
        var locacao = new Locacao(
            veiculoId: 1,
            condutorId: 1,
            configuracaoCombustivelId: 1,
            planoCobranca: TipoPlanoCobrancaEnum.Diario,
            dataLocacao: DateTime.Now,
            devolucaoPrevista: DateTime.Now.AddDays(-1) // Devolução atrasada
        );

        locacao.RealizarDevolucao();

        // Act
        var temMulta = locacao.TemMulta();

        // Assert
        Assert.IsTrue(temMulta);
    }
}