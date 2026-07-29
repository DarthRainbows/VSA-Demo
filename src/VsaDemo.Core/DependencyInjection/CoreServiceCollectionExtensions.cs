using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace VsaDemo.Core.DependencyInjection;

public static class CoreServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, Assembly? assembly = null)
    {
        var coreAssembly = assembly ?? typeof(ApplicationMarker).Assembly;

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(coreAssembly));
        services.AddValidatorsFromAssembly(coreAssembly);

        return services;
    }
}
