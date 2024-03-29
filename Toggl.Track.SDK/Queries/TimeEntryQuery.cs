using Toggl.Track.SDK.Models;

namespace Toggl.Track.SDK.Queries
{
    public class TimeEntryQuery : Query<TimeEntry>
    {
        private readonly bool _meta;
        private readonly bool _includeSharing;

        public TimeEntryQuery() : this(false, false) { }

        public TimeEntryQuery(bool withMetaEntities, bool includeSharing) : base("me/time_entries")
        {
            _meta = withMetaEntities;
            _includeSharing = includeSharing;
        }

        public static readonly TimeEntryQuery WithMetaEntities = new(true, false);
        public static readonly TimeEntryQuery IncludeSharing = new(false, true);

        public static TimeEntryQuery operator |(TimeEntryQuery left, TimeEntryQuery right) =>
            new(left._meta || right._meta, left._includeSharing || right._includeSharing);

        protected override OptionsBuilder Options(OptionsBuilder builder) => builder
            .Add("meta", _meta)
            .Add("include_sharing", _includeSharing);
    }
}
