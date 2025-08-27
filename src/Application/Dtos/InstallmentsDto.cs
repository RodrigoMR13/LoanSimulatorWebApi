using System.Text.Json.Serialization;

namespace Application.Dtos
{
    public class InstallmentsDto(short installmentNumber, decimal amortizationValue, decimal interestValue, decimal installmentValue)
    {
        [JsonPropertyName("numero")]
        public short InstallmentNumber { get; set; } = installmentNumber;
        [JsonPropertyName("valorAmortizacao")]
        public decimal AmortizationValue { get; set; } = amortizationValue;
        [JsonPropertyName("valorJuros")]
        public decimal InterestValue { get; set; } = interestValue;
        [JsonPropertyName("valorPrestacao")]
        public decimal InstallmentValue { get; set; } = installmentValue;
    }
}
