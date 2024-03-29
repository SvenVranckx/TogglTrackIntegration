using System.Diagnostics;

namespace Toggl.Track.SDK
{
    public interface IRepository<TEntity, TQuery>
        where TEntity : class
        where TQuery : class, IQuery<TEntity>
    {
        Task<TEntity?> Get(long id, TQuery? query = null);
        Task<TEntity?> Get(TQuery query);
        Task<TEntity[]> Collect(TQuery? query = null);
    }

    public interface IRepository<TEntity> : IRepository<TEntity, Query<TEntity>>
        where TEntity : class
    {
    }

    internal class Repository<TEntity, TQuery> : IRepository<TEntity, TQuery>
        where TEntity : class
        where TQuery : class, IQuery<TEntity>
    {
        private readonly IClient _client;
        private readonly IQuery<TEntity> _defaultQuery;

        internal Repository(IClient client, IQuery<TEntity> defaultQuery)
        {
            _client = client;
            _defaultQuery = defaultQuery;
        }

        internal Repository(IClient client, string path) : this(client, new Query<TEntity>(path))
        {
        }

        [DebuggerStepThrough]
        private IQuery<TEntity> QueryOrDefault(TQuery? query) => query ?? _defaultQuery;

        public async Task<TEntity?> Get(long id, TQuery? query = null) =>
            await QueryOrDefault(query).GetEntity(_client, id);

        public async Task<TEntity?> Get(TQuery query) =>
            await QueryOrDefault(query).GetEntity(_client);

        public async Task<TEntity[]> Collect(TQuery? query = null) =>
            await QueryOrDefault(query).GetEntities(_client);
    }

    internal class Repository<TEntity> : Repository<TEntity, Query<TEntity>>, IRepository<TEntity>
        where TEntity : class
    {
        internal Repository(IClient client, IQuery<TEntity> defaultQuery) : base(client, defaultQuery)
        {
        }

        internal Repository(IClient client, string path) : base(client, path)
        {
        }
    }

    internal static class Repository
    {
        internal static Repository<TEntity> Create<TEntity>(IClient client, string path)
            where TEntity : class
        {
            return new Repository<TEntity>(client, path);
        }

        internal static Repository<TEntity, TQuery> Create<TEntity, TQuery>(IClient client)
            where TEntity : class
            where TQuery : class, IQuery<TEntity>, new()
        {
            return new Repository<TEntity, TQuery>(client, new TQuery());
        }
    }

    public static class RepositoryExtensions
    {
        public static async Task<TEntity> Single<TEntity, TQuery>(this IRepository<TEntity, TQuery> repository, TQuery? query = null)
            where TEntity : class
            where TQuery : class, IQuery<TEntity>, new()
        {
            return (await repository.Collect(query)).Single();
        }

        public static async Task<TEntity?> SingleOrDefault<TEntity, TQuery>(this IRepository<TEntity, TQuery> repository, TQuery? query = null)
            where TEntity : class
            where TQuery : class, IQuery<TEntity>, new()
        {
            return (await repository.Collect(query)).SingleOrDefault();
        }

        public static async Task<TEntity> First<TEntity, TQuery>(this IRepository<TEntity, TQuery> repository, TQuery? query = null)
            where TEntity : class
            where TQuery : class, IQuery<TEntity>, new()
        {
            return (await repository.Collect(query)).First();
        }

        public static async Task<TEntity?> FirstOrDefault<TEntity, TQuery>(this IRepository<TEntity, TQuery> repository, TQuery? query = null)
            where TEntity : class
            where TQuery : class, IQuery<TEntity>, new()
        {
            return (await repository.Collect(query)).FirstOrDefault();
        }

        public static async Task<TEntity> Single<TEntity>(this IRepository<TEntity> repository)
            where TEntity : class
        {
            return (await repository.Collect()).Single();
        }

        public static async Task<TEntity?> SingleOrDefault<TEntity>(this IRepository<TEntity> repository)
            where TEntity : class
        {
            return (await repository.Collect()).SingleOrDefault();
        }

        public static async Task<TEntity> First<TEntity>(this IRepository<TEntity> repository)
            where TEntity : class
        {
            return (await repository.Collect()).First();
        }

        public static async Task<TEntity?> FirstOrDefault<TEntity>(this IRepository<TEntity> repository)
            where TEntity : class
        {
            return (await repository.Collect()).FirstOrDefault();
        }
    }
}
