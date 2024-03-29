using FluentAssertions;
using Toggl.Track.SDK.Queries;

namespace Toggl.Track.SDK.Test.Queries
{
    public class TimeEntryTests : IClassFixture<ContextFixture>
    {
        public TimeEntryTests(ContextFixture fixture) { Context = fixture.Context; }

        protected ApiContext Context { get; }

        [Fact]
        public async Task GetEntries()
        {
            var entries = await Context.TimeEntries.Collect();
            Assert.NotNull(entries);
        }

        [Fact]
        public async Task GetEntriesWithMetaAndSharing()
        {
            var entries = await Context.TimeEntries.Collect(TimeEntryQuery.WithMetaEntities | TimeEntryQuery.IncludeSharing);
            Assert.NotNull(entries);
            entries.Should().NotBeEmpty();
        }
    }
}
