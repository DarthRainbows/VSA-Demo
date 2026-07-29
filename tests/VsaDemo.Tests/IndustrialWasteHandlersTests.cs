using FluentAssertions;
using Moq;
using VsaDemo.Contracts.ContainerTransfer;
using VsaDemo.Contracts.Infrastructure;
using VsaDemo.Contracts.UnloadContainer;
using VsaDemo.Core.Features.ContainerTransfer;
using VsaDemo.Core.Features.UnloadContainer;

namespace VsaDemo.Tests;

public class IndustrialWasteHandlersTests
{
    [Fact]
    public async Task ContainerTransferHandler_UsesRepositoryAndPublishesMessage()
    {
        var repository = new Mock<IContainerTransferRepository>();
        var publisher = new Mock<IIntegrationEventPublisher>();
        var validator = new ContainerTransferValidator();

        repository
            .Setup(x => x.TransferAsync(It.IsAny<TransferRepositoryRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TransferRecord("C-100", "BAY-01", "BAY-02"));

        var handler = new ContainerTransferHandler(repository.Object, publisher.Object, validator);

        var result = await handler.Handle(
            new ContainerTransferRequest("C-100", "BAY-01", "BAY-02"),
            CancellationToken.None);

        result.ContainerId.Should().Be("C-100");
        repository.Verify(x => x.TransferAsync(It.IsAny<TransferRepositoryRequest>(), It.IsAny<CancellationToken>()), Times.Once);
        publisher.Verify(x => x.PublishAsync(It.Is<IntegrationMessage>(m => m.ContainerId == "C-100"), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UnloadContainerHandler_GroupsWasteByTypeAndPublishesMessage()
    {
        var publisher = new Mock<IIntegrationEventPublisher>();
        var lubricantClient = new Mock<ILubricantProcessingClient>();
        var antifreezeClient = new Mock<IAntifreezeProcessingClient>();
        var solventClient = new Mock<ISolventProcessingClient>();
        var validator = new UnloadContainerValidator();

        lubricantClient
            .Setup(x => x.HandleAsync(It.IsAny<UnloadWasteRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new VsaDemo.Contracts.Infrastructure.ProcessingResult("lubricants", "C-300", "Accepted"));
antifreezeClient
            .Setup(x => x.HandleAsync(It.IsAny<UnloadWasteRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new VsaDemo.Contracts.Infrastructure.ProcessingResult("antifreeze", "C-300", "Accepted"));
solventClient
            .Setup(x => x.HandleAsync(It.IsAny<UnloadWasteRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new VsaDemo.Contracts.Infrastructure.ProcessingResult("solvents", "C-300", "Accepted"));

        var request = new UnloadContainerRequest(
            "C-300",
            [
                new WasteItem("lubricants", 12.5m),
                new WasteItem("antifreeze", 8m),
                new WasteItem("solvents", 4.5m)
            ]);

        var handler = new UnloadContainerHandler(publisher.Object, lubricantClient.Object, antifreezeClient.Object, solventClient.Object, validator);

        var result = await handler.Handle(request, CancellationToken.None);

        result.ContainerId.Should().Be("C-300");
        lubricantClient.Verify(x => x.HandleAsync(It.Is<UnloadWasteRequest>(c => c.ContainerId == "C-300"), It.IsAny<CancellationToken>()), Times.Once);
        antifreezeClient.Verify(x => x.HandleAsync(It.Is<UnloadWasteRequest>(c => c.ContainerId == "C-300"), It.IsAny<CancellationToken>()), Times.Once);
        solventClient.Verify(x => x.HandleAsync(It.Is<UnloadWasteRequest>(c => c.ContainerId == "C-300"), It.IsAny<CancellationToken>()), Times.Once);
        publisher.Verify(x => x.PublishAsync(It.Is<IntegrationMessage>(m => m.ContainerId == "C-300"), It.IsAny<CancellationToken>()), Times.Once);
    }
}
