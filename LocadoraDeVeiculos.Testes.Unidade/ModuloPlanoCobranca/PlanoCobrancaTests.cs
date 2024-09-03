using LocadoraDeVeiculos.Dominio.ModuloPlanoCobranca;

namespace LocadoraDeVeiculos.Testes.Unidade.ModuloPlanoCobranca;

[TestClass]
[TestCategory("Unidade")]
public class PlanoCobrancaTests
{
    [TestMethod]
    public void Deve_Criar_Instancia_Valida()
    {
        var planoCobranca = new PlanoCobranca
        (
            1,
            100.0m,   // PrecoDiarioPlanoDiario
            1.0m,     // PrecoQuilometroPlanoDiario
            200.0m,   // QuilometrosDisponiveisPlanoControlado
            80.0m,    // PrecoDiarioPlanoControlado
            2.0m,     // PrecoQuilometroExtrapoladoPlanoControlado
            150.0m    // PrecoDiarioPlanoLivre
        );

        var erros = planoCobranca.Validar();

        Assert.AreEqual(0, erros.Count);
    }

    [TestMethod]
    public void Deve_Criar_Instancia_Com_Erro()
    {
        var planoCobranca = new PlanoCobranca
        (
            0,        // GrupoVeiculosId
            0.0m,     // PrecoDiarioPlanoDiario
            0.0m,     // PrecoQuilometroPlanoDiario
            0.0m,     // QuilometrosDisponiveisPlanoControlado
            0.0m,     // PrecoDiarioPlanoControlado
            0.0m,     // PrecoQuilometroExtrapoladoPlanoControlado
            0.0m      // PrecoDiarioPlanoLivre
        );

        var erros = planoCobranca.Validar();

        List<string> errosEsperados =
        [
            "O grupo de veículos é obrigatório",
        ];

        Assert.AreEqual(errosEsperados.Count, erros.Count);
        CollectionAssert.AreEqual(errosEsperados, erros);
    }
}