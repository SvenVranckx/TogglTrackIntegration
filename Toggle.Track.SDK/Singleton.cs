namespace Toggle.Track.SDK
{
    public interface ISingleton<TEntity, TOptions>
        where TEntity : class
        where TOptions : class, IQueryOptions
    {
        Task<TEntity?> Get(TOptions? options = null);
    }

    internal class Singleton<TEntity, TOptions> : ISingleton<TEntity, TOptions>
        where TEntity : class
        where TOptions : class, IQueryOptions
    {
        private readonly ApiClient _client;
        private readonly string _query;

        internal Singleton(ApiClient client, string query)
        {
            _client = client;
            _query = query;
        }

        public async Task<TEntity?> Get(TOptions? options = null)
        {
            var query = QueryOptions.Apply(options, _query);
            var entity = await _client.GetEntity<TEntity>(query);
            return entity;
        }
    }
}
