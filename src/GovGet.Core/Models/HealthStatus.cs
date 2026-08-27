namespace GovGet.Core.Models;

/// <summary>
/// Represents the health status of a component or service.
/// </summary>
public enum HealthStatus
{
    /// <summary>
    /// Indicates that the component or service is healthy and functioning normally.
    /// </summary>
    Healthy,

    /// <summary>
    /// Indicates that the component or service is functioning with reduced reliability or performance.
    /// </summary>
    Degraded,

    /// <summary>
    /// Indicates that the component or service is not functioning properly or is unavailable.
    /// </summary>
    Unhealthy
}
