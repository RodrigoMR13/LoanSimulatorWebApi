using Application.Dtos;
using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers
{
    public class GetSimulationsVolumeHandler(ILoanSimulationRepository repository, ILogger<GetSimulationsVolumeHandler> logger)
        : IRequestHandler<GetSimulationsVolumeRequest, GetSimulationsVolumeResponse>
    {
        private readonly ILoanSimulationRepository _repository = repository;
        private readonly ILogger<GetSimulationsVolumeHandler> _logger = logger;

        public async Task<GetSimulationsVolumeResponse> Handle(
            GetSimulationsVolumeRequest request,
            CancellationToken cancellationToken)
        {
            int totalRecords = await _repository.GetTotalRecordsByDateAsync(request.ReferenceDate);
            if (totalRecords == 0)
            {
                _logger.LogWarning($"Nenhuma simulação encontrada no banco de dados para a data {request.ReferenceDate}.");
                return new GetSimulationsVolumeResponse
                {
                    ReferenceDate = request.ReferenceDate,
                    SimulationsVolumeData = []
                };
            }

            var simulations = await _repository.GetByDateAsync(request.ReferenceDate);

            var simulationsVolume = CalcSimulationsVolume(simulations);

            var response = new GetSimulationsVolumeResponse
            {
                ReferenceDate = request.ReferenceDate,
                SimulationsVolumeData = simulationsVolume
            };

            return response;
        }

        private List<SimulationVolumeDataDto> CalcSimulationsVolume(IEnumerable<LoanSimulation> simulations)
        {
            _logger.LogInformation("Agrupando simulações e realizando cálculos...");
            var simulationsVolume = simulations
                .GroupBy(sim => new { sim.ProductId, sim.ProductName })
                .Select(group => new SimulationVolumeDataDto
                {
                    ProductId = group.Key.ProductId,
                    ProductDescription = group.Key.ProductName,
                    MeanInterestRate = group
                        .SelectMany(sim => sim.Simulations)
                        .SelectMany(s => s.Installments)
                        .DefaultIfEmpty()
                        .Average(i => i != null ? i.InterestValue : 0),
                    MeanInstallmentValue = group
                        .SelectMany(sim => sim.Simulations)
                        .SelectMany(s => s.Installments)
                        .DefaultIfEmpty()
                        .Average(i => i != null ? i.InstallmentValue : 0),
                    TotalValue = group
                        .SelectMany(sim => sim.Simulations)
                        .SelectMany(s => s.Installments)
                        .Sum(i => i.AmortizationValue + i.InterestValue),
                    TotalValueInstallments = group
                        .SelectMany(sim => sim.Simulations)
                        .SelectMany(s => s.Installments)
                        .Sum(i => i.InstallmentValue)
                }).ToList();

            return simulationsVolume;
        }
    }
}