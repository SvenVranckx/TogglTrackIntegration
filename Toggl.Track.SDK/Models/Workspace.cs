using System.Text.Json.Serialization;

namespace Toggl.Track.SDK.Models
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

        [JsonPropertyName("ical_url")]
        public string? ICalUrl { get; set; }

        [JsonPropertyName("ical_enabled")]
        public bool ICalEnabled { get; set; }

        [JsonPropertyName("csv_upload")]
        public CsvUpload? CsvUpload { get; set; }

        [JsonPropertyName("subscription")]
        public Subscription? Subscription { get; set; }

        [JsonPropertyName("suspended_at")]
        public DateTimeOffset? SuspendedAt { get; set; }

        [JsonPropertyName("hide_start_end_times")]
        public bool HideStartEndTimes { get; set; }

        [JsonPropertyName("working_hours_in_minutes")]
        public int? WorkingHoursInMinutes { get; set; }
    }

    public class CsvUpload
    {
        [JsonPropertyName("at")]
        public DateTimeOffset? At { get; set; }

        [JsonPropertyName("log_id")]
        public long LogId { get; set; }
    }

    public class Subscription
    {
        [JsonPropertyName("auto_renew")]
        public bool AutoRenew { get; set; }

        [JsonPropertyName("card_details")]
        public CardDetails? CardDetails { get; set; }

        [JsonPropertyName("company_id")]
        public long CompanyId { get; set; }

        [JsonPropertyName("contact_detail")]
        public ContactDetail? ContactDetail { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("customer_id")]
        public long CustomerId { get; set; }

        [JsonPropertyName("deleted_at")]
        public DateTimeOffset? DeletedAt { get; set; }

        [JsonPropertyName("last_pricing_plan_id")]
        public long LastPricingPlanId { get; set; }

        [JsonPropertyName("organization_id")]
        public long OrganizationId { get; set; }

        [JsonPropertyName("payment_details")]
        public PaymentDetail? PaymentDetails { get; set; }

        [JsonPropertyName("pricing_plan_id")]
        public long PricingPlanId { get; set; }

        [JsonPropertyName("renewal_at")]
        public DateTimeOffset? RenewalAt { get; set; }

        [JsonPropertyName("subscription_id")]
        public long SubscriptionId { get; set; }

        [JsonPropertyName("subscription_period")]
        public Period? SubscriptionPeriod { get; set; }

        [JsonPropertyName("workspace_id")]
        public long WorkspaceId { get; set; }
    }

    public class CardDetails { }
    public class ContactDetail { }
    public class PaymentDetail { }
}
