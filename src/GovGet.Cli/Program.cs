using GovGet.Core.Clients;
using GovGet.Core.Models;
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
    {
        Console.WriteLine("Available commands:");
        Console.WriteLine("  help - Show this help message.");
        Console.WriteLine("  ping - Check the health status of the application.");
        Console.WriteLine("  usgs - Interact with the USGS Earthquake Catalog API.");
        break;
    }

    case "ping":
    {
        var pingService = new PingService();
        var result = pingService.Ping();

        Console.WriteLine($"Status: {result.Status}");
        Console.WriteLine($"Timestamp: {result.Timestamp:u}");
        break;
    }

    case "usgs":
    {
        await HandleUsgsCommandAsync(args);
        break;
    }

    default:
    {
        Console.WriteLine($"Unknown command: {args[0]}");
        break;
    }
}

static async Task HandleUsgsCommandAsync(string[] args)
{
    if (args.Length < 2)
    {
        Console.WriteLine("Usage: govget usgs <command>");
        Console.WriteLine("Use 'govget usgs help' to see available commands.");
        return;
    }

    switch (args[1].ToLowerInvariant())
    {
        case "help":
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("  help    - Show this help message.");
            Console.WriteLine("  version - Show the current USGS API version.");
            Console.WriteLine("  count   - Show the number of earthquakes in the last 30 days.");
            Console.WriteLine();
            Console.WriteLine("Count options:");
            Console.WriteLine("  --starttime <ISO 8601 date>");
            Console.WriteLine("  --endtime <ISO 8601 date>");
            Console.WriteLine("  --limit <positive integer>");
            Console.WriteLine("  --minmagnitude <number>");
            break;
        }

        case "version":
        {
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri(
                    "https://earthquake.usgs.gov/fdsnws/event/1/"
                )
            };

            var usgsClient = new UsgsClient(httpClient);

            var version = await usgsClient.GetVersionAsync();

            Console.WriteLine($"USGS Earthquake API version: {version}");
            break;
        }

        case "count":
        {
            UsgsEarthquakeQuery query;

            try
            {
                query = UsgsEarthquakeQuery.Parse(args[2..]);
            }
            catch (ArgumentException exception)
            {
                Console.Error.WriteLine(exception.Message);
                Environment.ExitCode = 1;
                break;
            }

            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri(
                    "https://earthquake.usgs.gov/fdsnws/event/1/"
                )
            };

            var usgsClient = new UsgsClient(httpClient);

            uint count = await usgsClient.GetCountAsync(query);

            if (query.HasParameters)
            {
                Console.WriteLine($"The query matched {count} earthquake{((count == 1) ? "" : "s")}.");
            }
            else
            {
                Console.WriteLine($"There have been {count} earthquake{((count == 1) ? "" : "'s")} in the last 30 days.");
            }

            break;
        }

        default:
        {
            Console.WriteLine($"Unknown USGS command: {args[1]}");
            break;
        }
    }
}
