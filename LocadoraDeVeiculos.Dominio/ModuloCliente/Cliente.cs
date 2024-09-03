using LocadoraDeVeiculos.Dominio.Compartilhado;
using System.Net.Mail;

namespace LocadoraDeVeiculos.Dominio.ModuloCliente;

public class Cliente : EntidadeBase
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }

    public TipoCadastroClienteEnum TipoCadastro { get; set; }
    public string NumeroDocumento { get; set; }

    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string Bairro { get; set; }
    public string Rua { get; set; }
    public string Numero { get; set; }

    protected Cliente() { }

    public Cliente(
        string nome,
        string email,
        string telefone,
        TipoCadastroClienteEnum tipoCadastro,
        string numeroDocumento,
        string cidade,
        string estado,
        string bairro,
        string rua,
        string numero
    ) : this()
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;

        TipoCadastro = tipoCadastro;
        NumeroDocumento = numeroDocumento;

        Cidade = cidade;
        Estado = estado;
        Bairro = bairro;
        Rua = rua;
        Numero = numero;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O nome é obrigatório");

        if (string.IsNullOrWhiteSpace(Email))
            erros.Add("O email é obrigatório");

        else if (MailAddress.TryCreate(Email, out _) is false)
            erros.Add("O email deve seguir um padrão válido");

        if (string.IsNullOrWhiteSpace(Telefone))
            erros.Add("O telefone é obrigatório");

        if (string.IsNullOrWhiteSpace(NumeroDocumento))
            erros.Add("O número do documento é obrigatório");

        if (string.IsNullOrWhiteSpace(Cidade))
            erros.Add("A cidade é obrigatória");

        if (string.IsNullOrWhiteSpace(Estado))
            erros.Add("O estado é obrigatório");

        if (string.IsNullOrWhiteSpace(Bairro))
            erros.Add("O bairro é obrigatório");

        if (string.IsNullOrWhiteSpace(Rua))
            erros.Add("A rua é obrigatória");

        if (string.IsNullOrWhiteSpace(Numero))
            erros.Add("O número é obrigatório");

        return erros;
    }
}