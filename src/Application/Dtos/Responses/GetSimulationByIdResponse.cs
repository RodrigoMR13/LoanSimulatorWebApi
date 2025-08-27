using System.Text.Json.Serialization;

namespace Application.Dtos.Responses
{
    public class GetSimulationByIdResponse
    {
        [JsonPropertyName("idSimulacao")]
        public long Id { get; set; }
        [JsonPropertyName("valorDesejado")]
        public decimal Value { get; set; }
        [JsonPropertyName("prazo")]
        public int Installments { get; set; }
        [JsonPropertyName("valorTotalParcelas")]
        public decimal TotalValueInstallments { get; set; }
    }
}