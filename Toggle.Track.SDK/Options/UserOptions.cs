namespace Toggle.Track.SDK.Options
{
    public class UserOptions : IQueryOptions
    {
        private readonly bool _withRelatedData;

        public UserOptions(bool withRelatedData) => _withRelatedData = withRelatedData;

        public static readonly UserOptions WithRelatedData = new(true);
        public static readonly UserOptions WithoutRelatedData = new(false);

        string IQueryOptions.ApplyTo(string query) =>
            $"{query}?{QueryOptions.Format("with_related_data", _withRelatedData)}";

        string IQueryOptions.ApplyTo(string query, long id) =>
            $"{query}/{id}?{QueryOptions.Format("with_related_data", _withRelatedData)}";
    }
}
