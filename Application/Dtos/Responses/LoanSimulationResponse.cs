using System.Text.Json.Serialization;

namespace Application.Dtos.Responses
{
    public class LoanSimulationResponse(long simulationId, int productId, string productName,
        decimal interestRate, List<SimulationDto> simulationResult)
    {
        [JsonPropertyName("idSimulacao")]
        public long SimulationId { get; set; } = simulationId;
        [JsonPropertyName("codigoProduto")]
        public int ProductId { get; set; } = productId;
        [JsonPropertyName("descricaoProduto")]
        public string ProductName { get; set; } = productName;
        [JsonPropertyName("taxaJuros")]
        public decimal InterestRate { get; set; } = interestRate;
        [JsonPropertyName("resultadoSimulacao")]
        public List<SimulationDto> SimulationResult { get; set; } = simulationResult;
    }
}
