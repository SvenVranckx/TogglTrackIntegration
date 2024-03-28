#pragma warning disable CS8603 // Possible null reference return.

using System.Text.Json.Serialization;

namespace Toggle.Track.SDK.Models
{
    public class Entity
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("at")]
        public DateTimeOffset? At { get; set; }
    }

    public class NamedEntity : Entity
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        public override string ToString() => Name ?? base.ToString();
    }

    public class ConfigurationEntity : NamedEntity
    {
        [JsonPropertyName("suspended_at")]
        public DateTimeOffset? SuspendedAt { get; set; }

        [JsonPropertyName("server_deleted_at")]
        public DateTimeOffset? ServerDeletedAt { get; set; }
    }
}
