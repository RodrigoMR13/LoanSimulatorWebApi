using Application.Dtos;
using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Handlers
{
    public class SimulateLoanHandler : IRequestHandler<SimulateLoanRequest, LoanSimulationResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILoanSimulatorService _loanSimulatorService;

        public SimulateLoanHandler(IProductRepository productRepository, ILoanSimulatorService loanSimulatorService)
        {
            _productRepository = productRepository;
            _loanSimulatorService = loanSimulatorService;
        }

        public async Task<LoanSimulationResponse> Handle(SimulateLoanRequest request, CancellationToken cancellationToken)
        {
            var appropriateProduct = await SearchAppropriateProduct(request) ?? throw new Exception("Não há produto que atenda essas condições");

            var sacSimulationResult = _loanSimulatorService.SimulateSac(request, appropriateProduct.InterestRate);

            var priceSimulationResult = _loanSimulatorService.SimulatePrice(request, appropriateProduct.InterestRate);

            var response = new LoanSimulationResponse(1, appropriateProduct.Id, appropriateProduct.Name,
                appropriateProduct.InterestRate, [sacSimulationResult, priceSimulationResult]);

            return response;
        }

        private async Task<Product?> SearchAppropriateProduct(SimulateLoanRequest request)
        {
            var products = await _productRepository.GetAllAsync();

            var appropriateProduct = products.FirstOrDefault(p =>
                (p.MinMonth <= request.Period && p.MaxMonth >= request.Period) &&
                (p.MinValue <= request.Value && p.MaxValue >= request.Value));

            return appropriateProduct;
        }
    }
}
