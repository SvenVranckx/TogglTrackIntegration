namespace Toggle.Track.SDK
{
    public class Repository<T>
    {
        private readonly Client _client;
        private readonly string _query;

        public Repository(Client client, string query)
        {
            _client = client;
            _query = query;
        }

        public async Task<T[]> Get()
        {
            var entities = await _client.GetEntities<T>(_query);
            return entities;
        }
    }
}
