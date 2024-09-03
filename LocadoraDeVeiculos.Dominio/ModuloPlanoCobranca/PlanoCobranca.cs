using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculos;

namespace LocadoraDeVeiculos.Dominio.ModuloPlanoCobranca;

public class PlanoCobranca : EntidadeBase
{
    public int GrupoVeiculosId { get; set; }
    public GrupoVeiculos? GrupoVeiculos { get; set; }

    public decimal PrecoDiarioPlanoDiario { get; set; }
    public decimal PrecoQuilometroPlanoDiario { get; set; }

    public decimal QuilometrosDisponiveisPlanoControlado { get; set; }
    public decimal PrecoDiarioPlanoControlado { get; set; }
    public decimal PrecoQuilometroExtrapoladoPlanoControlado { get; set; }

    public decimal PrecoDiarioPlanoLivre { get; set; }

    protected PlanoCobranca() { }

    public PlanoCobranca(
        int grupoVeiculosId,
        decimal precoDiarioPlanoDiario,
        decimal precoQuilometroPlanoDiario,
        decimal quilometrosDisponiveisPlanoControlado,
        decimal precoDiarioPlanoControlado,
        decimal precoQuilometroExtrapoladoPlanoControlado,
        decimal precoDiarioPlanoLivre
    )
    {
        GrupoVeiculosId = grupoVeiculosId;

        PrecoDiarioPlanoDiario = precoDiarioPlanoDiario;
        PrecoQuilometroPlanoDiario = precoQuilometroPlanoDiario;

        QuilometrosDisponiveisPlanoControlado = quilometrosDisponiveisPlanoControlado;
        PrecoDiarioPlanoControlado = precoDiarioPlanoControlado;
        PrecoQuilometroExtrapoladoPlanoControlado = precoQuilometroExtrapoladoPlanoControlado;

        PrecoDiarioPlanoLivre = precoDiarioPlanoLivre;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (GrupoVeiculosId == 0)
            erros.Add("O grupo de veículos é obrigatório");

        return erros;
    }
}