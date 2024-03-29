namespace Toggl.Track.SDK
{
    public interface IQuery<T>
    {
        Task<T?> GetSingleton(IClient client);
        Task<T?> GetEntity(IClient client, long id);
        Task<T[]> GetEntities(IClient client);                
    }

    public class Query<T> : IQuery<T>
    {
        private readonly string _path;

        public Query(string path)
        {
            _path = path;
        }

        public async Task<T?> GetSingleton(IClient client) =>
            await client.GetEntity<T>(Build());

        public async Task<T?> GetEntity(IClient client, long id) =>
            await client.GetEntity<T>(Build(id));

        public async Task<T[]> GetEntities(IClient client) =>
            await client.GetEntities<T>(Build());

        protected virtual string Build() => ApplyOptions(_path);
        protected virtual string Build(long id) => ApplyOptions($"{_path}/{id}");

        private string ApplyOptions(string query)
        {
            var builder = new OptionsBuilder();
            var options = Options(builder).ToString();
            return string.IsNullOrWhiteSpace(options) ? query : $"{query}?{options}";
        }

        protected virtual OptionsBuilder Options(OptionsBuilder builder) => builder;
    }

    public class OptionsBuilder
    {
        private readonly List<string> _options = new();

        public OptionsBuilder Add<T>(string name, T value)
        {
            _options.Add($"{name}={value}");
            return this;
        }

        public OptionsBuilder Add(string name, bool value) =>
            Add(name, value.ToString().ToLowerInvariant());

        public override string ToString() => string.Join("&", _options);
    }
}