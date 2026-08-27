using GovGet.Core.Models;
using GovGet.Core.Services;

namespace GovGet.Core.Tests.Services;

public sealed class PingServiceTests
{
    [Fact]
    public void Ping_ReturnsHealthyStatus()
    {
        var service = new PingService();

        var result = service.Ping();

        Assert.Equal(HealthStatus.Healthy, result.Status);
    }

    [Fact]
    public void Ping_ReturnsCurrentTimestamp()
    {
        var service = new PingService();
        var before = DateTimeOffset.UtcNow;
    
        var result = service.Ping();
    
        var after = DateTimeOffset.UtcNow;
    
        Assert.InRange(result.Timestamp, before, after);
    }
}
