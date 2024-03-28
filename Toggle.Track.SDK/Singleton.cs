namespace Toggle.Track.SDK
{
    public interface ISingleton<T>
    {
        Task<T?> Get(bool expand = false);
    }

    internal class Singleton<T> : ISingleton<T>
    {
        private readonly ApiClient _client;
        private readonly string _query;
        private readonly string? _expansionParameter;

        internal Singleton(ApiClient client, string query, string? expansionParameter)
        {
            _client = client;
            _query = query;
            _expansionParameter = expansionParameter;
        }

        public async Task<T?> Get(bool expand = false)
        {
            var query = expand ? $"{_query}?{_expansionParameter}=true" : _query;
            var entity = await _client.GetEntity<T>(query);
            return entity;
        }
    }
}
