namespace LocadoraDeVeiculos.Dominio.ModuloCombustivel;

public interface IRepositorioConfiguracaoCombustivel
{
    void GravarConfiguracao(ConfiguracaoCombustivel configuracaoCombustivel);
    ConfiguracaoCombustivel? ObterConfiguracao();
}