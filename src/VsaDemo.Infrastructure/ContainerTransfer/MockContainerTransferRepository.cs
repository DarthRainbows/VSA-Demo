using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using VsaDemo.Contracts.Infrastructure;

namespace VsaDemo.Infrastructure.ContainerTransfer;

public sealed class MockContainerTransferRepository : IContainerTransferRepository, IDisposable
{
    private readonly IDbConnection _connection;

    public MockContainerTransferRepository()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();

        _connection.Execute(@"
            CREATE TABLE ContainerTransfers (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ContainerId TEXT NOT NULL,
                SourceLocation TEXT NOT NULL,
                DestinationLocation TEXT NOT NULL
            )");
    }

    public async Task<TransferRecord> TransferAsync(TransferRepositoryRequest request, CancellationToken cancellationToken)
    {
        await _connection.ExecuteAsync(@"
            INSERT INTO ContainerTransfers (ContainerId, SourceLocation, DestinationLocation)
            VALUES (@ContainerId, @SourceLocation, @DestinationLocation)",
            new
            {
                request.ContainerId,
                request.SourceLocation,
                request.DestinationLocation
            });

        return new TransferRecord(request.ContainerId, request.SourceLocation, request.DestinationLocation);
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
}
