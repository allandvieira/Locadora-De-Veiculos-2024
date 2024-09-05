using System.ComponentModel.DataAnnotations;

namespace LocadoraDeVeiculos.Dominio.ModuloAutenticacao;

public enum TipoUsuarioEnum
{
    Empresa,
    [Display(Name = "Funcionário")] Funcionario
}