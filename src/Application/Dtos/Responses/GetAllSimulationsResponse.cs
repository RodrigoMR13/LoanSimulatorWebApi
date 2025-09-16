using System.Text.Json.Serialization;

namespace Application.Dtos.Responses
{
    public class GetAllSimulationsResponse
    {
        [JsonPropertyName("pagina")]
        public int Page { get; set; }
        [JsonPropertyName("qtdRegistros")]
        public int RecordsQty { get; set; }
        [JsonPropertyName("qtdRegistrosPagina")]
        public int RecordsQtyPerPage { get; set; }
        [JsonPropertyName("registros")]
        public List<GetSimulationByIdResponse> Records { get; set; }
    }
}