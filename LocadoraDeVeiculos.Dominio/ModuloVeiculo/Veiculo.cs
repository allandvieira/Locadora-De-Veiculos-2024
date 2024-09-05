using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculos;
using LocadoraDeVeiculos.Dominio.ModuloLocacao;

namespace LocadoraDeVeiculos.Dominio.ModuloVeiculo;

public class Veiculo : EntidadeBase
{
    public string Modelo { get; set; }
    public string Marca { get; set; }
    public TipoCombustivelEnum TipoCombustivel { get; set; }
    public int CapacidadeTanque { get; set; }
    public byte[] Foto { get; set; }

    public int GrupoVeiculosId { get; set; }
    public GrupoVeiculos? GrupoVeiculos { get; set; }

    public bool Alugado { get; set; }

    protected Veiculo() { }

    public Veiculo(
        string modelo,
        string marca,
        TipoCombustivelEnum tipoCombustivel,
        int capacidadeTanque,
        int grupoVeiculosId
    )
    {
        Modelo = modelo;
        Marca = marca;
        TipoCombustivel = tipoCombustivel;
        CapacidadeTanque = capacidadeTanque;
        GrupoVeiculosId = grupoVeiculosId;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrEmpty(Modelo))
            erros.Add("O modelo é obrigatório");

        if (string.IsNullOrEmpty(Marca))
            erros.Add("A marca é obrigatória");

        if (CapacidadeTanque == 0)
            erros.Add("A capacidade do tanque precisa ser informada");

        if (GrupoVeiculosId == 0)
            erros.Add("O grupo de veículos é obrigatório");

        return erros;
    }

    public void Alugar()
    {
        Alugado = true;
    }

    public void Desocupar()
    {
        Alugado = false;
    }

    public decimal CalcularLitrosParaAbastecimento(MarcadorCombustivelEnum marcadorCombustivel)
    {
        switch (marcadorCombustivel)
        {
            case MarcadorCombustivelEnum.Vazio: return CapacidadeTanque;

            case MarcadorCombustivelEnum.UmQuarto: return (CapacidadeTanque - (CapacidadeTanque * 1 / 4));

            case MarcadorCombustivelEnum.MeioTanque: return (CapacidadeTanque - (CapacidadeTanque * 1 / 2));

            case MarcadorCombustivelEnum.TresQuartos: return (CapacidadeTanque - (CapacidadeTanque * 3 / 4));

            default:
                return 0;
        }
    }
}