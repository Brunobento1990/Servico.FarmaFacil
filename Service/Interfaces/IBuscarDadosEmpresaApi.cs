using Servico.FarmaFacil.Database.Entidades.Servico;

namespace Servico.FarmaFacil.Service.Interfaces
{
    public interface IBuscarDadosEmpresaApi
    {
        Task<PrimeiraIntegracao> BuscarDadosApi(string cnpj);
    }
}
