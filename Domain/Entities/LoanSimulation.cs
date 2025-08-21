using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("SIMULACAO_EMPRESTIMO", Schema = "dbo")]
    public class LoanSimulation
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Required]
        [Column("CREATE_DATETIME")]
        public DateTime CreatedDate { get; set; }
        [Required]
        [Column("ID_PRODUTO")]
        public int ProductId { get; set; }
        [Required]
        [Column("NO_PRODUTO", TypeName = "varchar(200)")]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        [Column("TAXA_JUROS", TypeName = "numeric(10,9)")]
        public decimal InterestRate { get; set; }
        public ICollection<Simulation> Simulations { get; set; } = [];
    }
}
