using Toggl.Track.SDK.Models;
using Toggl.Track.SDK.Options;
using Task = Toggl.Track.SDK.Models.Task;

namespace Toggl.Track.SDK
{
    public class ApiContext : IDisposable
    {
        private readonly ApiClient _client;
        private bool _disposed;

        public ApiContext(string apiToken)
        {
            _client = new ApiClient(apiToken);
            Me = new Singleton<User, UserOptions>(_client, "me");
            Preferences = new Singleton<Preferences, DefaultOptions>(_client, "me/preferences");
            Organizations = new Repository<Organization, DefaultOptions>(_client, "me/organizations");
            Workspaces = new Repository<Workspace, DefaultOptions>(_client, "workspaces");
            Clients = new Repository<Client, DefaultOptions>(_client, "me/clients");
            Projects = new Repository<Project, DefaultOptions>(_client, "me/projects");
            Tasks = new Repository<Task, DefaultOptions>(_client, "me/tasks");
            Tags = new Repository<Tag, DefaultOptions>(_client, "me/tags");
            TimeEntries = new Repository<TimeEntry, TimeEntryOptions>(_client, "me/time_entries");
        }

        public ISingleton<User, UserOptions> Me { get; }
        public ISingleton<Preferences, DefaultOptions> Preferences { get; }

        public IRepository<Organization, DefaultOptions> Organizations { get; }
        public IRepository<Workspace, DefaultOptions> Workspaces { get; }
        public IRepository<Client, DefaultOptions> Clients { get; }
        public IRepository<Project, DefaultOptions> Projects { get; }
        public IRepository<Task, DefaultOptions> Tasks { get; }
        public IRepository<Tag, DefaultOptions> Tags { get; }
        public IRepository<TimeEntry, TimeEntryOptions> TimeEntries { get; }

        public void Dispose()
        {
            if (_disposed)
                return;
            _disposed = true;
            _client.Dispose();
        }
    }
}
