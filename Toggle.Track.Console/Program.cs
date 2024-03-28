using Microsoft.Extensions.Configuration;
using Toggle.Track.SDK;

namespace Toggle.Track
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var token = GetApiToken();
            using var context = new Context(token!);

            var organizations = await context.Organizations.Get();
            Console.Write(organizations.Length);

            var workspaces = await context.Workspaces.Get();
            Console.Write(workspaces.Length);

            var entries = await context.TimeEntries.Get();
            Console.Write(entries.Length);
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
