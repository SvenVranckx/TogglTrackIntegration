using FluentAssertions;
using Toggl.Track.SDK.Queries;

namespace Toggl.Track.SDK.Test.Queries
{
    public class ProjectTests : IClassFixture<ContextFixture>
    {
        public ProjectTests(ContextFixture fixture) { Context = fixture.Context; }

        protected ApiContext Context { get; }

        [Fact]
        public async Task GetProjects()
        {
            var projects = await Context.Projects.Collect();
            Assert.NotNull(projects);
            projects.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetProjectsByWorkspace()
        {
            var workspace = (await Context.Workspaces.Collect()).First();
            var projects = await Context.Projects.Collect(ProjectQuery.ByWorkspace(workspace));
            Assert.NotNull(projects);
            projects.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetProjectsByClient()
        {
            var client = (await Context.Clients.Collect()).First();
            var projects = await Context.Projects.Collect(ProjectQuery.ByClient(client));
            Assert.NotNull(projects);
            projects.Should().NotBeEmpty();
            projects.Should().OnlyContain(p => p.ClientId == client.Id);
        }

        [Fact]
        public async Task GetProjectsByClients()
        {
            var workspace = (await Context.Workspaces.Collect()).First();
            var clients = (await  Context.Clients.Collect()).Where(c => c.WorkspaceId == workspace.Id).ToArray();
            var clientIds = new HashSet<long>(clients.Select(c => c.Id));
            var projects = await Context.Projects.Collect(ProjectQuery.ByClients(workspace, clients));
            Assert.NotNull(projects);
            projects.Should().NotBeEmpty();
            projects.Should().OnlyContain(p => clientIds.Contains(p.ClientId));
        }
    }
}
