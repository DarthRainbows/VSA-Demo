namespace VsaDemo.Contracts.Infrastructure;

public sealed record IntegrationMessage(
    string ContainerId,
    string SourceLocation,
    string DestinationLocation,
    string EventType,
    DateTimeOffset OccurredAt);
