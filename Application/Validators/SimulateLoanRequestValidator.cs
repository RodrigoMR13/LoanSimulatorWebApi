using Application.Dtos.Requests;
using FluentValidation;

namespace Application.Validators
{
    public class SimulateLoanRequestValidator : AbstractValidator<SimulateLoanRequest>
    {
        public SimulateLoanRequestValidator()
        {
            RuleFor(x => x.Value)
                .GreaterThan(0).WithMessage("O valor do empréstimo deve ser maior do que zero.");

            RuleFor(x => x.Period)
                .InclusiveBetween(1, 360).WithMessage("O período deve estar entre 1 e 360 meses.");
        }
    }
}
