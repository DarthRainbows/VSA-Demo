using MediatR;

namespace VsaDemo.Contracts.UnloadContainer;

public sealed record UnloadContainerRequest(string ContainerId, IReadOnlyList<WasteItem> WasteItems)
    : IRequest<UnloadContainerResult>;
