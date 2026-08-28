using GovGet.Core.Services;

if (args.Length == 0)
{
    Console.WriteLine("Usage: govget <command>");
    Console.WriteLine("Use 'govget help' to see available commands.");
    return;
}

switch (args[0].ToLowerInvariant())
{
    case "help":
        Console.WriteLine("Available commands:");
        Console.WriteLine("  ping - Check the health status of the application.");
        break;

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
