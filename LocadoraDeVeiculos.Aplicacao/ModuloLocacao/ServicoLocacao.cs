using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloCombustivel;
using LocadoraDeVeiculos.Dominio.ModuloLocacao;
using LocadoraDeVeiculos.Dominio.ModuloVeiculo;

namespace LocadoraDeVeiculos.Aplicacao.ModuloLocacao;

public class ServicoLocacao
{
    private readonly IRepositorioLocacao repositorioLocacao;
    private readonly IRepositorioConfiguracaoCombustivel repositorioCombustivel;
    private readonly IRepositorioVeiculo repositorioVeiculo;

    public ServicoLocacao(
        IRepositorioLocacao repositorioLocacao,
        IRepositorioConfiguracaoCombustivel repositorioCombustivel,
        IRepositorioVeiculo repositorioVeiculo
    )
    {
        this.repositorioLocacao = repositorioLocacao;
        this.repositorioCombustivel = repositorioCombustivel;
        this.repositorioVeiculo = repositorioVeiculo;
    }

    public Result<Locacao> Inserir(Locacao locacao)
    {
        var config = repositorioCombustivel.ObterConfiguracao(locacao.EmpresaId);

        if (config is null)
            return Result.Fail("Não foi possível obter a configuração de valores de combustíveis.");

        locacao.ConfiguracaoCombustivelId = config.Id;

        var erros = locacao.Validar();

        if (erros.Count > 0)
            return Result.Fail(erros);

        AbrirLocacao(locacao);

        repositorioLocacao.Inserir(locacao);

        return Result.Ok(locacao);
    }

    public Result<Locacao> RealizarDevolucao(Locacao locacaoParaDevolucao)
    {
        if (locacaoParaDevolucao.DataDevolucao is not null)
            return Result.Fail("A devolução já foi realizada!");

        FecharLocacao(locacaoParaDevolucao);

        repositorioLocacao.Editar(locacaoParaDevolucao);

        return Result.Ok(locacaoParaDevolucao);
    }

    public Result<Locacao> Editar(Locacao locacaoAtualizada)
    {
        var locacao = repositorioLocacao.SelecionarPorId(locacaoAtualizada.Id);

        if (locacao is null)
            return Result.Fail("A locação não foi encontrada!");

        if (locacao.DataDevolucao is not null)
            return Result.Fail("A devolução já foi realizada!");

        var erros = locacaoAtualizada.Validar();

        if (erros.Count > 0)
            return Result.Fail(erros);

        locacao.Veiculo!.Desocupar();

        locacao.VeiculoId = locacaoAtualizada.VeiculoId;
        locacao.CondutorId = locacaoAtualizada.CondutorId;
        locacao.ConfiguracaoCombustivelId = locacaoAtualizada.ConfiguracaoCombustivelId;
        locacao.TipoPlano = locacaoAtualizada.TipoPlano;
        locacao.MarcadorCombustivel = locacaoAtualizada.MarcadorCombustivel;
        locacao.QuilometragemPercorrida = locacaoAtualizada.QuilometragemPercorrida;
        locacao.DataLocacao = locacaoAtualizada.DataLocacao;
        locacao.DevolucaoPrevista = locacaoAtualizada.DevolucaoPrevista;
        locacao.DataDevolucao = locacaoAtualizada.DataDevolucao;
        locacao.TaxasSelecionadas = locacaoAtualizada.TaxasSelecionadas;

        repositorioLocacao.Editar(locacao);

        return Result.Ok(locacao);
    }

    public Result<Locacao> Excluir(int locacaoId)
    {
        var locacao = repositorioLocacao.SelecionarPorId(locacaoId);

        if (locacao is null)
            return Result.Fail("A locação não foi encontrada!");

        repositorioLocacao.Excluir(locacao);

        return Result.Ok(locacao);
    }

    public Result<Locacao> SelecionarPorId(int locacaoId)
    {
        var locacao = repositorioLocacao.SelecionarPorId(locacaoId);

        if (locacao is null)
            return Result.Fail("A locação não foi encontrada!");

        return Result.Ok(locacao);
    }

    public Result<List<Locacao>> SelecionarTodos(int empresaId)
    {
        var locacoes = repositorioLocacao.Filtrar(l => l.EmpresaId == empresaId);

        return Result.Ok(locacoes);
    }

    private void AbrirLocacao(Locacao locacao)
    {
        var veiculoSelecionado = repositorioVeiculo.SelecionarPorId(locacao.VeiculoId);

        locacao.Veiculo = veiculoSelecionado;

        locacao.Abrir();

        repositorioVeiculo.Editar(locacao.Veiculo!);
    }

    private void FecharLocacao(Locacao locacao)
    {
        var veiculoSelecionado = repositorioVeiculo.SelecionarPorId(locacao.VeiculoId);

        locacao.Veiculo = veiculoSelecionado;

        locacao.RealizarDevolucao();

        repositorioVeiculo.Editar(locacao.Veiculo!);
    }
}