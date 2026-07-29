using VsaDemo.Api.Endpoints;

namespace VsaDemo.Api;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapDemoEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapContainerTransferEndpoint();
        app.MapUnloadContainerEndpoint();
        return app;
    }
}
