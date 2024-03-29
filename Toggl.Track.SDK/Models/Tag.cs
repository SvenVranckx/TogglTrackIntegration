using System.Text.Json.Serialization;

namespace Toggl.Track.SDK.Models
{
    public class Tag : ProtectedEntity
    {
        [JsonPropertyName("creator_id")]
        public long CreatorId { get; set; }

        [JsonPropertyName("deleted_at")]
        public DateTimeOffset? DeletedAt { get; set; }        

        [JsonPropertyName("workspace_id")]
        public long WorkspaceId { get; set; }
    }
}
