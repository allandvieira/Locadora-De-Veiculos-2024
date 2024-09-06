using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using Microsoft.AspNetCore.Identity;
using System.Security.Authentication;
using System.Security.Claims;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAutenticacao;

public class ServicoAutenticacao
{
    private readonly UserManager<Usuario> userManager;
    private readonly SignInManager<Usuario> signInManager;
    private readonly RoleManager<Perfil> roleManager;

    private readonly IRepositorioFuncionario repositorioFuncionario;

    public ServicoAutenticacao(
        UserManager<Usuario> userManager,
        SignInManager<Usuario> signInManager,
        RoleManager<Perfil> roleManager,
        IRepositorioFuncionario repositorioFuncionario
    )
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
        this.repositorioFuncionario = repositorioFuncionario;
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

    public async Task<int?> ObterIdEmpresaAsync(ClaimsPrincipal claim)
    {
        var usuario = await userManager.GetUserAsync(claim);

        var perfilSelecionado = TipoUsuarioEnum.Funcionario.ToString();

        if (claim.IsInRole(perfilSelecionado))
        {
            var funcionario = repositorioFuncionario
                .SelecionarPorId(f => f.UsuarioId == usuario!.Id);

            if (funcionario is null)
                throw new AuthenticationException("Não foi possível selecionar o funcionário registrado!");

            return funcionario.EmpresaId;
        }

        return usuario?.Id;
    }
}