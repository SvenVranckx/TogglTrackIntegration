using Toggl.Track.SDK.Models;
using Toggl.Track.SDK.Queries;
using Task = Toggl.Track.SDK.Models.Task;

namespace Toggl.Track.SDK
{
    public class ApiContext : IDisposable
    {
        private readonly IClient _client;
        private bool _disposed;

        public ApiContext(string apiToken) : this(new ApiClient(apiToken))
        {
        }

        public ApiContext(IClient client)
        {
            _client = client;
            Me = Singleton.Create<User, UserQuery>(_client);
            Preferences = Singleton.Create<Preferences>(_client, "me/preferences");
            Organizations = Repository.Create<Organization>(_client, "me/organizations");
            Workspaces = Repository.Create<Workspace>(_client, "workspaces");
            Clients = Repository.Create<Client>(_client, "me/clients");
            Projects = Repository.Create<Project>(_client, "me/projects");
            Tasks = Repository.Create<Task>(_client, "me/tasks");
            Tags = Repository.Create<Tag>(_client, "me/tags");
            TimeEntries = Repository.Create<TimeEntry, TimeEntryQuery>(_client);
        }

        public ISingleton<User, UserQuery> Me { get; }
        public ISingleton<Preferences> Preferences { get; }

        public IRepository<Organization> Organizations { get; }
        public IRepository<Workspace> Workspaces { get; }
        public IRepository<Client> Clients { get; }
        public IRepository<Project> Projects { get; }
        public IRepository<Task> Tasks { get; }
        public IRepository<Tag> Tags { get; }
        public IRepository<TimeEntry, TimeEntryQuery> TimeEntries { get; }

        public void Dispose()
        {
            if (_disposed)
                return;
            _disposed = true;
            _client.Dispose();
        }
    }
}
