namespace VsaDemo.Contracts.Infrastructure;

public interface ISolventProcessingClient
{
    Task<ProcessingResult> HandleAsync(UnloadWasteRequest request, CancellationToken cancellationToken);
}
