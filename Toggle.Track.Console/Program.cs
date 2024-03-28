﻿using Microsoft.Extensions.Configuration;
using Toggle.Track.SDK;

namespace Toggle.Track
{
    public class Program
    {
        public static async Task Main()
        {
            var token = GetApiToken();
            using var context = new ApiContext(token!);

            var me = await context.Me.Get(expand: true);
            Console.WriteLine(me?.FullName);

            var preferences = await context.Preferences.Get();
            Console.WriteLine(preferences?.PgTimeZoneName);

            var organizations = await context.Organizations.Get();
            Console.WriteLine(organizations.Length);

            var workspaces = await context.Workspaces.Get();
            Console.WriteLine(workspaces.Length);

            var clients = await context.Clients.Get();
            Console.WriteLine(clients.Length);

            var projects = await context.Projects.Get();
            Console.WriteLine(projects.Length);

            var tags = await context.Tags.Get();
            Console.WriteLine(tags.Length);

            var entries = await context.TimeEntries.Get();
            Console.WriteLine(entries.Length);
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
