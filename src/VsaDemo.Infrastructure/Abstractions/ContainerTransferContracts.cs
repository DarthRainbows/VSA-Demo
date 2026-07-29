namespace VsaDemo.Infrastructure.Abstractions.ContainerTransfer;

public sealed record TransferRepositoryRequest(string ContainerId, string SourceLocation, string DestinationLocation);

public sealed record TransferRecord(string ContainerId, string SourceLocation, string DestinationLocation);

public interface IContainerTransferRepository
{
    Task<TransferRecord> TransferAsync(TransferRepositoryRequest request, CancellationToken cancellationToken);
}
