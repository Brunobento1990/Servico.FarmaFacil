using Microsoft.EntityFrameworkCore;
using Servico.FarmaFacil;
using Servico.FarmaFacil.Context;
using Servico.FarmaFacil.Database.Interfaces;
using Servico.FarmaFacil.Database.Repository;
using Servico.FarmaFacil.Service.Interfaces;
using Servico.FarmaFacil.Service.Services;

var configBuilder = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json");

var configuration = configBuilder.Build();


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        var conectionString = "server=localhost;database=SincronizadorFarmaFacil;user=root;password=BmLC9621@?";
        services.AddDbContext<ContextServico>(opt => opt.UseMySql(conectionString, ServerVersion.AutoDetect(conectionString)));
        
        services.AddScoped<ISincronizadorService, SincronizadorService>();
        services.AddScoped<IBuscarDadosEmpresaApi, BuscarDadosEmpresaApi>();

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ISincronizarDadosRepository, SincronizarDadosRepository>();
        services.AddScoped<ISincronizarPrimeirosDadosRepository, SincronizarPrimeirosDadosRepository>();

        services.AddScoped<IBuscarDadosEmpresaApi, BuscarDadosEmpresaApi>();
        services.AddHttpClient<IBuscarDadosEmpresaApi, BuscarDadosEmpresaApi>(s => s.BaseAddress = new Uri(configuration["ServiceUrls:DadosEmpresa"]));

    })
    .Build();

await host.RunAsync();