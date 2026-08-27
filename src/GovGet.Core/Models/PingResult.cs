namespace GovGet.Core.Models;

/// <summary>
/// Represents the result of a health check.
/// </summary>
/// <param name="Status">The health status of the component or service.</param>
/// <param name="Timestamp">The UTC time at which the health check was performed.</param>
public sealed record PingResult(
    HealthStatus Status,
    DateTimeOffset Timestamp
);
