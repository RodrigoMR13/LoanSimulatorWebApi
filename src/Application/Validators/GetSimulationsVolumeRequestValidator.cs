using Application.Dtos.Requests;
using FluentValidation;

namespace Application.Validators
{
    public class GetSimulationsVolumeRequestValidator : AbstractValidator<GetSimulationsVolumeRequest>
    {
        public GetSimulationsVolumeRequestValidator()
        {
            RuleFor(x => x.ReferenceDate)
                .NotEmpty().WithMessage("A data de referência não pode ser vazia.")
                .Must(BeValidDate).WithMessage("A data de referência deve ser uma data válida.");
        }

        private bool BeValidDate(DateOnly date)
        {
            return date <= DateOnly.FromDateTime(DateTime.Now);
        }
    }
}
