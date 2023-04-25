namespace Servico.FarmaFacil.Database.Interfaces
{
    public interface ISincronizarPrimeirosDadosRepository
    {
        Task<bool> Sincronizar();
    }
}
