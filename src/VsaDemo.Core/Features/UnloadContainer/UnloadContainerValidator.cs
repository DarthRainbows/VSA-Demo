using FluentValidation;
using UnloadContainerRequest = VsaDemo.Contracts.UnloadContainer.UnloadContainerRequest;

namespace VsaDemo.Core.Features.UnloadContainer;

public sealed class UnloadContainerValidator : AbstractValidator<UnloadContainerRequest>
{
    public UnloadContainerValidator()
    {
        RuleFor(x => x.ContainerId)
            .NotEmpty()
            .MaximumLength(32);

        RuleFor(x => x.WasteItems)
            .NotNull();

        RuleForEach(x => x.WasteItems)
            .ChildRules(items =>
            {
                items.RuleFor(x => x.WasteType)
                    .NotEmpty()
                    .Must(type => type is "lubricants" or "antifreeze" or "solvents")
                    .WithMessage("Waste type must be lubricants, antifreeze, or solvents.");

                items.RuleFor(x => x.QuantityKg)
                    .GreaterThan(0);
            });
    }
}
