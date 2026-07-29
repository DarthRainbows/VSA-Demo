using MediatR;
using ContainerTransferRequest = VsaDemo.Contracts.ContainerTransfer.ContainerTransferRequest;
using TransferResult = VsaDemo.Contracts.ContainerTransfer.TransferResult;
using UnloadContainerRequest = VsaDemo.Contracts.UnloadContainer.UnloadContainerRequest;
using UnloadContainerResult = VsaDemo.Contracts.UnloadContainer.UnloadContainerResult;

namespace VsaDemo.Api.Endpoints;

public static class UnloadContainerEndpoint
{
    public static IEndpointRouteBuilder MapUnloadContainerEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/unload-container", async (UnloadContainerRequest request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(request, cancellationToken);
            return Results.Ok(result);
        })
        .WithName("UnloadContainer")
        .Produces<UnloadContainerResult>(StatusCodes.Status200OK)
        .ProducesValidationProblem();

        return app;
    }
}
