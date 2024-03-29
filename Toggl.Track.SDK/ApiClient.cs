using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace Toggl.Track.SDK
{
    internal class ApiClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private bool _disposed;

        private const string BaseUrl = @"https://api.track.toggl.com";
        private const string ApiRoot = @"/api/v9";

        private static string GetRequestUrl(string query) => $"{ApiRoot}/{query.TrimStart('/')}";

        public ApiClient(string apiToken)
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

        public async Task<T?> GetEntity<T>(string query)
        {
            var entity = await _httpClient.GetFromJsonAsync<T>(GetRequestUrl(query));
            return entity;
        }

        public async Task<T[]> GetEntities<T>(string query)
        {
            var entities = await _httpClient.GetFromJsonAsync<T[]>(GetRequestUrl(query));
            return entities ?? [];
        }
    }
}
