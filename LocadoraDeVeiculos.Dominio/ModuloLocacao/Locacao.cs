using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloCombustivel;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using LocadoraDeVeiculos.Dominio.ModuloPlanoCobranca;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using LocadoraDeVeiculos.Dominio.ModuloVeiculo;

namespace LocadoraDeVeiculos.Dominio.ModuloLocacao;
public class Locacao : EntidadeBase
{
    public int CondutorId { get; set; }
    public Condutor? Condutor { get; set; }

    public int VeiculoId { get; set; }
    public Veiculo? Veiculo { get; set; }

    public int ConfiguracaoCombustivelId { get; set; }
    public ConfiguracaoCombustivel? ConfiguracaoCombustivel { get; set; }

    public TipoPlanoCobrancaEnum TipoPlano { get; set; }
    public MarcadorCombustivelEnum MarcadorCombustivel { get; set; }

    public int QuilometragemPercorrida { get; set; }

    public DateTime DataLocacao { get; set; }
    public DateTime DevolucaoPrevista { get; set; }
    public DateTime? DataDevolucao { get; set; }

    public List<Taxa> TaxasSelecionadas { get; set; }

    protected Locacao()
    {
        TaxasSelecionadas = new List<Taxa>();
        DataDevolucao = null;
        MarcadorCombustivel = MarcadorCombustivelEnum.Completo;
    }

    public Locacao(
        int condutorId,
        int veiculoId,
        int configuracaoCombustivelId,
        TipoPlanoCobrancaEnum planoCobranca,
        DateTime dataLocacao,
        DateTime devolucaoPrevista
    ) : this()
    {
        CondutorId = condutorId;
        VeiculoId = veiculoId;
        ConfiguracaoCombustivelId = configuracaoCombustivelId;
        TipoPlano = planoCobranca;
        DataLocacao = dataLocacao;
        DevolucaoPrevista = devolucaoPrevista;
    }

    public void Abrir()
    {
        if (Veiculo is null) return;

        Veiculo.Alugar();
    }

    public void RealizarDevolucao()
    {
        DataDevolucao = DateTime.Now;

        if (Veiculo is null) return;

        Veiculo.Desocupar();
    }

    public bool TemMulta()
    {
        if (DataDevolucao is null)
            return (DateTime.Now - DevolucaoPrevista).Days > 0;

        return (DataDevolucao - DevolucaoPrevista).Value.Days > 0;
    }

    public decimal CalcularValorParcial(PlanoCobranca planoSelecionado)
    {
        var quantidadeDiasPercorridos = ObterQuantidadeDeDiasPercorridos();

        decimal valorPlano = planoSelecionado.CalcularValor(
            quantidadeDiasPercorridos,
            QuilometragemPercorrida,
            TipoPlano
        );

        decimal valorTaxas = 0;

        if (TaxasSelecionadas.Count > 0)
            valorTaxas = TaxasSelecionadas.Sum(tx => tx.CalcularValor(quantidadeDiasPercorridos));

        return valorPlano + valorTaxas;
    }

    public decimal CalcularValorTotal(PlanoCobranca planoCobranca)
    {
        var valorParcial = CalcularValorParcial(planoCobranca);

        decimal totalAbastecimento = 0;

        if (Veiculo is not null && ConfiguracaoCombustivel is not null)
        {
            var valorCombustivel = ConfiguracaoCombustivel.ObterValorCombustivel(Veiculo.TipoCombustivel);

            totalAbastecimento = Veiculo.CalcularLitrosParaAbastecimento(MarcadorCombustivel) * valorCombustivel;
        }

        decimal valorTotal = valorParcial + totalAbastecimento;

        if (TemMulta()) // Multa de 10%
            valorTotal += valorTotal * (10m / 100m);

        return valorTotal;
    }

    private int ObterQuantidadeDeDiasPercorridos()
    {
        int qtdDiasLocacao;

        if (DataDevolucao is null)
            qtdDiasLocacao = (DevolucaoPrevista.Date - DataLocacao.Date).Days;
        else
            qtdDiasLocacao = (DataDevolucao - DataLocacao).Value.Days;

        return qtdDiasLocacao;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (CondutorId == 0)
            erros.Add("O condutor é obrigatório");

        if (VeiculoId == 0)
            erros.Add("O veículo é obrigatório");

        if (DataLocacao == DateTime.MinValue)
            erros.Add("Selecione a data da locação");

        if (DevolucaoPrevista == DateTime.MinValue)
            erros.Add("Selecione a data prevista da entrega");

        if (DevolucaoPrevista < DataLocacao)
            erros.Add("A data prevista da entrega não pode ser menor que data da locação");

        return erros;
    }
}