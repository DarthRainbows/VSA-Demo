namespace VsaDemo.Contracts.Infrastructure;

public interface IContainerTransferRepository
{
    Task<TransferRecord> TransferAsync(TransferRepositoryRequest request, CancellationToken cancellationToken);
}
