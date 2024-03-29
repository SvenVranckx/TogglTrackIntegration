using FluentAssertions;

namespace Toggl.Track.SDK.Test.Queries
{
    public class OrganizationTests : IClassFixture<ContextFixture>
    {
        public OrganizationTests(ContextFixture fixture) { Context = fixture.Context; }

        protected ApiContext Context { get; }

        [Fact]
        public async Task GetOrganizations()
        {
            var organizations = await Context.Organizations.Get();
            Assert.NotNull(organizations);
            organizations.Should().NotBeEmpty();
        }
    }
}
