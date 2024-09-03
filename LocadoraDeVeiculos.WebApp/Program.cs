using LocadoraDeVeiculos.Aplicacao.ModuloCliente;
using LocadoraDeVeiculos.Aplicacao.ModuloGrupoVeiculos;
using LocadoraDeVeiculos.Aplicacao.ModuloPlanoCobranca;
using LocadoraDeVeiculos.Aplicacao.ModuloTaxa;
using LocadoraDeVeiculos.Aplicacao.ModuloVeiculo;
using LocadoraDeVeiculos.Dominio.ModuloCliente;
using LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculos;
using LocadoraDeVeiculos.Dominio.ModuloPlanoCobranca;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using LocadoraDeVeiculos.Dominio.ModuloVeiculo;
using LocadoraDeVeiculos.Infra.Orm.Compartilhado;
using LocadoraDeVeiculos.Infra.Orm.ModuloCliente;
using LocadoraDeVeiculos.Infra.Orm.ModuloGrupoVeiculos;
using LocadoraDeVeiculos.Infra.Orm.ModuloPlanoCobranca;
using LocadoraDeVeiculos.Infra.Orm.ModuloTaxa;
using LocadoraDeVeiculos.Infra.Orm.ModuloVeiculo;
using LocadoraDeVeiculos.WebApp.Mapping.Resolvers;
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
        builder.Services.AddScoped<IRepositorioTaxa, RepositorioTaxaEmOrm>();
        builder.Services.AddScoped<IRepositorioCliente, RepositorioClienteEmOrm>();

        builder.Services.AddScoped<ServicoGrupoVeiculos>();
        builder.Services.AddScoped<ServicoVeiculo>();
        builder.Services.AddScoped<ServicoPlanoCobranca>();
        builder.Services.AddScoped<ServicoTaxa>();
        builder.Services.AddScoped<ServicoCliente>();

        builder.Services.AddScoped<FotoValueResolver>();
        builder.Services.AddScoped<GrupoVeiculosValueResolver>();

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