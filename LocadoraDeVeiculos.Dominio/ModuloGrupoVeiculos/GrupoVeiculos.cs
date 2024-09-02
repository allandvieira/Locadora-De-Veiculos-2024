using LocadoraDeVeiculos.Dominio.Compartilhado;

namespace LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculos;

public class GrupoVeiculos : EntidadeBase
{
    protected GrupoVeiculos()
    {
    }

    public GrupoVeiculos(string nome)
    {
        Nome = nome;
    }

    public string Nome { get; set; }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (Nome.Length < 3)
            erros.Add("O nome é obrigatório");

        return erros;
    }
}
