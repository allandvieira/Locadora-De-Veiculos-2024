using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using LocadoraDeVeiculos.Dominio.ModuloVeiculo;

namespace LocadoraDeVeiculos.Dominio.ModuloCombustivel;

public class ConfiguracaoCombustivel
{
    public int Id { get; set; }
    public DateTime DataCriacao { get; set; }

    public decimal ValorGasolina { get; set; }
    public decimal ValorGas { get; set; }
    public decimal ValorDiesel { get; set; }
    public decimal ValorAlcool { get; set; }

    public int EmpresaId { get; set; }
    public Usuario? Empresa { get; set; }

    protected ConfiguracaoCombustivel() { }

    public ConfiguracaoCombustivel(
        decimal valorGasolina,
        decimal valorGas,
        decimal valorDiesel,
        decimal valorAlcool
    ) : this()
    {
        ValorGasolina = valorGasolina;
        ValorGas = valorGas;
        ValorDiesel = valorDiesel;
        ValorAlcool = valorAlcool;
    }

    public decimal ObterValorCombustivel(TipoCombustivelEnum tipoCombustivel)
    {
        return tipoCombustivel switch
        {
            TipoCombustivelEnum.Alcool => ValorAlcool,
            TipoCombustivelEnum.Diesel => ValorDiesel,
            TipoCombustivelEnum.Gas => ValorGas,
            _ => ValorGasolina
        };
    }
}