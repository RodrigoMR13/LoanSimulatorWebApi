using Application.Dtos.Responses;
using Domain.Common;
using MediatR;

namespace Application.Dtos.Requests
{
    public class GetAllSimulationsRequest(PaginationRequest paginationRequest) : IRequest<GetAllSimulationsResponse>
    {
        public PaginationRequest PaginationRequest { get; set; } = paginationRequest;
    }
}

