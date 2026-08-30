using GovGet.Core.Clients;
using GovGet.Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSingleton<PingService>();

builder.Services.AddHttpClient<UsgsClient>(client =>
{
    client.BaseAddress = new Uri(
        "https://earthquake.usgs.gov/fdsnws/event/1/"
    );
});

var app = builder.Build();

// Existing configuration...

// This ping is for the application as a whole and not for a single endpoint or 
// specific service or component.
app.MapGet("/api/ping", (PingService pingService) =>
{
    return pingService.Ping();
});

app.MapGet("/api/count", async (UsgsClient usgsClient) =>
{
    uint count = await usgsClient.GetCountAsync();
    return new { count };
});

app.Run();

app.Run();
