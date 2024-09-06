using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDeVeiculos.Infra.Orm.ModuloCliente;

public class RepositorioClienteEmOrm : RepositorioBaseEmOrm<Cliente>, IRepositorioCliente
{
    public RepositorioClienteEmOrm(LocadoraDbContext dbContext) : base(dbContext)
    {
    }

    protected override DbSet<Cliente> ObterRegistros()
    {
        return dbContext.Clientes;
    }

    public List<Cliente> Filtrar(Func<Cliente, bool> predicate)
    {
        return dbContext.Clientes
            .Where(predicate)
            .ToList();
    }
}