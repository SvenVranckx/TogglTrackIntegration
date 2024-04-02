using Microsoft.Extensions.Configuration;

namespace Toggl.Track.Interactive
{
    public class Program
    {
        public static async Task Main()
        {
            var token = GetApiToken();
            using var session = new Session(token);
            await session.Run();
        }

        private static string GetApiToken()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();
            var token = configuration["ApiToken"];
            return token;
        }
    }
}
