namespace Toggl.Track.SDK.Test
{
    public class ContextTests : IDisposable
    {
        private bool _disposed;

        public ContextTests()
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

        protected ApiContext Context { get; }
    }
}
