namespace VsaDemo.Contracts.Infrastructure;

public sealed record UnloadWasteRequest(string ContainerId, string WasteType, decimal QuantityKg);
