using Servico.FarmaFacil.Database.Entidades.Servico;
using Servico.FarmaFacil.Database.Interfaces;
using Servico.FarmaFacil.Service.Interfaces;

namespace Servico.FarmaFacil.Service.Services
{
    public class SincronizadorService : ISincronizadorService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IGenericRepository<PrimeiraIntegracao> _genericRepository;
        private readonly IBuscarDadosEmpresaApi _buscarDadosEmpresaApi;
        private readonly ISincronizarDadosRepository _sincronizarDadosRepository;
        private readonly ISincronizarPrimeirosDadosRepository _sincronizarPrimeirosDadosRepository;
        public SincronizadorService(ILogger<Worker> logger,
            IGenericRepository<PrimeiraIntegracao> genericRepository,
            IBuscarDadosEmpresaApi buscarDadosEmpresaApi,
            ISincronizarDadosRepository sincronizarDadosRepository,
            ISincronizarPrimeirosDadosRepository sincronizarPrimeirosDadosRepository
            )
        {
            _sincronizarDadosRepository = sincronizarDadosRepository;
            _sincronizarPrimeirosDadosRepository = sincronizarPrimeirosDadosRepository;
            _buscarDadosEmpresaApi = buscarDadosEmpresaApi; 
            _genericRepository = genericRepository;
            _logger = logger;
        }

        public async Task IniciarSincronizacao()
        {
            try
            {
                var cnpj = await _genericRepository.GetCnpj();

                var primeiraSincronizacao = await _genericRepository.VerificarPrimeiraIntegracao(cnpj);

                if(primeiraSincronizacao is null)
                {
                    var dadosDaEmpresaApi = await _buscarDadosEmpresaApi.BuscarDadosApi(cnpj);

                    if (dadosDaEmpresaApi is null) 
                        throw new Exception($"Não foi possível retornar os dados da empresa na api com cnpj :{cnpj}");

                    await _genericRepository.AdicionarAsync(dadosDaEmpresaApi);

                    await _sincronizarPrimeirosDadosRepository.Sincronizar();
                }
                else
                {
                    await _sincronizarDadosRepository.Sincronizar();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
