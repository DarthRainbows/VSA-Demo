using Microsoft.Extensions.DependencyInjection;
using VsaDemo.Contracts.Infrastructure;
using VsaDemo.Infrastructure.ContainerTransfer;
using VsaDemo.Infrastructure.Messaging;
using VsaDemo.Infrastructure.WasteProcessing;

namespace VsaDemo.Infrastructure.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IIntegrationEventPublisher, InMemoryServiceBusPublisher>();
        services.AddSingleton<IContainerTransferRepository, MockContainerTransferRepository>();
        services.AddSingleton<ILubricantProcessingClient, LubricantProcessingClient>();
        services.AddSingleton<IAntifreezeProcessingClient, AntifreezeProcessingClient>();
        services.AddSingleton<ISolventProcessingClient, SolventProcessingClient>();
        return services;
    }
}
