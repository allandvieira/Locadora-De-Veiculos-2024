using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using Microsoft.AspNetCore.Identity;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAutenticacao;

public class ServicoAutenticacao
{
    private readonly UserManager<Usuario> userManager;
    private readonly SignInManager<Usuario> signInManager;
    private readonly RoleManager<Perfil> roleManager;

    public ServicoAutenticacao(
        UserManager<Usuario> userManager,
        SignInManager<Usuario> signInManager,
        RoleManager<Perfil> roleManager
    )
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
    }

    public async Task<Result<Usuario>> Registrar(
        Usuario usuario, string senha, TipoUsuarioEnum tipoUsuario)
    {
        var resultadoCriacaoUsuario = await userManager.CreateAsync(usuario, senha);

        var tipoUsuarioStr = tipoUsuario.ToString();

        var resultadoBuscaTipoUsuario = await roleManager.FindByNameAsync(tipoUsuarioStr);

        if (resultadoBuscaTipoUsuario is null)
        {
            var perfil = new Perfil()
            {
                Name = tipoUsuarioStr,
                NormalizedName = tipoUsuarioStr.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            await roleManager.CreateAsync(perfil);
        }

        await userManager.AddToRoleAsync(usuario, tipoUsuarioStr);

        if (resultadoCriacaoUsuario.Succeeded && tipoUsuario == TipoUsuarioEnum.Empresa)
        {
            await signInManager.SignInAsync(usuario, isPersistent: false);
        }
        else if (!resultadoCriacaoUsuario.Succeeded)
        {
            var erros = resultadoCriacaoUsuario.Errors.Select(s => s.Description);

            return Result.Fail(erros);
        }

        return Result.Ok(usuario);
    }

    public async Task<Result> Login(string usuario, string senha)
    {
        var resultadoLogin = await signInManager.PasswordSignInAsync(
            usuario,
            senha,
            false,
            false
        );

        if (!resultadoLogin.Succeeded)
            return Result.Fail("Login ou senha incorretos");

        return Result.Ok();
    }

    public async Task<Result> Logout()
    {
        await signInManager.SignOutAsync();

        return Result.Ok();
    }
}