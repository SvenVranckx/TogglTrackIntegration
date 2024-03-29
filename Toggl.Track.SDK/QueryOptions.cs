namespace Toggl.Track.SDK
{
    public interface IQueryOptions
    {
        string ApplyTo(string query);
        string ApplyTo(string query, long id);
    }

    internal static class QueryOptions
    {
        public static readonly IQueryOptions Default = new DefaultOptions();

        public static string Apply(IQueryOptions? options, string query) =>
            options?.ApplyTo(query) ?? Default.ApplyTo(query);

        public static string Apply(IQueryOptions? options, string query, long id) =>
            options?.ApplyTo(query, id) ?? Default.ApplyTo(query, id);
    }

    public class DefaultOptions : IQueryOptions
    {
        string IQueryOptions.ApplyTo(string query) => query;
        string IQueryOptions.ApplyTo(string query, long id) => $"{query}/{id}";
    }

    public abstract class QueryOptionsImpl : IQueryOptions
    {
        string IQueryOptions.ApplyTo(string query) => ApplyOptions(query);
        string IQueryOptions.ApplyTo(string query, long id) => ApplyOptions($"{query}/{id}");

        protected abstract OptionsBuilder Build(OptionsBuilder builder);

        private string ApplyOptions(string query)
        {
            var builder = new OptionsBuilder();
            var options = Build(builder).ToString();
            return string.IsNullOrWhiteSpace(options) ? query : $"{query}?{options}";
        }
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