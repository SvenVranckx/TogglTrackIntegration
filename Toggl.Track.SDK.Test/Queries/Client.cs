using FluentAssertions;

namespace Toggl.Track.SDK.Test.Queries
{
    public class ClientTests : IClassFixture<ContextFixture>
    {
        public ClientTests(ContextFixture fixture) { Context = fixture.Context; }

        protected ApiContext Context { get; }

        [Fact]
        public async Task GetClients()
        {
            var clients = await Context.Clients.Get();
            Assert.NotNull(clients);
            clients.Should().NotBeEmpty();
        }
    }
}