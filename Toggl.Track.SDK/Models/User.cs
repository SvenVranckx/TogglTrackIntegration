using System.Text.Json.Serialization;

namespace Toggl.Track.SDK.Models
{
    public class User : Entity
    {
        [JsonPropertyName("api_token")]
        public string? ApiToken { get; set; }

        [JsonPropertyName("beginning_of_week")]
        public int BeginningOfWeek { get; set; }

        [JsonPropertyName("clients")]
        public Client[]? Clients { get; set; }

        [JsonPropertyName("country_id")]
        public long CountryId { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonPropertyName("default_workspace_id")]
        public long DefaultWorkspaceId { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("fullname")]
        public string? FullName { get; set; }

        [JsonPropertyName("has_password")]
        public bool HasPassword { get; set; }

        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }

        [JsonPropertyName("openid_email")]
        public string? OpenIdEmail { get; set; }

        [JsonPropertyName("openid_enabled")]
        public bool OpenIdEnabled { get; set; }

        [JsonPropertyName("projects")]
        public Project[]? Projects { get; set; }

        [JsonPropertyName("tags")]
        public Tag[]? Tags { get; set; }

        [JsonPropertyName("tasks")]
        public ProjectTask[]? Tasks { get; set; }

        [JsonPropertyName("time_entries")]
        public TimeEntry[]? TimeEntries { get; set; }

        [JsonPropertyName("timezone")]
        public string? TimeZone { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonPropertyName("workspaces")]
        public Workspace[]? Workspaces { get; set; }

        public override string? ToString() => FullName ?? base.ToString();
    }
}
