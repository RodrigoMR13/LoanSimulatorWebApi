using Application.Dtos.Requests;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Validators
{
    public class SimulateLoanRequestValidator : AbstractValidator<SimulateLoanRequest>
    {
        private readonly IProductRepository _productRepository;
        public SimulateLoanRequestValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            RuleFor(x => x.Value)
                .GreaterThan(200).WithMessage("O valor do empréstimo deve ser maior do que R$200,00.");

            RuleFor(x => x.Period)
                .InclusiveBetween(1, 360).WithMessage("O período deve estar entre 1 e 360 meses.");

            RuleFor(x => x)
                .MustAsync(BeValidForSomeProduct)
                .WithMessage("Não há produto que atenda esse valor e/ou período.");
        }
        private async Task<bool> BeValidForSomeProduct(SimulateLoanRequest request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();

            return products.Any(p =>
                (p.MinMonth == 0 || request.Period >= p.MinMonth) &&
                (p.MaxMonth == null || request.Period <= p.MaxMonth) &&
                request.Value >= p.MinValue &&
                (p.MaxValue == null || request.Value <= p.MaxValue));
        }
    }
}
