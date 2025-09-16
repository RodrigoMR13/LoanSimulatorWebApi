using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("PARCELAS", Schema = "dbo")]
    public class Installment
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Required]
        [Column("CREATED_AT")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedDateTime { get; set; }
        [Required]
        [Column("NUM_PARCELA")]
        public short InstallmentNumber { get; set; }
        [Required]
        [Column("VR_AMORTIZACAO", TypeName = "numeric(18,2)")]
        public decimal AmortizationValue { get; set; }
        [Required]
        [Column("VR_JUROS", TypeName = "numeric(18,2)")]
        public decimal InterestValue { get; set; }
        [Required]
        [Column("VR_PARCELA", TypeName = "numeric(18,2)")]
        public decimal InstallmentValue { get; set; }
        [Required]
        [Column("ID_SIMULACAO")]
        public long SimulationId { get; set; }
        public Simulation Simulation { get; set; } = null!;
    }
}
