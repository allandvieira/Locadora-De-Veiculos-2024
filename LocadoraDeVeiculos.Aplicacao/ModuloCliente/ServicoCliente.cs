using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloCliente;

namespace LocadoraDeVeiculos.Aplicacao.ModuloCliente;

public class ServicoCliente
{
    private readonly IRepositorioCliente repositorioCliente;

    public ServicoCliente(IRepositorioCliente repositorioCliente)
    {
        this.repositorioCliente = repositorioCliente;
    }

    public Result<Cliente> Inserir(Cliente cliente)
    {
        var errosValidacao = cliente.Validar();

        if (errosValidacao.Count > 0)
            return Result.Fail(errosValidacao);

        repositorioCliente.Inserir(cliente);

        return Result.Ok(cliente);
    }

    public Result<Cliente> Editar(Cliente clienteAtualizado)
    {
        var cliente = repositorioCliente.SelecionarPorId(clienteAtualizado.Id);

        if (cliente is null)
            return Result.Fail("O cliente não foi encontrado!");

        var errosValidacao = clienteAtualizado.Validar();

        if (errosValidacao.Count > 0)
            return Result.Fail(errosValidacao);

        cliente.Nome = clienteAtualizado.Nome;
        cliente.Email = clienteAtualizado.Email;
        cliente.Telefone = clienteAtualizado.Telefone;
        cliente.TipoCadastro = clienteAtualizado.TipoCadastro;
        cliente.NumeroDocumento = clienteAtualizado.NumeroDocumento;
        cliente.Cidade = clienteAtualizado.Cidade;
        cliente.Estado = clienteAtualizado.Estado;
        cliente.Bairro = clienteAtualizado.Bairro;
        cliente.Rua = clienteAtualizado.Rua;
        cliente.Numero = clienteAtualizado.Numero;

        repositorioCliente.Editar(cliente);

        return Result.Ok(cliente);
    }

    public Result<Cliente> Excluir(int clienteId)
    {
        var cliente = repositorioCliente.SelecionarPorId(clienteId);

        if (cliente is null)
            return Result.Fail("O cliente não foi encontrado!");

        repositorioCliente.Excluir(cliente);

        return Result.Ok(cliente);
    }

    public Result<Cliente> SelecionarPorId(int clienteId)
    {
        var cliente = repositorioCliente.SelecionarPorId(clienteId);

        if (cliente is null)
            return Result.Fail("O cliente não foi encontrado!");

        return Result.Ok(cliente);
    }

    public Result<List<Cliente>> SelecionarTodos(int empresaId)
    {
        var clientes = repositorioCliente.Filtrar(c => c.EmpresaId == empresaId);

        return Result.Ok(clientes);
    }
}