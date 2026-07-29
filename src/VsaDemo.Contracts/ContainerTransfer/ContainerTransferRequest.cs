using MediatR;

namespace VsaDemo.Contracts.ContainerTransfer;

public sealed record ContainerTransferRequest(string ContainerId, string SourceLocation, string DestinationLocation)
    : IRequest<TransferResult>;
