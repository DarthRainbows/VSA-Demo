using FluentValidation;
using MediatR;
using ContainerTransferRequest = VsaDemo.Contracts.ContainerTransfer.ContainerTransferRequest;
using TransferResult = VsaDemo.Contracts.ContainerTransfer.TransferResult;
using VsaDemo.Contracts.Infrastructure;

namespace VsaDemo.Core.Features.ContainerTransfer;

public sealed class ContainerTransferHandler(
    IContainerTransferRepository repository,
    IIntegrationEventPublisher integrationEventPublisher,
    IValidator<ContainerTransferRequest> validator)
    : IRequestHandler<ContainerTransferRequest, TransferResult>
{
    public async Task<TransferResult> Handle(ContainerTransferRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var transfer = await repository.TransferAsync(
            new VsaDemo.Contracts.Infrastructure.TransferRepositoryRequest(
                request.ContainerId,
                request.SourceLocation,
                request.DestinationLocation),
            cancellationToken);

        await integrationEventPublisher.PublishAsync(
            new IntegrationMessage(
                transfer.ContainerId,
                transfer.SourceLocation,
                transfer.DestinationLocation,
                "ContainerTransferred",
                DateTimeOffset.UtcNow),
            cancellationToken);

        return new TransferResult(transfer.ContainerId, transfer.SourceLocation, transfer.DestinationLocation, "Transferred");
    }
}
