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
        var version = await _httpClient.GetStringAsync("version");

        return version.Trim();
    }
}
