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
            GC.SuppressFinalize(this);
        }

        public ApiContext Context { get; }
    }
}
