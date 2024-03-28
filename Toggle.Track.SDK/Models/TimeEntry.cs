using System.Text.Json.Serialization;

namespace Toggle.Track.SDK.Models
{
    public class TimeEntry : Entity
    {
        [JsonPropertyName("workspace_id")]
        public long? WorkspaceId { get; set; }

        [JsonPropertyName("project_id")]
        public long? ProjectId { get; set; }

        [JsonPropertyName("task_id")]
        public long? TaskId { get; set; }

        [JsonPropertyName("billable")]
        public bool Billable { get; set; }

        [JsonPropertyName("start")]
        public DateTimeOffset? Start { get; set; }

        [JsonPropertyName("stop")]
        public DateTimeOffset? Stop { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("tags")]
        public string[]? Tags { get; set; }

        [JsonPropertyName("tag_ids")]
        public long[]? TagIds { get; set; }

        [JsonPropertyName("duronly")]
        public bool DurationOnly { get; set; }

        [JsonPropertyName("server_deleted_at")]
        public DateTimeOffset? ServerDeletedAt { get; set; }

        [JsonPropertyName("user_id")]
        public long UserId { get; set; }
    }
}
