using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using LocadoraDeVeiculos.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDeVeiculos.Infra.Orm.ModuloTaxa;

public class RepositorioTaxaEmOrm : RepositorioBaseEmOrm<Taxa>, IRepositorioTaxa
{
    public RepositorioTaxaEmOrm(LocadoraDbContext dbContext) : base(dbContext)
    {
    }

    protected override DbSet<Taxa> ObterRegistros()
    {
        return dbContext.Taxas;
    }
}