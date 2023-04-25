using Servico.FarmaFacil.Database.Entidades.Servico;

namespace Servico.FarmaFacil.Database.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<T> AdicionarAsync(T entity);
        Task<string> GetCnpj();
        Task<PrimeiraIntegracao> VerificarPrimeiraIntegracao(string cnpj);
    }
}
