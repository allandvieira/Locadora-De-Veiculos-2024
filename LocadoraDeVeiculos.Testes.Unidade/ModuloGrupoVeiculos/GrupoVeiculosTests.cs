using LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculos;

namespace LocadoraDeVeiculos.Testes.Unidade.ModuloGrupoVeiculos;

[TestClass]
[TestCategory("Unidade")]
public class GrupoVeiculosTests
{
    [TestMethod]
    public void Deve_Criar_Instancia_Valida()
    {
        var grupo = new GrupoVeiculos("SUV");

        var erros = grupo.Validar();

        Assert.AreEqual(0, erros.Count);
    }

    [TestMethod]
    public void Deve_Criar_Instancia_Com_Erro()
    {
        var grupo = new GrupoVeiculos("");

        var erros = grupo.Validar();

        List<string> errosEsperados = ["O nome é obrigatório"];

        Assert.AreEqual(1, erros.Count);
        CollectionAssert.AreEqual(errosEsperados, erros);
    }
}