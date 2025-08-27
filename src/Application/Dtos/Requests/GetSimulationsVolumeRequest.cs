using System.Text.Json.Serialization;
using Application.Dtos.Responses;
using MediatR;

namespace Application.Dtos.Requests
{
    public class GetSimulationsVolumeRequest : IRequest<GetSimulationsVolumeResponse>
    {
        [JsonPropertyName("dataReferencia")]
        public DateOnly ReferenceDate { get; set; }
    }
}

