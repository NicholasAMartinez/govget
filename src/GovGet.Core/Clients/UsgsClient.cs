namespace GovGet.Core.Clients;

/// <summary>
/// Provides access to the USGS Earthquake Catalog API.
/// </summary>
public sealed class UsgsClient
{
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Creates a new USGS API client.
    /// </summary>
    /// <param name="httpClient">
    /// The HTTP client used to communicate with the USGS API.
    /// </param>
    public UsgsClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Gets the current version of the USGS Earthquake Catalog API.
    /// </summary>
    /// <returns>The API version reported by USGS.</returns>
    public async Task<string> GetVersionAsync()
    {
        string version = await _httpClient.GetStringAsync("version");

        return version.Trim();
    }

    public async Task<uint> GetCountAsync(string filter = "")
    {
        string response = await _httpClient.GetStringAsync($"count?{filter}");

        return uint.Parse(response.Trim());
    }
}
