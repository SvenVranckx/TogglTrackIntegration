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

        [Fact]
        public async Task GetCurrentEntry()
        {
            var entry = await Context.TimeEntries.Get(TimeEntryQuery.Current);
            if (entry is not null)
            {
                entry.Description.Should().NotBeNullOrEmpty();
                entry.WorkspaceId.Should().NotBe(0);
            }
        }

        [Fact]
        public async Task GetEntryById()
        {
            var entries = await Context.TimeEntries.Collect();
            var first = entries.FirstOrDefault();
            Assert.NotNull(first);
            var id = first.Id;
            id.Should().NotBe(0);

            var entry = await Context.TimeEntries.Get(id);
            Assert.NotNull(entry);
            entry.Id.Should().Be(id);
            entry.Description.Should().NotBeNullOrEmpty();
            entry.WorkspaceId.Should().NotBe(0);
        }

        [Fact]
        public async Task GetEntriesLastMonth()
        {            
            var entries = await Context.TimeEntries.Collect(TimeEntryQuery.LastMonth);
            Assert.NotNull(entries);
        }

        [Fact]
        public async Task GetEntriesThisMonth()
        {
            var entries = await Context.TimeEntries.Collect(TimeEntryQuery.ThisMonth);
            Assert.NotNull(entries);
            entries.Should().NotBeEmpty();
        }
    }
}
