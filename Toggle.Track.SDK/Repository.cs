namespace Toggle.Track.SDK
{
    public interface IRepository<T>
    {
        Task<T?> Get(long id, bool expand = false);
        Task<T[]> Get();
    }

    internal class Repository<T> : IRepository<T>
    {
        private readonly ApiClient _client;
        private readonly string _query;

        internal Repository(ApiClient client, string query)
        {
            _client = client;
            _query = query;
        }

        public async Task<T?> Get(long id, bool expand = false)
        {
            var entity = await _client.GetEntity<T>($"{_query}/{id}");
            return entity;
        }

        public async Task<T[]> Get()
        {
            var entities = await _client.GetEntities<T>(_query);
            return entities;
        }
    }
}
