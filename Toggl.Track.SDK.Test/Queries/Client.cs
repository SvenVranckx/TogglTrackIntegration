using FluentAssertions;
using Toggl.Track.SDK.Queries;

namespace Toggl.Track.SDK.Test.Queries
{
    public class ClientTests : IClassFixture<ContextFixture>
    {
        public ClientTests(ContextFixture fixture) { Context = fixture.Context; }

        protected ApiContext Context { get; }

        [Fact]
        public async Task GetClients()
        {
            var clients = await Context.Clients.Collect();
            Assert.NotNull(clients);
            clients.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetClientsByName()
        {
            var workspace = (await Context.Workspaces.Collect()).First();
            var clients = await Context.Clients.Collect(ClientQuery.ByWorkspace(workspace) | ClientQuery.Name("TEQ"));
            Assert.NotNull(clients);
            clients.Should().NotBeEmpty();
            clients.Should().OnlyContain(c => c.Name != null && c.Name.Contains("TEQ", StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public async Task GetActiveClients()
        {
            var workspace = (await Context.Workspaces.Collect()).First();
            var clients = await Context.Clients.Collect(ClientQuery.ByWorkspace(workspace) | ClientQuery.Active);
            Assert.NotNull(clients);
            clients.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetArchivedClients()
        {
            var workspace = (await Context.Workspaces.Collect()).First();
            var clients = await Context.Clients.Collect(ClientQuery.ByWorkspace(workspace) | ClientQuery.Archived);
            Assert.NotNull(clients);
            clients.Should().BeEmpty();
        }

        [Fact]
        public async Task GetActiveOrArchivedClients()
        {
            var workspace = (await Context.Workspaces.Collect()).First();
            var clients = await Context.Clients.Collect(ClientQuery.ByWorkspace(workspace) | ClientQuery.Active | ClientQuery.Archived);
            Assert.NotNull(clients);
            clients.Should().NotBeEmpty();
        }
    }
}