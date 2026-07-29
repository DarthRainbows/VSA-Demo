namespace VsaDemo.Infrastructure.Abstractions;

public sealed record IntegrationMessage(
    string ContainerId,
    string SourceLocation,
    string DestinationLocation,
    string EventType,
    DateTimeOffset OccurredAt);

public interface IIntegrationEventPublisher
{
    Task PublishAsync(IntegrationMessage message, CancellationToken cancellationToken);
}
