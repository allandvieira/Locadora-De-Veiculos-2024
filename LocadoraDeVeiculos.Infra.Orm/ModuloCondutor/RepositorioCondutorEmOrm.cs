using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using LocadoraDeVeiculos.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDeVeiculos.Infra.Orm.ModuloCondutor;

public class RepositorioCondutorEmOrm : RepositorioBaseEmOrm<Condutor>, IRepositorioCondutor
{
    public RepositorioCondutorEmOrm(LocadoraDbContext dbContext) : base(dbContext)
    {
    }

    protected override DbSet<Condutor> ObterRegistros()
    {
        return dbContext.Condutores;
    }

    public override Condutor? SelecionarPorId(int id)
    {
        return ObterRegistros()
            .Include(c => c.Cliente)
            .FirstOrDefault(c => c.Id == id);
    }

    public override List<Condutor> SelecionarTodos()
    {
        return ObterRegistros()
            .Include(c => c.Cliente)
            .ToList();
    }
}