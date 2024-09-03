using LocadoraDeVeiculos.Aplicacao.ModuloGrupoVeiculos;
using LocadoraDeVeiculos.Aplicacao.ModuloPlanoCobranca;
using LocadoraDeVeiculos.Aplicacao.ModuloVeiculo;
using LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculos;
using LocadoraDeVeiculos.Dominio.ModuloPlanoCobranca;
using LocadoraDeVeiculos.Dominio.ModuloVeiculo;
using LocadoraDeVeiculos.Infra.Orm.Compartilhado;
using LocadoraDeVeiculos.Infra.Orm.ModuloGrupoVeiculos;
using LocadoraDeVeiculos.Infra.Orm.ModuloPlanoCobranca;
using LocadoraDeVeiculos.Infra.Orm.ModuloVeiculo;
using LocadoraDeVeiculos.WebApp.Mapping;
using System.Reflection;

namespace LocadoraDeVeiculos.WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<LocadoraDbContext>();

        builder.Services.AddScoped<IRepositorioGrupoVeiculos, RepositorioGrupoVeiculosEmOrm>();
        builder.Services.AddScoped<IRepositorioVeiculo, RepositorioVeiculoEmOrm>();
        builder.Services.AddScoped<IRepositorioPlanoCobranca, RepositorioPlanoCobrancaEmOrm>();

        builder.Services.AddScoped<ServicoGrupoVeiculos>();
        builder.Services.AddScoped<ServicoVeiculo>();
        builder.Services.AddScoped<ServicoPlanoCobranca>();

        builder.Services.AddScoped<FotoValueResolver>();

        builder.Services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(Assembly.GetExecutingAssembly());
        });

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            // Faz com que a aplicação permita apenas conexões HTTPS em navegadores suportados
            app.UseHsts();
        }

        // Redireciona requisições HTTP para HTTPS
        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}