namespace VsaDemo.Contracts.Infrastructure;

public interface ILubricantProcessingClient
{
    Task<ProcessingResult> HandleAsync(UnloadWasteRequest request, CancellationToken cancellationToken);
}
