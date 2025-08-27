using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    [Table("PRODUTO", Schema = "dbo")]
    public class Product
    {
        [Key]
        [Column("CO_PRODUTO")]
        public int Id { get; set; }

        [Required]
        [Column("NO_PRODUTO", TypeName = "varchar(200)")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column("PC_TAXA_JUROS", TypeName = "numeric(10,9)")]
        public decimal InterestRate { get; set; }

        [Required]
        [Column("NU_MINIMO_MESES")]
        public short MinMonth { get; set; }

        [Column("NU_MAXIMO_MESES")]
        public short? MaxMonth { get; set; }

        [Required]
        [Column("VR_MINIMO", TypeName = "numeric(18,2)")]
        public decimal MinValue { get; set; }

        [Column("VR_MAXIMO", TypeName = "numeric(18,2)")]
        public decimal? MaxValue { get; set; }
    }
}
