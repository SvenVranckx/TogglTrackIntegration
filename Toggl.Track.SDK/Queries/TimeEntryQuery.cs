using Toggl.Track.SDK.Models;

namespace Toggl.Track.SDK.Queries
{
    public class TimeEntryQuery : Query<TimeEntry>
    {
        private readonly bool _current;
        private readonly bool _meta;
        private readonly bool _includeSharing;

        public TimeEntryQuery() : this(false, false, false) { }

        public TimeEntryQuery(bool current, bool withMetaEntities, bool includeSharing) : base("me/time_entries")
        {
            _current = current;
            _meta = withMetaEntities;
            _includeSharing = includeSharing;
        }

        public static readonly TimeEntryQuery Current = new(true, false, false);
        public static readonly TimeEntryQuery WithMetaEntities = new(false, true, false);
        public static readonly TimeEntryQuery IncludeSharing = new(false, false, true);

        public static TimeEntryQuery operator |(TimeEntryQuery left, TimeEntryQuery right) =>
            new(left._current || right._current,
                left._meta || right._meta,
                left._includeSharing || right._includeSharing);

        protected override OptionsBuilder Options(OptionsBuilder builder) => builder
            .Add("meta", _meta)
            .Add("include_sharing", _includeSharing);

        protected override string Build()
        {
            if (_current)
                return ApplyOptions($"{Path}/current");
            return ApplyOptions(Path);
        }
    }
}
