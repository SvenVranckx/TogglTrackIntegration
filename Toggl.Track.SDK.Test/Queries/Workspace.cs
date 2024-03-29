using FluentAssertions;

namespace Toggl.Track.SDK.Test.Queries
{
    public class WorkspaceTests : IClassFixture<ContextFixture>
    {
        public WorkspaceTests(ContextFixture fixture) { Context = fixture.Context; }

        protected ApiContext Context { get; }

        [Fact]
        public async Task GetWorkspaces()
        {
            var workspaces = await Context.Workspaces.Get();
            Assert.NotNull(workspaces);
            workspaces.Should().NotBeEmpty();
        }
    }
}
