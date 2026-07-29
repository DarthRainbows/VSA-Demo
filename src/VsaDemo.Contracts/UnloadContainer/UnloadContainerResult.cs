namespace VsaDemo.Contracts.UnloadContainer;

public sealed record UnloadContainerResult(string ContainerId, IReadOnlyList<ProcessingResult> ProcessedWaste, string Status);
