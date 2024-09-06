using Microsoft.AspNetCore.Identity;

namespace LocadoraDeVeiculos.Dominio.ModuloAutenticacao;

public class Usuario : IdentityUser<int>
{
    public Usuario()
    {
        EmailConfirmed = true;
    }
}