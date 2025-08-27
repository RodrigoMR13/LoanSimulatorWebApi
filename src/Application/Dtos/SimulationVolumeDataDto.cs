using Domain.Enums;
using System.Text.Json.Serialization;

namespace Application.Dtos
{
    public class SimulationVolumeDataDto
    {
        [JsonPropertyName("codigoProduto")]
        public long ProductId { get; set; }
        [JsonPropertyName("descricaoProduto")]
        public string ProductDescription { get; set; }
        [JsonPropertyName("taxaMediaJuro")]
        public decimal MeanInterestRate { get; set; }
        [JsonPropertyName("valorMedioPrestacao")]
        public decimal MeanInstallmentValue { get; set; }
        [JsonPropertyName("valorTotalDesejado")]
        public decimal TotalValue { get; set; }
        [JsonPropertyName("valorTotalCredito")]
        public decimal TotalValueInstallments { get; set; }
    }
}
