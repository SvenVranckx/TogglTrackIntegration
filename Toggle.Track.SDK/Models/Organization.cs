using System.Text.Json.Serialization;

namespace Toggle.Track.SDK.Models
{
    public class Organization : ConfigurationEntity
    {
        [JsonPropertyName("pricing_plan_id")]
        public long PricingPlanId { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonPropertyName("is_multi_workspace_enabled")]
        public bool IsMultiWorkspaceEnabled { get; set; }

        [JsonPropertyName("user_count")]
        public int UserCount { get; set; }

        [JsonPropertyName("trial_info")]
        public TrialInfo? TrialInfo { get; set; }

        [JsonPropertyName("is_unified")]
        public bool IsUnified { get; set; }

        [JsonPropertyName("max_workspaces")]
        public int MaxWorkspaces { get; set; }

        [JsonPropertyName("admin")]
        public bool Administrator { get; set; }

        [JsonPropertyName("owner")]
        public bool Owner { get; set; }
    }

    public class TrialInfo
    {
        [JsonPropertyName("trial")]
        public bool Trial { get; set; }

        [JsonPropertyName("trial_available")]
        public bool TrialAvailable { get; set; }

        [JsonPropertyName("trial_end_date")]
        public DateTimeOffset? TrialEndDate { get; set; }

        [JsonPropertyName("next_payment_date")]
        public DateTimeOffset? NextPaymentDate { get; set; }

        [JsonPropertyName("last_pricing_plan_id")]
        public long? LastPricingPlanId { get; set; }

        [JsonPropertyName("can_have_trial")]
        public bool CanHaveTrial { get; set; }
    }
}
