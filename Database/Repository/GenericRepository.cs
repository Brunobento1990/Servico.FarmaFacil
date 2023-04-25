using Microsoft.EntityFrameworkCore;
using Npgsql;
using Servico.FarmaFacil.Context;
using Servico.FarmaFacil.Database.Entidades.Servico;
using Servico.FarmaFacil.Database.Interfaces;

namespace Servico.FarmaFacil.Database.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ILogger<Worker> _logger;
        private static string stringConecctionLocal = "User ID=postgres; Password=prixpto; Host=zeus.prismafive.com.br; Port=49282; Database=farmacil-web-compras; Pooling=true;";
        
        protected ContextServico _dbContext;
        public GenericRepository(ContextServico dbContext,
                ILogger<Worker> logger
            )
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<T> AdicionarAsync(T entity)
        {
            try
            {
                _dbContext.Set<T>().Add(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<string> GetCnpj()
        {
            try
            {
                var sql = "select cnpjempresa from data.parametro";

                using (var pgConecction = new NpgsqlConnection(stringConecctionLocal))
                {
                    string? cnpj = string.Empty;

                    NpgsqlCommand comand = new NpgsqlCommand(sql, pgConecction);
                    await pgConecction.OpenAsync();

                    NpgsqlDataReader npgsqlDataReader = await comand.ExecuteReaderAsync();

                    while (await npgsqlDataReader.ReadAsync())
                    {
                        cnpj = npgsqlDataReader["cnpjempresa"].ToString();
                    }

                    if (cnpj is null)
                    {
                        string exMessage = $"Não foi possível localizar o cnpj da empresa para efetuar a primeira sincronização : {DateTime.Now.ToString()}";
                        _logger.LogInformation(exMessage);
                        throw new Exception(exMessage);

                    }

                    pgConecction.Close();

                    return cnpj;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PrimeiraIntegracao> VerificarPrimeiraIntegracao(string cnpj)
        {
            return await _dbContext.Set<PrimeiraIntegracao>()
                .SingleOrDefaultAsync(x => x.Cnpj == cnpj);
        }
    }
}
