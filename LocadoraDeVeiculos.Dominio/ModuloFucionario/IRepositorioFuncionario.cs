namespace LocadoraDeVeiculos.Dominio.ModuloFuncionario;

public interface IRepositorioFuncionario
{
    void Inserir(Funcionario funcionario);
    void Editar(Funcionario funcionario);
    void Excluir(Funcionario funcionario);
    Funcionario? SelecionarPorId(Func<Funcionario, bool> predicate);
    List<Funcionario> SelecionarTodos(Func<Funcionario, bool> predicate);
}