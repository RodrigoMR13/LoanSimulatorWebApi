using Application.Dtos.Requests;
using Application.Dtos.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers
{
    public class GetAllSimulationsHandler(ILoanSimulationRepository repository, ILogger<GetAllSimulationsHandler> logger, IMapper mapper)
        : IRequestHandler<GetAllSimulationsRequest, GetAllSimulationsResponse>
    {
        private readonly ILoanSimulationRepository _repository = repository;
        private readonly ILogger<GetAllSimulationsHandler> _logger = logger;
        private readonly IMapper _mapper = mapper;

        public async Task<GetAllSimulationsResponse> Handle(
            GetAllSimulationsRequest request,
            CancellationToken cancellationToken)
        {
            var totalRecords = await _repository.GetTotalRecordsAsync();
            if (totalRecords == 0) {
                _logger.LogWarning("Nenhuma simulação encontrada no banco de dados.");
                return new GetAllSimulationsResponse
                {
                    Page = request.PaginationRequest.PageNumber,
                    RecordsQty = totalRecords,
                    RecordsQtyPerPage = request.PaginationRequest.RecordsQtyPerPage,
                    Records = []
                };
            }

            var simulations = await _repository.GetAllAsync(request.PaginationRequest);

            var responseRecords = _mapper.Map<List<GetSimulationByIdResponse>>(simulations);

            var response = new GetAllSimulationsResponse
            {
                Page = request.PaginationRequest.PageNumber,
                RecordsQty = totalRecords,
                RecordsQtyPerPage = request.PaginationRequest.RecordsQtyPerPage,
                Records = responseRecords
            };

            return response;
        }
    }
}