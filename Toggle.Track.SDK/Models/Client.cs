using System.Text.Json.Serialization;

namespace Toggle.Track.SDK.Models
{
    public class Client : ConfigurationEntity
    {
        [JsonPropertyName("archived")]
        public bool Archived { get; set; } 

        [JsonPropertyName("creator_id")]
        public long CreatorId { get; set; }

        [JsonPropertyName("wid")]
        public long WorkspaceId { get; set; }
    }
}
