using Application.Dtos;
using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System.Text.Json;

namespace Application.Handlers
{
    public class LoanSimulationHandler(IProductRepository productRepository,
        ILoanSimulationRepository loanSimulationRepository,
        ILoanSimulatorService loanSimulatorService,
        IEventHubProducer eventHubProducer) 
        : IRequestHandler<SimulateLoanRequest, LoanSimulationResponse>
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ILoanSimulationRepository _loanSimulationRepository = loanSimulationRepository;
        private readonly ILoanSimulatorService _loanSimulatorService = loanSimulatorService;
        private readonly IEventHubProducer _eventHubProducer = eventHubProducer;

        public async Task<LoanSimulationResponse> Handle(SimulateLoanRequest request,
            CancellationToken cancellationToken)
        {
            var appropriateProduct = await SearchAppropriateProduct(request) ?? throw new Exception("Não há produto que atenda essas condições");

            var sacSimulationResult = _loanSimulatorService.SimulateSac(request, appropriateProduct.InterestRate);
            var priceSimulationResult = _loanSimulatorService.SimulatePrice(request, appropriateProduct.InterestRate);

            var loanSimulation = GetLoanSimulationEntity(sacSimulationResult, priceSimulationResult, appropriateProduct);

            _ = await _loanSimulationRepository.AddAsync(loanSimulation);

            var response = new LoanSimulationResponse(1, appropriateProduct.Id, appropriateProduct.Name,
                appropriateProduct.InterestRate, [sacSimulationResult, priceSimulationResult]);

            await _eventHubProducer.SendMessageAsync(JsonSerializer.Serialize(response));

            return response;
        }

        private async Task<Product?> SearchAppropriateProduct(SimulateLoanRequest request)
        {
            var products = await _productRepository.GetAllAsync();

            var appropriateProduct = products.FirstOrDefault(p =>
                p.MinMonth <= request.Period && p.MaxMonth >= request.Period &&
                p.MinValue <= request.Value && p.MaxValue >= request.Value);

            return appropriateProduct;
        }

        private static LoanSimulation GetLoanSimulationEntity(SimulationDto sacSimulationResult,
            SimulationDto priceSimulationResult, Product appropriateProduct)
        {
            var sacSimulationEntity = new Simulation
            {
                SimulationType = sacSimulationResult.SimulationType,
                Installments = [.. sacSimulationResult.Installments.Select(i => new Installment
                {
                    InstallmentNumber = i.InstallmentNumber,
                    AmortizationValue = i.AmortizationValue,
                    InterestValue = i.InterestValue,
                    InstallmentValue = i.InstallmentValue
                })]
            };

            var priceSimulationEntity = new Simulation
            {
                SimulationType = priceSimulationResult.SimulationType,
                Installments = [.. priceSimulationResult.Installments.Select(i => new Installment
                {
                    InstallmentNumber = i.InstallmentNumber,
                    AmortizationValue = i.AmortizationValue,
                    InterestValue = i.InterestValue,
                    InstallmentValue = i.InstallmentValue
                })]
            };

            var loanSimulation = new LoanSimulation
            {
                ProductId = appropriateProduct.Id,
                ProductName = appropriateProduct.Name,
                InterestRate = appropriateProduct.InterestRate,
                Simulations = [sacSimulationEntity, priceSimulationEntity]
            };

            return loanSimulation;
        }
    }
}
