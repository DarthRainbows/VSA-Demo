namespace VsaDemo.Contracts.ContainerTransfer;

public sealed record TransferResult(string ContainerId, string SourceLocation, string DestinationLocation, string Status);
