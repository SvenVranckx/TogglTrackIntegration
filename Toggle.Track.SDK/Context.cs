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
            TimeEntries = new Repository<TimeEntry>(_client, "me/time_entries");
        }

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
