using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers
{
    public class GetAllSimulationsHandler(ILoanSimulationRepository repository, ILogger<GetAllSimulationsHandler> logger) : IRequestHandler<GetAllSimulationsRequest, GetAllSimulationsResponse>
    {
        private readonly ILoanSimulationRepository _repository = repository;
        private readonly ILogger<GetAllSimulationsHandler> _logger = logger;

        public async Task<GetAllSimulationsResponse> Handle(
            GetAllSimulationsRequest request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Obtendo o número de simulações no Banco de Dados");
            var totalRecords = await _repository.GetTotalRecords();
            _logger.LogInformation("Obtendo todas as simulações no Banco de Dados...");
            var simulations = await _repository.GetAllAsync(request.PaginationRequest);
            var responseRecords = simulations.Select(simulation => new GetSimulationByIdResponse
            {
                Id = simulation.Id,
                Value = simulation.Simulations.First().Installments.Sum(i => i.AmortizationValue + i.InterestValue),
                Installments = simulation.Simulations.First().Installments.Count,
                TotalValueInstallments = simulation.Simulations.SelectMany(s => s.Installments).Sum(i => i.InstallmentValue)
            }).ToList();

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