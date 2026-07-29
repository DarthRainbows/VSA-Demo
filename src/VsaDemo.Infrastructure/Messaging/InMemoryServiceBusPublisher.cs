using VsaDemo.Contracts.Infrastructure;

namespace VsaDemo.Infrastructure.Messaging;

public sealed class InMemoryServiceBusPublisher : IIntegrationEventPublisher
{
    public Task PublishAsync(IntegrationMessage message, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Service Bus: {message.EventType} for container {message.ContainerId}");
        return Task.CompletedTask;
    }
}
