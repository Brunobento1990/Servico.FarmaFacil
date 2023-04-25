namespace Servico.FarmaFacil.Database.Interfaces
{
    public interface ISincronizarDadosRepository
    {
        Task<bool> Sincronizar();
    }
}
