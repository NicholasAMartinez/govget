using GovGet.Core.Services;

if (args.Length == 0)
{
    Console.WriteLine("Usage: govget <command>");
    return;
}

switch (args[0].ToLowerInvariant())
{
    case "ping":
        var pingService = new PingService();
        var result = pingService.Ping();

        Console.WriteLine($"Status: {result.Status}");
        Console.WriteLine($"Timestamp: {result.Timestamp:u}");
        break;

    default:
        Console.WriteLine($"Unknown command: {args[0]}");
        break;
}
