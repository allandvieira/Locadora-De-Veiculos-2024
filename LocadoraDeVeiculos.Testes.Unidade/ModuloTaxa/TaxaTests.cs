using LocadoraDeVeiculos.Dominio.ModuloTaxa;

namespace LocadoraDeVeiculos.Testes.Unidade.ModuloTaxa;

[TestClass]
[TestCategory("Unidade")]
public class TaxaTests
{
    [TestMethod]
    public void Deve_Criar_Instancia_Valida()
    {
        var taxa = new Taxa
        (
            "Taxa de Serviço",
            10.0m,
            TipoCobrancaEnum.Diaria
        );

        var erros = taxa.Validar();

        Assert.AreEqual(0, erros.Count);
    }

    [TestMethod]
    public void Deve_Criar_Instancia_Com_Erro_Nome()
    {
        var taxa = new Taxa
        (
            "Tx",
            10.0m,
            TipoCobrancaEnum.Diaria
        );

        var erros = taxa.Validar();

        List<string> errosEsperados =
        [
            "O nome precisa conter ao menos 3 caracteres"
        ];

        Assert.AreEqual(errosEsperados.Count, erros.Count);
        CollectionAssert.AreEqual(errosEsperados, erros);
    }

    [TestMethod]
    public void Deve_Criar_Instancia_Com_Erro_Valor()
    {
        var taxa = new Taxa
        (
            "Taxa de Serviço",
            0.5m,
            TipoCobrancaEnum.Diaria
        );

        var erros = taxa.Validar();

        List<string> errosEsperados =
        [
            "O valor precisa ser ao menos 1"
        ];

        Assert.AreEqual(errosEsperados.Count, erros.Count);
        CollectionAssert.AreEqual(errosEsperados, erros);
    }

    [TestMethod]
    public void Deve_Criar_Instancia_Com_Erros_Nome_Valor()
    {
        var taxa = new Taxa
        (
            "Tx",
            0.5m,
            TipoCobrancaEnum.Diaria
        );

        var erros = taxa.Validar();

        List<string> errosEsperados =
        [
            "O nome precisa conter ao menos 3 caracteres",
            "O valor precisa ser ao menos 1"
        ];

        Assert.AreEqual(errosEsperados.Count, erros.Count);
        CollectionAssert.AreEqual(errosEsperados, erros);
    }
}