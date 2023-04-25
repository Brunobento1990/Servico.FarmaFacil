using Servico.FarmaFacil.Database.Entidades.Servico;
using Servico.FarmaFacil.Database.Interfaces;
using Servico.FarmaFacil.Service.Interfaces;

namespace Servico.FarmaFacil
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _provider;
        private readonly IGenericRepository<PrimeiraIntegracao> _genericRepository;

        public Worker(ILogger<Worker> logger,
            IServiceProvider provider
            )
        {
            _provider = provider;
            _logger = logger;
        }

        public async Task DoSomething()
        {
            using (var scope = _provider.CreateScope())
            {
                var sincronizadorService = scope.ServiceProvider.GetRequiredService<ISincronizadorService>();
                await sincronizadorService.IniciarSincronizacao();

            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var timer = new Timer(async state =>
            {
                _logger.LogInformation("Executando tarefa...");

                _ = DoSomething();

            }, null, TimeSpan.Zero, TimeSpan.FromMinutes(0.5));

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}