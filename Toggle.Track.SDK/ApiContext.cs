using Toggle.Track.SDK.Models;
using Task = Toggle.Track.SDK.Models.Task;

namespace Toggle.Track.SDK
{
    public class ApiContext : IDisposable
    {
        private readonly ApiClient _client;
        private bool _disposed;

        public ApiContext(string apiToken)
        {
            _client = new ApiClient(apiToken);
            Me = new Singleton<User>(_client, "me", "with_related_data");
            Preferences = new Singleton<Preferences>(_client, "me/preferences", null);
            Organizations = new Repository<Organization>(_client, "me/organizations");
            Workspaces = new Repository<Workspace>(_client, "workspaces");
            Clients = new Repository<Client>(_client, "me/clients");
            Projects = new Repository<Project>(_client, "me/projects");
            Tasks = new Repository<Task>(_client, "me/tasks");
            Tags = new Repository<Tag>(_client, "me/tags");
            TimeEntries = new Repository<TimeEntry>(_client, "me/time_entries");
        }

        public ISingleton<User> Me { get; }
        public ISingleton<Preferences> Preferences { get; }

        public IRepository<Organization> Organizations { get; }
        public IRepository<Workspace> Workspaces { get; }
        public IRepository<Client> Clients { get; }
        public IRepository<Project> Projects { get; }
        public IRepository<Task> Tasks { get; }
        public IRepository<Tag> Tags { get; }
        public IRepository<TimeEntry> TimeEntries { get; }

        public void Dispose()
        {
            if (_disposed)
                return;
            _disposed = true;
            _client.Dispose();
        }
    }
}
