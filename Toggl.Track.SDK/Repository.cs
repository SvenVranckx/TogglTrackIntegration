namespace Toggl.Track.SDK
{
    public interface IRepository<TEntity, TOptions>
        where TEntity : class
        where TOptions : class, IQueryOptions
    {
        Task<TEntity?> Get(long id, TOptions? options = null);
        Task<TEntity[]> Get(TOptions? options = null);
    }

    internal class Repository<TEntity, TOptions> : IRepository<TEntity, TOptions>
        where TEntity : class
        where TOptions : class, IQueryOptions
    {
        private readonly ApiClient _client;
        private readonly string _query;

        internal Repository(ApiClient client, string query)
        {
            _client = client;
            _query = query;
        }

        public async Task<TEntity?> Get(long id, TOptions? options = null)
        {
            var query = QueryOptions.Apply(options, _query, id);
            var entity = await _client.GetEntity<TEntity>(query);
            return entity;
        }

        public async Task<TEntity[]> Get(TOptions? options = null)
        {
            var query = QueryOptions.Apply(options, _query);
            var entities = await _client.GetEntities<TEntity>(query);
            return entities;
        }
    }
}
