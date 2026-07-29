using MediatR;
using ContainerTransferRequest = VsaDemo.Contracts.ContainerTransfer.ContainerTransferRequest;
using TransferResult = VsaDemo.Contracts.ContainerTransfer.TransferResult;
using UnloadContainerRequest = VsaDemo.Contracts.UnloadContainer.UnloadContainerRequest;
using UnloadContainerResult = VsaDemo.Contracts.UnloadContainer.UnloadContainerResult;

namespace VsaDemo.Api.Endpoints;

public static class ContainerTransferEndpoint
{
    public static IEndpointRouteBuilder MapContainerTransferEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/container-transfer", async (ContainerTransferRequest request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(request, cancellationToken);
            return Results.Created($"/container-transfer/{request.ContainerId}", result);
        })
        .WithName("TransferContainer")
        .Produces<TransferResult>(StatusCodes.Status201Created)
        .ProducesValidationProblem();

        return app;
    }
}
