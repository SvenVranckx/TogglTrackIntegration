namespace Toggle.Track.SDK.Options
{
    public class TimeEntryOptions : QueryOptionsImpl
    {
        private readonly bool _meta;
        private readonly bool _includeSharing;

        public TimeEntryOptions(bool withMetaEntities, bool includeSharing)
        {
            _meta = withMetaEntities;
            _includeSharing = includeSharing;
        }

        public static readonly TimeEntryOptions WithMetaEntities = new(true, false);
        public static readonly TimeEntryOptions IncludeSharing = new(false, true);

        public static TimeEntryOptions operator |(TimeEntryOptions left, TimeEntryOptions right) =>
            new(left._meta || right._meta, left._includeSharing || right._includeSharing);

        protected override OptionsBuilder Build(OptionsBuilder builder) => builder
            .Add("meta", _meta)
            .Add("include_sharing", _includeSharing);
    }
}
