using VsaDemo.Contracts.Infrastructure;

namespace VsaDemo.Infrastructure.WasteProcessing;

public sealed class LubricantProcessingClient : ILubricantProcessingClient
{
    public Task<ProcessingResult> HandleAsync(UnloadWasteRequest request, CancellationToken cancellationToken)
        => Task.FromResult(new ProcessingResult("lubricants", request.ContainerId, "Accepted"));
}

public sealed class AntifreezeProcessingClient : IAntifreezeProcessingClient
{
    public Task<ProcessingResult> HandleAsync(UnloadWasteRequest request, CancellationToken cancellationToken)
        => Task.FromResult(new ProcessingResult("antifreeze", request.ContainerId, "Accepted"));
}

public sealed class SolventProcessingClient : ISolventProcessingClient
{
    public Task<ProcessingResult> HandleAsync(UnloadWasteRequest request, CancellationToken cancellationToken)
        => Task.FromResult(new ProcessingResult("solvents", request.ContainerId, "Accepted"));
}
