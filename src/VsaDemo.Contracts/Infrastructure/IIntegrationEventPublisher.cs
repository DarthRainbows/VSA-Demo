namespace VsaDemo.Contracts.Infrastructure;

public interface IIntegrationEventPublisher
{
    Task PublishAsync(IntegrationMessage message, CancellationToken cancellationToken);
}
