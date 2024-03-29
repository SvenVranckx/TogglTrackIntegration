﻿using Microsoft.Extensions.Configuration;
using Toggl.Track.SDK;

namespace Toggl.Track.Interactive
{
    public class Program
    {
        public static async Task Main()
        {
            var token = GetApiToken();
            using var context = new ApiContext(token!);

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