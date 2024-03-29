using System.Text.Json.Serialization;

namespace Toggl.Track.SDK.Models
{
    public class Task : ConfigurationEntity
    {
        [JsonPropertyName("active")]
        public bool Active { get; set; }

        [JsonPropertyName("estimated_seconds")]
        public int? EstimatedSeconds { get; set; }

        [JsonPropertyName("project_id")]
        public long ProjectId { get; set; }

        [JsonPropertyName("recurring")]
        public bool Recurring { get; set; }

        [JsonPropertyName("toggl_accounts_id")]
        public string? TogglAccountsId { get; set; }

        [JsonPropertyName("tracked_seconds")]
        public long TrackedMilliseconds { get; set; }

        [JsonPropertyName("user_id")]
        public long? UserId { get; set; }

        [JsonPropertyName("workspace_id")]
        public long WorkspaceId { get; set; }
    }
}
