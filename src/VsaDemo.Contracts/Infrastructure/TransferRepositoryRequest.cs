namespace VsaDemo.Contracts.Infrastructure;

public sealed record TransferRepositoryRequest(string ContainerId, string SourceLocation, string DestinationLocation);
