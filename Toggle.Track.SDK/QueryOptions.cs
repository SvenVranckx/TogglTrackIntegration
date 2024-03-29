namespace Toggle.Track.SDK
{
    public interface IQueryOptions
    {
        string ApplyTo(string query);
        string ApplyTo(string query, long id);
    }

    internal static class QueryOptions
    {
        public static readonly DefaultOptions Default = new();

        public static string Apply(IQueryOptions? options, string query) =>
            options?.ApplyTo(query) ?? Default.ApplyTo(query);

        public static string Apply(IQueryOptions? options, string query, long id) =>
            options?.ApplyTo(query, id) ?? Default.ApplyTo(query, id);

        public static string Format(string name, bool value) =>
            $"{name}={value.ToString().ToLowerInvariant()}";
    }

    public class DefaultOptions : IQueryOptions
    {
        public string ApplyTo(string query) => query;
        public string ApplyTo(string query, long id) => $"{query}/{id}";
    }
}
