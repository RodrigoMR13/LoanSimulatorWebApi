using Application.Dtos;
using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers
{
    public class GetSimulationsVolumeHandler(ILoanSimulationRepository repository, ILogger<GetSimulationsVolumeHandler> logger) : IRequestHandler<GetSimulationsVolumeRequest, GetSimulationsVolumeResponse>
    {
        private readonly ILoanSimulationRepository _repository = repository;
        private readonly ILogger<GetSimulationsVolumeHandler> _logger = logger;

        public async Task<GetSimulationsVolumeResponse> Handle(
            GetSimulationsVolumeRequest request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Obtendo todas as simulações para o dia {request.ReferenceDate} no Banco de Dados...");
            var simulations = await _repository.GetByDateAsync(request.ReferenceDate);

            var simulationsVolume = simulations.GroupBy(sim => new { sim.ProductId, sim.ProductName })
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

            var response = new GetSimulationsVolumeResponse
            {
                ReferenceDate = request.ReferenceDate,
                SimulationsVolumeData = simulationsVolume
            };

            return response;
        }
    }
}