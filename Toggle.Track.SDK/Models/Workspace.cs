using System.Text.Json.Serialization;

namespace Toggle.Track.SDK.Models
{
    public class Workspace
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}
