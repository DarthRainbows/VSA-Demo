namespace VsaDemo.Contracts.Infrastructure;

public interface IAntifreezeProcessingClient
{
    Task<ProcessingResult> HandleAsync(UnloadWasteRequest request, CancellationToken cancellationToken);
}
