using Domain.Enums;
using System.Text.Json.Serialization;

namespace Application.Dtos
{
    public class SimulationDto
    {
        [JsonPropertyName("tipo")]
        public AmortizationMethodsEnum SimulationType { get; set; }
        [JsonPropertyName("parcelas")]
        public List<InstallmentsDto> Installments { get; set; }
    }
}
