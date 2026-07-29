using FluentValidation;
using ContainerTransferRequest = VsaDemo.Contracts.ContainerTransfer.ContainerTransferRequest;

namespace VsaDemo.Core.Features.ContainerTransfer;

public sealed class ContainerTransferValidator : AbstractValidator<ContainerTransferRequest>
{
    public ContainerTransferValidator()
    {
        RuleFor(x => x.ContainerId)
            .NotEmpty()
            .MaximumLength(32);

        RuleFor(x => x.SourceLocation)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(x => x.DestinationLocation)
            .NotEmpty()
            .MaximumLength(64);
    }
}
