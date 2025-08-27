using System.Text.Json.Serialization;

namespace Application.Dtos.Responses
{
    public class GetSimulationsVolumeResponse
    {
        [JsonPropertyName("dataReferencia")]
        public DateOnly ReferenceDate { get; set; }
        [JsonPropertyName("simulacoes")]
        public List<SimulationVolumeDataDto> SimulationsVolumeData { get; set; }
    }
}