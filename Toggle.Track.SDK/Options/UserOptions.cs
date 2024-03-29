namespace Toggle.Track.SDK.Options
{
    public class UserOptions : QueryOptionsImpl
    {
        private readonly bool _withRelatedData;

        public UserOptions(bool withRelatedData) => _withRelatedData = withRelatedData;

        public static readonly UserOptions WithRelatedData = new(true);
        public static readonly UserOptions WithoutRelatedData = new(false);

        protected override OptionsBuilder Build(OptionsBuilder builder) =>
            builder.Add("with_related_data", _withRelatedData);
    }
}
