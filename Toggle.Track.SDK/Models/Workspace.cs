using System.Text.Json.Serialization;

namespace Toggle.Track.SDK.Models
{
    public class Workspace : ConfigurationEntity
    {
        [JsonPropertyName("organization_id")]
        public long OrganizationId { get; set; }

        [JsonPropertyName("profile")]
        public long Profile { get; set; }

        [JsonPropertyName("premium")]
        public bool Premium { get; set; }

        [JsonPropertyName("business_ws")]
        public bool BusinessWorkspace { get; set; }

        [JsonPropertyName("admin")]
        public bool Administrator { get; set; }

        [JsonPropertyName("role")]
        public string? Role { get; set; }

        [JsonPropertyName("default_hourly_rate")]
        public decimal? DefaultHourlyRate { get; set; }

        [JsonPropertyName("rate_last_updated")]
        public DateTimeOffset? RateLastUpdated { get; set; }

        [JsonPropertyName("default_currency")]
        public string? DefaultCurrency { get; set; }

        [JsonPropertyName("only_admins_may_create_projects")]
        public bool OnlyAdminsMayCreateProjects { get; set; }
        
        [JsonPropertyName("only_admins_may_create_tags")]
        public bool OnlyAdminsMayCreateTags { get; set; }
        
        [JsonPropertyName("only_admins_see_billable_rates")]
        public bool OnlyAdminsSeeBillableRates { get; set; }
        
        [JsonPropertyName("only_admins_see_team_dashboard")]
        public bool OnlyAdminsSeeTeamDashboard { get; set; }
        
        [JsonPropertyName("projects_billable_by_default")]
        public bool ProjectsBillableByDefault { get; set; }
        
        [JsonPropertyName("projects_private_by_default")]
        public bool ProjectsPrivateByDefault { get; set; }

        [JsonPropertyName("last_modified")]
        public DateTimeOffset? LastModified { get; set; }

        [JsonPropertyName("reports_collapse")]
        public bool ReportsCollapse { get; set; }

        [JsonPropertyName("rounding")]
        public int Rounding { get; set; }

        [JsonPropertyName("rounding_minutes")]
        public int RoundingMinutes { get; set; }

        [JsonPropertyName("api_token")]
        public string? ApiToken { get; set; }

        [JsonPropertyName("logo_url")]
        public string? LogoUrl { get; set; }
    }
}
