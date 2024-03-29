using Toggl.Track.SDK.Models;

namespace Toggl.Track.SDK.Queries
{
    public class UserQuery : Query<User>
    {
        private readonly bool _withRelatedData;

        public UserQuery() : this(false) { }

        public UserQuery(bool withRelatedData) : base("me")
        {
            _withRelatedData = withRelatedData;
        }

        public static readonly UserQuery WithRelatedData = new(true);
        public static readonly UserQuery WithoutRelatedData = new(false);

        protected override OptionsBuilder Options(OptionsBuilder builder) =>
            builder.Add("with_related_data", _withRelatedData);
    }
}
