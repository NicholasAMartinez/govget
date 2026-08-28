using System.Net;
using System.Net.Http;
using GovGet.Core.Clients;

namespace GovGet.Core.Tests.Clients;

public sealed class UsgsClientTests
{
    [Fact]
    public async Task GetVersionAsync_ReturnsTrimmedVersion()
    {
        var handler = new FakeHttpMessageHandler(
            HttpStatusCode.OK,
            "2.7.0\n"
        );

        using var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri(
                "https://earthquake.usgs.gov/fdsnws/event/1/"
            )
        };

        var client = new UsgsClient(httpClient);

        var result = await client.GetVersionAsync();

        Assert.Equal("2.7.0", result);
    }

    private sealed class FakeHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpStatusCode _statusCode;
        private readonly string _content;

        public FakeHttpMessageHandler(
            HttpStatusCode statusCode,
            string content)
        {
            _statusCode = statusCode;
            _content = content;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(_statusCode)
            {
                Content = new StringContent(_content)
            };

            return Task.FromResult(response);
        }
    }
}
