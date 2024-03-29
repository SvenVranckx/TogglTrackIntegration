namespace Toggl.Track.SDK
{
    public interface ISingleton<TEntity, TQuery>
        where TEntity : class
        where TQuery : class, IQuery<TEntity>
    {
        Task<TEntity?> Get(TQuery? query = null);
    }

    public interface ISingleton<TEntity> : ISingleton<TEntity, Query<TEntity>>
        where TEntity : class
    {
    }

    internal class Singleton<TEntity, TQuery> : ISingleton<TEntity, TQuery>
        where TEntity : class
        where TQuery : class, IQuery<TEntity>
    {
        private readonly IClient _client;
        private readonly IQuery<TEntity> _defaultQuery;

        internal Singleton(IClient client, IQuery<TEntity> defaultQuery)
        {
            _client = client;
            _defaultQuery = defaultQuery;
        }

        internal Singleton(IClient client, string path) : this(client, new Query<TEntity>(path))
        {
        }

        private IQuery<TEntity> QueryOrDefault(TQuery? query) => query ?? _defaultQuery;

        public async Task<TEntity?> Get(TQuery? query = null) =>
            await QueryOrDefault(query).GetSingleton(_client);
    }

    internal class Singleton<TEntity> : Singleton<TEntity, Query<TEntity>>, ISingleton<TEntity>
        where TEntity : class
    {
        internal Singleton(IClient client, IQuery<TEntity> defaultQuery) : base(client, defaultQuery)
        {
        }

        internal Singleton(IClient client, string path) : base(client, path)
        {
        }
    }

    internal static class Singleton
    {
        internal static Singleton<TEntity> Create<TEntity>(IClient client, string path)
            where TEntity : class
        {
            return new Singleton<TEntity>(client, path);
        }

        internal static Singleton<TEntity, TQuery> Create<TEntity, TQuery>(IClient client)
            where TEntity : class
            where TQuery : class, IQuery<TEntity>, new()
        {
            return new Singleton<TEntity, TQuery>(client, new TQuery());
        }
    }
}