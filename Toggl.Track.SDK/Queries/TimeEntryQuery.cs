using Toggl.Track.SDK.Models;

namespace Toggl.Track.SDK.Queries
{
    public class TimeEntryQuery : Query<TimeEntry>
    {
        private readonly bool _current;
        private readonly bool _meta;
        private readonly bool _includeSharing;
        private readonly DateTimeOffset? _since;
        private readonly DateTimeOffset? _before;

        public TimeEntryQuery() : this(false, false, false, null, null)
        { }

        public TimeEntryQuery(bool current = false, bool withMetaEntities = false, bool includeSharing = false, DateTimeOffset? since = null, DateTimeOffset? before = null) :
            base("me/time_entries")
        {
            _current = current;
            _meta = withMetaEntities;
            _includeSharing = includeSharing;
            _since = since;
            _before = before;
        }

        public static readonly TimeEntryQuery Current = new(current: true);
        public static readonly TimeEntryQuery WithMetaEntities = new(withMetaEntities: true);
        public static readonly TimeEntryQuery IncludeSharing = new(includeSharing: true);

        public static TimeEntryQuery LastMonth => PreviousMonth(DateTime.Today);
        public static TimeEntryQuery ThisMonth => CurrentMonth(DateTime.Today);
        public static TimeEntryQuery LastWeek => PreviousWeek(DateTime.Today);
        public static TimeEntryQuery ThisWeek => CurrentWeek(DateTime.Today);

        public static TimeEntryQuery Since(DateTimeOffset since) => new(since: since);
        public static TimeEntryQuery Before(DateTimeOffset before) => new(before: before);
        public static TimeEntryQuery Between(DateTimeOffset start, DateTimeOffset end) => new(since: start, before: end);

        public static TimeEntryQuery PreviousMonth(DateTime today)
        {
            var firstDayOfThisMonth = new DateTime(today.Year, today.Month, 1);
            var firstDayOfPreviousMonth = firstDayOfThisMonth.AddMonths(-1);
            return new(since: firstDayOfPreviousMonth, before: firstDayOfThisMonth.AddSeconds(-1.0));
        }

        public static TimeEntryQuery CurrentMonth(DateTime today)
        {
            var firstDayOfThisMonth = new DateTime(today.Year, today.Month, 1);
            var firstDayOfNextMonth = firstDayOfThisMonth.AddMonths(1);
            return new(since: firstDayOfThisMonth, before: firstDayOfNextMonth.AddSeconds(-1.0));
        }

        public static TimeEntryQuery PreviousWeek(DateTime today)
        {
            var firstDayOfWeek = DayOfWeek.Monday;
            var start = StartOfWeek(today, firstDayOfWeek).AddDays(-7);
            var end = EndOfWeek(today, firstDayOfWeek).AddDays(-7);
            return new(since: start, before: end);
        }

        public static TimeEntryQuery CurrentWeek(DateTime today)
        {
            var firstDayOfWeek = DayOfWeek.Monday;
            var start = StartOfWeek(today, firstDayOfWeek);
            var end = EndOfWeek(today, firstDayOfWeek);
            return new(since: start, before: end);
        }

        private static DateTimeOffset StartOfWeek(DateTimeOffset time, DayOfWeek firstDayOfWeek)
        {
            int offset = (7 + (time.DayOfWeek - firstDayOfWeek)) % 7;
            return time.Date.AddDays(-1 * offset);
        }

        private static DateTimeOffset EndOfWeek(DateTimeOffset time, DayOfWeek firstDayOfWeek) => StartOfWeek(time, firstDayOfWeek).AddDays(7);

        public static TimeEntryQuery operator |(TimeEntryQuery left, TimeEntryQuery right) =>
            new(left._current || right._current,
                left._meta || right._meta,
                left._includeSharing || right._includeSharing,
                left._since ?? right._since,
                left._before ?? right._before);

        protected override OptionsBuilder Options(OptionsBuilder builder)
        {
            builder
                .Add("meta", _meta)
                .Add("include_sharing", _includeSharing);
            if (_since is not null || _before is not null)
            {
                var since = _since ?? DateTimeOffset.MinValue;
                var before = _before ?? DateTimeOffset.MaxValue;
                var minimum = DateTimeOffset.Now.AddMonths(-3);
                if (since < minimum)
                    since = minimum;
                builder
                    .Add("start_date", RFC3339(since.ToUniversalTime()))
                    .Add("end_date", RFC3339(before.ToUniversalTime()));
            }
            return builder;
        }

        private static string RFC3339(DateTimeOffset time) => time.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.00Z");

        protected override string Build()
        {
            if (_current)
                return ApplyOptions($"{Path}/current");
            return ApplyOptions(Path);
        }
    }
}
