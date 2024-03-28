using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Toggle.Track.SDK
{
    public class Client : IDisposable
    {
        private readonly HttpClient _httpClient;
        private bool _disposed;

        private const string BaseUrl = @"https://api.track.toggl.com";

        public Client(string apiToken)
        {
            var handler = new HttpClientHandler();
            _httpClient = new HttpClient(handler, true)
            {
                BaseAddress = new Uri(BaseUrl)
            };

            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{apiToken}:api_token"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        }

        public void Dispose()
        {
            if (_disposed)
                return;
            _disposed = true;
            _httpClient.Dispose();
        }

        public async Task<string> GetString(string query)
        {
            return await _httpClient.GetStringAsync(query);
        }
    }
}
