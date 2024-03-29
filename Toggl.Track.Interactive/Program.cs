using Microsoft.Extensions.Configuration;
using Toggl.Track.SDK;
using Toggl.Track.SDK.Queries;

namespace Toggl.Track.Interactive
{
    public class Program
    {
        public static async Task Main()
        {
            var token = GetApiToken();
            using var context = new ApiContext(token!);

            var client = await context.Clients.Single(ClientQuery.Name("iNNOVATEQ"));
            var projects = (await context.Projects.Collect(ProjectQuery.ByClient(client))).ToDictionary(p => p.Id);

            var entries = await context.TimeEntries.Collect(TimeEntryQuery.ThisMonth);
            var matching = entries
                .Where(e => projects.ContainsKey(e.ProjectId ?? 0))
                .ToArray();

            await Task.CompletedTask;
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
