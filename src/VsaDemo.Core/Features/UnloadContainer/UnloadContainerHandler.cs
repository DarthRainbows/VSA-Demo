using FluentValidation;
using MediatR;
using UnloadContainerRequest = VsaDemo.Contracts.UnloadContainer.UnloadContainerRequest;
using UnloadContainerResult = VsaDemo.Contracts.UnloadContainer.UnloadContainerResult;
using ProcessingResult = VsaDemo.Contracts.UnloadContainer.ProcessingResult;
using VsaDemo.Contracts.Infrastructure;

namespace VsaDemo.Core.Features.UnloadContainer;

public sealed class UnloadContainerHandler(
    IIntegrationEventPublisher integrationEventPublisher,
    ILubricantProcessingClient lubricantClient,
    IAntifreezeProcessingClient antifreezeClient,
    ISolventProcessingClient solventClient,
    IValidator<UnloadContainerRequest> validator)
    : IRequestHandler<UnloadContainerRequest, UnloadContainerResult>
{
    public async Task<UnloadContainerResult> Handle(UnloadContainerRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var processingResults = new List<ProcessingResult>();

        foreach (var wasteItem in request.WasteItems)
        {
            var result = wasteItem.WasteType.ToLowerInvariant() switch
            {
                "lubricants" => await lubricantClient.HandleAsync(
                    new VsaDemo.Contracts.Infrastructure.UnloadWasteRequest(request.ContainerId, wasteItem.WasteType, wasteItem.QuantityKg),
                    cancellationToken),
                "antifreeze" => await antifreezeClient.HandleAsync(
                    new VsaDemo.Contracts.Infrastructure.UnloadWasteRequest(request.ContainerId, wasteItem.WasteType, wasteItem.QuantityKg),
                    cancellationToken),
                "solvents" => await solventClient.HandleAsync(
                    new VsaDemo.Contracts.Infrastructure.UnloadWasteRequest(request.ContainerId, wasteItem.WasteType, wasteItem.QuantityKg),
                    cancellationToken),
                _ => throw new ValidationException(new[] { new FluentValidation.Results.ValidationFailure("WasteType", $"Unsupported waste type '{wasteItem.WasteType}'.") })
            };

            processingResults.Add(new ProcessingResult(result.WasteType, result.ContainerId, result.Status));
        }

        await integrationEventPublisher.PublishAsync(
            new IntegrationMessage(
                request.ContainerId,
                "container-unloading",
                "processing-bay",
                "ContainerUnloaded",
                DateTimeOffset.UtcNow),
            cancellationToken);

        return new UnloadContainerResult(request.ContainerId, processingResults, "Processed");
    }
}
