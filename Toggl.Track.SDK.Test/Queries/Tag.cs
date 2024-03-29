using FluentAssertions;

namespace Toggl.Track.SDK.Test.Queries
{
    public class TagTests : IClassFixture<ContextFixture>
    {
        public TagTests(ContextFixture fixture) { Context = fixture.Context; }

        protected ApiContext Context { get; }

        [Fact]
        public async Task GetTags()
        {
            var tags = await Context.Tags.Get();
            Assert.NotNull(tags);
            tags.Should().NotBeEmpty();
        }
    }
}
