using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using System.Net.Mail;

namespace LocadoraDeVeiculos.Dominio.ModuloCondutor;

public class Condutor : EntidadeBase
{
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }
    public bool ClienteCondutor { get; set; }

    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string CPF { get; set; }
    public string CNH { get; set; }
    public DateTime ValidadeCNH { get; set; }

    protected Condutor() { }

    public Condutor(
        int clienteId,
        bool clienteCondutor,
        string nome,
        string email,
        string telefone,
        string cpf,
        string cnh,
        DateTime validadeCnh
    ) : this()
    {
        ClienteId = clienteId;
        ClienteCondutor = clienteCondutor;
        Nome = nome;
        Email = email;
        Telefone = telefone;
        CPF = cpf;
        CNH = cnh;
        ValidadeCNH = validadeCnh;
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

        if (string.IsNullOrWhiteSpace(CPF))
            erros.Add("O número do CPF é obrigatório");

        if (string.IsNullOrWhiteSpace(CNH))
            erros.Add("O número da CNH é obrigatório");

        if (ValidadeCNH < DateTime.Today)
            erros.Add("A CNH está vencida");

        return erros;
    }
}