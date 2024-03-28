using Toggle.Track.SDK.Models;

namespace Toggle.Track.SDK
{
    public class Context : IDisposable
    {
        private readonly Client _client;
        private bool _disposed;

        public Context(string apiToken)
        {
            _client = new Client(apiToken);
            Organizations = new Repository<Organization>(_client, "me/organizations");
            Workspaces = new Repository<Workspace>(_client, "workspaces");
            TimeEntries = new Repository<TimeEntry>(_client, "me/time_entries");
        }

        public Repository<Organization> Organizations { get; }
        public Repository<Workspace> Workspaces { get; }
        public Repository<TimeEntry> TimeEntries { get; }

        public void Dispose()
        {
            if (_disposed)
                return;
            _disposed = true;
            _client.Dispose();
        }
    }
}
