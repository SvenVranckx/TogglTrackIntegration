using Microsoft.Extensions.Configuration;

namespace Toggl.Track.SDK.Test
{
    internal class Secrets
    {
        private static readonly IConfigurationRoot _configuration =
            new ConfigurationBuilder()
                .AddUserSecrets<Secrets>()
                .Build();

        public static string ApiToken => _configuration["ApiToken"];
    }
}
