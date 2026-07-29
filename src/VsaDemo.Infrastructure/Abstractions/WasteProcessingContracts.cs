namespace VsaDemo.Infrastructure.Abstractions.WasteProcessing;

public sealed record UnloadWasteRequest(string ContainerId, string WasteType, decimal QuantityKg);

public sealed record ProcessingResult(string WasteType, string ContainerId, string Status);

public interface ILubricantProcessingClient
{
    Task<ProcessingResult> HandleAsync(UnloadWasteRequest request, CancellationToken cancellationToken);
}

public interface IAntifreezeProcessingClient
{
    Task<ProcessingResult> HandleAsync(UnloadWasteRequest request, CancellationToken cancellationToken);
}

public interface ISolventProcessingClient
{
    Task<ProcessingResult> HandleAsync(UnloadWasteRequest request, CancellationToken cancellationToken);
}
