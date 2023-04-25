using System.ComponentModel.DataAnnotations;

namespace Servico.FarmaFacil.Database.Entidades.Servico
{
    public class PrimeiraIntegracao
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Cnpj { get; set; } = string.Empty;
        [Required]
        public DateTime DataDaIntegracao { get; set; } = DateTime.Now;
        [Required]
        public Guid EmpresaId { get; set; }
    }
}
