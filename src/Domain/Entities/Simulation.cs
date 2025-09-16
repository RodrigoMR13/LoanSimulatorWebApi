using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("SIMULACOES", Schema = "dbo")]
    public class Simulation
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Required]
        [Column("CREATED_AT")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedDateTime { get; set; }
        [Required]
        [Column("TIPO_AMORTIZACAO", TypeName = "varchar(80)")]
        public AmortizationMethodsEnum SimulationType { get; set; }
        [Required]
        [Column("ID_SIMULACAO_EMPRESTIMO")]
        public long LoanSimulationId { get; set; }
        public LoanSimulation LoanSimulation { get; set; } = null!;
        public ICollection<Installment> Installments { get; set; } = [];
    }
}
