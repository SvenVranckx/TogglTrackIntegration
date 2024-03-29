namespace Toggl.Track.SDK.Test
{
    public class ContextFixture : IDisposable
    {
        private bool _disposed;

        public ContextFixture()
        {
            Context = new ApiContext(Secrets.ApiToken);
        }

        public void Dispose()
        {
            if (_disposed)
                return;
            _disposed = true;
            Context.Dispose();
        }

        public ApiContext Context { get; }
    }
}
