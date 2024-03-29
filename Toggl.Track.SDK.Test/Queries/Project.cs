using FluentAssertions;

namespace Toggl.Track.SDK.Test.Queries
{
    public class ProjectTests : IClassFixture<ContextFixture>
    {
        public ProjectTests(ContextFixture fixture) { Context = fixture.Context; }

        protected ApiContext Context { get; }

        [Fact]
        public async Task GetProjects()
        {
            var projects = await Context.Projects.Get();
            Assert.NotNull(projects);
            projects.Should().NotBeEmpty();
        }
    }
}
