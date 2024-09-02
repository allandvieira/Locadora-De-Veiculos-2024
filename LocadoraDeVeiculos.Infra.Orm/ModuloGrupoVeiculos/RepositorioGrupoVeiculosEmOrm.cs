using LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculos;
using LocadoraDeVeiculos.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDeVeiculos.Infra.Orm.ModuloGrupoVeiculos;

public class RepositorioGrupoVeiculosEmOrm : RepositorioBaseEmOrm<GrupoVeiculos>, IRepositorioGrupoVeiculos
{
    public RepositorioGrupoVeiculosEmOrm(LocadoraDbContext dbContext) : base(dbContext)
    {
    }

    protected override DbSet<GrupoVeiculos> ObterRegistros()
    {
        return dbContext.GruposVeiculos;
    }
}
