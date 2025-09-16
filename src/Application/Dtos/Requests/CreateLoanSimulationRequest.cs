using Application.Dtos.Responses;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Dtos.Requests
{
    public class CreateLoanSimulationRequest : IRequest<CreateLoanSimulationResponse>
    {
        [JsonPropertyName("valorDesejado")]
        public decimal Value { get; set; }
        [JsonPropertyName("prazo")]
        public int Period { get; set; }
    }
}
