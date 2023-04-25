using Microsoft.EntityFrameworkCore;
using Servico.FarmaFacil.Database.Entidades.Servico;

namespace Servico.FarmaFacil.Context
{
    public class ContextServico : DbContext
    {
        public ContextServico(DbContextOptions<ContextServico> options) : base(options)
        {
        }

        public DbSet<PrimeiraIntegracao> PrimeiraIntegracao { get; set; }
    }
}
