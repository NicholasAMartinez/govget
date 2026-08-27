using GovGet.Core.Models;

namespace GovGet.Core.Services;

/// <summary>
/// Provides health check functionality for GovGet.
/// </summary>
public sealed class PingService
{
    /// <summary>
    /// Performs a basic health check.
    /// </summary>
    /// <returns>The result of the health check.</returns>
    public PingResult Ping()
    {
        return new PingResult(
            HealthStatus.Healthy,
            DateTimeOffset.UtcNow
        );
    }
}
