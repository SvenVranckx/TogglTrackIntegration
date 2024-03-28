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

        public override string? ToString() => Name ?? base.ToString();
    }

    public class ProtectedEntity : NamedEntity
    {
        [JsonPropertyName("permissions")]
        public string? Permissions { get; set; }
    }

    public class ConfigurationEntity : ProtectedEntity
    {
        [JsonPropertyName("server_deleted_at")]
        public DateTimeOffset? ServerDeletedAt { get; set; }
    }
}
