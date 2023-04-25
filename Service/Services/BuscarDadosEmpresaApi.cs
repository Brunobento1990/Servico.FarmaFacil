using Servico.FarmaFacil.Database.Entidades.Servico;
using Servico.FarmaFacil.Service.Interfaces;
using System.Configuration;
using System.Text.Json;

namespace Servico.FarmaFacil.Service.Services
{
    public class BuscarDadosEmpresaApi : IBuscarDadosEmpresaApi
    {
        private HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public BuscarDadosEmpresaApi(HttpClient httpClient,
                IConfiguration configuration
            )
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }
        public async Task<PrimeiraIntegracao> BuscarDadosApi(string cnpj)
        {
            var token = new CreateToken(_configuration);
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.GenerateToken(cnpj)}");

            var resp = _httpClient.GetStringAsync("RetornaEmpresaProCnpj");
            resp.Wait();

            return await Task.Run(() => JsonSerializer.Deserialize<PrimeiraIntegracao>(resp.Result));
        }
    }
}
