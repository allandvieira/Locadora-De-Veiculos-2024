using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using LocadoraDeVeiculos.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDeVeiculos.Infra.Orm.ModuloFuncionario;

public class RepositorioFuncionarioEmOrm : RepositorioBaseEmOrm<Funcionario>, IRepositorioFuncionario
{
    public RepositorioFuncionarioEmOrm(LocadoraDbContext dbContext) : base(dbContext)
    {
    }

    protected override DbSet<Funcionario> ObterRegistros()
    {
        return dbContext.Funcionarios;
    }

    public override Funcionario? SelecionarPorId(int funcionarioId)
    {
        return dbContext.Funcionarios
            .Include(u => u.Empresa)
            .FirstOrDefault(f => f.Id == funcionarioId);
    }

    public Funcionario? SelecionarPorId(Func<Funcionario, bool> predicate)
    {
        return dbContext.Funcionarios
            .Include(u => u.Empresa)
            .FirstOrDefault(predicate);
    }

    public List<Funcionario> SelecionarTodos(Func<Funcionario, bool> predicate)
    {
        return dbContext.Funcionarios
            .Include(u => u.Empresa)
            .Where(predicate)
            .ToList();
    }
}